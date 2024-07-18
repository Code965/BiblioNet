<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Biblioteca._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>

        <section class="row" aria-labelledby="aspnetTitle">
            
        </section>

        <%-- Qui c'è il grid sistem di bootstrap --%>
        <div class="row">

            <%-- invece dei div ci sono i section --%>
            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                A
            </section>

            <section class="col-md-4" aria-labelledby="librariesTitle">
               A
            </section>

            <section class="col-md-4" aria-labelledby="hostingTitle">
               A
            </section>
        </div>
    </main>

</asp:Content>
