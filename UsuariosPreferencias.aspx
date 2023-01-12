<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UsuariosPreferencias.aspx.cs" Inherits="UsuariosPreferencias" %>

<%@ Register Src="wucMenu.ascx" TagName="wucMenu" TagPrefix="uc2" %>
<%@ Register Src="wucHeader.ascx" TagName="wucHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="Script/JavaScript.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="masterPageHeaderDiv">
        <uc1:wucHeader ID="wucHeader1" runat="server" />
    </div>

    <div style="width: 100%">
        <table width="100%" align="left" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <table cellpadding="0" cellspacing="0">
                        <tr class="masterPageMenuPrincipalTable">
                            <td valign="top">
                                <uc2:wucMenu ID="wucMenu1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="conteudoPrincipal">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="padding: 5px">
                                <div>
                                    <asp:Label ID="lblTitulo0" runat="server" Text="Preferências de Usuário" Font-Bold="True"></asp:Label>
                                    <hr />
                                    <asp:CheckBox ID="CheckBoxNaoExibirPainelEtapasAtrasadas" runat="server" Text="Não exibir painel de etapas atrasadas (na página inicial)" AutoPostBack="True" OnCheckedChanged="CheckBoxNaoExibirPainelEtapasAtrasadas_CheckedChanged" /> 
                                    <hr />
                                </div>
                                <div>
                                    <asp:Label ID="lblTitulo1" runat="server" Text="Contratos permitidos" Font-Bold="True"></asp:Label>
                                    <br />
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" DataSourceID="SqlDataSource1" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="ContratoID" HeaderText="ContratoID" SortExpression="ContratoID" >
                                            <ItemStyle Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" >
                                            <ItemStyle Width="600px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BU" HeaderText="BU" SortExpression="BU" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT UsuariosPermissoes.ContratoID, Contratos.DescContract AS Nome, BUs.Nome AS BU FROM Contratos INNER JOIN BUs ON Contratos.BUId = BUs.BUId INNER JOIN UsuariosPermissoes ON Contratos.ContratoID = UsuariosPermissoes.ContratoID WHERE (UsuariosPermissoes.ContratoID &gt; 0) AND (UsuariosPermissoes.UserID = @UserId) ORDER BY Nome">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="UserId" QueryStringField="id" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <div style="text-align: center">
                                        <img alt="" src="img2/progresss.gif" />
                                        <br />
                                        <asp:Label ID="lblCarregando2" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt" Text="Carregando..."></asp:Label>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>


</asp:Content>

