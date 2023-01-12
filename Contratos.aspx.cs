using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using App_Code;

public partial class myHome : System.Web.UI.Page{

    readonly smsSelect smslinQcs = new smsSelect();
    readonly cSession SgcSess = new cSession();
    readonly Permissoes _consulta = new Permissoes();
    Persistencia_Fast consulta = new Persistencia_Fast();


    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (SgcSess.UserAdmin == "S")
            {
                btnImportarSGC.Enabled = true;
            }
        }
    }

    public static AjaxControlToolkit.Slide[] GetSlides(string contextKey)
    {
        return default(AjaxControlToolkit.Slide[]);
    }

    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("myHome.aspx");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("myHome.aspx");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Seleciona")
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView1.Rows[index];
            TableCell ID = selectedRow.Cells[1];

            Panel_Edita.Visible = true;
            txtId.Text = ID.Text;
            DropDownListBU.SelectedValue = consulta.Consulta("SELECT BUId FROM Contratos WHERE ContratoID = " + ID.Text, "BUId");
            txtDescricao.Text = consulta.Consulta("SELECT DescContract FROM Contratos WHERE ContratoID = " + ID.Text, "DescContract");
            RadioButtonListAtivoInativo.SelectedValue = consulta.Consulta("SELECT Ativo FROM Contratos WHERE ContratoID = " + ID.Text, "Ativo");
       }
        else
        {
            Panel_Edita.Visible = false;
        }
    }

    protected void btnFechar_Click(object sender, EventArgs e)
    {
        Panel_Edita.Visible = false;
    }
    protected void btnImportarSGC_Click(object sender, EventArgs e)
    {
        string QuantidadeNovosContratosService = consulta.Consulta(@"SELECT Count(ContratoID) as NumeroContratos
		                                                      FROM [SGC_NET_V1].[dbo].[Contrato]
		                                                      WHERE (ContratoId > 0 AND ((BU = 'Service') OR (BU = 'Central de Serviços')) 
		                                                      AND ContratoId Not In (SELECT [ContratoID]  FROM [DMS].[dbo].[Contratos]))", "NumeroContratos");

        string QuantidadeNovosContratosFacilities = consulta.Consulta(@"SELECT Count(ContratoID) as NumeroContratos
		                                                      FROM [SGC_NET_V1].[dbo].[Contrato]
		                                                      WHERE (ContratoId > 0 AND (BU = 'Facilities') 
		                                                      AND ContratoId Not In (SELECT [ContratoID]  FROM [DMS].[dbo].[Contratos]))", "NumeroContratos");


//A Importação de contrato da Systems foi desabilitada porque, no SGC, o controle é por CC/Projeto, o que gera uma lista muito extensa. A adição deve ser manual. Ou o usuário deve usar uma das BUs Systems já cadastradas.
//        string QuantidadeNovosContratosSystems = consulta.Consulta(@"SELECT Count(ContratoID) as NumeroContratos
//		                                                      FROM [SGC_NET_V1_SYSTEMS].[dbo].[Contrato]
//		                                                      WHERE (ContratoId > 0 AND (BU = 'Systems') 
//		                                                      AND ContratoId Not In (SELECT '21' + Cast([ContratoID] as Varchar(10)) FROM [DMS].[dbo].[Contratos]))", "NumeroContratos");


        string QuantidadeNovosContratosEnteCentral = consulta.Consulta(@"SELECT Count(ContratoID) as NumeroContratos
		                                                      FROM [SGC_NET_V1_ENTE_CENTRAL].[dbo].[Contrato]
		                                                      WHERE (ContratoId > 0 AND (BU = 'Ente Central') 
		                                                      AND ContratoId Not In (SELECT '19' + Cast([ContratoID] as Varchar(10)) FROM [DMS].[dbo].[Contratos]))", "NumeroContratos");

        //Importa do banco de dados da Service (SGC)
        consulta.atualizaInsereDados("INSERT INTO [DMS].[dbo].[Contratos] " +
                                       "([ContratoID],[BU],[DescContract],[Ativo],[BUId]) "+
                            "SELECT [ContratoID],[BU],[DescContract],1,1 FROM [SGC_NET_V1].[dbo].[Contrato] " +
		                              " WHERE (ContratoId >0 AND ((BU = 'Service') OR (BU = 'Central de Serviços') OR (BU = 'Facilities'))  " +
		                              " AND ContratoId Not In (SELECT [ContratoID]  FROM [DMS].[dbo].[Contratos]))");

        //Importa do banco de dados da Facilities (SGC)
        consulta.atualizaInsereDados("INSERT INTO [DMS].[dbo].[Contratos] " +
                                       "([ContratoID],[BU],[DescContract],[Ativo],[BUId]) " +
                            "SELECT [ContratoID],[BU],[DescContract],1,7 FROM [SGC_NET_V1].[dbo].[Contrato] " +
                                      " WHERE (ContratoId >0 AND BU = 'Facilities')  " +
                                      " AND ContratoId Not In (SELECT [ContratoID]  FROM [DMS].[dbo].[Contratos]))");

        //A Importação de contrato da Systems foi desabilitada porque, no SGC, o controle é por CC/Projeto, o que gera uma lista muito extensa. A adição deve ser manual. Ou o usuário deve usar uma das BUs Systems já cadastradas.
        ////Importa do banco de dados da Systems (SGC)
        //consulta.atualizaInsereDados("INSERT INTO [DMS].[dbo].[Contratos] " +
        //                               "([ContratoID],[BU],[DescContract],[Ativo],[BUId]) " +
        //                    "SELECT '20' + Cast([ContratoID] as Varchar(10)),[BU],[DescContract],1,2 FROM [SGC_NET_V1_SYSTEMS].[dbo].[Contrato] " +
        //                                " WHERE ContratoId >0 AND BU = 'Systems' " +
        //                                " AND ContratoId Not In (SELECT [ContratoID]  FROM [DMS].[dbo].[Contratos]))");

        //Importa do banco de dados da Ente Central (SGC)
        consulta.atualizaInsereDados("INSERT INTO [DMS].[dbo].[Contratos] " +
                                       "([ContratoID],[BU],[DescContract],[Ativo],[BUId]) " +
                             "SELECT '19' + Cast([ContratoID] as Varchar(10)),[BU],[DescContract],1,6 FROM [SGC_NET_V1_ENTE_CENTRAL].[dbo].[Contrato] " +
                                        " WHERE (ContratoId >0 AND ((BU = 'Ente Central'))  " +
                                        " AND ContratoId Not In (SELECT [ContratoID]  FROM [DMS].[dbo].[Contratos]))");

        GridView1.DataBind();

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Quantidade de novos contratos importados: " + 
                                                                                                (Convert.ToInt32(QuantidadeNovosContratosService) 
                                                                                            //+   Convert.ToInt32(QuantidadeNovosContratosSystems) 
                                                                                            +   Convert.ToInt32(QuantidadeNovosContratosEnteCentral)
                                                                                            +   Convert.ToInt32(QuantidadeNovosContratosFacilities)).ToString() + ".')", true);

    }
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtId.Text != "")
            {
                consulta.atualizaInsereDados("UPDATE Contratos SET BU = '" + DropDownListBU.SelectedItem + "', BUId = " + DropDownListBU.SelectedValue + ", Ativo = " + RadioButtonListAtivoInativo.SelectedValue + " WHERE ContratoID = " + txtId.Text);
                GridView1.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Gravado.')", true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Registro não gravado porque o ID não foi localizado.')", true);

        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha na gravação.')", true);
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
}