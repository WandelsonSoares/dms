using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace App_Code
{

    public class Persistencia_Fast
    {
        string retorno;
        //public int opcao;

        private readonly cSession appSession = new cSession();

        SqlConnection conn;
        readonly String strConn = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;

        public void atualizaInsereDados(string sql)
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            try
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery(); //O ExecuteNonQuery é usado com os comandos Update e Insert  
            }
            catch
            {


            }
            finally
            {
                conn.Close();
                conn.Dispose(); //Destroi a conexão 
            }

        }

        public string Consulta(string sql, string ConsultaColuna)
        {
            //_Usuario refresh = new _Usuario();
            //

            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand(sql, conn);

            try
            {
                var reader = cmd.ExecuteReader();

                //  cmd.ExecuteReader(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

                retorno = reader.Read() ? reader["" + ConsultaColuna + ""].ToString() : null;

            }
            catch
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose(); //Destroi a conexão
            }

            return retorno;

        }

        public void getStatusFechamento()
        {
            appSession.MaxMes_Consuntivo = Consulta(@"
                                                    SELECT Max(Mes) as Mes
                                                    FROM [dbo].[Publicacao]
                                                    WHERE BU = " + appSession.BU + @" 
                                                    AND ANO = " + appSession.AnoAtual + @"
                                                    AND LIBERADO = 2
                                                    AND [STATUS] = 'CONSUNTIVO'
                                                    ", "");

            appSession.MaxMes_Consuntivo = Consulta(@"
                                                    SELECT Max(Mes) as Mes
                                                    FROM [dbo].[Publicacao]
                                                    WHERE BU = " + appSession.BU + @" 
                                                    AND ANO = " + appSession.AnoAtual + @"
                                                    AND LIBERADO = 2
                                                    AND [STATUS] = 'FLASH'
                                                    ", "");
        }

        public string ConsultaPublicacao(string sql, string ConsultaColuna)
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand(sql, conn);

            try
            {
                var reader = cmd.ExecuteReader();

                retorno = reader.Read() ? reader["" + ConsultaColuna + ""].ToString() : "0";
            }
            catch
            {
            }
            finally
            {

                conn.Close();
                conn.Dispose(); //Destroi a conexão
            }

            return retorno;

        }

        public string ConsultaRapida(string sql, ref string retorno2S, string ConsultaColuna)
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand(sql, conn);

            try
            {
                var reader = cmd.ExecuteReader();

                //  cmd.ExecuteReader(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

                retorno2S = reader.Read() ? reader["" + ConsultaColuna + ""].ToString() : null;
            }
            catch
            {
            }
            finally
            {

                conn.Close();
                conn.Dispose(); //Destroi a conexão 
            }

            return retorno2S;

        }

        public void Consulta_Contrato_CC(string sql, ref string CCusto, ref int ContratoID, ref int ret)
        {
            // o nome é "ConsultaRapida_INT"  porque consulta na coluna de numeros do TIPO INT            
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand(sql, conn);

            var reader = cmd.ExecuteReader();

            //  cmd.ExecuteReader(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            if (!reader.Read())
            {
                ret = 0;
            }
            else
            {
                try
                {
                    CCusto = reader["CCusto"].ToString();
                    ContratoID = Convert.ToInt32(reader["ContratoID"]);
                    ret = 1;
                }
                catch
                {
                    CCusto = "0";
                    ContratoID = 0;
                    ret = 0;
                }
            }

            conn.Close();
            conn.Dispose(); //Destroi a conexão

        }

        public void Executa_sp_statusAtualizaTodasAcoes()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString2"].ConnectionString);
            var cmm = new SqlCommand("sp_statusAtualizaTodasAcoes", connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60000
            };

            connection.Open();
            cmm.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
        }

        public DataSet DTSetConsulta(string sql) // Cria um dataset
        {
            var resultadoConsulta = new DataSet();

            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
                conn = new SqlConnection(connectionString);
                var da = new SqlDataAdapter(sql, conn);

                da.Fill(resultadoConsulta); /* Preenche os registros recuperados do BD e carrega os dados em um objeto DataSet ds. Obs: A classe SqlDataAdapter media a interação entre a classe DataSet e banco de dados reais.*/


                return (resultadoConsulta);
            }
            catch
            {
                resultadoConsulta.Clear();
                return (resultadoConsulta);
            }
        }

        public DataTable DtTableConsulta(string sql) // Cria um dataset
        {
            var resultadoConsulta = new DataTable();

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);
            var da = new SqlDataAdapter(sql, conn);

            da.Fill(resultadoConsulta); /* Preenche os registros recuperados do BD e carrega os dados em um objeto DataSet ds.
                                            * Obs: A classe SqlDataAdapter media a interação entre a classe DataSet e banco de dados reais.*/


            return (resultadoConsulta);
        }

        public DataSet CarregaContratos(string UserId, string UserAdmin)
        {
            var ds = new DataSet();
            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            if (UserAdmin == "S" || appSession.UserGrupoId != "1") //Diferente de solicitante
                sql = "SELECT ContratoID, DescContract FROM Contratos WHERE Ativo = 1 ORDER BY DescContract ";
            else
                sql = "SELECT ContratoID, DescContract FROM Contratos WHERE Ativo = 1 AND (ContratoID IN (SELECT ContratoID From UsuariosPermissoes WHERE (UserID = " + UserId + "))) order by DescContract ";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Contrato");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaContratosEdit()
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            switch (appSession.UserAdmin)
            {
                case "S":
                    sql = "SELECT ContratoID, DescContract as DescContract FROM Contrato where ContratoID > 0 and BU = '" + appSession.BU + "' AND Ativo = 1 order by DescContract";
                    break;
                default:
                    if (appSession.UserControl == "S")
                    {
                        sql = "SELECT ContratoID, DescContract as DescContract FROM Contrato where ContratoID > 0 and BU = '" + appSession.BU + "' AND Ativo = 1 order by DescContract";
                    }
                    else
                    {
                        sql = "SELECT ContratoID, DescContract as DescContract FROM Contrato WHERE ContratoID > 0 and BU = '" + appSession.BU + "' and ContratoID <> 0 AND Ativo = 1 and (ContratoID IN (SELECT ContratoID From UsuariosPermissoes WHERE (UserID = " + appSession.UserId + "))) order by DescContract ";
                    }
                    break;
            }

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Contrato");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaResponsaveis()
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            switch (appSession.UserAdmin)
            {
                case "S":
                    sql = "SELECT UserID, Upper(Nome) as Nome From Usuarios where Bloqueado = 0 order by Nome";
                    break;
                default:
                    if (appSession.UserControl == "S")
                    {
                        sql = "SELECT UserID, Upper(Nome) as Nome From Usuarios where Bloqueado = 0 order by Nome";
                    }
                    else
                    {
                        sql = "SELECT UserID, Upper(Nome) as Nome From Usuarios where Bloqueado = 0 order by Nome";
                    }
                    break;
            }

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Usuarios");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaResponsaveisAtendentes()
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            sql = "SELECT     Usuarios.UserID, UPPER(Usuarios.Nome) AS Nome " +
                  " FROM         Usuarios INNER JOIN UsuariosPermissoes ON Usuarios.UserID = UsuariosPermissoes.UserID " +
                   " WHERE     (Usuarios.Bloqueado = 0) AND (UsuariosPermissoes.AreaID = 108) " +
                   " ORDER BY Usuarios.Nome";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Usuarios");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        /// <summary>
        /// Carrega um dataset para preenchimento de listas e dropdownlists. Se informar BUId retorna apenas uma BU.
        /// </summary>
        /// <param name="EmpresaId"></param>
        /// <param name="BUId"></param>
        /// <returns></returns>
        public DataSet CarregaBUs(int EmpresaId = 0, int BUId = 0)
        {
            var ds = new DataSet();
            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);


            if (EmpresaId == 0 && BUId == 0)
                sql = "SELECT BUId, Nome FROM BUs ORDER BY Nome";
            else
            {
                if (BUId == 0)
                    sql = "SELECT BUId, Nome FROM BUs WHERE EmpresaId = " + EmpresaId + " ORDER BY Nome";
                else
                    sql = "SELECT BUId, Nome FROM BUs WHERE BUId = " + BUId + " ORDER BY Nome";
            }

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "BUs");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaDepartamentos()
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            //Filtra apenas os departamentos ligados às áreas que são permitidas aos usuários
            //Mesmo administradores precisam ter o acesso às áreas cadastrados para que o departamento seja listado
            sql = "SELECT DepartamentoId, Nome FROM Departamentos " +
                        "WHERE DepartamentoId IN (SELECT  DepartamentoId " +
                                                        "FROM AreasUsuarios INNER JOIN " +
                                                            " Areas ON AreasUsuarios.AreaId = Areas.AreaId INNER JOIN " +
                                                            " Setores ON Areas.SetorId = Setores.SetorId " +
                                                            " WHERE     AreasUsuarios.UsuarioId = " + appSession.UserId +
                                                 ") ORDER BY NOME";


            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Departamentos");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaSetores(int DepartamentoId = 0)
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            if (DepartamentoId == 0)
                sql = "SELECT SetorId, Nome From Setores Order by Nome";
            else
                sql = "SELECT SetorId, Nome From Setores WHERE DepartamentoId = " + DepartamentoId + " Order by Nome";


            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Setores");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaAreas(int SetorId = 0)
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            if (SetorId == 0)
                sql = "SELECT AreaId, Nome From Areas Order by Nome";
            else
                sql = "SELECT AreaId, Nome From Areas WHERE SetorId = " + SetorId + " Order by Nome";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Areas");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaAreasFiltro(string usuarioId, string usuarioAdmin = "")
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            var ds = new DataSet();
            string sql;


            if (usuarioAdmin != "S")

                sql = "SELECT     AreasUsuarios.AreaId, Areas.Nome FROM AreasUsuarios INNER JOIN Areas ON AreasUsuarios.AreaId = Areas.AreaId " +
                        " WHERE (AreasUsuarios.UsuarioId = " + usuarioId + ") " +
                            " ORDER BY Areas.Nome";
            else
                sql = "SELECT AreaId, Nome From Areas Order by Nome";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "AreasUsuarios");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaContratosFiltro(string usuarioId, string GrupoId)
        {

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            string sql = "SELECT ContratoId, DescContract as Nome FROM Contratos WHERE ContratoId IN (" + ListaContratosPermitidos(usuarioId, GrupoId) + ") ORDER BY Nome";
            var ds = new DataSet();
            var da = new SqlDataAdapter(sql, conn);

            try
            {
                da.Fill(ds, "Contratos");

                conn.Close();
                conn.Dispose();
                return (ds);
            }
            catch
            {
                conn.Close();
                conn.Dispose();
                return (ds);
            }

        }

        public DataSet CarregaPrioridadeFiltro()
        {

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            string sql = "SELECT PrioridadeId, Nome FROM DemandasPrioridades ORDER BY PrioridadeId";
            var ds = new DataSet();
            var da = new SqlDataAdapter(sql, conn);

            try
            {
                da.Fill(ds, "DemandasPrioridades");

                conn.Close();
                conn.Dispose();
                return (ds);
            }
            catch
            {
                conn.Close();
                conn.Dispose();
                return (ds);
            }

        }

        public DataSet CarregaResponsaveisFiltro(string usuarioId)
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            sql = "SELECT DISTINCT   Demandas.ResponsavelId, Usuarios.Nome " +
                    " FROM         Demandas INNER JOIN " +
                      " Atividades ON Demandas.AtividadeId = Atividades.AtividadeId INNER JOIN " +
                      " Subprocessos ON Atividades.SubprocessoId = Subprocessos.SubprocessoId INNER JOIN " +
                      " Processos ON Subprocessos.ProcessoId = Processos.ProcessoId INNER JOIN " +
                      " AreasUsuarios ON Processos.AreaId = AreasUsuarios.AreaId INNER JOIN " +
                      " Usuarios ON Demandas.ResponsavelId = Usuarios.UserID " +
                      " WHERE     (AreasUsuarios.UsuarioId = " + usuarioId + ") " +
                      " ORDER BY Usuarios.Nome";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Responsaveis");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaSolicitantesFiltro(string usuarioId)
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            sql = "SELECT DISTINCT   Demandas.SolicitanteId, Usuarios.Nome " +
                    " FROM         Demandas INNER JOIN " +
                      " Atividades ON Demandas.AtividadeId = Atividades.AtividadeId INNER JOIN " +
                      " Subprocessos ON Atividades.SubprocessoId = Subprocessos.SubprocessoId INNER JOIN " +
                      " Processos ON Subprocessos.ProcessoId = Processos.ProcessoId INNER JOIN " +
                      " AreasUsuarios ON Processos.AreaId = AreasUsuarios.AreaId INNER JOIN " +
                      " Usuarios ON Demandas.SolicitanteId = Usuarios.UserID " +
                      " WHERE     (AreasUsuarios.UsuarioId = " + usuarioId + ") " +
                      " ORDER BY Usuarios.Nome";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Solicitantes");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaProcessos(int AreaId = 0)
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            if (AreaId == 0)
                sql = "SELECT ProcessoId, Nome From Processos WHERE Ativo = 1 Order by Nome";
            else
                sql = "SELECT ProcessoId, Nome From Processos WHERE AreaId = " + AreaId + " AND Ativo = 1 Order by Nome";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Processos");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaSubProcessos(int ProcessoId = 0)
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);


            if (ProcessoId == 0)
                sql = "SELECT SubprocessoId, Nome From Subprocessos Order by Nome";
            else
                sql = "SELECT SubprocessoId, Nome From Subprocessos WHERE ProcessoId = " + ProcessoId + " Order by Nome";


            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Subprocessos");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaAtividades(int SubprocessoId = 0)
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            if (SubprocessoId == 0)
                sql = "SELECT AtividadeId, Nome From Atividades Order by Nome";
            else
                sql = "SELECT AtividadeId, Nome From Atividades WHERE SubprocessoId = " + SubprocessoId + " Order by Nome";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Atividades");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaResponsaveisTipo()
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            sql = "SELECT TipoId, Nome From EtapasResponsaveisTipos Order by Nome";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "EtapasResponsaveisTipos");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaDocumentosTipo()
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            sql = "SELECT TipoId, Nome From EtapasDocumentosTipos Order by Nome";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "EtapasDocumentosTipos");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaEtapasPrecedentes(int AtividadeId, int EtapaId)
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            sql = "SELECT EtapaId, Nome FROM Etapas WHERE AtividadeId = " + AtividadeId + " AND EtapaId <> " + EtapaId + " Order by Ordem";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Etapas");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaEmpresas()
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            sql = "SELECT EmpresaId, Upper(Nome) as Nome From Empresas Order by Nome";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Empresas");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaFuncionarios()
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            sql = "SELECT Matricula, Upper(Nome) as Nome FROM tblFuncionarios where Ativo = 1 order by Nome";



            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "tblFuncionarios");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public DataSet CarregaPermissoesOperacao()
        {
            var ds = new DataSet();

            string sql;

            var connectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;
            conn = new SqlConnection(connectionString);

            sql = "SELECT Id, Descricao FROM Permissoes Order by Descricao";

            var da = new SqlDataAdapter(sql, conn);

            da.Fill(ds, "Permissoes");

            conn.Close();
            conn.Dispose();

            return (ds);
        }

        public string ListaContratosPermitidos(string UsuarioId, string GrupoId)
        {
            string Contratos = "";
            string sql = "";

            if (GrupoId == "1" || GrupoId == "6" || GrupoId == "9") //Se Grupo é Solicitantes (1), HR Business Partner (6) ou Gerentes (9) busca contratos permitidos
                sql = "SELECT ContratoId FROM UsuariosPermissoes WHERE UserId = " + UsuarioId;
            else
                sql = "SELECT ContratoId FROM Contratos WHERE Ativo = 1";

            //Obtém a lista de IDs das áreas que o usuário faz parte ou é superior
            var con = new SqlConnection(strConn);
            var command = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                if (Contratos != "")
                    Contratos = Contratos + ", " + dr.Field<Int32>("ContratoId").ToString();
                else
                    Contratos = dr.Field<Int32>("ContratoId").ToString();
            }
            con.Close();

            return Contratos;
        }

        public void NotificacaoEnvia(string DestinatarioId, string Assunto, string Notificacao, string URL = "#", string RemententeId = "0")
        {
            atualizaInsereDados("INSERT INTO Notificacoes VALUES (" + DestinatarioId + ", " + RemententeId + ", '" + Assunto.Replace("'", "") + "', '" + Notificacao.Replace("'", "") + "', 0, '" + URL.Replace("'", "") + "', GETDATE())");
        }

    }
}
