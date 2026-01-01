Imports System
Imports System.IO
Imports System.Web
Imports System.Web.Script.Serialization

Namespace LogScript
    Public Module LogHelper
        Public Sub WriteLog(msg As String, path As String)

            'Prendo il path del file di log su cui voglio scrivere
            Dim filePath As String = HttpContext.Current.Server.MapPath(path)

            ' Aggiungi il messaggio al file con timestamp
            Dim logMessage As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " - " & msg & Environment.NewLine
            File.AppendAllText(filePath, logMessage)
        End Sub
    End Module
End Namespace

