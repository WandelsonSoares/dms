using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class Areas : System.Web.UI.Page
{
    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        wucMenu1.Visible = false;

        if (!IsPostBack)
        {
            CarregaSetores();
            CarregaResponsaveis();

            usuario.LogIsert(appSession.FullName, "Áreas", "Acessou tela de áreas.", appSession.IP);
        }
        if (Request.QueryString["id"] != "")
            Label1.Text = "Áreas do Setor " + consult.Consulta("SELECT Nome FROM Setores WHERE SetorId = " + Request.QueryString["id"], "Nome") + ":";
    }
    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        LimpaCampos();
    }

    protected void btnGravar_Click(object sender, EventArgs e)
    {
        if (DropDownListSetor.SelectedIndex == 0)
        {
            return;
        }
        if (DropDownListResponsavel.SelectedIndex == 0)
        {
            return;
        }

        if (txtId.Text == "") //Insere
        {
            var maiorId = consult.Consulta("SELECT IsNull(MAX(AreaId),0) as MaiorId FROM Areas", "MaiorId");

            int novoId = Convert.ToInt32(maiorId) + 1;

            consult.atualizaInsereDados("INSERT INTO Areas VALUES (" + novoId + ", '" + txtNome.Text.Replace("'", "").Replace("/", "") + "', " + DropDownListSetor.SelectedValue + ", " + DropDownListResponsavel.SelectedValue + ")");
            txtId.Text = novoId.ToString();
            GridView1.DataBind();
        }
        else
        {
            consult.atualizaInsereDados("UPDATE Areas SET Nome = '" + txtNome.Text.Replace("'", "").Replace("/", "") + "', SetorId = " + DropDownListSetor.SelectedValue + ", ResponsavelId = " + DropDownListResponsavel.SelectedValue + " WHERE AreaId = " + txtId.Text);
            GridView1.DataBind();
        }
    }

    protected void CarregaSetores()
    {
        var ds = consult.CarregaSetores();
        if (ds != null)
        {
            DropDownListSetor.DataSource = ds.Tables["Setores"];
            DropDownListSetor.DataTextField = ds.Tables["Setores"].Columns["Nome"].ToString();
            DropDownListSetor.DataValueField = ds.Tables["Setores"].Columns["SetorId"].ToString();
            DropDownListSetor.DataBind();
            DropDownListSetor.Items.Insert(0, "Selecione...");
            DropDownListSetor.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar setores.')", true);
        }
        if (DropDownListSetor.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar setores.')", true);
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

    private void LimpaCampos()
    {
        txtId.Text = "";
        txtNome.Text = "";
        DropDownListSetor.SelectedIndex = 0;
        DropDownListResponsavel.SelectedIndex = 0;
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
            if (Request.QueryString["id"] != null)
                DropDownListSetor.SelectedValue = Request.QueryString["id"];
        }
        #endregion
    }

    private void CarregaRegistro(string id)
    {
        txtId.Text = id;
        txtNome.Text = consult.Consulta("SELECT NOME FROM Areas WHERE AreaId = " + id, "NOME");
        DropDownListSetor.SelectedValue = consult.Consulta("SELECT SetorId FROM Areas WHERE AreaId = " + id, "SetorId");
        DropDownListResponsavel.SelectedValue = consult.Consulta("SELECT ResponsavelId FROM Areas WHERE AreaId = " + id, "ResponsavelId");
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

                if (Convert.ToInt32(consult.Consulta("SELECT COUNT (AreaId) AS Quantidade FROM Processos WHERE AreaId = " + ID, "Quantidade")) > 0)
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não é possível excluir registros que possuam dependentes.')", true);
                else
                    consult.atualizaInsereDados("DELETE FROM Areas WHERE AreaId = " + ID);

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
}