<%@ Page Title="Biblionet" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.vb" Inherits="BiblioNet.Details" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/site.css" rel="stylesheet" />

    <!-- Scheda prodotto -->
    <div class="product-detail-container">

        <!-- Titolo e autore -->
        <h1>s</h1>
        <p class="product-author">n</p>

        <div class="product-main">

            <!-- Sezione Immagine -->
            <div class="product-image">
                <img src="/Images/copertine/barack.jpeg"
                    class="img-fluid shadow-sm"
                    style="border-radius: 6px; max-height: 350px;"
                    alt="Copertina libro" />

            </div>

            <!-- Info prodotto -->
            <div class="product-info">

                <!-- Descrizione breve -->
                <%--                <p class="product-category"><strong>Materia:</strong> <%= Me.ProductCategory %></p>
                <p class="product-edition"><strong>Edizione:</strong> <%= Me.ProductEdition %></p>--%>

                <!-- Descrizione -->
                <div class="description-box">
                    <h3>Descrizione</h3>
                    <%--                    <p><%= Me.ProductDescription %></p>--%>
                </div>

                <!-- Prezzi e pulsanti -->
                <div class="product-pricing">
                    <p class="price"><strong>Prezzo:</strong> € </p>
                    <p class="price-ebook"><strong>eBook:</strong> € </p>

<%--                    <asp:Button ID="btnAddToCart" runat="server" Text="Aggiungi al carrello"
                        CssClass="btn btn-primary" OnClick="btnAddToCart_Click" />--%>

                </div>

                <!-- Dettagli aggiuntivi -->
                <div class="product-more-info">
                    <p><strong>ISBN:</strong> ISBN</p>
                    <p><strong>Pagine:</strong> 2</p>
                </div>

            </div>
        </div>

    </div>

</asp:Content>
