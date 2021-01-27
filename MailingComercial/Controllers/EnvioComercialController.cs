using MailingComercial.Config;
using MailingComercial.DAO;
using MailingComercial.Email;
using MailingComercial.Guardian;
using MailingComercial.Model;
using MailingComercial.Service;
using MailingComercial.ServiceLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailingComercial.Controllers
{
    class EnvioComercialController : RotinaEspecifica
    {
        public static bool EnvioHabilitado { get; set; } = false;
        public static int HoraRotina;

        Contato_LeadDAO contato_LeadDAO = new Contato_LeadDAO();
        Contato_ProspectDAO contato_ProspectDAO = new Contato_ProspectDAO();
        Contato_ClienteDAO contato_ClienteDAO = new Contato_ClienteDAO();
        ConfigBancoDAO configBancoDAO = new ConfigBancoDAO();
        public EnvioComercialController()
        {
            Nome = "Mailing Comercial";
            Sigla = "MAICOM";
        }

        public override void CarregarConfig()
        {
            configBancoDAO.BuscarConfigs(true);
            Hora = HoraRotina;
        }

        public override void Executar()
        {
            if (Service_Config.CadastroHabilitado)
            {
                CarregarConfig();
                UltimaExecucao = BuscarUltimaExecucao();
                Descricao = "Hora Definida: " + Hora;
                ValidarExecucao();

                try
                {
                    if (Service_Config.CadastroHabilitado && ExecutarRotina)
                    {
                        Guardian_Log.Log_Rotina(Sigla, Nome, Tipo.Iniciado);

                        List<Contato_Lead> lead_ontem = contato_LeadDAO.BuscarLeadsOntem();
                        List<Contato_Lead> lead_hoje = contato_LeadDAO.BuscarLeadsHoje();

                        List<Contato_Prospect> prospect_ontem = contato_ProspectDAO.BuscarProspectsOntem();
                        List<Contato_Prospect> prospect_hoje = contato_ProspectDAO.BuscarProspectsHoje();

                        List<Contato_Cliente> cliente_ontem = contato_ClienteDAO.BuscarClientesOntem();
                        List<Contato_Cliente> cliente_hoje = contato_ClienteDAO.BuscarClientesHoje();

                        PortalEmail portalEmail = new PortalEmail();
                        portalEmail.EnviarDadosComercial(lead_ontem, lead_hoje, prospect_ontem, prospect_hoje, cliente_ontem, cliente_hoje);

                        RegistrarExecucao();
                        Guardian_Log.Log_Rotina(Sigla, Nome, Tipo.Finalizado);
                    }
                }
                catch (Exception ex)
                {
                    Guardian_LogTxt.LogAplicacao(Service_Config.NomeServico, ex.ToString());
                    Guardian_Log.Log_Ocorrencia(Service_Config.NomeServico, "Erro ao executar o serviço.", ex.ToString(), "");
                }
            }
        }
    }
}
