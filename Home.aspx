<%@ Page Title="About" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.vb" Inherits="BiblioNet.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body{
            background-color:var(--bg);
        }

        #catalogo {
            width: 600px;
            background-color: chartreuse;
        }

        #vetrina {
            width: 600px;
            background-color: chartreuse;
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


    </style>

        <link href="/Content/site.css" rel="stylesheet" />



    <div class="d-flex w-100 p-2 gap-4 flex-column align-items-center ">

        <div id="banner" class="d-flex align-items-center">

            <div>
                <img src="/Images/Icons/typing.png" width="200" height="200" alt="Alternate Text" />
            </div>

            <div class="d-flex flex-column">
                <h1>Black History Month</h1>
                <p>njnjnjnj</p>

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
                    <div class=" text-muted line " style="line-height:10px;">

                        <%-- BOTTONE CHE APRE UN DIALOG PER INSERIRE NUOVI LIBRI --%>
                        <asp:Button type="button" CssClass="btn btn-primary vertical-btn" Text="+" runat="server"></asp:Button>


                    </div>
                </div>

            </div>


        </div>

    <asp:GridView ID="GrdBook" AutoGenerateColumns="true" runat="server"></asp:GridView>

    </div>



    <div>
    </div>

</asp:Content>
