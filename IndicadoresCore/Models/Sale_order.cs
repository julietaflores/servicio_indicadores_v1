using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Models
{
    public class Sale_order
    {
        public decimal idsaleorder { get; set; }
        public decimal id { get; set; }
        public string name { get; set; }
        public decimal userid { get; set; }
        public DateTime date_order { get; set; }
        public decimal partner_id { get; set; }

        public decimal currency_id { get; set; }
        public string note { get; set; }
        public double amount_total { get; set; }
        public double amount_untaxed { get; set; }
        public double amount_tax { get; set; }
        public decimal company_id { get; set; }
        public decimal warehouse_id { get; set; }

        public decimal dbempresaid { get; set; }

    }
}
