using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using IndicadoresCore.Models.BC;
using System.Runtime.ExceptionServices;

namespace IndicadoresCore.Models.contribucion_de_portaforlio.top5
{
    public class Top5_cp_total
    {

        ClaseConexion ClaseConexiond = new ClaseConexion();
        public List<portafolio_datos> devolver_top5_cp(int anioant, int anio, string mes, Compania info_compania, decimal idDB)
        {
            List<portafolio_datos> lst_datos = new List<portafolio_datos>();
            portafolio_datos lst_datos_l = new portafolio_datos();
            List<portafolio_infoo> portafolio_Infoos_lista_mes = new List<portafolio_infoo>();
            List<portafolio_infoo> portafolio_Infoos_lista_mes_aux = new List<portafolio_infoo>();
            List<portafolio_infoo> portafolio_Infoos_lista_anual = new List<portafolio_infoo>();
            List<portafolio_infoo> portafolio_Infoos_lista_anual_aux = new List<portafolio_infoo>();

            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);

            double tota_importe_mes = 0;
            double tota_cantidad_mes = 0;
            double tota_gross_margin_mes = 0;

            double tota_importe_anual = 0;
            double tota_cantidad_anual = 0;
            double tota_gross_margin_anual = 0;


            //Anual


            if (conexion.State != ConnectionState.Open)
            {

                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[Contribucion_de_portafolio_Top5Anual]", conexion);
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
                portafolio_Infoos_lista_anual = Conversor.portafolio_info_top5(dt.Select());
                tota_gross_margin_anual = Conversor.total_gross_margin(dt.Select());
                tota_cantidad_anual = Conversor.total_cantidad(dt.Select());
                tota_importe_anual = Conversor.total_importe(dt.Select());
                conexion.Close();


            }


            foreach (var lista_mes in portafolio_Infoos_lista_anual)
            {
                if (lista_mes.importe_actual > 0 && lista_mes.coste_actual > 0)
                {
                    portafolio_infoo llenar = new portafolio_infoo();
                    double camop1 = lista_mes.importe_actual;
                    double camop_a = lista_mes.importe_actual;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importe_actual = camop1;

                    double camopct = lista_mes.coste_actual;
                    camopct = Math.Round(camopct, 2);
                    lista_mes.coste_actual = camopct;


                    double camocantidad = lista_mes.cantidad_actual;
                    camocantidad = Math.Round(camocantidad, 2);
                    lista_mes.cantidad_actual = camocantidad;




                    double gross_profit_actual = lista_mes.importe_actual - lista_mes.coste_actual;
                    double gross_margin_actual = gross_profit_actual / lista_mes.importe_actual;
                    gross_margin_actual = Math.Round(gross_margin_actual, 2);
                    lista_mes.porcentaje_margen_actual = gross_margin_actual;


                    camop1 = camop1 / 1000;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importe_actual = camop1;

                    camopct = camopct / 1000;
                    camopct = Math.Round(camopct, 2);
                    lista_mes.coste_actual = camopct;




                    double por_gross_margin = ((lista_mes.porcentaje_margen_actual * 100) / tota_gross_margin_anual);
                    por_gross_margin = Math.Round(por_gross_margin, 2);
                    lista_mes.porcentaje_margen_actual_porcentaje = por_gross_margin.ToString();

                    double por_cantidad = ((lista_mes.cantidad_actual * 100) / tota_cantidad_anual);
                    por_cantidad = Math.Round(por_cantidad, 2);
                    lista_mes.cantidad_porcentaje = por_cantidad.ToString();



                    double por_importe = ((camop_a * 100) / tota_importe_anual);
                    por_importe = Math.Round(por_importe, 2);
                    lista_mes.importe_porcentaje = por_importe.ToString();

                    lista_mes.coste_porcentaje = "0";


                    llenar.porcentaje_margen_actual_porcentaje = lista_mes.porcentaje_margen_actual_porcentaje;
                    llenar.cantidad_porcentaje = lista_mes.cantidad_porcentaje;
                    llenar.coste_porcentaje = "0";
                    llenar.importe_porcentaje = lista_mes.importe_porcentaje;
                    llenar.porcentaje_margen_actual = lista_mes.porcentaje_margen_actual;
                    llenar.cantidad_actual = lista_mes.cantidad_actual;
                    llenar.importe_actual = lista_mes.importe_actual;
                    llenar.coste_actual = lista_mes.coste_actual;
                    llenar.Id = lista_mes.Id;
                    llenar.descripcion = lista_mes.descripcion;

                    portafolio_Infoos_lista_anual_aux.Add(llenar);

                }
                else
                {
                    lista_mes.importe_actual = 0;
                    lista_mes.coste_actual = 0;
                    lista_mes.cantidad_actual = 0;
                    lista_mes.porcentaje_margen_actual = 0;
                    lista_mes.importe_porcentaje = "0";
                    lista_mes.coste_porcentaje = "0";
                    lista_mes.cantidad_porcentaje = "0";
                    lista_mes.porcentaje_margen_actual_porcentaje = "0";
                }




            }



            lst_datos_l.Lista_anual = portafolio_Infoos_lista_anual_aux;





            if (conexion.State != ConnectionState.Open)
            {

                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[Contribucion_de_portafolio_Top5Mes]", conexion);
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
                portafolio_Infoos_lista_mes = Conversor.portafolio_info_top5(dt.Select());
                tota_gross_margin_mes = Conversor.total_gross_margin(dt.Select());
                tota_cantidad_mes = Conversor.total_cantidad(dt.Select());
                tota_importe_mes = Conversor.total_importe(dt.Select());
                conexion.Close();


            }





            foreach (var lista_mes in portafolio_Infoos_lista_mes)
            {
                if (lista_mes.importe_actual > 0 && lista_mes.coste_actual > 0)
                {
                    portafolio_infoo llenar = new portafolio_infoo();

                    double camop1 = lista_mes.importe_actual;
                    double camop_a = lista_mes.importe_actual;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importe_actual = camop1;

                    double camopct = lista_mes.coste_actual;
                    camopct = Math.Round(camopct, 2);
                    lista_mes.coste_actual = camopct;


                    double camocantidad = lista_mes.cantidad_actual;
                    camocantidad = Math.Round(camocantidad, 2);
                    lista_mes.cantidad_actual = camocantidad;




                    double gross_profit_actual = lista_mes.importe_actual - lista_mes.coste_actual;
                    double gross_margin_actual = gross_profit_actual / lista_mes.importe_actual;
                    gross_margin_actual = Math.Round(gross_margin_actual, 2);
                    lista_mes.porcentaje_margen_actual = gross_margin_actual;


                    camop1 = camop1 / 1000;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importe_actual = camop1;

                    camopct = camopct / 1000;
                    camopct = Math.Round(camopct, 2);
                    lista_mes.coste_actual = camopct;




                    double por_gross_margin = ((lista_mes.porcentaje_margen_actual * 100) / tota_gross_margin_mes);
                    por_gross_margin = Math.Round(por_gross_margin, 2);
                    lista_mes.porcentaje_margen_actual_porcentaje = por_gross_margin.ToString();

                    double por_cantidad = ((lista_mes.cantidad_actual * 100) / tota_cantidad_mes);
                    por_cantidad = Math.Round(por_cantidad, 2);
                    lista_mes.cantidad_porcentaje = por_cantidad.ToString();



                    double por_importe = ((camop_a * 100) / tota_importe_mes);
                    por_importe = Math.Round(por_importe, 2);
                    lista_mes.importe_porcentaje = por_importe.ToString();

                    lista_mes.coste_porcentaje = "0";


                    llenar.porcentaje_margen_actual_porcentaje = lista_mes.porcentaje_margen_actual_porcentaje;
                    llenar.cantidad_porcentaje = lista_mes.cantidad_porcentaje;
                    llenar.coste_porcentaje = "0";
                    llenar.importe_porcentaje = lista_mes.importe_porcentaje;
                    llenar.porcentaje_margen_actual = lista_mes.porcentaje_margen_actual;
                    llenar.cantidad_actual = lista_mes.cantidad_actual;
                    llenar.importe_actual = lista_mes.importe_actual;
                    llenar.coste_actual = lista_mes.coste_actual;
                    llenar.Id = lista_mes.Id;
                    llenar.descripcion = lista_mes.descripcion;

                    portafolio_Infoos_lista_mes_aux.Add(llenar);

                }
                else
                {


                    lista_mes.importe_actual = 0;
                    lista_mes.coste_actual = 0;
                    lista_mes.cantidad_actual = 0;
                    lista_mes.porcentaje_margen_actual = 0;
                    lista_mes.importe_porcentaje = "0";
                    lista_mes.coste_porcentaje = "0";
                    lista_mes.cantidad_porcentaje = "0";
                    lista_mes.porcentaje_margen_actual_porcentaje = "0";
                }

            }

            lst_datos_l.Lista_mes = portafolio_Infoos_lista_mes_aux;



            lst_datos.Add(lst_datos_l);
            return lst_datos;

        }







    }
}
