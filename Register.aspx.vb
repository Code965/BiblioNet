Imports System.Security.Cryptography
Imports BiblioNet.JsScript.JsHelper
Imports Google.Protobuf.Reflection
Imports MyTools

Public Class Register
    Inherits System.Web.UI.Page


    Private Const SaltSize As Integer = 16          ' 16 bytes = 128 bit
    Private Const HashSize As Integer = 32          ' 32 bytes = 256 bit
    Private Const Iterations As Integer = 100000    ' aumentare se hardware lo permette


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then
            InitPage()
        End If

    End Sub


    Protected Sub InitDropDown()
        InitDrpCAP()
        InitDrpCountry()
        InitDrpCity()
        InitDrpProvince()
        InitDrpRegion()
    End Sub

    Protected Sub InitDrpRegion()
        DrpRegion.Items.Clear()
        DrpRegion.Items.Add(New ListItem("-- Seleziona Regione --", ""))
        DrpRegion.Items.Add(New ListItem("Emilia-Romagna", "Emilia-Romagna"))
        DrpRegion.Items.Add(New ListItem("Lombardia", "Lombardia"))
        DrpRegion.Items.Add(New ListItem("Lazio", "Lazio"))
        DrpRegion.Items.Add(New ListItem("Sicilia", "Sicilia"))
    End Sub

    Protected Sub InitDrpProvince()
        DrpProvince.Items.Clear()
        DrpProvince.Items.Add(New ListItem("-- Seleziona Provincia --", ""))
        DrpProvince.Items.Add(New ListItem("Modena", "MO"))
        DrpProvince.Items.Add(New ListItem("Bologna", "BO"))
        DrpProvince.Items.Add(New ListItem("Milano", "MI"))
        DrpProvince.Items.Add(New ListItem("Roma", "RM"))
        DrpProvince.Items.Add(New ListItem("Palermo", "PA"))
    End Sub

    Protected Sub InitDrpCity()
        DrpCity.Items.Clear()
        DrpCity.Items.Add(New ListItem("-- Seleziona Città --", ""))
        DrpCity.Items.Add(New ListItem("Modena", "Modena"))
        DrpCity.Items.Add(New ListItem("Carpi", "Carpi"))
        DrpCity.Items.Add(New ListItem("Bologna", "Bologna"))
        DrpCity.Items.Add(New ListItem("Milano", "Milano"))
        DrpCity.Items.Add(New ListItem("Roma", "Roma"))
        DrpCity.Items.Add(New ListItem("Palermo", "Palermo"))
    End Sub

    Protected Sub InitDrpCountry()
        DrpCountry.Items.Clear()
        DrpCountry.Items.Add(New ListItem("-- Seleziona Paese --", ""))
        DrpCountry.Items.Add(New ListItem("Italia", "IT"))
        DrpCountry.Items.Add(New ListItem("Francia", "FR"))
        DrpCountry.Items.Add(New ListItem("Germania", "DE"))
        DrpCountry.Items.Add(New ListItem("Spagna", "ES"))
    End Sub

    Protected Sub InitDrpCAP()
        DrpCap.Items.Clear()
        DrpCap.Items.Add(New ListItem("-- Seleziona CAP --", ""))
        DrpCap.Items.Add(New ListItem("41121", "41121")) ' Modena centro
        DrpCap.Items.Add(New ListItem("41012", "41012")) ' Carpi
        DrpCap.Items.Add(New ListItem("40121", "40121")) ' Bologna
        DrpCap.Items.Add(New ListItem("20121", "20121")) ' Milano
        DrpCap.Items.Add(New ListItem("00184", "00184")) ' Roma
        DrpCap.Items.Add(New ListItem("90133", "90133")) ' Palermo
    End Sub

    'METODI PER HASHING DELLA PASSWORD

    ' Restituisce una stringa da salvare nel DB, es: salt:hash:iterations (tutte Base64)
    Public Shared Function HashPassword(password As String) As String
        If password Is Nothing Then Throw New ArgumentNullException(NameOf(password))

        Dim salt(SaltSize - 1) As Byte
        Using rng = RandomNumberGenerator.Create()
            rng.GetBytes(salt)
        End Using

        ' Se il framework è >= 4.7.2 puoi usare l'overload con HashAlgorithmName per SHA256:
        ' Using pbkdf2 = New Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256)
        '     Dim hash = pbkdf2.GetBytes(HashSize)
        ' End Using

        ' Versione compatibile più ampia (HMAC-SHA1) — comunque sicura se usi molte iterazioni:
        Dim hash As Byte()
        Using pbkdf2 = New Rfc2898DeriveBytes(password, salt, Iterations)
            hash = pbkdf2.GetBytes(HashSize)
        End Using

        Dim saltB64 As String = Convert.ToBase64String(salt)
        Dim hashB64 As String = Convert.ToBase64String(hash)

        ' Formato: iterations:salt:hash (comodo per upgrade parametri futuri)
        Return $"{Iterations}:{saltB64}:{hashB64}"
    End Function




    'Mi fa registrare l'utente
    Private Sub Register(sender As Object, e As EventArgs) Handles BtnRegister.Click

        Dim q = New Database.MySql()

        Dim name As String = TxtName.Text.Trim
        Dim surname As String = TxtSurname.Text.Trim
        Dim email As String = TxtEmail.Text.Trim
        Dim password As String = TxtPassword.Text
        Dim phone As String = TxtPhone.Text.Trim
        Dim cf As String = TxtCF.Text.Trim
        Dim region As String = DrpRegion.SelectedValue
        Dim province As String = DrpProvince.SelectedValue
        Dim city As String = DrpCity.SelectedValue
        Dim cap As String = DrpCap.SelectedValue
        Dim country As String = DrpCountry.SelectedValue


        If name = "" OrElse surname = "" OrElse password = "" OrElse phone = "" OrElse cf = "" OrElse region = "" OrElse province = "" OrElse city = "" OrElse cap = "" OrElse country = "" Then
            AddJScript("Tutti i campi sono obbligatori.")
            Return
        End If

        ' Esempio di hashing della password:
        Dim hashedPassword As String = HashPassword(password)

        Try
            'Completarla
            q.InsertInto("users",
    "name, surname, email,password, phone, role, CF, city, region, province, cap, country, created_at, updated_at",
    "'" & name & "', " &
    "'" & surname & "', " &
        "'" & email & "', " &
    "'" & hashedPassword & "', " &
    "'" & phone & "', " &
    0 & ", " &                  ' int, quindi senza apici
    "'" & cf & "', " &
    "'" & city & "', " &
    "'" & region & "', " &
    "'" & province & "', " &
    "'" & cap & "', " &
    "'" & country & "', " &
    "NOW(), " &
    "NOW()").ExecuteNonQuery()


        Catch ex As Exception
            Throw New Exception("Errore durante la registrazione: " & ex.Message)
        End Try

        TxtName.Text = ""
        TxtSurname.Text = ""
        TxtPassword.Text = ""
        TxtPhone.Text = ""
        TxtCF.Text = ""
    End Sub

    'Inizializzo la pagina
    Protected Sub InitPage()
        InitDropDown()
    End Sub


End Class