using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_locadora_webApi.Domains
    /// <summary>
    /// Classe representa a entidade (tabela) CLIENTE
    /// </summary>
public class ClienteDomain
    {
        public int idCliente { get; set; }
        public string nomeCliente { get; set; }
        public string sobrenomeCliente { get; set; }
        public int CNH { get; set; }
    }
}
