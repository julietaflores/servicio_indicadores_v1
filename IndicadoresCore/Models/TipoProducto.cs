using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class TipoProducto
    {
        public decimal TipoProductoId { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }

        public List<Producto> listaproducto { get; set; }
    }
}
