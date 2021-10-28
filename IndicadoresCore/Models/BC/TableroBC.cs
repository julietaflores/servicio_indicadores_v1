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
    public class TableroBC
    {
        public TableroBC() : base()
        {
        }

        public TableroBC(string cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<Tablero> CargarBE(DataRow[] dr)
        {
            List<Tablero> lst = new List<Tablero>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public Tablero CargarBE(DataRow dr)
        {
            Tablero obj = new Tablero();

            obj.idTablero = Convert.ToDecimal(dr["idTablero"].ToString());
            obj.nombreTablero = dr["nombreTablero"].ToString();
            obj.estadoTablero = Convert.ToBoolean(dr["estadoTablero"].ToString());
            obj.urlTablero = dr["urlTablero"].ToString();
         
            return obj;
        }








        public Tablero datostableroid(decimal idtablero, decimal codigoIdioma)
        {
            Tablero obj = new Tablero();
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select tb.idTablero, e.EquivalenciaValor as nombreTablero, tb.estadoTablero, tb.urlTablero from Tablero  tb 
inner join Equivalencia e on e.EquivalenciaObjetoId1= tb.idTablero
inner join Idioma id on id.codigoIdioma= e.IdiomaId
where tb.IdTablero={0}  and e.ObjetoId=2 and tb.estadoTablero=1 and id.codigoIdioma={1}", Convert.ToInt32(idtablero), Convert.ToInt32(codigoIdioma));
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


        public List<Tablero> listadatostablero(decimal id_categoria, decimal codigoIdioma)
        {
            List<Tablero> obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select tb.idTablero, e.EquivalenciaValor as nombreTablero, tb.estadoTablero, tb.urlTablero, tb.IdCategoria from Tablero  tb 
inner join Equivalencia e on e.EquivalenciaObjetoId1= tb.idTablero
inner join Idioma id on id.codigoIdioma= e.IdiomaId
where tb.IdCategoria={0}  and e.ObjetoId=2 and estadoTablero=1 and id.codigoIdioma={1}  order by tb.orderid asc", Convert.ToInt32(id_categoria), Convert.ToInt32(codigoIdioma));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);

                    CargarRelacionesindicadores(ref obj, codigoIdioma);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }




        public List<Tablero> datostablero_nuevo_menu_lang(decimal idusuario, decimal IdCompania, decimal categoriaid, decimal codidioma)
        {
            List<Tablero> obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select ii.idTablero, e.EquivalenciaValor as nombreTablero,tt.estadoTablero , tt.urlTablero, tt.orderid from lista_menu lm
inner join CategoriaCompania cc on cc.CategoriaCompaniaId= lm.CategoriaCompaniaId
inner join TableroIndicadorCompania tic on tic.idcompania= lm.Idcompania
inner join Indicador ii on ii.IdIndicador = lm.TableroIndicadorCompaniaId
inner join Tablero tt on tt.idtablero= ii.IDtablero
inner join Equivalencia e on e.EquivalenciaObjetoId1= ii.IDtablero
inner join Idioma id on id.codigoIdioma= e.IdiomaId
where  lm.estado=1 and tic.Estado=1  and  tt.estadoTablero=1 and ii.estadoIndicador=1  and e.ObjetoId=2
and  lm.idusuario={0}  and lm.IdCompania={1}  and lm.CategoriaCompaniaId={2} and id.codigoIdioma={3}
group by   ii.idTablero ,e.EquivalenciaValor,tt.estadoTablero, tt.urlTablero ,tt.orderid
order by tt.orderid asc", Convert.ToInt32(idusuario), Convert.ToInt32(IdCompania), Convert.ToInt32(categoriaid), Convert.ToInt32(codidioma));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    

                  CargarRelacionesindicadores1(ref obj, idusuario, IdCompania, categoriaid, codidioma);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }



        public void CargarRelacionesindicadores1(ref List<Tablero> obj, decimal idusuario, decimal IdCompania, decimal categoriaid, decimal codidioma)
        {

            foreach (var lista1 in obj)
            {
                decimal tableroid = lista1.idTablero;

                IndicadorBC bcIndicador = new BC.IndicadorBC("cadenaCnx");

                lista1.indicadores = bcIndicador.datosindicador_nuevo_menu_lang(idusuario,IdCompania,categoriaid,tableroid, codidioma);
                bcIndicador = null;


            }
        }

        public void CargarRelacionesindicadores(ref List<Tablero> obj,decimal codigoIdioma)
        {

            foreach (var lista1 in obj)
            {
                decimal tableroid = lista1.idTablero;

                IndicadorBC bcIndicador = new BC.IndicadorBC("cadenaCnx");

                lista1.indicadores = bcIndicador.listadatosindricaresxtablero(tableroid, codigoIdioma);
                bcIndicador = null;


            }
        }

      






    }
}
