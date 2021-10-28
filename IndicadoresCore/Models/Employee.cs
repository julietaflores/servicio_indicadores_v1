using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace IndicadoresCore.Models
{
    public class Employee:BEEntidad
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public bool Estado { get; set; }

    }
}
