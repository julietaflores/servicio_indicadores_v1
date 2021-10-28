using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Compania
    {
        public decimal IdCompania { get; set; }

        public decimal IdEmpresa { get; set; }

        public decimal IdCompaniaOdoo { get; set; }

        public string Name { get; set; }

        public decimal IdMonedaOdoo { get; set; }
        public string ImagenUrl { get; set; }
        public bool Estado { get; set; }
        public lista_moneda Monedass { get; set; }
        public MonedaCompania MonedaEmpresaa { get; set; }
    }
}
