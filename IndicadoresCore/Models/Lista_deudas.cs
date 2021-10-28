using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Lista_deudas
    {
        public string requiereservicioid { get; set; }
        public string servasigid { get; set; }
        public decimal deuda { get; set; }
        public string personanombres { get; set; }
        public string personaapellidos { get; set; }
        public string personadni { get; set; }
        public string personatelefono { get; set; }
        public string personacorreo { get; set; }
        public string servasigfhinicio { get; set; }
        public string servasigfhfin { get; set; }
    }
}
