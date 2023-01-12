<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DemandasEditar.aspx.cs" Inherits="DemandasEditar" %>

<%@ Register Src="wucMenu.ascx" TagName="wucMenu" TagPrefix="uc2" %>
<%@ Register Src="wucHeader.ascx" TagName="wucHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript">


        function Confirmacao() {

            if (confirm("Confirma essa operação?")) {

                return true;

            } else {

                return false;

            }

        }


    </script>



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
                            <div>
                                <div>
                                    <asp:Label ID="lblTitulo" Style="font-weight: bold" runat="server" Text="Demanda de atendimento"></asp:Label>
                                    <hr style="padding: 0px; margin: 0px" />
                                </div>

                                <div style="padding-bottom: 2px">
                                    <asp:Button ID="btnVoltar" CssClass="btn btn-primary" runat="server" Text="Voltar" Enabled="True" PostBackUrl="~/Demandas.aspx" Width="200px" />
                                    &nbsp;&nbsp;<asp:Button ID="btnCancelar" OnClientClick="javascript:return Confirmacao();" CssClass="btn btn-danger" runat="server" Text="Cancelar Demanda" Enabled="False" OnClick="btnCancelar_Click" Width="200px" />
                                    &nbsp;&nbsp;<asp:Button ID="btnReabrir" OnClientClick="javascript:return Confirmacao();" CssClass="btn btn-default" runat="server" Text="Reabrir Demanda" OnClick="btnReabrir_Click" Visible="False" />

                                </div>

                                <div id="dvContents" class="col-md-6" style="padding: 10px; height: 800px; width: 30%; border: 1px; border-style: solid; border-color: lightgray; overflow-y: scroll">

                                    <div class="col-md-6" style="padding: 0">
                                        <asp:Label ID="lblCodigo" runat="server" Text="Código"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtCodigo" runat="server" Width="150px" Enabled="False" BackColor="White"></asp:TextBox>
                                        <br />
                                    </div>

                                    <div class="col-md-6" style="padding: 0">

                                        <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtStatus" runat="server" Width="150px" Enabled="False" BackColor="White"></asp:TextBox>
                                        <br />

                                    </div>



                                    <div class="form-group">

                                        <div class="col-md-4" style="padding: 0">
                                            <asp:Label ID="lblDataAbertura" runat="server" Text="Data de Abertura"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtDataAbertura" runat="server" Width="150px" Enabled="False" BackColor="White"></asp:TextBox>
                                            <br />
                                        </div>

                                        <div class="col-md-4" style="padding: 0">
                                            <asp:Label ID="lblPrazo" runat="server" Text="Prazo"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtPrazo" runat="server" Width="120px" Enabled="False" BackColor="White"></asp:TextBox>
                                            <br />
                                        </div>

                                        <div class="col-md-4" style="padding: 0">
                                            <asp:Label ID="lblDataConclusao" runat="server" Text="Data de Conclusão"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtDataConclusao" runat="server" Enabled="False" BackColor="White"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div>

                                        <asp:Label ID="lblCN" runat="server" Text="Centro de Negócios demandante"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListCN" runat="server" Width="390px" BackColor="White" Enabled="False"></asp:DropDownList>
                                        <br />
                                        <br />

                                        <asp:Label ID="lblSolicitante" runat="server" Text="Solicitante"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtSolicitante" runat="server" Width="400px" Enabled="False" BackColor="White"></asp:TextBox>
                                        <br />


                                        <asp:Label ID="lblResponsavel" runat="server" Text="Responsável pelo atendimento"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtResponsavel" runat="server" Width="400px" Enabled="False" BackColor="White"></asp:TextBox>
                                        <br />


                                    </div>

                                    <hr style="color: lightgray; width: 1px;" />



                                    <div class="form-group" style="padding: 0">

                                        <asp:Label ID="lblProcesso" runat="server" Text="Processo"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtProcesso" runat="server" Width="400px" Enabled="False" BackColor="White"></asp:TextBox>
                                        <br />

                                        <asp:Label ID="lblSubprocesso" runat="server" Text="Subprocesso"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtSubprocesso" runat="server" Width="400px" Enabled="False" BackColor="White"></asp:TextBox>
                                        <br />

                                        <asp:Label ID="lblAtividade" runat="server" Text="Atividade"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtAtividade" runat="server" Width="400px" Enabled="False" BackColor="White"></asp:TextBox>
                                    </div>

                                    <div>
                                        <asp:Label ID="lblDetalhe" runat="server" Text="Detalhamento / comentário do solicitante"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtDetalhe" runat="server" Width="400px" Height="100px" TextMode="MultiLine" Enabled="False" BackColor="White"></asp:TextBox>
                                        <br />
                                    </div>

                                    <br />

                                    <div>
                                        <asp:Label ID="lblJustificativa" runat="server" Text="Justificativa de cancelamento"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtJustificativa" runat="server" Width="400px" Height="100px" TextMode="MultiLine" Enabled="False" BackColor="White" Visible="False"></asp:TextBox>
                                    </div>

                                    <div>

                                        <asp:Label ID="lblPrioridade" runat="server" Text="Aprovação"></asp:Label>
                                        <br />
                                        <asp:Image ID="ImagePrioridade" runat="server" ImageUrl="~/img/demandasPrioridades/0.png" Width="18px" />
                                        <asp:DropDownList ID="DropDownListPrioridade" runat="server" Width="375px" Enabled="False" DataSourceID="SqlDataSourceDemandasPrioridades" DataTextField="Nome" DataValueField="PrioridadeId" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPrioridade_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourceDemandasPrioridades" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT PrioridadeId, Nome FROM DemandasPrioridades ORDER BY PrioridadeId"></asp:SqlDataSource>
                                        <br />
                                        <br />

                                        <asp:Label ID="lblClassificacao" runat="server" Text="Classificação da demanda pelo atendente"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListClassificacao" runat="server" Width="400px" Enabled="False" DataSourceID="SqlDataSourceDemandasClassificacoes" DataTextField="Nome" DataValueField="DemandaClassificacaoId">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourceDemandasClassificacoes" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT DemandaClassificacaoId, Nome FROM DemandasClassificacoes ORDER BY Nome"></asp:SqlDataSource>
                                        <br />
                                    </div>

                                    <br />

                                    <div>
                                        <asp:Label ID="lblComentarioAtendente" runat="server" Text="Comentário do atendente"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtComentarioAtendente" runat="server" Width="400px" Height="100px" TextMode="MultiLine" Enabled="False" BackColor="White"></asp:TextBox>
                                        <br />
                                    </div>

                                    <br />

                                    <div style="padding-bottom: 2px">
                                        <asp:Button ID="btnGravar" CssClass="btn btn-primary" runat="server" Text="Gravar" Enabled="False" Width="200px" OnClick="btnGravar_Click" />
                                    </div>

                                    <br />
                                    <div style="padding-bottom: 2px">
                                        <asp:LinkButton ID="HyperLinkPesquisaSatisfacao" runat="server" OnClick="btnAvaliacaoSatisfacaoAbrir_Click" Font-Bold="True">Pesquisa de Satisfação</asp:LinkButton>
                                    </div>

                                    <asp:Label ID="lblIndividualColetivo" runat="server" Text="Demanda para atendimento a:" Visible="false"></asp:Label>
                                    <asp:RadioButtonList ID="RadioButtonListIndividualColetivo" CssClass="radio" runat="server" OnSelectedIndexChanged="RadioButtonListIndividualColetivo_SelectedIndexChanged" AutoPostBack="True" Enabled="False" Visible="False">
                                        <asp:ListItem Value="1">Único funcionário</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True">Múltiplos funcionários</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <asp:Label ID="lblMatricula" runat="server" Text="Matrícula do funcionário" Visible="false"></asp:Label>
                                    <br />
                                    <div style="vertical-align: central">
                                        <asp:TextBox ID="txtMatriculaFuncionario" runat="server" Width="100px" OnTextChanged="txtMatriculaFuncionario_TextChanged" AutoPostBack="True" Enabled="False" Visible="False"></asp:TextBox>
                                        &nbsp;<asp:Label ID="lblFuncionarioNome" runat="server" ForeColor="#0000CC" Visible="False"></asp:Label>
                                    </div>
                                    <br />

                                </div>



                                <div class="col-md-6" style="padding: 10px; min-height: 1px; float: left; width: 70%; height: 100%; left: 0px; padding-left: 15px; padding-right: 15px;">
                                    <asp:Label ID="lblTitulo2" Style="font-weight: bold" runat="server" Text="Fluxo de Atendimento (Etapas da atividade)"></asp:Label>
                                    <br />

                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" EmptyDataText="Nenhum registro para ser exibido." ForeColor="#333333" GridLines="None" Style="font-family: Tahoma; font-size: 8pt; text-align: left" Width="98%" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:ImageField DataImageUrlField="Status" runat="server" DataImageUrlFormatString="~/img/demandasEtapasStatus/{0}.png"></asp:ImageField>
                                            <asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" InsertVisible="False" ReadOnly="True" />
                                            <asp:BoundField DataField="Ordem" HeaderText="Ordem" SortExpression="Ordem" />
                                            <asp:BoundField DataField="Nome" HeaderText="Descrição" SortExpression="Nome" />
                                            <asp:BoundField DataField="Responsavel" HeaderText="Responsavel" SortExpression="Responsavel"></asp:BoundField>


                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/img3/Edit.png"
                                                Text="Editar Status" CommandName="EditarStatus">
                                                <ItemStyle Width="1px" HorizontalAlign="Center" />
                                            </asp:ButtonField>

                                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                            <asp:BoundField DataField="DataPrazo" HeaderText="Data Prazo" SortExpression="DataPrazo" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="Documento" HeaderText="Documento Necessário" SortExpression="Documento"></asp:BoundField>
                                            <asp:BoundField DataField="DocumentoOK" HeaderText="Doc OK?" SortExpression="DocumentoOK"></asp:BoundField>

                                            <asp:ButtonField ButtonType="Image" runat="server" ImageUrl="~/img3/documentos.png"
                                                Text="Ver" CommandName="Documentos" HeaderText="Documentos">
                                                <ItemStyle Width="1px" HorizontalAlign="Center" />
                                            </asp:ButtonField>

                                            <asp:ButtonField ButtonType="Image" runat="server" ImageUrl="~/img3/comment.png"
                                                Text="Comentários" CommandName="AbrirComentarios" HeaderText="Comentários">
                                                <ItemStyle Width="1px" HorizontalAlign="Center" />
                                            </asp:ButtonField>

                                            <asp:ButtonField ButtonType="Image" runat="server" ImageUrl="~/img3/DocumentoModeloDownload.png"
                                                Text="Ver" CommandName="DocumentosModelo" HeaderText="Download">
                                                <ItemStyle Width="1px" HorizontalAlign="Center" />
                                            </asp:ButtonField>

                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />

                                        </Columns>
                                        <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Gray" />
                                        <HeaderStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Gray" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#FFFFFF" BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT DemandasEtapas.DemandaEtapaId AS Codigo, DemandasEtapas.Ordem, DemandasEtapas.Nome, EtapasResponsaveisTipos.Nome AS Responsavel, DemandasEtapas.DocumentoNome AS Documento, DemandasEtapas.Prazo, DemandasEtapas.DataPrazo, DemandasEtapas.Status, DemandasEtapas.DocumentoOK, EtapasDocumentosTipos.Nome AS Tipo FROM EtapasDocumentosTipos RIGHT OUTER JOIN DemandasEtapas ON EtapasDocumentosTipos.TipoId = DemandasEtapas.DocumentoTipoId LEFT OUTER JOIN EtapasResponsaveisTipos ON DemandasEtapas.ResponsavelTipoId = EtapasResponsaveisTipos.TipoId WHERE (DemandasEtapas.DemandaId = @DemandaId) ORDER BY DemandasEtapas.Ordem">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="DemandaId" QueryStringField="id" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>

                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <div style="text-align: center; background-color: white; opacity: 0.9">
                                                    <img alt="" src="img2/progresss.gif" />
                                                    <br />
                                                    <asp:Label ID="lblCarregando2" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt" Text="Carregando..."></asp:Label>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:AlwaysVisibleControlExtender ID="UpdateProgress1_AlwaysVisibleControlExtender"
                                            runat="server" Enabled="True" HorizontalOffset="600"
                                            TargetControlID="UpdateProgress1" VerticalOffset="300">
                                        </asp:AlwaysVisibleControlExtender>

                                    </div>

                                </div>
                            </div>

                            <div>
                                <asp:Panel ID="PanelEtapaStatus" runat="server" BackColor="#FFFFCC" Height="150px" Width="400px" HorizontalAlign="Center" Visible="False">
                                    <br />
                                    <asp:Label ID="lblEtapaStatus" runat="server" Text="Status"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="DropDownListEtapaStatus" runat="server" Width="300px">
                                        <asp:ListItem>AGUARDANDO</asp:ListItem>
                                        <asp:ListItem>PROCESSANDO</asp:ListItem>
                                        <asp:ListItem>CONCLUIDA</asp:ListItem>
                                        <asp:ListItem>ATRASADA</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                    <asp:Button ID="btnGravarStatus" CssClass="btn btn-primary" Width="100px" runat="server" Text="Gravar" OnClick="btnGravarStatus_Click" />

                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancelarStatus" CssClass="btn btn-danger" Width="100px" runat="server" Text="Fechar" OnClick="btnCancelarStatus_Click" />

                                </asp:Panel>

                                <asp:AlwaysVisibleControlExtender ID="PanelEtapaStatus_AlwaysVisibleControlExtender" runat="server" BehaviorID="PanelEtapaStatus_AlwaysVisibleControlExtender" HorizontalSide="Center" TargetControlID="PanelEtapaStatus">
                                </asp:AlwaysVisibleControlExtender>

                            </div>
                            <div>
                                <asp:Panel ID="PanelJustificativaCancelamento" runat="server" BackColor="#FFFFCC" Height="300px" Width="400px" HorizontalAlign="Center" Visible="False">
                                    <asp:Label ID="lblJustificativaCancelamento" runat="server" Text="Insira a justificativa:"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtJustificativaCancelamento" runat="server" Height="200px" Width="340px" TextMode="MultiLine"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Button ID="btnConfirmar" CssClass="btn btn-primary" Width="100px" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnFechar" CssClass="btn btn-danger" Width="100px" runat="server" Text="Fechar" OnClick="btnFechar_Click" />

                                </asp:Panel>
                                <asp:AlwaysVisibleControlExtender ID="PanelJustificativaCancelamento_AlwaysVisibleControlExtender" runat="server" BehaviorID="PanelJustificativaCancelamento_AlwaysVisibleControlExtender" HorizontalSide="Center" TargetControlID="PanelJustificativaCancelamento">
                                </asp:AlwaysVisibleControlExtender>
                            </div>
                            <asp:Panel ID="PanelEtapaComentario" runat="server" BackColor="#FFFFCC" Height="300px" Width="400px" HorizontalAlign="Center" Visible="False">
                                <asp:Label ID="lblAutor" runat="server" Text="Autor:"></asp:Label>&nbsp;<asp:Label ID="lblAutorNome" runat="server"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtEtapaComentario" runat="server" Height="200px" TextMode="MultiLine" Width="340px"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Button ID="btnEtapaComentarioGravar" CssClass="btn btn-primary" Width="100px" runat="server" Text="Confirmar" OnClick="btnEtapaComentarioGravar_Click" Enabled="False" />
                                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnEtapaComentarioFechar" CssClass="btn btn-danger" Width="100px" runat="server" Text="Fechar" OnClick="btnEtapaComentarioFechar_Click" />

                            </asp:Panel>

                            <asp:AlwaysVisibleControlExtender ID="PanelEtapaComentario_AlwaysVisibleControlExtender" runat="server" BehaviorID="PanelEtapaComentario_AlwaysVisibleControlExtender" HorizontalSide="Center" TargetControlID="PanelEtapaComentario">
                            </asp:AlwaysVisibleControlExtender>

                            <div>
                            </div>
                            <asp:HiddenField ID="HiddenFieldEtapaId" runat="server" />
                            <asp:HiddenField ID="HiddenFieldResponsavelDemandaId" runat="server" />
                            <asp:HiddenField ID="HiddenFieldSolicitanteDemandaId" runat="server" />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>

    </div>
</asp:Content>

