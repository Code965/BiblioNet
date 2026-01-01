<%@ Page Title="BiblioNet" Language="VB" MasterPageFile="~/Tools.Master" AutoEventWireup="true" CodeBehind="BiblionetImport.aspx.vb" Inherits="BiblioNet.BiblionetImport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SecondContent" runat="server">

    <link href="/Content/site.css" rel="stylesheet" />
    <script src="/Scripts/js/Default.js"></script>

    
    <script>
        $(function () {



        })

        function deleteDuplicates() {
            __doPostBack('<%= BtnDeleteDuplicates.UniqueID %>', '');
        }


    </script>



    <div class="container py-4">

        <asp:Panel ID="PnlImport" runat="server">

            <p class="alert alert-info">
                La pagina BiblioNet Import è uno strumento intuitivo per la gestione e l’importazione dei dati dei libri da OpenLibrary, progettata per rendere rapide e semplici operazioni altrimenti complesse.
            </p>

            <div class="card shadow-sm border-0">

                <div class="card-header bg-dark text-white">
                    <h3 class="mb-0">📚 BIBLIONET IMPORT</h3>
                </div>

                <div class="card-body">

                    <!-- CATEGORIE -->
                    <div class="mb-4">
                        <label class="form-label fw-bold">Inserisci una categoria</label>

                        <div class="input-group">
                            <asp:TextBox ID="TxtCategories" CssClass="form-control" placeholder="science..." runat="server" />
                            <asp:Button Text="Importa valori" CssClass="btn btn-dark" ID="BtnImportBook" runat="server" />
                        </div>

                        <small class="text-muted">
                            Importa i libri da OpenLibrary in base ad un genere.
                        </small>
                    </div>

                    <hr />

                    <h3>IMPORTAZIONE DATI</h3> <br />

                    <%-- IMPORT BOOKS --%>

                    <div class="mb-4">
                        <asp:Button Text="IMPORT BOOKS" ID="BtnImportBooks" CssClass="btn btn-outline-dark w-100" runat="server" />
                        <small class="text-muted">Importa tutti i libri booksopenlibrary.</small>
                    </div>

                    <!-- ADD JSON DATE -->
                    <div class="mb-4">
                        <asp:Button Text="ADD JSON DATE" ID="BtnAddJsonDate" CssClass="btn btn-outline-dark w-100" runat="server" />
                        <small class="text-muted">Inserisce un JSON aggiuntivo di dati alla tabella booksopenlibrary.</small>
                    </div>

                    <%-- ADD NEW DATA IN OpenLibraryEditionDetails --%>

                    <div class="mb-4">
                        <asp:Button Text="ADD JSON DATE ON OpenLibraryEditionDetails" Width="300" ID="BtnAddOnOpenLibraryEditionDetails" CssClass="btn btn-outline-dark w-100" runat="server" />
                        <small class="text-muted">Inserisce dati aggiuntivi sulla tabella BtnAddOnOpenLibraryEd.</small>
                    </div>


                    <%-- GET ADDRESS PUBLISHER BY OPEN STREET MAP --%>

                    <div class="mb-4">
                        <asp:Button Text="GET ADDRESS BY OPENSTREETMAP" Width="300" ID="BtnAddressByOpenStreetMap" CssClass="btn btn-outline-dark w-100" runat="server" OnClientClick="FindAddressByPublisher" />
                        <small class="text-muted">Ottengo l'indirizzo dell'editore con openstreetmap</small>
                    </div>

                    <hr />

                    <h3>ELABORAZIONE DATI</h3> <br />

                    <!-- DELETE DUPLICATES -->
                    <div class="mb-4">
                      <asp:Button
    ID="BtnDeleteDuplicates"
    Text="Elimina Duplicati"
    CssClass="btn btn-danger w-100"
    runat="server"
    OnClientClick="
        BiblionetMainWindow.confirm_box(
            'Conferma eliminazione',
            'Sei sicuro di eliminare i duplicati?',
            deleteDuplicates  
        );
        return false;
    " />


                        <small class="text-muted">Rimuove tutti i duplicati dalla tabella booksopenlibrary.</small>
                    </div>

                    <!-- ADD AUTHORS -->
                    <div class="mb-4">
                        <asp:Button Text="INSERT INTO Author" ID="BtnAddAuthor" CssClass="btn btn-outline-dark w-100" runat="server" />
                        <small class="text-muted">Inserisce tutti gli autori da booksopenlibrary nella tabella authors.</small>
                    </div>

                    <%-- ADD PUBLISHER --%>
                    <div class="mb-4">
                        <asp:Button Text="INSERT INTO publisher" ID="BtnAddNamePublisher" CssClass="btn btn-outline-dark w-100" runat="server" />
                        <small class="text-muted">Inserisce tutti i nomi editori da booksopenlibrary_ed. nella tabella publisher.</small>
                    </div>
                </div>

            </div>

        </asp:Panel>

    </div>

</asp:Content>
