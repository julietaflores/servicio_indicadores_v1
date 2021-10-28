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
    public class MonedaCompaniaBC
    {
        public List<MonedaCompania> CargarBE(DataRow[] dr)
        {
            List<MonedaCompania> lst = new List<MonedaCompania>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public MonedaCompania CargarBE(DataRow dr)
        {
            MonedaCompania obj = new MonedaCompania();

            obj.MonedaId = Convert.ToDecimal(dr["MonedaId"].ToString());
            obj.IdCompania = Convert.ToDecimal(dr["IdCompania"].ToString());
            obj.IdMonedaOdoo = Convert.ToDecimal(dr["IdMonedaOdoo"].ToString());
            obj.Name = dr["Name"].ToString();
            obj.Symbol = dr["Symbol"].ToString();
            obj.Rate = Convert.ToDouble(dr["Rate"].ToString());
            obj.Estado = Convert.ToBoolean(dr["Estado"].ToString());


            return obj;
        }


        public List<MonedaCompania> listamonedaxcompania(decimal companiaid)
        {
            List<MonedaCompania> obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select mm.*  from MonedaCompania mm where mm.IdCompania={0} and mm.Estado=1", Convert.ToInt32(companiaid));
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



   


        public MonedaCompania datoscompaniamoneda(decimal companiaid, decimal moneda)
        {
            MonedaCompania obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select mm.*  from MonedaCompania mm where mm.IdCompania={0}  and mm.IdMonedaOdoo={1}", Convert.ToInt32(companiaid), Convert.ToInt32(moneda));
                DataRow dr = conx.ObtenerFila(sql);
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
