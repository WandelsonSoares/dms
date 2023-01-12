<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DemandasNova.aspx.cs" Inherits="DemandasNova" %>

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

                            <div class="col-md-6">
                                <div style="padding: 10px; border: 1px; border-style: solid; border-color: grey">


                                    <asp:Label ID="lblTitulo" Style="font-weight: bold" runat="server" Text="Nova Demanda"></asp:Label>
                                    <hr style="padding: 0px; margin: 0px" />
                                    <br />

                                    <asp:Label ID="lblUsuarioId" runat="server" Text="Usuário Id"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtUsuarioId" runat="server" Width="50px" BackColor="White" Enabled="False"></asp:TextBox>
                                    <br />

                                    <asp:Label ID="lblNome" runat="server" Text="Solicitante"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtNome" runat="server" Width="400px" BackColor="White" Enabled="False"></asp:TextBox>
                                    <br />

                                    <br />
                                    <asp:Label ID="lblIndividualColetivo" runat="server" Text="Demanda para atendimento a:"></asp:Label>
                                    <div style="border-style: solid; border-width: 1px; border-color: lightgray; padding: 5px">
                                        <asp:Label ID="lblCN" runat="server" Text="Centro de Negócios demandante"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListCN" runat="server" Width="400px"></asp:DropDownList>
                                        <br />
                                    </div>
                                    <br />

                                    <asp:Label ID="lblTituloServicos" runat="server" Text="Dados sobre o serviço"></asp:Label>
                                    <div style="border-style: solid; border-width: 1px; border-color: lightgray; padding: 5px">
                                        <asp:Label ID="lblBU" runat="server" Text="Estrutura de Atendimento" Visible="False"></asp:Label>
                                        <%--<br />--%>
                                        <asp:DropDownList ID="DropDownListBU" runat="server" Width="400px" OnSelectedIndexChanged="DropDownListBU_SelectedIndexChanged" AutoPostBack="True" Visible="False"></asp:DropDownList>
                                        <%--<br />--%>

                                        <asp:Label ID="lblDepartamento" runat="server" Text="Departamento Atendente"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListDepartamento" runat="server" Width="400px" OnSelectedIndexChanged="DropDownListDepartamento_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        <br />
                                        <br />

                                        <asp:Label ID="lblSetor" runat="server" Text="Setor"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListSetor" runat="server" Width="400px" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSetor_SelectedIndexChanged"></asp:DropDownList>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbArea" runat="server" Text="Área"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListArea" runat="server" Width="400px" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="DropDownListArea_SelectedIndexChanged"></asp:DropDownList>
                                        <br />
                                        <br />

                                        <asp:Label ID="lblProcesso" runat="server" Text="Processo"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListProcesso" runat="server" Width="400px" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="DropDownListProcesso_SelectedIndexChanged"></asp:DropDownList>
                                        <br />
                                        <br />

                                        <asp:Label ID="lblSubprocesso" runat="server" Text="Subprocesso"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListSubprocesso" runat="server" Width="400px" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSubprocesso_SelectedIndexChanged"></asp:DropDownList>
                                        <br />
                                        <br />

                                        <asp:Label ID="lblAtividade" runat="server" Text="Atividade"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="DropDownListAtividade" runat="server" Width="400px" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="DropDownListAtividade_SelectedIndexChanged"></asp:DropDownList>
                                        <br />
                                        <br />

                                        <asp:Label ID="lblDetalhe" runat="server" Text="Detalhe"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtDetalhe" runat="server" Height="200px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                        <br />
                                        <br />
                                    </div>



                                    <div style="padding: 10px;">
                                        <asp:Button ID="btnGravar" class="btn btn-primary" runat="server" Text="Gravar" OnClick="btnGravar_Click" />
                                        &nbsp;<asp:Button ID="btnFechar" runat="server" class="btn btn-danger" Style="height: 36px" Text="Fechar" PostBackUrl="~/Demandas.aspx" />
                                    </div>

                                </div>
                            </div>

                            <asp:RadioButtonList ID="RadioButtonListIndividualColetivo" CssClass="radio" runat="server" OnSelectedIndexChanged="RadioButtonListIndividualColetivo_SelectedIndexChanged" AutoPostBack="True" Visible="False">
                                <asp:ListItem Value="1">Único funcionário</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">Múltiplos funcionários</asp:ListItem>
                            </asp:RadioButtonList>

                            <asp:Label ID="lblMatricula" runat="server" Text="Matrícula do funcionário" Visible="False"></asp:Label>
                            <br />
                            <div style="vertical-align: central">
                                <asp:TextBox ID="txtMatriculaFuncionario" runat="server" Width="100px" OnTextChanged="txtMatriculaFuncionario_TextChanged" AutoPostBack="True" Visible="False"></asp:TextBox>
                                &nbsp;<asp:Label ID="lblFuncionarioNome" runat="server" ForeColor="Blue" Visible="False"></asp:Label>
                            </div>
                            <asp:Label ID="lblIndividualColetivoMensagem" runat="server" Height="30px" Text="Para atendimento a múltiplos funcionários, informar as matrículas anexando documento na próxima etapa." Width="350px" CssClass="fontePadrao" Visible="False" ForeColor="Blue"></asp:Label>
                            <br />


                            <div class="col-md-6" style="vertical-align: central; background-color: white; opacity: 0.9">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
                                        <div style="text-align: center">
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

