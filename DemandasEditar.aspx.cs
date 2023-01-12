using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using System.IO;
using Email;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;


public partial class DemandasEditar : System.Web.UI.Page
{
    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    Permissoes Permissao = new Permissoes();
    _Usuario usuario = new _Usuario();
    string PrioridadeId;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Permissao.PermissaoParaVerDemanda(appSession.UserId, appSession.UserAdmin, Request.QueryString["id"]) == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Acesso não autorizado para esta demanda.')", true);
                Response.Redirect("Demandas.aspx");
                usuario.LogIsert(appSession.FullName, "Demandas - Editar", "Teve acesso negado para a demanda código " + Request.QueryString["id"] + ".", appSession.IP);
            }
            else
            {
                CarregaContrato();
                ControlaPermissoes();
                CarregaDemanda();

                usuario.LogIsert(appSession.FullName, "Demandas - Editar", "Acessou demanda código " + Request.QueryString["id"] + ".", appSession.IP);
            }
        }

    }

    private void ControlaPermissoes()
    {
        //Botão Cancelar Demanda

        string responsavelId = consult.Consulta("SELECT ResponsavelId FROM Demandas WHERE DemandaId = " + Request.QueryString["id"], "ResponsavelId");
        string solicitanteId = consult.Consulta("SELECT SolicitanteId FROM Demandas WHERE DemandaId = " + Request.QueryString["id"], "SolicitanteId");

        if ((responsavelId == appSession.UserId || solicitanteId == appSession.UserId || appSession.UserAdmin == "S") && (txtStatus.Text != "CONCLUIDA" || txtStatus.Text != "CANCELADA" || txtStatus.Text != ""))
        {
            btnCancelar.Enabled = true;
        }

        if (appSession.UserAdmin == "S" && txtStatus.Text == "CANCELADA")
        {
            btnReabrir.Visible = true;
        }

        if (appSession.UserAdmin == "S" || responsavelId == appSession.UserId)
        {
            DropDownListClassificacao.Enabled = true;
            txtComentarioAtendente.Enabled = true;
            btnGravar.Enabled = true;
            btnEtapaComentarioGravar.Enabled = true;
            DropDownListPrioridade.Enabled = true;
        }

        if ((appSession.UserAdmin == "S" || appSession.UserId == solicitanteId) && txtStatus.Text != "CONCLUIDA" && txtStatus.Text != "CANCELADA" )
        {
            DropDownListCN.Enabled = true;
            RadioButtonListIndividualColetivo.Enabled = true;
            txtMatriculaFuncionario.Enabled = true;
            txtDetalhe.Enabled = true;
            btnGravar.Enabled = true;
        }

    }

    private void CarregaDemanda()
    {
        if (Request.QueryString["id"] != null)
        {
            string DemandaId = Request.QueryString["id"];

            txtCodigo.Text = Request.QueryString["id"];
            txtStatus.Text = consult.Consulta("SELECT Status FROM Demandas WHERE DemandaId = " + DemandaId, "Status");
            txtDataAbertura.Text = consult.Consulta("SELECT DataAbertura FROM Demandas WHERE DemandaId = " + DemandaId, "DataAbertura");
            txtPrazo.Text = consult.Consulta("SELECT DataPrazo FROM Demandas WHERE DemandaId = " + DemandaId, "DataPrazo");
            if (txtPrazo.Text != "")
                txtPrazo.Text = Convert.ToDateTime(txtPrazo.Text).ToString("dd/MM/yyyy");

            DropDownListCN.SelectedValue = consult.Consulta("SELECT CNId FROM Demandas WHERE DemandaId = " + DemandaId, "CNId");
            RadioButtonListIndividualColetivo.SelectedValue = consult.Consulta("SELECT UnicoMultiplo FROM Demandas WHERE DemandaId = " + DemandaId, "UnicoMultiplo");

            if (RadioButtonListIndividualColetivo.SelectedValue == "1") //Individual
            {
                txtMatriculaFuncionario.Text = consult.Consulta("SELECT Matricula FROM Demandas WHERE DemandaId = " + DemandaId, "Matricula");
                lblFuncionarioNome.Text = consult.Consulta("SELECT Nome FROM Funcionarios WHERE Matricula = " + txtMatriculaFuncionario.Text, "Nome");
            }
            else
                txtMatriculaFuncionario.Text = "";

            txtDataConclusao.Text = consult.Consulta("SELECT DataEncerramento FROM Demandas WHERE DemandaId = " + DemandaId, "DataEncerramento");
            txtSolicitante.Text = consult.Consulta("SELECT Nome FROM Usuarios INNER JOIN Demandas ON Usuarios.UserId = Demandas.SolicitanteId WHERE Demandas.DemandaId = " + DemandaId, "Nome");
            txtResponsavel.Text = consult.Consulta("SELECT Nome FROM Usuarios INNER JOIN Demandas ON Usuarios.UserId = Demandas.ResponsavelId WHERE Demandas.DemandaId = " + DemandaId, "Nome");
            txtProcesso.Text = consult.Consulta("SELECT Nome FROM Processos INNER JOIN Demandas ON Processos.ProcessoId = Demandas.ProcessoId WHERE Demandas.DemandaId = " + DemandaId, "Nome");
            txtSubprocesso.Text = consult.Consulta("SELECT Nome FROM Subprocessos INNER JOIN Demandas ON Subprocessos.SubprocessoId = Demandas.SubprocessoId WHERE Demandas.DemandaId = " + DemandaId, "Nome");
            txtAtividade.Text = consult.Consulta("SELECT Nome FROM Atividades INNER JOIN Demandas ON Atividades.AtividadeId = Demandas.AtividadeId WHERE Demandas.DemandaId = " + DemandaId, "Nome");
            txtDetalhe.Text = consult.Consulta("SELECT Detalhe FROM Demandas WHERE DemandaId = " + DemandaId, "Detalhe");

            if (txtStatus.Text == "CANCELADA")
            {
                lblJustificativa.Visible = true;
                txtJustificativa.Text = consult.Consulta("SELECT JustificativaCancelamento FROM Demandas WHERE DemandaId = " + DemandaId, "JustificativaCancelamento");
                txtJustificativa.Visible = true;
            }
            else
            {
                lblJustificativa.Visible = false;
                txtJustificativa.Text = "";
                txtJustificativa.Visible = false;
            }

            string ClassificacaoId = consult.Consulta("SELECT DemandaClassificacaoId FROM Demandas WHERE DemandaId = " + DemandaId, "DemandaClassificacaoId");
            DropDownListClassificacao.SelectedValue = ClassificacaoId == "" ? null : ClassificacaoId;
            txtComentarioAtendente.Text = consult.Consulta("SELECT ComentarioAtendente FROM Demandas WHERE DemandaId = " + DemandaId, "ComentarioAtendente");

            HiddenFieldResponsavelDemandaId.Value = consult.Consulta("SELECT ResponsavelId FROM Demandas WHERE DemandaId = " + DemandaId, "ResponsavelId");
            HiddenFieldSolicitanteDemandaId.Value = consult.Consulta("SELECT SolicitanteId FROM Demandas WHERE DemandaId = " + DemandaId, "SolicitanteId");

            DropDownListPrioridade.SelectedValue = PrioridadeId = consult.Consulta("SELECT PrioridadeId FROM Demandas WHERE DemandaId = " + DemandaId, "PrioridadeId");

            ImagePrioridade.ImageUrl = "~/img/demandasPrioridades/" + PrioridadeId + ".png";

            GridView1.DataBind();

            //if (txtStatus.Text == "CONCLUÍDA")
            //btnAvaliacaoSatisfacaoAbrir.Enabled = true;
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = -1;
        string ResponsavelEtapaId = "";
        string StatusAtual = "";

        index = Convert.ToInt32(e.CommandArgument);
        GridViewRow selectedRow = GridView1.Rows[index];
        TableCell Id = selectedRow.Cells[1];
        TableCell TipoArquivo = selectedRow.Cells[13];


        ResponsavelEtapaId = consult.Consulta("SELECT ResponsavelId FROM DemandasEtapas WHERE DemandaEtapaId = " + Id.Text, "ResponsavelId");
        StatusAtual = consult.Consulta("SELECT Status FROM DemandasEtapas WHERE DemandaEtapaId = " + Id.Text, "Status");


        //Edição autorizada para:
        // - Administrador 
        // - Responsável pela etapa
        // - Responsável pelo atendimento
        // - Responsável pelo setor 
        if (ResponsavelEtapaId == appSession.UserId || HiddenFieldResponsavelDemandaId.Value == appSession.UserId || Permissao.PermissaoParaAtualizarDemanda(appSession.UserId, appSession.UserAdmin, txtCodigo.Text))
        {
            if (e.CommandName == "EditarStatus" && txtStatus.Text != "CANCELADA")
            {
                HiddenFieldEtapaId.Value = Id.Text;
                DropDownListEtapaStatus.SelectedValue = StatusAtual;
                PanelEtapaStatus.Visible = true;
            }
            else
            {
                HiddenFieldEtapaId.Value = "";
                PanelEtapaStatus.Visible = false;
            }

            if (e.CommandName == "Documentos")
            {
                string op = "";

                //Se o usuário logado for o solicitante, o atendente ou administrador, a Query String conterá permissão de edição, que desbloqueia o botão excluir e upload.
                if (appSession.UserId == HiddenFieldSolicitanteDemandaId.Value || appSession.UserId == HiddenFieldResponsavelDemandaId.Value || appSession.UserAdmin == "S")
                    op = "ed";
                else
                    op = "vw";

                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'Anexos.aspx?Id=" + Id.Text + "&st=" + txtStatus.Text + "&op=" + op + "&t=" + TipoArquivo.Text + "', null, 'height=450,width=800,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes' );", true);
            }

            if (e.CommandName == "DocumentosModelo")
            {
                string CaminhoDocumento = consult.Consulta("SELECT DocumentoModeloCaminho FROM DemandasEtapas WHERE DemandaEtapaId = " + Id.Text, "DocumentoModeloCaminho");
                if (CaminhoDocumento != "")
                {

                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'EtapasDocumentoModeloDownload.aspx?id=" + Id.Text + "', null, 'height=200,width=400,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes' );", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Esta etapa não possui documento padrão.')", true);
                    return;
                }
            }

            if (e.CommandName == "AbrirComentarios")
            {
                txtEtapaComentario.Text = consult.Consulta("SELECT ComentarioAtendente FROM DemandasEtapas WHERE DemandaEtapaId = " + Id.Text, "ComentarioAtendente");
                lblAutorNome.Text = txtResponsavel.Text;
                HiddenFieldEtapaId.Value = Id.Text;
                PanelEtapaComentario.Visible = true;
            }

        }

        else
        {
            HiddenFieldEtapaId.Value = "";
            PanelEtapaComentario.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Acesso negado para essa operação.')", true);
            return;
        }
    }

    protected void btnGravarStatus_Click(object sender, EventArgs e)
    {

        try
        {
            consult.atualizaInsereDados("UPDATE DemandasEtapas SET Status = '" + DropDownListEtapaStatus.SelectedValue + "' WHERE DemandaEtapaId = " + HiddenFieldEtapaId.Value);

            usuario.LogIsert(appSession.FullName, "Demandas - Editar", "Atualizou status para " + DropDownListEtapaStatus.SelectedItem + " para a Etapa código " + HiddenFieldEtapaId.Value + ".", appSession.IP);

            AtualizaStatusDemanda(txtCodigo.Text);

            PanelEtapaStatus.Visible = false;

        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha na operação.')", true);
        }

        CarregaDemanda();

        if (txtStatus.Text == "CONCLUIDA")
            consult.NotificacaoEnvia(HiddenFieldSolicitanteDemandaId.Value, "Demanda atualizada", "Sua demanda código " + txtCodigo.Text + " [" + txtAtividade.Text + " para o contrato " +
                                       DropDownListCN.SelectedItem + "] recebeu status " + txtStatus.Text + ".", "DemandasEditar.aspx?Id=" + txtCodigo.Text);

    }

    private void AtualizaStatusDemanda(string DemandaId)
    {
        string Status = "";
        int QuantidadeEtapasConcluido;
        int QuantidadeEtapasProcessando;
        int QuantidadeEtapasTotal;
        string DataPrazo;

        QuantidadeEtapasTotal = Convert.ToInt32(consult.Consulta("SELECT COUNT (EtapaId) AS Quantidade FROM DemandasEtapas WHERE DemandaId = " + DemandaId, "Quantidade"));
        QuantidadeEtapasConcluido = Convert.ToInt32(consult.Consulta("SELECT COUNT (EtapaId) AS Quantidade FROM DemandasEtapas WHERE Status = 'CONCLUIDA' AND DemandaId = " + DemandaId, "Quantidade"));

        if (QuantidadeEtapasConcluido < QuantidadeEtapasTotal)
        {

            DataPrazo = txtPrazo.Text;

            if (DataPrazo != "")
            {
                if (Convert.ToDateTime(DataPrazo) < DateTime.Today)
                {
                    Status = "ATRASADA";
                }
            }

            if (Status != "ATRASADA")
            {

                //Não estão todas as etapas concluídas
                QuantidadeEtapasProcessando = Convert.ToInt32(consult.Consulta("SELECT COUNT (EtapaId) AS Quantidade FROM DemandasEtapas WHERE Status = 'PROCESSANDO' AND DemandaId = " + DemandaId, "Quantidade"));
                QuantidadeEtapasConcluido = Convert.ToInt32(consult.Consulta("SELECT COUNT (EtapaId) AS Quantidade FROM DemandasEtapas WHERE Status = 'CONCLUIDA' AND DemandaId = " + DemandaId, "Quantidade"));

                if (QuantidadeEtapasProcessando > 0 || QuantidadeEtapasConcluido > 0)
                    Status = "PROCESSANDO";
                else
                    Status = "AGUARDANDO";
            }
        }
        else
        {
            Status = "CONCLUIDA";
        }


        if (Status == "CONCLUIDA")
        {
            //Verifica se foi concluída no prazo.
            string AtendidaNoPrazo = "";

            TimeSpan DiasAtraso = DateTime.Today - Convert.ToDateTime(txtPrazo.Text);

            if (DiasAtraso.Days > 0)
                AtendidaNoPrazo = "NÃO";
            else
            {
                AtendidaNoPrazo = "SIM";
            }

            consult.atualizaInsereDados("UPDATE Demandas SET Status = '" + Status +
                                                            "', DataEncerramento = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                                                            "', CumprimentoPrazo = '" + AtendidaNoPrazo +
                                                            "', DiasAtrasoFinal = " +
                                                            DiasAtraso.Days.ToString() + " WHERE DemandaId = " + DemandaId);

            consult.atualizaInsereDados("DELETE FROM AvaliacoesSatisfacao WHERE DemandaId = " + DemandaId);
            consult.atualizaInsereDados("INSERT INTO AvaliacoesSatisfacao VALUES (" + HiddenFieldSolicitanteDemandaId.Value + ", " + DemandaId + ", NULL, GETDATE(), DATEADD(DD,5,GETDATE()), NULL, NULL, NULL, NULL)");

            EnviarEmailPrepara(2, DemandaId, "Demanda " + DemandaId + " concluída.");
        }
        //else
        //{
        //    consult.atualizaInsereDados("UPDATE Demandas SET Status = '" + Status + "', DataEncerramento = null, CumprimentoPrazo = null, DiasAtrasoFinal = null WHERE DemandaId = " + DemandaId);

        //    if (consult.Consulta("SELECT EnviarEmailDemandasAtualizacao FROM ConfiguracoesEmails", "EnviarEmailDemandasAtualizacao") == "True")
        //        EnviarEmailPrepara(2, DemandaId, "A demanda abaixo foi atualizada.");
        //}
    }

    protected void btnCancelarStatus_Click(object sender, EventArgs e)
    {
        PanelEtapaStatus.Visible = false;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (txtStatus.Text == "CONCLUIDA" && appSession.UserAdmin != "S")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Demandas concluídas não podem ser canceladas.')", true);
            return;
        }
        else
        {

            if (Permissao.PermissaoParaCancelarDemanda(appSession.UserId, appSession.UserAdmin, txtCodigo.Text, txtStatus.Text) == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Você não tem permissões para essa operação.')", true);
            }
            else
            {
                txtJustificativaCancelamento.Text = "";
                PanelJustificativaCancelamento.Visible = true;
            }
        }
    }

    protected void btnReabrir_Click(object sender, EventArgs e)
    {
        if (txtStatus.Text == "CANCELADA")
        {
            if (appSession.UserAdmin == "S")
            {
                consult.atualizaInsereDados("UPDATE Demandas SET Status = 'AGUARDANDO', UsuarioCancelamento = null, JustificativaCancelamento = null WHERE DemandaId = " + txtCodigo.Text);
                CarregaDemanda();
                usuario.LogIsert(appSession.FullName, "Demandas - Editar", "Reabriu demanda cancelada código " + txtCodigo.Text + ".", appSession.IP);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Apenas administradores do sistema podem reabrir demandas.')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('O status dessa demanda não permite essa operação.')", true);
        }
    }

    /// <summary>
    /// Classe que compõe o email de notificação de alteração da demanda. É enviado para solicitante e para o responsável pelo atendimento.
    /// </summary>
    /// <param name="TipoMensagem">Tipo 1 = Cancelamento, Tipo 2 = Atualização.</param>
    /// <param name="DemandaId">Código da demanda que está sendo editada.</param>
    /// <param name="MensagemCorpo">Mensagem impressa no corpo do email identificando que tipo de atualização ocorreu na demanda.</param>
    /// <param name="JustificativaCancelamento">Justificativa de cancelamento da demanda.</param>
    private void EnviarEmailPrepara(int TipoMensagem, string DemandaId, string MensagemCorpo)
    {
        try
        {
            //Refatorar este trecho futuramente para evitar as várias idas ao banco em busca das informações. Ler do próprio formulário.
            //Seleciona lista de planos de ação com ações atrasadas para o contrato atual
            string EmailResponsavelAtendimento = consult.Consulta("SELECT Email FROM Usuarios INNER JOIN Demandas ON Usuarios.UserID = Demandas.ResponsavelId WHERE Demandas.DemandaId = " + DemandaId, "Email");
            string ResponsavelId = consult.Consulta("SELECT ResponsavelId FROM Demandas WHERE DemandaId = " + DemandaId, "ResponsavelId");

            string EmailSolicitanteAtendimento = consult.Consulta("SELECT Email FROM Usuarios INNER JOIN Demandas ON Usuarios.UserID = Demandas.SolicitanteId WHERE Demandas.DemandaId = " + DemandaId, "Email");
            string SolicitanteId = consult.Consulta("SELECT SolicitanteId FROM Demandas WHERE DemandaId = " + DemandaId, "SolicitanteId");

            string Atividade = consult.Consulta("SELECT Nome FROM Atividades INNER JOIN Demandas ON Demandas.AtividadeId = Atividades.AtividadeId WHERE DemandaId = " + DemandaId, "Nome");
            string DemandaStatus = consult.Consulta("SELECT Status FROM Demandas WHERE DemandaId = " + DemandaId, "Status");

            string Classificacao = consult.Consulta("SELECT Nome FROM DemandasClassificacoes INNER JOIN " +
                                "Demandas ON Demandas.DemandaClassificacaoId = DemandasClassificacoes.DemandaClassificacaoId WHERE DemandaId = " + DemandaId, "Nome");

            string ComentarioAtendente = consult.Consulta("SELECT ComentarioAtendente FROM Demandas WHERE DemandaId = " + DemandaId, "ComentarioAtendente");

            string MensagemCorpoHTML = "<b>" + MensagemCorpo + "</b><br>"; ;
            string JustificativaCancelamento = "";
            string JustificativaCancelamentoHTML = "";
            string UsuarioCancelamento = "";
            string UsuarioCancelamentoHTML = "";

            if (TipoMensagem == 1) //CANCELAMENTO
            {
                JustificativaCancelamento = consult.Consulta("SELECT JustificativaCancelamento FROM Demandas WHERE DemandaId = " + DemandaId, "JustificativaCancelamento");
                JustificativaCancelamentoHTML = "<b>Justificativa do cancelamento: </b>" + JustificativaCancelamento + "<br/><br>";
                UsuarioCancelamento = consult.Consulta("SELECT Nome FROM Usuarios INNER JOIN Demandas ON Usuarios.UserId = Demandas.UsuarioCancelamento WHERE DemandaId = " + DemandaId, "Nome");
                UsuarioCancelamentoHTML = "<b>Cancelado pelo usuário: </b>" + UsuarioCancelamento + "<br>";
            }

            string ConteudoHTML = "<form><div style=\" padding: 5px\">" +
                                        "<b>Sistema de Gestão de Demandas</b><br><br>" +
                                        MensagemCorpoHTML +
                                        JustificativaCancelamentoHTML +
                                        UsuarioCancelamentoHTML +
                                        "<b>Código da demanda: </b>" + DemandaId + "<br>" +
                                        "<b>Status da demanda: </b>" + DemandaStatus + "<br>" +
                                        "<b>Classificão pelo atendente: </b>" + Classificacao + "<br>" +
                                        "<b>Comentário do atendente: </b><span style=\"color:red\">" + ComentarioAtendente + "</span><br>" +
                                        "<b>Atividade:</b> " + Atividade + "<br>" +
                                        "<b>Detalhe:</b> " + txtDetalhe.Text.Replace("'","") + "<br>" +
                                        "<b>Data de Abertura: </b>" + txtDataAbertura.Text + "<br>" +
                                        "<b>Data prazo para encerramento: </b>" + txtPrazo.Text + "<br>" +
                                        "<b>Responsável pelo atendimento: </b>" + ResponsavelId + " - " + txtResponsavel.Text + "<br>" +
                                        "<b>Solicitante: </b>" + SolicitanteId + " - " + txtSolicitante.Text + "<br><br>" +
                                        EtapasHTML(DemandaId) + "<br><br>" +
                                        "   *   *   * <br>" +
                                        "Mensagem enviada automaticamente. <br>" +
                                        "<br><a href=\"http://dms-br.comaugroup.com\">Clique aqui para acessar ao sistema.</a><br></div>" +
                                   "</div></form>";

            //Pega emails
            ArrayList emailDestinatarios = new ArrayList();

            emailDestinatarios.Add(EmailResponsavelAtendimento);


            EnviarEmail(emailDestinatarios, EmailSolicitanteAtendimento, MensagemCorpo, ConteudoHTML);

        }

        catch (Exception ex)
        {
            Response.Write("<script>window.alert('Falha no envio do email de notificação. " + ex.Message + "');</script>");
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

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        if (txtJustificativaCancelamento.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Informe a justificativa.')", true);
            return;
        }


        try
        {
            consult.atualizaInsereDados("UPDATE Demandas SET Status = 'CANCELADA', UsuarioCancelamento = " + appSession.UserId + ", JustificativaCancelamento = '" +
                                        txtJustificativaCancelamento.Text.Replace("'", "").Replace("\\", "") + "' WHERE DemandaId = " + txtCodigo.Text);
            CarregaDemanda();
            EnviarEmailPrepara(1, txtCodigo.Text, "Atenção! A demanda abaixo foi CANCELADA.");

            usuario.LogIsert(appSession.FullName, "Demandas - Editar", "Cancelou a demanda código " + txtCodigo.Text + ".", appSession.IP);

            Response.Redirect("Demandas.aspx");
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha no envio do email de notificação.')", true);
        }

    }

    protected void btnFechar_Click(object sender, EventArgs e)
    {
        PanelJustificativaCancelamento.Visible = false;
    }

    protected void btnAvaliacaoSatisfacaoAbrir_Click(object sender, EventArgs e)
    {
        string op = "";
        string id = consult.Consulta("SELECT AvaliacaoId FROM AvaliacoesSatisfacao WHERE DemandaId = " + txtCodigo.Text, "AvaliacaoId");

        if (HiddenFieldSolicitanteDemandaId.Value == appSession.UserId && txtStatus.Text == "CONCLUIDA")
        {
            if (id == null)
            {
                consult.atualizaInsereDados("INSERT INTO AvaliacoesSatisfacao (AvaliadorId, DemandaId, DataAbertura, DataEncerramento) " +
                                    "VALUES (" + HiddenFieldSolicitanteDemandaId.Value + ", " +
                                     txtCodigo.Text + ", '" +
                                     Convert.ToDateTime(txtDataConclusao.Text) + "', '" +
                                     Convert.ToDateTime(txtDataConclusao.Text).AddDays(5) + "')");
                id = consult.Consulta("SELECT MAX(AvaliacaoId) AS MaiorID from AvaliacoesSatisfacao WHERE DemandaId = " + txtCodigo.Text, "MaiorId");

                consult.atualizaInsereDados("INSERT INTO AvaliacoesSatisfacaoQuestoesRespostas (AvaliacaoId, Descricao, Ordem, DemandaId) VALUES (" + id +
                                            ", '1. A resposta dada a sua demanda.', 1, " + txtCodigo.Text + ")");
                consult.atualizaInsereDados("INSERT INTO AvaliacoesSatisfacaoQuestoesRespostas (AvaliacaoId, Descricao, Ordem, DemandaId) VALUES (" + id +
                                                ", '2. O tempo de resposta à sua demanda.', 2, " + txtCodigo.Text + ")");
                consult.atualizaInsereDados("INSERT INTO AvaliacoesSatisfacaoQuestoesRespostas (AvaliacaoId, Descricao, Ordem, DemandaId) VALUES (" + id +
                                ", '3. A cordialidade do atendente.', 3, " + txtCodigo.Text + ")");
                consult.atualizaInsereDados("INSERT INTO AvaliacoesSatisfacaoQuestoesRespostas (AvaliacaoId, Descricao, Ordem, DemandaId) VALUES (" + id +
                                ", '4. A clareza na resposta à sua demanda.', 4, " + txtCodigo.Text + ")");

                op = "ed";
            }
            else
            {
                if (consult.Consulta("SELECT DataEncerramento = null FROM AvaliacoesSatisfacao WHERE AvaliacaoId = " + id, "DataEncerramento") == "")
                    op = "ed";
                else
                    op = "vw";
            }

        }

        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'AvaliacaoSatisfacao.aspx?id=" + id + "&op=" + op + "', null, 'height=750,width=550,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes' );", true);

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
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Matrícula não localizada na lista de funcionários.')", true);
            }
            else
                lblFuncionarioNome.Text = FuncionarioNome;
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
        }
        else
        {
            txtMatriculaFuncionario.Visible = false;
            lblFuncionarioNome.Visible = false;
        }
    }

    public void CarregaContrato()
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
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        string Matricula = String.IsNullOrEmpty(txtMatriculaFuncionario.Text.Replace("'", "")) ? "NULL" : txtMatriculaFuncionario.Text.Replace("'", "");

        if (RadioButtonListIndividualColetivo.SelectedValue == "1" && txtMatriculaFuncionario.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Informe a matrícula do funcionário.')", true);
            RadioButtonListIndividualColetivo.Focus();
            return;
        }

        if ((DropDownListClassificacao.SelectedValue == "2" || DropDownListClassificacao.SelectedValue == "3") && txtComentarioAtendente.Text == "")
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Insira um comentário sobre esta classificação.')", true);
        else
        {
            try
            {
                consult.atualizaInsereDados("UPDATE Demandas SET ComentarioAtendente = '" + txtComentarioAtendente.Text + "', DemandaClassificacaoId = " + DropDownListClassificacao.SelectedValue +
                                            ", Detalhe = '" + txtDetalhe.Text.Replace("'", "") + "', CNId = " + DropDownListCN.SelectedValue + ", Matricula = " + Matricula +  
                                            ", UnicoMultiplo = " + RadioButtonListIndividualColetivo.SelectedValue + 
                                            ", PrioridadeId = " + DropDownListPrioridade.SelectedValue + 
                                            " WHERE DemandaId = " + txtCodigo.Text);
                
                string atualizador = "administrador";

                if (appSession.UserId == HiddenFieldResponsavelDemandaId.Value)
                    atualizador = "atendente";
                else
                    if (appSession.UserId == HiddenFieldSolicitanteDemandaId.Value)
                        atualizador = "solicitante";

                EnviarEmailPrepara(2, txtCodigo.Text, "A demanda " + txtCodigo.Text + " foi atualizada pelo " + atualizador + ".");

                usuario.LogIsert(appSession.FullName, "Demandas - Editar", "Gravou atualização / comentário para a demanda código " + txtCodigo.Text + ".", appSession.IP);

                consult.NotificacaoEnvia(HiddenFieldSolicitanteDemandaId.Value, "Demanda atualizada", "Sua demanda códgo " + txtCodigo.Text + " (" + txtAtividade.Text + " para " +
                                                                        " o contrato " + DropDownListCN.SelectedItem + ") foi atualizada pelo " + atualizador + ".", "/DemandasEditar.aspx?Id=" + txtCodigo.Text);

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Gravado com sucesso. Um email de notificação foi enviado aos envolvidos.')", true);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha na gravação.')", true);
            }
        }

    }
    protected void btnEtapaComentarioFechar_Click(object sender, EventArgs e)
    {
        PanelEtapaComentario.Visible = false;
    }
    protected void btnEtapaComentarioGravar_Click(object sender, EventArgs e)
    {
        try
        {
            consult.atualizaInsereDados("UPDATE DemandasEtapas SET ComentarioAtendente = '" + txtEtapaComentario.Text.Replace("'", "") + "' WHERE DemandaEtapaId = " + HiddenFieldEtapaId.Value);
            usuario.LogIsert(appSession.FullName, "Demandas - Editar", "Gravou comentário para a etapa código " + HiddenFieldEtapaId.Value + ".", appSession.IP);
            consult.NotificacaoEnvia(HiddenFieldSolicitanteDemandaId.Value, "Comentário do atendente",
                                    "Atendente comentou \"" + txtComentarioAtendente.Text + "\" em relação à sua demanda código " + txtCodigo.Text, "DemandasEditar.aspx?Id=" + txtCodigo.Text);
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha na gravação.')", true);
        }
    }

    protected void DropDownListPrioridade_SelectedIndexChanged(object sender, EventArgs e)
    {
        PrioridadeId = DropDownListPrioridade.SelectedValue;
        ImagePrioridade.ImageUrl = "~/img/demandasPrioridades/" + PrioridadeId + ".png";
        ImagePrioridade.Focus();
    }
}