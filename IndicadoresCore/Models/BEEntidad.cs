using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class BEEntidad
    {
        private TipoEstado enuTipoEstado = TipoEstado.SinAccion;


        public TipoEstado TipoEstado
        {
            get
            {
                return enuTipoEstado;
            }
            set
            {
                enuTipoEstado = value;
            }
        }


        public BEEntidad Clonar()
        {
            return (BEEntidad)this.MemberwiseClone();
        }
    }
}
