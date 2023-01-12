<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Funcionarios.aspx.cs" Inherits="Funcionarios" %>

<%@ Register Src="wucHeader.ascx" TagName="wucHeader" TagPrefix="uc1" %>
<%@ Register Src="wucMenu.ascx" TagName="wucMenu" TagPrefix="uc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/StyleSheet.css" type="text/css" />

    <style type="text/css">
        .auto-style2 {
            s font-size: x-small;
        }

        .auto-style3 {
            font-size: 9pt;
        }

        </style>
</asp:Content>


<asp:Content ID="Content2" runat="server"
    ContentPlaceHolderID="ContentPlaceHolder1">

    <link rel="stylesheet" href="css/StyleSheet.css" type="text/css">

    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <uc1:wucHeader ID="wucHeader1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <table bgcolor="White" width="100%" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="3" valign="top">
                                        <uc2:wucMenu ID="wucMenu1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" width="100%" class="TD_PRINCIPAL" bgcolor="White">
                            <div style="padding: 10px">

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>

                                        <%--<script language="jscript" src="~/Script/JavaScript.js" type="text/jscript"> </script>--%>

                                        <script type="text/javascript">

                                            function apagarMensagem() {
                                                timeoutID = setTimeout("hideDiv()", 5000);
                                            }

                                            function hideDiv() {
                                                document.getElementById('mensagem').style.display = "none";
                                            }

                                        </script>


                                        <div>

                                            <asp:Label ID="Label61" runat="server" Text="Funcionários" Font-Bold="False" Font-Names="Trebuchet MS" Font-Size="11pt"></asp:Label>

                                            <br />

                                        </div>

                                        <div>

                                            <div class="col-md-6">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="10pt" Text="Filtros"></asp:Label>
                                                <br />
                                                <asp:Label ID="Label60" runat="server" Font-Size="9pt" ForeColor="#333333" Text="Nome:" CssClass="auto-style3"></asp:Label>
                                                &nbsp;<br />
                                                <asp:TextBox ID="txtNomeFiltro" runat="server" Font-Names="Trebuchet MS" Style="color: #666666; font-size: 9pt" Width="200px" OnTextChanged="txtNomeFiltro_TextChanged"></asp:TextBox>

                                                &nbsp;&nbsp;&nbsp;<br />
                                                <asp:Label ID="Label62" runat="server" CssClass="auto-style3" Font-Size="9pt" ForeColor="#333333" Text="Matrícula:"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtMatriculaFiltro" runat="server" Font-Names="Trebuchet MS" Style="color: #666666; font-size: 9pt" Width="200px" OnTextChanged="txtMatriculaFiltro_TextChanged"></asp:TextBox>

                                                &nbsp;&nbsp;
                                <br />
                                                &nbsp;<asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" Width="150px" OnClick="btnFiltrar_Click" CssClass="btn btn-primary" />
                                                &nbsp;<asp:Button ID="btnRemoverFiltro" runat="server" Text="Remover filtro" Width="150px" CssClass="btn btn-danger" />
                                                <br />
                                            </div>

                                            <br />
                                            <asp:Button ID="btnImportar" CssClass="btn btn-primary" runat="server" Text="Importar funcionários" OnClick="btnImportar_Click" />
                                            <br />
                                            <br />

                                            <div style="padding-left: 5px">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="auto-style2" DataKeyNames="Matricula" ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" PageSize="15" OnRowCommand="GridView1_RowCommand">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/img/icon_mini_search.gif"
                                                            Text="Ver" CommandName="Seleciona">
                                                            <ItemStyle Width="1px" HorizontalAlign="Center" />
                                                        </asp:ButtonField>

                                                        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                                                        <asp:BoundField DataField="Matricula" HeaderText="Matricula" ReadOnly="True" SortExpression="Matricula" />
                                                        <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                        <asp:BoundField DataField="Cargo" HeaderText="Cargo" SortExpression="Cargo" />
                                                        <asp:BoundField DataField="CentroCusto" HeaderText="CentroCusto" SortExpression="CentroCusto" />
                                                        <asp:BoundField DataField="Situacao" HeaderText="Situacao" SortExpression="Situacao" />
                                                        <asp:BoundField DataField="Ativo" HeaderText="Ativo" SortExpression="Ativo" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="SqlDataSourceGeral100" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT TOP (100) [Id], [Matricula], [Nome], [Email], [Cargo], [CentroCusto], [Situacao], [Ativo] FROM [Funcionarios] ORDER BY [Nome]"></asp:SqlDataSource>
                                                <asp:SqlDataSource ID="SqlDataSourceFiltraNome" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Id, Matricula, Nome, Email, Cargo, CentroCusto, Situacao, Ativo FROM Funcionarios WHERE (Nome LIKE @Nome) ORDER BY Nome">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtNomeFiltro" Name="Nome" PropertyName="Text" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:SqlDataSource ID="SqlDataSourceFiltraMatricula" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Id, Matricula, Nome, Email, Cargo, CentroCusto, Situacao, Ativo FROM Funcionarios WHERE (Matricula = @Matricula) ORDER BY Nome">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="txtMatriculaFiltro" Name="Matricula" PropertyName="Text" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                            <br />

                                        </div>
                                        <div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


