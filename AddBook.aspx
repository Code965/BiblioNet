<%@ Page Title="BiblioNet" Language="VB" MasterPageFile="~/Tools.Master" AutoEventWireup="true" CodeBehind="AddBook.aspx.vb" Inherits="BiblioNet.AddBook" %>


<asp:Content ID="Content2" ContentPlaceHolderID="SecondContent" runat="server">

    <div class="row">

        <h1>Inserisci un nuovo libro</h1>
        <div class="d-flex mt-3 flex-column gap-2 col-md-6">

            <asp:TextBox ID="TxtTitle" CssClass="form-control" placeholder="Inserisci il titolo..." runat="server" />

            <asp:DropDownList ID="DrpAutore" runat="server" CssClass="form-select">
                <asp:ListItem Value="1" Text="Autore 1" />
                <asp:ListItem Value="2" Text="Autore 2" />
            </asp:DropDownList>

            <asp:DropDownList ID="DrpEditore" runat="server" CssClass="form-select">
                <asp:ListItem Value="1" Text="Editore 1" />
                <asp:ListItem Value="2" Text="Editore 2" />
            </asp:DropDownList>

            <asp:TextBox ID="TxtIsbn" CssClass="form-control" placeholder="Inserisci l'ISBN..." runat="server" />

            <asp:TextBox ID="TxtDescription" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5"></asp:TextBox>


        </div>

        <div class="col-md-6 d-flex mt-3 flex-column gap-2">
            <asp:TextBox ID="TxtPrice" CssClass="form-control" placeholder="Inserisci il prezzo..." runat="server" />
            <asp:TextBox ID="TxtQtn" CssClass="form-control" placeholder="Inserisci la quantità..." runat="server" />
            <asp:TextBox ID="TxtImagePath" CssClass="form-control" placeholder="Inserisci il percorso dell'immagine..." runat="server" />
            <asp:TextBox ID="txtDataPublicazione" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>


            <asp:DropDownList ID="DrpCategoria" runat="server" CssClass="form-select">
                <asp:ListItem Value="1" Text="Categoria 1" />
                <asp:ListItem Value="2" Text="Categoria 2" />
            </asp:DropDownList>

                        <asp:TextBox ID="TxtGenre" CssClass="form-control" placeholder="Inserisci il genere..." runat="server" />

        </div>

        <div class="w-100 d-flex justify-content-end">
                    <asp:Button CssClass="btn btn-dark mt-2" ID="BtnSubmitBook" Width="200" Text="Invia" runat="server" />

        </div>

    </div>

</asp:Content>
