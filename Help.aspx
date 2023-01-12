<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="Help" %>


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
                            <div style="padding: 10px">
                                <asp:Label ID="lblTitulo0" runat="server" Text="HELP" Font-Bold="True"></asp:Label>
                                <br />
                                <hr />

                                <asp:Label ID="Label2" runat="server" Text="Sobre o sistema" Font-Bold="True"></asp:Label>
                                <br />
                                <div style="vertical-align: central">
                                    <table>
                                        <tr style="vertical-align: central">
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:LinkButton ID="btnLinkSobre" runat="server" OnClick="btnLinkSobre_Click">Sobre / Suporte</asp:LinkButton></td>
                                        </tr>
                                    </table>

                                </div>
                                <br />


                                <asp:Label ID="Label1" runat="server" Text="Vídeo aulas disponíveis" Font-Bold="True"></asp:Label>
                                <br />
                                <div style="vertical-align: central">
                                    <table>
                                        <tr style="vertical-align: central">
                                            <td>
                                                <asp:ImageButton ID="btn01Torial" runat="server" ImageUrl="~/img3/windows-media-player.png" PostBackUrl="~/Help1RegistrarDemanda.aspx" /></td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Help1RegistrarDemanda.aspx">Como registrar uma demanda</asp:LinkButton></td>
                                        </tr>
                                    </table>

                                </div>
                                <br />
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

