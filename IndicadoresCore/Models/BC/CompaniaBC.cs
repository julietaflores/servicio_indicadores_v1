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
    public class CompaniaBC
    {
        public List<Compania> CargarBE(DataRow[] dr)
        {
            List<Compania> lst = new List<Compania>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public Compania CargarBE(DataRow dr)
        {
            Compania obj = new Compania();

            obj.IdCompania = Convert.ToDecimal(dr["IdCompania"].ToString());
            obj.IdEmpresa = Convert.ToDecimal(dr["IdEmpresa"].ToString());
            obj.IdCompaniaOdoo = Convert.ToDecimal(dr["IdCompaniaOdoo"].ToString());
            obj.Name = dr["Name"].ToString();
            obj.IdMonedaOdoo = Convert.ToDecimal(dr["IdMonedaOdoo"].ToString());
            obj.ImagenUrl = dr["ImagenUrl"].ToString();
            obj.Estado = Convert.ToBoolean(dr["Estado"].ToString());


            return obj;
        }


        public List<Compania> listaCompaniaxEmoresa(decimal empresaid)
        {
            List<Compania> obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select cp.*  from Compania cp where cp.IdEmpresa={0}", Convert.ToInt32(empresaid));
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


        public List<Compania> listaCompaniaxUser(decimal userid, decimal codidioma)
        {
            List<Compania> obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"
select lm.IdCompania,c.IdEmpresa ,c.IdCompaniaOdoo, c.Name ,c.IdMonedaOdoo,c.ImagenUrl ,c.Estado , ee.nombreEmpresa from lista_menu lm
inner join compania c on lm.idcompania= c.Idcompania
inner join Usuario uu on uu.idUsuario= lm.IdUsuario
inner join empresa ee on ee.Idempresa= ee.idempresa
where lm.idusuario={0} and lm.estado=1  and c.Estado=1 and c.IdEmpresa= uu.IdEmpresa
group by lm.idcompania,c.IdEmpresa ,c.IdCompaniaOdoo, c.Name,c.IdMonedaOdoo ,c.ImagenUrl, c.Estado, ee.nombreEmpresa", Convert.ToInt32(userid));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones_monedas(ref obj, codidioma);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }



        public void CargarRelaciones_monedas(ref List<Compania> obj, decimal codidioma)
        {

            foreach (var lista1 in obj)
            {
                decimal idcompania = lista1.IdCompania;


                MonedaCompaniaBC monedaCompaniaBC = new MonedaCompaniaBC();
                AuxiliarBC auxiliarBC = new AuxiliarBC();
                Auxiliar auxiliar = new Auxiliar();
                auxiliar = auxiliarBC.imformacion_lang(1, codidioma);
                lista_moneda lis_moneda = new lista_moneda();
                lis_moneda.descripcion_moneda = auxiliar;
                lis_moneda.info_moneda = monedaCompaniaBC.listamonedaxcompania(idcompania);

                lista1.Monedass = lis_moneda;

            }



            //  obj.Monedass = lis_moneda;

           //   decimal categoriaid = obj.id_categoria;
           // TableroBC bctablero = new BC.TableroBC("cadenaCnx");
          //  obj.tableross = bctablero.listadatostablero(categoriaid, codidioma);

        }



        public decimal dbempresacompania(decimal userid)
        {
            decimal dbempresa = 0;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select c.IdEmpresa from lista_menu lm
inner join compania c on lm.idcompania= c.Idcompania
where lm.idusuario={0} and lm.estado=1
group by c.IdEmpresa", Convert.ToInt32(userid));


                DataRow dr = conx.ObtenerFila(sql);

                dbempresa = (decimal)dr["IdEmpresa"];
             


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dbempresa;
        }



        public Compania datosempresacompania(decimal empresaid, decimal compania)
        {
            Compania obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select cp.*  from Compania cp where cp.IdEmpresa={0} and cp.IdCompaniaOdoo={1}", Convert.ToInt32(empresaid), Convert.ToInt32(compania));
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



        public Compania datosempresacompaniaid(decimal companiaid)
        {
            Compania obj = null;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select cp.*  from Compania cp where cp.IdCompania={0} ", Convert.ToInt32(companiaid));
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
