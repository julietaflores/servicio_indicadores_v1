using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class CamposPrecioPromedioVentas
    {
        public double acumulado_anio_actual_ventas_netas { get; set; }
        public double acumulado_anio_actual_cantidad_productos_vendidos { get; set; }
        public double acumulado_mes_actual_ventas_netas { get; set; }

        public double acumulado_mes_actual_productos_vendidos { get; set; }

        public double acumulado_anio_anterior_ventas_netas { get; set; }
        public double acumulado_anio_anterior_productos_vendidos { get; set; }
        public double acumulado_mes_anterior_ventas_netas { get; set; }
        public double acumulado_mes_anterior_productos_vendidos { get; set; }
    }
}
