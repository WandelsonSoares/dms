using System;
using System.Configuration;
using System.Data.SqlClient;

namespace App_Code
{
    public class delete_
    {
        private readonly cSession SgcSess = new cSession();
        SqlConnection conn;
        readonly String strConn = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;

        public void DeleteCustom(string Tabela, string WhereColuna, string valorRef)
        {     
            
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand("DELETE FROM " + Tabela + " WHERE "+ WhereColuna + " = " + valorRef, conn);
            cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            conn.Close();
            conn.Dispose(); //Destroi a conexão
    
        }
        public void DeleteOrganico(string ContratoID, string Mes, string Ano)
        {

            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand("DELETE FROM Organico WHERE Mes = " + Mes + " and Ano = " + Ano + " and ContratoID = " + ContratoID, conn);
            cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            conn.Close();
            conn.Dispose(); //Destroi a conexão

        }

        public void DelGrupContrato(string N_Grupo)
        {
            conn = new SqlConnection(strConn);
            conn.Open();


            //Atenção! A seguir são 2 Querys de DELETE concatenadas (Agem nas Tabelas Contrato e Contrato_Grupo_Resultado)

            var cmd = new SqlCommand("DELETE FROM Contrato_Grupo_Resultado WHERE (N_Grupo = " + N_Grupo + ") DELETE FROM Contrato WHERE (ContratoID = " + N_Grupo + ") and (ContratoID < 0)", conn);
            cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            conn.Close();
            conn.Dispose(); //Destroi a conexão
        }

        public void DelContratoDoGrupo(string N_Grupo, string ContratoID)
        {
            conn = new SqlConnection(strConn);
            conn.Open();        
        
            var cmd = new SqlCommand("DELETE FROM Contrato_Grupo_Resultado WHERE (ContratoID = " + ContratoID + ") and (N_Grupo = " + N_Grupo + ")", conn);
            cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            conn.Close();
            conn.Dispose(); //Destroi a conexão
        }
    }
}