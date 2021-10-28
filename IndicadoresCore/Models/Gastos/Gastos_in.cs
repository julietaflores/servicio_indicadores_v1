using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models.Gastos
{
    public class Gastos_in
    {
        public List<Informacion_Gastos> Informacion_Gastos_mes { get; set; }
        public List<Informacion_Gastos> Informacion_Gastos_anual { get; set; }
    }
}
