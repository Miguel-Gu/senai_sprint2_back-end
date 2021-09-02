using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_locadora_webApi.Domains
{   /// <summary>
    /// Classe representa a entidade (tabela) MODELO
    /// </summary>
    public class ModeloDomain
    {
        public int idModelo { get; set; }
        public string nomeModelo { get; set; }
        public int idMarca { get; set; }
        public MarcaDomain marca { get; set; }
    }
}
