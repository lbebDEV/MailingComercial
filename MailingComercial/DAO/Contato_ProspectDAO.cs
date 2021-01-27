using MailingComercial.Guardian;
using MailingComercial.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MailingComercial.DAO
{
    class Contato_ProspectDAO
    {
        public List<Contato_Prospect> BuscarProspectsOntem()
        {
            List<Contato_Prospect> prospects = new List<Contato_Prospect>();

            string query =
                  "SELECT PROSPECTS.RAZAO, P_NOME, P_OBSERVACAO, P_OBS_PROXIMO, P_DATA_PROXIMO, P_HORA_PROXIMO " +
                  "FROM CONTATO_PROSPECT " +
                  "INNER JOIN PROSPECTS ON CONTATO_PROSPECT.P_COD_PROSPECT = PROSPECTS.COD " +
                  "WHERE P_DATA_ATUAL = '" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "' " +
                  "ORDER BY PROSPECTS.RAZAO";

            using (SqlConnection connection = new SqlConnection(ConexaoGestor.Conexao()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 240;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contato_Prospect prospect = new Contato_Prospect
                            {
                                Razao = reader["RAZAO"].ToString().Trim(),
                                NomeContato = reader["P_NOME"].ToString().Trim(),
                                Observacao = reader["P_OBSERVACAO"].ToString().Trim(),
                                ObservacaoProximo = reader["P_OBS_PROXIMO"].ToString().Trim(),
                                DataProximo = reader["P_DATA_PROXIMO"].ToString().Trim(),
                                HoraProximo = reader["P_HORA_PROXIMO"].ToString().Trim(),
                            };

                            prospects.Add(prospect);
                        }
                    }
                }
            }

            return prospects;
        }

        public List<Contato_Prospect> BuscarProspectsHoje()
        {
            List<Contato_Prospect> prospects = new List<Contato_Prospect>();

            string query =
                  "SELECT PROSPECTS.RAZAO, P_NOME, P_OBSERVACAO, P_OBS_PROXIMO " +
                  "FROM CONTATO_PROSPECT " +
                  "INNER JOIN PROSPECTS ON CONTATO_PROSPECT.P_COD_PROSPECT = PROSPECTS.COD " +
                  "WHERE P_DATA_PROXIMO = '" + DateTime.Now.ToString("yyyyMMdd") + "' " +
                  "ORDER BY PROSPECTS.RAZAO";

            using (SqlConnection connection = new SqlConnection(ConexaoGestor.Conexao()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 240;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contato_Prospect prospect = new Contato_Prospect
                            {
                                Razao = reader["RAZAO"].ToString().Trim(),
                                NomeContato = reader["P_NOME"].ToString().Trim(),
                                Observacao = reader["P_OBSERVACAO"].ToString().Trim(),
                                ObservacaoProximo = reader["P_OBS_PROXIMO"].ToString().Trim(),
                            };

                            prospects.Add(prospect);
                        }
                    }
                }
            }

            return prospects;
        }
    }
}
