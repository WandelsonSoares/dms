using System;
using App_Code;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Email;

public partial class ActionPlan : System.Web.UI.Page
{

    readonly Persistencia_Fast consult = new Persistencia_Fast();


    protected void Page_Load(object sender, EventArgs e)
    {
    }



    protected void btnEnviarEmail_Click(object sender, EventArgs e)
    {
        this.lblStatus.Text = "Enviando mensagem...";
        csEmail MensagemEletronica = new csEmail();
        
        try
        {
            MensagemEletronica.Enviar("wandelson.soares@comau.com", "wandelson.soares@comau.com", "Novo DMS", "Teste de envio de e-mail.");
            
            this.lblStatus.Text = "Mensagem enviada com sucesso.";
        }
        catch
        {
            this.lblStatus.Text = "Falha no envio da mensagem.";
        }

    }
}
