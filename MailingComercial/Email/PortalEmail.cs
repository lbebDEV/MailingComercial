using MailingComercial.Config;
using MailingComercial.Guardian;
using MailingComercial.Model;
using MailingComercial.ServiceLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MailingComercial.Email
{
    class PortalEmail
    {
        public void EnviarDadosComercial(List<Contato_Lead> leadsOntem, List<Contato_Lead> leadsHoje, List<Contato_Prospect> prospectOntem, List<Contato_Prospect> prospectHoje, List<Contato_Cliente> clienteOntem, List<Contato_Cliente> clienteHoje)
        {
            List<string> emailsDestino = new List<string>();
            emailsDestino.Add("comercial@lbeb.com.br");
            emailsDestino.Add("all@lbeb.com.br");

            try
            {
                GuardianEmail email = Email_Config.CarregarConfiguracoes();

                using (StreamReader html = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/Template/LayoutComercial.html"))
                {
                    email.Mensagem = html.ReadToEnd();
                }


                int TotalLeadsOntem = 0;
                int TotalLeadsHoje = 0;
                int TotalProspectsOntem = 0;
                int TotalProspectsHoje = 0;
                int TotalClientesOntem = 0;
                int TotalClientesHoje = 0;

                string contatoLeadsOntem = "";
                foreach (var item in leadsOntem)
                {
                    contatoLeadsOntem +=
                 "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                        item.Razao +
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         item.NomeContato +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px; '>" +
                         item.Observacao +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         item.ObservacaoProximo +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         Guardian_Util.FormatarData(Guardian_Util.FormatarData(item.DataProximo)) +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         item.HoraProximo +
                     "</td>" +
                 "</tr>";
                    TotalLeadsOntem++;
                }
                if (String.IsNullOrEmpty(contatoLeadsOntem))
                {
                    contatoLeadsOntem +=
                 "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                        "SEM DADOS"+
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                          "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                          "SEM DADOS" +
                     "</td>" +
                 "</tr>";
                }

                string contatoProspectOntem = "";
                foreach (var item in prospectOntem)
                {
                    contatoProspectOntem +=
                 "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                        item.Razao +
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         item.NomeContato +
                     "</td>" +
                      "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         item.Observacao +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         item.ObservacaoProximo +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         Guardian_Util.FormatarData(item.DataProximo) +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         item.HoraProximo +
                     "</td>" +
                 "</tr>";
                    TotalProspectsOntem++;
                }
                if (String.IsNullOrEmpty(contatoProspectOntem))
                {
                    contatoProspectOntem +=
                 "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                        "SEM DADOS" +
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                          "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                          "SEM DADOS" +
                     "</td>" +
                 "</tr>";
                }

                string contatoClienteOntem = "";
                foreach (var item in clienteOntem)
                {
                    contatoClienteOntem +=
                 "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                        item.Razao +
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         item.NomeContato +
                     "</td>" +
                      "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; '>" +
                         item.Observacao +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         item.ObservacaoProximo +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         Guardian_Util.FormatarData(item.DataProximo) +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         item.HoraProximo +
                     "</td>" +
                 "</tr>";
                    TotalClientesOntem++;
                }
                if (String.IsNullOrEmpty(contatoClienteOntem))
                {
                    contatoClienteOntem +=
                 "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                        "SEM DADOS" +
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                          "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                          "SEM DADOS" +
                     "</td>" +
                 "</tr>";
                }
                
                string contatoLeadsHoje = "";
                foreach (var item in leadsHoje)
                {
                    contatoLeadsHoje +=
                 "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                        item.Razao +
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         item.NomeContato +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         item.Observacao +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         item.ObservacaoProximo +
                     "</td>" +
                 "</tr>";
                    TotalLeadsHoje++;
                }
                if (String.IsNullOrEmpty(contatoLeadsHoje))
                {
                    contatoLeadsHoje +=
                 "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                        "SEM DADOS" +
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                          "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         "SEM DADOS" +
                     "</td>" +
                 "</tr>";
                }

                string contatoProspectsHoje = "";
                foreach (var item in prospectHoje)
                {
                    contatoProspectsHoje +=
                  "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                        item.Razao +
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         item.NomeContato +
                     "</td>" +
                      "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         item.Observacao +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         item.ObservacaoProximo +
                     "</td>" +
                 "</tr>";
                    TotalProspectsHoje++;
                }
                if (String.IsNullOrEmpty(contatoProspectsHoje))
                {
                    contatoProspectsHoje +=
                 "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                        "SEM DADOS" +
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                          "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         "SEM DADOS" +
                     "</td>" +
                 "</tr>";
                }

                string contatoClientesHoje = "";
                foreach (var item in clienteHoje)
                {
                    contatoClientesHoje +=
                  "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                        item.Razao +
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         item.NomeContato +
                     "</td>" +
                      "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         item.Observacao +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         item.ObservacaoProximo +
                     "</td>" +
                 "</tr>";
                    TotalClientesHoje++;
                }
                if (String.IsNullOrEmpty(contatoClientesHoje))
                {
                    contatoClientesHoje +=
                 "<tr style='vertical-align: top;' align='center'> " +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                        "SEM DADOS" +
                    "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                          "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black; border-right: 0px;'>" +
                         "SEM DADOS" +
                     "</td>" +
                     "<td colspan='1' align='center' style='font-weight:normal; font-size: 12px; border: thin solid black;'>" +
                         "SEM DADOS" +
                     "</td>" +
                 "</tr>";
                }
                
                email.Mensagem = email.Mensagem.Replace("#CLO", TotalLeadsOntem.ToString());
                email.Mensagem = email.Mensagem.Replace("#CPO", TotalProspectsOntem.ToString());
                email.Mensagem = email.Mensagem.Replace("#CCO", TotalClientesOntem.ToString());
                email.Mensagem = email.Mensagem.Replace("#CLH", TotalLeadsHoje.ToString());
                email.Mensagem = email.Mensagem.Replace("#CPH", TotalProspectsHoje.ToString());
                email.Mensagem = email.Mensagem.Replace("#CCH", TotalClientesHoje.ToString());
                
                email.Mensagem = email.Mensagem.Replace("#ContatoLeadsOntem", contatoLeadsOntem);
                email.Mensagem = email.Mensagem.Replace("#ContatoProspectsOntem", contatoProspectOntem);
                email.Mensagem = email.Mensagem.Replace("#ContatoClienteOntem", contatoClienteOntem);
                email.Mensagem = email.Mensagem.Replace("#ContatoLeadsHoje", contatoLeadsHoje);
                email.Mensagem = email.Mensagem.Replace("#ContatoProspectsHoje", contatoProspectsHoje);
                email.Mensagem = email.Mensagem.Replace("#ContatoClienteHoje", contatoClientesHoje);
                email.Mensagem = email.Mensagem.Replace("#DataOntem", DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
                email.Mensagem = email.Mensagem.Replace("#DataHoje", DateTime.Now.ToString("dd/MM/yyyy"));
                email.Mensagem = email.Mensagem.Replace("#DiaSemanaOntem", DateTime.Now.AddDays(-1).ToString("ddd", new CultureInfo("pt-BR")));
                email.Mensagem = email.Mensagem.Replace("#DiaSemanaHoje", DateTime.Now.ToString("ddd", new CultureInfo("pt-BR")));


                email.Assunto = "Mailing Comercial | " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                email.Mensagem = email.Mensagem;
                email.EmailsDestinatario = emailsDestino;

                if (!String.IsNullOrEmpty(Service_Config.EmailValidacao))
                    email.EmailsDestinatario = new List<string> { Service_Config.EmailValidacao };

                if (email.Enviar())
                    Guardian_Log.Log_Email(email.EmailsDestinatario[0], "Envio de Mailing do Dia", Status.Sucesso, "Mailing Comercial");
                else
                    Guardian_Log.Log_Email(email.EmailsDestinatario[0], "Envio de Mailing do Dia", Status.Falha, "Mailing Comercial");
            }
            catch (Exception ex)
            {
                Guardian_Log.Log_Ocorrencia("Enviar Mailing Comercial", "Erro ao enviar email", ex.ToString(), "Email: " + emailsDestino[0]);
            }
        }
    }
}
