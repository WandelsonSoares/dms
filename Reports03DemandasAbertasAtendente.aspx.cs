using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class Reports03DemandasAbertasAtendente : System.Web.UI.Page
{
    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridView1.DataSource = SqlDataSourceTodos;
            GridView1.DataBind();

            usuario.LogIsert(appSession.FullName, "Relatório 3", "Visualizou relatório.", appSession.IP);
        }
    }
    protected void btnAplicar_Click(object sender, EventArgs e)
    {
        if (txtDataInicio.Text != "" && txtDataFim.Text != "")
        {
            try
            {
                string sql = "SELECT Usuarios.Nome AS Atendente, Atividades.Nome AS Atividade, Subprocessos.Nome AS Subprocesso, " +
                            " Demandas.DataPrazo, Demandas.Detalhe, Contratos.DescContract AS Contrato, Demandas.Status " +
                            " FROM Usuarios INNER JOIN Demandas ON Usuarios.UserID = Demandas.ResponsavelId INNER JOIN Atividades " +
                            " ON Demandas.AtividadeId = Atividades.AtividadeId INNER JOIN Subprocessos ON Atividades.SubprocessoId = Subprocessos.SubprocessoId " +
                            " INNER JOIN Contratos ON Demandas.CNId = Contratos.ContratoID WHERE (Demandas.Status <> 'CONCLUIDA') AND (Demandas.Status <> 'CANCELADA') " +
                            " AND (Demandas.DataPrazo BETWEEN '" + Convert.ToDateTime(txtDataInicio.Text).ToString("yyyy-MM-dd")
                            + "' AND '" + Convert.ToDateTime(txtDataFim.Text).ToString("yyyy-MM-dd") + "') ORDER BY Subprocesso, Contrato, Demandas.DataPrazo";

                var ds = consult.DTSetConsulta(sql);

                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível filtrar. Informe datas em formato válido.')", true);
                txtDataInicio.Focus();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Informe data inicial e data final.')", true);
            txtDataInicio.Focus();
        }

   }
    protected void btnRemover_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = SqlDataSourceTodos;
        GridView1.DataBind();
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        for (int i = GridView1.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = GridView1.Rows[i];
            GridViewRow previousRow = GridView1.Rows[i - 1];
            for (int j = 0; j < 4; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                        }
                        row.Cells[j].Visible = false;
                    }
                }
            }
        }
    }
    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        Session["ctrl"] = Panel1;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'Imprimir.aspx', 'Imprimir', 'height=500,width=1300,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes' );", true);

    }
}