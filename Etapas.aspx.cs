using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using System.IO;

public partial class Etapas : System.Web.UI.Page
{

    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();


    protected void Page_Load(object sender, EventArgs e)
    {
        wucMenu1.Visible = false;

        if (!IsPostBack)
        {
            CarregaAtividades();
            CarregaResponsavelTipo();
            CarregaDocumentoTipo();
            CarregaEtapaPrecedente();

            usuario.LogIsert(appSession.FullName, "Etapas", "Acessou tela de etapas.", appSession.IP);

        }

        if (Request.QueryString["id"] != "")
            Label1.Text = "Etapas da Atividade " + consult.Consulta("SELECT Nome FROM Atividades WHERE AtividadeId = " + Request.QueryString["id"], "Nome") + ":";

    }
    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        LimpaCampos();
    }

    private void LimpaCampos()
    {
        DropDownListAtividade.SelectedIndex = 0;
        txtId.Text = "";
        txtNome.Text = "";
        DropDownListResponsavelTipo.SelectedIndex = 0;
        txtOrdem.Text = "";
        txtDocumentoNome.Text = "";
        DropDownListDocumentoTipo.SelectedIndex = 0;
        txtPrazo.Text = "";
    }

    protected void CarregaAtividades()
    {
        var ds = consult.CarregaAtividades();
        if (ds != null)
        {
            DropDownListAtividade.DataSource = ds.Tables["Atividades"];
            DropDownListAtividade.DataTextField = ds.Tables["Atividades"].Columns["Nome"].ToString();
            DropDownListAtividade.DataValueField = ds.Tables["Atividades"].Columns["AtividadeId"].ToString();
            DropDownListAtividade.DataBind();
            DropDownListAtividade.Items.Insert(0, "Selecione...");
            DropDownListAtividade.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar atividades.')", true);
        }
        if (DropDownListAtividade.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar atividades.')", true);
        }
    }

    protected void CarregaResponsavelTipo()
    {
        var ds = consult.CarregaResponsaveisTipo();
        if (ds != null)
        {
            DropDownListResponsavelTipo.DataSource = ds.Tables["EtapasResponsaveisTipos"];
            DropDownListResponsavelTipo.DataTextField = ds.Tables["EtapasResponsaveisTipos"].Columns["Nome"].ToString();
            DropDownListResponsavelTipo.DataValueField = ds.Tables["EtapasResponsaveisTipos"].Columns["TipoId"].ToString();
            DropDownListResponsavelTipo.DataBind();
            DropDownListResponsavelTipo.Items.Insert(0, "Selecione...");
            DropDownListResponsavelTipo.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar tipos de responsáveis.')", true);
        }
        if (DropDownListResponsavelTipo.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar tipos de responsáveis.')", true);
        }
    }


    protected void CarregaDocumentoTipo()
    {
        var ds = consult.CarregaDocumentosTipo();
        if (ds != null)
        {
            DropDownListDocumentoTipo.DataSource = ds.Tables["EtapasDocumentosTipos"];
            DropDownListDocumentoTipo.DataTextField = ds.Tables["EtapasDocumentosTipos"].Columns["Nome"].ToString();
            DropDownListDocumentoTipo.DataValueField = ds.Tables["EtapasDocumentosTipos"].Columns["TipoId"].ToString();
            DropDownListDocumentoTipo.DataBind();
            DropDownListDocumentoTipo.Items.Insert(0, "Selecione...");
            DropDownListDocumentoTipo.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar tipos de documento.')", true);
        }
        if (DropDownListDocumentoTipo.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar tipos de documento.')", true);
        }
    }

    protected void CarregaEtapaPrecedente()
    {
        int idEtapa = txtId.Text == "" ? 0 : Convert.ToInt32(txtId.Text);
        int idAtividade = DropDownListAtividade.SelectedValue == "Selecione..." ? 0 : Convert.ToInt32(DropDownListAtividade.SelectedValue);

        var ds = consult.CarregaEtapasPrecedentes(idAtividade, idEtapa);
        if (ds != null && ds.ExtendedProperties.Count == 0)
        {
            try
            {
                DropDownListEtapaPrecedente.DataSource = ds.Tables["Etapas"];
                DropDownListEtapaPrecedente.DataTextField = ds.Tables["Etapas"].Columns["Nome"].ToString();
                DropDownListEtapaPrecedente.DataValueField = ds.Tables["Etapas"].Columns["EtapaId"].ToString();
                DropDownListEtapaPrecedente.DataBind();
                DropDownListEtapaPrecedente.Items.Insert(0, "Selecione...");
                DropDownListEtapaPrecedente.SelectedIndex = 0;
            }
            catch
            {

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar etapas precedentes.')", true);
        }
        if (DropDownListEtapaPrecedente.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar etapas precedentes.')", true);
        }
    }

    protected void btnAdicionar_Click1(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        LimpaCampos();
        if (Request.QueryString["id"] != null)
            DropDownListAtividade.SelectedValue = Request.QueryString["id"];
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
                ID = GridView1.Rows[count].Cells[2].Text;

                consult.atualizaInsereDados("DELETE FROM Etapas WHERE EtapaId = " + ID);

                usuario.LogIsert(appSession.FullName, "Etapas", "Excluiu Etapa código " + ID + " da atividade " + DropDownListAtividade.SelectedItem + ".", appSession.IP);
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

        if (DropDownListAtividade.SelectedIndex == 0)
        {
            DropDownListAtividade.Focus();
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Selecione a atividade.')", true);
            return;
        }
        if (DropDownListResponsavelTipo.SelectedIndex == 0)
        {
            DropDownListResponsavelTipo.Focus();
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Selecione o tipo de responsável.')", true);
            return;
        }

        if (CheckBoxDocumentoObrigatorio.Checked)
        {
            if (txtDocumentoNome.Text == "" || DropDownListDocumentoTipo.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Informe o nome do documento e o tipo do documento.')", true);
                return;
            }
        }

        if (txtTempo.Text == "")
        {
            txtTempo.Focus();
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Informe o tempo necessário.')", true);
            return;
        }
        else
        {
            Util util = new Util();

            if (util.IsNumeric(txtTempo.Text.Replace(",","").Replace(".","")) == false || txtTempo.Text.Contains("-"))
            {
                txtTempo.Focus();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Informe o tempo necessário em formato numérico maior ou igual zero.')", true);
                return;
            }
            
        }

        var EtapaPrecedenteId = DropDownListEtapaPrecedente.SelectedIndex == 0 ? "null" : DropDownListEtapaPrecedente.SelectedValue;
        var DocumentoTipoId = DropDownListDocumentoTipo.SelectedIndex == 0 ? "null" : DropDownListDocumentoTipo.SelectedValue;
        var DocumentoObrigatorio = CheckBoxDocumentoObrigatorio.Checked == true ? 1 : 0; 

        if (txtId.Text == "") //Insere
        {
            var maiorId = consult.Consulta("SELECT IsNull(MAX(EtapaId),0) as MaiorId FROM Etapas", "MaiorId");
            int novoId = Convert.ToInt32(maiorId) + 1;

            consult.atualizaInsereDados("INSERT INTO Etapas VALUES (" + novoId + ", "
                                                                    + txtOrdem.Text + ", '" 
                                                                    + txtNome.Text.Replace("'", "").Replace("/", "") + "', " 
                                                                    + DropDownListAtividade.SelectedValue + ", " 
                                                                    + DropDownListResponsavelTipo.SelectedValue 
                                                                    + ", null, '" 
                                                                    + txtDocumentoNome.Text + "', " 
                                                                    + DocumentoTipoId + ", " 
                                                                    + txtPrazo.Text + ", " 
                                                                    + EtapaPrecedenteId + ", "
                                                                    + DocumentoObrigatorio + ", " + 
                                                                    "null, " +
                                                                    "null, "
                                                                    + txtTempo.Text + ")");
            txtId.Text = novoId.ToString();

            GridView1.DataBind();
            usuario.LogIsert(appSession.FullName, "Etapas", "Adicionou nova etapa código " + txtId.Text + " à atividade " + DropDownListAtividade.SelectedItem + ".", appSession.IP);
        }
        else
        {
            consult.atualizaInsereDados("UPDATE Etapas SET Ordem = " + txtOrdem.Text + 
                                                        ", Nome = '" + txtNome.Text.Replace("'", "").Replace("/", "") + 
                                                        "', AtividadeId = " + DropDownListAtividade.SelectedValue +
                                                        ", ResponsavelTipoId = " + DropDownListResponsavelTipo.SelectedValue + 
                                                        ", DocumentoNome = '" + txtDocumentoNome.Text +
                                                        "', DocumentoTipoId = " + DocumentoTipoId + 
                                                        ", Prazo = " + txtPrazo.Text + 
                                                        ", EtapaPrecedenteId = " + EtapaPrecedenteId +
                                                        ", DocumentoObrigatorio = " + DocumentoObrigatorio +
                                                        ", Tempo = " + txtTempo.Text.Replace(",",".") + 
                                                        " WHERE EtapaId = " + txtId.Text);

            GridView1.DataBind();

            usuario.LogIsert(appSession.FullName, "Etapas", "Atualizou Etapa código " + txtId.Text + " da atividade " + DropDownListAtividade.SelectedItem + ".", appSession.IP);
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
            TableCell IdRegistro = selectedRow.Cells[2];

            CarregaRegistro(IdRegistro.Text);

            Panel1.Visible = true;

        }
        else
        {
            Panel1.Visible = false;
            LimpaCampos();
        }
        #endregion

        CarregaEtapaPrecedente();
    }

    private void CarregaRegistro(string id)
    {
        string EtapaPrecedenteId = consult.Consulta("SELECT EtapaPrecedenteId FROM Etapas WHERE EtapaId = " + id, "EtapaPrecedenteId");
        string DocumentoTipoId = consult.Consulta("SELECT DocumentoTipoId FROM Etapas WHERE EtapaId = " + id, "DocumentoTipoId");
        string DocumentoModeloCaminho = consult.Consulta("SELECT DocumentoModeloCaminho FROM Etapas WHERE EtapaId = " + id, "DocumentoModeloCaminho");

        DropDownListAtividade.SelectedValue = consult.Consulta("SELECT AtividadeId FROM Etapas WHERE EtapaId = " + id, "AtividadeId");
        txtId.Text = id;
        txtNome.Text = consult.Consulta("SELECT NOME FROM Etapas WHERE EtapaId = " + id, "NOME");
        DropDownListResponsavelTipo.SelectedValue = consult.Consulta("SELECT ResponsavelTipoId FROM Etapas WHERE EtapaId = " + id, "ResponsavelTipoId");
        txtOrdem.Text = consult.Consulta("SELECT Ordem FROM Etapas WHERE EtapaId = " + id, "Ordem");
        txtDocumentoNome.Text = consult.Consulta("SELECT DocumentoNome FROM Etapas WHERE EtapaId = " + id, "DocumentoNome");
        txtPrazo.Text = consult.Consulta("SELECT Prazo FROM Etapas WHERE EtapaId = " + id, "Prazo");
        CheckBoxDocumentoObrigatorio.Checked = Convert.ToBoolean(consult.Consulta("SELECT DocumentoObrigatorio FROM Etapas WHERE EtapaId = " + id, "DocumentoObrigatorio"));
        txtTempo.Text = consult.Consulta("SELECT Tempo FROM Etapas WHERE EtapaId = " + id, "Tempo");


        if (DocumentoTipoId == "")
        {
            DropDownListDocumentoTipo.SelectedIndex = 0;
        }
        else
        {
            DropDownListDocumentoTipo.SelectedValue = DocumentoTipoId;
        }

        if (EtapaPrecedenteId == "")
        {
            DropDownListEtapaPrecedente.SelectedIndex = 0;
        }
        else
        {
            CarregaEtapaPrecedente();
            DropDownListEtapaPrecedente.SelectedValue = EtapaPrecedenteId;
        }

        
    }
    protected void CheckBoxDocumentoObrigatorio_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBoxDocumentoObrigatorio.Checked)
        {
            txtDocumentoNome.Enabled = true;
            DropDownListDocumentoTipo.Enabled = true;
        }
        else
        {
            txtDocumentoNome.Enabled = false;
            txtDocumentoNome.Text = "";
            DropDownListDocumentoTipo.Enabled = false;
            DropDownListDocumentoTipo.SelectedIndex = 0;
        }
    }
    protected void btnDocumentosModelosAbrir_Click(object sender, EventArgs e)
    {
        
        if (txtId.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Primeiramente grave a nova demanda.')", true);
            return;
        }
        
        string op = "";

        //Se o usuário logado for o solicitante ou administrador, a Query String conterá permissão de edição, que desbloqueia o botão excluir e upload.
        if (appSession.UserAdmin == "S")
            op = "ed";
        else
            op = "vw";

        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'EtapasDocumentoModeloUpload.aspx?Id=" + txtId.Text + "&op=" + op + "', null, 'height=450,width=800,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes' );", true);
    }
}