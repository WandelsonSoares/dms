using System;

using System.Data;

using System.Configuration;

using System.Web;

using System.Web.Security;

using System.Web.UI;

using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;

using System.Web.UI.HtmlControls;



/// <summary>

/// Summary description for Class1

/// </summary>

public class MinhaPagina : Page
{



    public override void VerifyRenderingInServerForm(Control controle)
    {



        GridView grid = controle as GridView;



        if (grid != null && grid.ID == "GridView1")

            return;

        else

            base.VerifyRenderingInServerForm(controle);



    }



}