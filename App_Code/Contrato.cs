using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace App_Code
{
    public class Contrato
    {   
    
        string _retorno, _sql;       

        readonly cSession _sgcSess = new cSession();

        SqlConnection _conn;
        readonly String _strConn = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;

        public void SpConsultaContratoInformacoes(int contratoID, ref ArrayList consultaContratos)
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
            var cmm = new SqlCommand("SP_SelectContrato", connection) {CommandType = CommandType.StoredProcedure};

            //Adiciona os parâmetros da storedprocedure
            var contratoid = new SqlParameter("@ContratoID", SqlDbType.Int) {Value = contratoID};
            cmm.Parameters.Add(contratoid);

            connection.Open();

            var reader = cmm.ExecuteReader();

            if (reader.Read())
            {
                //0
                consultaContratos.Add(reader["ValorMensal"].ToString());
                //1
                consultaContratos.Add(reader["ISSQN"].ToString());
                //2
                consultaContratos.Add(reader["PIS_COFINS"].ToString());
                //3
                consultaContratos.Add(reader["ICMS"].ToString());
                //4
                consultaContratos.Add(reader["IPI"].ToString());
                //5
                consultaContratos.Add(reader["NucleoID"].ToString());
                //6
                consultaContratos.Add(reader["GrupoContractID"].ToString());
                //7
                consultaContratos.Add(reader["SeguimentoID"].ToString());
                //8
                consultaContratos.Add(reader["TipoVenda"].ToString());
                //9
                consultaContratos.Add(reader["RDNID"].ToString());
                //10
                consultaContratos.Add(reader["RCNID"].ToString());
                //11
                consultaContratos.Add(reader["Filial"].ToString());
                //12
                consultaContratos.Add(reader["DescContract"].ToString());
                //13
                consultaContratos.Add(reader["Regional"].ToString());
                //14
                consultaContratos.Add(reader["ProdutoPrincipal"].ToString());
                //15
                consultaContratos.Add(reader["Razao_Social"].ToString());
                //16
                consultaContratos.Add(reader["CNPJ"].ToString());
                //17
                consultaContratos.Add(reader["IE"].ToString());
                //18
                consultaContratos.Add(reader["Contato"].ToString());
                //19
                consultaContratos.Add(reader["Telefone_Contato"].ToString());
                //20
                consultaContratos.Add(reader["Email_Contato"].ToString());
                //21
                consultaContratos.Add(reader["Endereco"].ToString());
                //22
                consultaContratos.Add(reader["Numero"].ToString());
                //23
                consultaContratos.Add(reader["Complemento"].ToString());
                //24
                consultaContratos.Add(reader["Bairro"].ToString());
                //25
                consultaContratos.Add(reader["Cidade"].ToString());
                //26
                consultaContratos.Add(reader["UF"].ToString());
                //27
                consultaContratos.Add(reader["Pais"].ToString());
                //28
                consultaContratos.Add(reader["CEP"].ToString());
                //29
                consultaContratos.Add(reader["SituacaoContratual"].ToString());
                //30
                consultaContratos.Add(reader["Pendencia"].ToString());
                //31
                consultaContratos.Add(reader["DescPendencia"].ToString());
                //32
                consultaContratos.Add(reader["ResponsPendSitContrat"].ToString());
                //33
                consultaContratos.Add(reader["CodDocContratual"].ToString());
                //34
                consultaContratos.Add(reader["SituacaoArquivoCentral"].ToString());
                //35
                consultaContratos.Add(reader["ObsSituacaoArqCentral"].ToString());
                //36
                consultaContratos.Add(reader["Performance"].ToString());
                //37
                consultaContratos.Add(reader["CondicaoPagamento"].ToString());
                //38
                consultaContratos.Add(reader["BaseReajuste"].ToString());
                //39
                consultaContratos.Add(reader["ClausulaReajuste"].ToString());
                //40
                consultaContratos.Add(reader["SituacaoFilial"].ToString());
                //41
                consultaContratos.Add(reader["EnderecoFilial"].ToString());
                //42
                consultaContratos.Add(reader["PendenciaAberturaFilial"].ToString());
                //43
                consultaContratos.Add(reader["ResponsavelAberturaFilial"].ToString());
                //44
                consultaContratos.Add(reader["DataInicioAtividades"].ToString());
                //45
                consultaContratos.Add(reader["DataAssinaturaContrato"].ToString());
                //46
                consultaContratos.Add(reader["DataRescisao"].ToString());
                //47
                consultaContratos.Add(reader["DataVencimentoContrato"].ToString());
                //48
                consultaContratos.Add(reader["DataProximoReajuste"].ToString());
                //49
                consultaContratos.Add(reader["DataLimiteAberturaFilial"].ToString());
                //50
                consultaContratos.Add(reader["Cod_PCP"].ToString());
                //51
                consultaContratos.Add(reader["Class"].ToString());

            }
            connection.Close();
            connection.Dispose();    

        }

        public void SpUpdateContratos(int contratoID, string descContract, int nucleoID, int grupoContractID, int regional, int produtoPrincipal, int seguimentoID, int tipoVenda, int codPcp, string razaoSocial, string cnpj, string IE, string contato, string telefoneContato, string Email_Contato, string Endereco, string Numero, string Complemento, string Bairro, string Cidade, string UF, string Pais, string CEP, int SituacaoContratual, int Pendencia, string DescPendencia, string ResponsPendSitContrat, string CodDocContratual, string DataInicioAtividades, string DataAssinaturaContrato, string DataRescisao, string DataVencimentoContrato, string SituacaoArquivoCentral, string ObsSituacaoArqCentral, int Performance, string ValorMensal, string ISSQN, string PIS_COFINS, string ICMS, string IPI, string CondicaoPagamento, string DataProximoReajuste, int BaseReajuste, string ClausulaReajuste, string classif)
        {
            var atualizaContrato = new Persistencia_Fast();

            atualizaContrato.atualizaInsereDados(@"

                UPDATE [Contrato]
                   SET 
                       [DescContract] = '" + descContract + @"'
                      ,[NucleoID] = " + nucleoID + @"
                      ,[GrupoContractID] = " + grupoContractID + @"
                      ,[Regional] = " + regional + @"
                      ,[ProdutoPrincipal] = " + produtoPrincipal + @"
                      ,[SeguimentoID] = " + seguimentoID + @"
                      ,[TipoVenda] = " + tipoVenda + @"
                      ,[Cod_PCP] = " + codPcp + @"
                      ,[Razao_Social] = '" + razaoSocial + @"'
                      ,[CNPJ] = '" + cnpj + @"'
                      ,[IE] = '" + IE + @"'
                      ,[Contato] = '" + contato + @"'
                      ,[Telefone_Contato] = '" + telefoneContato + @"'
                      ,[Email_Contato] = '" + Email_Contato + @"'
                      ,[Endereco] = '" + Endereco + @"'
                      ,[Numero] = '" + Numero + @"'
                      ,[Complemento] = '" + Complemento + @"'
                      ,[Bairro] = '" + Bairro + @"'
                      ,[Cidade] = '" + Cidade + @"'
                      ,[UF] = '" + UF + @"'
                      ,[Pais] = '" + Pais + @"'
                      ,[CEP] = '" + CEP + @"'
                      ,[SituacaoContratual] = " + SituacaoContratual + @"
                      ,[Pendencia] = " + Pendencia + @"
                      ,[DescPendencia] = '" + DescPendencia + @"'
                      ,[ResponsPendSitContrat] = '" + ResponsPendSitContrat + @"'
                      ,[CodDocContratual] = '" + CodDocContratual + @"'
                      
                      ,[DataInicioAtividades] = '" +DataInicioAtividades + @"'
                      ,[DataAssinaturaContrato] = '" + DataAssinaturaContrato + @"'
                      ,[DataRescisao] = '" + DataRescisao + @"'
                      ,[DataVencimentoContrato] = '"+ DataVencimentoContrato + @"'
                      
                      ,[SituacaoArquivoCentral] = '" + SituacaoArquivoCentral + @"'
                      ,[ObsSituacaoArqCentral] = '" + ObsSituacaoArqCentral + @"'
                      ,[Performance] = " + Performance + @"

                      ,[ValorMensal] = " + ValorMensal.Replace(".", "").Replace(",", ".") + @"
                      ,[ISSQN] = " + ISSQN.Replace(".", "").Replace(",", ".") + @"
                      ,[PIS_COFINS] = " + PIS_COFINS.Replace(".", "").Replace(",", ".") + @"
                      ,[ICMS] = " + ICMS.Replace(".", "").Replace(",", ".") + @"
                      ,[IPI] = " + IPI.Replace(".", "").Replace(",", ".") + @"

                      ,[CondicaoPagamento] = '" + CondicaoPagamento + @"'
                      
                      ,[DataProximoReajuste] = '" + DataProximoReajuste + @"'

                      ,[BaseReajuste] = " + BaseReajuste + @"
                      ,[ClausulaReajuste] = '" + ClausulaReajuste + @"'                  
                      ,[BU] = '" + _sgcSess.BU + @"' 
                      ,[Class] = '" + classif + @"' 
                 
                    WHERE ContratoID = " + contratoID);
        
        }

        public string SpConsultaRapidaContrato(int contratoID, string colunaConsultada)
        {
            var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
            var cmm = new SqlCommand("SP_ConsultaFASTContrato", sqlConnection)
                          {CommandType = CommandType.StoredProcedure};

            //Adiciona os parâmetros da storedprocedure
            var contratoid = new SqlParameter("@ContratoID", SqlDbType.Int) {Value = contratoID};
            cmm.Parameters.Add(contratoid);

            sqlConnection.Open();

            SqlDataReader reader = cmm.ExecuteReader();

            _retorno = reader.Read() ? reader["" + colunaConsultada + ""].ToString() : null;

            sqlConnection.Close();
            sqlConnection.Dispose(); //Destroi a conexão

            return _retorno;
        }

        public void InsertContratoID(int contratoID, string tabela)
        {
            _conn = new SqlConnection(_strConn);
            _conn.Open();

            _sql = "INSERT INTO " + tabela + " (ContratoID) VALUES (" + contratoID + ")";

            var cmd = new SqlCommand(_sql, _conn);
            cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            _conn.Close();
            _conn.Dispose(); //Destroi a conexão

            InseriParametrosContratos(contratoID);
        }

        public string ConsultaQuantidadeContratos(string tabelaDoContrato)
        {        
            _conn = new SqlConnection(_strConn);
            _conn.Open();
            var cmd = new SqlCommand("SELECT MAX(ContratoID) as ContratoID FROM " + tabelaDoContrato + "", _conn);
            var reader = cmd.ExecuteReader();

            _retorno = reader.Read() ? reader["ContratoID"].ToString() : null;

            _conn.Close();
            _conn.Dispose(); //Destroi a conexão

            return _retorno;
        }

        public void ConsultaParametros(string contratoID, ref ArrayList consultaParametro)
        {

            _conn = new SqlConnection(_strConn);
            _conn.Open();

            var cmd = new SqlCommand("SELECT StaffNucleo, SuporteBU, EnteCentral, ResponsavelBU, StaffBrasil, StaffRegional, TipoStaff, TipoProdutivo, Comercial, SA, SESMT, gp_Fiasa, GrupoIn, GrupoOut, Interdivisional, CustoDespesa FROM Contrato_Parametros WHERE (ContratoID = " + contratoID + ")", _conn);

            var reader = cmd.ExecuteReader();

            //  cmd.ExecuteReader(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            if (reader.Read())
            {
                //0
                consultaParametro.Add(reader["Comercial"].ToString());
                //1
                consultaParametro.Add(reader["StaffRegional"].ToString());
                //2
                consultaParametro.Add(reader["StaffNucleo"].ToString());
                //3
                consultaParametro.Add(reader["StaffBrasil"].ToString());
                //4
                consultaParametro.Add(reader["SuporteBU"].ToString());
                //5
                consultaParametro.Add(reader["ResponsavelBU"].ToString());
                //6
                consultaParametro.Add(reader["EnteCentral"].ToString());
                //7
                consultaParametro.Add(reader["TipoStaff"].ToString());
                //8
                consultaParametro.Add(reader["TipoProdutivo"].ToString());
                //9
                consultaParametro.Add(reader["SA"].ToString());
                //10
                consultaParametro.Add(reader["SESMT"].ToString());
                //11
                consultaParametro.Add(reader["GrupoIn"].ToString());
                //12
                consultaParametro.Add(reader["GrupoOut"].ToString());
                //13
                consultaParametro.Add(reader["Interdivisional"].ToString());
                //14
                consultaParametro.Add(reader["gp_Fiasa"].ToString());
                //15
                consultaParametro.Add(reader["CustoDespesa"].ToString());

            }
            else
            {
                //0
                consultaParametro.Add("n");
                //1
                consultaParametro.Add("n");
                //2
                consultaParametro.Add("n");
                //3
                consultaParametro.Add("n");
                //4
                consultaParametro.Add("n");
                //5
                consultaParametro.Add("n");
                //6
                consultaParametro.Add("n");
                //7
                consultaParametro.Add("n");
                //8
                consultaParametro.Add("n");
                //9
                consultaParametro.Add("n");
                //10
                consultaParametro.Add("n");
                //11
                consultaParametro.Add("n");
                //12
                consultaParametro.Add("n");
                //13
                consultaParametro.Add("n");
                //14
                consultaParametro.Add("n");
                //15
                consultaParametro.Add("c");

            }

            _conn.Close();
            _conn.Dispose(); //Destroi a conexão

        }

        public void MsgMetasText(string contratoID, string ano, ref ArrayList MsgJustMetas)
        {             
            _conn = new SqlConnection(_strConn);
            _conn.Open();

            var cmd = new SqlCommand("SELECT [Resp], [Assunto], [data] FROM [msg_contrato] where (Ano = " + ano + ") and msgTipo = 'Metas' and (ContratoID = " + contratoID + ")", _conn);

            var reader = cmd.ExecuteReader();            

            if (reader.Read())
            {
                //0
                MsgJustMetas.Add(reader["Resp"].ToString());
                //1
                MsgJustMetas.Add(reader["Assunto"].ToString());
                //2
                MsgJustMetas.Add(reader["data"].ToString());               

            }          

            _conn.Close();
            _conn.Dispose(); //Destroi a conexão

        }

        public void UpdateParametrosContratos(int contratoID, string staffRegional, string staffNucleo, string suporteBU, string enteCentral, string responsavelBU, string staffBrasil, string tipoStaff, string tipoProdutivo, string comercial, string sa, string sesmt, string grupo, string foraGrupo, string interdivisional, string gpFiasa, string CustoDespesa)
        {
            _conn = new SqlConnection(_strConn);
            _conn.Open();

            _sql = "UPDATE Contrato_Parametros SET StaffRegional ='" + staffRegional + "', StaffNucleo ='" + staffNucleo + "', SuporteBU ='" + suporteBU + "', EnteCentral ='" + enteCentral + "', ResponsavelBU ='" + responsavelBU + "', StaffBrasil = '" + staffBrasil + "', TipoStaff = '" + tipoStaff + "', TipoProdutivo = '" + tipoProdutivo + "', Comercial = '" + comercial + "', SA = '" + sa + "', SESMT = '" + sesmt + "', GrupoIN = '" + grupo + "', GrupoOut = '" + foraGrupo + "', Interdivisional = '" + interdivisional + "', gp_Fiasa = '" + gpFiasa + "', CustoDespesa = '" + CustoDespesa + "'   WHERE ContratoID = " + contratoID; 

            var cmd = new SqlCommand(_sql, _conn);
            cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            _conn.Close();
            _conn.Dispose(); //Destroi a conexão
        }

        public void InseriParametrosContratos(int contratoID)
        {
            _conn = new SqlConnection(_strConn);
            _conn.Open();

            _sql = "INSERT INTO Contrato_Parametros (ContratoID) VALUES (" + contratoID + ")";

            var cmd = new SqlCommand(_sql, _conn);
            cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            _conn.Close();
            _conn.Dispose(); //Destroi a conexão
        }

        public void STP_CONSULTA_METAS_CONTRATOS(string Ano, string Perc, string BU)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
            SqlCommand cmm = new SqlCommand("STP_REL_METAS", conn);
            cmm.CommandType = CommandType.StoredProcedure;
            cmm.CommandTimeout = 60000;
              

            SqlParameter userID = new SqlParameter("@UserID", SqlDbType.Int);
            userID.Value = _sgcSess.UserId;
            cmm.Parameters.Add(userID);

            SqlParameter ano = new SqlParameter("@Ano", SqlDbType.Int);
            ano.Value = Ano;
            cmm.Parameters.Add(ano);

            SqlParameter bu = new SqlParameter("@BU", SqlDbType.VarChar, 50);
            bu.Value = BU;
            cmm.Parameters.Add(bu);

            SqlParameter perc = new SqlParameter("@Perc", SqlDbType.VarChar, 50);
            perc.Value = Perc;
            cmm.Parameters.Add(perc);


            conn.Open();

            cmm.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();
        }
       
    }
}




