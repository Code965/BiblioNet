<%@ Page Title="BiblioNet" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="BiblioNet._Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/site.css" rel="stylesheet" />

    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">

        <link href="/Content/site.css" rel="stylesheet" />

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
            width: 80px; /* larghezza */
            height: 185px; /* altezza */
            font-size: 3rem; /* grandezza del + */
            font-weight: bold;
            display: flex;
            align-items: center;
            justify-content: center;
            writing-mode: vertical-rl; /* scrittura verticale */
            text-orientation: upright; /* mantiene il + dritto */
        }


        .ui-dialog-buttonpane {
            border:none;
        }

    </style>

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
        }
    });


}

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
                        <asp:Button type="button" ID="BtnOpenDialogBook" Width="50" CssClass="btn btn-primary vertical-btn d-flex align-items-center" Text="+" runat="server"/>
                       

                    </div>
                </div>

            </div>


        </div>

    <asp:GridView ID="GrdBook" AutoGenerateColumns="true" runat="server"></asp:GridView>

    </div>



    <div>
    </div>

</asp:Content>



