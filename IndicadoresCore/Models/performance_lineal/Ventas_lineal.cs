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


namespace IndicadoresCore.Models.performance_lineal
{
    public class Ventas_lineal
    {
        IndicadorBC indicadorBC = new IndicadorBC();

        ClaseConexion ClaseConexiond = new ClaseConexion();




        public Devolucion devolver_ventas_mesanual_performancelineal(int anioant, int anio, string mes, Compania info_compania, decimal idDB,decimal tproductoid)
        {
            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);
            Devolucion devolucion = new Devolucion();
            Indicador indicadorf = new Indicador();
            indicadorf = indicadorBC.datosindicador(2);
            devolucion.idIndicador = indicadorf.idIndicador;
            devolucion.nombreIndicador = indicadorf.nombreIndicador;
            devolucion.vs = anioant.ToString();
            CampoVentas lst = new CampoVentas();

            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[performancelineal_importe]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@iddbempresa", idDB);
                sqlCmd.Parameters.AddWithValue("@companiaid", info_compania.IdCompaniaOdoo);
                sqlCmd.Parameters.AddWithValue("@monedaid", info_compania.IdMonedaOdoo);
                sqlCmd.Parameters.AddWithValue("@anioact", anio);
                sqlCmd.Parameters.AddWithValue("@anioant", anioant);
                sqlCmd.Parameters.AddWithValue("@mess", mes);
                sqlCmd.Parameters.AddWithValue("@tproductoid", tproductoid);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                lst = Conversor.camposventas(dt.Rows[0]);
                conexion.Close();
            }






            if (lst.acumulado_anio_actual > 0 && lst.acumulado_anio_anterior == 0)
            {
            //    double camop = lst.acumulado_anio_actual * moneda_destino.Rate;
          //      camop = Math.Round(camop, 2);
          //      devolucion.Monto_Acumulado = camop.ToString();
                devolucion.Porcentaje_Monto_Acumulado = "100";
            }

            if (lst.acumulado_anio_actual == 0 && lst.acumulado_anio_anterior > 0)
            {
              //  double camop = lst.acumulado_anio_actual * moneda_destino.Rate;
             //   camop = Math.Round(camop, 2);
            //    devolucion.Monto_Acumulado = camop.ToString();
                devolucion.Porcentaje_Monto_Acumulado = "0";
            }

            if (lst.acumulado_anio_actual > 0 && lst.acumulado_anio_anterior > 0)
            {
             //   double camop = lst.acumulado_anio_actual * moneda_destino.Rate;
             //   camop = Math.Round(camop, 2);
             ///   devolucion.Monto_Acumulado = camop.ToString();


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
               // double camop = lst.acumulado_mes_actual * moneda_destino.Rate;
              //  camop = Math.Round(camop, 2);
              //  devolucion.Monto_Mes = camop.ToString();
                devolucion.Porcentaje_Monto_Mes = "100";
            }

            if (lst.acumulado_mes_actual == 0 && lst.acumulado_mes_anterior > 0)
            {
              //  double camop = lst.acumulado_mes_actual * moneda_destino.Rate;
              //  camop = Math.Round(camop, 2);
              //  devolucion.Monto_Mes = camop.ToString();
                devolucion.Porcentaje_Monto_Mes = "0";
            }
            if (lst.acumulado_mes_actual > 0 && lst.acumulado_mes_anterior > 0)
            {
              // double camop = lst.acumulado_mes_actual * moneda_destino.Rate;
                //camop = Math.Round(camop, 2);
              //  devolucion.Monto_Mes = camop.ToString();


              
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
