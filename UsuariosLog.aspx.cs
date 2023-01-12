using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UsuariosLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex != -1)
        {

            e.Row.Attributes.Add("onMouseover", "this.style.background='#DFEFFF'");

            if (e.Row.RowIndex % 2 == 1)
            {
                e.Row.Attributes.Add("onMouseout", "this.style.background='#FFFFFF'");
            }
            else
            {
                e.Row.Attributes.Add("onMouseout", "this.style.background='#FFFFFF'");
            }
        }
    }
}