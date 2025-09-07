Public Class Home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load





        If Not IsPostBack Then
            initPage()
        End If

    End Sub


    Protected Sub initPage()
        Dim q As MyTools.Database.MySql = New MyTools.Database.MySql()

        Dim dt = q.Select("*").From("book").Where("nameBook", "=", "'HarryPotter'").ToDataTable()

        GrdBook.DataSource = dt
        GrdBook.DataBind()
    End Sub


End Class