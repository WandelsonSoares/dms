<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucMenu.ascx.cs" Inherits="wucMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/wucTreeViewPrincipal.ascx" TagPrefix="uc1" TagName="wucTreeViewPrincipal" %>


<link href="css/StyleSheet.css" rel="stylesheet" />

<style type="text/css">
    a:link {
        text-decoration: none;
    }

    a:visited {
        text-decoration: none;
    }

    a:hover {
        text-decoration: underline;
        color: #00b0f2;
    }

    a:active {
        text-decoration: none;
    }

    .style4 {
        width: 18px;
    }

    .style20 {
        width: 19px;
    }


    /* Larger Side Border */
    .tb8 {
        border: 1px solid #3366FF;
        border-left: 4px solid #3366FF;
    }
    /* Larger Side Border */
    .tb9 {
        border: 1px solid #808080;
        border-left: 4px solid Silver;
    }

    .style21 {
    }
</style>

<body style="margin-top: 0px; margin-left: 0px; margin-right: 0px">
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="masterPageMenuPrincipalTable" style="height: 100%">
                    <tr style="vertical-align: top;">
                        <td rowspan="2" valign="top">
                            <asp:Panel ID="Panel_redimension" runat="server">
                                <asp:Panel ID="PanelHome" runat="server" Width="210px">
                                    <table cellpadding="2" cellpadding="2" cellspacing="2" cellspacing="2"
                                        width="100%">
                                        <tr>
                                            <td class="style4"></td>
                                            <td class="style20">
                                                <img alt="" src="img3/home.png" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButtonHome" runat="server" CssClass="masterPageMenuPrincipalLinkButton" Width="80%" PostBackUrl="~/Home.aspx">Home</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="PanelDemandas" runat="server" Width="210px">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                    <tr>
                                                        <td class="style4"></td>
                                                        <td class="style20">
                                                            <img alt="" src="img3/basket.png" />
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="LinkButtonDemandas" runat="server" CssClass="masterPageMenuPrincipalLinkButton"
                                                                Enabled="False" PostBackUrl="~/Demandas.aspx">Demandas</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="PanelRelatorios" runat="server" Width="210px" CssClass="style21"
                                    Height="37px">
                                    <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                        <tr>
                                            <td class="style4"></td>
                                            <td class="style20">
                                                <img width="25px" alt="" src="img3/report.png" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButtonRelatorios" runat="server" CssClass="masterPageMenuPrincipalLinkButton"
                                                    Enabled="False" PostBackUrl="~/Reports.aspx">Relatórios</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="PanelConfiguracoes" runat="server" Width="210px" CssClass="style21"
                                    Height="37px">
                                    <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                        <tr>
                                            <td class="style4"></td>
                                            <td class="style20">
                                                <img width="25px" alt="" src="img3/gear.png" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButtonConfiguracoes" runat="server" CssClass="masterPageMenuPrincipalLinkButton"
                                                    Enabled="False" PostBackUrl="~/Configuracoes.aspx">Configurações</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="PanelConfig" runat="server" Width="210px">
                                    <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                        <tr>
                                            <td class="style4"></td>
                                            <td class="style20">
                                                <img alt="" src="img3/user.png" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButtonUsuarios" runat="server" BorderColor="Transparent"
                                                    CssClass="masterPageMenuPrincipalLinkButton"
                                                    Enabled="False" PostBackUrl="~/Usuarios.aspx">Usuários</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="PanelHelp" runat="server" Width="210px">
                                    <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                        <tr>
                                            <td class="style4"></td>
                                            <td class="style20">
                                                <img alt="" src="img3/help-information.png" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButtonHelp" runat="server" BorderColor="Transparent"
                                                    CssClass="masterPageMenuPrincipalLinkButton" PostBackUrl="~/Help.aspx">Help</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:Panel>
                            <asp:CollapsiblePanelExtender ID="Panel_redimension_CollapsiblePanelExtender"
                                runat="server" CollapseControlID="Image1"
                                CollapsedImage="~/img2/SideMenu/btn3.png" CollapsedSize="0" Enabled="True"
                                ExpandControlID="Image1" ExpandDirection="Horizontal" ExpandedSize="215"
                                TargetControlID="Panel_redimension"
                                ExpandedImage="~/img3/left.png" ImageControlID="Image1">
                            </asp:CollapsiblePanelExtender>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</body>
