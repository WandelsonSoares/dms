<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="myHome" %>

<%@ Register Src="wucMenu.ascx" TagName="wucMenu" TagPrefix="uc2" %>
<%@ Register Src="wucHeader.ascx" TagName="wucHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style22 {
            height: 104px;
        }

        .auto-style26 {
            position: relative;
            top: 1px;
            left: -151px;
            height: 33px;
            width: 50px;
        }

        .auto-style27 {
            color: #0000CC;
        }

        .auto-style29 {
            height: 25px;
        }
    </style>

    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="Script/JavaScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" runat="server"
    ContentPlaceHolderID="ContentPlaceHolder1">
    <div style="height: 100vh">
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <uc1:wucHeader ID="wucHeader1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table width="100%" align="center" cellpadding="0" cellspacing="0">
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

                                            <table cellpadding="2" cellspacing="2" width="100%" border="0">
                                                <tr>
                                                    <td style="text-align: center">&nbsp;</td>
                                                    <td
                                                        valign="top" width="100%" class="style22">
                                                        <asp:Label ID="Label9" runat="server" Text="Bem-vindo(a),"></asp:Label>
                                                        &nbsp;<asp:Label ID="lbl_Fullname" runat="server"></asp:Label>
                                                        <asp:Label ID="lbl_cmb" runat="server"></asp:Label>

                                                        <br />

                                                        <div>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td class="dashboardIconeMedio">
                                                                        <asp:ImageButton ID="imgOcorrenciasAbertas" runat="server" ImageUrl="~/img3/DashboardDemandasIconeGrande.png" CssClass="dashboardIcone" />
                                                                    </td>
                                                                    <td class="dashboardLabelMedio">
                                                                        <table class="dashboardTabelaLabel" style="background-color: #6b9edb">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblQuanditadeProcessando" runat="server" CssClass="dashboardLabelGrandeText" ForeColor="White">0</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblAbertas" runat="server" CssClass="dashboardLabelPequenoText" ForeColor="White">Demandas em processamento</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td></td>

                                                                    <td class="dashboardIconeMedio">
                                                                        <asp:ImageButton ID="imgOcorrenciasAbertas0" runat="server" ImageUrl="~/img3/DashboardAtrasadosIconeGrande.png" CssClass="dashboardIcone" />
                                                                    </td>
                                                                    <td class="dashboardLabelMedio">
                                                                        <table class="dashboardTabelaLabel" style="background-color: #ff4343">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblQuantidadeAtrasada" runat="server" CssClass="dashboardLabelGrandeText" ForeColor="White">0</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="auto-style29">
                                                                                    <asp:Label ID="lblPlanejadas" runat="server" CssClass="dashboardLabelPequenoText" ForeColor="White">Demandas atrasadas</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td></td>

                                                                    <td class="dashboardIconeMedio">
                                                                        <asp:ImageButton ID="imgOcorrenciasAbertas1" runat="server" ImageUrl="~/img3/DashboardConcluidosIconeGrande.png" CssClass="dashboardIcone" />
                                                                    </td>
                                                                    <td class="dashboardLabelMedio">
                                                                        <table class="dashboardTabelaLabel" style="background-color: #41a7c3">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblQuantidadeAguardando" runat="server" CssClass="dashboardLabelGrandeText" ForeColor="White">0</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblEmExecucao" runat="server" CssClass="dashboardLabelPequenoText" ForeColor="White">Demandas aguardando</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td></td>

                                                                    <td class="dashboardIconeMedio">
                                                                        <asp:ImageButton ID="imgOcorrenciasAbertas2" runat="server" ImageUrl="~/img3/DashboardSatisfacaoIconeGrande.png" CssClass="dashboardIcone" />
                                                                    </td>
                                                                    <td class="dashboardLabelMedio">
                                                                        <table class="dashboardTabelaLabel" style="background-color: #678034">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblQuantidadeSatisfacao" runat="server" CssClass="dashboardLabelGrandeText" ForeColor="White">0</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblAtrasadas" runat="server" CssClass="dashboardLabelPequenoText" ForeColor="White">Satisfação dos clientes</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 20px">
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <%--  Início do Painel 1--%>
                                                                <tr>
                                                                    <td colspan="11">
                                                                        <asp:Panel ID="Panel1" runat="server">
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="text-align: center; background-color: white" colspan="5" rowspan="3">
                                                                                        <asp:LinkButton ID="LinkButtonPeriodoDatas4" runat="server" OnClick="LinkButtonPeriodoDatas4_Click" Visible="False">Período</asp:LinkButton>
                                                                                        <asp:Panel ID="PanelGrafico1" runat="server">
                                                                                            <strong>
                                                                                                <asp:Label ID="lblGrafico1" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="10pt" Text="Demandas Recebidas por Área"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Chart ID="Chart1" runat="server" BackImageAlignment="Center" EnableTheming="True" Height="350px" Palette="None" PaletteCustomColors="LightSkyBlue" ViewStateContent="All" Width="700px">
                                                                                                    <Series>
                                                                                                        <asp:Series ChartType="StackedColumn" IsValueShownAsLabel="True" Name="Series1">
                                                                                                        </asp:Series>
                                                                                                    </Series>
                                                                                                    <ChartAreas>
                                                                                                        <asp:ChartArea BackColor="White" BorderColor="White" Name="ChartArea1" ShadowColor="White">
                                                                                                            <AxisY IsLabelAutoFit="False" LabelAutoFitMaxFontSize="6" LabelAutoFitStyle="WordWrap" TitleFont="Arial, 9.75pt" TitleForeColor="Gray" InterlacedColor="Transparent" LineColor="Transparent">
                                                                                                                <MajorGrid Enabled="True" LineColor="LightGray" LineWidth="1" LineDashStyle="Solid" />
                                                                                                                <MajorTickMark Enabled="False" />
                                                                                                                <LabelStyle Enabled="False" ForeColor="Red" />
                                                                                                                <ScaleBreakStyle LineColor="Gray" />
                                                                                                            </AxisY>
                                                                                                            <AxisX IsLabelAutoFit="False" LabelAutoFitMaxFontSize="6" LabelAutoFitStyle="WordWrap" TitleFont="Arial, 12pt" InterlacedColor="Transparent" LineColor="Transparent">
                                                                                                                <MajorGrid Enabled="False" />
                                                                                                                <MajorTickMark Enabled="False" />
                                                                                                                <LabelStyle Font="Arial, 10pt" ForeColor="DimGray" />
                                                                                                            </AxisX>
                                                                                                            <AxisY2 LineColor="Transparent">
                                                                                                            </AxisY2>
                                                                                                            <Area3DStyle Inclination="5" IsClustered="True" IsRightAngleAxes="False" LightStyle="Realistic" Perspective="2" PointDepth="0" PointGapDepth="0" Rotation="0" WallWidth="10" />
                                                                                                        </asp:ChartArea>
                                                                                                    </ChartAreas>
                                                                                                    <BorderSkin BackColor="Transparent" />
                                                                                                </asp:Chart>
                                                                                                <br />

                                                                                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Area, Valor FROM Grafico1 WHERE (UserId = @UserId) ORDER BY Area">
                                                                                                    <SelectParameters>
                                                                                                        <asp:SessionParameter Name="UserId" SessionField="UserId" />
                                                                                                    </SelectParameters>
                                                                                                </asp:SqlDataSource>
                                                                                            </strong>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td style="background-color: white" colspan="5">
                                                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="Gray" GridLines="None" Width="100%" Font-Size="9pt">
                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                            <Columns>

                                                                                                <asp:ImageField DataImageUrlField="Atrasado" ItemStyle-HorizontalAlign="Center" DataImageUrlFormatString="~/img3/Usuarios/Atrasado/{0}.png">
                                                                                                    <ItemStyle Width="5%" />
                                                                                                </asp:ImageField>

                                                                                                <asp:ImageField DataImageUrlField="FotoNomeArquivo" ItemStyle-HorizontalAlign="Center" DataImageUrlFormatString="~/img3/Usuarios/Perfil/Reduzidas/{0}" NullImageUrl="~/img3/Usuarios/Perfil/Reduzidas/0.png">
                                                                                                    <ItemStyle Width="5%" />
                                                                                                </asp:ImageField>

                                                                                                <asp:BoundField DataField="Nome" HeaderText="Funcionário">
                                                                                                    <ItemStyle Width="50%" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Atendimento" HeaderText="Em Atendimento">
                                                                                                    <HeaderStyle CssClass="headerTextCenter" />
                                                                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Font-Size="12pt" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Backlog" HeaderText="Backlog">
                                                                                                    <HeaderStyle CssClass="headerTextCenter" />
                                                                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Font-Size="12pt" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="FotoNomeArquivo" Visible="False" />
                                                                                                <asp:BoundField DataField="Atrasado" HeaderText="Atrasado" Visible="False" />
                                                                                            </Columns>
                                                                                            <EditRowStyle BackColor="#2461BF" />
                                                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                                            <RowStyle BackColor="#EFF3FB" />
                                                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                                        </asp:GridView>
                                                                                        <hr style="margin-top: 5px; margin-bottom: 5px" />
                                                                                        <asp:Label ID="lblLegenda" runat="server" Font-Size="8pt" ForeColor="Gray" Text="Legenda:"></asp:Label>
                                                                                        &nbsp;<asp:Image ID="Image2" runat="server" ImageUrl="~/img3/Usuarios/Atrasado/SIM.png" />
                                                                                        <asp:Label ID="lblLegendaAtrasado" runat="server" Font-Size="8pt" ForeColor="Gray" Text="Possui demanda atrasada no backlog."></asp:Label>
                                                                                        &nbsp;
                                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/img3/Usuarios/Atrasado/NAO.png" />
                                                                                        <asp:Label ID="lblLegendaAtrasado0" runat="server" Font-Size="8pt" ForeColor="Gray" Text="Não possui demandas atrasadas."></asp:Label>
                                                                                        <br />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 20px">
                                                                                    <td>&nbsp;</td>
                                                                                    <td colspan="5">&nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>&nbsp;</td>
                                                                                    <td style="text-align: center; vertical-align: sub; background-color: white" colspan="5">
                                                                                        <asp:Panel ID="PanelGrafico2" runat="server" Width="100%">
                                                                                            <strong>
                                                                                                <asp:Label ID="lblGrafico2" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="10pt" Text="Baklog"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Chart ID="Chart2" runat="server" BackImageAlignment="Center" DataSourceID="SqlDataSource1" EnableTheming="True" Height="170px" Palette="None" PaletteCustomColors="Red" ViewStateContent="All" Width="700px">
                                                                                                    <Series>
                                                                                                        <asp:Series ChartType="Bar" IsValueShownAsLabel="True" MarkerSize="10" Name="Series1" XValueMember="Nome" YValueMembers="Demandas">
                                                                                                        </asp:Series>
                                                                                                    </Series>
                                                                                                    <ChartAreas>
                                                                                                        <asp:ChartArea BackColor="White" BorderColor="White" Name="ChartArea1" ShadowColor="White">
                                                                                                            <AxisY IsLabelAutoFit="False" LabelAutoFitMaxFontSize="6" LabelAutoFitMinFontSize="5" LabelAutoFitStyle="WordWrap" TitleFont="Microsoft Sans Serif, 6pt" MaximumAutoSize="100" Enabled="False">
                                                                                                                <MajorGrid Enabled="False" />
                                                                                                            </AxisY>
                                                                                                            <AxisX IsLabelAutoFit="False" LabelAutoFitMaxFontSize="6" LabelAutoFitStyle="WordWrap" Minimum="0" MaximumAutoSize="100" LineColor="Transparent" LineWidth="0" TitleFont="Arial, 8pt" TitleForeColor="Gray">
                                                                                                                <MajorGrid Enabled="False" />
                                                                                                                <MajorTickMark Enabled="False" />
                                                                                                                <ScaleBreakStyle LineColor="Transparent" />
                                                                                                            </AxisX>
                                                                                                            <AxisY2 LineColor="Transparent">
                                                                                                            </AxisY2>
                                                                                                            <Area3DStyle Inclination="5" IsClustered="True" IsRightAngleAxes="False" LightStyle="Realistic" Perspective="2" PointDepth="0" PointGapDepth="0" Rotation="0" WallWidth="10" />
                                                                                                        </asp:ChartArea>
                                                                                                    </ChartAreas>
                                                                                                    <BorderSkin BackColor="Transparent" />
                                                                                                </asp:Chart>
                                                                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Usuarios.Nome, COUNT(Demandas.DemandaId) AS Demandas FROM Demandas INNER JOIN Usuarios ON Demandas.ResponsavelId = Usuarios.UserID INNER JOIN Atividades ON Demandas.AtividadeId = Atividades.AtividadeId INNER JOIN Subprocessos ON Atividades.SubprocessoId = Subprocessos.SubprocessoId INNER JOIN Processos ON Subprocessos.ProcessoId = Processos.ProcessoId WHERE (Demandas.Status &lt;&gt; 'CONCLUIDA') AND (Demandas.Status &lt;&gt; 'CANCELADA') AND (Processos.AreaId IN (SELECT AreaId FROM AreasUsuarios WHERE (UsuarioId = @UserId))) GROUP BY Usuarios.Nome ORDER BY Demandas">
                                                                                                    <SelectParameters>
                                                                                                        <asp:SessionParameter Name="UserId" SessionField="UserId" />
                                                                                                    </SelectParameters>
                                                                                                </asp:SqlDataSource>
                                                                                            </strong>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 20px">
                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButtonMesAno" runat="server" OnClick="LinkButtonMesAno_Click">Período</asp:LinkButton>
                                                                                    </td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td colspan="12" style="text-align: center; background-color: white"><strong>
                                                                                        <asp:Panel ID="PanelGrafico3" runat="server">
                                                                                            <div class="text-center" style="align-items: center">
                                                                                                <br />
                                                                                                <strong>
                                                                                                    <asp:Label ID="lblTituloGrafico3" runat="server" CssClass="fontePadrao" Font-Bold="True" Text="Quantidade de demandas abertas a vencer"></asp:Label>
                                                                                                    <br />
                                                                                                    <asp:Chart ID="Chart3" runat="server" BackImageAlignment="Center" EnableTheming="True" Height="250px" Palette="None" PaletteCustomColors="OrangeRed" ViewStateContent="All" Width="1600px">
                                                                                                        <Series>
                                                                                                            <asp:Series IsValueShownAsLabel="True" Name="Series1">
                                                                                                            </asp:Series>
                                                                                                        </Series>
                                                                                                        <ChartAreas>
                                                                                                            <asp:ChartArea BackColor="White" BorderColor="White" Name="ChartArea1" ShadowColor="White">
                                                                                                                <AxisY IsLabelAutoFit="False" LabelAutoFitMaxFontSize="6" LabelAutoFitStyle="WordWrap" TitleFont="Microsoft Sans Serif, 6pt" InterlacedColor="Transparent" LineColor="Transparent">
                                                                                                                    <MajorGrid LineColor="Silver" />
                                                                                                                    <MajorTickMark Enabled="False" />
                                                                                                                    <LabelStyle Enabled="False" />
                                                                                                                </AxisY>
                                                                                                                <AxisX IntervalAutoMode="VariableCount" IsLabelAutoFit="False" IsMarksNextToAxis="False" LabelAutoFitMaxFontSize="6" LabelAutoFitStyle="WordWrap" Maximum="31" Minimum="0">
                                                                                                                    <MajorGrid Enabled="False" />
                                                                                                                    <MajorTickMark Enabled="False" />
                                                                                                                    <ScaleBreakStyle MaxNumberOfBreaks="1" />
                                                                                                                    <ScaleView Size="31" SizeType="Number" />
                                                                                                                </AxisX>
                                                                                                                <AxisX2 LineColor="LightGray">
                                                                                                                </AxisX2>
                                                                                                                <AxisY2 Enabled="False" InterlacedColor="LightGray" LineColor="Transparent">
                                                                                                                </AxisY2>
                                                                                                                <Area3DStyle Inclination="5" IsClustered="True" IsRightAngleAxes="False" LightStyle="Realistic" Perspective="2" PointDepth="0" PointGapDepth="0" Rotation="0" WallWidth="10" />
                                                                                                            </asp:ChartArea>
                                                                                                        </ChartAreas>
                                                                                                        <BorderSkin BackColor="Transparent" />
                                                                                                    </asp:Chart>
                                                                                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Dia, Quantidade FROM Grafico3 GROUP BY Dia, Quantidade HAVING (UserId = @UserId) ORDER BY Dia">
                                                                                                        <SelectParameters>
                                                                                                            <asp:SessionParameter Name="UserId" SessionField="UserId" />
                                                                                                        </SelectParameters>
                                                                                                    </asp:SqlDataSource>
                                                                                                </strong>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                    </strong></td>
                                                                                </tr>
                                                                                <tr style="height: 20px">
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>





                                                                <%--Fim do Painel 1--%>

                                                                <tr style="height: 50px; text-align: center">
                                                                    <td colspan="11">
                                                                        <div>
                                                                            <asp:Label ID="lblTituloNotificacoes" runat="server" Font-Bold="True" Text="Últimas 5 notificações"></asp:Label>
                                                                            &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" Font-Size="8pt" NavigateUrl="~/Notificacoes.aspx">Clique aqui para ver todas.</asp:HyperLink>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Repeater ID="Repeater1" runat="server" EnableTheming="True">
                                                                                <ItemTemplate>
                                                                                    <div class="rptr">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <th colspan="2"><%#Eval("Assunto") %></th>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Notificação</td>
                                                                                                <td><%#Eval("Notificacao") %></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Data e Hora</td>
                                                                                                <td><%#Eval("DataHora") %></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# string.Format("~/img3/Notificacoes/Leitura/{0}.png",Eval("Lida"))%>' />
                                                                                                </td>
                                                                                                <td></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:HyperLink ID="HyperLinkVerMais" runat="server" NavigateUrl='<%# Eval("URL") %>'>Ver mais</asp:HyperLink>
                                                                                                </td>
                                                                                                <td></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                    </td>
                                                    <td></td>
                                                </tr>

                                            </table>
                                            <div class="col-md-12" style="text-align: center">
                                                <asp:Panel ID="PanelPeriodo" runat="server" BackColor="#FFFFCC" Height="80px" HorizontalAlign="Center" Visible="False" Width="600px">
                                                    <asp:Label ID="lblTituloPeriodo" runat="server" Text="Período de dados dos gráficos"></asp:Label>
                                                    <br />
                                                    <div style="vertical-align: central">
                                                        <asp:Label ID="lblDataInicial" runat="server" Height="20px" Text="Início"></asp:Label>
                                                        &nbsp;<asp:TextBox ID="txtDataInicio" runat="server" Height="20px" Width="75px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtDataInicio_CalendarExtender" runat="server" BehaviorID="txtDataInicio_CalendarExtender" TargetControlID="txtDataInicio" Format="dd/MM/yyyy">
                                                        </asp:CalendarExtender>
                                                        &nbsp;
                                                            <asp:Label ID="lblDataFim" runat="server" Height="20px" Text="Fim"></asp:Label>
                                                        &nbsp;<asp:TextBox ID="txtDataFim" runat="server" Height="20px" Width="75px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtDataFim_CalendarExtender" runat="server" BehaviorID="txtDataFim_CalendarExtender" TargetControlID="txtDataFim" Format="dd/MM/yyyy">
                                                        </asp:CalendarExtender>
                                                        &nbsp;<asp:Button ID="btnAplicar" runat="server" CssClass="btn btn-primary" OnClick="btnAplicar_Click" Text="Aplicar" Width="100px" />
                                                        &nbsp;<asp:Button ID="btnCancelarStatus" runat="server" CssClass="btn btn-danger" OnClick="btnCancelarStatus_Click" Text="Fechar" Width="100px" />
                                                    </div>

                                                </asp:Panel>
                                                <asp:Panel ID="PanelMesAno" runat="server" BackColor="#FFFFCC" Height="80px" HorizontalAlign="Center" Visible="False" Width="600px">
                                                    <asp:Label ID="Label1" runat="server" Text="Mês e Ano"></asp:Label>
                                                    <br />
                                                    <div style="vertical-align: central">
                                                        &nbsp;<asp:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="txtDataInicio_CalendarExtender" TargetControlID="txtDataInicio" Format="dd/MM/yyyy">
                                                        </asp:CalendarExtender>
                                                        &nbsp; &nbsp;<asp:DropDownList ID="DropDownListMes" runat="server" Width="50px">
                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp;<asp:DropDownList ID="DropDownListAno" runat="server" Width="100px">
                                                        </asp:DropDownList>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" BehaviorID="txtDataFim_CalendarExtender" TargetControlID="txtDataFim" Format="dd/MM/yyyy">
                                                        </asp:CalendarExtender>
                                                        &nbsp;<asp:Button ID="btnAplicarMesAno" runat="server" CssClass="btn btn-primary" OnClick="btnAplicarMesAno_Click" Text="Aplicar" Width="100px" />
                                                        &nbsp;<asp:Button ID="btnMesAno" runat="server" CssClass="btn btn-danger" OnClick="btnMesAno_Click" Text="Fechar" Width="100px" />
                                                    </div>

                                                </asp:Panel>
                                                <asp:AlwaysVisibleControlExtender ID="PanelMesAno_AlwaysVisibleControlExtender" runat="server" BehaviorID="PanelMesAno_AlwaysVisibleControlExtender" HorizontalSide="Center" TargetControlID="PanelMesAno">
                                                </asp:AlwaysVisibleControlExtender>
                                                <asp:AlwaysVisibleControlExtender ID="PanelPeriodo_AlwaysVisibleControlExtender" runat="server" BehaviorID="PanelEtapaStatus_AlwaysVisibleControlExtender" HorizontalSide="Center" TargetControlID="PanelPeriodo">
                                                </asp:AlwaysVisibleControlExtender>
                                            </div>
                                            <div style="position: fixed; height: 680px; top: 100px; right: 10px; padding: 2px;">
                                                <asp:Panel ID="PanelEtapas" runat="server" Height="700px" Width="400px" BackColor="White" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Visible="True">
                                                    <div style="background-color: red; padding: 2px; text-align: center">
                                                        <asp:Label ID="lblTitulo" runat="server" Text=" >>> ATENÇÃO ÀS ETAPAS ABAIXO <<<" Font-Bold="True" ForeColor="White"></asp:Label>

                                                    </div>
                                                    <div style="background-color: red; padding: 2px; text-align: center; vertical-align: central">
                                                        <asp:LinkButton ID="btnFecharPainelEstaSemana" runat="server" Font-Size="10px" ForeColor="White" OnClick="btnFecharPainelEstaSemana_Click">Fechar</asp:LinkButton>
                                                        &nbsp;<asp:CheckBox ID="CheckBoxNaoExibirMaisEssaSemana" runat="server" ForeColor="White" Font-Size="7pt" Text="Não exibir novamente" AutoPostBack="True" OnCheckedChanged="CheckBoxNaoExibirMaisEssaSemana_CheckedChanged" />                                                       
                                                    </div>
                                            <div style="overflow: scroll; height: 600px;">
                                                <asp:Repeater ID="RepeaterEtapas" runat="server" EnableTheming="True">
                                                    <ItemTemplate>
                                                        <div class="repeaterEtapas">
                                                            <table>
                                                                <tr>
                                                                    <th colspan="2">Etapa: <%#Eval("Etapa") %></th>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20%">Detalhe</td>
                                                                    <td><%#Eval("Detalhe") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Atividade</td>
                                                                    <td><%#Eval("Atividade") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Data Prazo</td>
                                                                    <td style="color: red"><%#Eval("DataPrazo", "{0:d}") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Status</td>
                                                                    <td>
                                                                        <asp:Image ID="ImageStatus" runat="server" ImageUrl='<%# string.Format("~/img/demandasEtapasStatus/{0}.png",Eval("Status"))%>' />&nbsp<%#Eval("Status") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td>
                                                                        <asp:HyperLink ID="HyperLinkVerMais" runat="server" NavigateUrl='<%# string.Format("/DemandasEditar.aspx?Id={0}", Eval("DemandaId")) %>'>Ver mais</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <hr />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>

                                            </asp:Panel>
                                                <asp:AlwaysVisibleControlExtender ID="PanelEtapas_AlwaysVisibleControlExtender" runat="server" BehaviorID="PanelEtapas_AlwaysVisibleControlExtender" HorizontalSide="Right" TargetControlID="PanelEtapas">
                                                </asp:AlwaysVisibleControlExtender>
                                            </div>

                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                <ProgressTemplate>
                                                    <div style="text-align: center; background-color: white; opacity: 0.9">
                                                        <asp:Label ID="lblCarregando2" runat="server" CssClass="auto-style27" Font-Names="Trebuchet MS" Font-Size="10pt" Text="Carregando..."></asp:Label>
                                                        <img alt="" src="img2/progresss.gif" class="auto-style26" />
                                                        &nbsp;
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <asp:AlwaysVisibleControlExtender ID="UpdateProgress1_AlwaysVisibleControlExtender"
                                                runat="server" Enabled="True" HorizontalOffset="600"
                                                TargetControlID="UpdateProgress1" VerticalOffset="300">
                                            </asp:AlwaysVisibleControlExtender>


                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                </td>
                            </tr>
                        </table>

                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


