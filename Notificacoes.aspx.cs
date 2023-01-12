using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;
using System.Configuration;

public partial class Notificacoes : System.Web.UI.Page
{
    cSession appSession = new cSession();
    readonly String strConn = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
    Persistencia_Fast consult = new Persistencia_Fast();
        
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregaNotificacoes();
            consult.atualizaInsereDados("UPDATE Notificacoes SET Lida = 1 WHERE DestinatarioId = " + appSession.UserId);
        }
    }

    private void CarregaNotificacoes()
    {

        string sql = "SELECT TOP 50 CONVERT(varchar(3),DATEPART(DAY,DataHora)) + '/' + " +
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

}