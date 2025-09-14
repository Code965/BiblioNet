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
    </style>

        <link href="/Content/site.css" rel="stylesheet" />



    <div class="d-flex w-100 p-2 gap-4 flex-column ">

        <div id="banner" class="">


            ciao

        </div>

           <div id="on-loan" class="">


       In prestito

   </div>

    <asp:GridView ID="GrdBook" AutoGenerateColumns="true" runat="server"></asp:GridView>

    </div>



    <div>
    </div>

</asp:Content>
