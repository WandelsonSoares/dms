using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using System.Data.Objects;

public partial class Funcionarios : System.Web.UI.Page
{
    readonly Persistencia_Fast consult = new Persistencia_Fast();
    readonly insert_ GravaPA = new insert_();
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridView1.DataSource = SqlDataSourceGeral100;
            GridView1.DataBind();

            usuario.LogIsert(appSession.FullName, "Funcionários", "Acessou tela Funcionários.", appSession.IP);
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'FuncionariosUpload.aspx', null, 'height=450,width=800,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes' );", true);
    }
    protected void txtNomeFiltro_TextChanged(object sender, EventArgs e)
    {
        if (txtNomeFiltro.Text != "")
            txtMatriculaFiltro.Text = "";
    }
    protected void txtMatriculaFiltro_TextChanged(object sender, EventArgs e)
    {
        if (txtMatriculaFiltro.Text != "")
            txtNomeFiltro.Text = "";
    }
    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        if (txtNomeFiltro.Text != "")
        {
            GridView1.DataSource = SqlDataSourceFiltraNome;
            GridView1.DataBind();
        }

        if (txtMatriculaFiltro.Text != "")
        {
            GridView1.DataSource = SqlDataSourceFiltraMatricula;
            GridView1.DataBind();
        }
    }
}