using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class Subprocessos : System.Web.UI.Page
{

    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        wucMenu1.Visible = false;

        if (!IsPostBack)
        {
            CarregaProcessos();
            CarregaResponsaveis();

            usuario.LogIsert(appSession.FullName, "Subprocessos", "Acessou tela de subprocessos.", appSession.IP);
        }
        if (Request.QueryString["id"] != "")
            Label1.Text = "Subprocessos do Processo " + consult.Consulta("SELECT Nome FROM Processos WHERE ProcessoId = " + Request.QueryString["id"], "Nome") + ":";
    }


    protected void CarregaProcessos()
    {
        var ds = consult.CarregaProcessos();
        if (ds != null)
        {
            DropDownListProcesso.DataSource = ds.Tables["Processos"];
            DropDownListProcesso.DataTextField = ds.Tables["Processos"].Columns["Nome"].ToString();
            DropDownListProcesso.DataValueField = ds.Tables["Processos"].Columns["ProcessoId"].ToString();
            DropDownListProcesso.DataBind();
            DropDownListProcesso.Items.Insert(0, "Selecione...");
            DropDownListProcesso.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar processos.')", true);
        }
        if (DropDownListProcesso.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar processos.')", true);
        }
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


    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        LimpaCampos();
        if (Request.QueryString["id"] != null)
            DropDownListProcesso.SelectedValue = Request.QueryString["id"];
    }

    private void LimpaCampos()
    {
        txtId.Text = "";
        txtNome.Text = "";
        DropDownListProcesso.SelectedIndex = 0;
        DropDownListResponsavel.SelectedIndex = 0;
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

                if (Convert.ToInt32(consult.Consulta("SELECT COUNT (SubprocessoId) AS Quantidade FROM Atividades WHERE SubprocessoId = " + ID, "Quantidade")) > 0)
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não é possível excluir registros que possuam dependentes.')", true);
                else
                    consult.atualizaInsereDados("DELETE FROM Subprocessos WHERE SubprocessoId = " + ID);

            }
            count++;
        }

        GridView1.DataBind();
        LimpaCampos();
        Panel1.Visible = false;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
        Panel1.Visible = false;
    }
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        if (DropDownListProcesso.SelectedIndex == 0)
        {
            return;
        }
        if (DropDownListResponsavel.SelectedIndex == 0)
        {
            return;
        }

        if (txtId.Text == "") //Insere
        {
            var maiorId = consult.Consulta("SELECT IsNull(MAX(SubprocessoId),0) as MaiorId FROM Subprocessos", "MaiorId");

            int novoId = Convert.ToInt32(maiorId) + 1;

            consult.atualizaInsereDados("INSERT INTO Subprocessos VALUES (" + novoId + ", '" + txtNome.Text.Replace("'", "").Replace("/", "") + "', " + DropDownListProcesso.SelectedValue + ", " + DropDownListResponsavel.SelectedValue + ")");
            txtId.Text = novoId.ToString();
            GridView1.DataBind();
        }
        else
        {
            consult.atualizaInsereDados("UPDATE Subprocessos SET Nome = '" + txtNome.Text.Replace("'", "").Replace("/", "") + "', ProcessoId = " + DropDownListProcesso.SelectedValue + ", ResponsavelId = " + DropDownListResponsavel.SelectedValue + " WHERE SubprocessoId = " + txtId.Text);
            GridView1.DataBind();
        }
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
        txtNome.Text = consult.Consulta("SELECT NOME FROM Subprocessos WHERE SubprocessoId = " + id, "NOME");
        DropDownListProcesso.SelectedValue = consult.Consulta("SELECT ProcessoId FROM Subprocessos WHERE SubprocessoId = " + id, "ProcessoId");
        DropDownListResponsavel.SelectedValue = consult.Consulta("SELECT ResponsavelId FROM Subprocessos WHERE ProcessoId = " + id, "ResponsavelId");
    }
}