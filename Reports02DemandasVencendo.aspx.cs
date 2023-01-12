using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using Email;

public partial class Reports02DemandasVencendo : System.Web.UI.Page
{
    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();
    int Executando;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (appSession.UserAdmin == "S")
                btnExibirOcultarOpcoesEmail.Visible = true;
            else
                btnExibirOcultarOpcoesEmail.Visible = false;



            CarregaAreasFitlro();
            DropDownListArea_SelectedIndexChanged(sender, e);

            usuario.LogIsert(appSession.FullName, "Relatório 02 - Demandas próximas ao vencimento", "Acessou relatório de demandas próximas ao vencimento.", appSession.IP);
        }

        Executando = Convert.ToInt32(consult.Consulta("SELECT [Executando] FROM ProcessosStatus", "Executando"));

        if (Executando == 1)
        {
            txtStatus.Text = consult.Consulta("SELECT [Status] FROM ProcessosStatus", "Status");
            UpdatePanel1.Update();
        }
        else
        {
            if (Executando == 2)
            {
                if (IsPostBack)
                {
                    Timer1.Enabled = false;
                    txtStatus.Text = consult.Consulta("SELECT [Status] FROM ProcessosStatus", "Status");
                    consult.atualizaInsereDados("UPDATE ProcessosStatus SET Executando = 0 WHERE IdProcesso = 1");
                    ComponentesHabilita();
                    UpdatePanel1.Update();
                    UpdatePanel2.Update();
                }
                else
                {
                    txtStatus.Text = "";
                    UpdatePanel1.Update();
                }
            }
            Timer1.Enabled = false;
            imgEmail.Visible = false;
            lblEnviando.Visible = false;
        }
    }

    protected void btnExibirOcultarOpcoesEmail_Click(object sender, EventArgs e)
    {
        Panel1.Visible = !Panel1.Visible;
    }

    protected void CarregaAreasFitlro()
    {
        var ds = consult.CarregaAreasFiltro(appSession.UserId, appSession.UserAdmin);
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
    protected void DropDownListArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListArea.SelectedIndex != 0)
        {
            GridView1.DataSource = SqlDataSourceProximasAoFimArea;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = SqlDataSourceProximasAoFim;
            GridView1.DataBind();
        }
    }
    protected void btnEnviarEmailTodos_Click(object sender, EventArgs e)
    {
        if (CheckBoxResponsavelArea.Checked == false && CheckBoxResponsavelSetor.Checked == false)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Não foi identificado nenhum destinatário.')", true);
            return;
        }

        ComponentesDesabilita();
        txtStatus.Text = "";
        imgEmail.Visible = true;
        lblEnviando.Visible = true;

        Timer1.Enabled = true;
        Thread t = new Thread(EnviarEmailPrepara);
        t.Start(0);
    }
    public void EnviarEmailPrepara(object idArea)
    {
        string statusAtual = "";

        try
        {
            //Connecta do banco para buscar lista de emails dos envolvidos
            var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);

            #region Seleciona Lista de Areas para o Loop
            ////////////////////////////
            string strSQL;

            if (Convert.ToInt32(idArea) == 0) //Envia email para todos os contratos
            {
                strSQL = "SELECT DISTINCT Areas.AreaId, Areas.Nome FROM Demandas INNER JOIN Atividades ON Demandas.AtividadeId = Atividades.AtividadeId INNER JOIN " +
                                        "Subprocessos ON Atividades.SubprocessoId = Subprocessos.SubprocessoId INNER JOIN " +
                                        "Processos ON Subprocessos.ProcessoId = Processos.ProcessoId INNER JOIN " +
                                        "Areas ON Processos.AreaId = Areas.AreaId " +
                                        "WHERE     (DATEDIFF(Day, Demandas.DataPrazo, GETDATE()) > 0) AND (Demandas.Status = 'ATRASADA') " +
                                        "ORDER BY Areas.Nome";
            }
            else //Enviar email para apenas uma área
            {
                strSQL = "SELECT DISTINCT Areas.AreaId FROM Demandas INNER JOIN Atividades ON Demandas.AtividadeId = Atividades.AtividadeId INNER JOIN " +
                                        "Subprocessos ON Atividades.SubprocessoId = Subprocessos.SubprocessoId INNER JOIN " +
                                        "Processos ON Subprocessos.ProcessoId = Processos.ProcessoId INNER JOIN " +
                                        "Areas ON Processos.AreaId = Areas.AreaId " +
                                        "WHERE     (DATEDIFF(Day, Demandas.DataPrazo, GETDATE()) > 0) AND (Demandas.Status = 'ATRASADA') AND (Processos.AreaId = " + DropDownListArea.SelectedValue + ")";
            }

            var command = new SqlCommand(strSQL, con);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            da.Fill(dt);
            /////////////////////////
            #endregion


            consult.atualizaInsereDados("UPDATE ProcessosStatus SET Executando = 1 WHERE IdProcesso = 1");

            //Adiciona ao array os emails dos envolvidos
            foreach (DataRow dr in dt.Rows)
            {

                //SeLeciona a área
                DropDownListArea.SelectedValue = dr.Field<Int32>("AreaId").ToString();

                GridView1.DataSource = SqlDataSourceProximasAoFimArea;

                GridView1.DataBind();
                UpdatePanel2.Update();

                //Com o gridview atualizado, monta o corpo do email.
                MinhaPagina tmpPage = new MinhaPagina();
                HtmlForm formulario = new HtmlForm();

                formulario.Controls.Add(GridView1);
                tmpPage.Controls.Add(formulario);

                StringWriter sw = new StringWriter();
                HtmlTextWriter textoHTML = new HtmlTextWriter(sw);

                formulario.Controls[0].RenderControl(textoHTML);

                string conteudo = GridView1.ToString();
                string ConteudoHTML = "<div><br>" + txtCabecalhoEmail.Text.Replace("\r\n", "<br/>") + "</br></div><br/>" + sw.ToString();
                ConteudoHTML = ConteudoHTML + "<div> <br/> <br>   *   *   *   </br>" +
                                        "<br> Mensagem enviada automaticamente. </br>" +
                                        "<br><a href=\"http://dms-br.comaugroup.com\">Clique aqui para acessar ao sistema.</a><br> </div>";

                //Pega emails
                ArrayList emailDestinatarios = new ArrayList();

                if (CheckBoxResponsavelArea.Checked)
                {
                    string EmailResponsavelArea;
                    EmailResponsavelArea = consult.Consulta("SELECT DISTINCT Email FROM Usuarios INNER JOIN Areas ON Areas.ResponsavelId = Usuarios.UserId WHERE Areas.AreaId = " + DropDownListArea.SelectedValue, "Email");
                    emailDestinatarios.Add(EmailResponsavelArea);
                }



                string EmailResponsavelSetor;

                if (CheckBoxResponsavelSetor.Checked)
                {
                    EmailResponsavelSetor = consult.Consulta("SELECT DISTINCT Email FROM Usuarios INNER JOIN Setores ON Usuarios.UserID = Setores.ResponsavelId "
                                                                + " INNER JOIN Areas ON Areas.SetorId = Setores.SetorId WHERE AreaId = " + DropDownListArea.SelectedValue, "Email");
                }
                else
                    EmailResponsavelSetor = "";



                EnviarEmail(emailDestinatarios, EmailResponsavelSetor, "DMS - Demandas próximas ao fim do prazo (" + DropDownListArea.SelectedItem.Text + ")", ConteudoHTML);

                statusAtual = "Email enviado com sucesso área " + DropDownListArea.SelectedItem + "..." + Environment.NewLine + statusAtual;

                consult.atualizaInsereDados("UPDATE ProcessosStatus SET Status = '" + statusAtual + "' WHERE IdProcesso = 1");

                System.Threading.Thread.Sleep(1000); //Pausa para evitar que e-mails sejam interpretados como span pelo servidor smtp.
            }


            consult.atualizaInsereDados("UPDATE ProcessosStatus SET Executando = 2 WHERE IdProcesso = 1");
            statusAtual = "* * * CONCLUÍDO * * *." + Environment.NewLine + statusAtual;
            consult.atualizaInsereDados("UPDATE ProcessosStatus SET Status = '" + statusAtual + "' WHERE IdProcesso = 1");

        }

        catch (Exception ex)
        {
            consult.atualizaInsereDados("UPDATE ProcessosStatus SET Executando = 2 WHERE IdProcesso = 1");
            statusAtual = "Falha no envio do email." + Environment.NewLine + "Descrição da falha: " + ex.Message;
            consult.atualizaInsereDados("UPDATE ProcessosStatus SET Status = '" + statusAtual + "' WHERE IdProcesso = 1");
        }

    }
    protected void EnviarEmail(ArrayList emailDestinatario, string emailUsuarioCriador, string assunto, string mensagem)
    {
        csEmail novoEmail = new csEmail();
        novoEmail.Enviar2(emailDestinatario, emailUsuarioCriador, assunto, mensagem);
    }
    private void ComponentesHabilita()
    {
        DropDownListArea.Enabled = true;
        txtCabecalhoEmail.Enabled = true;
        btnEnviarEmail.Enabled = true;
        btnEnviarEmailTodos.Enabled = true;
    }
    private void ComponentesDesabilita()
    {
        DropDownListArea.Enabled = false;
        txtCabecalhoEmail.Enabled = false;
        btnEnviarEmail.Enabled = false;
        btnEnviarEmailTodos.Enabled = false;
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {

    }
    protected void dpdDias_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListArea.SelectedIndex == 0)
            GridView1.DataSource = SqlDataSourceProximasAoFim;
        else
            GridView1.DataSource = SqlDataSourceProximasAoFimArea;

        GridView1.DataBind();
    }
}
