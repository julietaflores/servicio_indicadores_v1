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
    public class ProductoBC
    {
        public ProductoBC() : base()
        {
        }

        public ProductoBC(string cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<Producto> CargarBE(DataRow[] dr)
        {
            List<Producto> lst = new List<Producto>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public Producto CargarBE(DataRow dr)
        {
            Producto obj = new Producto();
            obj.ProdId = Convert.ToDecimal(dr["ProdId"].ToString());
            obj.Name = dr["Name"].ToString();
            return obj;
        }


        //public List<Producto> listadatostablero(decimal id_categoria)
        //{
        //    List<Producto> obj = null;
        //    ClaseConexion conx = new ClaseConexion("cadenaCnx");
        //    try
        //    {
        //        string sql = String.Format(@"select tb.* from Producto  tb where tb.IdCategoria={0}", Convert.ToInt32(id_categoria));
        //        DataRow[] dr = conx.ObtenerFilas(sql);
        //        if (dr != null)
        //        {
        //            obj = CargarBE(dr);

        //            CargarRelacionesindicadores(ref obj);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return obj;
        //}


        //public void CargarRelacionesindicadores(ref List<Producto> obj)
        //{

        //    foreach (var lista1 in obj)
        //    {
        //        decimal tableroid = lista1.idProducto;

        //        IndicadorBC bcIndicador = new BC.IndicadorBC("cadenaCnx");

        //        lista1.indicadores = bcIndicador.listadatosindricaresxtablero(tableroid);
        //        bcIndicador = null;


        //    }
        //}


        public Producto datostableroid(decimal idtablero)
        {
            Producto obj = new Producto();
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select t.* from Producto t where t.idProducto={0}", Convert.ToInt32(idtablero));
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
