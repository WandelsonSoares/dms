<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FuncionariosUpload.aspx.cs" Inherits="FuncionariosUpload" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Script/JavaScript.js"></script>
    </head>
<body style="background-color: #e6efee;">
    <form id="form1" runat="server" style="">
        <div style="padding: 10px; text-align: left">

            <br />
            <div style="text-align: center; font-weight: bold; font-size: 14px">
                <asp:Label ID="lblUpload" runat="server" Text="UPLOAD DE LISTA DE FUNCIONÁRIOS"></asp:Label>
            </div>


            <br />
            <br />
            <div style="align-content: flex-start; text-align: left; padding-left: 10px">
                <asp:FileUpload ID="FileUpload1" runat="server" Width="375px" />
            </div>
            <br />
            <asp:Button ID="btnImportar" CssClass="btn btn-primary" runat="server" Text="Importar" OnClick="btnImportar_Click" />


            <br />
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Downloads/Funcionarios.xls">Planilha Padrão</asp:HyperLink>


        </div>

    </form>
</body>
</html>

