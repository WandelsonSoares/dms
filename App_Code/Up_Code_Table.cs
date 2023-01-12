using System;
using System.Configuration;
using System.Data.SqlClient;

namespace App_Code
{
    public class Up_Code_Table
    {
   
    
        public int UserID, NivelID, Mes, Ano, MesAtual, AnoAtual, ContratoIDAtual;
        public string TipoResultado;

        SqlConnection conn;
        readonly String strConn = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;

    
        //Atuailiza a Tabela Code_Table
        public void Atualiza_Code_Table(ref int ID, ref int userId2I, ref int nivelId2I, ref int mes2I, ref int ano2I, ref int mesAtual2I, ref int anoAtual2I, ref int contratoIdAtual2I, ref string tipoResultado2S, ref string IP)
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand("UPDATE code_table SET userId2I =" + userId2I + ", nivelId2I =" + nivelId2I + ", mes2I =" + mes2I + ", ano2I =" + ano2I + ", mesAtual2I =" + mesAtual2I + ", anoAtual2I =" + anoAtual2I + ", tipoResultado2S ='" + tipoResultado2S + "', contratoIdAtual2I =" + contratoIdAtual2I + ", IP = '" + IP + "'  WHERE (ID = " + ID + ")", conn);
            cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            conn.Close();
            conn.Dispose(); //Destroi a conexão      
        }

    


        //Consulta a Tabela Code_Table
        public void Consulta_Code_Table(ref int ID)
        {        
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand("SELECT userId2I, nivelId2I, mes2I, ano2I, mesAtual2I, anoAtual2I, tipoResultado2S, ISNULL(contratoIdAtual2I, 0) AS contratoIdAtual2I FROM code_table WHERE (ID = " + ID + ")", conn);

            var reader = cmd.ExecuteReader();

            //  cmd.ExecuteReader(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            if (reader.Read())
            {
                UserID = Convert.ToInt32(reader["" + UserID + ""]);
                NivelID = Convert.ToInt32(reader["" + NivelID + ""]);
                Mes = Convert.ToInt32(reader["" + Mes + ""]);
                Ano = Convert.ToInt32(reader["" + Ano + ""]);
                MesAtual = Convert.ToInt32(reader["" + MesAtual + ""]);
                AnoAtual = Convert.ToInt32(reader["" + AnoAtual + ""]);
                TipoResultado = reader["" + TipoResultado + ""].ToString();
                ContratoIDAtual = Convert.ToInt32(reader["" + ContratoIDAtual + ""]);
            }
            else
            {
                UserID = 0;
                NivelID = 0;
                Mes = 0;
                Ano = 0;
                MesAtual = 0;
                AnoAtual = 0;
                TipoResultado = null;
                ContratoIDAtual = 0;
            }

            conn.Close();
            conn.Dispose(); //Destroi a conexão    
        }
    



    
    }
}