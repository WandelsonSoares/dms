using System;
using App_Code;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Collections;
using Email;

public partial class Anexos : System.Web.UI.Page
{

    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    Permissoes permissao = new Permissoes();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["st"] == "PROCESSANDO" || Request.QueryString["st"] == "AGUARDANDO")
            {
                if (Request.QueryString["op"] == "ed") //Editar
                {
                    FileUpload1.Enabled = true;
                    btnImportar.Enabled = true;
                    GridView1.Columns[1].Visible = true;
                }
            }

            string ResponsavelId = consult.Consulta("SELECT ResponsavelId FROM DemandasEtapas WHERE DemandaEtapaId = " + Request.QueryString["id"], "ResponsavelId");

            if ((appSession.UserId == ResponsavelId && Request.QueryString["op"] == "ed") || appSession.UserAdmin == "S")
            {
                RadioButtonListDocOK.Enabled = true;
                txtComentario.Enabled = true;
                btnGravar.Enabled = true;
            }
            RadioButtonListDocOK.SelectedValue = consult.Consulta("SELECT DocumentoOK FROM DemandasEtapas WHERE DemandaEtapaId = " + Request.QueryString["id"], "DocumentoOK");
            txtComentario.Text = consult.Consulta("SELECT ComentarioAtendente FROM DemandasEtapas WHERE DemandaEtapaId = " + Request.QueryString["id"], "ComentarioAtendente");
        }

    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        Persistencia_Fast consulta = new Persistencia_Fast();

        if (FileUpload1.PostedFile == null)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha no upload.')", true);
            return;
        }

        string diretorio = "C:\\DMS\\Documentos\\Etapas\\";

        if (FileUpload1.HasFile)
        {
            try
            {
                string arq = FileUpload1.PostedFile.FileName;
                string extensao5caract = arq.Substring(arq.Length - 5).ToLower();
                string extensao4caract = arq.Substring(arq.Length - 4).ToLower();

                if ((extensao4caract != ".msg"
                    && extensao4caract != ".pdf"
                    && extensao4caract != ".jpg"
                    && extensao4caract != ".gif"
                    && extensao4caract != ".doc"
                    && extensao5caract != ".docx"
                    && extensao4caract != ".xls"
                    && extensao5caract != ".xlsx"))
                {
                    Label1.Text = "Arquivo inválido. Tipos permitidos: .jpg, .xlsX, .docX, .pdf, .gif e .msg.";
                }
                else
                {

                    if (!String.IsNullOrEmpty(Request.QueryString["t"]) && !String.IsNullOrWhiteSpace(Request.QueryString["t"]))

                        if (Request.QueryString["t"] != extensao4caract && Request.QueryString["t"] != extensao5caract)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Tipo de arquivo não compatível com o estabelecido pelo administrador do sistema. "
                                                                                                            + "Você deve anexar arquivo com extensão " + Request.QueryString["t"] + ".')", true);
                            return;
                        }
                    string NomeArquivo = FileUpload1.FileName;
                    NomeArquivo = tira_acentos(NomeArquivo).Replace(" ", "").Replace("'", "");
                    string caminho = diretorio + Request.QueryString["id"] + "-" + NomeArquivo;

                    FileUpload1.SaveAs(caminho);

                    consulta.atualizaInsereDados("UPDATE DemandasEtapas SET CaminhoDocumento = '" + caminho + "' WHERE DemandaEtapaId = " + Request.QueryString["Id"]);
                    GridView1.DataBind();

                    Label1.Text = "Arquivo enviado com sucesso.";

                }
            }
            catch (Exception ex)
            {
                Label1.Text = "ERRO: " + ex.Message.ToString();
            }
        }
        else
        {
            Label1.Text = "Escolha um arquivo para o upload.";
        }

    }


    public static string tira_acentos(string texto)
    {
        string ComAcentos = "!@#$%¨&*()-?:{}][ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç ";

        string SemAcentos = "_________________AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc_";

        for (int i = 0; i < ComAcentos.Length; i++)

            texto = texto.Replace(ComAcentos.ToString(), SemAcentos.ToString()).Trim();
        return texto;
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AbreDocumento")
        {
            string CaminhoDocumento = consult.Consulta("SELECT CaminhoDocumento FROM DemandasEtapas WHERE DemandaEtapaId = " + Request.QueryString["id"], "CaminhoDocumento");
            if (CaminhoDocumento != "")
            {
                if (Download(CaminhoDocumento, true) == true)
                {

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Arquivo não localizado no servidor.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Caminho de arquivo inválido.')", true);
                return;
            }

        }


        if (e.CommandName == "ExcluiDocumento")
        {


            string path = consult.Consulta("SELECT CaminhoDocumento FROM DemandasEtapas WHERE DemandaEtapaId = " + Request.QueryString["id"], "CaminhoDocumento");
            try
            {
                using (StreamWriter sw = File.CreateText(path)) { }
                string path2 = path;


                File.Delete(path2);

                consult.atualizaInsereDados("UPDATE DemandasEtapas SET CaminhoDocumento = '' WHERE DemandaEtapaId = " + Request.QueryString["id"]);
                GridView1.DataBind();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha no processo.')", true);
            }



        }


    }

    private bool Download(string fname, bool forceDownload)
    {
        try
        {

            string path = fname;
            string name = Path.GetFileName(path);
            string ext = Path.GetExtension(path);
            string type = "";
            // set known types based on file extension  
            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    case ".jpg":
                        type = "image/jpg";
                        break;

                    case ".htm":
                    case ".html":
                        type = "text/HTML";
                        break;

                    case ".txt":
                        type = "text/plain";
                        break;

                    case ".msg":
                    case ".doc":
                    case ".rtf":
                    case ".xls":
                    case ".docx":
                    case ".xlsx":
                    case ".pdf":
                        type = "Application/msword";
                        break;


                }
            }
            if (forceDownload)
            {
                Response.AppendHeader("content-disposition", "attachment; filename=" + name);
            }
            if (type != "")
            {
                Response.ContentType = type;
                Response.TransmitFile(path);
                Response.End();
            }

            return true;
        }
        catch
        {
            return false;
        }


    }

    private void downloadAnImage(string strImage)
    {
        Response.ContentType = "image/jpg";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + strImage);
        Response.TransmitFile(strImage);
        Response.End();
    }

    protected void btnGravar_Click(object sender, EventArgs e)
    {
        if (RadioButtonListDocOK.SelectedValue != "" && RadioButtonListDocOK.SelectedValue != null)
        {
            if (RadioButtonListDocOK.SelectedValue == "NÃO")
            {
                if (txtComentario.Text != "")
                {
                    consult.atualizaInsereDados("UPDATE DemandasEtapas SET DocumentoOK = '" + RadioButtonListDocOK.SelectedValue +
                                                        "', ComentariosExistencia = 'n' " +
                                                        ", ComentarioAtendente = '" + txtComentario.Text.Replace("'", "") + "' WHERE DemandaEtapaId = " + Request.QueryString["id"]);

                    string DemandaId = consult.Consulta("SELECT DemandaId FROM DemandasEtapas WHERE DemandaEtapaId = " + Request.QueryString["id"], "DemandaId");

                    EnviarEmailPrepara(DemandaId, Request.QueryString["id"], "ATENÇÃO! O anexo da solicitação abaixo foi indicado como NÃO CONFORME pelo atendente. Gentileza verificar.");
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Insira no campo comentário uma justificativa para a não conformidade do documento.')", true);
            }
            else
            {
                consult.atualizaInsereDados("UPDATE DemandasEtapas SET DocumentoOK = '" + RadioButtonListDocOK.SelectedValue + "', ComentariosExistencia = 's' WHERE DemandaEtapaId = " + Request.QueryString["id"]);
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Selecione uma opção de conformidade do arquivo anexado.')", true);

    }

    /// <summary>
    /// Classe que compõe o email de notificação de alteração da demanda. É enviado para solicitante e para o responsável pelo atendimento.
    /// </summary>
    /// <param name="TipoMensagem">Tipo 1 = Cancelamento, Tipo 2 = Atualização.</param>
    /// <param name="DemandaId">Código da demanda que está sendo editada.</param>
    /// <param name="MensagemCorpo">Mensagem impressa no corpo do email identificando que tipo de atualização ocorreu na demanda.</param>
    /// <param name="JustificativaCancelamento">Justificativa de cancelamento da demanda.</param>
    private void EnviarEmailPrepara(string DemandaId, string DemandaEtapaId, string MensagemCorpo)
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



            string MensagemCorpoHTML = "<b>" + MensagemCorpo + "</b></br>"; ;

            string ConteudoHTML = "<div style=\" padding: 5px\">" +
                                        "<b>DMS - SISTEMA DE GESTÃO DE DEMANDAS</b></br></br>" +
                                        MensagemCorpoHTML + "</br></br>" +
                                        "<b>Comentário do atendente: </b>" + txtComentario.Text + "</br></br>" +
                                        "<b>Código da demanda: </b>" + DemandaId + "</br>" +
                                        "<b>Status da demanda: </b>" + DemandaStatus + "</br>" +
                                        "<b>Atividade:</b> " + Atividade + "</br>" +
                                        EtapasHTML(DemandaId) + "</br></br>" +
                                        "   *   *   * </br>" +
                                        "Mensagem enviada automaticamente. </br>" +
                                        "<br><a href=\"http://dms-br.comaugroup.com\">Clique aqui para acessar ao sistema.</a><br></div>" +
                                   "</div>";

            //Pega emails
            ArrayList emailDestinatarios = new ArrayList();

            emailDestinatarios.Add(EmailResponsavelAtendimento);

            EnviarEmail(emailDestinatarios, EmailSolicitanteAtendimento, "DMS - Sistema de Gestão de Demandas - Atualização de demanda", ConteudoHTML);

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
}