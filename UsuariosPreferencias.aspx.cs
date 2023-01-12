using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class UsuariosPreferencias : System.Web.UI.Page
{
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();
    readonly Persistencia_Fast consult = new Persistencia_Fast();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (appSession.UserId != Request.QueryString["id"])
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Acesso não autorizado às preferências deste usuário.')", true);
                Response.Redirect("Home.aspx");
                usuario.LogIsert(appSession.FullName, "Usuários - Preferências", "Teve acesso negado para preferências do usuário id " + Request.QueryString["id"] + ".", appSession.IP);
            }
            else
            {
                usuario.LogIsert(appSession.FullName, "Usuários - Preferências", "Acessou tela de preferências de usuário.", appSession.IP);
                CheckBoxNaoExibirPainelEtapasAtrasadas.Checked = !Convert.ToBoolean(consult.Consulta("SELECT IsNull(ExibePainelEtapas, 0) AS ExibePainelEtapas FROM UsuariosConfig WHERE UserId = " + Request.QueryString["id"], "ExibePainelEtapas"));
            }
        }
    }
    protected void CheckBoxNaoExibirPainelEtapasAtrasadas_CheckedChanged(object sender, EventArgs e)
    {
        if (consult.Consulta("SELECT COUNT (UserId) AS Quantidade FROM UsuariosConfig WHERE UserId = " + appSession.UserId, "Quantidade") == "0")
            consult.atualizaInsereDados("INSERT INTO UsuariosConfig (UserId, ExibePainelEtapas) VALUES (" + appSession.UserId + ", 1)");

        consult.atualizaInsereDados("UPDATE UsuariosConfig SET ExibePainelEtapas = " + Convert.ToInt32(!CheckBoxNaoExibirPainelEtapasAtrasadas.Checked) + " WHERE UserId = " + Request.QueryString["id"]);
    }
}