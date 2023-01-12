using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class Sobre : System.Web.UI.Page
{
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
         usuario.LogIsert(appSession.FullName, "Sobre", "Visualizou a tela Sobre.", appSession.IP);
    }
    protected void LinkButtonSobreDesenvolvedor_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '/html/Index.html', null, 'height=1024,width=768,status=yes,toolbar=yes,menubar=yes,location=no,scrollbars=yes,resizable=yes' );", true);
        usuario.LogIsert(appSession.FullName, "Sobre", "Acessou a tela [+ Sobre o Desenvolvedor].", appSession.IP);
    }
}