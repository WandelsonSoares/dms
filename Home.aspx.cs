using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using App_Code;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class myHome : System.Web.UI.Page
{

    readonly cSession appSession = new cSession();
    readonly Permissoes permissao = new Permissoes();
    readonly Persistencia_Fast consult = new Persistencia_Fast();
    _Usuario usuario = new _Usuario();
    AppStoredProcedures storedProcedure = new AppStoredProcedures();

    readonly String strConn = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Session["UserId"] = appSession.UserId;
            CarregaDatasIniciais();
            lbl_Fullname.Text = appSession.FullName;
            lbl_cmb.Text = appSession.login;
            lblQuanditadeProcessando.Text = CarregaDemandas("PROCESSANDO");
            lblQuantidadeAtrasada.Text = CarregaDemandas("ATRASADA");
            lblQuantidadeAguardando.Text = CarregaDemandas("AGUARDANDO");
            lblQuantidadeSatisfacao.Text = consult.Consulta("SELECT ROUND(AVG(Nota),1)*10 as Media FROM AvaliacoesSatisfacaoQuestoesRespostas", "Media") + "%";

            if (appSession.UserGrupoId == "1" || appSession.UserGrupoId == "4" || appSession.UserGrupoId == "6" || appSession.UserGrupoId == "8" || appSession.UserGrupoId == "9")
                GraficosVisiveis(false);

            CarregaNotificacoes();

            if (appSession.UserGrupoId == "2" || appSession.UserAdmin == "S")
            {
                //Notificações de Etapas Vencidas ou Com vencimento para o dia
                if (consult.Consulta("SELECT DISTINCT ExibePainelEtapas FROM UsuariosConfig WHERE UserId = " + appSession.UserId, "ExibePainelEtapas") == "True")
                    CarregaPainelNessaSemana();
                else
                    PanelEtapas.Visible = false;
            }
            else
            {
                PanelEtapas.Visible = false;
            }

            usuario.LogIsert(appSession.FullName, "Home", "Visualizou tela Home.", appSession.IP);


            CarregaMeses();
            CarregaAnos();
      
        }

        CarregaGrafico01();
        CarregaGrafico03();
        CarregaTabela1();
        
    }

    private string CarregaDemandas(string Status)
    {
        string quantidade = "0";


        if (appSession.UserGrupoId != "7" && (appSession.UserGrupoId == "3" || appSession.UserGrupoId == "2")) //Se não for administrador, mas for 'Responsável por Setor (3)' ou 'Atendente (2)'
            quantidade = consult.Consulta("SELECT     COUNT(Demandas.DemandaId) AS Quantidade FROM Processos INNER JOIN " +
                                                " Demandas INNER JOIN Subprocessos ON Demandas.SubProcessoId = Subprocessos.SubprocessoId ON Processos.ProcessoId = Subprocessos.ProcessoId " +
                                                " WHERE (Demandas.Status = '"+ Status + "') AND (Processos.AreaId IN " +
                                                "(SELECT AreaID FROM AreasUsuarios WHERE (UsuarioId = " + appSession.UserId + ")))", "Quantidade");
        else
            quantidade = consult.Consulta("SELECT COUNT(DemandaId) AS Quantidade FROM Demandas WHERE Status = '" + Status + "'", "Quantidade");

        return quantidade;

    }

    private void CarregaNotificacoes()
    {
        string sql = "SELECT TOP 5 CONVERT(varchar(3),DATEPART(DAY,DataHora)) + '/' + " +
                                  " CONVERT(varchar(3), DATENAME(MONTH, DataHora)) + ' ' + " +
        " CONVERT(varchar(2), DATEPART(HOUR, DataHora)) + ':' + " +
        " CONVERT(varchar(2), DATEPART(MINUTE, DataHora)) AS DataHora " +
        " , Assunto " +
        " , Notificacao, Lida, URL, NotificacaoId FROM Notificacoes " +
        " WHERE DestinatarioId = " + appSession.UserId + " ORDER BY NotificacaoId DESC";


        SqlDataAdapter sda = new SqlDataAdapter(sql, strConn);

        DataTable dt = new DataTable();
        sda.Fill(dt);

        Repeater1.DataSource = dt;
        Repeater1.DataBind();


    }

    private void CarregaPainelNessaSemana()
    {

        if (consult.Consulta("SELECT  COUNT (DemandasEtapas.Nome) AS Quantidade " +
                                    "FROM         DemandasEtapas INNER JOIN Demandas ON DemandasEtapas.DemandaId = Demandas.DemandaId INNER JOIN Atividades ON Demandas.AtividadeId = Atividades.AtividadeId " +
                                " WHERE     (Demandas.Status <> 'CANCELADA' AND Demandas.Status <> 'CONCLUIDA' AND DemandasEtapas.Status <> 'CONCLUIDA') " +
                                " AND (DemandasEtapas.Status = 'ATRASADA') " +
                                " AND (Demandas.ResponsavelId = " + appSession.UserId + ")", "Quantidade") == "0")
        {
            PanelEtapas.Visible = false;
        }

        else
        {
            string sql = "SELECT     Demandas.DemandaId, Atividades.Nome AS Atividade, Demandas.Detalhe, DemandasEtapas.Nome AS Etapa, DemandasEtapas.DataPrazo, DemandasEtapas.Status " +
                            "FROM         DemandasEtapas INNER JOIN Demandas ON DemandasEtapas.DemandaId = Demandas.DemandaId INNER JOIN Atividades ON Demandas.AtividadeId = Atividades.AtividadeId " +
                            " WHERE     (Demandas.Status <> 'CANCELADA' AND Demandas.Status <> 'CONCLUIDA' AND DemandasEtapas.Status <> 'CONCLUIDA') " +
                            " AND (DemandasEtapas.Status = 'ATRASADA') " +
                            " AND (Demandas.ResponsavelId = " + appSession.UserId + ") " +
                            " ORDER BY DemandasEtapas.DataPrazo";

            SqlDataAdapter sda = new SqlDataAdapter(sql, strConn);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            RepeaterEtapas.DataSource = dt;
            RepeaterEtapas.DataBind();

            PanelEtapas.Visible = true;
        }


    }

    private void GraficosVisiveis(bool valor)
    {
        PanelGrafico1.Visible = valor;
        lblGrafico1.Visible = valor;
        PanelGrafico2.Visible = valor;
        lblGrafico2.Visible = valor;
        PanelGrafico3.Visible = valor;
        Panel1.Visible = valor;
    }

    private void CarregaDatasIniciais()
    {
        txtDataInicio.Text = "1/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        txtDataFim.Text = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month).ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
    }

    private void CarregaGrafico01()
    {
        var ultimoDia = DateTime.DaysInMonth(Convert.ToInt32(DropDownListAno.SelectedValue), Convert.ToInt32(DropDownListMes.SelectedValue));
        DateTime dataInicio = Convert.ToDateTime("1/"+ DropDownListMes.SelectedValue + "/" + DropDownListAno.SelectedValue);
        DateTime dataFim = Convert.ToDateTime(ultimoDia + "/" + DropDownListMes.SelectedValue + "/" + DropDownListAno.SelectedValue);

        if (dataInicio != null && dataFim != null)
        {
            try
            {

                storedProcedure.ExecutaSP_Grafico01(dataInicio, dataFim, Convert.ToInt32(appSession.UserId));

                string sql = "SELECT Area, Valor FROM Grafico1 WHERE UserId = " + appSession.UserId + " ORDER BY Area";

                var ds = consult.DTSetConsulta(sql);
                Chart1.DataSource = ds;
                Chart1.Series["Series1"].XValueMember = "Area";
                Chart1.Series["Series1"].YValueMembers = "Valor";
                Chart1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha no carregamento do gráfico 1. " + ex.Message + "')", true);
                txtDataInicio.Focus();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Período de datas inválido.')", true);
            txtDataInicio.Focus();
        }
    }

    protected void LinkButtonPeriodoDatas4_Click(object sender, EventArgs e)
    {
        PanelPeriodo.Visible = true;
    }
    protected void btnCancelarStatus_Click(object sender, EventArgs e)
    {
        PanelPeriodo.Visible = false;
    }
    protected void btnAplicar_Click(object sender, EventArgs e)
    {
        CarregaGrafico01();
    }

    private void CarregaGrafico03()
    {
        if (DropDownListMes.Text != "" && DropDownListAno.Text != "")
        {
            try
            {
                storedProcedure.ExecutaSP_Grafico03(Convert.ToInt32(DropDownListMes.SelectedValue), Convert.ToInt32(DropDownListAno.SelectedValue), Convert.ToInt32(appSession.UserId));

                string sql = "SELECT Dia, Quantidade FROM Grafico3 WHERE UserId = " + appSession.UserId + " ORDER BY Dia";

                var ds = consult.DTSetConsulta(sql);
                Chart3.DataSource = ds;
                Chart3.Series["Series1"].XValueMember = "Dia";
                Chart3.Series["Series1"].YValueMembers = "Quantidade";
                Chart3.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha no carregamento do gráfico 3. " + ex.Message + "')", true);
                DropDownListMes.Focus();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Mes e ano selecionado inválidos.')", true);
            DropDownListMes.Focus();
        }
    }

    private void CarregaTabela1()
    {

        try
        {
            storedProcedure.ExecutaSP_DashboardTabela01(Convert.ToInt32(appSession.UserId));

            string sql = "SELECT Nome, Atendimento, Backlog, FotoNomeArquivo, Atrasado, UserId  FROM DashboardTabela01  WHERE UserId = " + appSession.UserId + " ORDER BY Nome";

            var ds = consult.DTSetConsulta(sql);

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Falha no carregamento da tabela de atendimentos. " + ex.Message + "')", true);
            DropDownListMes.Focus();
        }

    }

    private void CarregaMeses()
    {
        for (int i = 0; i <= 11; i++)
        {
            DropDownListMes.Items.Insert(i, (i + 1).ToString());
        }

        DropDownListMes.SelectedIndex = DateTime.Today.Month - 1;
    }

    private void CarregaAnos()
    {
        for (int i = 0; i < 10; i++)
        {
            DropDownListAno.Items.Insert(i, (Convert.ToInt32(DateTime.Today.Year) + i).ToString());
        }
    }

    protected void btnAplicarMesAno_Click(object sender, EventArgs e)
    {
        CarregaGrafico01();
        CarregaGrafico03();
    }
    protected void btnMesAno_Click(object sender, EventArgs e)
    {
        PanelMesAno.Visible = false;
    }
    protected void LinkButtonMesAno_Click(object sender, EventArgs e)
    {
        PanelMesAno.Visible = true;
    }
    protected void btnFecharPainelEstaSemana_Click(object sender, EventArgs e)
    {
        PanelEtapas.Visible = false;
    }
    protected void CheckBoxNaoExibirMaisEssaSemana_CheckedChanged(object sender, EventArgs e)
    {
        if (consult.Consulta("SELECT COUNT (UserId) AS Quantidade FROM UsuariosConfig WHERE UserId = " + appSession.UserId, "Quantidade") == "0")
            consult.atualizaInsereDados("INSERT INTO UsuariosConfig (UserId, ExibePainelEtapas) VALUES (" + appSession.UserId + ", 1)");

        consult.atualizaInsereDados("UPDATE UsuariosConfig SET ExibePainelEtapas = " + Convert.ToInt32(!CheckBoxNaoExibirMaisEssaSemana.Checked) + " WHERE UserId = " + appSession.UserId);
    }
}