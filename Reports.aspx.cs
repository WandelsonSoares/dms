using System;
using App_Code;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Email;

public partial class ActionPlan : System.Web.UI.Page
{
    private readonly cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (appSession.UserAdmin == "S")
            {

            }
            usuario.LogIsert(appSession.FullName, "Relatórios", "Acessou tela de relatórios.", appSession.IP);
        }

    }

    protected void btn01_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'Reports01DemandasAtrasadas.aspx', null, 'height=700,width=1300,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes' );", true);
    }
    protected void btn0_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'Reports02DemandasVencendo.aspx', null, 'height=700,width=1300,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes' );", true);
    }
}