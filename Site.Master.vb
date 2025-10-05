Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub lnkLogout_Click(sender As Object, e As EventArgs)
        ' Rimuove cookie e sessione
        FormsAuthentication.SignOut()
        Session.Clear()
        Session.Abandon()

        ' Reindirizza al login senza generare eccezioni
        Response.Redirect("Login.aspx", False)
        Context.ApplicationInstance.CompleteRequest()
    End Sub

    Protected Sub btnDropDown_Click(sender As Object, e As EventArgs)
        Response.Redirect("Default.aspx")
    End Sub

End Class