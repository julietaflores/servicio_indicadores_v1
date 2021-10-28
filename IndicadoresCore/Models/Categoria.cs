using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Categoria
    {
        public decimal id_categoria { get; set; }

        public string nombrecategoria { get; set; }

        public bool estadoCategoria { get; set; }
        public decimal? idcategoriaPadre { get; set; }

        public List<Tablero> tableross { get; set; }
    }
}
