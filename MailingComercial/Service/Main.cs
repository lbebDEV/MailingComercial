using MailingComercial.Config;
using MailingComercial.Controllers;
using MailingComercial.Guardian;
using MailingComercial.ServiceLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailingComercial.Service
{
    class Main
    {
        public static string IdCiclo { get; set; }

        public void ExecucaoServico()
        {
            IdCiclo = DateTime.Now.ToString("yyyyMMddHHmmss");

            try
            {
                Service_Config.CarregarConfiguracoes();
                Guardian_LogTxt.LogControle(TipoControle.Ciclo_Iniciado);
                Guardian_Log.Log_Rotina("", Service_Config.NomeServico, Tipo.Iniciado);
                if (Service_Config.Status)
                {
                    EnvioComercialController envioComercialController = new EnvioComercialController();
                    envioComercialController.Executar();
                }
              
                Guardian_LogTxt.LogControle(TipoControle.Ciclo_Finalizado);
                Guardian_Log.Log_Rotina("", Service_Config.NomeServico, Tipo.Finalizado);
            }
            catch (Exception ex)
            {
                Guardian_LogTxt.LogAplicacao(Service_Config.NomeServico, ex.ToString());
                Guardian_Log.Log_Ocorrencia(Service_Config.NomeServico, "Erro ao executar o serviço.", ex.ToString(), "");
            }
        }
    }
}
