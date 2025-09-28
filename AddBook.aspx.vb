Public Class AddBook
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then
            InitPage()
        End If

    End Sub



    Protected Sub InitPage()

    End Sub

    'Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    '    Dim titolo As String = txtTitle.Text
    '    Dim autore As String = txtAuthor.Text

    '    Dim q As MyTools.Database.MySql = New MyTools.Database.MySql()

    '    q.InsertInto("book", "nameBook, author", "'" & titolo & "', '" & autore & "'").ExecuteNonQuery()



    'End Sub

End Class