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
    public class AuxiliarBC
    {

        public List<Auxiliar> CargarBE(DataRow[] dr)
        {
            List<Auxiliar> lst = new List<Auxiliar>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public Auxiliar CargarBE(DataRow dr)
        {
            Auxiliar obj = new Auxiliar();
            obj.AuxiliarId = Convert.ToDecimal(dr["AuxiliarId"].ToString());
            obj.Nombre = dr["Nombre"].ToString();
            return obj;
        }



        public Auxiliar imformacion_lang(decimal idauxiliar, decimal codidioma_user)
        {
            Auxiliar obj = new Auxiliar();
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select au.AuxiliarId, e.EquivalenciaValor as Nombre  from Auxiliar au 
inner join Equivalencia e on e.EquivalenciaObjetoId1= au.AuxiliarId
inner join Idioma id on id.codigoIdioma= e.IdiomaId
where au.AuxiliarId={0} and e.ObjetoId=4 and id.codigoIdioma={1}", Convert.ToInt32(idauxiliar), Convert.ToInt32(codidioma_user));
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
