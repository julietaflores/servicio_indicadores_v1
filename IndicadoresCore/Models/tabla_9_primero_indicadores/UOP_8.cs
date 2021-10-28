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
    public class UOP_8
    {

        
        IndicadorBC indicadorBC = new IndicadorBC();
        ClaseConexion ClaseConexiond = new ClaseConexion();




        public Devolucion devolver_cantidad_1(int anioant, int anio, string mes, Compania compania1, int compania, decimal idDB, MonedaCompania moneda1)
        {
            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);
            Devolucion devolucion = new Devolucion();
            Indicador indicadorf = new Indicador();
            indicadorf = indicadorBC.datosindicador(8);
            devolucion.idIndicador = indicadorf.idIndicador;
            devolucion.nombreIndicador = indicadorf.nombreIndicador;
            devolucion.vs = anioant.ToString();


            if (conexion.State != ConnectionState.Open) conexion.Open();
            SqlCommand sqlCmd = new SqlCommand("[CifrasNotables_Cantidad_1]", conexion);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@iddbempresa", idDB);
            sqlCmd.Parameters.AddWithValue("@companiaid", compania);
            sqlCmd.Parameters.AddWithValue("@monedaid", compania1.IdMonedaOdoo);
            sqlCmd.Parameters.AddWithValue("@anioact", anio);
            sqlCmd.Parameters.AddWithValue("@anioant", anioant);
            sqlCmd.Parameters.AddWithValue("@mess", mes);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            CampoVentas lst = Conversor.camposventas(dt.Rows[0]);

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


                double diff = (((lst.acumulado_anio_actual - lst.acumulado_anio_anterior) - 1) * 100);
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


                double diff = (((lst.acumulado_mes_actual - lst.acumulado_mes_anterior) - 1) * 100);
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
