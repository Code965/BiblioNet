<%@ Page Title="BiblioNet" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="BiblioNet._Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/site.css" rel="stylesheet" />

    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">

        <link href="/Content/site.css" rel="stylesheet" />
<link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,400,0,0&icon_names=book_5" />
    
    
    <script>
        function openDialog(dialogSelector, pageUrl, title, width, height) {

            var $dialog = $(dialogSelector);

            // se il div non esiste, lo creo e lo aggiungo al body
            if ($dialog.length === 0) {
                $dialog = $('<div>', { id: dialogSelector.replace('#', '') });
                $('body').append($dialog);
            }


            // pulisco il contenuto
            $dialog.empty();

            // inserisco l'iframe
            $dialog.html('<iframe src="' + pageUrl + '" style="border:0;width:100%;height:100%;"></iframe>');

            // apro il dialog
            $dialog.dialog({
                modal: true,
                title: title,
                width: width,
                height: height,
                dialogClass: "dialog-open",
                buttons: {
                    "Chiudi": function () {
                        $(this).dialog("close");
                    }
                },
                open: function () {
                    // Aggiungo classi Bootstrap ai pulsanti quando il dialog si apre
                    $(this).parent().find(".ui-dialog-buttonpane button").addClass("btn btn-dark");
                }
            });

            $dialog.parent().find(".ui-dialog-title").html(`
          <div class="d-flex align-items-center gap-2">
            <span class="material-symbols-outlined icon-circle d-flex aling-items-center" style="color:black;">book_5</span>
            Aggiungi Libro
          </div>
        `);
        }

    </script>
    
    
    
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

        


    </style>

    <script>
       

    </script>


    <div class="d-flex w-100 p-2 gap-4 flex-column align-items-center ">

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
              
            <h1>Scambio Libri</h1> 


            <%-- ELENCO DI IMMAGINI - LO FACCIAMO CON UN REPEATER --%>
            <div class="d-flex flex-wrap gap-3 p-4 mt-3">

                <asp:Repeater ID="RptScambio" runat="server">
                    <ItemTemplate>

                        <div>
                            <img src="/Images/copertine/barack.jpeg" alt="Alternate Text" style="border-radius: 5px;" />
                            <div class="mt-2 text-muted line " style="line-height: 10px;">
                                <p><%# Eval("Title") %> </p>

                                <p><%# Eval("AuthorID") %></p>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>
                                       

               


            <%--    <div>
                    <img src="/Images/copertine/barack.jpeg" alt="Alternate Text" style="border-radius: 5px;" />
                    <div class="mt-2 text-muted line " style="line-height: 10px;">
                        <p>A Promise Land</p>
                        <p>Barack Obama</p>
                    </div>
                </div>--%>

                <div>
                    <div class=" text-muted line " style="line-height: 10px;">

                        <%-- BOTTONE CHE APRE UN DIALOG PER INSERIRE NUOVI LIBRI --%>
                        <asp:Button type="button" ID="BtnOpenDialogBook" Width="50" CssClass="btn btn-primary d-flex align-items-center" Text="+" OnClientClick="openDialog('#dialog-scambio','/AddBook.aspx','Aggiungi Libro',1200,500); return false;" runat="server"/>

                    </div>
                </div>

            </div>


        </div>


         <div id="on-sell" class=" d-flex p-2 flex-column" style="width:95%">
            <h1>Vendita Libri</h1>

            <%-- DIALOG PER INSERIRE NUOVI LIBRI--%>
            <div id="addBooksDialog" title="Aggiungi libri:">                
            </div>

            <%-- ELENCO DI IMMAGINI - LO FACCIAMO CON UN REPEATER --%>
            <div class="d-flex flex-wrap gap-3 p-4 mt-3">


                <div>
                    <img src="/Images/copertine/barack.jpeg" alt="Alternate Text" style="border-radius: 5px;" />
                    <div class="mt-2 text-muted line " style="line-height: 10px;">
                        <p>A Promise Land</p>
                        <p>Barack Obama</p>
                    </div>
                </div>


                <div>
                    <img src="/Images/copertine/barack.jpeg" alt="Alternate Text" style="border-radius: 5px;" />
                    <div class="mt-2 text-muted line " style="line-height: 10px;">
                        <p>A Promise Land</p>
                        <p>Barack Obama</p>
                    </div>
                </div>

                <div>
                    <div class=" text-muted line " style="line-height: 10px;">

                        <%-- BOTTONE CHE APRE UN DIALOG PER INSERIRE NUOVI LIBRI --%>
                        <asp:Button type="button" CssClass="btn btn-primary vertical-btn" Text="+" OnClientClick="openDialog('#dialog-scambio','/AddBook.aspx','Aggiungi Libro',1200,500); return false;" runat="server"></asp:Button>
                                                
                       
                    </div>
                </div>

            </div>

        </div>

    <asp:GridView ID="GrdBook" AutoGenerateColumns="true" runat="server"></asp:GridView>

    </div>



    <div>
    </div>

</asp:Content>



