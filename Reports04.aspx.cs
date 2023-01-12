using App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports04 : System.Web.UI.Page
{
    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();
    AppStoredProcedures storedProcedure = new AppStoredProcedures();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregaDatasIniciais();
            CarregaRelatorio();

            usuario.LogIsert(appSession.FullName, "Relatório 4", "Visualizou relatório.", appSession.IP);
        }

    }

    private void CarregaRelatorio()
    {
        if (txtDataInicio.Text != "" && txtDataFim.Text != "")
        {
            try
            {

                storedProcedure.ExecutaSP_Relatorio04(Convert.ToDateTime(txtDataInicio.Text), Convert.ToDateTime(txtDataFim.Text), Convert.ToInt32(appSession.UserId));

                string sql = "SELECT Atendente, DemandasRecebidas, DemandasVencendo, DemandasAtendidas, DemandasAtendidasPrazo, DemandasAtendidasForaPrazo, DemandasNaoAtendidas " +
                            " FROM Relatorio04  WHERE UserId = " + appSession.UserId + " ORDER BY Atendente";

                var ds = consult.DTSetConsulta(sql);

                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível filtrar. Informe datas em formato válido. " + ex.Message + "')", true);
                txtDataInicio.Focus();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Informe data inicial e data final.')", true);
            txtDataInicio.Focus();
        }
    }

    private void CarregaDatasIniciais()
    {
        txtDataInicio.Text = "1/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        txtDataFim.Text = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month).ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
    }
    protected void btnAplicar_Click(object sender, EventArgs e)
    {
        CarregaRelatorio();
    }
    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        Session["ctrl"] = Panel1;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'Imprimir.aspx', 'Imprimir', 'height=500,width=1300,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes' );", true);

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