using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace IndicadoresCore.Models
{
    public class Usuario : BEEntidad
    {
        public decimal idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string usuario { get; set; }
        public string passwordd { get; set; }
        public DateTime? fechacreacionusuario { get; set; }
        public decimal CodIdioma { get; set; }
        public bool Estado { get; set; }

        public decimal IdEmpresa { get; set; }


        public List<CategoriaCompania> categoria_roll { get; set; }
        public List<Compania> Companiaa { get; set; }

        public lista_mes Mess { get; set; }
        public lista_anio Anioo { get; set; }
        public Idioma idioma { get; set; }
    }
}
