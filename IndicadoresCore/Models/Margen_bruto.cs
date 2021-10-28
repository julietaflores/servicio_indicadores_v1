using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Margen_bruto
    {
        public Tablero tablero { get; set; }
        public List<Ranking_Margenes> Lista_mes { get; set; }
        public List<Ranking_Margenes> Lista_anual { get; set; }
    }
}
