<%@ Page Title="BiblioNet" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Scambio.aspx.vb" Inherits="BiblioNet.Scambio" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/site.css" rel="stylesheet" />

    <div class="ms-2 d-flex align-items-center ms-4">

        <asp:Repeater ID="RptUsers" runat="server" OnItemDataBound="RptUsers_ItemDataBound">
            <HeaderTemplate>
                <div class="users-container" style="display: flex; flex-direction: column;">
            </HeaderTemplate>

            <ItemTemplate>
    <div class="user-item-container mb-4">

        <!-- Riga superiore: SVG + Nome/Descrizione -->
        <div class="d-flex align-items-center gap-2 mb-2">
            <!-- Icona utente -->
            <div class="mb-0">
                <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-person-circle" viewBox="0 0 16 16">
                    <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0" />
                    <path fill-rule="evenodd" d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8m8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1" />
                </svg>
            </div>

            <!-- Nome + descrizione -->
            <div>
                <h3 class="mb-0"><%# Eval("Name") %></h3>
                <p class="mb-0 text-muted">Amo leggere e scambiare libri</p>
            </div>
        </div>

        <!-- Repeater dei libri: sotto la riga superiore -->
        <asp:Repeater ID="RptLibri" runat="server">
            <HeaderTemplate><div class="d-flex flex-wrap gap-2"></HeaderTemplate>
            <ItemTemplate>
                <div class="card ms-5" style="width: 150px;">
                    <img src='<%# Eval("cover_image_url") %>' class="card-img-top" alt="Copertina" style="border-radius: 5px;" />
                    <div class="card-body p-2 text-muted" style="line-height: 1.2;">
                        <p class="mb-1"><%# Eval("title") %></p>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate></div></FooterTemplate>
        </asp:Repeater>

    </div>
</ItemTemplate>


<%--            <ItemTemplate>
                <div class="w-100 d-flex align-items-center gap-2 mt-2 container ">
                    <div class="mb-4">
                        <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-person-circle" viewBox="0 0 16 16">
                            <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0" />
                            <path fill-rule="evenodd" d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8m8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1" />
                        </svg>
                    </div>

                    <div class="user-item mt-2" style="margin-bottom: 10px;">
                        <h3><%# Eval("Name") %></h3>
                        <p>Amo leggere e scambiare libri</p>

                        <!-- Repeater annidato per i libri -->
                        <asp:Repeater ID="RptLibri" runat="server">
                            <HeaderTemplate>
                                <div class="d-flex flex-wrap gap-2">
                            </HeaderTemplate>

                            <ItemTemplate>
                                <div class="container ms-5">
                                    <div class="card " style="width: 150px;">
                                        <img src='<%# Eval("cover_image_url") %>' class="card-img-top" alt="Copertina" style="border-radius: 5px;" />
                                        <div class="card-body p-2 text-muted" style="line-height: 1.2;">
                                            <p class="mb-1"><%# Eval("title") %></p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>

                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>

                    </div>
                </div>
            </ItemTemplate>--%>

            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>



    </div>





</asp:Content>
