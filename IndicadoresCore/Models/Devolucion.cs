using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Devolucion
    {

        public decimal idIndicador { get; set; }

        public string nombreIndicador { get; set; }

        public string Monto_Mes { get; set; }

        public string Porcentaje_Monto_Mes { get; set; }

        public string Monto_Acumulado { get; set; }

        public string Porcentaje_Monto_Acumulado { get; set; }
        public string vs { get; set; }


    }
}
