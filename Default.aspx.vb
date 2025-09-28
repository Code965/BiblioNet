

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then
            InitPage()
        End If

    End Sub


    Protected Sub InitPage()
        Dim q As MyTools.Database.MySql = New MyTools.Database.MySql()

    End Sub


    Protected Sub BtnOpenDialogBook_Click(sender As Object, e As EventArgs) Handles BtnOpenDialogBook.Click
        JsScript.JsHelper.AddJScript("openDialog(""#dialog-scambio"", ""/AddBook.aspx"", 'Aggiungi Libro', '1200', '500');")
    End Sub




End Class