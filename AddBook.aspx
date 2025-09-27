<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="AddBook.aspx.vb" Inherits="BiblioNet.AddBook" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aggiungi Libro</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtTitle" runat="server" />
            <asp:TextBox ID="txtAuthor" runat="server" />
            <asp:Button ID="btnSave" runat="server" Text="Salva" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
