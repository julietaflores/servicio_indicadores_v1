using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IndicadoresCore.Models.BC;
using System.Runtime.ExceptionServices;

namespace IndicadoresCore.Models.tabla_9_primero_indicadores
{
    public class Cantidad_1 
    {
    
        IndicadorBC indicadorBC = new IndicadorBC();
        ClaseConexion ClaseConexiond = new ClaseConexion();


        public Devolucion devolver_cantidad_1(Usuario usuario,int anioant, int anio, string mes, Compania info_compania, decimal idDB , MonedaCompania moneda_destino)
        {
            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);
            Devolucion devolucion = new Devolucion();
            Indicador indicadorf = new Indicador();
            indicadorf = indicadorBC.datosindicador_lang(1, usuario.CodIdioma);
            devolucion.idIndicador = indicadorf.idIndicador;
            devolucion.nombreIndicador = indicadorf.nombreIndicador;
            devolucion.vs = anioant.ToString();

            CampoVentas lst = new CampoVentas();


            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[CifrasNotables_Cantidad_1]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@iddbempresa", idDB);
                sqlCmd.Parameters.AddWithValue("@companiaid", info_compania.IdCompaniaOdoo);
                sqlCmd.Parameters.AddWithValue("@monedaid", info_compania.IdMonedaOdoo);
                sqlCmd.Parameters.AddWithValue("@anioact", anio);
                sqlCmd.Parameters.AddWithValue("@anioant", anioant);
                sqlCmd.Parameters.AddWithValue("@mess", mes);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                lst = Conversor.camposventas(dt.Rows[0]);
                conexion.Close();
            }
          

            if (lst.acumulado_anio_actual > 0 && lst.acumulado_anio_anterior == 0)
            {
                double camop = lst.acumulado_anio_actual * moneda_destino.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Acumulado = camop.ToString();
                devolucion.Porcentaje_Monto_Acumulado = "100";
            }

            if (lst.acumulado_anio_actual == 0 && lst.acumulado_anio_anterior > 0)
            {
                double camop = lst.acumulado_anio_actual * moneda_destino.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Acumulado = camop.ToString();
                devolucion.Porcentaje_Monto_Acumulado = "0";
            }

            if (lst.acumulado_anio_actual > 0 && lst.acumulado_anio_anterior > 0)
            {
                double camop = lst.acumulado_anio_actual * moneda_destino.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Acumulado = camop.ToString();


                double diff = (((lst.acumulado_anio_actual / lst.acumulado_anio_anterior)-1)*100);
                double camopp = Math.Round(diff, 2);
                devolucion.Porcentaje_Monto_Acumulado = camopp.ToString();
            }

            if (lst.acumulado_anio_actual == 0 && lst.acumulado_anio_anterior == 0)
            {
                devolucion.Monto_Acumulado = "0";
                devolucion.Porcentaje_Monto_Acumulado = "0";
            }






            if (lst.acumulado_mes_actual > 0 && lst.acumulado_mes_anterior == 0)
            {
                double camop = lst.acumulado_mes_actual * moneda_destino.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Mes = camop.ToString();
                devolucion.Porcentaje_Monto_Mes = "100";
            }

            if (lst.acumulado_mes_actual == 0 && lst.acumulado_mes_anterior > 0)
            {
                double camop = lst.acumulado_mes_actual * moneda_destino.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Mes = camop.ToString();
                devolucion.Porcentaje_Monto_Mes = "0";
            }
            if (lst.acumulado_mes_actual > 0 && lst.acumulado_mes_anterior > 0)
            {
                double camop = lst.acumulado_mes_actual * moneda_destino.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Mes = camop.ToString();


                double diff = (((lst.acumulado_mes_actual / lst.acumulado_mes_anterior) - 1) * 100);
                double camopp = Math.Round(diff, 2);
                devolucion.Porcentaje_Monto_Mes = camopp.ToString();
            }

            if (lst.acumulado_mes_actual == 0 && lst.acumulado_mes_anterior == 0)
            {
                devolucion.Monto_Mes = "0";
                devolucion.Porcentaje_Monto_Mes = "0";
            }




            return devolucion;

        }




        public Devolucion devolver_cantidad_mesanual(int anioant, int anio, string mes, Compania info_compania, decimal idDB,  decimal proidd)
        {
            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);
            Devolucion devolucion = new Devolucion();
            Indicador indicadorf = new Indicador();
            indicadorf = indicadorBC.datosindicador(1);
            devolucion.idIndicador = indicadorf.idIndicador;
            devolucion.nombreIndicador = indicadorf.nombreIndicador;
            devolucion.vs = anioant.ToString();

            CampoVentas lst = new CampoVentas();
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[performancetop5_cantidad]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@iddbempresa", idDB);
                sqlCmd.Parameters.AddWithValue("@companiaid", info_compania.IdCompaniaOdoo);
                sqlCmd.Parameters.AddWithValue("@monedaid", info_compania.IdMonedaOdoo);
                sqlCmd.Parameters.AddWithValue("@anioact", anio);
                sqlCmd.Parameters.AddWithValue("@anioant", anioant);
                sqlCmd.Parameters.AddWithValue("@mess", mes);
                sqlCmd.Parameters.AddWithValue("@proid", proidd);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                lst = Conversor.camposventas(dt.Rows[0]);
                conexion.Close();
            }
                
                
              

            if (lst.acumulado_anio_actual > 0 && lst.acumulado_anio_anterior == 0)
            {
             
                devolucion.Porcentaje_Monto_Acumulado = "100";
            }

            if (lst.acumulado_anio_actual == 0 && lst.acumulado_anio_anterior > 0)
            {
            
                devolucion.Porcentaje_Monto_Acumulado = "0";
            }

            if (lst.acumulado_anio_actual > 0 && lst.acumulado_anio_anterior > 0)
            {
               


           
                double diff = (((lst.acumulado_anio_actual / lst.acumulado_anio_anterior) - 1));
                double camopp = Math.Round(diff, 2);
                devolucion.Porcentaje_Monto_Acumulado = camopp.ToString();
            }

            if (lst.acumulado_anio_actual == 0 && lst.acumulado_anio_anterior == 0)
            {
                devolucion.Monto_Acumulado = "0";
                devolucion.Porcentaje_Monto_Acumulado = "0";
            }






            if (lst.acumulado_mes_actual > 0 && lst.acumulado_mes_anterior == 0)
            {
              
                devolucion.Porcentaje_Monto_Mes = "100";
            }

            if (lst.acumulado_mes_actual == 0 && lst.acumulado_mes_anterior > 0)
            {
             
                devolucion.Porcentaje_Monto_Mes = "0";
            }
            if (lst.acumulado_mes_actual > 0 && lst.acumulado_mes_anterior > 0)
            {
                

            //    double diff = (((lst.acumulado_mes_actual / lst.acumulado_mes_anterior) - 1) * 100);
                double diff = (((lst.acumulado_mes_actual / lst.acumulado_mes_anterior) - 1));
                double camopp = Math.Round(diff, 2);
                devolucion.Porcentaje_Monto_Mes = camopp.ToString();
            }

            if (lst.acumulado_mes_actual == 0 && lst.acumulado_mes_anterior == 0)
            {
                devolucion.Monto_Mes = "0";
                devolucion.Porcentaje_Monto_Mes = "0";
            }




            return devolucion;

        }




        public Devolucion devolver_cantidad_mesanual_performanceregion(int anioant, int anio, string mes, Compania info_compania,  decimal idDB, decimal ciudadid)
        {
            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);
            Devolucion devolucion = new Devolucion();
            Indicador indicadorf = new Indicador();
            indicadorf = indicadorBC.datosindicador(1);
            devolucion.idIndicador = indicadorf.idIndicador;
            devolucion.nombreIndicador = indicadorf.nombreIndicador;
            devolucion.vs = anioant.ToString();
            CampoVentas lst = new CampoVentas();
            if (conexion.State != ConnectionState.Open)
            {

                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[performanceregion_cantidad]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@iddbempresa", idDB);
                sqlCmd.Parameters.AddWithValue("@companiaid", info_compania.IdCompaniaOdoo);
                sqlCmd.Parameters.AddWithValue("@monedaid", info_compania.IdMonedaOdoo);
                sqlCmd.Parameters.AddWithValue("@anioact", anio);
                sqlCmd.Parameters.AddWithValue("@anioant", anioant);
                sqlCmd.Parameters.AddWithValue("@mess", mes);
                sqlCmd.Parameters.AddWithValue("@ciudadid", ciudadid);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                lst = Conversor.camposventas(dt.Rows[0]);
                conexion.Close();
            }
                
                
                
                
                
               

            if (lst.acumulado_anio_actual > 0 && lst.acumulado_anio_anterior == 0)
            {
              
                devolucion.Porcentaje_Monto_Acumulado = "100";
            }

            if (lst.acumulado_anio_actual == 0 && lst.acumulado_anio_anterior > 0)
            {
               
                devolucion.Porcentaje_Monto_Acumulado = "0";
            }

            if (lst.acumulado_anio_actual > 0 && lst.acumulado_anio_anterior > 0)
            {
             
                double diff = (((lst.acumulado_anio_actual / lst.acumulado_anio_anterior) - 1));
                double camopp = Math.Round(diff, 2);
                devolucion.Porcentaje_Monto_Acumulado = camopp.ToString();
            }

            if (lst.acumulado_anio_actual == 0 && lst.acumulado_anio_anterior == 0)
            {
                devolucion.Monto_Acumulado = "0";
                devolucion.Porcentaje_Monto_Acumulado = "0";
            }






            if (lst.acumulado_mes_actual > 0 && lst.acumulado_mes_anterior == 0)
            {
                devolucion.Porcentaje_Monto_Mes = "100";
            }

            if (lst.acumulado_mes_actual == 0 && lst.acumulado_mes_anterior > 0)
            {
               
                devolucion.Porcentaje_Monto_Mes = "0";
            }
            if (lst.acumulado_mes_actual > 0 && lst.acumulado_mes_anterior > 0)
            {
                
                double diff = (((lst.acumulado_mes_actual / lst.acumulado_mes_anterior) - 1));
                double camopp = Math.Round(diff, 2);
                devolucion.Porcentaje_Monto_Mes = camopp.ToString();
            }

            if (lst.acumulado_mes_actual == 0 && lst.acumulado_mes_anterior == 0)
            {
                devolucion.Monto_Mes = "0";
                devolucion.Porcentaje_Monto_Mes = "0";
            }




            return devolucion;

        }










        public Devolucion devolver_cantidad_mesanual_performancelineal(int anioant, int anio, string mes, Compania compania1, int compania, decimal idDB, MonedaCompania moneda1, decimal ciudadid)
        {
            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);
            Devolucion devolucion = new Devolucion();
            Indicador indicadorf = new Indicador();
            indicadorf = indicadorBC.datosindicador(1);
            devolucion.idIndicador = indicadorf.idIndicador;
            devolucion.nombreIndicador = indicadorf.nombreIndicador;
            devolucion.vs = anioant.ToString();
            CampoVentas lst = new CampoVentas();
            if (conexion.State != ConnectionState.Open)
            {

                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[performanceregion_cantidad]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@iddbempresa", idDB);
                sqlCmd.Parameters.AddWithValue("@companiaid", compania);
                sqlCmd.Parameters.AddWithValue("@monedaid", compania1.IdMonedaOdoo);
                sqlCmd.Parameters.AddWithValue("@anioact", anio);
                sqlCmd.Parameters.AddWithValue("@anioant", anioant);
                sqlCmd.Parameters.AddWithValue("@mess", mes);
                sqlCmd.Parameters.AddWithValue("@ciudadid", ciudadid);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                lst = Conversor.camposventas(dt.Rows[0]);
                conexion.Close();
            }







            if (lst.acumulado_anio_actual > 0 && lst.acumulado_anio_anterior == 0)
            {
                double camop = lst.acumulado_anio_actual * moneda1.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Acumulado = camop.ToString();
                devolucion.Porcentaje_Monto_Acumulado = "100";
            }

            if (lst.acumulado_anio_actual == 0 && lst.acumulado_anio_anterior > 0)
            {
                double camop = lst.acumulado_anio_actual * moneda1.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Acumulado = camop.ToString();
                devolucion.Porcentaje_Monto_Acumulado = "0";
            }

            if (lst.acumulado_anio_actual > 0 && lst.acumulado_anio_anterior > 0)
            {
                double camop = lst.acumulado_anio_actual * moneda1.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Acumulado = camop.ToString();


                //   double diff = (((lst.acumulado_anio_actual / lst.acumulado_anio_anterior) - 1) * 100);
                double diff = (((lst.acumulado_anio_actual / lst.acumulado_anio_anterior) - 1));
                double camopp = Math.Round(diff, 2);
                devolucion.Porcentaje_Monto_Acumulado = camopp.ToString();
            }

            if (lst.acumulado_anio_actual == 0 && lst.acumulado_anio_anterior == 0)
            {
                devolucion.Monto_Acumulado = "0";
                devolucion.Porcentaje_Monto_Acumulado = "0";
            }






            if (lst.acumulado_mes_actual > 0 && lst.acumulado_mes_anterior == 0)
            {
                double camop = lst.acumulado_mes_actual * moneda1.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Mes = camop.ToString();
                devolucion.Porcentaje_Monto_Mes = "100";
            }

            if (lst.acumulado_mes_actual == 0 && lst.acumulado_mes_anterior > 0)
            {
                double camop = lst.acumulado_mes_actual * moneda1.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Mes = camop.ToString();
                devolucion.Porcentaje_Monto_Mes = "0";
            }
            if (lst.acumulado_mes_actual > 0 && lst.acumulado_mes_anterior > 0)
            {
                double camop = lst.acumulado_mes_actual * moneda1.Rate;
                camop = Math.Round(camop, 2);
                devolucion.Monto_Mes = camop.ToString();


                //    double diff = (((lst.acumulado_mes_actual / lst.acumulado_mes_anterior) - 1) * 100);
                double diff = (((lst.acumulado_mes_actual / lst.acumulado_mes_anterior) - 1));
                double camopp = Math.Round(diff, 2);
                devolucion.Porcentaje_Monto_Mes = camopp.ToString();
            }

            if (lst.acumulado_mes_actual == 0 && lst.acumulado_mes_anterior == 0)
            {
                devolucion.Monto_Mes = "0";
                devolucion.Porcentaje_Monto_Mes = "0";
            }




            return devolucion;

        }








    }
}
