using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class Configuracoes : System.Web.UI.Page
{
    cSession appSession = new cSession();
    Persistencia_Fast consult = new Persistencia_Fast();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (appSession.UserAdmin == "S")
                btnEstruturaOrganizacional.Enabled = true;

            CheckBoxEnviarEmailDemandasCriacao.Checked = Convert.ToBoolean(consult.Consulta("SELECT EnviarEmailDemandasCriacao FROM ConfiguracoesEmails", "EnviarEmailDemandasCriacao"));
            CheckBoxEnviarEmailDemandasAtrasadas.Checked = Convert.ToBoolean(consult.Consulta("SELECT EnviarEmailDemandasAtrasadas FROM ConfiguracoesEmails", "EnviarEmailDemandasAtrasadas"));
            CheckBoxEnviarEmailDemandasAtualizacao.Checked = Convert.ToBoolean(consult.Consulta("SELECT EnviarEmailDemandasAtualizacao FROM ConfiguracoesEmails", "EnviarEmailDemandasAtualizacao"));

            usuario.LogIsert(appSession.FullName, "Configurações", "Acessou tela de configurações.", appSession.IP);

        }
    }
    protected void CheckBoxEnviarEmailDemandasCriacao_CheckedChanged(object sender, EventArgs e)
    {
        consult.atualizaInsereDados("UPDATE ConfiguracoesEmails SET EnviarEmailDemandasCriacao = " + Convert.ToInt32(CheckBoxEnviarEmailDemandasCriacao.Checked));
    }
    protected void CheckBoxEnviarEmailDemandasAtrasadas_CheckedChanged(object sender, EventArgs e)
    {
        consult.atualizaInsereDados("UPDATE ConfiguracoesEmails SET EnviarEmailDemandasAtrasadas = " + Convert.ToInt32(CheckBoxEnviarEmailDemandasAtrasadas.Checked));
    }
    protected void CheckBoxEnviarEmailDemandasAtualizacao_CheckedChanged(object sender, EventArgs e)
    {
        consult.atualizaInsereDados("UPDATE ConfiguracoesEmails SET EnviarEmailDemandasAtualizacao = " + Convert.ToInt32(CheckBoxEnviarEmailDemandasAtualizacao.Checked));
    }
}