using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailingComercial.Model
{
    public class Contato_Prospect
    {
        public string Razao { get; set; }
        public string NomeContato { get; set; }
        public string Observacao { get; set; }
        public string ObservacaoProximo { get; set; }
        public string DataProximo { get; set; }
        public string HoraProximo { get; set; }
    }
}
