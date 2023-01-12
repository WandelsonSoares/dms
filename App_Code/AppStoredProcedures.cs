using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace App_Code
{

    public class AppStoredProcedures
    {
        cSession appSession = new cSession();

        public void ExecutaSP_ImportaFuncionarios(string FileName)
        {
            ExecutaSP_RenamePlanilhaFuncionarios(FileName);

            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
            var cmm = new SqlCommand("Stp_ImportaFuncionarios", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60000
            };     

            conn.Open();

            cmm.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();
        }

        public void ExecutaSP_ImportaFuncionariosPlansDelete ()
        {
            ///<summary> Deleta a planilha temporária do servidor utilizada para consulta de versão. </summary>

            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
            var cmm = new SqlCommand("Stp_ImportaFuncionariosPlansDelete", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60000
            };  

            conn.Open();

            cmm.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();
        }

        public void ExecutaSP_RenamePlanilhaFuncionarios(string FileName)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
            var cmm = new SqlCommand("sp_ReplaceFileOrDirNames", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60000
            };

            var pathToObject = new SqlParameter("@pathToObject", SqlDbType.VarChar, 50) { Value = "C:\\SGC_NET\\V1\\Import\\Funcionarios" };
            cmm.Parameters.Add(pathToObject);

            var oldName = new SqlParameter("@oldName", SqlDbType.VarChar, 50) { Value = FileName };
            cmm.Parameters.Add(oldName);

            var newName = new SqlParameter("@newName", SqlDbType.VarChar, 50) { Value = "Funcionarios.xls" };
            cmm.Parameters.Add(newName);
            
            conn.Open();

            cmm.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();
        }

        public void ExecutaSP_Relatorio04(DateTime DataInicio, DateTime DataFim, int UserId)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
            var cmm = new SqlCommand("sp_relatorio04", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60000
            };

            var dataInicio = new SqlParameter("@DataInicio", SqlDbType.Date) { Value = DataInicio };
            cmm.Parameters.Add(dataInicio);

            var dataFim = new SqlParameter("@DataFim", SqlDbType.Date) { Value = DataFim };
            cmm.Parameters.Add(dataFim);

            var userId = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            cmm.Parameters.Add(userId);

            conn.Open();

            cmm.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();


        }

        public void ExecutaSP_Relatorio05(DateTime DataInicio, DateTime DataFim, int UserId)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
            var cmm = new SqlCommand("sp_relatorio05", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60000
            };

            var dataInicio = new SqlParameter("@DataInicio", SqlDbType.Date) { Value = DataInicio };
            cmm.Parameters.Add(dataInicio);

            var dataFim = new SqlParameter("@DataFim", SqlDbType.Date) { Value = DataFim };
            cmm.Parameters.Add(dataFim);

            var userId = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            cmm.Parameters.Add(userId);

            conn.Open();

            cmm.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();


        }

        public void ExecutaSP_Grafico01(DateTime DataInicio, DateTime DataFim, int UserId)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
            var cmm = new SqlCommand("sp_grafico01", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60000
            };

            var dataInicio = new SqlParameter("@DataInicio", SqlDbType.Date) { Value = DataInicio };
            cmm.Parameters.Add(dataInicio);

            var dataFim = new SqlParameter("@DataFim", SqlDbType.Date) { Value = DataFim };
            cmm.Parameters.Add(dataFim);

            var userId = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            cmm.Parameters.Add(userId);

            conn.Open();

            cmm.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();


        }

        public void ExecutaSP_Grafico03(int Mes, int Ano, int UserId)
        {

            try
            {
                var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
                var cmm = new SqlCommand("sp_grafico03", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 60000
                };

                var mes = new SqlParameter("@Mes", SqlDbType.Int) { Value = Mes };
                cmm.Parameters.Add(mes);

                var ano = new SqlParameter("@Ano", SqlDbType.Int) { Value = Ano };
                cmm.Parameters.Add(ano);

                var userId = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
                cmm.Parameters.Add(userId);

                conn.Open();

                cmm.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();
            }
            catch
            {

            }


        }

        public void ExecutaSP_DashboardTabela01(int UserId)
        {

            try
            {
                var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
                var cmm = new SqlCommand("sp_dashboardTabela01", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 60000
                };

                var userId = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
                cmm.Parameters.Add(userId);

                conn.Open();

                cmm.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();
            }
            catch
            {

            }


        }
    }
}
