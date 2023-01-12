using System.Web;

namespace App_Code
{
    public class cSession
    {
        //Login
        public string login
        {
            set
            {
                HttpContext.Current.Session.Add("Login_s", value);
            }

            get
            {
                return HttpContext.Current.Session["Login_s"].ToString();
            }
        }

        //LogTela
        public string LogTela
        {
            set
            {
                HttpContext.Current.Session.Add("LogTela_s", value);
            }

            get
            {
                return HttpContext.Current.Session["LogTela_s"].ToString();
            }
        }

        //Log_Atividade
        public string LogAtividade
        {
            set
            {
                HttpContext.Current.Session.Add("LogAtividade_s", value);
            }

            get
            {
                return HttpContext.Current.Session["LogAtividade_s"].ToString();
            }
        }

        //FullName
        public string FullName
        {
            set
            {
                HttpContext.Current.Session.Add("Nome_s", value);
            }

            get
            {
                return HttpContext.Current.Session["Nome_s"].ToString();
            }
        }

        //BU
        public string BU
        {
            set
            {
                HttpContext.Current.Session.Add("BU_s", value);
            }

            get
            {
                return HttpContext.Current.Session["BU_s"].ToString();
            }
        }

        //UserID
        public string UserId
        {
            set
            {
                HttpContext.Current.Session.Add("UserID_s", value);
            }
            
            get
            {
                return HttpContext.Current.Session["UserID_s"].ToString();
            }
        }

        //NivelID
        public string NivelId
        {
            set
            {
                HttpContext.Current.Session.Add("NivelID_s", value);
            }

            get
            {
                return HttpContext.Current.Session["NivelID_s"].ToString();
            }
        }
        
        //ContratoID 
        public string ContratoId
        {
            set
            {
                HttpContext.Current.Session.Add("ContratoID_s", value);
            }

            get
            {
                return HttpContext.Current.Session["ContratoID_s"].ToString();
            }
        }

        //Tipo_FOR_OU_RAG 
        public string Tipo_RAG
        {
            set
            {
                HttpContext.Current.Session.Add("Tipo_RAG", value);
            }

            get
            {
                return HttpContext.Current.Session["Tipo_RAG"].ToString();
            }
        }

        //Nome Contrato (DescContrato)
        public string DescContrato
        {
            set
            {
                HttpContext.Current.Session.Add("DescContrato_s", value);
            }

            get
            {
                return HttpContext.Current.Session["DescContrato_s"].ToString();
            }
        }

        //AnoAtual
        public string AnoAtual
        {
            set
            {
                HttpContext.Current.Session.Add("Ano_s", value);
            }

            get
            {
                return HttpContext.Current.Session["Ano_s"].ToString();
            }
        }
        
        //MesAtual
        public string MesAtual
        {
            set
            {
                HttpContext.Current.Session.Add("Mes_s", value);
            }

            get
            {
                return HttpContext.Current.Session["Mes_s"].ToString();
            }
        }

        //DiaAtual
        public string DiaAtual
        {
            set
            {
                HttpContext.Current.Session.Add("Dia_s", value);
            }

            get
            {
                return HttpContext.Current.Session["Dia_s"].ToString();
            }
        }

        //UserAdmin (S/N)
        public string UserAdmin
        {
            set
            {
                HttpContext.Current.Session.Add("UserAdmin_s", value);
            }

            get
            {
                return HttpContext.Current.Session["UserAdmin_s"].ToString();
            }
        }

        //Grupo de Usuários
        public string UserGrupoId
        {
            set
            {
                HttpContext.Current.Session.Add("GrupoId", value);
            }

            get
            {
                return HttpContext.Current.Session["GrupoId"].ToString();
            }
        }

        //UserControl (S/N)
        public string UserControl
        {
            set
            {
                HttpContext.Current.Session.Add("UserControl_s", value);
            }

            get
            {
                return HttpContext.Current.Session["UserControl_s"].ToString();
            }
        }

        //Tipo (Flash/Consuntivo)
        public string Tipo
        {
            set
            {
                HttpContext.Current.Session.Add("Tipo_s", value);
            }

            get
            {
                return HttpContext.Current.Session["Tipo_s"].ToString();
            }
        }

        //Tipo Tela que estou
        public string TELA
        {
            set
            {
                HttpContext.Current.Session.Add("TELA_s", value);
            }

            get
            {
                return HttpContext.Current.Session["TELA_s"].ToString();
            }
        }

        //Tipo Tela que estou
        public string ErrosDeSistema
        {
            set
            {
                HttpContext.Current.Session.Add("ErrosDeSistema_s", value);
            }

            get
            {
                return HttpContext.Current.Session["ErrosDeSistema_s"].ToString();
            }
        }

        //IP Usuário
        public string IP
        {
            set
            {
                HttpContext.Current.Session.Add("IP_s", value);
            }

            get
            {
                return HttpContext.Current.Session["IP_s"].ToString();
            }
        }

        //AddCalendar
        public string AddCalendar
        {
            set
            {
                HttpContext.Current.Session.Add("AddCalendar_s", value);
            }

            get
            {
                return HttpContext.Current.Session["AddCalendar_s"].ToString();
            }
        }
        
        //Último Mês Consuntivado ou em Flash
        public string Mes_Pub
        {
            set
            {
                HttpContext.Current.Session.Add("Mes_Pub_s", value);
            }

            get
            {
                return HttpContext.Current.Session["Mes_Pub_s"].ToString();
            }
        }
       
        //Último Ano Consuntivado ou em Flash
        public string Ano_Pub
        {
            set
            {
                HttpContext.Current.Session.Add("Ano_Pub_s", value);
            }

            get
            {
                return HttpContext.Current.Session["Ano_Pub_s"].ToString();
            }
        }

        //Mês em fechamento
        public string MaxMes_Consuntivo
        {
            set
            {
                HttpContext.Current.Session.Add("MesF_s", value);
            }

            get
            {
                return HttpContext.Current.Session["MesF_s"].ToString();
            }
        }

        public string MaxMes_Flash
        {
            set
            {
                HttpContext.Current.Session.Add("MesF_s", value);
            }

            get
            {
                return HttpContext.Current.Session["MesF_s"].ToString();
            }
        }
       
        //Para gravar string de SQL para identificar erro!
        public string SqlString
        {
            set
            {
                HttpContext.Current.Session.Add("SqlString_s", value);
            }

            get
            {
                return HttpContext.Current.Session["SqlString_s"].ToString();
            }
        }

        //Flag SideMenu ON/OFF
        public string SideMenu_OnOff
        {
            set
            {
                HttpContext.Current.Session.Add("Flag_s", value);
            }

            get
            {
                return HttpContext.Current.Session["Flag_s"].ToString();
            }
        }

        //Flag Alterações 'S' ou 'N'
        public string Alter
        {
            set
            {
                HttpContext.Current.Session.Add("Alter_s", value);
            }

            get
            {
                return HttpContext.Current.Session["Alter_s"].ToString();
            }
        }

        //Flag Alterações 'S' ou 'N'
        public string GetPagina
        {
            set
            {
                HttpContext.Current.Session.Add("_GetPagina", value);
            }

            get
            {
                return HttpContext.Current.Session["_GetPagina"].ToString();
            }
        }

        public string IdPlanoAcao
        {
            set
            {
                HttpContext.Current.Session.Add("_IdPlanoAcao", value);
            }

            get
            {
                return HttpContext.Current.Session["_IdPlanoAcao"].ToString();
            }
        }

        public string IdAcao
        {
            set
            {
                HttpContext.Current.Session.Add("_IdAcao", value);
            }

            get
            {
                return HttpContext.Current.Session["_IdAcao"].ToString();
            }
        }

        public string AcaoEmEdicao
        {
            set
            {
                HttpContext.Current.Session.Add("_AcaoEmEdicao", value);
            }

            get
            {
                return HttpContext.Current.Session["_AcaoEmEdicao"].ToString();
            }
        }

        public string FiltroAreaIndexId
        {
            get { return HttpContext.Current.Session["_Area"].ToString(); }
            set { HttpContext.Current.Session.Add("_Area", value); }
        }

        public string FiltroResponsavelIndexId
        {
            get { return HttpContext.Current.Session["_Responsavel"].ToString(); }
            set { HttpContext.Current.Session.Add("_Responsavel", value); }
        }

        public string FiltroSolicitanteIndexId
        {
            get { return HttpContext.Current.Session["_Solicitante"].ToString(); }
            set { HttpContext.Current.Session.Add("_Solicitante", value); }
        }

        public string FiltroStatusIndexId
        {
            get { return HttpContext.Current.Session["_Status"].ToString(); }
            set { HttpContext.Current.Session.Add("_Status", value); }
        }

        public string FiltroContratoIndexId
        {
            get { return HttpContext.Current.Session["_Contrato"].ToString(); }
            set { HttpContext.Current.Session.Add("_Contrato", value); }
        }

        public string FiltroOcultarCanceladasConcluidas
        {
            get { return HttpContext.Current.Session["_Ocultar"].ToString(); }
            set { HttpContext.Current.Session.Add("_Ocultar", value); }
        }

        public string FiltroOrdenarPor
        {
            get { return HttpContext.Current.Session["_OrdenarPor"].ToString(); }
            set { HttpContext.Current.Session.Add("_OrdenarPor", value); }
        }

    }
}

