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
    public class CategoriaCompaniaBC
    {
        public List<CategoriaCompania> CargarBE(DataRow[] dr)
        {
            List<CategoriaCompania> lst = new List<CategoriaCompania>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public CategoriaCompania CargarBE(DataRow dr)
        {
            CategoriaCompania obj = new CategoriaCompania();

            obj.CategoriaCompaniaId = Convert.ToDecimal(dr["CategoriaCompaniaId"].ToString());
            obj.Nombre = dr["Nombre"].ToString();
            obj.IdCompania = Convert.ToDecimal(dr["IdCompania"].ToString());
            obj.FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString());
            obj.Estado = Convert.ToBoolean(dr["Estado"].ToString());
 
            return obj;
        }


        //public List<CategoriaCompania> listadatoscategoriarolusuario(decimal id_rolusuario)
        //{
        //    List<CategoriaCompania> obj = null;
        //    ClaseConexion conx = new ClaseConexion("cadenaCnx");
        //    try
        //    {
        //        string sql = String.Format(@"select cr.* from Categoria_Rol cr where cr.ID_rolUsuario={0}", Convert.ToInt32(id_rolusuario));
        //        DataRow[] dr = conx.ObtenerFilas(sql);
        //        if (dr != null)
        //        {
        //            obj = CargarBE(dr);


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return obj;
        //}


        public List<CategoriaCompania> listadatoscategoriacompania_lista_menu(decimal id_usuario, decimal codidioma, decimal companiaid)
        {
            List<CategoriaCompania> obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"
select  cc.CategoriaCompaniaId,e.EquivalenciaValor as Nombre, cc.IdCompania,cc.FechaRegistro ,lm.Estado from lista_menu lm
inner join compania c on lm.idcompania= c.Idcompania
inner join CategoriaCompania cc on cc.CategoriaCompaniaId= lm.CategoriaCompaniaId
inner join Equivalencia e on e.EquivalenciaObjetoId1= cc.CategoriaCompaniaId
inner join Idioma id on id.codigoIdioma= e.IdiomaId
where  lm.estado=1  and e.ObjetoId=1 and  lm.idusuario={0}  and c.IdCompania={1}  and id.codigoIdioma={2}
group by cc.CategoriaCompaniaId,e.EquivalenciaValor, cc.IdCompania,cc.FechaRegistro ,lm.Estado
", Convert.ToInt32(id_usuario), Convert.ToInt32(companiaid), Convert.ToInt32(codidioma));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones_tablero(ref obj, id_usuario,codidioma);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        // poner estado



        public void CargarRelaciones_tablero(ref List<CategoriaCompania> obj, decimal idusuario ,decimal codidioma)
        {

            foreach (var lista1 in obj)
            {
                decimal categoriaid = lista1.CategoriaCompaniaId;
                TableroBC tableroBC = new TableroBC();
                lista1.tablero = tableroBC.datostablero_nuevo_menu_lang(idusuario, lista1.IdCompania,categoriaid, codidioma);
                tableroBC = null;


            }


        }

    }
}
