<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Contratos.aspx.cs" Inherits="myHome" %>

<%@ Register src="wucMenu.ascx" tagname="wucMenu" tagprefix="uc2" %>
<%@ Register src="wucHeader.ascx" tagname="wucHeader" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style22
        {
            height: 104px;
        }
        .auto-style3 {
            height: 104px;
            width: 104px;
        }
        .auto-style25 {
            text-align: center;
        }
        .auto-style26 {
            position: relative;
            top: 115px;
            left: -72px;
            height: 33px;
            width: 50px;
        }
        .auto-style27 {
            color: #0000CC;
        }
        .auto-style28 {
            height: 23px;
        }
        .auto-style29 {
            height: 32px;
        }
    </style>

<link href="Content/bootstrap.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
    <uc1:wucHeader ID="wucHeader1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
    <table bgcolor="#E6E6E6" width="100%" align=center cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top"  >
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <uc2:wucMenu ID="wucMenu1" runat="server" />
                        </td>
                    </tr>
                    </table>
            </td>
            <td valign="top" width="100%" >
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <div class="auto-style25">
                                    <asp:Label ID="lblCarregando" runat="server" CssClass="auto-style27" Font-Names="Trebuchet MS" Font-Size="10pt" Text="Carregando..."></asp:Label>
                                    <img alt="" src="img2/progresss.gif" class="auto-style26"/>
                                    &nbsp;</div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <table bgcolor="#E6E6E6" cellpadding="2" cellspacing="2" width="100%" border="0" 
                            style="height: 50px">
                            <tr>
                                <td style="text-align: center" class="auto-style3">
                                    &nbsp;</td>
                                <td  
                                    valign="top" width="100%" class="style22">
                                    <asp:Button ID="btnImportarSGC" CssClass="btn btn-primary" runat="server" OnClick="btnImportarSGC_Click" Text="Importar do SGC" Width="150px" Enabled="False" />
                                    <br />
                                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ContratoID" DataSourceID="SqlDataSourceContratos" EmptyDataText="Nenhum plano cadastrado." GridLines="Horizontal" onrowcommand="GridView1_RowCommand" style="text-align: center;" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" CommandName="Seleciona" ImageUrl="~/img2/lupa_clean.png" Text="Ver">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="1px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="ContratoID" HeaderText="Id" ReadOnly="True" SortExpression="ContratoID" >
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BU" HeaderText="BU" SortExpression="BU" >
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="15px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DescContract" HeaderText="Contrato" SortExpression="DescContract" >
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:BoundField>

                                            <asp:ImageField DataImageUrlField="Ativo" DataImageUrlFormatString="~/img/contratos/{0}.png" HeaderText="Ativo">
                                                <ItemStyle Width="15px" />
                                            </asp:ImageField>

                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <HeaderStyle  Font-Bold="true" ForeColor="Black" Height="10px" />
                                        <RowStyle BorderStyle="Dotted" BorderWidth="1px" Height="10px" />
                                        <SelectedRowStyle BackColor="#6699FF" />
                                       
                                    </asp:GridView>
                                    <asp:Panel ID="Panel_Edita" runat="server" BackColor="#FFFFCC" Width="500px" Visible="False">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Id:"></asp:Label>
                                                    &nbsp;<asp:TextBox ID="txtId" runat="server" Enabled="False" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                                <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="BU"></asp:Label>
                                                    &nbsp;<asp:DropDownList ID="DropDownListBU" runat="server" Enabled="False" DataSourceID="SqlDataSourceBUs" DataTextField="Nome" DataValueField="BUId">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSourceBUs" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Nome, BUId FROM BUs ORDER BY Nome"></asp:SqlDataSource>
                                                </td>
                                            </tr>
                                                <tr>
                                                <td class="auto-style28">
                                                    <asp:Label ID="Label3" runat="server" Text="Descrição:"></asp:Label>
                                                    <asp:TextBox ID="txtDescricao" runat="server" Enabled="False" Width="400px" EnableTheming="True"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:RadioButtonList ID="RadioButtonListAtivoInativo" runat="server" RepeatDirection="Horizontal" Width="170px" Font-Bold="False">
                                                        <asp:ListItem Value="1">Ativo</asp:ListItem>
                                                        <asp:ListItem Value="0">Inativo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style25">
                                                    <asp:Button ID="btnGravar" CssClass="btn btn-primary" runat="server" Text="Gravar" Width="150px" OnClick="btnGravar_Click" />
                                                    &nbsp;<asp:Button ID="btnFechar" runat="server" CssClass="btn btn-danger" OnClick="btnFechar_Click" Text="Fechar" Width="150px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>

                                        </table>
                                    </asp:Panel>
                                    <asp:AlwaysVisibleControlExtender ID="Panel_Edita_AlwaysVisibleControlExtender" runat="server" BehaviorID="Panel_Edita_AlwaysVisibleControlExtender" HorizontalSide="Center" TargetControlID="Panel_Edita" VerticalSide="Middle">
                                    </asp:AlwaysVisibleControlExtender>
                                    <asp:SqlDataSource ID="SqlDataSourceContratos" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT ContratoID, BU, DescContract, Ativo FROM Contratos ORDER BY DescContract"></asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSourceUsuarios" runat="server" ConnectionString="<%$ ConnectionStrings:SGC_NET_V1ConnectionString1 %>" SelectCommand="SELECT Nome, UserID From Usuarios WHERE (Bloqueado = 0) ORDER BY Nome"></asp:SqlDataSource>
                                </td>
                                <td>


                                </td>
                            </tr>

                        </table>

                        
                     

                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>
            </td>
        </tr>
        </table>
    </asp:Content>


