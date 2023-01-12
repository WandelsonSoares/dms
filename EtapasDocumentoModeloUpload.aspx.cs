using System;
using App_Code;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using App_Code;

public partial class EtapasDocumentoModeloUpload : System.Web.UI.Page
{
    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    Permissoes permissao = new Permissoes();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            usuario.LogIsert(appSession.FullName, "Etapas - Modelo de Documentos", "Acessou tela de modelo de documentos", appSession.IP);

            if (Request.QueryString["op"] == "ed") //Editar
            {
                FileUpload1.Enabled = true;
                btnImportar.Enabled = true;
                GridView1.Columns[1].Visible = true;
            }

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

        string diretorio = "C:\\DMS\\Documentos\\Modelos\\";

        if (FileUpload1.HasFile)
        {
            try
            {
                string arq = FileUpload1.PostedFile.FileName;
                string extensao = arq.Substring(arq.Length - 5).ToLower();
                string extensao2 = arq.Substring(arq.Length - 4).ToLower();


                if ((extensao2 != ".msg" && extensao2 != ".txt" && extensao != ".xlsx" && extensao != ".docx" && extensao2 != ".jpg" && extensao2 != ".gif" && extensao2 != ".doc" && extensao2 != ".pdf" && extensao2 != ".xls"))
                {
                    Label1.Text = "Arquivo inválido. Tipos permitidos: .jpg, .xls, .xlsX, .doc, .docX, .pdf, .gif e .msg.";
                }
                else
                {
                    string NomeArquivo = FileUpload1.FileName;
                    NomeArquivo = tira_acentos(NomeArquivo).Replace(" ", "");
                    string caminho = diretorio + Request.QueryString["id"] + "-" + NomeArquivo;

                    FileUpload1.SaveAs(caminho);

                    consulta.atualizaInsereDados("UPDATE Etapas SET DocumentoModeloCaminho = '" + caminho + "' WHERE EtapaId = " + Request.QueryString["Id"]);
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
            string CaminhoDocumento = consult.Consulta("SELECT DocumentoModeloCaminho FROM Etapas WHERE EtapaId = " + Request.QueryString["id"], "DocumentoModeloCaminho");
            if (CaminhoDocumento != "")
            {
                if (Download(CaminhoDocumento, true) == true)
                {

                }
                else
                {
                    Response.Write("<script>window.alert('Arquivo não localizado no servidor.');</script>");
                }
            }
            else
            {
                Response.Write("<script>window.alert('Caminho do arquivo não localizado.');</script>");
                return;
            }

        }


        if (e.CommandName == "ExcluiDocumento")
        {


            string path = consult.Consulta("SELECT DocumentoModeloCaminho FROM Etapas WHERE EtapaId = " + Request.QueryString["id"], "DocumentoModeloCaminho");
            try
            {
                using (StreamWriter sw = File.CreateText(path)) { }
                string path2 = path;


                File.Delete(path2);

                consult.atualizaInsereDados("UPDATE Etapas SET DocumentoModeloCaminho = null WHERE EtapaId = " + Request.QueryString["id"]);
                GridView1.DataBind();
            }
            catch
            {
                Response.Write("<script>window.alert('Falha no processo.');</script>");
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

}