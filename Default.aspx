<%@ Page Title="BiblioNet" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="BiblioNet._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- CSS -->
    <link href="/Content/site.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,400,0,0&icon_names=book_5" />

    <!-- JS -->
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>

    <style>
        body { 
            background-color: var(--bg);
        }

        .BtnFindMonthBook {
            background-color: var(--accent-600) !important;
            color: white;
            border-radius: 5px;
            border: none;
            height: 40px;
            width: 150px;
        }

        .vertical-btn {
            height: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .line p {
            margin: 0;
            line-height: 1.2;
        }
    </style>

    <div class="d-flex w-100 p-2 gap-4 flex-column align-items-center">

        <asp:Literal ID="litProgress" runat="server"></asp:Literal>

        <!-- BANNER -->
        <div id="banner" class="d-flex align-items-center">
            <div>
                <img src="/Images/Icons/typing.png" width="200" height="200" alt="Alternate Text" />
            </div>
            <div class="d-flex flex-column">
                <h1>Black History Month</h1>
                <div>
                    <asp:Button ID="BtnMonthBook" Text="Find Out More" CssClass="btn btn-default BtnFindMonthBook" runat="server" />
                </div>
            </div>
        </div>

        <div id="on-loan" class=" d-flex p-2 flex-column">


            <%-- ELENCO DI IMMAGINI - LO FACCIAMO CON UN REPEATER --%>

            <div class="d-flex flex-column gap-3 p-3 w-100">

                <!-- Titolo e ricerca -->
                <div class="d-flex justify-content-between align-items-center w-100">
                    <span class="h5 mb-0">Scambio Libri</span>
                    <div class="d-flex gap-2">
                        <asp:TextBox ID="TxtSearchBook" CssClass="form-control" placeholder="Cerca libro..." runat="server" />
                        <asp:Button Text="Aggiorna" ID="BtnApply" CssClass="btn btn-dark" runat="server" />
                    </div>
                </div>

                <!-- Repeater libri -->
                <div class="d-flex flex-wrap gap-3">
                    <asp:Repeater ID="RptExchange" runat="server">
                        <ItemTemplate>
                            <div style="width: 150px;">
                                <img src="/Images/copertine/barack.jpeg" class="card-img-top" alt="Copertina" style="border-radius: 5px;" />
                                <div class="card-body p-2 text-muted">
                                    <p><%# Eval("Title") %></p>
                                    <p><%# Eval("AuthorID") %></p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <!-- Bottone aggiungi libro -->
                    <div class="d-flex align-items-center">
                        <asp:Button
                            ID="BtnOpenDialogBook"
                            CssClass="btn btn-primary"
                            Text="+"
                            OnClientClick="openDialog('#dialog-scambio','/AddBook.aspx','Aggiungi Libro',1200,500); return false;"
                            runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <!-- VENDITA LIBRI -->
        <div id="on-sell" class="d-flex p-2 flex-column" style="width: 95%;">
            <h1>Vendita Libri</h1>

            <div id="addBooksDialog" title="Aggiungi libri:"></div>

            <div class="d-flex flex-wrap gap-3 p-4 mt-3">

                <div>
                    <img src="/Images/copertine/barack.jpeg" alt="Copertina" style="border-radius: 5px;" />
                    <div class="mt-2 text-muted line">
                        <p>A Promise Land</p>
                        <p>Barack Obama</p>
                    </div>
                </div>

                <div>
                    <img src="/Images/copertine/barack.jpeg" alt="Copertina" style="border-radius: 5px;" />
                    <div class="mt-2 text-muted line">
                        <p>A Promise Land</p>
                        <p>Barack Obama</p>
                    </div>
                </div>

                <div>
                    <asp:Button type="button" CssClass="btn btn-primary vertical-btn" Text="+" 
                        OnClientClick="openDialog('#dialog-scambio','/AddBook.aspx','Aggiungi Libro',1200,500); return false;" runat="server" />
                </div>

            </div>
        </div>

    </div>

    <asp:Panel ID="PnlImport" runat="server">
        <div class="d-flex w-100 p-2 gap-4 flex-column align-items-center ">

            <div id="import" class=" d-flex p-2 flex-column" style="width: 95%">
                <h1>IMPORT</h1>

                    <div class="d-flex flex-column gap-2">
                        <asp:Label Text="Inserisci una categoria" runat="server" /> 
                        <div class="d-flex gap-2">
                            <asp:TextBox ID="TxtCategories" CssClass="form-control" placeholder="science..." runat="server" />
                            <asp:Button Text="Importa valori" CssClass="btn btn-dark" ID="BtnImportBook" runat="server" />
                        </div>
                        <small>Importa i libri da openlibrary</small>
                    </div>

                    <div class="d-flex flex-column mt-3">
                        <asp:Button Text="Elimina Duplicati" ID="BtnDeleteDuplicates" CssClass="btn btn-dark" runat="server" />
                        <small>Elimina tutti i duplicati</small>
                    </div>

                    <div class="d-flex flex-column mt-3">
                        <asp:Button Text="INSERT INTO Author" ID="BtnAddAuthor" CssClass="btn btn-dark" runat="server" />
                        <small>Inserisce tutti gli autori da booksopenlibrary a authors</small>
                    </div>

                </div>
            </div>
        </asp:Panel>

        <asp:Button ID="BtnReload" runat="server" Text="Button" CssClass="btn btn-primary" Visible="False" />

    </div>

</asp:Content>
