<%@ Page Title="Biblionet" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BooksList.aspx.vb" Inherits="BiblioNet.BooksList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/site.css" rel="stylesheet" />

    <style>
        h2 {
            font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
        }

        p {
            font-size: 15px;
        }

        .btn-outline-dark:hover {
            background-color: #000;
            color: #fff;
        }

        .btn-outline-danger:hover {
            background-color: #dc3545;
            color: #fff;
        }
    </style>

    <script>

    

    </script>


    <h3 class="ms-3 mb-4">Risultati per la ricerca</h3>



    <!-- NAV TAB -->
    <ul class="nav nav-tabs nav-fill ms-3 mb-4" id="bookTab" role="tablist" style="width:97%">
        <li class="nav-item" role="presentation">
            <button class="nav-link active fw-bold"
                id="cartaceo-tab"
                data-bs-toggle="tab"
                data-bs-target="#cartaceo"
                type="button"
                role="tab">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-newspaper" viewBox="0 0 16 16">
                    <path d="M0 2.5A1.5 1.5 0 0 1 1.5 1h11A1.5 1.5 0 0 1 14 2.5v10.528c0 .3-.05.654-.238.972h.738a.5.5 0 0 0 .5-.5v-9a.5.5 0 0 1 1 0v9a1.5 1.5 0 0 1-1.5 1.5H1.497A1.497 1.497 0 0 1 0 13.5zM12 14c.37 0 .654-.211.853-.441.092-.106.147-.279.147-.531V2.5a.5.5 0 0 0-.5-.5h-11a.5.5 0 0 0-.5.5v11c0 .278.223.5.497.5z" />
                    <path d="M2 3h10v2H2zm0 3h4v3H2zm0 4h4v1H2zm0 2h4v1H2zm5-6h2v1H7zm3 0h2v1h-2zM7 8h2v1H7zm3 0h2v1h-2zm-3 2h2v1H7zm3 0h2v1h-2zm-3 2h2v1H7zm3 0h2v1h-2z" />
                </svg>
                CARTACEO
            </button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link fw-bold"
                id="ebook-tab"
                data-bs-toggle="tab"
                data-bs-target="#ebook"
                type="button"
                role="tab">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-tv" viewBox="0 0 16 16">
                    <path d="M2.5 13.5A.5.5 0 0 1 3 13h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5M13.991 3l.024.001a1.5 1.5 0 0 1 .538.143.76.76 0 0 1 .302.254c.067.1.145.277.145.602v5.991l-.001.024a1.5 1.5 0 0 1-.143.538.76.76 0 0 1-.254.302c-.1.067-.277.145-.602.145H2.009l-.024-.001a1.5 1.5 0 0 1-.538-.143.76.76 0 0 1-.302-.254C1.078 10.502 1 10.325 1 10V4.009l.001-.024a1.5 1.5 0 0 1 .143-.538.76.76 0 0 1 .254-.302C1.498 3.078 1.675 3 2 3zM14 2H2C0 2 0 4 0 4v6c0 2 2 2 2 2h12c2 0 2-2 2-2V4c0-2-2-2-2-2" />
                </svg>
                EBOOK
            </button>
        </li>
    </ul>

    <!-- TAB CONTENT -->
    <div class="tab-content" id="bookTabContent">

        <!-- TAB CARTACEO -->
        <div class="tab-pane fade show active" id="cartaceo" role="tabpanel">

            <asp:Repeater ID="RptBookDetails" runat="server">
                <ItemTemplate>

                    <div class="row g-0">

                        <!-- COPERTINA -->
                        <div class="col-md-3 mb-4 text-center">
                            <img src="/Images/copertine/barack.jpeg"
                                class="img-fluid shadow-sm"
                                style="border-radius: 6px; max-height: 350px;"
                                alt="Copertina libro" />
                        </div>

                        <!-- DETTAGLI -->
                        <div class="col-md-9">

                            <h2 class="fw-bold mb-2">
                                <%# Eval("Title") %>
                            </h2>


                            <asp:LinkButton
                                ID="LnkDetails"
                                CssClass="btn btn-dark"
                                runat="server"
                                OnClick="LnkDetails_Click"
                                CommandArgument='<%# Eval("ID") %>'>

                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-arrow-bar-right" viewBox="0 0 16 16">
                                  <path fill-rule="evenodd" d="M6 8a.5.5 0 0 0 .5.5h5.793l-2.147 2.146a.5.5 0 0 0 .708.708l3-3a.5.5 0 0 0 0-.708l-3-3a.5.5 0 0 0-.708.708L12.293 7.5H6.5A.5.5 0 0 0 6 8m-2.5 7a.5.5 0 0 1-.5-.5v-13a.5.5 0 0 1 1 0v13a.5.5 0 0 1-.5.5"/>
                                </svg> Vai al dettaglio

                            </asp:LinkButton>


                        </div>
                    </div>

                </ItemTemplate>
            </asp:Repeater>

        </div>

        <!-- TAB EBOOK -->
        <div class="tab-pane fade" id="ebook" role="tabpanel">

            <div class="row g-4">


                <div class="col-md-9">

                    <h2 class="fw-bold mb-2">Versione Ebook</h2>

                    <p class="text-muted">
                        Disponibile in formato PDF / EPUB
                    </p>

                    <p>
                        <strong>Formato:</strong> Ebook<br />
                        <strong>Download:</strong> Immediato
                    </p>

                    <a href="#" class="btn btn-outline-danger">Acquista Ebook
                    </a>

                </div>
            </div>

        </div>

    </div>



</asp:Content>
