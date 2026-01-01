Public Class Details
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
    End Sub

End Class