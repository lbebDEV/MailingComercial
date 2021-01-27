using MailingComercial.Guardian;
using MailingComercial.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MailingComercial.DAO
{
    class Contato_LeadDAO
    {
        public List<Contato_Lead> BuscarLeadsOntem()
        {
            List<Contato_Lead> leads = new List<Contato_Lead>();

            string query =
                  "SELECT LEADS.L_RAZAO, C_NOME, C_OBSERVACAO, C_OBS_PROXIMO, C_DATA_PROXIMO, C_HORA_PROXIMO " +
                  "FROM CONTATO_LEAD " +
                  "INNER JOIN LEADS ON CONTATO_LEAD.C_COD_LEAD = LEADS.L_COD " +
                  "WHERE C_DATA_ATUAL = '" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "'" +
                  "ORDER BY LEADS.L_RAZAO";

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
                            Contato_Lead lead = new Contato_Lead
                            {
                                Razao = reader["L_RAZAO"].ToString().Trim(),
                                NomeContato = reader["C_NOME"].ToString().Trim(),
                                Observacao = reader["C_OBSERVACAO"].ToString().Trim(),
                                ObservacaoProximo = reader["C_OBS_PROXIMO"].ToString().Trim(),
                                DataProximo = reader["C_DATA_PROXIMO"].ToString().Trim(),
                                HoraProximo = reader["C_HORA_PROXIMO"].ToString().Trim(),
                            };

                            leads.Add(lead);
                        }
                    }
                }
            }

            return leads;
        }

        public List<Contato_Lead> BuscarLeadsHoje()
        {
            List<Contato_Lead> leads = new List<Contato_Lead>();

            string query =
               "SELECT LEADS.L_RAZAO, C_NOME, C_OBSERVACAO, C_OBS_PROXIMO " +
               "FROM CONTATO_LEAD " +
               "INNER JOIN LEADS ON CONTATO_LEAD.C_COD_LEAD = LEADS.L_COD " +
               "WHERE C_DATA_PROXIMO = '" + DateTime.Now.ToString("yyyyMMdd") + "' " +
               "ORDER BY LEADS.L_RAZAO";

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
                            Contato_Lead lead = new Contato_Lead
                            {
                                Razao = reader["L_RAZAO"].ToString().Trim(),
                                NomeContato = reader["C_NOME"].ToString().Trim(),
                                Observacao = reader["C_OBSERVACAO"].ToString().Trim(),
                                ObservacaoProximo = reader["C_OBS_PROXIMO"].ToString().Trim(),
                            };

                            leads.Add(lead);
                        }
                    }
                }
            }

            return leads;
        }
    }
}
