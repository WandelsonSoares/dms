using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace App_Code
{
    public class Permissoes
    {
        
        SqlConnection conn;
        String strConn = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString;

        Persistencia_Fast consult = new Persistencia_Fast();

        string retorno;

        /// <summary>
        /// Consulta se usuário logado possui permissão para operação. Retorna o Id da área do sistema caso o usuário seja autorizado.
        /// </summary>
        /// <param name="codigoOpcao">Lista de id de áreas do sistema a ser pesquisada. Os Ids devem ser separados por vírgula.</param>
        /// <param name="userId">Id do usuário logado.</param>
        /// <returns>Retorna o Id da área do sistema caso o usuário seja autorizado.</returns>
        public string AreaPermitida(string codigoOpcao, string userId)
        {
            conn = new SqlConnection(strConn);
            conn.Open();

            var cmd = new SqlCommand("SELECT AreaID From UsuariosPermissoes (nolock) WHERE (AreaID in( " + codigoOpcao + ")) AND (userId = " + userId + ")", conn);

            var reader = cmd.ExecuteReader();

            //  cmd.ExecuteReader(); //O ExecuteNonQuery é usado com os comandos Update e Insert  

            retorno = reader.Read()
                ? reader["AreaID"].ToString()
                : "0";

            conn.Close();
            conn.Dispose(); //Destroi a conexão

            return retorno;

        }

        /// <summary>
        /// Consulta se usuário logado pode mudar responsável pelo atendimento da demanda. Retorna true se o usuário for administrador ou se o responsável pelo setor de atendimento.
        /// </summary>
        /// <param name="UserId">Id do usuário logado.</param>
        /// <param name="UserAdmin">Parâmetro informando se o usuário logado é administrador.</param>
        /// <param name="DemandaId">Id da demanda a ser consultada.</param>
        /// <returns>Retorna true se o usuário for administrador ou se o responsável pelo setor de atendimento.</returns>
        public bool PermissaoParaMudarResponsavelDemanda(string UserId, string UserAdmin, string DemandaId)
        {
            bool TemPermissao = false;

            if (UserAdmin == "S")
            {
                TemPermissao = true;
            }
            else
            {
                if (UserId == ConsultaResponsavelSetorIdDemanda(DemandaId))
                    TemPermissao = true;
                else
                    //Se não é Responsável pelo setor, verifica se possui permissão para alterar o responsável pelo atendimento à demanda.
                    TemPermissao = AreaPermitida("112", UserId) == "0" ? false : true;
            }

            return TemPermissao;
        }

        /// <summary>
        /// Consulta se usuário logado pode atualizar a demanda. Retorna true caso seja usuário administrador ou responsável pelo setor de atendimento. 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserAdmin"></param>
        /// <param name="DemandaId"></param>
        /// <returns></returns>
        public bool PermissaoParaAtualizarDemanda(string UserId, string UserAdmin, string DemandaId)
        {
            bool TemPermissao = false;

            if (UserAdmin == "S")
            {
                TemPermissao = true;
            }
            else
            {
                if (UserId == ConsultaResponsavelSetorIdDemanda(DemandaId))
                {
                    TemPermissao = true;
                }
            }

            return TemPermissao;
        }

        /// <summary>
        /// Verifica se usuário pode cancelar a demanda. Retorna true caso o usuário seja administrador ou seja o solicitante (desde que o status da demanda seja 'AGUARDANDO') ou caso seja o responsável pelo setor de atendimento.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserAdmin"></param>
        /// <param name="DemandaId"></param>
        /// <param name="DemandaStatus"></param>
        /// <returns>Retorna true caso o usuário seja administrador ou seja o solicitante (desde que o status da demanda seja 'AGUARDANDO') ou caso seja o responsável pelo setor de atendimento.</returns>
        public bool PermissaoParaCancelarDemanda(string UserId, string UserAdmin, string DemandaId, string DemandaStatus)
        {
            bool TemPermissao = false;
            string SolicitanteId = "";
            string ResponsavelId = "";

            if (UserAdmin == "S")
            {
                TemPermissao = true;
            }
            else
            {

                SolicitanteId = consult.Consulta("SELECT SolicitanteId FROM Demandas WHERE DemandaId = " + DemandaId, "SolicitanteId");

                if (UserId == SolicitanteId && DemandaStatus == "AGUARDANDO")
                {
                    TemPermissao = true;
                }
                else
                {

                    ResponsavelId = consult.Consulta("SELECT ResponsavelId FROM Demandas WHERE DemandaId = " + DemandaId, "ResponsavelId");
                    if (UserId == ResponsavelId)
                    {
                        TemPermissao = true;
                    }
                    else
                    {

                        if (UserId == ConsultaResponsavelSetorIdDemanda(DemandaId))
                        {
                            TemPermissao = true;
                        }
                    }
                }

            }

            return TemPermissao;
        }

        /// <summary>
        /// Consulta o responsável pelo setor de atendimento da demanda. Retorna o id do responsável pelo setor de atendimento da demanda.
        /// </summary>
        /// <param name="DemandaId">Id da demanda a ser consultada.</param>
        /// <returns>Retorna o id do responsável pelo setor de atendimento da demanda.</returns>
        public string ConsultaResponsavelSetorIdDemanda(string DemandaId)
        {
            string ResponsavelSetorId = consult.Consulta("SELECT DISTINCT Setores.ResponsavelId FROM Processos " +
                                                                            " INNER JOIN " +
                                                                            " Demandas INNER JOIN Atividades ON Demandas.AtividadeId = Atividades.AtividadeId " +
                                                                            " INNER JOIN Subprocessos ON Atividades.SubprocessoId = Subprocessos.SubprocessoId ON Processos.ProcessoId = Subprocessos.ProcessoId " +
                                                                            " INNER JOIN Areas ON Processos.AreaId = Areas.AreaId INNER JOIN " +
                                                                            " Setores ON Areas.SetorId = Setores.SetorId " +
                                                                        " WHERE     Demandas.DemandaId = " + DemandaId, "ResponsavelId");

            return ResponsavelSetorId;
        }

        /// <summary>
        /// Consulta se usuário pode ver demanda. Esta validação evita acesso não autorizado à demanda digitando-se o id na query string na barra de endereços do navegador. Retorna true ou false.
        /// É permitida visualização a: responsável pelo atendimento, solicitante, responsável pelo setor, perfis de usuário específicos (HRBP, Gerente de Contrato, Gerente de Departamento, Diretoria).
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserAdmin"></param>
        /// <param name="DemandaId"></param>
        /// <returns></returns>
        public bool PermissaoParaVerDemanda(string UserId, string UserAdmin, string DemandaId)
        {
            bool TemPermissao = false;
            string SolicitanteId = "";
            string ResponsavelId = "";
            int GrupoUsuarioId = -1;
            string ContratoId = "";

            if (UserAdmin == "S")
            {
                TemPermissao = true;
            }
            else
            {

                SolicitanteId = consult.Consulta("SELECT SolicitanteId FROM Demandas WHERE DemandaId = " + DemandaId, "SolicitanteId");

                if (UserId == SolicitanteId)
                {
                    TemPermissao = true;
                }
                else
                {

                    ResponsavelId = consult.Consulta("SELECT ResponsavelId FROM Demandas WHERE DemandaId = " + DemandaId, "ResponsavelId");
                    if (UserId == ResponsavelId)
                    {
                        TemPermissao = true;
                    }
                    else
                    {

                        if (UserId == ConsultaResponsavelSetorIdDemanda(DemandaId))
                        {
                            TemPermissao = true;
                        }
                        else //Consulta se o perfil do usuário permite visualizar. Se sim, verifica se demanda está na lista de contratos permitidos.
                        {
                            GrupoUsuarioId = Convert.ToInt32(consult.Consulta("SELECT IsNull(GrupoId, 0) as GrupoId FROM Usuarios WHERE UserId = " + UserId, "GrupoId"));

                            switch (GrupoUsuarioId)
                            {
                                case 4: //Responsável pelo departamento de RH,  acesso a todos
                                    TemPermissao = true; 
                                    break;
                                case 5: //Diretoria Operacional e superintendente, acesso a todos
                                    TemPermissao = true;
                                    break;
                                case 6: //HR Business Partner
                                    ContratoId = consult.Consulta("SELECT CNId FROM Demandas WHERE DemandaId = " + DemandaId, "CNId");

                                    if (Convert.ToInt32(consult.Consulta("SELECT COUNT(ID) AS Quantidade FROM UsuariosPermissoes WHERE UserID = " + UserId + " AND ContratoId = " + ContratoId, "Quantidade")) > 0)
                                        TemPermissao = true;
                                    break;

                                case 8: //Diretoria de RH, acesso a todos
                                    TemPermissao = true;
                                    break;

                                case 9: //Gerentes
                                    ContratoId = consult.Consulta("SELECT CNId FROM Demandas WHERE DemandaId = " + DemandaId, "CNId");

                                    if (Convert.ToInt32(consult.Consulta("SELECT COUNT(ID) AS Quantidade FROM UsuariosPermissoes WHERE UserID = " + UserId + " AND ContratoId = " + ContratoId, "Quantidade")) > 0)
                                        TemPermissao = true;
                                    break;
                                default:
                                    TemPermissao = false;
                                    break;
                            }


                            //List<int> checkValues = new List<int> { 1, 2, 3 }; //Id dos grupos de usuários que podem ter acesso, desde que a demanda seja de um contrato no qual os mesmos possuem acesso
                            //if (checkValues.Contains(GrupoUsuarioId))
                        }
                    }
                }

            }

            return TemPermissao;
        }

        /// <summary>
        /// Consulta usuário tem acesso a determinada área do sistema. Retorna true ou false.
        /// </summary>
        /// <param name="UserId">Id do usuário logado.</param>
        /// <param name="AreaID">Id da área do sistema.</param>
        /// <returns>Retorna true ou false.</returns>
        public bool PermissaoParaSistemaArea(string UserId, string AreaId)
        {
            bool TemPermissao = false;

            string UsuarioId = consult.Consulta("SELECT DISTINCT UserId FROM UsuariosPermissoesSistemaAreas WHERE AreaId = " + AreaId + " AND UserId = " + UserId, "UserId");

            if (UsuarioId != "" && UsuarioId != null)
                TemPermissao = true;

            return TemPermissao;
        }

        public bool PermissaoParaNovaDemanda(string UserId, string UserAdmin)
        {
            bool TemPermissao = false;

            if (UserAdmin == "S")
                TemPermissao = true;
            else
            {
                if (UserId == consult.Consulta("SELECT  UserID FROM UsuariosPermissoes WHERE UserId = " + UserId + " AND AreaId = " + 109, "UserId"))
                    TemPermissao = true;
                else
                    TemPermissao = false;
            }
            return TemPermissao;
        }

    }


}