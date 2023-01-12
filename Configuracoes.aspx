<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Configuracoes.aspx.cs" Inherits="Configuracoes" %>

<%@ Register Src="wucMenu.ascx" TagName="wucMenu" TagPrefix="uc2" %>
<%@ Register Src="wucHeader.ascx" TagName="wucHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Script/JavaScript.js"></script>
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
                            <div style="padding: 10px">
                                <div>
                                    <asp:Label ID="lblTitulo" runat="server" Style="font-weight: bold; font-size: 14px;" Text="Configurações do sistema"></asp:Label>
                                </div>
                                <br />
                                <div>
                                    <asp:Button ID="btnEstruturaOrganizacional" CssClass="btn btn-primary" runat="server" PostBackUrl="~/Bus.aspx?id=1" Text="Processos e Prazos" Enabled="False" Width="250px" />
                                    &nbsp;<asp:Button ID="btnFuncionarios" runat="server" CssClass="btn btn-primary" PostBackUrl="~/Funcionarios.aspx" Text="Funcionários" Width="250px" />
                                    &nbsp;<asp:Button ID="btnContratos" runat="server" CssClass="btn btn-primary" PostBackUrl="~/Contratos.aspx" Text="Contratos" Width="250px" />
                                    &nbsp;<asp:Button ID="btnFeriados" runat="server" CssClass="btn btn-primary" PostBackUrl="~/Feriados.aspx" Text="Feriados" Width="250px" />
                                </div>
                                <br />
                                <div style="padding: 5px; border-color: lightgray; border-style: solid; border-width: 1px">
                                    <asp:Label ID="lblConfiguracoesEmail" runat="server" Text="Configurações de envio de email"></asp:Label>
                                    <br />
                                    <asp:CheckBox ID="CheckBoxEnviarEmailDemandasCriacao" class="checkbox" runat="server" Text="Enviar email de notificação quando for criada uma nova demanda." AutoPostBack="True" OnCheckedChanged="CheckBoxEnviarEmailDemandasCriacao_CheckedChanged" />
                                    <asp:CheckBox ID="CheckBoxEnviarEmailDemandasAtualizacao" class="checkbox" runat="server" Text="Enviar email de notificação quando uma demanda for atualizada." AutoPostBack="True" OnCheckedChanged="CheckBoxEnviarEmailDemandasAtualizacao_CheckedChanged" />
                                    <asp:CheckBox ID="CheckBoxEnviarEmailDemandasAtrasadas" class="checkbox" runat="server" Text="Enviar email com demandas atrasadas." AutoPostBack="True" OnCheckedChanged="CheckBoxEnviarEmailDemandasAtrasadas_CheckedChanged" />
                                    <br />
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

