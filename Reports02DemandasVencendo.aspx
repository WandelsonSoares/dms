<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Reports02DemandasVencendo.aspx.cs" Inherits="Reports02DemandasVencendo" %>

<!DOCTYPE html>



<script type="text/javascript">

    function Confirmacao() {

        if (confirm("Confirma essa operação?")) {

            return true;

        } else {

            return false;

        }

    }

</script>

<script src="Scripts/bootstrap.min.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="font-family: Tahoma; font-size: 12pt; font-weight: bold">
                <asp:Label ID="lblTitulo" runat="server" Text="Lista de Demandas próximas ao fim do prazo" Font-Names="Arial"></asp:Label>
            </div>
            <br />
            <asp:Label ID="lblDiasVencimento" runat="server" Text="Dias para o vencimento: " Font-Bold="True" Font-Names="Arial" ForeColor="Black" Height="24px" Font-Size="10pt"></asp:Label><strong>
                <br />
                <asp:DropDownList ID="dpdDias" runat="server" Font-Names="Arial" Font-Size="10pt" Height="24px" Width="50px" BackColor="#FFFFCC" Font-Bold="True" OnSelectedIndexChanged="dpdDias_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem Selected="True">2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList>

            </strong>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <strong>
                <asp:Label ID="lblArea" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Área"></asp:Label>
            </strong>
            <br />
            <div>
                <asp:DropDownList ID="DropDownListArea" runat="server" OnSelectedIndexChanged="DropDownListArea_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            </div>
            <br />
            <asp:Button ID="btnExibirOcultarOpcoesEmail" runat="server" OnClick="btnExibirOcultarOpcoesEmail_Click" Text="Exibir / Ocultar Opções de Email" Visible="False" />
            <br />

            <asp:Panel ID="Panel1" runat="server" Visible="False" BackColor="#F0F0F0">
                <div style="border-color: lightgray; border-width: 1px; border-style: solid; padding: 5px">
                    <div style="font-family: Tahoma; font-size: 10pt">
                        <strong>
                            <asp:Label ID="lblAvisodestinatarios" runat="server" Text="Selecione abaixo quem deve receber o email:"></asp:Label>
                        </strong>
                        <br />
                        <asp:CheckBox ID="CheckBoxResponsavelSetor" runat="server" Text="Responsável Setor" Checked="True" /><asp:CheckBox ID="CheckBoxResponsavelArea" runat="server" Text="Responsável pela Área" Checked="True" />
                    </div>
                    <br />
                    <div style="font-family: Tahoma; font-size: 10pt">
                        <strong>
                            <asp:Label ID="lblCabecalhoEmail" runat="server" Text="Cabeçalho do email:"></asp:Label>
                        </strong>
                        <br />
                        <asp:TextBox ID="txtCabecalhoEmail" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="8pt" Height="80px"
                            TextMode="MultiLine" Width="90%">RELATÓRIO DE DEMANDAS PRÓXIMAS AO FIM DO PRAZO

ATENÇÃO! As demandas abaixo estão próximas ao fim do prazo.</asp:TextBox>
                    </div>

                    <div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <strong>
                                    <asp:Label ID="Label43" runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="Status de email:"></asp:Label>
                                </strong>
                                <asp:Image ID="imgEmail" runat="server" ImageUrl="~/img2/email.gif" Visible="False" />
                                <font face="Tahoma, Tahoma, Helvetica, sans-serif" size="3"><span style="font-family: Tahoma"><span style="font-size: 10pt"><span style="FONT-SIZE: 10pt">
                            <asp:Label ID="lblEnviando" runat="server" Text="Enviando..." Visible="False"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtStatus" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" Font-Names="Trebuchet MS" Font-Size="8pt" Height="50px" TextMode="MultiLine" Width="90%" BorderWidth="1px"></asp:TextBox>
                            </span></span></span></font>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div>
                        <asp:Button ID="btnEnviarEmail" runat="server" OnClientClick="javascript:return Confirmacao();" Text="Enviar único" Width="170px" />
                        &nbsp;<asp:Button ID="btnEnviarEmailTodos" runat="server" OnClientClick="javascript:return Confirmacao();" Text="Enviar todos" Width="170px" OnClick="btnEnviarEmailTodos_Click" />
                    </div>
                </div>


            </asp:Panel>

            <br />

            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <font face="Tahoma, Tahoma, Helvetica, sans-serif" size="3"><span style="font-family: Tahoma"><span style="font-size: 10pt"><span style="FONT-SIZE: 10pt">
                                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="DemandaId" ForeColor="#333333" GridLines="None" Width="99%" EmptyDataText="Não há dados para exibição.">
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="DemandaId" HeaderText="Id" SortExpression="DemandaId" InsertVisible="False" ReadOnly="True">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Atividade" HeaderText="Atividade" SortExpression="Atividade">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Detalhe" HeaderText="Detalhe" SortExpression="Detalhe">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="DataAbertura" HeaderText="Data de Abertura" SortExpression="DataAbertura">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="DataPrazo" HeaderText="Data Prazo" SortExpression="DataPrazo" DataFormatString="{0:d}">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" SortExpression="Solicitante">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Responsavel" HeaderText="Responsável" SortExpression="Responsavel">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Area" HeaderText="Área" SortExpression="Area">
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Atraso" HeaderText="Restante (em dias)" SortExpression="Atraso" ReadOnly="True">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="Yellow" Font-Bold="True" ForeColor="Black" />
                                                                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                                                            <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                                                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                                                            <SortedDescendingHeaderStyle BackColor="#820000" />
                                                                        </asp:GridView>
                                                                <font face="Tahoma, Tahoma, Helvetica, sans-serif" size="3"><span style="font-family: Tahoma"><span style="font-size: 10pt"><span style="FONT-SIZE: 10pt">
                                                                <asp:SqlDataSource ID="SqlDataSourceProximasAoFim" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Demandas.DemandaId, Atividades.Nome AS Atividade, Demandas.Detalhe, Demandas.Status, Demandas.DataAbertura, Demandas.DataPrazo, Usuarios_1.Nome AS Solicitante, Usuarios.Nome AS Responsavel, Areas.Nome AS Area, DATEDIFF(Day, GETDATE(), Demandas.DataPrazo) AS Atraso FROM Demandas INNER JOIN Usuarios AS Usuarios_1 ON Demandas.SolicitanteId = Usuarios_1.UserID INNER JOIN Usuarios ON Demandas.ResponsavelId = Usuarios.UserID INNER JOIN Atividades ON Demandas.AtividadeId = Atividades.AtividadeId INNER JOIN Subprocessos ON Atividades.SubprocessoId = Subprocessos.SubprocessoId INNER JOIN Processos ON Subprocessos.ProcessoId = Processos.ProcessoId INNER JOIN Areas ON Processos.AreaId = Areas.AreaId WHERE (Demandas.Status &lt;&gt; 'CONCLUIDA') AND (DATEDIFF(Day, GETDATE(), Demandas.DataPrazo) BETWEEN 0 AND @Dias) AND (Demandas.Status &lt;&gt; 'CANCELADA') ORDER BY Atraso DESC, Atividade">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="dpdDias" Name="Dias" PropertyName="SelectedValue" />
                                                                    </SelectParameters>
                        </asp:SqlDataSource>
                                                                <asp:SqlDataSource ID="SqlDataSourceProximasAoFimArea" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Demandas.DemandaId, Atividades.Nome AS Atividade, Demandas.Detalhe, Demandas.Status, Demandas.DataAbertura, Demandas.DataPrazo, Usuarios_1.Nome AS Solicitante, Usuarios.Nome AS Responsavel, Areas.Nome AS Area, DATEDIFF(Day, GETDATE(), Demandas.DataPrazo) AS Atraso FROM Demandas INNER JOIN Usuarios AS Usuarios_1 ON Demandas.SolicitanteId = Usuarios_1.UserID INNER JOIN Usuarios ON Demandas.ResponsavelId = Usuarios.UserID INNER JOIN Atividades ON Demandas.AtividadeId = Atividades.AtividadeId INNER JOIN Subprocessos ON Atividades.SubprocessoId = Subprocessos.SubprocessoId INNER JOIN Processos ON Subprocessos.ProcessoId = Processos.ProcessoId INNER JOIN Areas ON Processos.AreaId = Areas.AreaId WHERE (Demandas.Status &lt;&gt; 'CONCLUIDA') AND (DATEDIFF(Day, GETDATE(), Demandas.DataPrazo) BETWEEN 0 AND @Dias) AND (Demandas.Status &lt;&gt; 'CANCELADA') AND (Areas.AreaId = @AreaId) ORDER BY Atraso DESC, Atividade">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="dpdDias" Name="Dias" PropertyName="SelectedValue" />
                                                                        <asp:ControlParameter ControlID="DropDownListArea" Name="AreaId" PropertyName="SelectedValue" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                                <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="2000" OnTick="Timer1_Tick">
                        </asp:Timer>
                                                                </span></span></span></font>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </form>
</body>
</html>
