using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class EtapasDocumentoModeloDownload : System.Web.UI.Page
{
    Persistencia_Fast consult = new Persistencia_Fast();
    cSession appSession = new cSession();
    _Usuario usuario = new _Usuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            usuario.LogIsert(appSession.FullName, "Demandas", "Realizou download de modelo de documento.", appSession.IP);

            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                string CaminhoDocumento = consult.Consulta("SELECT DocumentoModeloCaminho FROM DemandasEtapas WHERE DemandaEtapaId = " + Request.QueryString["id"], "DocumentoModeloCaminho");

                if (CaminhoDocumento != "")
                    Download(CaminhoDocumento, true);
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
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('" + ex.Message + ".')", true);
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