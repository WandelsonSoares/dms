<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Etapas.aspx.cs" Inherits="Etapas" %>

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
                                        <li role="presentation"><a href="#">Subprocessos</a></li>
                                        <li role="presentation"><a href="#">Atividades</a></li>
                                        <li role="presentation" class="active"><a href="Etapas.aspx">Etapas</a></li>
                                    </ul>
                                </div>
                                <div>
                                    <br />
                                    <table width="100%" style="height: 100%">
                                        <tr valign="top">
                                            <td style="width: 0%;">
                                                <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Height="100%" Width="400px" Visible="False">
                                                    <div style="vertical-align: central; height: 100%; padding: 4px">

                                                        <asp:Label ID="lblAtividade" runat="server" CssClass="fontePadrao" Height="18px" Text="Atividade"></asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="DropDownListAtividade" runat="server" Width="300px"></asp:DropDownList>
                                                        <br />
                                                        <br />

                                                        <asp:Label ID="lblId" runat="server" CssClass="fontePadrao" Height="18px" Text="Id"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtId" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Enabled="False" Height="18px" Width="50px"></asp:TextBox>
                                                        <br />

                                                        <asp:Label ID="lblNome" runat="server" CssClass="fontePadrao" Height="18px" Text="Nome"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtNome" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Height="18px" Width="300px"></asp:TextBox>
                                                        <br />

                                                        <br />
                                                        <asp:Label ID="lblTipoResponsavel" runat="server" CssClass="fontePadrao" Height="18px" Text="Tipo de Responsável"></asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="DropDownListResponsavelTipo" runat="server" Width="300px"></asp:DropDownList>
                                                        <br />
                                                        <br />

                                                        <div class="input-group" style="padding: 5px; border-color: grey; border-width: 1px; border-style: solid">
                                                            <asp:CheckBox ID="CheckBoxDocumentoObrigatorio" CssClass="checkbox" runat="server" Text="Exige envio de documento" AutoPostBack="True" Checked="True" OnCheckedChanged="CheckBoxDocumentoObrigatorio_CheckedChanged" />

                                                            <asp:Label ID="lblDocumentoNome" runat="server" CssClass="fontePadrao" Height="18px" Text="Documento necessário"></asp:Label>
                                                            <br />
                                                            <asp:TextBox ID="txtDocumentoNome" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Height="18px" Width="300px"></asp:TextBox>
                                                            <br />

                                                            <asp:Label ID="lblDocumentoTipo" runat="server" CssClass="fontePadrao" Height="18px" Text="Tipo de documento"></asp:Label>
                                                            <br />
                                                            <asp:DropDownList ID="DropDownListDocumentoTipo" runat="server" Width="300px"></asp:DropDownList>
                                                            <br />

                                                            <br />

                                                            <asp:Button ID="btnDocumentosModelosAbrir" CssClass="btn btn-primary" runat="server" Text="Modelo do Documento" OnClick="btnDocumentosModelosAbrir_Click" />

                                                        </div>
                                                        <br />

                                                        <asp:Label ID="lblPrazo" runat="server" CssClass="fontePadrao" Height="18px" Text="Prazo para execução (em dias úteis)" Font-Bold="True"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtPrazo" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Height="18px" Width="50px" Font-Bold="True"></asp:TextBox>
                                                        <br />
                                                        <br />

                                                        <asp:Label ID="lblTempo" runat="server" CssClass="fontePadrao" Height="18px" Text="Tempo necessário para executar (em horas)" Font-Bold="False"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtTempo" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Height="18px" Width="50px" Font-Bold="False"></asp:TextBox>
                                                        <br />
                                                        <br />

                                                        <asp:Label ID="lblOrdem" runat="server" CssClass="fontePadrao" Height="18px" Text="Ordem de execução"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtOrdem" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Height="18px" Width="50px"></asp:TextBox>
                                                        <br />
                                                        <br />

                                                        <asp:Label ID="lblEtapaPrecedente" runat="server" CssClass="fontePadrao" Height="18px" Text="Etapa precedente"></asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="DropDownListEtapaPrecedente" runat="server" Width="300px"></asp:DropDownList>
                                                        <br />
                                                        <br />

                                                        <div style="text-align: center; padding: 4px">
                                                            <asp:Button ID="btnGravar" runat="server" CssClass="btn btn-primary" Text="Gravar" Width="100px" OnClick="btnGravar_Click" />
                                                            &nbsp;
                                                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-danger" Text="Cancelar" Width="100px" OnClick="btnCancelar_Click" />
                                                        </div>

                                                    </div>

                                                </asp:Panel>
                                            </td>
                                            <td style="width: 100%; padding: 4px">
                                                <div>
                                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnAdicionar" CssClass="btn btn-primary" runat="server" Width="100px" Text="Novo" OnClick="btnAdicionar_Click1" />
                                                    &nbsp;<asp:Button ID="btnExcluir" runat="server" CssClass="btn btn-danger" Text="Excluir" Width="100px" OnClick="btnExcluir_Click" />

                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>

                                                        <div>
                                                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#333333" GridLines="None" Width="100%" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns>

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

                                                                    <asp:BoundField DataField="EtapaId" HeaderText="Id" ReadOnly="True" SortExpression="EtapaId">
                                                                        <ItemStyle Width="20px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Ordem" HeaderText="Ordem" SortExpression="Ordem" />
                                                                    <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                                                                    <asp:BoundField DataField="TipoResponsavel" HeaderText="Tipo de Responsável" SortExpression="TipoResponsavel" />
                                                                    <asp:BoundField DataField="DocumentoNome" HeaderText="Nome do Documento" SortExpression="DocumentoNome" />
                                                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo de Documento" SortExpression="Tipo" />
                                                                    <asp:BoundField DataField="Prazo" HeaderText="Prazo (Dias úteis)" SortExpression="Prazo" />
                                                                    <asp:BoundField DataField="EtapaPrecedenteId" HeaderText="Id da Etapa Precedente" SortExpression="EtapaPrecedenteId" />
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
                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Etapas.EtapaId, Etapas.Ordem, Etapas.Nome, EtapasResponsaveisTipos.Nome AS TipoResponsavel, Etapas.DocumentoNome, EtapasDocumentosTipos.Nome AS Tipo, Etapas.Prazo, Etapas.EtapaPrecedenteId FROM Atividades INNER JOIN Etapas ON Atividades.AtividadeId = Etapas.AtividadeId INNER JOIN EtapasResponsaveisTipos ON Etapas.ResponsavelTipoId = EtapasResponsaveisTipos.TipoId LEFT OUTER JOIN EtapasDocumentosTipos ON Etapas.DocumentoTipoId = EtapasDocumentosTipos.TipoId WHERE (Etapas.AtividadeId = @Atividade) ORDER BY Etapas.Ordem">
                                                            <SelectParameters>
                                                                <asp:QueryStringParameter Name="Atividade" QueryStringField="id" />
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

