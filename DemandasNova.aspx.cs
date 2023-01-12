using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using System.Data.SqlClient;
using System.Data;
using Email;
using System.Collections;


public partial class DemandasNova : System.Web.UI.Page
{
    Persistencia_Fast consult = new Persistencia_Fast();
    private readonly cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            //CarregaBUs();
            carregaDepartamentos();
            carregaContrato();
            txtUsuarioId.Text = appSession.UserId;
            txtNome.Text = consult.Consulta("SELECT Nome FROM Usuarios WHERE UserId = " + appSession.UserId, "Nome");
            usuario.LogIsert(appSession.FullName, "Nova Demanda", "Acessou tela de cadastro de nova demanda.", appSession.IP);
        }
    }

    protected void btnGravar_Click(object sender, EventArgs e)
    {

        //if (DropDownListCN.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Selecione um Centro de Negócios.')", true);
        //    return;
        //}
        
        if (RadioButtonListIndividualColetivo.SelectedValue == "1") //Individual
            if (txtMatriculaFuncionario.Text == "" || lblFuncionarioNome.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Informe a matrícula para atendimento a único funcionário.')", true);
                return;
            }

        if (txtUsuarioId.Text == "" || DropDownListBU.SelectedIndex == 0 || DropDownListDepartamento.SelectedIndex == 0
                || DropDownListSetor.SelectedIndex == 0 || DropDownListArea.SelectedIndex == 0 || DropDownListProcesso.SelectedIndex == 0
                || DropDownListSubprocesso.SelectedIndex == 0 || DropDownListAtividade.SelectedIndex == 0 || txtDetalhe.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Preencha os campos obrigatórios.')", true);
            return;
        }

        try
        {
            string ResponsavelAtendimentoId = consult.Consulta("SELECT ResponsavelId FROM Atividades WHERE AtividadeId = " + DropDownListAtividade.SelectedValue, "ResponsavelId");
            string MatriculaFuncionario = (txtMatriculaFuncionario.Text == "" || txtMatriculaFuncionario.Text == "") ? "null" : txtMatriculaFuncionario.Text;

            consult.atualizaInsereDados("INSERT INTO Demandas VALUES (" + txtUsuarioId.Text + ", " +
                                                                "null, '" +
                                                                 DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', " +
                                                                "null, " +
                                                                "null, " +
                                                                DropDownListAtividade.SelectedValue + ", " +
                                                                DropDownListSubprocesso.SelectedValue + ", " +
                                                                DropDownListProcesso.SelectedValue + ", " +
                                                                ResponsavelAtendimentoId + ", '" +
                                                                txtDetalhe.Text.Replace("'","") + "', " +
                                                                "'AGUARDANDO', " +
                                                                "null, " + //Cumprimento Prazo
                                                                "null, " + //Dias atraso final
                                                                "null, " + //Justificativa cancelamento
                                                                "null " + //Usuario cancelamento
                                                                ", " + DropDownListCN.SelectedValue + //CN
                                                                ", " + MatriculaFuncionario +
                                                                ", " + RadioButtonListIndividualColetivo.SelectedValue + 
                                                                ", 1 " +
                                                                ", null " +
                                                                ", 1)");


            string maiorDemandaId = consult.Consulta("SELECT IsNull(MAX(DemandaId),0) as DemandaId FROM Demandas WHERE SolicitanteId = " + appSession.UserId, "DemandaId");

        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha no registro da demanda.')", true);
            usuario.LogIsert(appSession.FullName, "Nova Demanda", "Recebeu mensagem de falha no registro da demanda.", appSession.IP);
            return;
        }

        string DemandaId = consult.Consulta("SELECT IsNull(MAX(DemandaId),0) AS DemandaId FROM Demandas WHERE SolicitanteId = " + appSession.UserId, "DemandaId");
        usuario.LogIsert(appSession.FullName, "Nova Demanda", "Registrou nova demanda código " + DemandaId + " para o contrato " + DropDownListCN.SelectedItem + ".", appSession.IP);

        if (InsereEtapas(DemandaId, DropDownListAtividade.SelectedValue.ToString()) == true)
        {

            //Enviar email de notificação
            if (DemandaId != "0")
            {
                string DataPrazo = consult.Consulta("SELECT IsNull(MAX(DataPrazo),0) as DataPrazo FROM DemandasEtapas WHERE DemandaId = " + DemandaId, "DataPrazo");

                consult.atualizaInsereDados("UPDATE Demandas SET DataPrazo = '" + Convert.ToDateTime(DataPrazo).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' WHERE DemandaId = " + DemandaId);

                if (consult.Consulta("SELECT EnviarEmailDemandasCriacao FROM ConfiguracoesEmails", "EnviarEmailDemandasCriacao") == "True")
                    EnviarEmailPrepara(DemandaId);

                string ResponsavelId = consult.Consulta("SELECT ResponsavelId FROM Demandas WHERE DemandaId = " + DemandaId, "ResponsavelId");
                string Contrato = consult.Consulta("SELECT DescContract FROM Contratos INNER JOIN Demandas ON Demandas.CNId = Contratos.ContratoId WHERE Demandas.DemandaId = " + DemandaId, "DescContract");
                string Atividade = consult.Consulta("SELECT Nome FROM Atividades INNER JOIN Demandas ON Demandas.AtividadeId = Atividades.AtividadeId WHERE Demandas.DemandaId = " + DemandaId, "Nome");

                consult.NotificacaoEnvia(ResponsavelId, "Nova Demanda: " + Atividade, "Você recebeu uma nova demanda para o contrato " + Contrato + ".", "/DemandasEditar.aspx?Id=" + DemandaId);
            
            }

            //Se nenhuma etapa foi registrada, acusa falha
            if (consult.Consulta("SELECT COUNT (DemandaEtapaId) AS Quantidade FROM DemandasEtapas WHERE DemandaId = " + DemandaId, "Quantidade") == "0")
            {
                usuario.LogIsert(appSession.FullName, "Nova Demanda", "Recebeu mensagem de falha na criação das etapas da demanda " + DemandaId + " para o contrato " + DropDownListCN.SelectedItem + ".", appSession.IP);
                consult.atualizaInsereDados("DELETE FROM Demandas WHERE DemandaId = " + DemandaId + " AND SolicitanteId = " + appSession.UserId);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha no registro da demanda. Tente novamente.')", true);
            }
            //Se não houve nenhum problema e todas as etapas foram criadas, direciona usuário para a página de edição.
            else
            {
                Response.Redirect("DemandasEditar.aspx?id=" + DemandaId);
            }
        }
        else
        {
            usuario.LogIsert(appSession.FullName, "Nova Demanda", "Recebeu mensagem de falha na criação das etapas da demanda " + DemandaId + " para o contrato " + DropDownListCN.SelectedItem + ".", appSession.IP);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha no registro de etapas para a demanda.')", true);

        }

    }

    private bool InsereEtapas(string DemandaId, string AtividadeId)
    {
        try
        {
            //Abre a tabela para pegar as etapas de uma atividade
            var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);

            string strSQL = "SELECT EtapaId, Ordem, Nome, AtividadeId, ResponsavelTipoId, " +
                              " ResponsavelId, DocumentoNome, DocumentoTipoId, Prazo, ISNULL(EtapaPrecedenteId,0) AS EtapaPrecedenteId, " +
                              " DocumentoObrigatorio, DocumentoModeloCaminho, DocumentoModeloValidade, Tempo " +
                              " FROM Etapas WHERE AtividadeId = " + AtividadeId + " ORDER BY Ordem";

            var command = new SqlCommand(strSQL, con);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            da.Fill(dt);

            //Percorre o array preenchido com as etapas para vincular à nova demanda criada
            foreach (DataRow dr in dt.Rows)
            {
                string EtapaId = dr.Field<Int32>("EtapaId").ToString();
                string Ordem = dr.Field<Int32>("Ordem").ToString();
                string Nome = dr.Field<String>("Nome");
                string ResponsavelTipoId = dr.Field<Int32>("ResponsavelTipoId").ToString();
                string DocumentoModeloCaminho = dr.Field<String>("DocumentoModeloCaminho");
                int EtapaPrecedenteId = dr.Field<Int32>("EtapaPrecedenteId");
                Double Tempo = dr.Field<Double>("Tempo");

                if (DocumentoModeloCaminho == "" || DocumentoModeloCaminho == null)
                    DocumentoModeloCaminho = "null";
                else
                    DocumentoModeloCaminho = "'" + DocumentoModeloCaminho + "'";

                string ResponsavelId = "";

                switch (ResponsavelTipoId)
                {
                    case "1": //Atendente é o responsável
                        ResponsavelId = consult.Consulta("SELECT ResponsavelId FROM Atividades WHERE AtividadeId = " + AtividadeId, "ResponsavelId");
                        break;
                    case "2": //Solicitante é o responsável
                        ResponsavelId = txtUsuarioId.Text;
                        break;
                    case "3": //Aprovador é o responsável
                        ResponsavelId = consult.Consulta("SELECT ResponsavelId FROM Atividades WHERE AtividadeId = " + AtividadeId, "ResponsavelId"); //Temporariamente consulta o próprio responsável pela atividade. Posteriormente criar tabela de aprovadores para cadastrar os HR Business Partners
                        break;
                    default:
                        break;
                }

                string DocumentoObrigatorio = dr.Field<bool>("DocumentoObrigatorio").ToString();
                string DocumentoNome = "";
                string DocumentoTipoId = "NULL";

                DocumentoObrigatorio = DocumentoObrigatorio == "True" ? "1" : "0";

                if (DocumentoObrigatorio == "1")
                {
                    DocumentoNome = dr.Field<String>("DocumentoNome"); ;
                    DocumentoTipoId = dr.Field<Int32>("DocumentoTipoId").ToString();
                }

                string Prazo = dr.Field<Int32>("Prazo").ToString();

                DateTime DataPrazo = DateTime.Today.AddDays(Convert.ToDouble(Prazo));

                string EtapaPrecedenteDataPrazo = "";

                //Verifica se há dependência de outra etapa.
                //Se sim, pega a data prazo da etapa anterior já criada.
                if (EtapaPrecedenteId.ToString() != "" && EtapaPrecedenteId.ToString() != "0")
                {
                    EtapaPrecedenteDataPrazo = consult.Consulta("SELECT DataPrazo FROM DemandasEtapas WHERE EtapaId = " + EtapaPrecedenteId + " AND DemandaId = " + DemandaId, "DataPrazo");

                    if (EtapaPrecedenteDataPrazo != null)
                    {
                        //Data prazo da nova etapa é a data prazo da etapa precedente mais o número de dias necessários para cumprimento da etapa dependente;
                        DataPrazo = Convert.ToDateTime(EtapaPrecedenteDataPrazo).AddDays(Convert.ToDouble(Prazo));

                        //Verifica se há sábados, domingos ou feriados no período e acrescenta ao prazo
                        DataPrazo = DataPrazo.AddDays(SabadoDomingoFeriado(Convert.ToDateTime(EtapaPrecedenteDataPrazo), DataPrazo));
                    }
                }
                //Se não há dependência
                else
                {
                    DataPrazo = DataPrazo.AddDays(SabadoDomingoFeriado(DateTime.Today, DataPrazo)); //Adiciona 
                }
                
                string DataAtualizacao = "null";
                string ComentariosExistencia = "n";

                consult.atualizaInsereDados("INSERT INTO DemandasEtapas VALUES (" +
                                                        EtapaId + ", " +
                                                        Ordem + ", '" +
                                                        Nome + "', " +
                                                        ResponsavelTipoId + ", " +
                                                        ResponsavelId + ", '" +
                                                        DocumentoNome + "', " +
                                                        DocumentoTipoId + ", " +
                                                        Prazo + ", '" +
                                                        DataPrazo.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', " +
                                                        EtapaPrecedenteId.ToString() + ", " +
                                                        DocumentoObrigatorio + ", " +
                                                        DataAtualizacao + ", '" +
                                                        ComentariosExistencia + "', " +
                                                        "'AGUARDANDO'," +
                                                        " null, " +
                                                        DemandaId + ", " +
                                                        "null, " +
                                                        DocumentoModeloCaminho + 
                                                        ", null, " +
                                                        Tempo.ToString().Replace(",",".") + ")");

            }

            con.Close();

            return (true);

        }
        catch
        {
            usuario.LogIsert(appSession.FullName, "Nova Demanda", "Sistema apresentou falha na geração das etapas da nova demanda código " + DemandaId + ".", appSession.IP);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha na criação das etapas.')", true);
            return (false);
        }


    }

    protected void DropDownListDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregaSetores();

        if (DropDownListDepartamento.SelectedValue=="6")
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "showDetalheMensagem()", true);
        else
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "hideDetalheMensagem()", true);
    }

    private void CarregaBUs()
    {

        var ds = consult.CarregaBUs(1, 1); //EmpresaId fixo em 1 provisoriamente em virtude de haver apenas uma úncia empresa cadastrada.
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

        controlaDropdownLists();
    }

    private void carregaDepartamentos()
    {
        try
        {
            var ds = consult.CarregaDepartamentos();
            if (ds != null)
            {
                DropDownListDepartamento.DataSource = ds.Tables["Departamentos"];
                DropDownListDepartamento.DataTextField = ds.Tables["Departamentos"].Columns["Nome"].ToString();
                DropDownListDepartamento.DataValueField = ds.Tables["Departamentos"].Columns["DepartamentoId"].ToString();
                DropDownListDepartamento.DataBind();
                DropDownListDepartamento.Items.Insert(0, "Selecione...");
                DropDownListDepartamento.SelectedIndex = 0;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar departamentos.')", true);
            }
            if (DropDownListDepartamento.SelectedValue == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar departamentos.')", true);
            }
        }
        catch
        {
            DropDownListDepartamento.Items.Insert(0, "Selecione...");
            DropDownListDepartamento.SelectedIndex = 0;
        }

        controlaDropdownLists();
    }

    protected void CarregaSetores()
    {
        var ds = consult.CarregaSetores(Convert.ToInt32(DropDownListDepartamento.SelectedValue));
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

        controlaDropdownLists();
    }

    protected void CarregaAreas()
    {
        var ds = consult.CarregaAreas(Convert.ToInt32(DropDownListSetor.SelectedValue));
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

        controlaDropdownLists();
    }

    protected void CarregaProcessos()
    {
        var ds = consult.CarregaProcessos(Convert.ToInt32(DropDownListArea.SelectedValue));
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

        controlaDropdownLists();
    }

    protected void CarregaSubprocessos()
    {
        var ds = consult.CarregaSubProcessos(Convert.ToInt32(DropDownListProcesso.SelectedValue));
        if (ds != null)
        {
            DropDownListSubprocesso.DataSource = ds.Tables["Subprocessos"];
            DropDownListSubprocesso.DataTextField = ds.Tables["Subprocessos"].Columns["Nome"].ToString();
            DropDownListSubprocesso.DataValueField = ds.Tables["Subprocessos"].Columns["SubprocessoId"].ToString();
            DropDownListSubprocesso.DataBind();
            DropDownListSubprocesso.Items.Insert(0, "Selecione...");
            DropDownListSubprocesso.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar subprocessos.')", true);
        }
        if (DropDownListSubprocesso.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar subprocessos.')", true);
        }

        controlaDropdownLists();
    }

    protected void CarregaAtividades()
    {
        var ds = consult.CarregaAtividades(Convert.ToInt32(DropDownListSubprocesso.SelectedValue));
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

        controlaDropdownLists();
    }

    private void controlaDropdownLists()
    {
        //DropDownListDepartamento.Enabled = DropDownListBU.SelectedIndex > 0 ? true : false;
        DropDownListSetor.Enabled = DropDownListDepartamento.SelectedIndex > 0 ? true : false;
        DropDownListArea.Enabled = DropDownListSetor.SelectedIndex > 0 ? true : false;
        DropDownListProcesso.Enabled = DropDownListArea.SelectedIndex > 0 ? true : false;
        DropDownListSubprocesso.Enabled = DropDownListProcesso.SelectedIndex > 0 ? true : false;
        DropDownListAtividade.Enabled = DropDownListSubprocesso.SelectedIndex > 0 ? true : false;
    }

    protected void DropDownListBU_SelectedIndexChanged(object sender, EventArgs e)
    {
        carregaDepartamentos();
    }
    protected void DropDownListSetor_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregaAreas();
    }
    protected void DropDownListArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregaProcessos();
    }
    protected void DropDownListProcesso_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregaSubprocessos();
    }
    protected void DropDownListSubprocesso_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregaAtividades();
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

            string ConteudoHTML = "<form><div style=\" padding: 5px\">" +
                                        "<b>DMS - SISTEMA DE GESTÃO DE DEMANDAS</b></br><br>" +
                                        "<b>Uma demanda de atendimento foi criada no sistema</b><br><br>" +
                                        "<b>Código da demanda: </b>" + DemandaId + "<br>" +
                                        "<b>Status da demanda: </b>" + DemandaStatus + "<br>" +
                                        "<b>Atividade:</b> " + Atividade + "<br>" +
                                        "<b>Detalhe:</b> " + Detalhe + "<br>" +
                                        "<b>Data de Abertura: </b>" + DataAbertura + "<br>" +
                                        "<b>Data prazo para encerramento: </b>" + Convert.ToDateTime(DataPrazo).ToString("dd/MM/yyyy") + "<br>" +
                                        "<b>Responsável pelo atendimento: </b>" + ResponsavelId + " - " + ResponsavelNome + "<br>" +
                                        "<b>Solicitante: </b>" + SolicitanteId + " - " + SolicitanteNome + "<br><br>" +
                                        EtapasHTML(DemandaId) + "<br><br>" +
                                        "   *   *   * </br>" +
                                        "Mensagem enviada automaticamente. <br>" +
                                        "<br><a href=\"http://dms-br.comaugroup.com\">Clique aqui para acessar ao sistema.</a><br> </div>" +
                                   "</div></form>";

            //Pega emails
            ArrayList emailDestinatarios = new ArrayList();

            emailDestinatarios.Add(EmailResponsavelAtendimento);

            EnviarEmail(emailDestinatarios, EmailSolicitanteAtendimento, "DMS - Nova Demanda: " + DemandaId, ConteudoHTML);

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
    protected void txtMatriculaFuncionario_TextChanged(object sender, EventArgs e)
    {
        if (txtMatriculaFuncionario.Text == "")
        {
            lblFuncionarioNome.Text = "";
        }
        else
        {
            string FuncionarioNome = consult.Consulta("SELECT Nome FROM Funcionarios WHERE Matricula = " + txtMatriculaFuncionario.Text, "Nome");

            if (FuncionarioNome == "" || FuncionarioNome == null)
            {
                lblFuncionarioNome.Text = "";
                lblFuncionarioNome.Focus();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Matrícula não localizada.')", true);
            }
            else
            {
                lblFuncionarioNome.Text = FuncionarioNome;
                DropDownListCN.Focus();
            }
        }
    }
    protected void RadioButtonListIndividualColetivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonListIndividualColetivo.SelectedValue == "1") //Individual
        {
            txtMatriculaFuncionario.Text = "";
            lblFuncionarioNome.Text = "";
            txtMatriculaFuncionario.Visible = true;
            lblFuncionarioNome.Visible = true;
            lblIndividualColetivoMensagem.Visible = false;
        }
        else
        {
            txtMatriculaFuncionario.Visible = false;
            lblFuncionarioNome.Visible = false;
            lblIndividualColetivoMensagem.Visible = true;
        }
    }

    public void carregaContrato()
    {
        var ds = consult.CarregaContratos(appSession.UserId, appSession.UserAdmin);

        if (ds != null)
        {
            DropDownListCN.DataSource = ds.Tables["Contrato"];
            DropDownListCN.DataTextField = ds.Tables["Contrato"].Columns["DescContract"].ToString();
            DropDownListCN.DataValueField = ds.Tables["Contrato"].Columns["ContratoID"].ToString();
            DropDownListCN.DataBind();
            DropDownListCN.Items.Insert(0, "Selecione...");
            DropDownListCN.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi possível listar contratos. Verifique suas permissões de acesso.')", true);
        }

    }

    public int SabadoDomingoFeriado(DateTime DataIni, DateTime DataFim)
    {
        int count = 0;
        DateTime DataVerifica = DataIni;

        while (DataVerifica <= DataFim)
        {
            if (DataVerifica.DayOfWeek == DayOfWeek.Sunday || DataVerifica.DayOfWeek == DayOfWeek.Saturday) 
                count++;

            DataVerifica = DataVerifica.Date.AddDays(1);
        }

        if (DataFim.DayOfWeek == DayOfWeek.Saturday)
            count++; //Se a data final for sábado, o domingo não foi contado no loop acima, por isso, adiciona-se mais um dia por causa do domingo

        count += Convert.ToInt32(consult.Consulta("SELECT COUNT (FeriadoId) as Quantidade FROM Feriados WHERE FeriadoData Between '" + DataIni.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'", "Quantidade"));
        
        return count;
    }

    protected void DropDownListAtividade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDetalhe.Text))
            txtDetalhe.Text = consult.Consulta("SELECT TemplateDetalhe FROM Atividades WHERE AtividadeId = " + DropDownListAtividade.SelectedValue, "TemplateDetalhe");
    }

}