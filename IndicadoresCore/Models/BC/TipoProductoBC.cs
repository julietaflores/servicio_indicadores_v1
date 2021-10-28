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
    public class TipoProductoBC
    {

        public TipoProductoBC() : base()
        {
        }

        public TipoProductoBC(string cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<TipoProducto> CargarBE(DataRow[] dr)
        {
            List<TipoProducto> lst = new List<TipoProducto>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public TipoProducto CargarBE(DataRow dr)
        {
            TipoProducto obj = new TipoProducto();

            obj.TipoProductoId = Convert.ToDecimal(dr["TipoProductoId"].ToString());
            obj.Nombre = dr["Nombre"].ToString();
            obj.Detalle = dr["Detalle"].ToString();
        

            return obj;
        }


        //public List<TipoProducto> listadatostablero()
        //{
        //    List<TipoProducto> obj = null;
        //    ClaseConexion conx = new ClaseConexion("cadenaCnx");
        //    try
        //    {
        //        string sql = String.Format(@"select tp.* from  TipoProducto tp");
        //        DataRow[] dr = conx.ObtenerFilas(sql);
        //        if (dr != null)
        //        {
        //            obj = CargarBE(dr);

        //            CargarRelacionesproducto(ref obj);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return obj;
        //}


        //public void CargarRelacionesproducto(ref List<TipoProducto> obj)
        //{

        //    foreach (var lista1 in obj)
        //    {
        //        decimal tipoproductoid = lista1.TipoProductoId;
        //        ProductoBC bcproducto = new BC.ProductoBC("cadenaCnx");
        //        lista1.listaproducto= bcproducto.
        //        bcproducto = null;


        //    }
        //}


     

    }
}
