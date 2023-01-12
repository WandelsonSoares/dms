<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Demandas.aspx.cs" Inherits="Demandas" %>

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
                    <div style="padding: 2px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div>
                                    <asp:Button ID="btnNovaDemanda" CssClass="btn btn-primary" runat="server" Text="Nova Demanda" PostBackUrl="~/DemandasNova.aspx" Width="150px" Enabled="False" />
                                    &nbsp;
                                    <asp:Button ID="btnAlterarResponsavel" CssClass="btn btn-primary" runat="server" Text="Alterar Responsável" Width="150px" OnClick="AlterarResponsavel_Click" Enabled="False" />
                                    &nbsp;
                                    <asp:Button ID="btnAplicarFiltro" CssClass="btn btn-primary" Width="150px" runat="server" Text="Aplicar filtro" OnClick="btnAplicarFiltro_Click" Style="height: 36px" />
                                    &nbsp;
                                    <asp:Button ID="btnRemoverFiltro" CssClass="btn btn-primary" Width="150px" runat="server" Text="Remover filtro" OnClick="btnRemoverFiltro_Click" />
                                </div>
                                <hr />

                                <div class="col-md-2" style="padding-left: 0px">
                                    <asp:Label ID="lblArea" runat="server" Text="Área"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="DropDownListArea" runat="server" Width="150px"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblResponsavel" runat="server" Text="Responsavel"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="DropDownListResponsavel" runat="server" Width="150px"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblSolicitante" runat="server" Text="Solicitante"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="DropDownListSolicitante" runat="server" Width="150px"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="DropDownListStatus" runat="server" Width="150px">
                                        <asp:ListItem Selected="True">TODOS</asp:ListItem>
                                        <asp:ListItem>AGUARDANDO</asp:ListItem>
                                        <asp:ListItem>ATRASADA</asp:ListItem>
                                        <asp:ListItem>CANCELADA</asp:ListItem>
                                        <asp:ListItem>CONCLUIDA</asp:ListItem>
                                        <asp:ListItem>PROCESSANDO</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblContrato" runat="server" Text="Contrato"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="DropDownListContrato" runat="server" Width="150px"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblOrdernarPor" runat="server" Text="Ordenar por"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="DropDownListOrdenarPor" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListOrdenarPor_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="DataPrazo">Data Prazo</asp:ListItem>
                                        <asp:ListItem Value="DataAbertura">Data Abertura</asp:ListItem>
                                        <asp:ListItem Value="Prioridade">Prioridade</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6" style="padding: 0px;">
                                    <asp:CheckBox ID="CheckBoxOcultarConcluidas" CssClass="checkbox" runat="server" Checked="True" ForeColor="Blue" Text="Ocultar Concluídas e Canceladas" AutoPostBack="True" OnCheckedChanged="CheckBoxOcultarConcluidas_CheckedChanged" />
                                </div>
                                <div>
                                    <asp:Label ID="lblTitulo2" Style="font-weight: bold" runat="server" Text="Demandas" Width="100%"></asp:Label>
                                </div>
                                <div>


                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3" EmptyDataText="Nenhum registro para ser exibido." GridLines="Horizontal" Style="font-family: Tahoma; font-size: 8pt; text-align: left" Width="98%" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
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


                                            <asp:ImageField DataImageUrlField="Status" DataImageUrlFormatString="~/img/demandasEtapasStatus/{0}.png">
                                                <ItemStyle Width="10px" />
                                            </asp:ImageField>


                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/img2/lupa_clean.PNG"
                                                Text="Comentários" CommandName="Seleciona" HeaderText="Abrir">
                                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            </asp:ButtonField>

                                            <asp:BoundField DataField="DemandaId" HeaderText="Código" SortExpression="DemandaId" InsertVisible="False" ReadOnly="True" />
                                            <asp:BoundField DataField="Detalhe" HeaderText="Detalhe" SortExpression="Detalhe" />
                                            <asp:BoundField DataField="Contrato" HeaderText="Contrato" />
                                            <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" SortExpression="Solicitante" />
                                            <asp:BoundField DataField="Processo" HeaderText="Processo" SortExpression="Processo"></asp:BoundField>

                                            <asp:BoundField DataField="Responsavel" HeaderText="Responsável" SortExpression="Responsavel"></asp:BoundField>


                                            <asp:BoundField DataField="DataAbertura" HeaderText="Abertura" SortExpression="DataAbertura" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="DataPrazo" HeaderText="Prazo" SortExpression="DataPrazo" DataFormatString="{0:d}"></asp:BoundField>

                                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                            <asp:BoundField DataField="DataEncerramento" DataFormatString="{0:d}" HeaderText="Conclusão" SortExpression="DataEncerramento" />
                                            <asp:ImageField DataImageUrlField="PrioridadeId" ItemStyle-HorizontalAlign="Center" DataImageUrlFormatString="~/img/demandasPrioridades/{0}.png">
                                                <ItemStyle Width="10px" />
                                            </asp:ImageField>

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

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Demandas.DemandaId, Demandas.Detalhe, Usuarios.Nome AS Solicitante, Processos.Nome AS Processo, Usuarios_1.Nome AS Responsavel, Demandas.DataAbertura, Demandas.DataPrazo, Demandas.Status, Demandas.DataEncerramento FROM Usuarios AS Usuarios_1 INNER JOIN Processos INNER JOIN Demandas ON Processos.ProcessoId = Demandas.ProcessoId INNER JOIN Usuarios ON Demandas.SolicitanteId = Usuarios.UserID ON Usuarios_1.UserID = Demandas.ResponsavelId"></asp:SqlDataSource>

                                </div>

                                <div>
                                    <asp:Panel ID="PanelResponsaveis" runat="server" BackColor="#FFFFCC" Height="150px" Width="400px" HorizontalAlign="Center" Visible="False">
                                        <asp:Label ID="lblNovoResponsavelInstrucao" Font-Size="12px" runat="server" Text="Selecion as demandas e o novo responsável."></asp:Label>
                                        <br />
                                        <asp:Label ID="lblResponsavelNovo" runat="server" Text="Novo Responsável"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListResponsavelNovo" runat="server" Width="300px">
                                        </asp:DropDownList>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnNovoResponsavelGravar" CssClass="btn btn-primary" Width="100px" runat="server" Text="Gravar" OnClick="btnNovoResponsavelGravar_Click" />

                                        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelarStatus" CssClass="btn btn-danger" Width="100px" runat="server" Text="Fechar" OnClick="btnCancelarStatus_Click" />

                                    </asp:Panel>


                                    <asp:AlwaysVisibleControlExtender ID="PanelResponsaveis_AlwaysVisibleControlExtender" runat="server" BehaviorID="PanelResponsaveis_AlwaysVisibleControlExtender" HorizontalSide="Center" TargetControlID="PanelResponsaveis">
                                    </asp:AlwaysVisibleControlExtender>


                                </div>

                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <div style="text-align: center; background-color: #3cbdfe; width: 250px; height: 50px; vertical-align: central">
                                            <img alt="" src="img2/progresss.gif" />
                                            <br />
                                            <asp:Label ID="lblCarregando2" runat="server" Font-Names="Trebuchet MS" ForeColor="White" Font-Size="10pt" Text="Carregando..."></asp:Label>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:AlwaysVisibleControlExtender ID="UpdateProgress1_AlwaysVisibleControlExtender"
                                    runat="server" Enabled="True" HorizontalOffset="600"
                                    TargetControlID="UpdateProgress1" VerticalOffset="300">
                                </asp:AlwaysVisibleControlExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

