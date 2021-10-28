using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models.Gastos
{
    public class Gastos_tablero
    {
        public Tablero tablero { get; set; }
        public List<Gastos_li_MA> informacion { get; set; }

    }
}
