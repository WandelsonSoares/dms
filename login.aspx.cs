using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class _Default : System.Web.UI.Page
{
    cSession appSession = new cSession();

    protected void Page_Load(object sender, EventArgs e)
    {
        appSession.GetPagina = Request.Url.LocalPath.ToString();      

        if (!IsPostBack)
        {

            #region Fazendo a leitura de Cookies


            if (Request.Cookies["CMB"] != null)
            {
                if (Request.Cookies["CMB"]["login"] != null)
                { //Lendo Cookie
                    txtLogin.Text = Request.Cookies["CMB"]["login"];
                    txtSenha.Focus();
                }
                else
                {
                    txtLogin.Focus();
                }
            }
            else
            {
                txtLogin.Focus();
                CheckBox1.Enabled = false;
            }


            #endregion

            #region  Label que informa Logoff por time.out

            if (Session["logoff_erro"] != null)
            {                
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('" + Session["logoff_erro"] + "')", true);
            }

            if (Session["logoff"] != null)
            {                
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('" + Session["logoff"] + "')", true);
            }

            if (Request.QueryString["ErrLogin"] != null)
            {                    
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "clientScript", "alert('" + Session["ErroLogin"] + "')", true);
                txtSenha.Focus();     
            }
            Session.Clear();
            #endregion

            //Coleta IP
            appSession.IP = Request.UserHostAddress;
        }

    }

    public String CurrentLogin
    {
        get
        {
            return txtLogin.Text;
        }
    }
    public String CurrentSenha
    {
        get
        {
            return txtSenha.Text;
        }
    }
    public String CurrentDomain
    {
        get
        {
            return dpd_Domains.SelectedValue;
        }
    }

    protected void txtLogin_TextChanged(object sender, EventArgs e)
    {
        txtLogin.Text = txtLogin.Text.ToLower();
        if (Request.Cookies["CMB"] != null)
        {
            if (txtLogin.Text != Request.Cookies["CMB"]["login"])
            {
                Response.Cookies["CMB"]["login"] = CheckBox1.Checked ? txtLogin.Text : null;
            }
        }
        else
        {
            if (CheckBox1.Checked)
            {
                HttpCookie myCookie1 = new HttpCookie("CMB");
                myCookie1.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie1);
                Response.Cookies["CMB"].Expires = DateTime.Now.AddDays(30d);
                Response.Cookies["CMB"]["login"] = txtLogin.Text;
            }
        }
        txtSenha.Focus();
    }
    protected void txtSenha_TextChanged(object sender, EventArgs e)
    {
        btn_login.Focus();
    }
    
}
