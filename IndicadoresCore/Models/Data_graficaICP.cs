using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Data_graficaICP
    {
        public List<Double> ingreso_miles { get; set; }
        public List<Double> cantidad_miles { get; set; }
        public List<Double> ppp { get; set; }
        public List<Double> cpp { get; set; }
        public List<Double> ebitda_miles { get; set; }
    }
}
