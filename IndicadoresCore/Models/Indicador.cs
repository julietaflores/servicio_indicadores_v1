using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Indicador
    {
        public decimal idIndicador { get; set; }

        public string nombreIndicador { get; set; }
        public bool estadoIndicador { get; set; }

        public decimal IDTablero { get; set; }
    }
}
