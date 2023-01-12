<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="ActionPlan" %>

<%@ Register src="wucHeader.ascx" tagname="wucHeader" tagprefix="uc1" %>
<%@ Register src="wucMenu.ascx" tagname="wucMenu" tagprefix="uc2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style22
        {
            font-family: Tahoma;
            font-size: 10pt;
        }

        .auto-style26 {
            position: relative;
            top: 115px;
            left: -72px;
            height: 33px;
            width: 50px;
        }
        .auto-style27 {
            color: #0000CC;
        }

    </style>
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
            <td valign="top" width="100%" bgcolor="White" 
                style="background-color: #FFFFFF" >
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="padding: 10px">
                            <span class="style22"><strong>Relatórios</strong></span><br />
                            <br class="style22" />
                            <asp:LinkButton ID="btn01" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt" OnClick="btn01_Click">01-Demandas atrasadas (envia email)</asp:LinkButton>
                            <br />
                            <br class="style22" />
                            <asp:LinkButton ID="btn0" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt" OnClick="btn0_Click">02-Demandas próximas ao fim do prazo (envia email)</asp:LinkButton>
                            <br />
                            <br />
                            <asp:LinkButton ID="btn03" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt" PostBackUrl="~/Reports03DemandasAbertasAtendente.aspx">03-Demandas abertas por atendente (saturação)</asp:LinkButton>
                            <br />
                            <br />
                            <asp:LinkButton ID="btn04" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt" PostBackUrl="~/Reports04.aspx">04-Demandas atendidas por atendente</asp:LinkButton>
                            <br />
                            <br />
                            <asp:LinkButton ID="btn05" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt" PostBackUrl="~/Reports05.aspx">05-Satisfação com atendimento</asp:LinkButton>
                            <br />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div style="text-align: center">
                        <asp:Label ID="lblCarregando" runat="server" CssClass="auto-style27" Font-Names="Trebuchet MS" Font-Size="10pt" Text="Carregando..."></asp:Label>
                        <img alt="" src="img2/progresss.gif" class="auto-style26"/>
                        &nbsp;</div>
                </ProgressTemplate>
            </asp:UpdateProgress>
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


