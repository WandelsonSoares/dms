<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UsuariosLog.aspx.cs" Inherits="UsuariosLog" %>

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
                                    <asp:Label ID="lblTitulo" runat="server" Style="font-weight: bold; font-size: 14px" Text="LOGS DE ATIVIDADES DE USUÁRIOS"></asp:Label>
                                </div>
                                <br />
                                <div>
                                </div>
                                <hr />
                                <div>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3" EmptyDataText="Nenhum registro para ser exibido." GridLines="Horizontal" Style="font-family: Tahoma; font-size: 9pt; text-align: left" Width="98%" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" DataSourceID="SqlDataSource1" DataKeyNames="LogID" OnRowDataBound="GridView1_RowDataBound">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>

                                            <asp:BoundField DataField="LogID" HeaderText="LogID" InsertVisible="False" ReadOnly="True" SortExpression="LogID" />

                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />
                                            <asp:BoundField DataField="Tela" HeaderText="Tela" SortExpression="Tela" />
                                            <asp:BoundField DataField="Atividades" HeaderText="Atividades" SortExpression="Atividades" />
                                            <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data"></asp:BoundField>


                                        </Columns>
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Gray" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                        <RowStyle BackColor="White" BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" ForeColor="Gray" Height="30px" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                    </asp:GridView>

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT TOP (100) LogID, Usuario, Tela, Atividades, Data FROM UsuariosLog ORDER BY LogID DESC"></asp:SqlDataSource>
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
