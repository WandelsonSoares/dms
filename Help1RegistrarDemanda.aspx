<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Help1RegistrarDemanda.aspx.cs" Inherits="Help1RegistrarDemanda" %>

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
                <td class="conteudoPrincipal" style="background-color: white">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div>
                                <div>
                                    <asp:Label ID="Label1" runat="server" Text="Como registrar demandas" CssClass="fontePadrao" style="font-size: 14px; font-weight: bold"></asp:Label>
                                </div>
                                <hr />
                                <div style="text-align: center">
                                    <video style="border: 2px solid gray" src="img3/DMS-Tutorial-Como-registrar-demandas.mp4" id="Video" controls>
                                        This browser or mode doesn't support HTML5 video.
                                 </video>
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

