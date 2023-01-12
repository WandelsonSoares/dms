using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class Feriados : System.Web.UI.Page
{
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregaAnos();
            DropDownListAno.SelectedValue = DateTime.Today.Year.ToString();

            usuario.LogIsert(appSession.FullName, "Feriados", "Acessou tela Feriados.", appSession.IP);
        }
    }

    private void CarregaAnos()
    {
        for (int i = 0; i < 10; i++)
        {
            DropDownListAno.Items.Insert(i, (Convert.ToInt32(DateTime.Today.Year) + i).ToString());
        }
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
    protected void DropDownListAno_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
}