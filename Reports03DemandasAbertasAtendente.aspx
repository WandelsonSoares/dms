<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reports03DemandasAbertasAtendente.aspx.cs" Inherits="Reports03DemandasAbertasAtendente" %>

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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="padding: 10px">
                                <div>
                                    <asp:Label ID="lblTitulo0" runat="server" Text="Relatório 03 - Demandas abertas por atendente"></asp:Label>
                                </div>
                                <hr />
                                <div>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div style="vertical-align: central">
                                            <asp:Label ID="lblFiltros" runat="server" Text="Filtrar por período de encerramento." Height="20px"></asp:Label>
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
                                            &nbsp;<asp:Button ID="btnRemover" runat="server" CssClass="btn btn-primary" Text="Remover" Width="100px" OnClick="btnRemover_Click" />
                                            &nbsp;
                                            <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/img3/printer2.png" OnClick="btnImprimir_Click" />
                                            <br />
                                            <br />
                                        </div>
                                        <asp:GridView ID="GridView1" runat="server" CellPadding="4" Font-Size="10pt" ForeColor="#333333" Width="100%" AutoGenerateColumns="False" OnDataBound="GridView1_DataBound" EmptyDataText="Não há dados para exibição.">
                                            <Columns>
                                                <asp:BoundField DataField="Atendente" HeaderText="Atendente" SortExpression="Atendente" />
                                                <asp:BoundField DataField="Contrato" HeaderText="Contrato" SortExpression="Contrato" />
                                                <asp:BoundField DataField="Subprocesso" HeaderText="Subprocesso" SortExpression="Subprocesso" />
                                                <asp:BoundField DataField="Atividade" HeaderText="Atividade" SortExpression="Atividade" />
                                                <asp:BoundField DataField="Detalhe" HeaderText="Detalhe" SortExpression="Detalhe" />
                                                <asp:BoundField DataField="DataPrazo" DataFormatString="{0:d}" HeaderText="Data Prazo" SortExpression="DataPrazo" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#FFFFFF" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SqlDataSourceTodos" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Usuarios.Nome AS Atendente, Atividades.Nome AS Atividade, Subprocessos.Nome AS Subprocesso, Demandas.DataPrazo, Demandas.Detalhe, Contratos.DescContract AS Contrato, Demandas.Status FROM Usuarios INNER JOIN Demandas ON Usuarios.UserID = Demandas.ResponsavelId INNER JOIN Atividades ON Demandas.AtividadeId = Atividades.AtividadeId INNER JOIN Subprocessos ON Atividades.SubprocessoId = Subprocessos.SubprocessoId INNER JOIN Contratos ON Demandas.CNId = Contratos.ContratoID WHERE (Demandas.Status &lt;&gt; 'CONCLUIDA') AND (Demandas.Status &lt;&gt; 'CANCELADA') ORDER BY Subprocesso, Contrato, Demandas.DataPrazo"></asp:SqlDataSource>
                                        <asp:SqlDataSource ID="SqlDataSourceFiltroData" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Usuarios.Nome AS Atendente, Atividades.Nome AS Atividade, Subprocessos.Nome AS Subprocesso, Demandas.DataPrazo, Demandas.Detalhe, Contratos.DescContract AS Contrato, Demandas.Status FROM Usuarios INNER JOIN Demandas ON Usuarios.UserID = Demandas.ResponsavelId INNER JOIN Atividades ON Demandas.AtividadeId = Atividades.AtividadeId INNER JOIN Subprocessos ON Atividades.SubprocessoId = Subprocessos.SubprocessoId INNER JOIN Contratos ON Demandas.CNId = Contratos.ContratoID WHERE (Demandas.Status &lt;&gt; 'CONCLUIDA') AND (Demandas.Status &lt;&gt; 'CANCELADA') AND (Demandas.DataPrazo BETWEEN @DataIni AND @DataFIm) ORDER BY Subprocesso, Contrato, Demandas.DataPrazo">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="lblDataInicial" Name="DataIni" PropertyName="Text" />
                                                <asp:ControlParameter ControlID="lblDataInicial" Name="DataFIm" PropertyName="Text" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </asp:Panel>

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

