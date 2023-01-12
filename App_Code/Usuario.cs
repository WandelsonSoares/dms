using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace App_Code
{
    public class _Usuario
    {
        public string _UserID, _NivelID, bloqueado, UsuarioBloqueado, UseSenha, GrupoId;
        
        readonly cSession appSession = new cSession();
        readonly Persistencia_Fast Consulta = new Persistencia_Fast();

        SqlConnection conn;
        readonly String strConn = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;        

        public void LogIsert(string nomeCompleto, string tela, string atividade, string enderecoIP)
        {
            var log = new _LinQ_UsuariosDataContext();
            log.Stp_LOG_Insert(nomeCompleto, tela, atividade, enderecoIP);
        }
    
        public void Inseri_Atualiza_Usuario(string sql)
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            conn.Close();
            conn.Dispose(); //Destroi a conexão
        }    
        public void Deleta_Usuario(string UserID)
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand("DELETE FROM [Usuario_Acesso_Area] WHERE userid = " + UserID + " DELETE From Usuarios WHERE UserID = " + UserID, conn);
            cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            conn.Close();
            conn.Dispose(); //Destroi a conexão
        }

        public void Consulta_Usuario(string sql, ref string Nome, ref string Setor, ref string Login, ref string Senha, ref string NivelID, ref string Email, ref string Telefone, ref string Bloqueado, ref string Localidade)
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand(sql, conn);

            var reader = cmd.ExecuteReader();

            //  cmd.ExecuteReader(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            if (reader.Read())
            {
                Nome = reader["Nome"].ToString();
                Setor = reader["Setor"].ToString();
                Login = reader["Login"].ToString();
                Senha = reader["Senha"].ToString();            
                NivelID = reader["NivelID"].ToString();
                Email = reader["Email"].ToString();
                Telefone = reader["Telefone"].ToString();
                Bloqueado = reader["Bloqueado"].ToString();
                Localidade = reader["Localidade"].ToString();
            }
        
            conn.Close();
            conn.Dispose(); //Destroi a conexão

        }

        //Metodo que mantem o ususário com os dados sempre atualizados.
        public string ConsultUserBlock(string cmb)
        {
            string cmbVerify = Consulta.Consulta("Select login From Usuarios where Login ='" + cmb + "'", "login");

            if (cmbVerify == null)
            {
                UsuarioBloqueado = "NaoCadastrado"; //Usuário não cadastrado
            }
            else
            {
                UsuarioBloqueado = Consulta.Consulta("Select bloqueado From Usuarios where Login ='" + cmb + "'", "bloqueado");
            }

            if (UsuarioBloqueado == "False")
            {
                UseSenha = Consulta.Consulta("SELECT LEN(Senha) as VL From Usuarios where Login ='" + cmb + "'", "VL");

                if (UseSenha != "" && UseSenha != null)
                {
                    if (Convert.ToInt32(UseSenha) > 1)
                        UsuarioBloqueado = "SenhaInterna";
                }
            }

            return UsuarioBloqueado;
        }

        public void Consulta_UserID(string CMB)
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand("Select UserID From Usuarios where Login ='" + CMB + "'", conn);

            var reader = cmd.ExecuteReader();      

            _UserID = reader.Read() ? reader["UserID"].ToString() : null;

            conn.Close();
            conn.Dispose(); //Destroi a conexão
        }

        public void refresUserOnlineStatus()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString);
            var cmm = new SqlCommand("Stp_UsuarioRefreshStatusOnline", connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 18000
            };

            var userid = new SqlParameter("@UserID", SqlDbType.Int) { Value = appSession.UserId };
            cmm.Parameters.Add(userid);

            var fullname = new SqlParameter("@FullName", SqlDbType.VarChar, 250) { Value = appSession.FullName };
            cmm.Parameters.Add(fullname);

            connection.Open();
            cmm.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
        }


    }
}