Imports BiblioNet.BookModels
Imports BiblioNet.JsScript.JsHelper
Imports MyTools

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then
            InitPage()


        End If

    End Sub


    Protected Sub InitPage()

        Dim q As New Database.MySql()

        InitRepeater()

    End Sub

    Protected Sub InitRepeater()

        'Possiamo farlo sia con una lista vedi sotto

        ' Dentro il tuo Page_Load o in un metodo
        Dim listaLibri As New List(Of Books) From {
    New Books With {
        .Id = 1,
        .Title = "Il Signore degli Anelli",
        .AuthorID = "AUTH001",
        .publisherID = "PUB001",
        .isbn = "978-8845292613",
        .stockQuantity = 10,
        .publicationhDate = New Date(1954, 7, 29),
        .categoryID = 1,
        .coverImageUrl = "images/lotr.jpg",
        .Genre = "Fantasy",
        .Price = 29.99
    },
    New Books With {
        .Id = 2,
        .Title = "1984",
        .AuthorID = "AUTH002",
        .publisherID = "PUB002",
        .isbn = "978-0451524935",
        .stockQuantity = 15,
        .publicationhDate = New Date(1949, 6, 8),
        .categoryID = 2,
        .coverImageUrl = "images/1984.jpg",
        .Genre = "Distopia",
        .Price = 19.99
    },
    New Books With {
        .Id = 3,
        .Title = "Il Nome della Rosa",
        .AuthorID = "AUTH003",
        .publisherID = "PUB003",
        .isbn = "978-8845242687",
        .stockQuantity = 8,
        .publicationhDate = New Date(1980, 9, 1),
        .categoryID = 3,
        .coverImageUrl = "images/nome_rosa.jpg",
        .Genre = "Storico / Giallo",
        .Price = 24.5
    },
    New Books With {
        .Id = 4,
        .Title = "Harry Potter e la Pietra Filosofale",
        .AuthorID = "AUTH004",
        .publisherID = "PUB004",
        .isbn = "978-0747532699",
        .stockQuantity = 20,
        .publicationhDate = New Date(1997, 6, 26),
        .categoryID = 4,
        .coverImageUrl = "images/hp1.jpg",
        .Genre = "Fantasy",
        .Price = 22.0
    },
    New Books With {
        .Id = 5,
        .Title = "Il Piccolo Principe",
        .AuthorID = "AUTH005",
        .publisherID = "PUB005",
        .isbn = "978-0156013987",
        .stockQuantity = 30,
        .publicationhDate = New Date(1943, 4, 6),
        .categoryID = 5,
        .coverImageUrl = "images/principe.jpg",
        .Genre = "Fiaba",
        .Price = 14.5
    }
}


        ' Associo la lista al Repeater
        RptScambio.DataSource = listaLibri
        RptScambio.DataBind()
    End Sub


    Protected Sub BtnOpenDialogBook_Click(sender As Object, e As EventArgs)


        AddJScript("openDialog(""#dialog-scambio"", ""/AddBook.aspx"", 'Aggiungi Libro', '1200', '500');")
    End Sub



End Class