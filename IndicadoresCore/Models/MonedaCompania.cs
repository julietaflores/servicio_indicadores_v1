using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class MonedaCompania
    {
        public decimal MonedaId { get; set; }

        public decimal IdCompania { get; set; }

        public decimal IdMonedaOdoo { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public double Rate { get; set; }
        public bool Estado { get; set; }
    }
}
