

Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'Oggetto query
        Dim q As MyTools.Database.MySql = New MyTools.Database.MySql()



        Dim dt As DataTable = q.Select("*").From("book").Where("nameBook = 'HarryPotter'").ToDataTable()
        Dim list = q.Select("*").From("book").Where("nameBook = 'HarryPotter'").ToList("String")

        GrdExp.DataSource = dt
        GrdExp.DataBind()

        Dim a As String = "b"




    End Sub
End Class