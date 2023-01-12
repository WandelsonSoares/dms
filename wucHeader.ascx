<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucHeader.ascx.cs" Inherits="wucHeader" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<link rel="shortcut icon" type="image/x-icon" href="/favicon.ico">
<link href="css/StyleSheet.css" rel="stylesheet" />
<link href="Content/bootstrap.min.css" rel="stylesheet" />


<script type="text/jscript">
    var gAutoPrint = true; // Flag for whether or not to automatically call the print function

    function printSpecial() {
        if (document.getElementById != null) {
            var html = '<HTML>\n<HEAD>\n';

            if (document.getElementsByTagName != null) {
                var headTags = document.getElementsByTagName("head");
                if (headTags.length > 0)
                    html += headTags[0].innerHTML;
            }

            html += '\n</HE' + 'AD>\n<BODY>\n';

            var printReadyElem = document.getElementById("printReady");

            if (printReadyElem != null) {
                html += printReadyElem.innerHTML;
            }
            else {
                alert("Could not find the printReady section in the HTML");
                return;
            }

            html += '\n</BO' + 'DY>\n</HT' + 'ML>';

            var printWin = window.open("", "printSpecial");
            printWin.document.open();
            printWin.document.write(html);
            printWin.document.close();
            if (gAutoPrint)
                printWin.print();
        }
        else {
            alert("Sorry, the print ready feature is only available in modern browsers.");
        }
    }
    /**************************************************************************
    Função para simular um Tab quando for pressionado a tecla Enter
    Exemplo: onKeyDown="TABEnter()"
    Funciona em TEXT BOX,RADIO BUTTON, CHECK BOX e menu DROP-DOWN
    **************************************************************************/
    function TABEnter(oEvent) {
        var oEvent = (oEvent) ? oEvent : event;
        var oTarget = (oEvent.target) ? oEvent.target : oEvent.srcElement;
        if (oEvent.keyCode == 13)
            oEvent.keyCode = 9;
        if (oTarget.type == "text" && oEvent.keyCode == 13)
            //return false;
            oEvent.keyCode = 9;
        if (oTarget.type == "radio" && oEvent.keyCode == 13)
            oEvent.keyCode = 9;
    }

    //Formata valores
    function currencyFormat(fld, milSep, decSep, e) {
        var sep = 0;
        var key = '';
        var i = j = 0;
        var len = len2 = 0;
        var strCheck = '0123456789';
        var aux = aux2 = '';
        var whichCode = (window.Event) ? e.which : e.keyCode;
        if (whichCode == 13) return true;// Enter
        key = String.fromCharCode(whichCode);// Get key value from key code
        if (strCheck.indexOf(key) == -1) return false;// Not a valid key
        len = fld.value.length;
        for (i = 0; i < len; i++)
            if ((fld.value.charAt(i) != '0') && (fld.value.charAt(i) != decSep)) break;
        aux = '';
        for (; i < len; i++)
            if (strCheck.indexOf(fld.value.charAt(i)) != -1) aux += fld.value.charAt(i);
        aux += key;
        len = aux.length;
        if (len == 0) fld.value = '';
        if (len == 1) fld.value = '0' + decSep + '0' + aux;
        if (len == 2) fld.value = '0' + decSep + aux;
        if (len > 2) {
            aux2 = '';
            for (j = 0, i = len - 3; i >= 0; i--) {
                if (j == 3) {
                    aux2 += milSep;
                    j = 0;
                }
                aux2 += aux.charAt(i);
                j++;
            }
            fld.value = '';
            len2 = aux2.length;
            for (i = len2 - 1; i >= 0; i--)
                fld.value += aux2.charAt(i);
            fld.value += decSep + aux.substr(len - 2, len);
        }
        return false;
    }
</script>

<%--Script Sem mensagem de erro!--%>
<script type="text/javascript">
    function semerro() {
        return true;
    }
    window.onerror = semerro;
</script>

<style type="text/css">
    img {
        border: 0px;
    }

        img a, img a:link, img a:active, img a:visited, img a:hover {
            border: 0px;
        }

    /* Sombra */
    .Sombra {
        border-left: 1px solid Silver;
        border-top: 1px solid Silver;
        border-right: 3px solid #727272;
        border-bottom: 3px solid #727272;
        text-align: center;
    }

    .SombraRed {
        border-left: 1px solid Silver;
        border-top: 1px solid Silver;
        border-right: 3px solid #727272;
        border-bottom: 3px solid #727272;
        text-align: center;
        background: #FF9999;
    }

    .Wattermarks {
        background: #FFFFFF url(img2/alerts.gif) no-repeat 4px 4px;
        padding: 4px 4px 4px 22px;
    }

    .style8 {
        text-align: left;
    }


    .style9 {
        font-family: Tahoma;
        font-size: 8pt;
        color: #999999;
    }
</style>
<body style="margin-top: 0px; margin-left: 0px; margin-right: 0px;">
    <div class="masterPageHeaderDiv">
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 240px; text-align: center; vertical-align: middle">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/img2/Logo-COMAU-white.png" PostBackUrl="~/Home.aspx" />
                    &nbsp;&nbsp;</td>

                <td style="text-align: left; width: 800px; vertical-align: middle">
                    <asp:Label ID="lblTitulo" Width="800px" runat="server" ForeColor="White" Font-Size="18pt" Text="DMS - Demand Management System" Font-Bold="False" Font-Names="Tahoma"></asp:Label>

                    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"
                        EnableScriptGlobalization="True" AsyncPostBackTimeout="3600">
                    </ajaxToolkit:ToolkitScriptManager>

                </td>
                <td align="right" style="background-repeat: no-repeat; vertical-align: middle">

                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPesquisa" runat="server" Height="24px" BackColor="#3CBDFE" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Size="10pt" ForeColor="White" Width="200px" Font-Italic="True"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="txtPesquisa_TextBoxWatermarkExtender" runat="server" BehaviorID="txtPesquisa_TextBoxWatermarkExtender" TargetControlID="txtPesquisa" WatermarkText="Pesquisar serviço">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                            </td>
                            <td style="height: 16px; width: 10px; padding-left: 4px; padding-right: 4px">
                                <asp:ImageButton ID="ImgBtnSearch" runat="server" ImageUrl="~/img3/search.png" Width="24px" OnClick="ImgBtnSearch_Click" />
                            </td>
                            <td style="padding-left: 4px; padding-right: 0px">
                                <asp:ImageButton ID="ImgBtnUsers" runat="server" ImageUrl="~/img3/user2.png" ToolTip="Número de usuários online." />
                            </td>

                            <td style="padding-left: 0;" height="16" valign="top" align="left">

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="lblOnLineUsers" runat="server"
                                            BorderColor="Transparent" Height="16px" Width="16px" BackColor="#3cbdfe" ForeColor="White"
                                            Text="0" Visible="True" class="masterPageHeaderNotificacoesLabel" Style="padding-left: 0;" ToolTip="Número de usuários online." />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>

                            <td style="padding-left: 4px; padding-right: 4px">
                                <asp:ImageButton ID="ImageButtonNotifications" runat="server" ImageUrl="~/img3/notification.png" PostBackUrl="~/Notificacoes.aspx" />
                            </td>

                            <td style="padding-left: 0; padding-right: 4px; vertical-align: middle; text-align: center">

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Label class="masterPageHeaderNotificacoesLabel" Height="16px" Width="16px" BackColor="Red" ID="lblNotificacoes" runat="server" Text="0" Font-Names="Arial" Font-Bold="True" Font-Size="8pt"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>

                            <td style="padding-left: 4px; padding-right: 4px">
                                <asp:ImageButton ID="ImgBtnConfiguracoes" runat="server" ImageUrl="~/img3/gear2.png" Width="24px" OnClick="ImgBtnConfiguracoes_Click" />
                            </td>

                            <td style="padding-left: 4px; padding-right: 30px">
                                <asp:LinkButton ID="LinkButtonSair" runat="server" Font-Names="Tahoma"
                                    Font-Size="10pt" OnClick="btn_login_Click" ForeColor="White">Sair</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>
    </div>
</body>
