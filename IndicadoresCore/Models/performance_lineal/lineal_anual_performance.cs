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

namespace IndicadoresCore.Models.performance_lineal
{
    public class lineal_anual_performance
    {


        ClaseConexion ClaseConexiond = new ClaseConexion();


        public List<Ranking> devolver_anual_lineal(int anioant, int anio, string mes, Compania info_compania, decimal idDB, MonedaCompania moneda_destino)
        {
            List<Ranking> lst = new List<Ranking>();
            double totap = 0;
            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[PerformaceLinealAnual]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@iddbempresa", idDB);
                sqlCmd.Parameters.AddWithValue("@companiaid", info_compania.IdCompaniaOdoo) ;
                sqlCmd.Parameters.AddWithValue("@monedaid", info_compania.IdMonedaOdoo);
                sqlCmd.Parameters.AddWithValue("@anioact", anio);
                sqlCmd.Parameters.AddWithValue("@anioant", anioant);
                sqlCmd.Parameters.AddWithValue("@mess", mes);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                lst = Conversor.listarankinglinealperformance(dt.Select());
                totap = Conversor.totalrakinglinealperformance(dt.Select());
                conexion.Close();
            }



            foreach (var lista_mes in lst)
            {
                Ranking_Lista detalle_Receptor = new Ranking_Lista();
                List<Devolucion> devolucions = new List<Devolucion>();
                if (lista_mes.importeactual > 0)
                {
                    double camop1 = lista_mes.importeactual * moneda_destino.Rate;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importeactual = camop1;


                    double por = ((lista_mes.importeactual * 100) / totap);
                    por = Math.Round(por, 2);
                    lista_mes.porcentajetorta = por.ToString();

                    camop1 = camop1 / 1000;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importeactual = camop1;

                    if (lista_mes.importeanterior > 0)
                    {

                        double camop11 = lista_mes.importeanterior * moneda_destino.Rate;
                        camop11 = Math.Round(camop11, 2);
                        camop11 = camop11 / 1000;
                        camop11 = Math.Round(camop11, 2);
                        lista_mes.importeanterior = camop11;
                    }
                    else
                    {
                        lista_mes.importeanterior = 0;
                    }






                }
                else
                {
                    lista_mes.importeactual = 0;
                    lista_mes.porcentajetorta = "0";

                    if (lista_mes.importeanterior > 0)
                    {

                        double camop11 = lista_mes.importeanterior * moneda_destino.Rate;
                        camop11 = Math.Round(camop11, 2);
                        camop11 = camop11 / 1000;
                        camop11 = Math.Round(camop11, 2);
                        lista_mes.importeanterior = camop11;
                    }
                    else
                    {
                        lista_mes.importeanterior = 0;
                    }

                }



                for (int nn = 1; nn <= 3; nn++)
                {
                    switch (nn)
                    {
                        case 1:
                            Ventas_lineal ventas_1 = new Ventas_lineal();
                            Devolucion devolucion1 = ventas_1.devolver_ventas_mesanual_performancelineal(anioant, anio, mes, info_compania, idDB, lista_mes.idPosicion);
                            devolucions.Add(devolucion1);
                            break;
                        case 2:
                            Cantidad_lineal cantidad_2 = new Cantidad_lineal();
                            Devolucion devolucion2 = cantidad_2.devolver_cantidad_mesanual_performancelineal(anioant, anio, mes, info_compania, idDB, lista_mes.idPosicion);
                            devolucions.Add(devolucion2);
                            break;
                        case 3:
                            PrecioPromedio_lineal precio_Promedio_3 = new PrecioPromedio_lineal();
                            Devolucion devolucion3 = precio_Promedio_3.devolver_Precio_Promedio_mesanual_performancelineal(anioant, anio, mes, info_compania,idDB,lista_mes.idPosicion);
                            devolucions.Add(devolucion3);
                            break;
                    }
                }

                detalle_Receptor.Lista = devolucions;
                lista_mes.detalle_Receptor = detalle_Receptor;


            }

            return lst;


        }









        public List<Ranking> devolver_anual_lineal_torta( int anioant, int anio, string mes, Compania info_compania, decimal idDB, MonedaCompania moneda_destino)
        {
            List<Ranking> lst = new List<Ranking>();
            double totap = 0;
            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);

            List<Ranking> lstg = new List<Ranking>();

            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[PerformaceLinealAnual]", conexion);
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
                lst = Conversor.listarankinglinealperformance(dt.Select());
                totap = Conversor.totalrakinglinealperformance(dt.Select());
                conexion.Close();
            }



            foreach (var lista_mes in lst)
            {

                if (lista_mes.importeactual > 0)
                {
                    Ranking llenar = new Ranking();
                    double camop1 = lista_mes.importeactual * moneda_destino.Rate;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importeactual = camop1;


                    double por = ((lista_mes.importeactual * 100) / totap);
                    por = Math.Round(por, 2);
                    lista_mes.porcentajetorta = por.ToString();

                    camop1 = camop1 / 1000;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importeactual = camop1;

                    if (lista_mes.importeanterior > 0)
                    {

                        double camop11 = lista_mes.importeanterior * moneda_destino.Rate;
                        camop11 = Math.Round(camop11, 2);
                        camop11 = camop11 / 1000;
                        camop11 = Math.Round(camop11, 2);
                        lista_mes.importeanterior = camop11;
                    }
                    else
                    {
                        lista_mes.importeanterior = 0;
                    }
                    llenar.porcentajetorta = por.ToString();
                    llenar.nombre = lista_mes.nombre;
                    llenar.idPosicion = lista_mes.idPosicion;
                    lstg.Add(llenar);

                }
                else
                {
                    lista_mes.importeactual = 0;
                    lista_mes.porcentajetorta = "0";

                    if (lista_mes.importeanterior > 0)
                    {

                        double camop11 = lista_mes.importeanterior * moneda_destino.Rate;
                        camop11 = Math.Round(camop11, 2);
                        camop11 = camop11 / 1000;
                        camop11 = Math.Round(camop11, 2);
                        lista_mes.importeanterior = camop11;
                    }
                    else
                    {
                        lista_mes.importeanterior = 0;
                    }

                }

            }

            return lstg;


        }









    }
}
