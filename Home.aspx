<%@ Page Title="About" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.vb" Inherits="BiblioNet.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <style>

        #catalogo{
        width: 600px;
 background-color:chartreuse;
}

                #vetrina{
        width: 600px;
 background-color:chartreuse;
}

    </style>


    <div class="d-flex w-100 gap-4 ">

        <div class="" id="catalogo" >


            Catalogo

        </div>

        <div class="" id="vetrina">

            Vetrina
        </div>


    </div>

    <h2>Libri in evidenza</h2>
            <asp:GridView ID="GrdBook" AutoGenerateColumns="true" runat="server"></asp:GridView>


    <div>
    </div>

</asp:Content>
