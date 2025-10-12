<%@ Page Title="BiblioNet" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Scambio.aspx.vb" Inherits="BiblioNet.Scambio" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/site.css" rel="stylesheet" />

    <div class="ms-2 d-flex align-items-center">

        <asp:Repeater ID="RptUsers" runat="server">
            <HeaderTemplate>
                <div class="users-container" style="display: flex; flex-direction: column;">
            </HeaderTemplate>

            <ItemTemplate>
                <div class="w-100 d-flex align-items-center gap-2 mt-2 ">

                    <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-person-circle" viewBox="0 0 16 16">
                        <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0" />
                        <path fill-rule="evenodd" d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8m8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1" />
                    </svg>

                    <div class="user-item" style="margin-bottom: 10px;">
                        <h3><%# Eval("Name") %></h3>
                    </div>
                </div>

            </ItemTemplate>

            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

    </div>


        <asp:Repeater ID="RptLibri" runat="server">
        <ItemTemplate>
            <div class="w-100 d-flex align-items-center">

                    <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-person-circle" viewBox="0 0 16 16">
                        <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0" />
                        <path fill-rule="evenodd" d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8m8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1" />
                    </svg>


            </div>

        </ItemTemplate>
    </asp:Repeater>



</asp:Content>
