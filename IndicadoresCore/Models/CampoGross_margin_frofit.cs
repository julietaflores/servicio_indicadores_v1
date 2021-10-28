using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class CampoGross_margin_frofit
    {
        public double acumulado_anio_actual_ganacia { get; set; }
        public double acumulado_anio_actual_costo { get; set; }
        public double acumulado_mes_actual_ganacia { get; set; }

        public double acumulado_mes_actual_costo { get; set; }

        public double acumulado_anio_anterior_ganacia { get; set; }
        public double acumulado_anio_anterior_costo { get; set; }
        public double acumulado_mes_anterior_ganacia { get; set; }
        public double acumulado_mes_anterior_costo { get; set; }
    }
}
