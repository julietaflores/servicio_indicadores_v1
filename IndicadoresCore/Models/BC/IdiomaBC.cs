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
    public class IdiomaBC
    {

        public List<Idioma> CargarBE(DataRow[] dr)
        {
            List<Idioma> lst = new List<Idioma>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public Idioma CargarBE(DataRow dr)
        {
            Idioma obj = new Idioma();

            obj.codigoIdioma = Convert.ToDecimal(dr["codigoIdioma"].ToString());
            obj.abreviaturaIdioma = dr["abreviaturaIdioma"].ToString();
            obj.DetalleIdioma = dr["DetalleIdioma"].ToString();
   
            return obj;
        }


        public List<Idioma> listaIdioma()
        {
            List<Idioma> obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select cp.*  from Idioma cp");
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

        public Idioma datosidioma_x_usuarioid(decimal usuarioid)
        {
            Idioma obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select cp.codigoIdioma, cp.abreviaturaIdioma, cp.DetalleIdioma  from Idioma cp 
inner join Usuario u on u.CODIdioma= cp.codigoIdioma
where u.idUsuario={0}", Convert.ToInt32(usuarioid));
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
