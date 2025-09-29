Imports System
Imports System.Web
Imports System.Web.UI

Namespace JsScript
    Public Module JsHelper
        Public Sub AddJScript(script As String)
            Dim currentPage As Page = TryCast(HttpContext.Current.Handler, Page)
            If currentPage IsNot Nothing Then
                ' Aggiunge sempre lo script alla pagina
                ScriptManager.RegisterStartupScript(currentPage, currentPage.GetType(), Guid.NewGuid().ToString(), script, True)
            End If
        End Sub
    End Module
End Namespace
