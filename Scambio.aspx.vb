Imports BiblioNet.UsersModel
Imports MyTools

Public Class Scambio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            InitPage()
        End If
    End Sub


    Protected Sub InitPage()
        InitRptUsers()
        InitRptLibri()
    End Sub

    Protected Sub InitRptUsers()
        Dim q = New Database.MySql()

        Dim dtUsers As DataTable = q.Select("*").From("users").ToDataTable()

        RptUsers.DataSource = dtUsers
        RptUsers.DataBind()

    End Sub

    Protected Sub InitRptLibri()
        Dim q = New Database.MySql()

        Dim ID = CurrentUser.UserId

        Dim dtLibri As DataTable = q.Select("*").From("book_exchanges").Where("owner_id", " = ", ID).ToDataTable()
        RptLibri.DataSource = dtLibri
        RptLibri.DataBind()
    End Sub

End Class