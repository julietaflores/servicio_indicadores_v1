using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Performance_indicadores
    {
        public Indicador indicador { get; set; }
        public List<Ranking> Lista_mes { get; set; }
        public List<Ranking> Lista_anual { get; set; }

    }
}
