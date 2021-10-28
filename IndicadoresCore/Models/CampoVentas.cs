using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class CampoVentas
    {
        public double acumulado_anio_actual { get; set; }
        public double acumulado_mes_actual { get; set; }
        public double acumulado_anio_anterior { get; set; }
        public double acumulado_mes_anterior { get; set; }
    }
}
