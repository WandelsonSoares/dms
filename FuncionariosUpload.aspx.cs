using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class FuncionariosUpload : System.Web.UI.Page
{
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            usuario.LogIsert(appSession.FullName, "Funcionários - Upload", "Acessou tela de upload de funcionários.", appSession.IP);
        }
    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        AppStoredProcedures exc = new AppStoredProcedures();

        try
        {
            string extensao = "";

            if (FileUpload1.PostedFile != null)
            {
                //verificamos a extensão através dos últimos 5 caracteres
                extensao = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.Length - 4).ToLower();

                string diretorio = "\\\\ubrbet01sqrp010\\Import\\Funcionarios\\" + FileUpload1.FileName;

                if ((extensao != ".xls"))
                {
                    Response.Write("<script>window.alert('Erro no Upload: Extensão inválida, só é permitida .xls!');</script>");
                    return;
                }

                FileUpload1.PostedFile.SaveAs(diretorio);
            }
        }
        catch
        {
            exc.ExecutaSP_ImportaFuncionariosPlansDelete(); //Exclui arquivos do diretório temporário no servidor
            usuario.LogIsert(appSession.FullName, "Funcionários - Upload", "Recebeu mensagem de falha no upload da planilha de importação.", appSession.IP);
            Response.Write("<script>window.alert('ERRO: Falha no upload. Certifique-se de que não exista espaço no nome do arquivo.);</script>");

            return;
        }

        try
        {
            //Aqui chama stp de importação da planilha
            exc.ExecutaSP_ImportaFuncionarios(FileUpload1.FileName);
            usuario.LogIsert(appSession.FullName, "Funcionários - Upload", "Importou planilha de funcionários com sucesso.", appSession.IP);
            Response.Write("<script>window.alert('Arquivo importado com sucesso.');</script>");
        }
        catch (ExecutionEngineException ex)
        {

            exc.ExecutaSP_ImportaFuncionariosPlansDelete(); //Exclui arquivos do diretório temporário no servidor
            usuario.LogIsert(appSession.FullName, "Funcionários - Upload", "Recebeu mensagem de falha na importação.", appSession.IP);
            Response.Write("<script>window.alert('Falha ao importar a planilha. " + ex.Message + "');</script>");
        }
    }
}