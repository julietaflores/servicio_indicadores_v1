using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class portafolio_infoo
    {
        public decimal Id { get; set; }
        public string descripcion { get; set; }
        public double importe_actual { get; set; }
        public double coste_actual { get; set; }
        public double cantidad_actual { get; set; }
        public double porcentaje_margen_actual { get; set; }



        public string importe_porcentaje { get; set; }
        public string coste_porcentaje { get; set; }
        public string cantidad_porcentaje { get; set; }
        public string porcentaje_margen_actual_porcentaje { get; set; }
    

    }
}
