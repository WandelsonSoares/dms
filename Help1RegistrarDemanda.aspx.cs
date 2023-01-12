using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class Help1RegistrarDemanda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cSession appSession = new cSession();
        _Usuario usuario = new _Usuario();

        if (!IsPostBack)
            usuario.LogIsert(appSession.FullName, "Help", "Visualizou tela Help - Como registrar demanda (vídeo)", appSession.IP);
    }
}