using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace App_Code
{
    /// <summary>
    /// Summary description for Gerar_MSG
    /// </summary>
    public class Gerar_MSG
    {

        public void GravaMSG(String Assunto, String Texto)
        {
            #region Comando para gravar no banco

            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
            var cmm = conn.CreateCommand();

            var Subject = new SqlParameter("@Subject", SqlDbType.VarChar) {Value = Assunto};
            cmm.Parameters.Add(Subject);

            var Message = new SqlParameter("@Message", SqlDbType.VarChar) {Value = Texto};
            cmm.Parameters.Add(Message);

            cmm.CommandText = "INSERT INTO InboxSGC (Subject, Message, Data) VALUES (@Subject, @Message, convert(varchar(20), getdate(), 111))";

            conn.Open();
            cmm.ExecuteNonQuery();
            conn.Close();

            #endregion

        }
    }
}
