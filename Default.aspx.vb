Imports System.Net
Imports System.Net.WebRequestMethods
Imports BiblioNet.BookModels
Imports BiblioNet.BookModels.Books
Imports BiblioNet.JsScript.JsHelper
Imports BiblioNet.UsersModel
Imports BiblioNet.UsersModel.CurrentUser
Imports Microsoft.Ajax.Utilities
Imports Microsoft.AspNet.Web.Optimization.WebForms
Imports MyTools
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            InitPage()
        End If

    End Sub

    Protected Sub BtnApply_Click(sender As Object, e As EventArgs) Handles BtnApply.Click
        Dim query As String = TxtSearchBook.Text.Trim()
        InitRepeater(query)
    End Sub


    Protected Sub BtnImportBook_Click(sender As Object, e As EventArgs) Handles BtnImportBook.Click

        Dim maxPagine As Integer = 3
        Dim categoria = TxtCategories.Text
        Dim q = New Database.MySql()
        Dim client As New WebClient()
        Dim count = 0
        Dim list As New List(Of String)()

        Response.Clear()
        Response.BufferOutput = True
        Response.ContentType = "text/html"

        'Svuoto la tabella e poi inserisco

        'q.Reset()

        q.DeleteFrom("booksopenlibrary").ExecuteNonQuery()

        For i As Integer = 1 To maxPagine
            Try
                Dim url As String = $"https://openlibrary.org/search.json?q={categoria}&page={i}"
                Dim json As String = client.DownloadString(url)
                Dim jObj As JObject = JObject.Parse(json)

                If jObj("docs") IsNot Nothing AndAlso jObj("docs").HasValues Then
                    Dim dt As DataTable = JsonConvert.DeserializeObject(Of DataTable)(jObj("docs").ToString())

                    For Each row As DataRow In dt.Rows
                        Dim title As String = If(Not IsDBNull(row("title")), row("title").ToString().Replace("'", "''"), "Titolo Sconosciuto")
                        Dim subtitle As String = If(Not IsDBNull(row("subtitle")), row("subtitle").ToString().Replace("'", "''"), "")
                        Dim coverEditionKey As String = If(Not IsDBNull(row("cover_edition_key")), row("cover_edition_key").ToString(), "")
                        Dim cover_i As String = If(Not IsDBNull(row("cover_i")), row("cover_i").ToString(), "NULL")
                        Dim ebookAccess As String = If(Not IsDBNull(row("ebook_access")), row("ebook_access").ToString(), "")
                        Dim editionCount As String = If(Not IsDBNull(row("edition_count")), row("edition_count").ToString(), "NULL")
                        Dim firstPublishYear As String = If(Not IsDBNull(row("first_publish_year")), row("first_publish_year").ToString(), "NULL")
                        Dim hasFullText As String = If(Not IsDBNull(row("has_fulltext")) AndAlso CBool(row("has_fulltext")), "1", "0")

                        Dim ia As String = If(Not IsDBNull(row("ia")),
                             JsonConvert.SerializeObject(row("ia")).Replace("'", "''"),
                             "[]")

                        Dim iaCollection As String = If(Not IsDBNull(row("ia_collection_s")), row("ia_collection_s").ToString().Replace("'", "''"), "")
                        Dim keyOpenLibrary As String = If(Not IsDBNull(row("key")), row("key").ToString(), "")


                        Dim language As String = If(Not IsDBNull(row("language")),
                             JsonConvert.SerializeObject(row("language")).Replace("'", "''"),
                             "[]")

                        Dim lendingEdition As String = If(Not IsDBNull(row("lending_edition_s")), row("lending_edition_s").ToString(), "")
                        Dim lendingIdentifier As String = If(Not IsDBNull(row("lending_identifier_s")), row("lending_identifier_s").ToString(), "")
                        Dim publicScan As String = If(Not IsDBNull(row("public_scan_b")) AndAlso CBool(row("public_scan_b")), "1", "0")

                        Dim author_key As String = If(Not IsDBNull(row("author_key")),
                             JsonConvert.SerializeObject(row("author_key")).Replace("'", "''"),
                             "[]")
                        Dim author_name As String = If(Not IsDBNull(row("author_name")),
                               JsonConvert.SerializeObject(row("author_name")).Replace("'", "''"),
                               "[]")

                        Dim rowValues As String = $"('{title}', '{subtitle}', '{coverEditionKey}', {cover_i}, '{ebookAccess}', {editionCount}, {firstPublishYear}, {hasFullText}, '{ia}', '{iaCollection}', '{keyOpenLibrary}', '{language}', '{lendingEdition}', '{lendingIdentifier}', {publicScan}, '{author_key}', '{author_name}')"
                        list.Add(rowValues)
                        count = count + list.Count
                    Next

                    ' Inserisci nel DB dopo aver 
                    q.Reset()
                    Dim columnDb = "title,subtitle,cover_edition_key,cover_i,ebook_access,edition_count,first_publish_year,has_fulltext,ia,ia_collection_s,key_openlibrary,language,lending_edition_s,lending_identifier_s,public_scan_b,author_key,author_name"
                    q.InsertMultipleRows("booksopenlibrary", columnDb, list).ExecuteNonQuery()

                    Response.Write($"<div style='padding:4px; font-family:Arial;'>Pagina {i} completata: inserite {list.Count} righe.</div>")
                    Response.Flush()
                Else
                    Response.Write($"<div style='color:red;'>Pagina {i} senza dati.</div>")
                End If

            Catch ex As Exception
                Response.Write($"<div style='color:red;'>Errore inatteso nella pagina {i}: {ex.Message}</div>")
                Exit For
            End Try
        Next

        Response.Write($"<div style='color:green;'>Importazione completata. Inserite {count} righe</div>")
        Response.Flush()
    End Sub


    'Importa i dati alla tabella Author
    Protected Sub BtnAddAuthor_Click(sender As Object, e As EventArgs) Handles BtnAddAuthor.Click

        Dim q = New Database.MySql()

        'Prendo i valori dalla booksopenlibrary
        q.Reset()
        Dim dt = q.Select("*").From("booksopenlibrary").ToDataTable()
        Dim listValueAuthor As New List(Of String)()

        'Recupero gli autori
        For Each row As DataRow In dt.Rows
            Dim authorKeys As JArray = JArray.Parse(row("author_key").ToString())
            Dim authorNames As JArray = JArray.Parse(row("author_name").ToString())

            For i As Integer = 0 To authorKeys.Count - 1
                ' Creiamo una riga singola per ogni autore
                Dim key As String = authorKeys(i).ToString()
                Dim name As String = authorNames(i).ToString()

                ' Escape eventuali apostrofi nel nome
                name = name.Replace("'", "''")

                Dim rowValues As String = $"('{key}', '{name}', '')"
                listValueAuthor.Add(rowValues)
            Next
        Next

        'For Each row As DataRow In dt.Rows

        '    Dim authorKeys As JArray = JArray.Parse(row("author_key").ToString())
        '    Dim authorNames As JArray = JArray.Parse(row("author_name").ToString())

        '    For i As Integer = 0 To authorKeys.Count - 1

        '        Dim rowValues As String = $"('{authorKeys}', '{authorNames}', '')"
        '        listValueAuthor.Add(rowValues)

        '    Next


        'Next

        Try
            Dim columnDb = "author_id,name,biography"
            q.InsertMultipleRows("authors", columnDb, listValueAuthor).ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception("Errore" & q.Build())
        End Try



        'Dim sql As String = "INSERT INTO biblionet.authors (author_id, name) " &
        '            "SELECT author_key, author_name " &
        '            "FROM biblionet.booksopenlibrary;"

        'q.Sql(sql).ExecuteSql()

    End Sub

    Protected Sub InitPage()

        Dim q As New Database.MySql()

        If CurrentUser.IsAdmin Then
            PnlImport.Visible = True
        Else
            PnlImport.Visible = False
        End If

        InitRepeater()

    End Sub


    Protected Sub InitRepeater(Optional titolo As String = Nothing)

        Dim q = New Database.MySql()

        Dim listaLibri = Nothing

        If titolo <> "" Then
            listaLibri = q.Select("*").From("books_personal").WhereLike("title", titolo).ToList(Of Books)()


        Else
            listaLibri = q.Select("*").From("books_personal").ToList(Of Books)()

        End If


        ' Associo la lista al Repeater
        RptExchange.DataSource = listaLibri
        RptExchange.DataBind()

    End Sub


    Protected Sub BtnOpenDialogBook_Click(sender As Object, e As EventArgs)

        'AddJScript("openDialog(""#dialog-scambio"", ""/AddBook.aspx"", 'Aggiungi Libro', '1200', '500');")

    End Sub




End Class