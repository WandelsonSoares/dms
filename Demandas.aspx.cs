using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using Email;

public partial class Demandas : System.Web.UI.Page
{

    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    Permissoes Permissao = new Permissoes();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregaListasFiltro();
            FiltrosGet();
            CarregaDemandas();
            CarregaResponsaveis();

            if (Permissao.PermissaoParaMudarResponsavelDemanda(appSession.UserId, appSession.UserAdmin, ID) == true)
                btnAlterarResponsavel.Enabled = true;

            if (Permissao.PermissaoParaNovaDemanda(appSession.UserId, appSession.UserAdmin) == true)
                btnNovaDemanda.Enabled = true;

            usuario.LogIsert(appSession.FullName, "Demandas", "Visualizou tela Demandas.", appSession.IP);
        }
    }

    private void CarregaListasFiltro()
    {
        CarregaAreasFitlro();
        CarregaResponsaveisFiltro();
        CarregaSolicitantesFiltro();
        CarregaContratosFitlro();
    }

    protected void CarregaAreasFitlro()
    {
        var ds = consult.CarregaAreasFiltro(appSession.UserId);
        if (ds != null)
        {
            DropDownListArea.DataSource = ds.Tables["AreasUsuarios"];
            DropDownListArea.DataTextField = ds.Tables["AreasUsuarios"].Columns["Nome"].ToString();
            DropDownListArea.DataValueField = ds.Tables["AreasUsuarios"].Columns["AreaId"].ToString();
            DropDownListArea.DataBind();
            DropDownListArea.Items.Insert(0, "TODOS");
            DropDownListArea.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar áreas para filtro.')", true);
        }
        if (DropDownListArea.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar áreas para filtro.')", true);
        }
    }

    protected void CarregaContratosFitlro()
    {
        var ds = consult.CarregaContratosFiltro(appSession.UserId, appSession.UserGrupoId);

        try
        {
            if (ds != null)
            {
                DropDownListContrato.DataSource = ds.Tables["Contratos"];
                DropDownListContrato.DataTextField = ds.Tables["Contratos"].Columns["Nome"].ToString();
                DropDownListContrato.DataValueField = ds.Tables["Contratos"].Columns["ContratoId"].ToString();
                DropDownListContrato.DataBind();
                DropDownListContrato.Items.Insert(0, "TODOS");
                DropDownListContrato.SelectedIndex = 0;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar contratos para filtro.')", true);
            }
            if (DropDownListContrato.SelectedValue == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar contratos para filtro.')", true);
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Ocorreu uma falha na listagem de contratos. Verifique suas permissões de acesso junto ao administrador do sistema.')", true);
        }
    }

    protected void CarregaResponsaveisFiltro()
    {
        var ds = consult.CarregaResponsaveisFiltro(appSession.UserId);
        if (ds != null)
        {
            DropDownListResponsavel.DataSource = ds.Tables["Responsaveis"];
            DropDownListResponsavel.DataTextField = ds.Tables["Responsaveis"].Columns["Nome"].ToString();
            DropDownListResponsavel.DataValueField = ds.Tables["Responsaveis"].Columns["ResponsavelId"].ToString();
            DropDownListResponsavel.DataBind();
            DropDownListResponsavel.Items.Insert(0, "TODOS");
            DropDownListResponsavel.SelectedIndex = 0;

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar responsáveis para filtro.')", true);
        }
        if (DropDownListResponsavel.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar responsáveis para filtro.')", true);
        }

    }

    protected void CarregaResponsaveis()
    {
        var ds = consult.CarregaResponsaveisFiltro(appSession.UserId);
        if (ds != null)
        {
            DropDownListResponsavelNovo.DataSource = ds.Tables["Responsaveis"];
            DropDownListResponsavelNovo.DataTextField = ds.Tables["Responsaveis"].Columns["Nome"].ToString();
            DropDownListResponsavelNovo.DataValueField = ds.Tables["Responsaveis"].Columns["ResponsavelId"].ToString();
            DropDownListResponsavelNovo.DataBind();
            DropDownListResponsavelNovo.Items.Insert(0, "Selecione...");
            DropDownListResponsavelNovo.SelectedIndex = 0;

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar responsáveis novo.')", true);
        }
        if (DropDownListResponsavelNovo.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar responsáveis novo.')", true);
        }

    }

    protected void CarregaSolicitantesFiltro()
    {
        var ds = consult.CarregaSolicitantesFiltro(appSession.UserId);
        if (ds != null)
        {
            DropDownListSolicitante.DataSource = ds.Tables["Solicitantes"];
            DropDownListSolicitante.DataTextField = ds.Tables["Solicitantes"].Columns["Nome"].ToString();
            DropDownListSolicitante.DataValueField = ds.Tables["Solicitantes"].Columns["SolicitanteId"].ToString();
            DropDownListSolicitante.DataBind();
            DropDownListSolicitante.Items.Insert(0, "TODOS");
            DropDownListSolicitante.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar solicitantes para filtro.')", true);
        }
        if (DropDownListSolicitante.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar solicitantes para filtro.')", true);
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Seleciona")
        {
            int index = -1;

            index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView1.Rows[index];
            TableCell Id = selectedRow.Cells[3];
            TableCell Contrato = selectedRow.Cells[5];
            TableCell Processo = selectedRow.Cells[7];

            Response.Redirect("DemandasEditar.aspx?id=" + Id.Text);

            usuario.LogIsert(appSession.FullName, "Demandas", "Abriu demanda código " + Id.Text + " relacionada ao processo " + Processo.Text + " do contrato " + Contrato.Text + ".", appSession.IP);
        }
    }

    private void CarregaDemandas()
    {

        string sqlBase;
        string sqlFiltroAreas;
        string sqlFiltroResponsavel;
        string sqlFiltroSolicitante;
        string sqlFiltroStatus;
        string sqlFiltroContratos;
        string sqlOrdenarPor;
        string sqlFiltroTotal;
        string Id_Areas = "";
        string Id_Contratos = "";

        switch (DropDownListOrdenarPor.SelectedValue)
        {
            case "DataAbertura":
                sqlOrdenarPor = " ORDER BY Demandas.DataAbertura, Demandas.PrioridadeId, Demandas.DataPrazo ";
                break;
            case "DataPrazo":
                sqlOrdenarPor = " ORDER BY Demandas.DataPrazo, Demandas.PrioridadeId, Demandas.DataAbertura ";
                break;
            default:
                sqlOrdenarPor = " ORDER BY Demandas.PrioridadeId, Demandas.DataPrazo, Demandas.DataAbertura ";
                break;
        }

        sqlBase = "SELECT     Demandas.DemandaId, Demandas.Detalhe, Usuarios.Nome AS Solicitante, Processos.Nome AS Processo, Usuarios_1.Nome AS Responsavel, " +
                                  "Demandas.DataAbertura, Demandas.DataPrazo, Demandas.Status, Demandas.DataEncerramento, Contratos.DescContract AS Contrato, " +
                                  "Demandas.PrioridadeId " +
                    "FROM         Usuarios AS Usuarios_1 INNER JOIN " +
                                          "Processos INNER JOIN " +
                                          "Demandas ON Processos.ProcessoId = Demandas.ProcessoId INNER JOIN " +
                                          "Usuarios ON Demandas.SolicitanteId = Usuarios.UserID ON Usuarios_1.UserID = Demandas.ResponsavelId INNER JOIN " +
                                          "Contratos ON Demandas.CNId = Contratos.ContratoID ";
        sqlFiltroTotal = "";

        if (DropDownListArea.SelectedIndex == 0)
            Id_Areas = ListaAreasPermitidas(appSession.UserId);
        else
            Id_Areas = DropDownListArea.SelectedValue;


        if (DropDownListContrato.SelectedIndex == 0)
            Id_Contratos = consult.ListaContratosPermitidos(appSession.UserId, appSession.UserGrupoId);
        else
            Id_Contratos = DropDownListContrato.SelectedValue;




        if (appSession.UserGrupoId == "1" || appSession.UserGrupoId == "9") //Se é Solicitante ou Gerente (RCN), vê apenas as demandas que o mesmo solicitou, desde que tenha acesso ao contrato
            sqlFiltroContratos = Id_Contratos != "" ? " AND (Demandas.CNId IN (" + Id_Contratos + "))" : "";
        else
            sqlFiltroContratos = Id_Contratos != "" ? " OR (Demandas.CNId IN (" + Id_Contratos + "))" : "";



        sqlFiltroAreas = Id_Areas != "" ? " AND (Processos.AreaId IN (" + Id_Areas + ")) " : "";

        sqlFiltroResponsavel = DropDownListResponsavel.SelectedIndex == 0 ? "" : " AND (Demandas.ResponsavelId = " + DropDownListResponsavel.SelectedValue + ") ";

        sqlFiltroSolicitante = DropDownListSolicitante.SelectedIndex == 0 ? "" : " AND (Demandas.SolicitanteId = " + DropDownListSolicitante.SelectedValue + ") ";


        if (CheckBoxOcultarConcluidas.Checked == true)
            sqlFiltroStatus = DropDownListStatus.SelectedIndex == 0 ? " AND (Demandas.Status <> 'CONCLUIDA' AND Demandas.Status <> 'CANCELADA') " : " AND (Demandas.Status = '" + DropDownListStatus.SelectedValue + "') ";
        else
            sqlFiltroStatus = DropDownListStatus.SelectedIndex == 0 ? "" : " AND (Demandas.Status = '" + DropDownListStatus.SelectedValue + "') ";


        sqlFiltroTotal = " WHERE (((Demandas.ResponsavelId = " + appSession.UserId + ") OR (Demandas.SolicitanteId = " + appSession.UserId + sqlFiltroContratos + ")) " + sqlFiltroAreas + ")" + sqlFiltroResponsavel + sqlFiltroSolicitante + sqlFiltroStatus;

        var ds = consult.DTSetConsulta(sqlBase + sqlFiltroTotal + sqlOrdenarPor);

        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    private string ListaAreasPermitidas(string UsuarioId)
    {
        string Areas = "";

        //Obtém a lista de IDs das áreas que o usuário faz parte ou é superior
        var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
        var command = new SqlCommand("SELECT AreaId FROM AreasUsuarios WHERE UsuarioId = " + UsuarioId, con);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        da.Fill(dt);


        foreach (DataRow dr in dt.Rows)
        {
            if (Areas != "")
                Areas = Areas + ", " + dr.Field<Int32>("AreaId").ToString();
            else
                Areas = dr.Field<Int32>("AreaId").ToString();
        }
        con.Close();

        return Areas;
    }

    protected void btnAplicarFiltro_Click(object sender, EventArgs e)
    {
        FiltrosSet();

        CarregaDemandas();
    }

    protected void btnRemoverFiltro_Click(object sender, EventArgs e)
    {

        DropDownListArea.SelectedIndex = 0;
        DropDownListResponsavel.SelectedIndex = 0;
        DropDownListSolicitante.SelectedIndex = 0;
        DropDownListStatus.SelectedIndex = 0;
        DropDownListContrato.SelectedIndex = 0;

        CarregaDemandas();

        FiltrosSet();
        
    }

    /// <summary>
    /// Salva na memória a configuração do filtro.
    /// </summary>
    protected void FiltrosSet()
    {
        appSession.FiltroAreaIndexId = DropDownListArea.SelectedIndex.ToString();
        appSession.FiltroResponsavelIndexId = DropDownListResponsavel.SelectedIndex.ToString();
        appSession.FiltroSolicitanteIndexId = DropDownListSolicitante.SelectedIndex.ToString();
        appSession.FiltroStatusIndexId = DropDownListStatus.SelectedIndex.ToString();
        appSession.FiltroContratoIndexId = DropDownListContrato.SelectedIndex.ToString();
        appSession.FiltroOcultarCanceladasConcluidas = CheckBoxOcultarConcluidas.Checked.ToString();
        appSession.FiltroOrdenarPor = DropDownListOrdenarPor.SelectedValue;
    }

    protected void FiltrosGet()
    {
        DropDownListArea.SelectedIndex = String.IsNullOrEmpty(appSession.FiltroAreaIndexId) ? 0 : Convert.ToInt32(appSession.FiltroAreaIndexId);
        DropDownListResponsavel.SelectedIndex = String.IsNullOrEmpty(appSession.FiltroResponsavelIndexId) ? 0 : Convert.ToInt32(appSession.FiltroResponsavelIndexId);
        DropDownListSolicitante.SelectedIndex = String.IsNullOrEmpty(appSession.FiltroSolicitanteIndexId) ? 0 : Convert.ToInt32(appSession.FiltroSolicitanteIndexId);
        DropDownListStatus.SelectedIndex = String.IsNullOrEmpty(appSession.FiltroStatusIndexId) ? 0 : Convert.ToInt32(appSession.FiltroStatusIndexId);
        DropDownListContrato.SelectedIndex = String.IsNullOrEmpty(appSession.FiltroContratoIndexId) ? 0 : Convert.ToInt32(appSession.FiltroContratoIndexId);
        CheckBoxOcultarConcluidas.Checked = String.IsNullOrEmpty(appSession.FiltroOcultarCanceladasConcluidas) ? true : Convert.ToBoolean(appSession.FiltroOcultarCanceladasConcluidas);
        if (!String.IsNullOrEmpty(appSession.FiltroOrdenarPor))
        {
            DropDownListOrdenarPor.SelectedValue = appSession.FiltroOrdenarPor;
        }
        
    }

    protected void AlterarResponsavel_Click(object sender, EventArgs e)
    {
        PanelResponsaveis.Visible = true;
        GridView1.Columns[0].Visible = true;
    }

    protected void btnNovoResponsavelGravar_Click(object sender, EventArgs e)
    {
        if (DropDownListResponsavelNovo.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Selecione um novo responsável.')", true);
            return;
        }


        int linha = 0;
        string DemandasNaoPermitidas = "";

        while (linha < GridView1.Rows.Count)
        {

            if (((CheckBox)GridView1.Rows[linha].Cells[0].FindControl("CheckBox1")).Checked)
            {
                ID = GridView1.Rows[linha].Cells[3].Text;

                //Verifica se o usuário tem permissão para mudar o atendente. Apenas administradores e o responsável pelo setor pode alterar atendente.
                if (Permissao.PermissaoParaMudarResponsavelDemanda(appSession.UserId, appSession.UserAdmin, ID) == true)
                {
                    try
                    {
                        consult.atualizaInsereDados("UPDATE Demandas SET ResponsavelId = " + DropDownListResponsavelNovo.SelectedValue + " WHERE DemandaId = " + ID);
                        consult.atualizaInsereDados("UPDATE DemandasEtapas SET ResponsavelId " + DropDownListResponsavelNovo.SelectedValue + " WHERE ResponsavelTipoId = 1 AND DemandaId = " + ID);
                        EnviarEmailPrepara(ID);
                        usuario.LogIsert(appSession.FullName, "Demandas", "Alterou responsável pela demanda código " + ID + " para o atendente " + DropDownListResponsavelNovo.SelectedItem + ".", appSession.IP);
                        
                        consult.NotificacaoEnvia(DropDownListResponsavelNovo.SelectedValue.ToString(), "Nova Demanda", "Você foi atribuído como novo responsável pela demanda " + ID + ".", "/DemandasEditar.aspx?Id=" + ID);
                    
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha na atribuição do novo responsável.')", true);
                    }
                }
                else
                {
                    if (DemandasNaoPermitidas == "")
                        DemandasNaoPermitidas = ID;
                    else
                        DemandasNaoPermitidas = DemandasNaoPermitidas + ", " + ID;
                }
            }

            linha++;
        }

        GridView1.Columns[0].Visible = false;
        PanelResponsaveis.Visible = false;

        if (DemandasNaoPermitidas != "")
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('As demandas a seguir não puderam ser atualizadas porque você não tem permissão: " + DemandasNaoPermitidas + " .')", true);

        Response.Redirect(Request.RawUrl);
    }

    private void EnviarEmailPrepara(string DemandaId)
    {
        try
        {
            //Refatorar este trecho futuramente para evitar as várias idas ao banco em busca das informações. Ler do próprio formulário.
            //Seleciona lista de planos de ação com ações atrasadas para o contrato atual
            string EmailResponsavelAtendimento = consult.Consulta("SELECT Email FROM Usuarios INNER JOIN Demandas ON Usuarios.UserID = Demandas.ResponsavelId WHERE Demandas.DemandaId = " + DemandaId, "Email");
            string ResponsavelId = consult.Consulta("SELECT ResponsavelId FROM Demandas WHERE DemandaId = " + DemandaId, "ResponsavelId");
            string ResponsavelNome = consult.Consulta("SELECT Nome FROM Usuarios INNER JOIN Demandas ON Usuarios.UserID = Demandas.ResponsavelId WHERE Demandas.DemandaId = " + DemandaId, "Nome");

            string EmailSolicitanteAtendimento = consult.Consulta("SELECT Email FROM Usuarios INNER JOIN Demandas ON Usuarios.UserID = Demandas.SolicitanteId WHERE Demandas.DemandaId = " + DemandaId, "Email");
            string SolicitanteId = consult.Consulta("SELECT SolicitanteId FROM Demandas WHERE DemandaId = " + DemandaId, "SolicitanteId");
            string SolicitanteNome = consult.Consulta("SELECT Nome FROM Usuarios INNER JOIN Demandas ON Usuarios.UserID = Demandas.SolicitanteId WHERE Demandas.DemandaId = " + DemandaId, "Nome");

            DateTime DataAbertura = Convert.ToDateTime(consult.Consulta("SELECT DataAbertura FROM Demandas WHERE DemandaId = " + DemandaId, "DataAbertura"));
            string DataAberturaStr = DataAbertura.ToString("dd/MM/yyyy");

            DateTime DataPrazo = Convert.ToDateTime(consult.Consulta("SELECT DataPrazo FROM Demandas WHERE DemandaId = " + DemandaId, "DataPrazo"));
            string DataPrazoStr = DataPrazo.ToString("dd/MM/yyyy");

            string Atividade = consult.Consulta("SELECT Nome FROM Atividades INNER JOIN Demandas ON Demandas.AtividadeId = Atividades.AtividadeId WHERE DemandaId = " + DemandaId, "Nome");
            string Detalhe = consult.Consulta("SELECT Detalhe FROM Demandas WHERE DemandaId = " + DemandaId, "Detalhe");
            string DemandaStatus = consult.Consulta("SELECT Status FROM Demandas WHERE DemandaId = " + DemandaId, "Status");

            string ConteudoHTML = "<div style=\" padding: 5px\">" +
                                        "<b>DMS - SISTEMA DE GESTÃO DE DEMANDAS</b></br></br>" +
                                        "<b>A demanda abaixo recebeu um novo responsável por atendimento.</b><br/></br>" +
                                        "<b>Código da demanda: </b>" + DemandaId + "</br>" +
                                        "<b>Status da demanda: </b>" + DemandaStatus + "</br>" +
                                        "<b>Atividade:</b> " + Atividade + "</br>" +
                                        "<b>Detalhe:</b> " + Detalhe + "</br>" +
                                        "<b>Data de Abertura: </b>" + DataAbertura + "</br>" +
                                        "<b>Data prazo para encerramento: </b>" + Convert.ToDateTime(DataPrazo).ToString("dd/MM/yyyy") + "</br>" +
                                        "<b>Responsável pelo atendimento: </b>" + ResponsavelId + " - " + ResponsavelNome + "</br>" +
                                        "<b>Solicitante: </b>" + SolicitanteId + " - " + SolicitanteNome + "</br></br>" +
                                        EtapasHTML(DemandaId) + "</br></br>" +
                                        "   *   *   * </br>" +
                                        "Mensagem enviada automaticamente. </br>" +
                                        "<br><a href=\"http://dms-br.comaugroup.com\">Clique aqui para acessar ao sistema.</a><br> </div>" +
                                   "</div>";

            //Pega emails
            ArrayList emailDestinatarios = new ArrayList();

            emailDestinatarios.Add(EmailResponsavelAtendimento);


            EnviarEmail(emailDestinatarios, EmailSolicitanteAtendimento, "DMS - Sistema de Gestão de Demandas - Redirecionamento de demanda", ConteudoHTML);

        }

        catch (Exception ex)
        {
            Response.Write("<script>window.alert('Falha no envio do email de notificação.');</script>");
        }
    }

    private string EtapasHTML(string DemandaId)
    {
        //Connecta do banco para buscar lista de emails dos envolvidos
        var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);

        string strSQL = "SELECT     DemandasEtapas.DemandaEtapaId AS Codigo, DemandasEtapas.Ordem, DemandasEtapas.Nome AS Descricao, Usuarios.Nome AS Responsavel, " +
                                    "DemandasEtapas.Status, DemandasEtapas.DataPrazo, DemandasEtapas.DocumentoNome " +
                                "FROM         DemandasEtapas INNER JOIN Usuarios ON DemandasEtapas.ResponsavelId = Usuarios.UserID " +
                                "WHERE     (DemandasEtapas.DemandaId = " + DemandaId + ") " +
                                "ORDER BY DemandasEtapas.Ordem";

        var command = new SqlCommand(strSQL, con);

        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(command);

        DataTable dt = new DataTable();
        da.Fill(dt);

        string EtapasHTMLConteudoCabecalho = "<div style=\" padding: 5px; border-width: 1px; border-color: gray; border-style: solid\">" +
                                                "<table style=\"width: 100%\">" +
                                                    "<thead  style=\"background-color: lightblue\">" +
                                                         "<tr>" +
                                                            "<th align=\"left\">Código da Etapa</th>" +
                                                            "<th align=\"left\">Ordem</th>" +
                                                            "<th align=\"left\">Descrição</th>" +
                                                            "<th align=\"left\">Responsável</th>" +
                                                            "<th align=\"left\">Status</th>" +
                                                            "<th align=\"left\">Data Prazo</th>" +
                                                            "<th align=\"left\">Documento</th>" +
                                                        "</tr>" +
                                                     "</thead>";
        string EtapasHTMLConteudoLinhas = "";

        foreach (DataRow dr in dt.Rows)
        {
            EtapasHTMLConteudoLinhas = EtapasHTMLConteudoLinhas +
                                        "<tr>" +
                                            "<td align=\"left\">" + dr.Field<Int32>("Codigo").ToString() + "</td>" +
                                            "<td align=\"left\">" + dr.Field<Int32>("Ordem").ToString() + "</td>" +
                                            "<td align=\"left\">" + dr.Field<String>("Descricao").ToString() + "</td>" +
                                            "<td align=\"left\">" + dr.Field<String>("Responsavel").ToString() + "</td>" +
                                            "<td align=\"left\">" + dr.Field<String>("Status").ToString() + "</td>" +
                                            "<td align=\"left\">" + dr.Field<DateTime>("DataPrazo").ToString("dd/MM/yyyy") + "</td>" +
                                            "<td align=\"left\">" + dr.Field<String>("DocumentoNome").ToString() + "</td>" +
                                         "</tr>";
        }

        string EtapasHTMLConteudoTotal = EtapasHTMLConteudoCabecalho + "<tbody>" + EtapasHTMLConteudoLinhas + "</tbody>" + "</table></div>";

        return EtapasHTMLConteudoTotal;
    }

    protected void EnviarEmail(ArrayList emailDestinatario, string emailUsuarioCriador, string assunto, string mensagem)
    {
        csEmail novoEmail = new csEmail();
        novoEmail.Enviar2(emailDestinatario, emailUsuarioCriador, assunto, mensagem);
    }

    protected void btnCancelarStatus_Click(object sender, EventArgs e)
    {
        GridView1.Columns[0].Visible = false;
        PanelResponsaveis.Visible = false;
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

    protected void CheckBoxOcultarConcluidas_CheckedChanged(object sender, EventArgs e)
    {
        CarregaDemandas();
    }
    protected void DropDownListOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
    {
        appSession.FiltroOrdenarPor = DropDownListOrdenarPor.SelectedValue;
        CarregaDemandas();
    }
}