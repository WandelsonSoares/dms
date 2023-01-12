using App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Usuarios : System.Web.UI.Page
{
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            usuario.LogIsert(appSession.FullName, "Usuários", "Acessou tela de usuários.", appSession.IP);
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Seleciona")
        {
            int index = -1;

            index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView1.Rows[index];
            TableCell Id = selectedRow.Cells[3];

            Response.Redirect("UsuariosEditar.aspx?id=" + Id.Text);
        }
    }
}