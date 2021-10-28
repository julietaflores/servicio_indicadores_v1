using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Tablero
    {
        public decimal idTablero { get; set; }

        public string nombreTablero { get; set; }
        public bool estadoTablero { get; set; }

        public string urlTablero { get; set; }
        public List<Indicador> indicadores { get; set; }
    }
}
