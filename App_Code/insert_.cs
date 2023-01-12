using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace App_Code
{
    public class insert_
    {

        private readonly cSession SgcSess = new cSession();

        SqlConnection conn;
        readonly String strConn = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
        public void InsertGrupoContratos(string NomeGrupo, string LoginAutor, string BU)
        {
            var Consulta = new Persistencia_Fast();
        
            //Verifica se Já existe esse Grupo Cadastrado
            string VCompara = Consulta.Consulta("Select DescContract From Contrato Where DescContract = '" + NomeGrupo.ToUpper() + "'", "DescContract");

            if (VCompara != NomeGrupo)
            {
                conn = new SqlConnection(strConn);
                conn.Open();

                //Obtendo um ID Min -1 da Tabela contrato
                var MinID = new Persistencia_Fast();
                string ContratoID = MinID.Consulta("SELECT MIN(ContratoID) - 1 AS MinID FROM Contrato", "MinID");

                //Atenção 2 Inserts Concatenados na Query.
                var cmd = new SqlCommand("INSERT INTO Contrato (ContratoID, DescContract, BU) VALUES (" + ContratoID + ", '(*) " + NomeGrupo.ToUpper() + "', '"+BU+"') INSERT INTO Contrato_Grupo_Resultado (N_grupo, Desc_Grupo, Login_Create, ContratoID) VALUES (" + ContratoID + ",'(*) " + NomeGrupo.ToUpper() + "','" + LoginAutor.ToUpper() + "',0)", conn);
                cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

                conn.Close();
                conn.Dispose(); //Destroi a conexão
            }
        }
        public void InsertContratosGrupo(string ContratoID, string N_Grupo, string LoginAutor)
        {            
            conn = new SqlConnection(strConn);
            conn.Open();
        
            var cmd = new SqlCommand(@"INSERT INTO Contrato_Grupo_Resultado
               (N_grupo,
                Login_Create,
                ContratoID) 
                VALUES 
               ("+N_Grupo+", '"+LoginAutor+"', "+ContratoID+")", conn);
        
            cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            conn.Close();
            conn.Dispose(); //Destroi a conexão        
        }    

        //public void InsertPA_PAI(string ContratoID, string Descricao, string Chances, string Mes_inicio, string Ano_inicio, string Mes_fim, string Ano_fim, string Responsavel, string TipoAcao, string Nivel)
        //{
        //    conn = new SqlConnection(strConn);
        //    conn.Open();
                    
        //    var cmd = new SqlCommand("INSERT INTO Forecast_PA_Lanc (ContratoID, Descricao, Perc, Mes_ini, Ano_ini, Mes_fim, Ano_fim, Responsavel, TipoAcao, BU, Nivel)  VALUES (" + ContratoID + ", '" + Descricao + "', " + Chances + ", " + Mes_inicio + ", " + Ano_inicio + ", " + Mes_fim + ", " + Ano_fim + ", '" + Responsavel + "','" + TipoAcao + "', '"+SgcSess.BU+"', "+Nivel+")", conn);

        //    cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

        //    conn.Close();
        //    conn.Dispose(); //Destroi a conexão
        //}
//        public void InsertPA_Acoes(string ContratoID, string Descricao, string Det_Descricao, string ContaID, string TipoConta, string Mes, string Ano, string Valor, string Total, string STATUS, string PA_LANC_ID)
//        {
//            conn = new SqlConnection(strConn);
//            conn.Open();

//            var cmd = new SqlCommand(@"INSERT INTO [FORECAST_ActionPlan]
//           ([Descricao]
//           ,[Det_Descricao]
//           ,[ContaID]
//           ,[ContratoID]
//           ,[TipoConta]
//           ,[Ano]
//           ,[Mes]
//           ,[Valor]
//           ,[Total]
//           ,[STATUS]
//           ,[PA_LANC_ID]
//           ,BU)
//     VALUES			
//           ('" + Descricao+@"'
//           ,'"+Det_Descricao+@"'
//           ,"+ContaID+@"
//           ,"+ContratoID+@"
//           ,"+TipoConta+@"
//           ,"+Ano+@"
//           ,"+Mes+@"
//           ,"+Valor+@"
//           ,"+Total+@"           
//           ,'"+STATUS+@"'
//           ,"+PA_LANC_ID+@"
//           ,'"+SgcSess.BU+"')", conn);

//            cmd.ExecuteNonQuery(); 

//            conn.Close();
//            conn.Dispose(); 
//        }
        
        public void Insert_Organico(string ContratoID, string Unid, string Mes, string Ano, string Tipo, string FlagPub)
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand(@"INSERT INTO [Organico]
           ([ContratoID]
           ,[Unid]
           ,[Mes]
           ,[Ano]
           ,[Tipo]
           ,[FlagPub]
           ,BU)
            
            VALUES
           (" + ContratoID + "," + Unid + "," + Mes + "," + Ano + ",'" + Tipo + "','" + FlagPub + "', '"+SgcSess.BU+"')", conn);

            cmd.ExecuteNonQuery(); 

            conn.Close();
            conn.Dispose(); 
        }

        public void Insert_HST_Contratos(string Contratoid, string Pleito, string dataAbertura, string anoBase, string PrevisaoConclusao, string valor, string tipo, string impacto, string responsaveis, string status, string observacoes)
        {
            _linQ_ContratosHST Insert_HST = new _linQ_ContratosHST(); 
            Insert_HST.stp_Contrato_History_Negociacoes_Insert(Convert.ToInt32(Contratoid), Pleito, dataAbertura, anoBase, PrevisaoConclusao, valor, tipo, impacto, responsaveis, status, observacoes, SgcSess.FullName);       
        }
        public void Update_HST_Contratos(string ID, string Pleito, string dataAbertura, string anoBase, string PrevisaoConclusao, string valor, string tipo, string impacto, string responsaveis, string status, string observacoes)
        {
            _linQ_ContratosHST Update_HST = new _linQ_ContratosHST();
            Update_HST.stp_Contrato_History_Negociacoes_Update(Convert.ToInt32(ID), Pleito, dataAbertura, anoBase, PrevisaoConclusao, valor, tipo, impacto, responsaveis, status, observacoes, SgcSess.FullName);
        }
    
    }
}



