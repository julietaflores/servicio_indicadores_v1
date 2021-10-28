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
    public class CategoriaBC
    {
        public CategoriaBC() : base()
        {
        }

        public CategoriaBC(string cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<Categoria> CargarBE(DataRow[] dr)
        {
            List<Categoria> lst = new List<Categoria>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public Categoria CargarBE(DataRow dr)
        {
            Categoria obj = new Categoria();

            obj.id_categoria = Convert.ToDecimal(dr["idCategoria"].ToString());
            obj.nombrecategoria = dr["nombreCategoria"].ToString();
            obj.estadoCategoria = Convert.ToBoolean(dr["estadoCategoria"].ToString());
            obj.idcategoriaPadre = dr.Field<Decimal?>("IdCategoriaPadre");

            return obj;
        }


        public List<Categoria> listadatoscategoriarolusuario(decimal id_rolusuario)
        {
            List<Categoria> obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select cr.* from Categoria cr where cr.ID_rolUsuario={0}", Convert.ToInt32(id_rolusuario));
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



        public Categoria listadatoscategoriarolusuario1(decimal idcategoria, decimal codigoIdioma)
        {
            Categoria obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select ct.* from Categoria ct  where ct.idCategoria={0}", Convert.ToInt32(idcategoria));
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelacionesTablero(ref obj, codigoIdioma);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        public Categoria listadatoscategoriarolusuario1_lang(decimal idcategoria, decimal codidioma)
        {
            Categoria obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select ct.idCategoria, e.EquivalenciaValor as nombreCategoria , ct.estadoCategoria,ct.IdCategoriaPadre from Categoria ct 
inner join Equivalencia e on e.EquivalenciaObjetoId1= ct.idCategoria
inner join Idioma id on id.codigoIdioma= e.IdiomaId
where ct.idCategoria={0} and e.ObjetoId=1 and id.codigoIdioma={1}", Convert.ToInt32(idcategoria), Convert.ToInt32(codidioma));
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelacionesTablero(ref obj,codidioma);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }




        public void CargarRelacionesTablero(ref Categoria obj, decimal codidioma)
        {

            decimal categoriaid = obj.id_categoria;

            TableroBC bctablero = new BC.TableroBC("cadenaCnx");
            obj.tableross = bctablero.listadatostablero(categoriaid, codidioma); 

        }
      

    }
}
