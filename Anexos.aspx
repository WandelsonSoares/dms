﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Anexos.aspx.cs" Inherits="Anexos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Script/JavaScript.js"></script>
</head>
<body style="background-color: #FFFFCC;">
    <form id="form1" runat="server" style="">
        <div style="padding: 10px; text-align: left">

            <br />
            <div style="text-align: center; font-weight: bold; font-size: 14px">
                <asp:Label ID="lblUpload" runat="server" Text="DOCUMENTOS"></asp:Label>
            </div>


            <br />
            <br />
            <div style="align-content: flex-start; text-align: left; padding-left: 10px">
                <asp:FileUpload ID="FileUpload1" runat="server" Width="375px" Enabled="False" />
            </div>
            <br />
            <asp:Button ID="btnImportar" CssClass="btn btn-primary" runat="server" Text="Importar" OnClick="btnImportar_Click" Enabled="False" />

            <asp:Label ID="Label1" ForeColor="Red" runat="server"></asp:Label>


        </div>

        <div style="padding: 10px">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                Width="100%"
                EmptyDataText="Nenhum registro localizado." BorderStyle="None" BorderWidth="1px" BackColor="White" BorderColor="#3366CC" CellPadding="4" OnRowCommand="GridView1_RowCommand">
                <AlternatingRowStyle BorderStyle="Dotted" BorderWidth="1px" />
                <Columns>

                    <asp:ButtonField ButtonType="Image" ImageUrl="~/img3/download.png"
                        Text="Ver" CommandName="AbreDocumento" HeaderText="Download">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="1px" HorizontalAlign="Center" />
                    </asp:ButtonField>

                    <asp:ButtonField ButtonType="Image" ImageUrl="~/img3/Del.png"
                        Text="Excluir" CommandName="ExcluiDocumento" HeaderText="Excluir" Visible="False">
                        <ItemStyle Width="1px" HorizontalAlign="Center" />
                    </asp:ButtonField>

                    <asp:BoundField DataField="DocumentoNome" HeaderText="Nome do Documento" SortExpression="DocumentoNome" />
                    <asp:BoundField DataField="Nome" HeaderText="Etapa" SortExpression="Nome" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#428bca" ForeColor="White" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BorderStyle="Dotted" BorderWidth="1px" BackColor="White" ForeColor="Gray" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT DocumentoNome, Nome FROM DemandasEtapas WHERE (DemandaEtapaId = @id) AND (CaminhoDocumento &lt;&gt; '')">
                <SelectParameters>
                    <asp:QueryStringParameter Name="id" QueryStringField="id" />
                </SelectParameters>
            </asp:SqlDataSource>

        </div>
        <br />
        <div style="padding: 10px">
            <asp:Label ID="lblDocumentacaoOK" runat="server" Text="Documentação OK?" Font-Bold="True"></asp:Label>
            <asp:RadioButtonList ID="RadioButtonListDocOK" CssClass="radio" runat="server" Enabled="False">
                <asp:ListItem>SIM</asp:ListItem>
                <asp:ListItem>NÃO</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Label ID="lblComentario" runat="server" Text="Comentário do atendente sobre o documento"></asp:Label>
            <br />
            <asp:TextBox ID="txtComentario" runat="server" Height="50px" Width="500px" BackColor="White" Enabled="False"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnGravar" CssClass="btn btn-primary" runat="server" Text="Gravar" OnClick="btnGravar_Click" Enabled="False" />
        </div>
    </form>
</body>
</html>
