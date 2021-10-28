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
    public class MesBC
    {

        public List<Mes> CargarBE(DataRow[] dr)
        {
            List<Mes> lst = new List<Mes>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public Mes CargarBE(DataRow dr)
        {
            Mes obj = new Mes();

            obj.Mesid = Convert.ToDecimal(dr["Mesid"].ToString());
            obj.Nombre = dr["Nombre"].ToString();

            return obj;
        }



        public List<Mes> listames_lang(decimal cod_idioma_user)
        {
            List<Mes> obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"
select ms.Mesid, e.EquivalenciaValor as Nombre  from Mes ms 
inner join Equivalencia e on e.EquivalenciaObjetoId1= ms.Mesid
inner join Idioma id on id.codigoIdioma= e.IdiomaId
where  e.ObjetoId=5 and id.codigoIdioma={0}",Convert.ToInt32(cod_idioma_user));
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
