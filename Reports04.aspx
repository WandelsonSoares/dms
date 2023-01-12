<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reports04.aspx.cs" Inherits="Reports04" %>

<%@ Register Src="wucMenu.ascx" TagName="wucMenu" TagPrefix="uc2" %>
<%@ Register Src="wucHeader.ascx" TagName="wucHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="Script/JavaScript.js"></script>

    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="masterPageHeaderDiv">
        <uc1:wucHeader ID="wucHeader1" runat="server" />
    </div>

    <div style="width: 100%">
        <table width="100%" cellpadding="0" cellspacing="0">
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
                                    <asp:Label ID="lblTitulo0" runat="server" Text="Relatório 04 - Demandas Atendidas por Atendente" Font-Bold="True"></asp:Label>
                                </div>
                                <hr />
                                <div>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div style="vertical-align: central">
                                            <asp:Label ID="lblFiltros" runat="server" Text="Filtrar período de vencimento:" Height="20px"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblDataInicial" runat="server" Text="Início" Height="20px"></asp:Label>
                                            <asp:TextBox ID="txtDataInicio" runat="server" Height="20px" Width="75px"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtDataInicio_CalendarExtender" runat="server" BehaviorID="txtDataInicio_CalendarExtender" TargetControlID="txtDataInicio">
                                            </asp:CalendarExtender>
                                            &nbsp;
                                           <asp:Label ID="lblDataFim" runat="server" Text="Fim" Height="20px"></asp:Label>
                                            <asp:TextBox ID="txtDataFim" runat="server" Height="20px" Width="75px"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtDataFim_CalendarExtender" runat="server" BehaviorID="txtDataFim_CalendarExtender" TargetControlID="txtDataFim">
                                            </asp:CalendarExtender>
                                            &nbsp;<asp:Button ID="btnAplicar" CssClass="btn btn-primary" runat="server" Text="Aplicar" Width="100px" OnClick="btnAplicar_Click" />
                                            &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/img3/printer2.png" OnClick="btnImprimir_Click" />
                                            <br />
                                        </div>
                                        <br />
                                        <div>
                                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" Font-Size="10pt" ForeColor="#333333" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="Não há dados para exibição.">
                                                <Columns>
                                                    <asp:BoundField DataField="Atendente" HeaderText="Atendente" SortExpression="Atendente">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DemandasRecebidas" HeaderText="Recebidas">
                                                        <HeaderStyle CssClass="headerTextCenter" />
                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DemandasVencendo" HeaderText="Vencendo">
                                                        <HeaderStyle  CssClass="headerTextCenter" />
                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DemandasAtendidas" HeaderText="Atendidas">
                                                        <HeaderStyle  CssClass="headerTextCenter" />
                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DemandasAtendidasPrazo" HeaderText="Atendidas Dentro do Prazo">
                                                        <HeaderStyle  CssClass="headerTextCenter" />
                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DemandasAtendidasForaPrazo" HeaderText="Atendidas Fora do Prazo">
                                                        <HeaderStyle  CssClass="headerTextCenter" />
                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DemandasNaoAtendidas" HeaderText="Não Atendidas">
                                                        <HeaderStyle  CssClass="headerTextCenter" />
                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#FFFFFF" />
                                                <AlternatingRowStyle BackColor="#f0f0f0" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </div>
                                        <br />
                                        <asp:Label ID="lblNotaTitulo" runat="server" Text="Notas sobre o relatório:" Font-Bold="True" Font-Size="8pt"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblNotaRecebidas" runat="server" Text="Recebidas: total de demandas cadastradas no período." Font-Size="8pt"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblNotaVencendo" runat="server" Text="Vencendo: demandas com data prazo no período selecionado." Font-Size="8pt"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblNotaAtendidas" runat="server" Text="Atendidas: demandas com data prazo no período selecionado e que já foram atendidas." Font-Size="8pt"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblNotaDentroPrazo" runat="server" Text="Dentro do prazo: demandas com data prazo no período selecionado e que foram atendidas dentro do prazo." Font-Size="8pt"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblNotaForaPrazo" runat="server" Text="Fora do Prazo: demandas com data prazo no período selecionado e que foram atendidas fora do prazo." Font-Size="8pt"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblNotaNaoAtendidas" runat="server" Text="Não Atendidas: demandas com data prazo no período selecionado e que ainda não foram atendidas." Font-Size="8pt"></asp:Label>
                                    </asp:Panel>

                                </div>
                                <div id="chart_div" style="width: 500px; height: 400px">
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

