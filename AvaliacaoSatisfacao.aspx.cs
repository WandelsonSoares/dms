using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class AvaliacaoSatisfacao : System.Web.UI.Page
{
    Persistencia_Fast consult = new Persistencia_Fast();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["op"] == "ed")
                btnConcluir.Enabled = true;
        }
    }
    protected void btnConcluir_Click(object sender, EventArgs e)
    {
        string Questao1Nota = RadioButtonList1.SelectedValue;
        string Questao2Nota = RadioButtonList2.SelectedValue;
        string Questao3Nota = RadioButtonList3.SelectedValue;
        string Questao4Nota = RadioButtonList4.SelectedValue;

        if (Questao1Nota == "" || Questao2Nota == "" || Questao3Nota == "" || Questao4Nota == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Responda à todas as questões.')", true);
            return;
        }
        else
        {
            consult.atualizaInsereDados("UPDATE AvaliacoesSatisfacaoQuestoesRespostas SET Nota = " + Questao1Nota + " WHERE AvaliacaoId = " + Request.QueryString["id"] + " AND Ordem = 1");
            consult.atualizaInsereDados("UPDATE AvaliacoesSatisfacaoQuestoesRespostas SET Nota = " + Questao2Nota + " WHERE AvaliacaoId = " + Request.QueryString["id"] + " AND Ordem = 2");
            consult.atualizaInsereDados("UPDATE AvaliacoesSatisfacaoQuestoesRespostas SET Nota = " + Questao3Nota + " WHERE AvaliacaoId = " + Request.QueryString["id"] + " AND Ordem = 3");
            consult.atualizaInsereDados("UPDATE AvaliacoesSatisfacaoQuestoesRespostas SET Nota = " + Questao4Nota + " WHERE AvaliacaoId = " + Request.QueryString["id"] + " AND Ordem = 4");

            List<double> notas = new List<double> { Convert.ToDouble(Questao1Nota), Convert.ToDouble(Questao2Nota), Convert.ToDouble(Questao3Nota), Convert.ToDouble(Questao4Nota)};
            double notaMedia = notas.Average();

            consult.atualizaInsereDados("UPDATE AvaliacoesSatisfacao SET Status = 'RESPONDIDA', DataEncerramento = '" + DateTime.Today.ToString("yyyy-MM-dd") + 
                                        "', NotaGeral = " + notaMedia.ToString().Replace(",",".") + ", ComentarioAvaliador = '" + txtComentarioAvaliador.Text.Replace("'","") + "' " +
                                        " WHERE AvaliacaoId = " + Request.QueryString["id"]);

            
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('Avaliação registrada. Obrigado pela participação.')", true);
        }
            
    }
}