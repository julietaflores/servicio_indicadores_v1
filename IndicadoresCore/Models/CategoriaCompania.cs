using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class CategoriaCompania
    {
        public decimal CategoriaCompaniaId { get; set; }

        public string Nombre { get; set; }
        public decimal IdCompania { get; set; }

        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }

        public List<Tablero> tablero { get; set; }
    }
}
