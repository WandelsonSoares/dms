<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AvaliacaoSatisfacao.aspx.cs" Inherits="AvaliacaoSatisfacao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Script/JavaScript.js"></script>
</head>
<body>
    <form id="form1" runat="server" style="">
        <div style="padding: 10px; text-align: left">

            <br />
            <div style="text-align: center; font-weight: bold; font-size: 14px">
                <asp:Label ID="lblTitulo" runat="server" Text="AVALIAÇÃO DE SATISFAÇÃO"></asp:Label>
            </div>
            <div style="padding: 20px">
                <div>
                    <asp:Label ID="lblCabecalho" runat="server" Text="Informe abaixo seu nível de satisfação com o atendimento recebido. Seu nome não será divulgado."></asp:Label>
                    <br />
                    <br />
                </div>
                <div>
                    <div>
                        <asp:Label ID="lblQuestao1" runat="server" Text="1. A resposta dada à sua demanda."></asp:Label>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Muito Insatisfeito"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" CellPadding="5">
                                        <asp:ListItem>0</asp:ListItem>
                                        <asp:ListItem Value="1"></asp:ListItem>
                                        <asp:ListItem Value="2"></asp:ListItem>
                                        <asp:ListItem Value="3"></asp:ListItem>
                                        <asp:ListItem Value="4"></asp:ListItem>
                                        <asp:ListItem Value="5"></asp:ListItem>
                                        <asp:ListItem Value="6"></asp:ListItem>
                                        <asp:ListItem Value="7"></asp:ListItem>
                                        <asp:ListItem Value="8"></asp:ListItem>
                                        <asp:ListItem Value="9"></asp:ListItem>
                                        <asp:ListItem Value="10"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Muito Satisfeito"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <hr />

                </div>
                <br />
                <div>
                    <div>
                        <asp:Label ID="Label3" runat="server" Text="2. O tempo de resposta à sua demanda"></asp:Label>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Muito Insatisfeito"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal" CellPadding="5">
                                        <asp:ListItem>0</asp:ListItem>
                                        <asp:ListItem Value="1"></asp:ListItem>
                                        <asp:ListItem Value="2"></asp:ListItem>
                                        <asp:ListItem Value="3"></asp:ListItem>
                                        <asp:ListItem Value="4"></asp:ListItem>
                                        <asp:ListItem Value="5"></asp:ListItem>
                                        <asp:ListItem Value="6"></asp:ListItem>
                                        <asp:ListItem Value="7"></asp:ListItem>
                                        <asp:ListItem Value="8"></asp:ListItem>
                                        <asp:ListItem Value="9"></asp:ListItem>
                                        <asp:ListItem Value="10"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Muito Satisfeito"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <hr />

                </div>
                <div>
                    <div>
                        <asp:Label ID="Label6" runat="server" Text="3. A cordialidade do atendente"></asp:Label>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Muito Insatisfeito"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal" CellPadding="5">
                                        <asp:ListItem>0</asp:ListItem>
                                        <asp:ListItem Value="1"></asp:ListItem>
                                        <asp:ListItem Value="2"></asp:ListItem>
                                        <asp:ListItem Value="3"></asp:ListItem>
                                        <asp:ListItem Value="4"></asp:ListItem>
                                        <asp:ListItem Value="5"></asp:ListItem>
                                        <asp:ListItem Value="6"></asp:ListItem>
                                        <asp:ListItem Value="7"></asp:ListItem>
                                        <asp:ListItem Value="8"></asp:ListItem>
                                        <asp:ListItem Value="9"></asp:ListItem>
                                        <asp:ListItem Value="10"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Muito Satisfeito"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <hr />

                </div>
                <div>
                    <div>
                        <asp:Label ID="Label9" runat="server" Text="4. A clareza na resposta à sua demanda."></asp:Label>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="Muito Insatisfeito"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal" CellPadding="5">
                                        <asp:ListItem>0</asp:ListItem>
                                        <asp:ListItem Value="1"></asp:ListItem>
                                        <asp:ListItem Value="2"></asp:ListItem>
                                        <asp:ListItem Value="3"></asp:ListItem>
                                        <asp:ListItem Value="4"></asp:ListItem>
                                        <asp:ListItem Value="5"></asp:ListItem>
                                        <asp:ListItem Value="6"></asp:ListItem>
                                        <asp:ListItem Value="7"></asp:ListItem>
                                        <asp:ListItem Value="8"></asp:ListItem>
                                        <asp:ListItem Value="9"></asp:ListItem>
                                        <asp:ListItem Value="10"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Text="Muito Satisfeito"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <hr />
                    <asp:Label ID="lblComentarioAvaliador" runat="server" Text="Comentário:"></asp:Label>
                    <asp:TextBox ID="txtComentarioAvaliador" runat="server" TextMode="MultiLine" Width="100%" Height="50px" MaxLength="500"></asp:TextBox>

                </div>
            </div>
            <div style="text-align: center">
                <asp:Button ID="btnConcluir" CssClass="btn btn-primary" runat="server" Text="Concluir" Enabled="False" OnClick="btnConcluir_Click" />
            </div>

        </div>

        <div style="padding: 10px">
        </div>
    </form>
</body>
</html>

