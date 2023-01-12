<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="shortcut icon" type="image/x-icon" href="/favicon.ico">
<head runat="server">
    <title>.:: DMS ::.</title>
    <style type="text/css">
        /* Image in Text Box */
        .tbLogin {
            text-align: left;
            padding-left: 10px;
        }

        .tbSenha {
            text-align: left;
            padding-left: 10px;
        }

        .cssClass1 {
            font-family: Tahoma;
            font-size: 7 px;
            background-color: Yellow;
        }

        .style5 {
            height: 52px;
            text-align: center;
            width: 486px;
        }

        .style8 {
        }

        .style10 {
            width: 48px;
            text-align: right;
            color: #005FFD;
        }

        .style12 {
            width: 223px;
        }

        .style22 {
            width: 253px;
            height: 12px;
        }

        .style6 {
            height: 54px;
            width: 486px;
        }

        .style18 {
            height: 12px;
            width: 110px;
            text-align: center;
        }

        #Button1 {
            width: 76px;
        }

        .style23 {
            height: 175px;
        }

        .style24 {
            width: 52px;
            height: 61px;
        }

        #img.source-image {
            width: 100%;
            position: absolute;
            top: 0;
            left: 0;
        }

        .auto-style2 {
            height: 12px;
            align-content: center;
            vertical-align: middle;
            text-align: center;
        }

        </style>

    <script type="text/javascript">
        function semerro() {
            return true;
        }
        window.onerror = semerro;
    </script>

    <script type="text/javascript">
        function TABEnter(oEvent) {
            var oEvent = (oEvent) ? oEvent : event;
            var oTarget = (oEvent.target) ? oEvent.target : oEvent.srcElement;
            if (oEvent.keyCode == 13)
                oEvent.keyCode = 9;
            if (oTarget.type == "text" && oEvent.keyCode == 13)
                //return false;
                oEvent.keyCode = 9;
            if (oTarget.type == "radio" && oEvent.keyCode == 13)
                oEvent.keyCode = 9;
        }
    </script>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />

</head>

<body style="margin-left: 0px; margin-right: 0px; margin-top: 0px; margin-bottom: 0px; background-color: #f5f5f5">

    <form id="form1" runat="server">

        <div align="center" style="text-align: center;">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server"
                AsyncPostBackTimeout="3600">
            </asp:ToolkitScriptManager>
            <asp:Panel ID="Panel1" runat="server">
                <table align="center">
                    <tr>
                        <td class="style23">
                            <table style="border: 1px solid #CCCCCC; border-style: solid; border-width: 1px; width: 435px; height: 176px;">
                                <tr>
                                    <td class="style5">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table border="0" cellpadding="2" cellspacing="2" align="center"
                                                    style="width: 315px">
                                                    <tr>
                                                        <td class="style8" colspan="3">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="12pt" Text="L O G I N"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style8" colspan="3">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;</td>
                                                        <td>
                                                            <div>
                                                                <span>
                                                                    <asp:Label ID="lbl_Login" runat="server"
                                                                        meta:resourcekey="lbl_LoginResource1"
                                                                        Text="Usuário"></asp:Label>
                                                                </span>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div>
                                                                <asp:TextBox ID="txtLogin" CssClass="tbLogin" runat="server" AutoPostBack="True"
                                                                    Font-Bold="True" Font-Italic="False" MaxLength="25"
                                                                    meta:resourcekey="txtLoginResource1" onKeyDown="TABEnter()"
                                                                    OnTextChanged="txtLogin_TextChanged" Width="100%"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="txtLogin_TextBoxWatermarkExtender"
                                                                    runat="server" Enabled="True" TargetControlID="txtLogin"
                                                                    WatermarkText="Digite seu usuário de rede.">
                                                                </asp:TextBoxWatermarkExtender>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style8">&nbsp;</td>
                                                        <td>
                                                            <span>
                                                                <asp:Label ID="lbl_Login0" runat="server"
                                                                    meta:resourcekey="lbl_LoginResource1" Text="Senha"></asp:Label>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSenha" CssClass="tbLogin" runat="server"
                                                                meta:resourcekey="txtSenhaResource1" OnTextChanged="txtSenha_TextChanged"
                                                                Style="font-weight: normal;" TextMode="Password" Width="100%" Font-Bold="True"></asp:TextBox>
                                                            <font face="Tahoma, Tahoma, Helvetica, sans-serif" size="1"></font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right" class="style8">&nbsp;</td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" meta:resourcekey="lbl_SenhaResource1" Text="Domínio"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style12">
                                                            <asp:DropDownList ID="dpd_Domains" runat="server" Font-Bold="False"
                                                                Font-Italic="False"
                                                                meta:resourcekey="dpd_DomainsResource1" Width="100%">
                                                                <asp:ListItem Selected="True">comaugroup.com</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td style="text-align: left">
                                                            <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" meta:resourcekey="CheckBox1Resource1"
                                                                Text="Lembrar minhas credenciais" Font-Bold="False" Font-Size="8pt" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style6" valign="top" width="100%">
                                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                            <tr>
                                                <td class="style22">&nbsp;</td>
                                                <td class="style18">
                                                    <asp:Button ID="btn_login" runat="server" Text="Entrar" PostBackUrl="~/AuthenticatingUser.aspx" BorderStyle="None" Style="color: #FFFFFF" CssClass="btn btn-primary" Width="150px" />
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr valign="baseline">
                                                <td class="auto-style2" colspan="3">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Panel ID="Panel_VIE" runat="server" BackColor="White"
                                BorderColor="#FF3300" BorderStyle="Solid" BorderWidth="1px"
                                Style="text-align: left" Visible="False" Width="430px">
                                <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial"
                                                Font-Size="8pt" Style="font-family: Verdana;"
                                                Text="ATENÇÃO!"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" Style="font-family: Verdana;"
                                                Text="Este sistema é compatível com Internet Explorer 9 ou superior."
                                                Width="99%"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:RoundedCornersExtender ID="Panel_VIE_RoundedCornersExtender"
                                runat="server" Enabled="True" TargetControlID="Panel_VIE">
                            </asp:RoundedCornersExtender>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:AlwaysVisibleControlExtender ID="Panel1_AlwaysVisibleControlExtender"
                runat="server" Enabled="True" HorizontalSide="Center"
                ScrollEffectDuration="0.3" TargetControlID="Panel1"
                VerticalOffset="20" VerticalSide="Middle">
            </asp:AlwaysVisibleControlExtender>
        </div>

        <%--<div align="center" style="text-align: center; background-color: #005FFD; height:50px; vertical-align:middle"; >

    <asp:Image ID="Image2" runat="server" Height="40px" ImageAlign="Middle" 
        ImageUrl="~/img2/plan2.png" />
    &nbsp;<asp:Label ID="lblTitulo" runat="server" ForeColor="White" 
        style="font-family: 'Trebuchet MS'; font-size: 16pt" 
        Text="Sistema de Planos de Ação"></asp:Label>

</div>--%>


        <div style="text-align: center; background-color: white; position: relative">
            <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%"
                bgcolor="White">
                <tr style="width: 30px">
                    <td align="left" width="100%" valign="middle" bgcolor="#3cbdfe">
                        <asp:Image ID="Image2" runat="server" Height="70px" ImageAlign="Middle"
                            ImageUrl="~/img2/LogoComau-2016.png" />
                        <asp:Label ID="lblTitulo" runat="server" ForeColor="White"
                            Style="font-size: 16pt" Text="DMS - Demand Management System" Font-Bold="False" Font-Names="Tahoma"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
