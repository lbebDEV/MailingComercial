using MailingComercial.Guardian;
using MailingComercial.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MailingComercial.DAO
{
    class Contato_ClienteDAO
    {
        public List<Contato_Cliente> BuscarClientesOntem()
        {
            List<Contato_Cliente> clientes = new List<Contato_Cliente>();

            string query =
                  "SELECT CLIENTES.RAZAO, CL_NOME, CL_OBSERVACAO, CL_OBS_PROXIMO, CL_DATA_PROXIMO, CL_HORA_PROXIMO " +
                  "FROM CONTATO_CLIENTE " +
                  "INNER JOIN CLIENTES ON CONTATO_CLIENTE.CL_COD = CONTATO_CLIENTE.CL_COD " +
                  "WHERE CL_DATA_ATUAL = '" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "' " +
                  "ORDER BY CLIENTES.RAZAO";

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
                            Contato_Cliente cliente = new Contato_Cliente
                            {
                                Razao = reader["RAZAO"].ToString().Trim(),
                                NomeContato = reader["CL_NOME"].ToString().Trim(),
                                Observacao = reader["CL_OBSERVACAO"].ToString().Trim(),
                                ObservacaoProximo = reader["CL_OBS_PROXIMO"].ToString().Trim(),
                                DataProximo = reader["CL_DATA_PROXIMO"].ToString().Trim(),
                                HoraProximo = reader["CL_HORA_PROXIMO"].ToString().Trim(),
                            };

                            clientes.Add(cliente);
                        }
                    }
                }
            }

            return clientes;
        }

        public List<Contato_Cliente> BuscarClientesHoje()
        {
            List<Contato_Cliente> prospects = new List<Contato_Cliente>();

            string query =
                "SELECT CLIENTES.RAZAO, CL_NOME, CL_OBSERVACAO, CL_OBS_PROXIMO, CL_DATA_PROXIMO, CL_HORA_PROXIMO " +
                "FROM CONTATO_CLIENTE " +
                "INNER JOIN CLIENTES ON CONTATO_CLIENTE.CL_COD = CONTATO_CLIENTE.CL_COD " +
                "WHERE CL_DATA_PROXIMO = '" + DateTime.Now.ToString("yyyyMMdd") + "' " +
                "ORDER BY CLIENTES.RAZAO";

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
                            Contato_Cliente prospect = new Contato_Cliente
                            {
                                Razao = reader["RAZAO"].ToString().Trim(),
                                NomeContato = reader["CL_NOME"].ToString().Trim(),
                                Observacao = reader["CL_OBSERVACAO"].ToString().Trim(),
                                ObservacaoProximo = reader["CL_OBS_PROXIMO"].ToString().Trim(),
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
