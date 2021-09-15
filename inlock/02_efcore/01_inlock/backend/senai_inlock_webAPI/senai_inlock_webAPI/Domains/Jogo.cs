using System;
using System.Collections.Generic;

#nullable disable

namespace senai_inlock_webAPI.Domains
{
    public partial class Jogo
    {
        public byte IdJogo { get; set; }
        public string NomeJogo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataLancamento { get; set; }
        public decimal Valor { get; set; }
        public byte IdEstudio { get; set; }

        public virtual Estudio IdEstudioNavigation { get; set; }
    }
}
