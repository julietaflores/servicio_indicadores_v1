using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Detalle_receptor
    {
        public Tablero tablero { get; set; }
        public List<Devolucion> Lista { get; set; }
    }
}
