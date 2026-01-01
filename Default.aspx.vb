Imports System.Net
Imports System.Net.WebRequestMethods
Imports System.Security.Cryptography
Imports BiblioNet.BookModels
Imports BiblioNet.BookModels.Books
Imports BiblioNet.JsScript.JsHelper
Imports BiblioNet.UsersModel
Imports BiblioNet.UsersModel.CurrentUser
Imports Microsoft.Ajax.Utilities
Imports MyTools
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String = Request.QueryString("action")
        Dim ids As String = Request.QueryString("ids")

        If action = "GetBooks" Then
            ' Ottieni il termine di ricerca dalla QueryString
            Dim searchTerm As String = Request.QueryString("q")
            Dim context As String = Request.QueryString("context")

            ' Passa il searchTerm alla funzione
            Dim jsonResult As String = GetBooks(searchTerm, context, ids)

            Response.Clear()
            Response.ContentType = "application/json"
            Response.Write(jsonResult)
            Response.End()
            Return
        End If


        If Not Page.IsPostBack Then
            InitPage()
        End If

    End Sub


#Region "Inizializzatori"
    Protected Sub InitPage()



        InitRepeater()

    End Sub


    Protected Sub InitRepeater(Optional titolo As String = Nothing)

        Dim q = New Database.MySql()

        Dim listaLibri = Nothing


        If Not String.IsNullOrWhiteSpace(titolo) Then
            listaLibri = q.Select("*").
        From("books_personal").
        WhereLike("title", titolo).
        ToList(Of Books)()
        Else
            listaLibri = q.Select("*").
        From("books_personal").
        ToList(Of Books)()
        End If


        'If titolo <> "" Then
        '    listaLibri = q.Select("*").From("books_personal").WhereLike("title", titolo).ToList(Of Books)()
        'Else
        '    listaLibri = q.Select("*").From("books_personal").ToList(Of Books)()
        'End If

        ' Associo la lista al Repeater
        RptExchange.DataSource = listaLibri
        RptExchange.DataBind()

    End Sub


#End Region

#Region "Eventi Pulsanti"
    Protected Sub BtnApply_Click(sender As Object, e As EventArgs) Handles BtnApply.Click
        Dim query As String = TxtSearchBook.Text.Trim()
        InitRepeater(query)
    End Sub



#End Region

#Region "Eventi Repeater"

    Protected Sub RptExchange_ItemCommand(sender As Object, e As RepeaterCommandEventArgs) Handles RptExchange.ItemCommand

        Select Case e.CommandName

            Case "Edit"
                Dim id As String = Convert.ToInt32(e.CommandArgument)
                ' Apri modal / redirect / carica dati
                EditBook(id)

            Case "Delete"
                Dim id As String = Convert.ToInt32(e.CommandArgument)
                DeleteBook(id)
                LoadRepeater() ' ricarica dati

        End Select

    End Sub

#End Region

#Region "Funzioni di supporto"
    Protected Sub EditBook(bookId As Integer)
        ' Logica per modificare il libro con l'ID specificato
        ' Ad esempio, puoi reindirizzare a una pagina di modifica o aprire un modal
        Response.Redirect($"EditBook.aspx?title={bookId}")
    End Sub
    Protected Sub DeleteBook(bookId As Integer)
        Dim q = New Database.MySql()
        q.DeleteFrom("books_personal").Where("id", "=", bookId).ExecuteNonQuery()
    End Sub
    Protected Sub LoadRepeater()
        InitRepeater()
    End Sub


    Protected Function GetBooks(searchTerm As String, context As String, Optional ids As String = Nothing) As String
        Dim q = New Database.MySql()
        Dim listaLibri As List(Of Books) = Nothing

        If ids IsNot Nothing Then
            listaLibri = GetBooksFromDinamicTableByIds(ids, "books_library")
        Else
            Select Case context
                Case "Scambio"
                Case "Catalogo"
                    listaLibri = GetBooksFromDinamicTable(searchTerm, "books_library")
                Case Else
                    listaLibri = GetBooksFromDinamicTable(searchTerm, "books_personal")
            End Select

        End If


        ' Se non ci sono risultati
        If listaLibri Is Nothing Then
            Return "{""results"":[]}"
        End If

        ' Crea direttamente la risposta nel formato Select2
        Dim response = New With {
        .results = listaLibri.
            Where(Function(l) l IsNot Nothing).
            Select(Function(libro) New With {
                .id = libro.Id,
                .text = libro.Title
            }).ToList()
    }


        Return Newtonsoft.Json.JsonConvert.SerializeObject(response)
    End Function

    Protected Function GetBooksFromDinamicTableByIds(ids As String, table As String) As List(Of Books)

        Dim q = New Database.MySql()
        Dim listaLibri As List(Of Books) = Nothing

        Dim idArray = ids.Split(","c).Select(Function(x) x.Trim()).ToArray()

        listaLibri = q.Select("*").
              From(table).
              Where("id", "IN", idArray.ToString).
              ToList(Of Books)()

        Return listaLibri

    End Function

    Protected Function GetBooksFromDinamicTable(searchTerm As String, table As String) As List(Of Books)

        Dim q = New Database.MySql()
        Dim listaLibri As List(Of Books) = Nothing

        ' Costruisci la query con filtro WHERE se c'è un searchTerm
        If Not String.IsNullOrEmpty(searchTerm) Then
            ' Usa Replace per gestire gli apostrofi nel searchTerm
            Dim safeSearchTerm As String = searchTerm.Replace("'", "''")

            ' Costruisci la query con i %
            Dim whereClause As String = String.Format("title LIKE '%{0}%' OR author_id LIKE '%{0}%'", safeSearchTerm)

            listaLibri = q.Select("*").
                From(table).
                Where(whereClause).
                ToList(Of Books)()
        Else
            ' Se non c'è ricerca, ritorna tutti i libri
            listaLibri = q.Select("*").
                From(table).
                OrderBy("Title").
                Limit("10").
                ToList(Of Books)()
        End If

        Return listaLibri
    End Function

    'Protected Function GetBooksFromDinamicTable(searchTerm As String, table As String) As List(Of Books)
    '    Dim q = New Database.MySql()
    '    Dim listaLibri As List(Of Books) = Nothing
    '    Dim query As String = "Title LIKE '%" & searchTerm & "%' OR AuthorID LIKE '%" & searchTerm & "%'"


    '    ' Costruisci la query con filtro WHERE se c'è un searchTerm
    '    If Not String.IsNullOrEmpty(searchTerm) Then
    '        ' Filtra per titolo o altri campi
    '        listaLibri = q.Select("*").
    '            From(table).
    '            Where(query).
    '            ToList(Of Books)()
    '    Else
    '        ' Se non c'è ricerca, ritorna tutti i libri (o un subset)
    '        listaLibri = q.Select("*").
    '            From(table).
    '            OrderBy("Title").
    '            ToList(Of Books)()
    '    End If

    '    Return listaLibri
    'End Function


#End Region


End Class