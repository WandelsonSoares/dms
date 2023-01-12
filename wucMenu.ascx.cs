using System;
using System.Web.UI;
using System.Drawing;
using App_Code;

//using System.Threading;

public partial class wucMenu : UserControl
{
    private readonly cSession appSession = new cSession();
    readonly smsSelect smsRead = new smsSelect();
    readonly Permissoes permissao = new Permissoes();

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (permissao.PermissaoParaSistemaArea(appSession.UserId, "1") == true || appSession.UserAdmin == "S")
                LinkButtonDemandas.Enabled = true;

            if (permissao.PermissaoParaSistemaArea(appSession.UserId, "2") == true || appSession.UserAdmin == "S")
                LinkButtonRelatorios.Enabled = true;

            if (permissao.PermissaoParaSistemaArea(appSession.UserId, "3") == true || appSession.UserAdmin == "S")
                LinkButtonConfiguracoes.Enabled = true;

            if (permissao.PermissaoParaSistemaArea(appSession.UserId, "4") == true || appSession.UserAdmin == "S")
                LinkButtonUsuarios.Enabled = true;
            
        }
    }


}
