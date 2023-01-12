//using System;
//using System.Data;
//using System.Configuration;
//using System.Collections;
//using System.Data.SqlClient;

//namespace App_Code
//{
//    /// <summary>
//    /// Summary description for Persistencia
//    /// </summary>
//    public class Persistencia
//    {


//        SqlConnection conn;
//        string strConn = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
//        string sql;

//        // =======================================================================================================================================
//        //Tratamento de eventos da p�gina BudGet_View3.aspx relacionados ao CheckBox de aprova��es
//        /// <summary>
//        /// Verifica se j� houve aprova��es para o contrato e atualiza os n�veis de aprova��o e reprova��o.
//        /// </summary>
//        /// <param name="SessionNivelAprovacaoID">N�vel de aprova��o que ser� inserido ou atualizado no BD</param>
//        /// <param name="SessionContratoID">Serve de filtro para consultar se j� houve aprova��es para o contrato</param>
//        /// <param name="SessionAno">Serve de filtro para consultar se j� houve aprova��es para o contrato</param>
//        /// <param name="SessionReprovacao">N�vel de reprova��o que ser� inserido ou atualizado no BD</param>
//        public void saveNivelAprovacaoID(string SessionNivelAprovacaoID, string SessionContratoID, string SessionAno, string SessionReprovacao)
//        {      
//            string verificaBudget = null;
//            DataSet resultadoConsulta = new DataSet();
//            //Verificar se existe alguma aprova��o para o contrato selecionado pelo usu�io no ano informado.   
//            sql = "SELECT Aprovacao FROM ApprovalBudget WHERE (ContratoID =" + SessionContratoID + ") AND (Ano = " + SessionAno + ")";
//            resultadoConsulta = this.executaConsulta(sql);

//            if ((this.executaConsulta(sql).Tables[0].Rows.Count) > 0)//Verifica se retornou alguma linha
//                verificaBudget = resultadoConsulta.Tables[0].Rows[0]["Aprovacao"].ToString();             

//            if (verificaBudget == null) //Se n�o existir Aprova��es para o Budget Selecionado
//            {
//                sql = "INSERT INTO ApprovalBudget (ContratoID, Ano, Aprovacao, Reprovacao) VALUES ('" + SessionContratoID + "', '" + SessionAno + "', '" + SessionNivelAprovacaoID + "', '" + SessionReprovacao + "')";
//                this.atualizaInsereDados(sql);
//            }
//            else  //Se j� existir Aprova��es para o Budget Selecionado apenas atualiza o campo Aprovacao
//            {
//                sql = "UPDATE ApprovalBudget SET Aprovacao = " + SessionNivelAprovacaoID + ", Reprovacao = " + SessionReprovacao + "  WHERE (ContratoID = " + SessionContratoID + ") AND (Ano = " + SessionAno + ")";
//                this.atualizaInsereDados(sql);
//            }
//        }
//        // =======================================================================================================================================
//        // Retorna o n�vel de aprova��o referente ao contrato selecionado
//        /// <summary>
//        /// Retorna o n�vel de aprova��o e reprova��o referente ao contrato selecionado por referencia
//        /// </summary>
//        /// <param name="contratoID">Serve de filtro para consultar o n�vel de aprova��o e reprova��o o contrato</param>
//        /// <param name="anoBudget">Serve de filtro para consultar o n�vel de aprova��o e reprova��o o contrato</param>
//        /// <param name="Aprovacao">Retorna por refer�ncia o n�vel de aprova��o e reprova��o do contrato</param>
//        public void getNivelAprovacao(string contratoID, string anoBudget, ref string[] Aprovacao)
//        {
//            string NvAprovacao = "0", Reprovacao = "0";
//            DataSet resultadoConsulta = new DataSet();

//            sql = "SELECT ApprovalBudget.Aprovacao, ApprovalBudget.Reprovacao FROM Contrato INNER JOIN ApprovalBudget ON Contrato.ContratoID = ApprovalBudget.ContratoID WHERE (Contrato.ContratoID =" + contratoID + ") AND (ApprovalBudget.Ano =" + anoBudget + ")";
//            resultadoConsulta = this.executaConsulta(sql);

//            if ((resultadoConsulta.Tables[0].Rows.Count) > 0)//Verifica se retornou alguma linha
//            {
//                NvAprovacao = resultadoConsulta.Tables[0].Rows[0]["Aprovacao"].ToString();
//                Reprovacao = resultadoConsulta.Tables[0].Rows[0]["Reprovacao"].ToString();
//            }
//            Aprovacao[0] = NvAprovacao;
//            Aprovacao[1] = Reprovacao;
//        }
//        // =======================================================================================================================================    
//        /// <summary>
//        /// Busca o NivelAprovacaoID para verificar se o usu�rio tem alguma aprova��o de Budget pendente para aprovar. Caso haja dispara uma menssagem informando a pendencia
//        /// </summary>
//        /// <param name="UserID">Serve de filtro para buscar o nivel de Aprova��o do contrato</param>
//        /// <param name="NivelID"></param>
//        /// <returns></returns>
//        public DataSet msgAprovacao(string UserID, string NivelID)
//        {
//            DataSet resultadoConsulta = new DataSet();        

//            if (NivelID == "2")  // Se o usuario for RDN executa o select que retorna os contratos que o mesmo � respons�vel e que foram aprovados pelo RCN
//                sql = "SELECT Contrato.ContratoID, Contrato.DescContract, Seguimento.SegDesc, Contrato.SeguimentoID, ApprovalBudget.Aprovacao, ApprovalBudget.Ano FROM Contrato INNER JOIN Seguimento ON Contrato.SeguimentoID = Seguimento.SeguimentoID INNER JOIN ApprovalBudget ON Contrato.ContratoID = ApprovalBudget.ContratoID WHERE (Contrato.RDNID =" + UserID + ") AND (ApprovalBudget.Aprovacao = 1)";

//            else if (NivelID == "3") // Se o usu�rio for Adm.Contrato o select retorna todos os contratos que foram aprovados por RDN`s.
//                sql = "SELECT Contrato.ContratoID, Contrato.DescContract, Seguimento.SegDesc, Contrato.SeguimentoID, ApprovalBudget.Aprovacao, ApprovalBudget.Ano FROM Contrato INNER JOIN Seguimento ON Contrato.SeguimentoID = Seguimento.SeguimentoID INNER JOIN ApprovalBudget ON Contrato.ContratoID = ApprovalBudget.ContratoID WHERE ApprovalBudget.Aprovacao = 2";

//            else //Se o usu�rio for Diretoria o select retorna todos os contratos que foram aprovados por Adm.Contrato.
//                sql = "SELECT Contrato.ContratoID, Contrato.DescContract, Seguimento.SegDesc, Contrato.SeguimentoID, ApprovalBudget.Aprovacao, ApprovalBudget.Ano FROM Contrato INNER JOIN Seguimento ON Contrato.SeguimentoID = Seguimento.SeguimentoID INNER JOIN ApprovalBudget ON Contrato.ContratoID = ApprovalBudget.ContratoID WHERE ApprovalBudget.Aprovacao = 3";

//            return this.executaConsulta(sql);
//        }
//        // =======================================================================================================================================    
//        // Busca o e-mail das pessoas que precisam receber receber menssagens informando sobre Aprova��es e reprova��es
//        public string getEmail(string contratoID, string UserID, string NivelID, string aprovacao, string grupoEmail)
//        {
//            ArrayList carregaEmail = new ArrayList();
//            /*                       ===========  TABELA DE ENVIO DE E-MAILS  ===========  
//                    *********************************                    ********************************* 
//                    *  RCN  |  RDN  |  ADM  |  DIR  *                    *  RCN  |  RDN  |  ADM  |  DIR  *   
//        *********************************************        *********************************************
//                    |   X   |  RCN  |  RCN  |  RCN  |                    |   X   |  RCN  |  RCN  |  RCN  |   
//        Envios qdo  ---------------------------------        Envios qdo  ---------------------------------
//        Aprovado    |  RDN  |   X   |  RDN  |  RDN  |        Reprovado   |   X   |   X   |  RDN  |  RDN  |
//                    ---------------------------------                    ---------------------------------
//                    |  ADM  |  ADM  |   x   |  ADM  |                    |   X   |  ADM  |   x   |  ADM  |    
//        *********************************************        **********************************************/

//            // ===================   Retorna o e-mail do RDN  =================== 
//            //Se o usu�rio for RCN ou AdmContrato e o evento ter sido disparado pelo btnAprovar, ou se o usu�rio for Diretor independente do bot�o Aprovar ou Reprovar
//            if (((NivelID == "1") && aprovacao == "aprovado") || (NivelID == "3") || (NivelID == "4"))
//                this.getEmailRDN(ref carregaEmail, contratoID);

//            // ===================  Retorna o e-mail do RCN  ===================
//            //   Se o usu�rio for RDN ou AdmContrato ou Diretor independente do bot�o(Aprova ou Reprova) que chamou o m�todo 
//            if ((NivelID == "2") || (NivelID == "3") || (NivelID == "4"))
//                this.getEmailRCN(ref carregaEmail, contratoID);

//            // ===================  Retorna o e-mail do AdmContrato  ===================
//            //  Se o usu�rio for RCN e o evento ter sido disparado pelo btnAprovar, ou se o usu�rio for RDN ou Diretor independente do bot�o Aprovar ou Reprovar
//            if (((NivelID == "1") && aprovacao == "aprovado") || ((NivelID == "2") || (NivelID == "4")))
//                this.getEmailAdmContrato(ref carregaEmail);

//            // ===================  Retorna o e-mail do Diretor  ===================
//            //  Se o usu�rio for Adm.Contrato e bot�o que chamou o m�todo getMail ter sido o Aprovar
//            if ((NivelID == "3") && aprovacao == "aprovado")
//                this.getEmailDiretor(ref carregaEmail);

//            foreach (string item in carregaEmail)
//            {
//                grupoEmail = grupoEmail + item;

//            }
//            return grupoEmail;
//        }
//        // =======================================================================================================================================
//        // Busca a descri��o do contrato para especificar no e-mail de referente a aprova��o e reprova��o 
//        /// <summary>
//        /// Busca a descri��o do contrato para especificar no e-mail referente a aprova��o e reprova��o 
//        /// </summary>
//        /// <param name="contratoID">Serve de filtro para buscar a especifica��o do contrato contrato</param>
//        /// <returns>Descri��o do contrato</returns>
//        public string descContrato(string contratoID)
//        {
//            string DescCont = null;
//            sql = "SELECT DescContract FROM Contrato WHERE ContratoID =" + contratoID;

//            return this.executaConsulta(sql).Tables[0].Rows[0]["DescContract"].ToString();
//        }
//        //======================================================================================================================================
//        //  Carrega o email do RCN no arrayList carregaEmail
//        public void getEmailRCN(ref ArrayList carregaEmail, string contratoID)
//        {
//            string emailUsuario = null, cmdsql2 = null;
//            cmdsql2 = "SELECT Usuario.Email FROM  Contrato INNER JOIN  Usuario ON Contrato.RCNID = Usuario.UserID WHERE (Contrato.ContratoID =" + contratoID + ")";
//            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
//            SqlCommand cmd2 = new SqlCommand(cmdsql2, conn2);
//            conn2.Open();
//            SqlDataReader reader2 = cmd2.ExecuteReader();
//            while (reader2.Read())
//            {
//                emailUsuario = reader2["Email"].ToString();     // Carrega os e-mails do usu�rio logado no sistema, seja ele RCN ou RDN
//                carregaEmail.Add(emailUsuario + ";");

//            }
//            conn2.Close();
//            conn2.Dispose();  //Destroi a conex�o        
//        }

//        //======================================================================================================================================
//        //  Carrega o email do RDN no arrayList carregaEmail
//        public void getEmailRDN(ref ArrayList carregaEmail, string contratoID)
//        {
//            string emailUsuario = null, cmdsql1 = null;
//            cmdsql1 = "SELECT Usuario.Email FROM  Contrato INNER JOIN  Usuario ON Contrato.RDNID = Usuario.UserID WHERE (Contrato.ContratoID =" + contratoID + ")";
//            SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
//            SqlCommand cmd1 = new SqlCommand(cmdsql1, conn1);
//            conn1.Open();
//            SqlDataReader reader1 = cmd1.ExecuteReader();
//            while (reader1.Read())
//            {
//                emailUsuario = reader1["Email"].ToString();     // Carrega os e-mails do usu�rio logado no sistema, seja ele RCN ou RDN
//                carregaEmail.Add(emailUsuario + ";");
//            }
//            conn1.Close();
//            conn1.Dispose();  //Destroi a conex�o    
//        }
//        //====================================================================================================================================== 
//        //  Carrega o email do AdmContrato no arrayList carregaEmail
//        public void getEmailAdmContrato(ref ArrayList carregaEmail)
//        {
//            string emailUsuario = null, cmdsql3 = null;
//            cmdsql3 = "SELECT Email From Usuarios WHERE NivelID = 3";
//            SqlConnection conn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
//            SqlCommand cmd3 = new SqlCommand(cmdsql3, conn3);
//            conn3.Open();
//            SqlDataReader reader3 = cmd3.ExecuteReader();
//            while (reader3.Read())
//            {
//                emailUsuario = reader3["Email"].ToString();
//                carregaEmail.Add(emailUsuario + ";");
//            }
//            conn3.Close();
//            conn3.Dispose();
//        }
//        //======================================================================================================================================
//        //  Carrega o email do AdmContrato no arrayList carregaEmail
//        public void getEmailDiretor(ref ArrayList carregaEmail)
//        {
//            string emailUsuario = null, cmdsql4 = null;
//            cmdsql4 = "SELECT Email From Usuarios WHERE NivelID = 4";
//            SqlConnection conn4 = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
//            SqlCommand cmd4 = new SqlCommand(cmdsql4, conn4);
//            conn4.Open();
//            SqlDataReader reader4 = cmd4.ExecuteReader();
//            while (reader4.Read())
//            {
//                emailUsuario = reader4["Email"].ToString();
//                carregaEmail.Add(emailUsuario + ";");
//            }
//            conn4.Close();
//            conn4.Dispose();
//        }
//        //======================================================================================================================================
//        // Verifica os contratos que tem menos de 90 dias para serem reajustados ou vencer, para enviar email para as pessoas envolvidas no contrato 
   
   
//        //======================================================================================================================================
//        /// <summary>
//        /// Cria e abre uma nova conex�o
//        /// </summary>
//        public void conectar()
//        {        
//            conn = new SqlConnection(strConn);
//            conn.Open();
//        }
//        //======================================================================================================================================
//        /// <summary>
//        /// Atualiza ou Insere dados no Banco de dados, de acordo com a Query que recebe como parametro com a string sql.
//        /// </summary>
//        /// <param name="sql">String com o Update ou Insert que deve ser executado</param>       
//        public void atualizaInsereDados(string sql)
//        {
//            try
//            {
//                conectar();
//                SqlCommand cmd = new SqlCommand(sql, conn);
//                cmd.ExecuteNonQuery(); //O ExecuteNonQuery � usado com os comandos Update e Insert  
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                conn.Close();
//                conn.Dispose(); //Destroi a conex�o
//            }
//        }
    
//        //======================================================================================================================================
//        /// <summary>
//        /// Realiza consultas no banco atrav�s da Query que recebe por parametro, e retorna um DataSet
//        /// </summary>
//        /// <param name="sql">String com as Querys que dever� ser executado</param>
//        /// <returns>DataSet com os dados da consulta</returns>   
//        public DataSet executaConsulta(string sql)
//        {
//            try
//            {
//                DataSet resultadoConsulta = new DataSet();
//                String strConn = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
//                conn = new SqlConnection(strConn);
//                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            
//                da.Fill(resultadoConsulta); /* Preenche os registros recuperados do BD e carrega os dados em um objeto DataSet ds. Obs: A classe SqlDataAdapter media a intera��o entre a classe DataSet e banco de dados reais.*/

//                return (resultadoConsulta);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}


