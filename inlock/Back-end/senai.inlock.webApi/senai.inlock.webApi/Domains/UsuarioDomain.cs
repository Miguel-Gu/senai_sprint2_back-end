using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Domains
{
    public class UsuarioDomain
    {
        public int idUsuario { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string senha { get; set; }
        [Required]
        public int idTipoUsuario { get; set; }
        public TipoUsuarioDomain tipoUsuario { get; set; }
    }
}
