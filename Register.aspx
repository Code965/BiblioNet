<%--<%@ Page Title="BiblioNet" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.vb" Inherits="BiblioNet.Register" %>--%>

<%@ Page Language="VB" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.vb" Inherits="BiblioNet.Register" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/site.css" rel="stylesheet" />

    <script>

        $('#searchInput').hide();


    </script>

    <style>
        input, select, textarea {
            max-width: none;
        }
    </style>

    <div class=" py-5 d-flex justify-content-center align-items-center">

        <div class="card shadow-lg border-0 rounded-4 p-4" style="max-width: 1080px; width: 100%;">

            <h2 class=" fw-bold mb-4 text-primary">
                <i class="bi bi-person-plus-fill me-2"></i>Registrazione Utente
            </h2>

            <!-- Nome -->
            <div class="mb-3">
                <label  class="form-label fw-semibold">
                    <i class="bi bi-person-fill me-1 text-secondary"></i>Nome
                </label>
                <asp:TextBox ID="TxtName" runat="server" CssClass="form-control rounded-pill p-2" placeholder="Inserisci il tuo nome" />
            </div>

            <!-- Cognome -->
            <div class="mb-3">
                <label  class="form-label fw-semibold">
                    <i class="bi bi-person-lines-fill me-1 text-secondary"></i>Cognome
                </label>
                <asp:TextBox ID="TxtSurname" runat="server" CssClass="form-control rounded-pill p-2" placeholder="Inserisci il tuo cognome" />
            </div>

            <%-- Email --%>
             <div class="mb-3">
     <label  class="form-label fw-semibold">
         <i class="bi bi-person-lines-fill me-1 text-secondary"></i>Email
     </label>
     <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control rounded-pill p-2" placeholder="Inserisci la tua email" />
 </div>

            <!-- Password -->
            <div class="mb-3">
                <label  class="form-label fw-semibold">
                    <i class="bi bi-lock-fill me-1 text-secondary"></i>Password
                </label>
                <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control rounded-pill p-2" TextMode="Password" placeholder="Crea una password" />
            </div>

            <!-- Telefono -->
            <div class="mb-3">
                <label  class="form-label fw-semibold">
                    <i class="bi bi-telephone-fill me-1 text-secondary"></i>Telefono
                </label>
                <asp:TextBox ID="TxtPhone" runat="server" CssClass="form-control rounded-pill p-2" placeholder="Numero di telefono" />
            </div>

            <!-- Codice Fiscale -->
            <div class="mb-3">
                <label  class="form-label fw-semibold">
                    <i class="bi bi-card-text me-1 text-secondary"></i>Codice Fiscale
                </label>
                <asp:TextBox ID="TxtCF" runat="server" CssClass="form-control rounded-pill p-2" placeholder="Inserisci il codice fiscale" />
            </div>

            <!-- Nazione -->
            <div class="mb-3">
                <label for="DrpCountry" class="form-label fw-semibold">
                    <i class="bi bi-globe2 me-1 text-secondary"></i>Nazione
                </label>
                <asp:DropDownList ID="DrpCountry" runat="server" CssClass="form-select rounded-pill p-2">
                    <asp:ListItem Text="Seleziona una nazione" Value="" />
                    <asp:ListItem Text="Italia" />
                    <asp:ListItem Text="Francia" />
                </asp:DropDownList>
            </div>

            <!-- Regione -->
            <div class="mb-3">
                <label for="DrpRegion" class="form-label fw-semibold">
                    <i class="bi bi-geo-alt-fill me-1 text-secondary"></i>Regione
                </label>
                <asp:DropDownList ID="DrpRegion" runat="server" CssClass="form-select rounded-pill p-2">
                    <asp:ListItem Text="Seleziona una regione" Value="" />
                    <asp:ListItem Text="Emilia-Romagna" />
                    <asp:ListItem Text="Lombardia" />
                </asp:DropDownList>
            </div>

            <!-- Provincia -->
            <div class="mb-3">
                <label for="DrpProvince" class="form-label fw-semibold">
                    <i class="bi bi-geo me-1 text-secondary"></i>Provincia
                </label>
                <asp:DropDownList ID="DrpProvince" runat="server" CssClass="form-select rounded-pill p-2">
                    <asp:ListItem Text="Seleziona una provincia" Value="" />
                    <asp:ListItem Text="Modena" />
                    <asp:ListItem Text="Bologna" />
                </asp:DropDownList>
            </div>

            <!-- Città -->
            <div class="mb-3">
                <label for="DrpCity" class="form-label fw-semibold">
                    <i class="bi bi-building me-1 text-secondary"></i>Città
                </label>
                <asp:DropDownList ID="DrpCity" runat="server" CssClass="form-select rounded-pill p-2">
                    <asp:ListItem Text="Seleziona una città" Value="" />
                    <asp:ListItem Text="Modena" />
                    <asp:ListItem Text="Carpi" />
                </asp:DropDownList>
            </div>

            <!-- CAP -->
            <div class="mb-4">
                <label for="DrpCap" class="form-label fw-semibold">
                    <i class="bi bi-mailbox2 me-1 text-secondary"></i>CAP
                </label>
                <asp:DropDownList ID="DrpCap" runat="server" CssClass="form-select rounded-pill p-2">
                    <asp:ListItem Text="Seleziona un CAP" Value="" />
                    <asp:ListItem Text="41121" />
                    <asp:ListItem Text="41012" />
                </asp:DropDownList>
            </div>

            <!-- Pulsante -->
            <div class="d-grid w-100 justify-content-end">
                <asp:Button ID="BtnRegister" runat="server" Text="Crea account" CssClass="btn btn-primary rounded-pill py-2 fw-semibold shadow-sm" />
            </div>

            <p class="text-center mt-3 mb-0 text-muted">
                Hai già un account?
            <a href="Login.aspx" class="text-decoration-none fw-semibold text-primary">Accedi</a>
            </p>
        </div>
    </div>




</asp:Content>
