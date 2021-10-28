using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
   

    public class Ranking_Margenes
    {

        public decimal id { get; set; }
        public string nombre { get; set; }
     
        //campos para el calculo
        public double importe_actual { get; set; }
        public double coste_actual { get; set; }
        public double porcentaje_margen_actual { get; set; }

        public double importe_anterior { get; set; }
        public double coste_anterior { get; set; }

        public double porcentaje_margen_anterior { get; set; }

        //campos afuera


        public string BPS { get; set; }
        public string calculo_grafico { get; set; }
        public string porcentajetorta { get; set; }

    }
}
