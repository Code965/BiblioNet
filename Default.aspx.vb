

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then
            InitPage()
        End If

    End Sub


    Protected Sub InitPage()
        Dim q As MyTools.Database.MySql = New MyTools.Database.MySql()

        Dim dt = q.Select("*").From("book").Where("nameBook", "=", "'HarryPotter'").ToDataTable()

        GrdBook.DataSource = dt
        GrdBook.DataBind()
    End Sub


    Protected Sub BtnOpenDialogBook_Click(sender As Object, e As EventArgs) Handles BtnOpenDialogBook.Click
        JsScript.JsHelper.AddJScript("openDialog('#dialog');")
    End Sub


    Protected Sub BtnAddBook_Click(sender As Object, e As EventArgs) Handles BtnAddBook.Click
        Dim title As String = TxtTitle.Text
        Dim author As String = TxtAutore.Text

        Dim q As MyTools.Database.MySql = New MyTools.Database.MySql()
        q.InsertInto("book", "nameBook, authorBook", "'" & title & "', '" & author & "'").ExecuteNonQuery()

        ' Chiudi il dialog dopo l'inserimento
        JsScript.JsHelper.AddJScript("$('#dialog').dialog('close'); location.reload();")
    End Sub

End Class