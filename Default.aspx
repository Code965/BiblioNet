<%@ Page Title="BiblioNet" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="BiblioNet._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/site.css" rel="stylesheet" />

    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">

    <link href="/Content/site.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,400,0,0&icon_names=book_5" />


    <script>
        $(function () {
            // ascolta il click sul logo
            $("#Logo").on("click", function (e) {
                if (e.ctrlKey) {
                    openBiblionetImport();
                }
            });
        });

        function openBiblionetImport() {
            window.open("BiblionetImport.aspx?", "_blank");
        }


    </script>


    <script src="/Scripts/js/Default.js"></script>


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

        .btn-custom {
            background-color: var(--primary-900);
            color: white;
            border: none;
            width: 100px;
            height: 227px;
            font-size: 24px;
            text-align: center;
            padding: 0;
            border: 1px var(--primary-300) dashed;
        }
    </style>

    <div class="d-flex w-100 p-2 gap-4 flex-column align-items-center ">

        <asp:Literal ID="litProgress" runat="server"></asp:Literal>


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

                <!-- RIGA SUPERIORE: titolo a sinistra, ricerca a destra -->
                <div class="d-flex justify-content-between align-items-center w-100">
                    <span class="h5 mb-0">Vendita Libri</span>
                    <div class="d-flex gap-2">
                        <asp:TextBox ID="TxtSearchBook" CssClass="form-control" placeholder="Cerca libro..." runat="server" />
                        <asp:Button Text="Aggiorna" ID="BtnApply" CssClass="btn btn-dark" runat="server" />
                    </div>
                </div>

                <!-- REPEATER LIBRI -->
                <div class="d-flex flex-wrap gap-3">
                    <asp:Repeater ID="RptExchange" runat="server">
                        <ItemTemplate>
                            <div class="" style="width: 150px;">
                                <img src="/Images/copertine/barack.jpeg" class="card-img-top" alt="Copertina" style="border-radius: 5px;" />
                                <div class="card-body p-2 text-muted" style="line-height: 1.2;">
                                    <p class="mb-1"><%# Eval("Title") %></p>
                                    <p class="mb-0"><%# Eval("AuthorID") %></p>
                                </div>

                                <div class="d-flex align-items-center justify-content-center gap-3 w-100">


                                    <asp:LinkButton
                                        ID="BtnEditBookOnLoan"
                                        runat="server"
                                        CssClass="btn btn-dark d-flex align-items-center w-25"
                                        ToolTip="Modifica"
                                        CommandName="Edit"
                                        CommandArgument='<%# Eval("id") %>'>                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                                          <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                                          <path fill-rule="evenodd" d="M1.05 3.5a.5.5 0 0 1 .5-.5h4.138l.5-.5H1.5a1.5 1.5 0 0 0-1.5 1.5v12.5A1.5 1.5 0 0 0 1.5 15.5h12.5a1.5 1.5 0 0 0 1.5-1.5V6.662l-.5.5V14.5a.5.5 0 0 1-.5.5H1.5a.5.5 0 0 1-.5-.5V3.5z"/>
                                        </svg>
                                    </asp:LinkButton>

                                    <asp:LinkButton
                                        ID="BtnDeleteBookOnLoan"
                                        runat="server"
                                        CssClass="btn btn-danger d-flex align-items-center w-25"
                                        ToolTip="Elimina"
                                        CommandName="Delete"
                                        CommandArgument='<%# Eval("id") %>'
                                        OnClientClick="return confirm('Sei sicuro di voler eliminare questo libro?');">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3" viewBox="0 0 16 16">
                                          <path d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5M11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47M8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5"/>
                                        </svg>
                                    </asp:LinkButton>
                                </div>

                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:Button
                        type="button"
                        ID="BtnOpenDialogBook"
                        CssClass="btn-custom"
                        Text="+"
                        OnClientClick="BiblionetMainWindow.openDialog('#dialog-scambio','/AddBook.aspx','Aggiungi Libro',1200,500); return false;"
                        runat="server" />

                </div>

            </div>
        </div>

        <div id="on-sell" class=" d-flex p-2 flex-column" style="width: 95%">

            <div class="d-flex flex-column gap-3 p-3 w-100">

                <!-- RIGA SUPERIORE: titolo a sinistra, ricerca a destra -->
                <div class="d-flex justify-content-between align-items-center w-100">
                    <span class="h5 mb-0">Scambio Libri</span>
                    <div class="d-flex gap-2">
                        <asp:TextBox ID="TxtSearchBookOnSell" CssClass="form-control" placeholder="Cerca libro..." runat="server" />
                        <asp:Button Text="Aggiorna" ID="BtnApplyOnSell" CssClass="btn btn-dark" runat="server" />
                    </div>
                </div>

                <!-- REPEATER LIBRI -->
                <div class="d-flex flex-wrap gap-3">


                    <asp:Button
                        type="button"
                        ID="Button2"
                        CssClass="btn-custom"
                        Text="+"
                        OnClientClick="BiblionetMainWindow.openDialog('#dialog-scambio','/AddBook.aspx','Aggiungi Libro',1200,500); return false;"
                        runat="server" />

                </div>

            </div>




        </div>

        <div class="d-flex justify-content-center gap-2" style="width: 100%">

            <div id="whislist" class=" d-flex p-2 flex-column">
                <div class="d-flex flex-column gap-3 p-3 w-100">

                    <div class="title w-100 text-center">
                        RESERVED BOOKS
                        <hr />

                        <asp:GridView id="GrdReservedBooks" runat="server">


                        </asp:GridView>

                    </div>



                </div>

            </div>

            <div id="favouriteAuthor" class=" d-flex p-2 flex-column">
                <div class="d-flex flex-column gap-3 p-3 w-100">
                    <div class="title w-100 text-center">
                        FAVOURITE AUTHORS
                         <hr />

                        <asp:GridView ID="GrdFavouriteAuthor" runat="server">

                        </asp:GridView>

                    </div>
                </div>

            </div>




        </div>


    </div>






</asp:Content>
