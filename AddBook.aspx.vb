Imports BiblioNet.JsScript.JsHelper
Imports BiblioNet.UsersModel

Public Class AddBook
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then
            InitPage()
        End If

    End Sub



    Protected Sub InitPage()

    End Sub

    Protected Sub BtnSubmitBook_Click(sender As Object, e As EventArgs) Handles BtnSubmitBook.Click

        Dim titolo As String = TxtTitle.Text
        Dim autore As String = DrpAutore.SelectedValue
        Dim edizione As String = DrpEditore.Text
        Dim ISBN As String = TxtIsbn.Text
        Dim descrizione As String = TxtDescription.Text
        Dim prezzo As String = TxtPrice.Text
        Dim quantita As String = TxtQtn.Text
        Dim categoria As String = DrpCategoria.SelectedValue
        Dim img As String = TxtImagePath.Text
        Dim dataPubblicazione As String = txtDataPublicazione.Text
        Dim genere As String = TxtGenre.Text

        If String.IsNullOrEmpty(titolo) OrElse
           String.IsNullOrEmpty(autore) OrElse
           String.IsNullOrEmpty(edizione) OrElse
           String.IsNullOrEmpty(ISBN) OrElse
           String.IsNullOrEmpty(descrizione) OrElse
           String.IsNullOrEmpty(prezzo) OrElse
           String.IsNullOrEmpty(quantita) OrElse
           String.IsNullOrEmpty(categoria) OrElse
           String.IsNullOrEmpty(dataPubblicazione) OrElse
           String.IsNullOrEmpty(genere) Then

            ' Mostra messaggio di errore
            AddJScript("alert('Tutti i campi obbligatori devono essere compilati.')")

            Exit Sub
        End If

        Dim q As MyTools.Database.MySql = New MyTools.Database.MySql()

        Try

            q.InsertInto("books_personal",
             "title, author_id, publisher_id, isbn, description, price, stock_quantity, category_id, cover_image_url, publication_date, genres, user_id, created_at, updated_at",
             "'" & titolo & "', " &
             "'" & autore & "', " &
             "'" & edizione & "', " &
             "'" & ISBN & "', " &
             "'" & descrizione & "', " &
             "'" & Convert.ToDouble(prezzo) & "', " &
             "'" & quantita & "', " &
             "'" & categoria & "', " &
             "'" & img & "', " &
             "'" & dataPubblicazione & "', " &
             "'" & genere & "', " &
             "'" & CurrentUser.UserId & "', " &
             "NOW(), " &
             "NOW()").ExecuteNonQuery()

            'Mostra messaggio di successo
            AddJScript("BiblionetMainWindow.show_success_message('Libro aggiunto con successo!')")



        Catch ex As Exception
            Throw New Exception("Errore durante l'inserimento del libro: " & ex.Message)
        End Try

    End Sub

End Class