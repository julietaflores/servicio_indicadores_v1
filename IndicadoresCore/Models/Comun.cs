using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public enum TipoEstado : byte
    {
        SinAccion = 0,
        Insertar = 1,
        Modificar = 2,
        Eliminar = 3
    }
}
