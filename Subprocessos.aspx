<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Subprocessos.aspx.cs" Inherits="Subprocessos" %>

<%@ Register Src="wucMenu.ascx" TagName="wucMenu" TagPrefix="uc2" %>
<%@ Register Src="wucHeader.ascx" TagName="wucHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/wucTreeViewPrincipal.ascx" TagPrefix="uc1" TagName="wucTreeViewPrincipal" %>


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


                <td valign="top">
                    <div>
                        <asp:Label ID="lblEstrutura" runat="server" Text="Árvore Estrutural" Width="150px"></asp:Label>
                    </div>

                    <uc1:wucTreeViewPrincipal runat="server" ID="wucTreeViewPrincipal" />
                </td>

                <td class="conteudoPrincipal">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li role="presentation"><a href="#">BUs</a></li>
                                        <li role="presentation"><a href="#">Departamentos</a></li>
                                        <li role="presentation"><a href="#">Setores</a></li>
                                        <li role="presentation"><a href="#">Áreas</a></li>
                                        <li role="presentation"><a href="#">Processos</a></li>
                                        <li role="presentation" class="active"><a href="#">Subprocessos</a></li>
                                        <li role="presentation"><a href="#">Atividades</a></li>
                                        <li role="presentation"><a href="#">Etapas</a></li>
                                    </ul>
                                </div>
                                <div>
                                    <br />
                                    <table width="100%" style="height: 100%">
                                        <tr valign="top">
                                            <td style="width: 0%;">
                                                <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Height="100%" Width="400px" Visible="False">
                                                    <div style="vertical-align: central; height: 100%; padding: 4px">
                                                        <asp:Label ID="lblId" runat="server" CssClass="fontePadrao" Height="18px" Text="Id"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtId" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Height="18px" Width="50px"></asp:TextBox>

                                                        <br />

                                                        <asp:Label ID="lblNome" runat="server" CssClass="fontePadrao" Height="18px" Text="Nome"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtNome" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Height="18px" Width="300px"></asp:TextBox>


                                                        <br />

                                                        <asp:Label ID="lblProcesso" runat="server" CssClass="fontePadrao" Height="18px" Text="Processo"></asp:Label>
                                                        <br />

                                                        <asp:DropDownList ID="DropDownListProcesso" runat="server" Width="300px"></asp:DropDownList>

                                                        <br />

                                                        <br />
                                                        <asp:Label ID="lblResponsavel" runat="server" CssClass="fontePadrao" Height="18px" Text="Responsável"></asp:Label>
                                                        <br />

                                                        <asp:DropDownList ID="DropDownListResponsavel" runat="server" Width="300px"></asp:DropDownList>

                                                        <br />
                                                        <b />


                                                    </div>
                                                    <div style="text-align: center; padding: 4px">
                                                        <asp:Button ID="btnGravar" runat="server" CssClass="btn btn-primary" Text="Gravar" Width="100px" OnClick="btnGravar_Click" />
                                                        &nbsp;
                                                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-danger" Text="Cancelar" Width="100px" OnClick="btnCancelar_Click" />
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 100%; padding: 4px">
                                                <div>
                                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnAdicionar" CssClass="btn btn-primary" runat="server" Width="100px" Text="Novo" OnClick="btnAdicionar_Click" />
                                                    &nbsp;<asp:Button ID="btnExcluir" runat="server" CssClass="btn btn-danger" Text="Excluir" Width="100px" OnClick="btnExcluir_Click" />

                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>

                                                        <div>
                                                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#333333" GridLines="None" Width="100%" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns>

                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("SubprocessoId", "~/Atividades.aspx?id={0}")  %>' Text="Abrir"></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="20px" />
                                                                    </asp:TemplateField>

                                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/img/edit/Lapis.gif"
                                                                        Text="Ver" CommandName="Seleciona" HeaderText="Editar">
                                                                        <ItemStyle Width="1px" HorizontalAlign="Center" />
                                                                    </asp:ButtonField>

                                                                    <asp:TemplateField HeaderText="Selec">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                            <asp:ToggleButtonExtender ID="CheckBox2_ToggleButtonExtender" runat="server"
                                                                                CheckedImageUrl="img2/tick-red.png" Enabled="True" ImageHeight="16"
                                                                                ImageWidth="16" TargetControlID="CheckBox2"
                                                                                UncheckedImageUrl="img2/_checkbox.png">
                                                                            </asp:ToggleButtonExtender>
                                                                        </ItemTemplate>
                                                                        <ControlStyle Width="2px" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="2px" />
                                                                        <ItemStyle Width="2px" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="SubprocessoId" HeaderText="Id" ReadOnly="True" SortExpression="SubprocessoId">
                                                                        <ItemStyle Width="20px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                                                                    <asp:BoundField DataField="Processo" HeaderText="Processo" SortExpression="Processo" />
                                                                    <asp:BoundField DataField="Responsavel" HeaderText="Responsavel" SortExpression="Responsavel" />
                                                                </Columns>
                                                                <EditRowStyle BackColor="#999999" />
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                            </asp:GridView>
                                                        </div>
                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Subprocessos.SubprocessoId, Subprocessos.Nome, Processos.Nome AS Processo, Usuarios.Nome AS Responsavel FROM Processos INNER JOIN Subprocessos ON Processos.ProcessoId = Subprocessos.ProcessoId INNER JOIN Usuarios ON Subprocessos.ResponsavelId = Usuarios.UserID WHERE (Subprocessos.ProcessoId = @ProcessoId)">
                                                            <SelectParameters>
                                                                <asp:QueryStringParameter Name="ProcessoId" QueryStringField="id" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
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

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

