Imports System.Net
Imports System.Web.Script.Serialization
Imports BiblioNet.BookModels
Imports BiblioNet.JsScript.JsHelper
Imports BiblioNet.LogScript
Imports MyTools
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class BiblionetImport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            InitPage()
        End If
    End Sub


    Protected Sub InitPage()
    End Sub


#Region "Import"

    'Importa i dati dalla OpenLibrary alla tabella BooksPersonal con tutte le categorie

    Protected Sub BtnImportBooks_Click(sender As Object, e As EventArgs) Handles BtnImportBooks.Click

        Dim categories() As String = {
            "Mystery & Detective Fiction",
            "Science Fiction",
            "Fantasy",
            "Romance",
            "Horror",
            "Thriller",
            "Historical Fiction",
            "Biography",
            "Autobiography",
            "Self Help",
            "Politics",
            "Science",
            "Philosophy",
            "Religion",
            "Poetry",
            "Drama",
            "Adventure",
            "Children's Literature",
            "Young Adult",
            "Graphic Novels",
            "Travel",
            "Cookbooks",
            "Art & Photography",
            "Music",
            "Health & Fitness"
        }

        For Each categoria As String In categories

            ImportBooksByCategories(categoria)
        Next
    End Sub

    Public Sub ImportBooksByCategories(categoria As String)

        Dim maxPagesAllowed As Integer = 50 ' limite di sicurezza
        Dim pageSize As Integer = 100       ' default API Open Library
        Dim q = New Database.MySql()
        Dim count As Integer = 0

        Response.Clear()
        Response.BufferOutput = True
        Response.ContentType = "text/html"

        ' Stampo all'inizio la categoria
        Response.Write($"<div style='color:blue; font-weight:bold;'>Inizio importazione categoria: '{categoria}'</div>")
        Response.Flush()

        Dim i As Integer = 1
        Dim continueImport As Boolean = True

        While continueImport AndAlso i <= maxPagesAllowed
            Dim retryCount As Integer = 0
            Dim success As Boolean = False

            While Not success AndAlso retryCount < 3
                Try
                    Dim url As String = $"https://openlibrary.org/search.json?q={categoria}&page={i}"

                    Using client As New WebClient()
                        client.Headers.Add("User-Agent", "BiblioNet/1.0 (domenicoemanuele.giannone@gmail.com)")

                        Dim json As String = client.DownloadString(url)
                        Dim jObj As JObject = JObject.Parse(json)

                        ' Controllo se ci sono risultati
                        If jObj("numFound") IsNot Nothing AndAlso CInt(jObj("numFound")) = 0 Then
                            Response.Write($"<div style='color:orange;'>Categoria '{categoria}' non trovata o senza risultati.</div>")
                            Response.Flush()
                            Return
                        End If

                        ' Se ci sono dati
                        If jObj("docs") IsNot Nothing AndAlso jObj("docs").HasValues Then
                            Dim dt As DataTable = JsonConvert.DeserializeObject(Of DataTable)(jObj("docs").ToString())
                            If dt.Rows.Count = 0 Then
                                continueImport = False
                                Exit While
                            End If

                            Dim list As New List(Of String)()

                            For Each row As DataRow In dt.Rows
                                Dim title As String = SafeValue(row, "title", "Titolo Sconosciuto")
                                Dim subtitle As String = SafeValue(row, "subtitle")
                                Dim coverEditionKey As String = SafeValue(row, "cover_edition_key")
                                Dim cover_i As String = SafeValue(row, "cover_i", "NULL")
                                Dim ebookAccess As String = SafeValue(row, "ebook_access")
                                Dim editionCount As String = SafeValue(row, "edition_count", "NULL")
                                Dim firstPublishYear As String = SafeValue(row, "first_publish_year", "NULL")
                                Dim hasFullText As String = If(row.Table.Columns.Contains("has_fulltext") AndAlso Not IsDBNull(row("has_fulltext")) AndAlso CBool(row("has_fulltext")), "1", "0")
                                Dim ia As String = SafeJson(row, "ia")
                                Dim iaCollection As String = SafeValue(row, "ia_collection_s")
                                Dim keyOpenLibrary As String = SafeValue(row, "key")
                                Dim language As String = SafeJson(row, "language")
                                Dim lendingEdition As String = SafeValue(row, "lending_edition_s")
                                Dim lendingIdentifier As String = SafeValue(row, "lending_identifier_s")
                                Dim publicScan As String = If(row.Table.Columns.Contains("public_scan_b") AndAlso Not IsDBNull(row("public_scan_b")) AndAlso CBool(row("public_scan_b")), "1", "0")
                                Dim author_key As String = SafeJson(row, "author_key")
                                Dim author_name As String = SafeJson(row, "author_name")

                                Dim rowValues As String = $"('{title}', '{subtitle}', '{coverEditionKey}', {cover_i}, '{ebookAccess}', {editionCount}, {firstPublishYear}, {hasFullText}, '{ia}', '{iaCollection}', '{keyOpenLibrary}', '{language}', '{lendingEdition}', '{lendingIdentifier}', {publicScan}, '{author_key}', '{author_name}', '{categoria}')"
                                list.Add(rowValues)
                            Next

                            ' Inserisco nel DB
                            q.Reset()
                            Dim columnDb = "title,subtitle,cover_edition_key,cover_i,ebook_access,edition_count,first_publish_year,has_fulltext,ia,ia_collection_s,key_openlibrary,language,lending_edition_s,lending_identifier_s,public_scan_b,author_key,author_name,category"
                            q.InsertMultipleRows("booksopenlibrary", columnDb, list).ExecuteNonQuery()

                            Dim rowsThisPage As Integer = list.Count
                            count += rowsThisPage
                            Response.Write($"<div style='padding:4px; font-family:Arial;'>Pagina {i} completata: inserite {rowsThisPage} righe.</div>")
                            Response.Flush()
                        Else
                            Response.Write($"<div style='color:red;'>Pagina {i} senza dati.</div>")
                            Response.Flush()
                            continueImport = False
                            Exit While
                        End If
                    End Using

                    success = True ' pagina completata
                    i += 1
                    Threading.Thread.Sleep(300) ' pausa tra le pagine

                Catch ex As WebException
                    retryCount += 1
                    If retryCount >= 3 Then
                        Response.Write($"<div style='color:red;'>Errore pagina {i} dopo {retryCount} tentativi: {ex.Message}. Salto pagina.</div>")
                        Response.Flush()
                        i += 1
                        success = True
                    Else
                        Response.Write($"<div style='color:orange;'>Errore pagina {i}: {ex.Message}. Riprovo ({retryCount}/3)...</div>")
                        Response.Flush()
                        Threading.Thread.Sleep(500)
                    End If
                Catch ex As Exception
                    Response.Write($"<div style='color:red;'>Errore inatteso pagina {i}: {ex.Message}</div>")
                    Response.Flush()
                    i += 1
                    success = True
                End Try
            End While
        End While

        ' Stampo riepilogo finale
        Response.Write($"<div style='color:green;'>Importazione completata per categoria '{categoria}'. Inserite {count} righe.</div>")
        Response.Flush()
    End Sub


    'Importa i dati dalla OpenLibrary alla tabella BooksOpenLibrary con una categoria specifica
    Protected Sub BtnImportBook_Click(sender As Object, e As EventArgs) Handles BtnImportBook.Click

        Dim maxPagine As Integer = 50
        Dim categoria = TxtCategories.Text
        Dim q = New Database.MySql()
        Dim client As New WebClient()
        client.Headers.Add("User-Agent", "BiblioNet/1.0 (domenicoemanuele.giannone@gmail.com)")

        Dim count = 0
        Dim list As New List(Of String)()

        Response.Clear()
        Response.BufferOutput = True
        Response.ContentType = "text/html"

        'Svuoto la tabella e poi inserisco

        q.DeleteFrom("booksopenlibrary").ExecuteNonQuery()

        For i As Integer = 1 To maxPagine
            Try

                'Mi prendo l'url
                Dim url As String = $"https://openlibrary.org/search.json?q={categoria}&page={i}"

                'Con web client instanzio l'oggetto client e scarico la stringa
                Dim json As String = client.DownloadString(url)

                'Prendo il json e lo metto dentro una stringa jobj
                Dim jObj As JObject = JObject.Parse(json)

                'Se il campo docs del json eisiste e ha dei valori
                If jObj("docs") IsNot Nothing AndAlso jObj("docs").HasValues Then


                    'Ottengo una datatable dal json
                    Dim dt As DataTable = JsonConvert.DeserializeObject(Of DataTable)(jObj("docs").ToString())


                    'Ciclo tutto
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

                        'Metto i valori in una riga
                        Dim rowValues As String = $"('{title}', '{subtitle}', '{coverEditionKey}', {cover_i}, '{ebookAccess}', {editionCount}, {firstPublishYear}, {hasFullText}, '{ia}', '{iaCollection}', '{keyOpenLibrary}', '{language}', '{lendingEdition}', '{lendingIdentifier}', {publicScan}, '{author_key}', '{author_name}')"

                        'Li inserisco nella lista
                        list.Add(rowValues)

                        count = count + dt.Rows.Count
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

    'Aggiunge il campo DateJSON alla tabella BooksOpenLibrary 
    Protected Sub BtnAddJsonDate_Click(sender As Object, e As EventArgs) Handles BtnAddJsonDate.Click

        'Prendo tutti i record dalla tabella BooksOpenLibrary
        Dim q = New Database.MySql()
        Dim dt = q.Select("*").From("booksopenlibrary").ToDataTable()

        Dim client As New WebClient()
        client.Encoding = System.Text.Encoding.UTF8
        client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)")

        'Creo una lista che può contenere 100 cover_edition_key
        Dim conver_edition_key_list As New List(Of String)(100)
        Dim count As Integer = 0

        For i As Integer = 0 To dt.Rows.Count - 1

            'Ad ogni ciclo conto i record inseriti
            q.Reset()
            Dim recordInseriti = q.Select("Count(*) AS Total").From("booksopenlibrary").Where("DateJSON IS NOT NULL").ToObj(Of Integer)


            Dim dr = dt.Rows(i)

            If IsDBNull(dr("DateJSON")) OrElse dr("DateJSON").ToString() = "" Then
                conver_edition_key_list.Add(dr("cover_edition_key").ToString())

                ' Chiama API ogni 100 record o alla fine
                If conver_edition_key_list.Count = 100 OrElse i = dt.Rows.Count - 1 Then

                    Dim editionUrl As String =
                "https://openlibrary.org/api/books?format=json&jscmd=data&bibkeys=" &
                String.Join(",", conver_edition_key_list)

                    Dim editionJson As String = client.DownloadString(editionUrl)
                    Dim editionObj As JObject = JObject.Parse(editionJson)
                    Dim updatedCount = 0

                    For Each k As String In conver_edition_key_list
                        If editionObj(k) IsNot Nothing Then
                            Dim jsonVal = editionObj(k).ToString().Replace("'", "''")
                            q.Reset()
                            q.Update("booksopenlibrary",
                             $"DateJSON='{jsonVal}'") _
                     .Where("cover_edition_key", "=", $"'{k.Replace("'", "''")}'") _
                     .ExecuteNonQuery()

                            updatedCount += 1
                            Response.Write($"<div>Record aggiornato per {k}</div>")
                            Response.Flush()
                        End If
                    Next

                    'Sommo i record aggiornati
                    count += updatedCount

                    Response.Write($"<div style='color:green;'>Aggiornamento completato. Record aggiornati: {count} su {dt.Rows.Count}</div>")
                    Response.Write($"<div style='color:green;'>Record totali aggiornati in tabella: {recordInseriti} </div>")
                    conver_edition_key_list.Clear()
                End If
            Else
                Continue For
            End If

        Next
    End Sub

    Protected Sub BtnAddOnOpenLibraryEditionDetails_Click(sender As Object, e As EventArgs) Handles BtnAddOnOpenLibraryEditionDetails.Click

        Response.Clear()
        Response.BufferOutput = False ' IMPORTANTISSIMO
        Response.ContentType = "text/html"
        Response.Charset = "utf-8"

        Response.Write("<div style='font-family:Arial;'>🚀 Avvio import dettagli OpenLibrary...</div>")
        Response.Flush()



        Dim q = New Database.MySql()

        Dim dt = q.Select("*").From("booksopenlibrary").ToDataTable()

        Dim listValues As New List(Of String)()

        Dim totRows As Integer = 0
        Dim okRows As Integer = 0
        Dim errorRows As Integer = 0


        Dim batchSize As Integer = 100
        Dim batchValues As New List(Of String)
        Dim columnDb = "id_op,ol_key,ol_url,title,subtitle,by_statement,number_of_pages,pagination,publish_date,cover_small,cover_medium,cover_large,authors_name,author_url,identifiers,classifications,publishers,subjects,links_url,links_title,source,created_at,updated_at"

        q.Reset()
        For Each row As DataRow In dt.Rows

            totRows += 1

            Response.Write(
                    $"<div style='font-family:Arial; font-size:12px;'>🔄 Elaborazione record {totRows}</div>"
                )

            Response.Flush()

            Dim dataObj As Object = row("DateJSON")

            If dataObj IsNot DBNull.Value Then

                Dim DataJSON As String = dataObj.ToString()

                If DataJSON <> "" Then

                    Dim bookEdition As OpenLibraryEditionDetails = Nothing
                    Try
                        bookEdition = JsonConvert.DeserializeObject(Of OpenLibraryEditionDetails)(DataJSON)
                    Catch ex As Exception
                        errorRows += 1
                        'Scrivo il log di errore 
                        'LogHelper.WriteLog($"Errore deserializzazione JSON : {DataJSON}: {ex.Message}", "/Log/LogImport.txt")

                        Response.Write(
                            $"<div style='color:red; font-family:Arial; font-size:12px;'>" &
                            $"❌ Errore record {totRows}: {ex.Message}</div>"
                        )
                        Response.Flush()
                        Continue For
                    End Try

                    Dim id_op = row("id")

                    'Prendo i vari campi (con controlli)
                    Dim url As String = If(bookEdition.Url IsNot Nothing,
                       bookEdition.Url.Replace("'", "''"),
                       "")

                    Dim key As String = If(bookEdition.Key IsNot Nothing,
                       bookEdition.Key.Replace("'", "''"),
                       "")

                    Dim title As String = If(bookEdition.Title IsNot Nothing AndAlso bookEdition.Title.Trim() <> "",
                         bookEdition.Title.Replace("'", "''"),
                         "Titolo sconosciuto")



                    Dim nameAuthor As String = ""
                    Dim urlAuthor As String = ""

                    If bookEdition.Authors IsNot Nothing Then
                        nameAuthor = String.Join(",", bookEdition.Authors.
                    Where(Function(s) s IsNot Nothing AndAlso s.Name IsNot Nothing).
                    Select(Function(s) s.Name.Replace("'", "''")))

                        urlAuthor = String.Join(",", bookEdition.Authors.
                    Where(Function(s) s IsNot Nothing AndAlso s.Url IsNot Nothing).
                    Select(Function(s) s.Url.Replace("'", "''")))
                    End If

                    Dim number_of_pages = bookEdition.NumberOfPages
                    Dim publish_date = If(bookEdition.PublishDate Is Nothing, "", bookEdition.PublishDate.Replace("'", "''"))
                    Dim pagination = If(bookEdition.Pagination Is Nothing, "", bookEdition.Pagination.Replace("'", "''"))
                    Dim subtitle = If(bookEdition.Subtitle Is Nothing, "", bookEdition.Subtitle.Replace("'", "''"))
                    Dim weight = If(bookEdition.Weight Is Nothing, "", bookEdition.Weight.Replace("'", "''"))
                    Dim notes As String = If(bookEdition.Notes Is Nothing, "", bookEdition.Notes.Replace("'", "''"))

                    'Prendo tutti gli identificatori
                    Dim identifierList As List(Of String) = New List(Of String)()
                    If bookEdition.Identifiers.Amazon IsNot Nothing Then
                        identifierList.AddRange(bookEdition.Identifiers.Amazon)
                    End If
                    If bookEdition.Identifiers.Lccn IsNot Nothing Then
                        identifierList.AddRange(bookEdition.Identifiers.Lccn)
                    End If
                    If bookEdition.Identifiers.OpenLibrary IsNot Nothing Then
                        identifierList.AddRange(bookEdition.Identifiers.OpenLibrary)
                    End If
                    If bookEdition.Identifiers.Isbn10 IsNot Nothing Then
                        identifierList.AddRange(bookEdition.Identifiers.Isbn10)
                    End If
                    If bookEdition.Identifiers.Isbn13 IsNot Nothing Then
                        identifierList.AddRange(bookEdition.Identifiers.Isbn13)
                    End If

                    Dim identifiers = String.Join(",", identifierList)

                    Dim publishers As String = ""

                    If bookEdition.Publishers IsNot Nothing Then
                        publishers = String.Join(",", bookEdition.Publishers.Select(Function(p) p.Name.Replace("'", "''")))
                    End If

                    Dim by_statement = If(bookEdition.ByStatement Is Nothing, "", bookEdition.ByStatement)

                    Dim DeweyDecimalClass As String = ""
                    Dim LcClassifications As String = ""

                    If bookEdition.Classifications IsNot Nothing Then
                        If bookEdition.Classifications.DeweyDecimalClass IsNot Nothing Then
                            DeweyDecimalClass = String.Join(",", bookEdition.Classifications.DeweyDecimalClass)
                        End If

                        If bookEdition.Classifications.LcClassifications IsNot Nothing Then
                            LcClassifications = String.Join(",", bookEdition.Classifications.LcClassifications)
                        End If
                    End If


                    Dim subjectsName As String = ""
                    Dim subjectsUrl As String = ""

                    If bookEdition.Subjects IsNot Nothing Then
                        subjectsName = String.Join(",", bookEdition.Subjects.
                    Where(Function(s) s IsNot Nothing AndAlso s.Name IsNot Nothing).
                    Select(Function(s) s.Name.Replace("'", "''")))

                        subjectsUrl = String.Join(",", bookEdition.Subjects.
                    Where(Function(s) s IsNot Nothing AndAlso s.Url IsNot Nothing).
                    Select(Function(s) s.Url.Replace("'", "''")))
                    End If

                    Dim subject_places_names As String = ""
                    Dim subject_places_urls As String = ""

                    If bookEdition.SubjectPlaces IsNot Nothing Then
                        subject_places_names = String.Join(",", bookEdition.SubjectPlaces.
                    Where(Function(s) s IsNot Nothing AndAlso s.Name IsNot Nothing).
                    Select(Function(s) s.Name.Replace("'", "''")))

                        subject_places_urls = String.Join(",", bookEdition.SubjectPlaces.
                    Where(Function(s) s IsNot Nothing AndAlso s.Url IsNot Nothing).
                    Select(Function(s) s.Url.Replace("'", "''")))
                    End If

                    Dim subject_times_names As String = ""
                    Dim subject_times_urls As String = ""

                    If bookEdition.subject_times IsNot Nothing Then
                        subject_times_names = String.Join(",", bookEdition.subject_times.
                    Where(Function(s) s IsNot Nothing AndAlso s.Name IsNot Nothing).
                    Select(Function(s) s.Name.Replace("'", "''")))

                        subject_times_urls = String.Join(",", bookEdition.subject_times.
                        Where(Function(s) s IsNot Nothing AndAlso s.Url IsNot Nothing).
                        Select(Function(s) s.Url.Replace("'", "''")))
                    End If

                    Dim subject_people_names As String = ""
                    Dim subject_people_urls As String = ""

                    If bookEdition.subject_people IsNot Nothing Then
                        subject_people_names = String.Join(",", bookEdition.subject_people.
                    Where(Function(s) s IsNot Nothing AndAlso s.Name IsNot Nothing).
                    Select(Function(s) s.Name.Replace("'", "''")))

                        subject_people_urls = String.Join(",", bookEdition.subject_people.
                    Where(Function(s) s IsNot Nothing AndAlso s.Url IsNot Nothing).
                    Select(Function(s) s.Url.Replace("'", "''")))
                    End If


                    Dim ebooksPreviewUrl As String = ""

                    If bookEdition.Ebooks IsNot Nothing Then
                        ebooksPreviewUrl = String.Join(",", bookEdition.Ebooks.Where(Function(b) b IsNot Nothing AndAlso b.PreviewUrl IsNot Nothing).Select(Function(b) b.PreviewUrl.Replace("'", "''")))
                    End If


                    Dim ebooksAvailability As String = ""

                    If bookEdition.Ebooks IsNot Nothing Then
                        ebooksAvailability = String.Join(",", bookEdition.Ebooks.Where(Function(a) e IsNot Nothing AndAlso a.Availability IsNot Nothing).Select(Function(a) a.Availability.Replace("'", "''")))
                    End If

                    Dim ebooksFormats As String = ""

                    If bookEdition.Ebooks IsNot Nothing Then
                        ebooksFormats = String.Join(" | ", bookEdition.Ebooks.Where(Function(f) f IsNot Nothing AndAlso f.Formats IsNot Nothing).Select(Function(f) JsonConvert.SerializeObject(f.Formats).Replace("'", "''")))
                    End If

                    Dim cover_large = If(bookEdition.Cover IsNot Nothing, bookEdition.Cover.Large, "")
                    Dim cover_medium = If(bookEdition.Cover IsNot Nothing, bookEdition.Cover.Medium, "")
                    Dim cover_small = If(bookEdition.Cover IsNot Nothing, bookEdition.Cover.Small, "")


                    Dim LinksUrl As String = ""

                    If bookEdition.Links IsNot Nothing Then
                        LinksUrl = String.Join(",", bookEdition.Links.Where(Function(l) l IsNot Nothing AndAlso l.Url IsNot Nothing).Select(Function(l) l.Url.Replace("'", "''")))
                    End If

                    Dim LinksTitle As String = ""
                    If bookEdition.Links IsNot Nothing Then
                        LinksTitle = String.Join(",", bookEdition.Links.Where(Function(l) l IsNot Nothing AndAlso l.Title IsNot Nothing).Select(Function(l) l.Title.Replace("'", "''")))
                    End If

                    Dim source = "OpenLibrary"

                    'Dim rowValues As String = $"({id_op}," &
                    '      $"('{key}'," &
                    '      $"'{url.Replace("'", "''")}'," &
                    '      $"'{title.Replace("'", "''")}'," &
                    '      $"'{subtitle.Replace("'", "''")}'," &
                    '      $"'{by_statement.Replace("'", "''")}'," &
                    '      $"{number_of_pages}," &
                    '      $"'{pagination.Replace("'", "''")}'," &
                    '      $"'{publish_date.Replace("'", "''")}'," &
                    '      $"'{cover_small.Replace("'", "''")}'," &
                    '      $"'{cover_medium.Replace("'", "''")}'," &
                    '      $"'{cover_large.Replace("'", "''")}'," &
                    '      $"'{nameAuthor.Replace("'", "''")}'," &
                    '      $"'{urlAuthor.Replace("'", "''")}'," &
                    '      $"'{identifiers.Replace("'", "''")}'," &
                    '      $"'{DeweyDecimalClass.Replace("'", "''")}|{LcClassifications.Replace("'", "''")}'," &
                    '      $"'{publishers.Replace("'", "''")}'," &
                    '      $"'{subjectsName.Replace("'", "''")}'," &
                    '      $"'{LinksUrl.Replace("'", "''")}'," &
                    '      $"'{LinksTitle.Replace("'", "''")}'," &
                    '      $"'{source}'," &
                    '      $"NOW()," &
                    '      $"NOW())"
                    Dim rowValues As String = $"({id_op}," &
                    $"'{EscapeMySQL(key)}'," &
                    $"'{EscapeMySQL(url)}'," &
                    $"'{EscapeMySQL(title)}'," &
                    $"'{EscapeMySQL(subtitle)}'," &
                    $"'{EscapeMySQL(by_statement)}'," &
                    $"{If(number_of_pages Is Nothing, "NULL", number_of_pages)}," &
                    $"'{EscapeMySQL(pagination)}'," &
                    $"'{EscapeMySQL(publish_date)}'," &
                    $"'{EscapeMySQL(cover_small)}'," &
                    $"'{EscapeMySQL(cover_medium)}'," &
                    $"'{EscapeMySQL(cover_large)}'," &
                    $"'{EscapeMySQL(nameAuthor)}'," &
                    $"'{EscapeMySQL(urlAuthor)}'," &
                    $"'{EscapeMySQL(identifiers)}'," &
                    $"'{EscapeMySQL(DeweyDecimalClass)}|{EscapeMySQL(LcClassifications)}'," &
                    $"'{EscapeMySQL(publishers)}'," &
                    $"'{EscapeMySQL(subjectsName)}'," &
                    $"'{EscapeMySQL(LinksUrl)}'," &
                    $"'{EscapeMySQL(LinksTitle)}'," &
                    $"'{EscapeMySQL(source)}'," &
                    $"NOW()," &
                    $"NOW())"




                    batchValues.Add(rowValues)
                    okRows += 1

                    Response.Write($"<div style='color:green; font-family:Arial; font-size:12px;'>✅ Record {totRows} pronto per inserimento (key: {key})</div>")
                    Response.Flush()

                    ' Inserisci ogni 100 record
                    q.Reset()
                    If batchValues.Count = batchSize Then
                        q.InsertMultipleRows("booksopenlibrary_details", columnDb, batchValues).ExecuteNonQuery()
                        batchValues.Clear()
                    End If

                End If
            Else
                Continue For
            End If

        Next

        Response.Write(
        $"<div style='color:green; font-family:Arial; font-size:14px; font-weight:bold;'>✔ Inserimento completato</div>" &
        $"<div style='font-family:Arial;'>Totali:</div>" &
        $"<ul style='font-family:Arial;'>" &
        $"<li>Letti: {totRows}</li>" &
        $"<li>Inseriti {okRows}</li>" &
        $"<li>Errori {errorRows}</li>" &
        $"</ul>"
    )
        Response.Flush()

    End Sub


    Protected Sub BtnAddressByOpenStreetMap_Click(sender As Object, e As EventArgs) Handles BtnAddressByOpenStreetMap.Click

        Dim q As New Database.MySql()
        Dim client As New WebClient()
        client.Encoding = System.Text.Encoding.UTF8
        client.Headers.Add("User-Agent", "Biblionet/1.0 (domenicoemanuele.giannone@gmail.com)")

        Dim dt As DataTable = q.Select("*").From("publishers").ToDataTable()
        Dim totalRows As Integer = dt.Rows.Count
        Dim updatedCount As Integer = 0
        Dim serializer As New JavaScriptSerializer()

        Response.Write($"<div>Inizio aggiornamento di {totalRows} publisher...</div>")
        Response.Flush()

        For i As Integer = 0 To totalRows - 1
            Dim row As DataRow = dt.Rows(i)
            Dim publisherName As String = row("name").ToString()
            Dim query As String = Uri.EscapeDataString(publisherName)

            Dim url As String = "https://nominatim.openstreetmap.org/search" &
                            "?q=" & query &
                            "&format=json" &
                            "&addressdetails=1" &
                            "&limit=1" &
                            "&countrycodes=us,gb,it,fr,de,es"

            Response.Write($"<div>Elaboro publisher {i + 1}/{totalRows}: {publisherName}...</div>")
            Response.Flush()

            ' Retry per gestire 403/429
            Dim maxRetry As Integer = 3
            For attempt As Integer = 1 To maxRetry
                Try
                    Dim json As String = client.DownloadString(url)

                    If String.IsNullOrEmpty(json) OrElse json.Trim() = "[]" Then
                        Response.Write($"<div style='color:orange;'>Nessun risultato per {publisherName}</div>")
                        Response.Flush()
                        Exit For
                    End If

                    Dim results As Object() = serializer.Deserialize(Of Object())(json)
                    If results.Length > 0 Then
                        Dim firstResult As Dictionary(Of String, Object) = results(0)
                        Dim address As String = If(firstResult.ContainsKey("display_name"), firstResult("display_name").ToString(), "")

                        If Not String.IsNullOrEmpty(address) Then
                            address = address.Replace("'", "''")
                            q.Reset()
                            q.Update("publishers", $"address='{address}'").Where("name", "=", $"'{publisherName.Replace("'", "''")}'").ExecuteNonQuery()
                            updatedCount += 1
                            Response.Write($"<div style='color:green;'>✓ Address aggiornato per {publisherName}</div>")
                            Response.Flush()
                        Else
                            Response.Write($"<div style='color:orange;'>Indirizzo vuoto per {publisherName}</div>")
                            Response.Flush()
                        End If
                    End If

                    Exit For ' esce dal retry se tutto ok

                Catch ex As WebException
                    Dim resp = TryCast(ex.Response, HttpWebResponse)
                    If resp IsNot Nothing Then
                        If resp.StatusCode = HttpStatusCode.Forbidden OrElse CInt(resp.StatusCode) = 429 Then
                            Response.Write($"<div style='color:red;'>Errore {resp.StatusCode} per {publisherName}, retry {attempt}/{maxRetry} dopo 10 sec</div>")
                            Response.Flush()
                            Threading.Thread.Sleep(10000) ' pausa lunga
                        Else
                            Response.Write($"<div style='color:red;'>Errore HTTP {resp.StatusCode} per {publisherName}: {ex.Message}</div>")
                            Response.Flush()
                            Exit For
                        End If

                        'If resp.StatusCode = HttpStatusCode.Forbidden Or resp.StatusCode = HttpStatusCode.TooManyRequests Then
                        '    Response.Write($"<div style='color:red;'>Errore {resp.StatusCode} per {publisherName}, retry {attempt}/{maxRetry} dopo 10 sec</div>")
                        '    Response.Flush()
                        '    Threading.Thread.Sleep(10000) ' pausa lunga
                        'Else
                        '    Response.Write($"<div style='color:red;'>Errore HTTP {resp.StatusCode} per {publisherName}: {ex.Message}</div>")
                        '    Response.Flush()
                        '    Exit For
                        'End If
                    Else
                        Response.Write($"<div style='color:red;'>Errore Web per {publisherName}: {ex.Message}</div>")
                        Response.Flush()
                        Exit For
                    End If
                End Try
            Next

            ' Pausa obbligatoria di 1 secondo tra le richieste
            Threading.Thread.Sleep(1000)
        Next

        Response.Write($"<div style='color:blue;font-weight:bold;'>Aggiornamento completato. Totale address aggiornati: {updatedCount} su {totalRows}</div>")
        Response.Flush()
    End Sub

    'Inserisce i nomi unici dei publisher nella tabella publishers
    Protected Sub BtnAddNamePublisher_click(sender As Object, e As EventArgs) Handles BtnAddNamePublisher.Click

        Dim q = New Database.MySql()


        q.Sql("DELETE FROM publishers;").ExecuteSql()

        Dim pubSql = "INSERT INTO publishers (name) " &
                    " SELECT DISTINCT publishers" &
                    " FROM booksopenlibrary_details" &
                    " WHERE publishers IS NOT NULL" &
                    " AND publishers <> '';"

        q.Sql(pubSql).ExecuteSql()

    End Sub

#End Region

#Region "Elaborazioni"
    Protected Sub BtnAddAuthor_Click(sender As Object, e As EventArgs) Handles BtnAddAuthor.Click

        Dim q = New Database.MySql()

        Dim sql As String = "INSERT IGNORE INTO biblionet.authors (author_id, Name, biography) " &
            "Select DISTINCT jk.author_key, jt.author_name, NULL " &
            "FROM biblionet.booksopenlibrary b, " &
            "JSON_TABLE(b.author_key, '$[*]' COLUMNS (author_key VARCHAR(50) PATH '$')) AS jk, " &
            "JSON_TABLE(b.author_name, '$[*]' COLUMNS (author_name VARCHAR(255) PATH '$')) AS jt " &
            "WHERE jt.author_name IS NOT NULL AND jk.author_key IS NOT NULL;"

        q.Sql(sql).ExecuteSql()

    End Sub

    'Rimuove i duplicati dalla tabella BooksOpenLibrary

    Protected Sub BtnDeleteDuplicates_Click(sender As Object, e As EventArgs) Handles BtnDeleteDuplicates.Click
        Dim q = New Database.MySql()



        Dim sql As String = "DELETE b1 FROM booksopenlibrary b1 " &
            "JOIN booksopenlibrary b2 " &
            "ON b1.title = b2.title " &
            "AND b1.author_name = b2.author_name " &
            "AND b1.first_publish_year = b2.first_publish_year " &
            "AND b1.cover_edition_key = b2.cover_edition_key " &
            "AND b1.id > b2.id;"

        Dim rowsAffected = q.Sql(sql).ExecuteSql()
        Response.Write($"<div style='color:green;'>Eliminati {rowsAffected} record duplicati.</div>")
        Response.Flush()

    End Sub


#End Region

#Region "Funzioni di supporto"

    Private Function EscapeMySQL(value As String) As String
        If String.IsNullOrEmpty(value) Then
            Return ""
        End If
        Return value.Replace("\", "\\").Replace("'", "''")
    End Function

    ' Funzione helper per valori semplici
    Private Function SafeValue(row As DataRow, columnName As String, Optional defaultValue As String = "") As String
        If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
            Return row(columnName).ToString().Replace("'", "''")
        Else
            Return defaultValue
        End If
    End Function

    ' Funzione helper per valori JSON
    Private Function SafeJson(row As DataRow, columnName As String) As String
        If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
            Return JsonConvert.SerializeObject(row(columnName)).Replace("'", "''")
        Else
            Return "[]"
        End If
    End Function


#End Region

End Class