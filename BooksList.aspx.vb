Imports BiblioNet.BookModels
Imports MyTools

Public Class BooksList
    Inherits System.Web.UI.Page

    Public listaIds As New List(Of Integer)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ids As String = Request.QueryString("ids") ' es: "5-30-4-34"

        If Not String.IsNullOrEmpty(ids) Then
            listaIds = ids.Split("-"c).
                Select(Function(id) Convert.ToInt32(id)).
                ToList()
        End If

        If Not Page.IsPostBack Then
            InitPage()
        End If

    End Sub

    Protected Sub InitPage()
        InitRepeater()
    End Sub

    'Inizializza il Repeater con i dettagli dei libri
    Protected Sub InitRepeater()

        If listaIds.Count = 0 Then Exit Sub

        Dim q = New Database.MySql()

        Dim listaLibri = q.
            Select("*").
            From("books_personal").
          Where("id IN (" & String.Join(",", listaIds) & ")").
        ToList(Of Books)()

        RptBookDetails.DataSource = listaLibri
        RptBookDetails.DataBind()

    End Sub

    ' Va direttamente alla pagina del dettaglio - Details.aspx
    Protected Sub LnkDetails_Click(sender As Object, e As EventArgs)

        'Response.Redirect("Details.aspx?id=5")
        Dim lnk As LinkButton = CType(sender, LinkButton)
        Dim id As String = lnk.CommandArgument

        Response.Redirect("Details.aspx?ids=" & id)


    End Sub



End Class
