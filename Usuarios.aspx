<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs" Inherits="Usuarios" %>

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
                                    <asp:Label ID="lblTitulo" runat="server" Style="font-weight: bold; font-size: 14px" Text="CADASTRO DE USUÁRIOS"></asp:Label>
                                </div>
                                <br />
                                <div>
                                    <asp:Button ID="btnNovoUsuario" CssClass="btn btn-primary" runat="server" Text="Novo usuário" PostBackUrl="~/UsuariosEditar.aspx" />

                                    &nbsp;<asp:Button ID="btnLogsUsuario" CssClass="btn btn-primary" runat="server" Text="Logs de Usuário" PostBackUrl="~/UsuariosLog.aspx" />
                                </div>
                                <hr />
                                <div>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3" EmptyDataText="Nenhum registro para ser exibido." GridLines="Horizontal" Style="font-family: Tahoma; font-size: 9pt; text-align: left" Width="98%" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>

                                            <asp:TemplateField Visible="False">
                                                <AlternatingItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                    <asp:ToggleButtonExtender ID="CheckBox1_ToggleButtonExtender" runat="server"
                                                        CheckedImageUrl="img2/tick-red.png" Enabled="True" ImageHeight="16"
                                                        ImageWidth="16" TargetControlID="CheckBox1"
                                                        UncheckedImageUrl="img2/_checkbox.png">
                                                    </asp:ToggleButtonExtender>
                                                </AlternatingItemTemplate>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                    <asp:ToggleButtonExtender ID="CheckBox1_ToggleButtonExtender" runat="server"
                                                        CheckedImageUrl="img2/tick-red.png" Enabled="True" ImageHeight="16"
                                                        ImageWidth="16" TargetControlID="CheckBox1"
                                                        UncheckedImageUrl="img2/_checkbox.png">
                                                    </asp:ToggleButtonExtender>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            </asp:TemplateField>


                                            <asp:ImageField DataImageUrlField="Bloqueado" DataImageUrlFormatString="~/img/usuarios/bloqueados/{0}.png">
                                                <ItemStyle Width="10px" />
                                            </asp:ImageField>


                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/img2/lupa_clean.PNG"
                                                Text="Comentários" CommandName="Seleciona" HeaderText="Abrir">
                                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            </asp:ButtonField>

                                            <asp:BoundField DataField="UserId" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="UserId" />

                                            <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                                            <asp:BoundField DataField="Setor" HeaderText="Setor" SortExpression="Setor" />
                                            <asp:BoundField DataField="Login" HeaderText="Login" SortExpression="Login" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"></asp:BoundField>

                                            <asp:BoundField DataField="UltimoAcesso" HeaderText="Último Acesso" SortExpression="UltimoAcesso"></asp:BoundField>


                                            <asp:BoundField DataField="Bloqueado" HeaderText="Bloqueado" SortExpression="Bloqueado" Visible="False" />

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

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT UserId, Nome, Setor, Login, Email, UltimoAcesso, Bloqueado FROM Usuarios ORDER BY Nome"></asp:SqlDataSource>
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

