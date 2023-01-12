<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Email.aspx.cs" Inherits="ActionPlan" %>

<%@ Register src="wucHeader.ascx" tagname="wucHeader" tagprefix="uc1" %>
<%@ Register src="wucMenu.ascx" tagname="wucMenu" tagprefix="uc2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    </asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
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
            <td valign="top"  >
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3" valign="top">
                            <uc2:wucMenu ID="wucMenu1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td valign="top" width="100%" class="TD_PRINCIPAL" bgcolor="White" >
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td ID="btn1"  height="20" width="100%">
                                    <table cellpadding="0" cellspacing="0" 
                                        style="border-right: 1px solid #C0C0C0; padding: 0px; margin: 0px; height: 27px;" 
                                        width="100%">
                                        <tr>
                                            <td onmouseout="this.bgColor=''" onmouseover="this.bgColor='#FFDF5E'" 
                                                style="text-align: center; background-color: #FFFFFF;">
                                                <table border="0" cellpadding="1" cellspacing="1" style="float: left">
                                                    <tr style="height: 8px">
                                                        <td width=" " style="width: 10px">
                                                            &nbsp;</td>
                                                        <td width=" ">
                                                            <asp:Label ID="Label46" runat="server" Text="Email destinatário"></asp:Label>
                                                            <asp:TextBox ID="txtEmailDestinatario" runat="server" Height="20px" 
                                                                Width="239px" style="text-align: left"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:8px">
                                                        <td style="width: 10px" width=" ">
                                                            &nbsp;</td>
                                                        <td width=" " style="text-align: right">
                                                            <asp:Label ID="Label48" runat="server" Text="Mensagem"></asp:Label>
                                                            <asp:TextBox ID="txtMensagem" runat="server" Height="20px" 
                                                                Width="239px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:8px">
                                                        <td style="width: 10px" width=" ">
                                                            &nbsp;</td>
                                                        <td width=" " style="text-align: right">
                                                            <asp:Label ID="Label47" runat="server" Text="Assunto"></asp:Label>
                                                            <asp:TextBox ID="txtAssunto" runat="server" Height="20px" Width="239px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:8px">
                                                        <td style="width: 10px" width=" ">
                                                            &nbsp;</td>
                                                        <td width=" " style="text-align: right">
                                                            <asp:Label ID="Label50" runat="server" Text="SmtpHostIP"></asp:Label>
                                                            <asp:TextBox ID="txtSmtpHostIP" runat="server" Height="20px" Width="239px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:8px">
                                                        <td style="width: 10px" width=" ">
                                                            &nbsp;</td>
                                                        <td width=" " style="text-align: right">
                                                            <asp:Label ID="Label49" runat="server" Text="SmtpCliente"></asp:Label>
                                                            <asp:TextBox ID="txtSmtpCliente" runat="server" Height="20px" Width="239px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:8px">
                                                        <td style="width: 10px" width=" ">
                                                            &nbsp;</td>
                                                        <td width=" " style="text-align: right">
                                                            <asp:Label ID="Label53" runat="server" Text="EmailRemetente"></asp:Label>
                                                            <asp:TextBox ID="txtEmailRemetente" runat="server" Height="20px" Width="239px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:8px">
                                                        <td style="width: 10px" width=" ">
                                                            &nbsp;</td>
                                                        <td width=" " style="text-align: right">
                                                            <asp:Label ID="Label51" runat="server" Text="SmtpPorta"></asp:Label>
                                                            <asp:TextBox ID="txtSmtpPorta" runat="server" Height="20px" Width="239px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:8px">
                                                        <td style="width: 10px" width=" ">
                                                            &nbsp;</td>
                                                        <td width=" ">
                                                            <asp:Label ID="Label52" runat="server" Text="Senha Remetente"></asp:Label>
                                                            <asp:TextBox ID="txtSenhaRemetente" runat="server" Height="20px" Width="239px" 
                                                                TextMode="Password"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="height:8px">
                                                        <td style="width: 10px" width=" ">
                                                            &nbsp;</td>
                                                        <td width=" ">
                                                            <asp:Button ID="btnEnviarEmail" runat="server" onclick="btnEnviarEmail_Click" 
                                                                Text="Enviar Email" />
                                                        </td>
                                                    </tr>
                                                    <tr style="height:8px">
                                                        <td style="width: 10px" width=" ">
                                                            &nbsp;</td>
                                                        <td style="text-align: left" width=" ">
                                                            <asp:Label ID="lblStatus" runat="server" Text="Status de envio."></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td valign="top" colspan="2" background="img2/header/spacetop2B.png">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            <img alt="" src="img2/header/spacetop2B_qe.png" /></td>
                        <td>
                            <img alt="" src="img2/header/spacetop2B.png" /></td>
                        <td style="text-align: right">
                            <img alt="" src="img2/header/spacetop2B_qd.png" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </asp:Content>


