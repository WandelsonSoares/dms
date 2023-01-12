using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class Departamentos : System.Web.UI.Page
{
    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        wucMenu1.Visible = false;

        if (!IsPostBack)
        {
            CarregaBUs();
            carregaResponsaveis();
            LimpaCampos();

            usuario.LogIsert(appSession.FullName, "Departamentos", "Acessou tela de departamentos.", appSession.IP);
        }

        if (Request.QueryString["id"] != "")
            Label1.Text = "Departamentos da BU " + consult.Consulta("SELECT Nome FROM BUs WHERE BuId = " + Request.QueryString["id"], "Nome") + ":";

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
    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
        Panel1.Visible = true;
        if (Request.QueryString["id"] != null)
            DropDownListBU.SelectedValue = Request.QueryString["id"];
    }

    private void LimpaCampos()
    {
        txtId.Text = "";
        txtNome.Text = "";
        DropDownListBU.SelectedIndex = 0;
        DropDownListResponsavel.SelectedIndex = 0;
    }

    private void CarregaBUs()
    {

        var ds = consult.CarregaBUs();
        if (ds != null)
        {
            DropDownListBU.DataSource = ds.Tables["BUs"];
            DropDownListBU.DataTextField = ds.Tables["BUs"].Columns["Nome"].ToString();
            DropDownListBU.DataValueField = ds.Tables["BUs"].Columns["BUId"].ToString();
            DropDownListBU.DataBind();
            DropDownListBU.Items.Insert(0, "Selecione...");
            DropDownListBU.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar empresas.')", true);
        }
        if (DropDownListBU.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar empresas.')", true);
        }
    }


    private void carregaResponsaveis()
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

    private void CarregaRegistro(string id)
    {
        txtId.Text = id;
        txtNome.Text = consult.Consulta("SELECT NOME FROM Departamentos WHERE DepartamentoId = " + id, "NOME");
        DropDownListBU.SelectedValue = consult.Consulta("SELECT BUId FROM Departamentos WHERE DepartamentoId = " + id, "BUId");
        DropDownListResponsavel.SelectedValue = consult.Consulta("SELECT ResponsavelId FROM Departamentos WHERE DepartamentoId = " + id, "ResponsavelId");
    }

    protected void btnGravar_Click(object sender, EventArgs e)
    {
        if (DropDownListBU.SelectedIndex == 0)
        {
            return;
        }
        if (DropDownListResponsavel.SelectedIndex == 0)
        {
            return;
        }

        if (txtId.Text == "") //Insere
        {
            var maiorId = consult.Consulta("SELECT IsNull(MAX(DepartamentoId),0) as MaiorId FROM Departamentos", "MaiorId");
            
            int novoId = Convert.ToInt32(maiorId) + 1;

            consult.atualizaInsereDados("INSERT INTO Departamentos VALUES (" + novoId + ", '" + txtNome.Text.Replace("'", "").Replace("/", "") + "', 1, " + DropDownListBU.SelectedValue + ", " + DropDownListResponsavel.SelectedValue + ")");
            txtId.Text = novoId.ToString();
            GridView1.DataBind();
        }
        else
        {
            consult.atualizaInsereDados("UPDATE Departamentos SET Nome = '" + txtNome.Text.Replace("'", "").Replace("/", "") + "', BUId = " + DropDownListBU.SelectedValue + ", ResponsavelId = " + DropDownListResponsavel.SelectedValue + " WHERE DepartamentoId = " + txtId.Text);
            GridView1.DataBind();
        }

    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
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

                if (Convert.ToInt32(consult.Consulta("SELECT COUNT (DepartamentoId) AS Quantidade FROM Setores WHERE DepartamentoId = " + ID, "Quantidade")) > 0)
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não é possível excluir registros que possuam dependentes.')", true);
                else
                consult.atualizaInsereDados("DELETE FROM Departamentos WHERE DepartamentoId = " + ID);

            }
            count++;
        }

        GridView1.DataBind();
    }
}