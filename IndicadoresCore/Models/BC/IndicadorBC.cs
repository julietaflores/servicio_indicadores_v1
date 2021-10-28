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
    public class IndicadorBC
    {
        public IndicadorBC() : base()
        {
        }

        public IndicadorBC(string cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<Indicador> CargarBE(DataRow[] dr)
        {
            List<Indicador> lst = new List<Indicador>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public Indicador CargarBE(DataRow dr)
        {
            Indicador obj = new Indicador();

            obj.idIndicador = Convert.ToDecimal(dr["idIndicador"].ToString());
            obj.nombreIndicador = dr["nombreIndicador"].ToString();
            obj.estadoIndicador = Convert.ToBoolean(dr["estadoIndicador"].ToString());
        //    obj.IDTablero = Convert.ToDecimal(dr["IDtablero"].ToString());

            return obj;
        }








        public List<Indicador> datosindicador_nuevo_menu_lang(decimal idusuario, decimal IdCompania, decimal categoriaid, decimal idtablero, decimal codidioma)
        {
            List<Indicador> obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select ii.idIndicador,ii.nombreIndicador  as nom_indicador ,e.EquivalenciaValor as nombreIndicador, ii.estadoIndicador,ii.IDTablero, ii.orderid from lista_menu lm
inner join CategoriaCompania cc on cc.CategoriaCompaniaId= lm.CategoriaCompaniaId
inner join TableroIndicadorCompania tic on tic.idcompania= lm.Idcompania
inner join Indicador ii on ii.IdIndicador= lm.TableroIndicadorCompaniaId
inner join Tablero tt on tt.idTablero= ii.IDtablero
inner join Equivalencia e on e.EquivalenciaObjetoId1= ii.idIndicador
inner join Idioma id on id.codigoIdioma= e.IdiomaId
where  lm.estado=1 and tic.Estado=1  and  tt.estadoTablero=1 and ii.estadoIndicador=1  and e.ObjetoId=3 and
lm.IdUsuario={0} and lm.IdCompania={1} and lm.Categoriacompaniaid={2} and ii.IDtablero={3}  and id.codigoIdioma={4}
group by  ii.idIndicador,ii.nombreIndicador,e.EquivalenciaValor, ii.estadoIndicador,ii.IDTablero , ii.orderid
order by ii.orderid asc", Convert.ToInt32(idusuario), Convert.ToInt32(IdCompania), Convert.ToInt32(categoriaid),Convert.ToInt32(idtablero), Convert.ToInt32(codidioma));
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


        public List<Indicador> listadatosindricaresxtablero(decimal idtablero, decimal codigoIdioma)
        {
            List<Indicador> obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"
select i.IdIndicador, e.EquivalenciaValor as nombreIndicador, i.estadoIndicador, i.IDtablero from Indicador i
inner join Equivalencia e on e.EquivalenciaObjetoId1= i.IdIndicador
inner join Idioma id on id.codigoIdioma= e.IdiomaId
where i.IDtablero={0} and estadoIndicador=1 and e.ObjetoId=3 and id.codigoIdioma={1} order by i.orderid asc", Convert.ToInt32(idtablero), Convert.ToInt32(codigoIdioma));
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


        public Indicador datosindicador(decimal indicadorid)
        {
            Indicador obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select ii.* from Indicador ii where ii.IdIndicador={0}", Convert.ToInt32(indicadorid));
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



        public Indicador datosindicador_lang(decimal indicadorid, decimal codigoIdioma)
        {
            Indicador obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select i.IdIndicador, e.EquivalenciaValor as nombreIndicador, i.estadoIndicador, i.IDtablero from Indicador i
inner join Equivalencia e on e.EquivalenciaObjetoId1= i.IdIndicador
inner join Idioma id on id.codigoIdioma= e.IdiomaId
where i.IdIndicador={0} and estadoIndicador=1 and e.ObjetoId=3 and id.codigoIdioma={1} order by i.orderid asc", Convert.ToInt32(indicadorid), Convert.ToInt32(codigoIdioma));
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
