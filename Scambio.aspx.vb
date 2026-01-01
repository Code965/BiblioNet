Imports BiblioNet.UsersModel
Imports Microsoft.VisualBasic.ApplicationServices
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
    End Sub

    Protected Sub InitRptUsers()
        Dim q = New Database.MySql()

        Dim dtUsers As DataTable = q.Select("*").From("users").ToDataTable()

        RptUsers.DataSource = dtUsers
        RptUsers.DataBind()

    End Sub




    Protected Function GetBooksByUser(userId As Integer) As DataTable
        Dim q = New Database.MySql()

        Dim dtLibri As DataTable = q.
        Select("b.id, b.title, b.cover_image_url").
        From("books_personal b").
        Join("book_exchanges bc", "bc.book_id", "b.id", "LEFT").
        Where("bc.owner_id = " & userId).ToDataTable()

        Return dtLibri
    End Function

    'Protected Function GetBooksByUser(userId As Integer) As DataTable
    '    Dim q = New Database.MySql()

    '    ' Seleziona tutti i libri dell'utente che sono disponibili per lo scambio
    '    Dim dtLibri As DataTable = q.
    '    Select("b.id, b.title, b.cover_image_url").
    '    From("books_personal b").
    '    Join("book_exchanges bc", "bc.book_id", "b.id").
    '    Where("bc.owner_id = " & userId).ToDataTable()

    '    Return dtLibri
    'End Function



    Protected Sub RptUsers_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles RptUsers.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dr As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim userId As Integer = Convert.ToInt32(dr("id"))

            Dim rptLibri As Repeater = CType(e.Item.FindControl("RptLibri"), Repeater)
            If rptLibri IsNot Nothing Then
                rptLibri.DataSource = GetBooksByUser(userId)
                rptLibri.DataBind()
            End If
        End If
    End Sub



    'Protected Sub RptUsers_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles RptUsers.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        ' Dati dell'utente corrente dal DataTable
    '        Dim dr As DataRowView = CType(e.Item.DataItem, DataRowView)
    '        Dim userId As Integer = Convert.ToInt32(dr("id"))

    '        ' Trova il Repeater annidato
    '        Dim rptLibri As Repeater = CType(e.Item.FindControl("RptLibri"), Repeater)

    '        ' Filtra i libri di questo utente dal database
    '        rptLibri.DataSource = GetBooksByUser(userId)
    '        rptLibri.DataBind()
    '    End If
    'End Sub

End Class