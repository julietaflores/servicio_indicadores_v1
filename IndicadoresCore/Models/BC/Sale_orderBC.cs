using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IndicadoresCore.Models;


namespace IndicadoresCore.Models.BC
{
    public class Sale_orderBC
    {
        public Sale_orderBC() : base()
        {
        }

        public Sale_orderBC(string cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }





        public List<Sale_order> CargarBE(DataRow[] dr)
        {
            List<Sale_order> lst = new List<Sale_order>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public Sale_order CargarBE(DataRow dr)
        {
            Sale_order obj = new Sale_order();

            obj.idsaleorder = Convert.ToDecimal(dr["idsaleorder"].ToString());
            obj.id = Convert.ToDecimal(dr["id"].ToString());
            obj.name = dr["name"].ToString();
            obj.userid = Convert.ToDecimal(dr["userid"].ToString());
            obj.date_order = Convert.ToDateTime(dr["date_order"].ToString());
            obj.partner_id = Convert.ToDecimal(dr["partner_id"].ToString());
            obj.currency_id = Convert.ToDecimal(dr["currency_id"].ToString());
            obj.note = dr["note"].ToString();
            obj.amount_total = Convert.ToDouble(dr["amount_total"].ToString());
            obj.amount_untaxed = Convert.ToDouble(dr["amount_untaxed"].ToString());
            obj.amount_tax = Convert.ToDouble(dr["amount_tax"].ToString());
            obj.company_id = Convert.ToDecimal(dr["company_id"].ToString());
            obj.warehouse_id = Convert.ToDecimal(dr["warehouse_id"].ToString());
            obj.dbempresaid = Convert.ToDecimal(dr["dbempresaid"].ToString());
            return obj;
        }


        public List<Sale_order> listasale_roder_por_base(decimal dbempresaid)
        {
            List<Sale_order> obj = new List<Sale_order>();
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select s.* from Sale_Order s where s.dbempresaid={0}", Convert.ToInt32(dbempresaid));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

    }
}
