Imports System.Security.Cryptography
Imports BiblioNet.JsScript.JsHelper
Imports BiblioNet.UsersModel
Imports MyTools

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then
            InitPage()
        End If

    End Sub


    Protected Sub Login(sender As Object, e As EventArgs) Handles BtnSignIn.Click
        Dim q = New Database.MySql()

        Try
            Dim email As String = TxtEmail.Text.Trim()
            Dim password As String = TxtPassword.Text.Trim()

            ' Recupera l'utente dal database tramite email
            Dim user = q.Select("*").From("users").Where("email", " = ", $"'{email}'").ToObj(Of Users)

            If user Is Nothing Then
                ' Utente non trovato
                AddJScript("alert('Email non valida.');")
                Return
            End If

            ' Verifica la password
            If Not VerifyPassword(password, user.Password) Then
                AddJScript("alert('Password non valida.');")
                Return

            End If

            ' Login corretto: crea il cookie
            FormsAuthentication.SetAuthCookie(email, False)

            ' Aggiorna il dropdown con il nome in modo sicuro
            'Dim safeName As String = EscapeJs(user.Name)
            'AddJScript("$('#btnDropDown').value('" & safeName & "'); window.location='Default.aspx';")

            Session("name") = user.Name

            CurrentUser.UserId = user.Id
            CurrentUser.Name = user.Name
            CurrentUser.Role = user.Role
            CurrentUser.email = user.Email

            TxtEmail.Text = ""
            TxtPassword.Text = ""

            Response.Redirect("Default.aspx")


        Catch ex As Exception
            ' Gestione errori
            Throw New Exception("Errore durante il login: " & ex.Message & q.Build)
        End Try
    End Sub

    ' Funzione helper per escape sicuro dei caratteri JS
    Private Function EscapeJs(s As String) As String
        Return s.Replace("\", "\\").Replace("'", "\'").Replace("""", "\""")
    End Function


    Public Shared Function VerifyPassword(password As String, stored As String) As Boolean
        If password Is Nothing Then Throw New ArgumentNullException(NameOf(password))
        If stored Is Nothing Then Throw New ArgumentNullException(NameOf(stored))

        ' stored expected format: iterations:salt:hash
        Dim parts() As String = stored.Split(":"c)
        If parts.Length <> 3 Then Throw New FormatException("Stored password has invalid format.")

        Dim iterations As Integer = Integer.Parse(parts(0))
        Dim salt As Byte() = Convert.FromBase64String(parts(1))
        Dim storedHash As Byte() = Convert.FromBase64String(parts(2))

        Dim computedHash As Byte()
        Using pbkdf2 = New Rfc2898DeriveBytes(password, salt, iterations)
            computedHash = pbkdf2.GetBytes(storedHash.Length)
        End Using

        ' Confronto tempo-costante per evitare attacchi timing
        Return FixedTimeEquals(storedHash, computedHash)
    End Function


    ' Confronto tempo-costante
    Private Shared Function FixedTimeEquals(a As Byte(), b As Byte()) As Boolean
        If a Is Nothing OrElse b Is Nothing Then Return False
        If a.Length <> b.Length Then Return False

        Dim diff As Integer = 0
        For i As Integer = 0 To a.Length - 1
            diff = diff Or (a(i) Xor b(i))
        Next
        Return diff = 0
    End Function


    Protected Sub InitPage()

    End Sub


End Class