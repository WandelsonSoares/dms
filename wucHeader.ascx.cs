using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class wucHeader : System.Web.UI.UserControl
{

    private readonly cSession appSession = new cSession();
    readonly Permissoes permissao = new Permissoes();
    Persistencia_Fast consult = new Persistencia_Fast();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            appSession.GetPagina = Request.Url.LocalPath.ToString();

            _Usuario refresh = new _Usuario();
            refresh.refresUserOnlineStatus();

            
            try
            {
                lblOnLineUsers.Text = consult.Consulta("SELECT Count(UserID) as UserOnline FROM UsuariosOnline", "UserOnline");
                lblOnLineUsers.Visible = true;
                lblNotificacoes.Text = NotificacoesNaoLidasConta(appSession.UserId);

                if (lblNotificacoes.Text != "" && lblNotificacoes.Text != "0")
                    lblNotificacoes.Visible = true;
                else
                    lblNotificacoes.Visible = false;
            }
            catch
            {
                lblOnLineUsers.Text = "#";
                lblOnLineUsers.Visible = true;
            }
        }
    }

    private void Logout()
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }

    protected void btn_login_Click(object sender, EventArgs e)
    {
        Logout();

    }

    private string NotificacoesNaoLidasConta(string usuarioId)
    {
        string NotificacoesNaoLidas = "0";

        NotificacoesNaoLidas = consult.Consulta("SELECT COUNT (NotificacaoId) AS Quantidade FROM Notificacoes WHERE DestinatarioId = " + usuarioId + " AND Lida = 0", "Quantidade");

        return NotificacoesNaoLidas;


    }
    protected void ImgBtnConfiguracoes_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("UsuariosPreferencias.aspx?id=" + appSession.UserId);
    }
    protected void ImgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtPesquisa.Text != "")
        {
            Response.Redirect("PesquisasServicoResultado.aspx?s=%" + tira_acentos(txtPesquisa.Text.Replace(" ","%").Replace("'","")) + "%");
        }
    }

    public static string tira_acentos(string texto)
    {
        string ComAcentos = "!@#$%¨&*()-?:{}][ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç ";

        string SemAcentos = "_________________AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc_";

        for (int i = 0; i < ComAcentos.Length; i++)

            texto = texto.Replace(ComAcentos.ToString(), SemAcentos.ToString()).Trim();
        return texto;
    }
}

