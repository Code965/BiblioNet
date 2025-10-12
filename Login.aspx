<%@ Page Title="BiblioNet" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.vb" Inherits="BiblioNet.Login" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/site.css" rel="stylesheet" />


    <script>

        $('#searchInput').hide();



    </script>


    <div class="mb-5 mt-5 row justify-content-center align-items-center ">

        <div class="col-md-11 w-50 d-flex  p-4 " style="border-radius: 5px;">

            <div class="d-flex w-50 align-items-center  flex-column gap-3 w-100" style="border-right: 1px solid black;">


                <h1>Sign In</h1>

                <%--                <asp:TextBox ID="TxtEmail" CssClass="form-control" placeholder="Inserisci l'email..." runat="server" />--%>

                <asp:TextBox ID="TxtEmail" CssClass="form-control" placeholder="Inserisci email..." runat="server" />

                <asp:TextBox ID="TxtPassword" CssClass="form-control" TextMode="Password" placeholder="Inserisci la password..." runat="server" />

                <asp:Button ID="BtnSignIn" CssClass="btn btn-dark w-100 mt-2" Text="Continue" runat="server" />

                <asp:CheckBox ID="ChkRememberMe" runat="server" Text="Remember Me" CssClass="mt-2 form-check" />
                <a href="/Register.aspx" style="color: black; text-decoration: none;" class="mt-3">Don't have an account? Sign Up</a>
            </div>

            <div class="w-50 d-flex flex-column  align-items-center justify-content-center">
                <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor"
                    class="bi bi-person-circle mb-3" viewBox="0 0 16 16">
                    <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0" />
                    <path fill-rule="evenodd"
                        d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8m8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 
           4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1" />
                </svg>
                <h2><%= If(Session("name") IsNot Nothing, Session("name"), "Guest") %></h2>
            </div>

        </div>


    </div>

</asp:Content>
