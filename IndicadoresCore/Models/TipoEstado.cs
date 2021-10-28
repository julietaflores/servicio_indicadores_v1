using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class TipoEstadoNotificacion : BEEntidad
    {
        public decimal TipoEstadoId { get; set; }
        public string TipoEstadoNombre { get; set; }
    }
}
