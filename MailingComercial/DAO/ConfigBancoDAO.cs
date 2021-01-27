using MailingComercial.Config;
using MailingComercial.Controllers;
using MailingComercial.Guardian;
using System;
using System.Data.SqlClient;

namespace MailingComercial.DAO
{
    class ConfigBancoDAO
    {
        public void BuscarConfigs(bool Rotina)
        {
            string query =
                "SELECT CADASTRO, DELAY_CICLO, EMAIL_VALIDACAO, TIPO_UPLOAD, VALOR_UPLOAD, DELAY_UPLOAD, HORA_INICIO, HORA_FIM, TOP_REGISTROS, HORA " +
                "FROM " + Tabelas_Guardian.ConfigUpload + " " +
                "WHERE ROTINA = 'MAILING'";

            using (SqlConnection conexao = new SqlConnection(ConexaoGestor.Conexao()))
            {
                using (SqlCommand command = new SqlCommand(query, conexao))
                {
                    conexao.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["CADASTRO"].ToString().TrimStart().TrimEnd() == "S")
                                Service_Config.CadastroHabilitado = true;
                            Service_Config.DelayCiclo = Convert.ToDouble(reader["DELAY_CICLO"].ToString().TrimStart().TrimEnd());
                            Service_Config.EmailValidacao = reader["EMAIL_VALIDACAO"].ToString().TrimStart().TrimEnd();
                            Service_Config.TipoUpload = reader["TIPO_UPLOAD"].ToString().TrimStart().TrimEnd();
                            Service_Config.ValorUpload = reader["VALOR_UPLOAD"].ToString().TrimStart().TrimEnd();
                            //Service_Config.DelayUpload = Convert.ToDouble(reader["DELAY_UPLOAD"].ToString().TrimStart().TrimEnd());
                            //Service_Config.UploadHoraInicio = Convert.ToDateTime(reader["HORA_INICIO"].ToString().TrimStart().TrimEnd());
                            //Service_Config.UploadHoraFim = Convert.ToDateTime(reader["HORA_FIM"].ToString().TrimStart().TrimEnd());
                            Service_Config.TopRegistros = reader["TOP_REGISTROS"].ToString().TrimStart().TrimEnd();
                            
                            if (Rotina)
                            {
                                EnvioComercialController.HoraRotina = Convert.ToInt32(reader["HORA"].ToString().TrimStart().TrimEnd());
                            }

                        }
                    }
                }
            }
        }
    }
}
