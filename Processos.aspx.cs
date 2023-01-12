using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class Processos : System.Web.UI.Page
{

    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();


    protected void Page_Load(object sender, EventArgs e)
    {
        wucMenu1.Visible = false;

        if (!IsPostBack)
        {
            CarregaAreas();
            CarregaResponsaveis();

            usuario.LogIsert(appSession.FullName, "Processos", "Acessou tela de processos.", appSession.IP);
        }
        if (Request.QueryString["id"] != "")
            Label1.Text = "Processos da Área " + consult.Consulta("SELECT Nome FROM Areas WHERE AreaId = " + Request.QueryString["id"], "Nome") + ":";
    }
    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        LimpaCampos();
        if (Request.QueryString["id"] != null)
            DropDownListArea.SelectedValue = Request.QueryString["id"];
    }

    private void CarregaResponsaveis()
    {
        var ds = consult.CarregaResponsaveis();
        if (ds != null)
        {
            DropDownListResponsavel.DataSource = ds.Tables["Usuarios"];
            DropDownListResponsavel.DataTextField = ds.Tables["Usuarios"].Columns["Nome"].ToString();
            DropDownListResponsavel.DataValueField = ds.Tables["Usuarios"].Columns["UserId"].ToString();
            DropDownListResponsavel.DataBind();
            DropDownListResponsavel.Items.Insert(0, "Selecione...");
            DropDownListResponsavel.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar empresas.')", true);
        }
        if (DropDownListResponsavel.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar empresas.')", true);
        }
    }

    protected void CarregaAreas()
    {
        var ds = consult.CarregaAreas();
        if (ds != null)
        {
            DropDownListArea.DataSource = ds.Tables["Areas"];
            DropDownListArea.DataTextField = ds.Tables["Areas"].Columns["Nome"].ToString();
            DropDownListArea.DataValueField = ds.Tables["Areas"].Columns["AreaId"].ToString();
            DropDownListArea.DataBind();
            DropDownListArea.Items.Insert(0, "Selecione...");
            DropDownListArea.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar áreas.')", true);
        }
        if (DropDownListArea.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar áreas.')", true);
        }
    }

    private void LimpaCampos()
    {
        txtId.Text = "";
        txtNome.Text = "";
        DropDownListArea.SelectedIndex = 0;
        DropDownListResponsavel.SelectedIndex = 0;
    }
    
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        if (DropDownListArea.SelectedIndex == 0)
        {
            return;
        }
        if (DropDownListResponsavel.SelectedIndex == 0)
        {
            return;
        }

        if (txtId.Text == "") //Insere
        {
            var maiorId = consult.Consulta("SELECT IsNull(MAX(ProcessoId),0) as MaiorId FROM Processos", "MaiorId");

            int novoId = Convert.ToInt32(maiorId) + 1;

            consult.atualizaInsereDados("INSERT INTO Processos VALUES (" + novoId + ", '" + txtNome.Text.Replace("'", "").Replace("/", "") + "', " + DropDownListArea.SelectedValue + ", NULL, NULL, NULL, NULL, " + DropDownListResponsavel.SelectedValue + ")");
            txtId.Text = novoId.ToString();
            GridView1.DataBind();
        }
        else
        {
            consult.atualizaInsereDados("UPDATE Processos SET Nome = '" + txtNome.Text.Replace("'", "").Replace("/", "") + "', AreaId = " + DropDownListArea.SelectedValue + ", ResponsavelId = " + DropDownListResponsavel.SelectedValue + " WHERE ProcessoId = " + txtId.Text);
            GridView1.DataBind();
        }

    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
        Panel1.Visible = false;
    }
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        GridView1.Columns[2].Visible = true;

        int count = 0;

        //GridView1.DataBind();
        while (count < GridView1.Rows.Count)
        {
            if (((CheckBox)GridView1.Rows[count].Cells[1].FindControl("CheckBox2")).Checked)
            {
                ID = GridView1.Rows[count].Cells[3].Text;

                if (Convert.ToInt32(consult.Consulta("SELECT COUNT (ProcessoId) AS Quantidade FROM Subprocessos WHERE ProcessoId = " + ID, "Quantidade")) > 0)
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não é possível excluir registros que possuam dependentes.')", true);
                else
                    consult.atualizaInsereDados("DELETE FROM Processos WHERE ProcessoId = " + ID);

            }
            count++;
        }

        GridView1.DataBind();
        LimpaCampos();
        Panel1.Visible = false;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = -1;

        #region Seleciona
        if (e.CommandName == "Seleciona")
        {
            index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView1.Rows[index];
            TableCell IdRegistro = selectedRow.Cells[3];

            CarregaRegistro(IdRegistro.Text);

            Panel1.Visible = true;

        }
        else
        {
            Panel1.Visible = false;
            LimpaCampos();
        }
        #endregion
    }

    private void CarregaRegistro(string id)
    {
        txtId.Text = id;
        txtNome.Text = consult.Consulta("SELECT NOME FROM Processos WHERE ProcessoId = " + id, "NOME");
        DropDownListArea.SelectedValue = consult.Consulta("SELECT AreaId FROM Processos WHERE ProcessoId = " + id, "AreaId");
        DropDownListResponsavel.SelectedValue = consult.Consulta("SELECT ResponsavelId FROM Processos WHERE ProcessoId = " + id, "ResponsavelId");
    }
}