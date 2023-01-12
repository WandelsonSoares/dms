<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sobre.aspx.cs" Inherits="Sobre" %>

<!DOCTYPE html>

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" href="Content/bootstrap.min.css" rel="stylesheet" />
    <link type="text/css"  href="css/StyleSheet.css" rel="stylesheet" />

    <title>Sobre</title>

</head>
<body>
    <form id="form1" runat="server">
        <div style="position: fixed">
        </div>
        <div style="text-align: center">
            <div class="masterPageHeaderDiv">
                <img alt="COMAU" src="/img2/Logo-COMAU-white.png" />
            </div>
            <h3>Sistema de Gestão de Demandas</h3>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br />
            <h4>Este software foi desenvolvido por <strong>Wandelson Soares </strong>para a Comau do Brasil Ind. e Com. Ltda.
                <h4>Suporte: (31) 2123-7169 / wandelson.soares@comau.com</h4>
            </h4>
            <br />
            <asp:LinkButton ID="LinkButtonSobreDesenvolvedor" runat="server" Font-Size="12pt" OnClick="LinkButtonSobreDesenvolvedor_Click">+ Sobre o desenvolvedor</asp:LinkButton>
        </div>
    </form>
</body>
</html>
