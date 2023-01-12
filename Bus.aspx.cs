using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;


public partial class Bus : System.Web.UI.Page
{
    private readonly cSession appSession = new cSession();
    Persistencia_Fast consult = new Persistencia_Fast();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        wucMenu1.Visible = false;

        if (!IsPostBack)
        {
            carregaEmpresas();
            usuario.LogIsert(appSession.FullName, "BUs", "Acessou tela de BUs", appSession.IP);
        }
        if (Request.QueryString["id"] != "")
            Label1.Text = "BUs da Empresa " + consult.Consulta("SELECT Nome FROM Empresas WHERE EmpresaId = " + Request.QueryString["id"], "Nome") + ":";
    }

    public void carregaEmpresas()
    {
        var ds = consult.CarregaEmpresas();
        if (ds != null)
        {
            DropDownListEmpresa.DataSource = ds.Tables["Empresas"];
            DropDownListEmpresa.DataTextField = ds.Tables["Empresas"].Columns["Nome"].ToString();
            DropDownListEmpresa.DataValueField = ds.Tables["Empresas"].Columns["EmpresaID"].ToString();
            DropDownListEmpresa.DataBind();
            DropDownListEmpresa.Items.Insert(0, "Selecione...");
            DropDownListEmpresa.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar empresas.')", true);
        }
        if (DropDownListEmpresa.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar empresas.')", true);
        }
    }
    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        LimpaCampos();
        if (Request.QueryString["id"] != null)
            DropDownListEmpresa.SelectedValue = Request.QueryString["id"];
    }
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        if (txtId.Text == "")
        {
            try
            {
                var maiorId = consult.Consulta("SELECT IsNull(MAX (BUId),0) AS MaiorId FROM BUs", "MaiorId");
                int novoId = Convert.ToInt32(maiorId) + 1;
                consult.atualizaInsereDados("INSERT INTO BUs VALUES (" + novoId + ", '" + txtNome.Text.Replace("'", "").Replace("/", "") + "', " + DropDownListEmpresa.SelectedValue + ")" );
                carregaRegistro(novoId.ToString());
                GridView1.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Gravado com sucesso.')", true);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha na gravação.')", true);
            }

        }
        else
        {
            try
            {
                consult.atualizaInsereDados("UPDATE BUs SET NOME = '" + txtNome.Text.Replace("'", "").Replace("/", "") + "', EmpresaId = " + DropDownListEmpresa.SelectedValue + " WHERE BUId = " + txtId.Text);
                GridView1.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Gravado com sucesso.')", true);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha na gravação.')", true);
            }

        }
    }

    protected void carregaRegistro(string id)
    {
        txtId.Text = id;
        txtNome.Text = consult.Consulta("SELECT NOME FROM BUs WHERE BUId = " + id, "NOME");
        DropDownListEmpresa.SelectedValue = consult.Consulta("SELECT EmpresaId FROM BUs WHERE BUId = " + id, "EmpresaId");
    }



    protected void GridViewBUs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = -1;

        #region Seleciona
        if (e.CommandName == "Seleciona")
        {
            index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView1.Rows[index];
            TableCell IdRegistro = selectedRow.Cells[3];

            carregaRegistro(IdRegistro.Text);
            
            Panel1.Visible = true;

        }
        else
        {
            Panel1.Visible = false;
            LimpaCampos();
        }
        #endregion
    }

    protected void LimpaCampos()
    {
        txtId.Text = "";
        txtNome.Text = "";
        DropDownListEmpresa.SelectedIndex = 0;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
    }
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        GridView1.Columns[2].Visible = true;

        int count = 0;

        while (count < GridView1.Rows.Count)
        {
            if (((CheckBox)GridView1.Rows[count].Cells[1].FindControl("CheckBox2")).Checked)
            {
                ID = GridView1.Rows[count].Cells[3].Text;

                if (Convert.ToInt32(consult.Consulta("SELECT COUNT (BUId) AS Quantidade FROM Departamentos WHERE BUId = " + ID, "Quantidade")) > 0)
                     ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não é possível excluir registros que possuam dependentes.')", true);
                else
                    consult.atualizaInsereDados("DELETE FROM BUs WHERE BUId= " + ID);
                
            }
            count++;
        }
       
       GridView1.DataBind();

    }
}