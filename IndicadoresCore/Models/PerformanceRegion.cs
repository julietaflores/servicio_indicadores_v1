using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class PerformanceRegion
    {
        public Tablero tablero { get; set; }
        public List<Ranking> Listames { get; set; }

        public List<Ranking> Listaanual { get; set; }


    }
}
