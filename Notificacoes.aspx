<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Notificacoes.aspx.cs" Inherits="Notificacoes" %>

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
                            <div>
                                <div>
                                    <div>
                                        <hr />
                                        <asp:Label ID="lblTituloNotificacoes" runat="server" Font-Bold="True" Text="Notificações"></asp:Label>
                                    </div>
                                    <div style="padding: 10px">
                                        <asp:Repeater ID="Repeater1" runat="server" EnableTheming="True">
                                            <ItemTemplate>
                                                <div class="rptr">
                                                    <table>
                                                        <tr>
                                                            <th colspan="2"><%#Eval("Assunto") %></th>
                                                        </tr>
                                                        <tr>
                                                            <td>Notificação</td>
                                                            <td><%#Eval("Notificacao") %></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Data e Hora</td>
                                                            <td><%#Eval("DataHora") %></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# string.Format("~/img3/Notificacoes/Leitura/{0}.png",Eval("Lida"))%>'/>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:HyperLink ID="HyperLinkVerMais" runat="server" NavigateUrl='<%# Eval("URL") %>' >Ver mais</asp:HyperLink>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <div style="text-align: center; background-color: white; opacity: 0.9">
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

