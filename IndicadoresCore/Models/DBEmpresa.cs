using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class DBEmpresa
    {
        public decimal idDB { get; set; }

        public string UrlBase { get; set; }

        public string NombreDB { get; set; }

        public string Usuario { get; set; }
        public string Password { get; set; }

        public string Version { get; set; }
        public decimal IdEmpresa { get; set; }
    }
}
