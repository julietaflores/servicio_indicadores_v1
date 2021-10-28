using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Ranking
    {
        public decimal idPosicion { get; set; }
        public string nombre { get; set; }
        public double importeactual { get; set; }
        public double importeanterior { get; set; }
        public string porcentajetorta { get; set; }
        public  Ranking_Lista detalle_Receptor { get; set; }

    }
}
