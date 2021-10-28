﻿using System;
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

namespace IndicadoresCore.Models.margen_bruto_top5
{
    public class mes_top5
    {

 
        ClaseConexion ClaseConexiond = new ClaseConexion();


        public List<Ranking_Margenes> devolver_mes(int anioant, int anio, string mes, Compania info_compania, decimal idDB, MonedaCompania monedadestino)
        {
            List<Ranking_Margenes> lst = new List<Ranking_Margenes>();
            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);



            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[MargenBrutoTop5Mes]", conexion);
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
                lst = Conversor.listarankingmargen_brutotop5(dt.Select());
                conexion.Close();
            }

           
            foreach (var lista_mes in lst)
            {



                if (lista_mes.importe_actual > 0 && lista_mes.coste_actual > 0)
                {

                    int anioantant = anioant - 1;



                    double camop1 = lista_mes.importe_actual * monedadestino.Rate;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importe_actual = camop1;

                    double camopct = lista_mes.coste_actual * monedadestino.Rate;
                    camopct = Math.Round(camopct, 2);
                    lista_mes.coste_actual = camopct;
                    Ranking_Margenes lstr = new Ranking_Margenes();

                    if (conexion.State != ConnectionState.Open)
                    {

                        conexion.Open();
                        SqlCommand sqlCmd1c = new SqlCommand("[MargenBrutoTop5Mes_x_producto]", conexion);
                        sqlCmd1c.CommandType = CommandType.StoredProcedure;
                        sqlCmd1c.CommandTimeout = 0;
                        sqlCmd1c.Parameters.AddWithValue("@iddbempresa", idDB);
                        sqlCmd1c.Parameters.AddWithValue("@companiaid", info_compania.IdCompaniaOdoo);
                        sqlCmd1c.Parameters.AddWithValue("@monedaid", info_compania.IdMonedaOdoo);
                        sqlCmd1c.Parameters.AddWithValue("@anioact", anioant);
                        sqlCmd1c.Parameters.AddWithValue("@anioant", anioantant);
                        sqlCmd1c.Parameters.AddWithValue("@mess", mes);
                        sqlCmd1c.Parameters.AddWithValue("@proidd", lista_mes.id);
                        SqlDataAdapter da1c = new SqlDataAdapter(sqlCmd1c);
                        DataTable dt1c = new DataTable();
                        da1c.Fill(dt1c);
                        lstr = Conversor.listarankingmargen_brutotop51(dt1c.Select().FirstOrDefault());
                        conexion.Close();

                    }


                        

                    if (lstr.importe_actual > 0 && lstr.coste_actual > 0)
                    {

                        double camop11 = lstr.importe_actual * monedadestino.Rate;
                        camop11 = Math.Round(camop11, 2);
                        lista_mes.importe_anterior = camop11;

                        double camopctt = lstr.coste_actual * monedadestino.Rate;
                        camopctt = Math.Round(camopctt, 2);
                        lista_mes.coste_anterior = camopctt;


                        double gross_profit_actual = lista_mes.importe_actual - lista_mes.coste_actual;
                        double gross_margin_actual = gross_profit_actual / lista_mes.importe_actual;
                        gross_margin_actual = Math.Round(gross_margin_actual, 2);
                        lista_mes.porcentaje_margen_actual = gross_margin_actual;

                        double gross_profit_anterior = lista_mes.importe_anterior - lista_mes.coste_anterior;
                        double gross_margin_anterior = gross_profit_anterior / lista_mes.importe_anterior;
                        gross_margin_anterior = Math.Round(gross_margin_anterior, 2);
                        lista_mes.porcentaje_margen_anterior = gross_margin_anterior;

                        camop1 = camop1 / 1000;
                        camop1 = Math.Round(camop1, 2);
                        lista_mes.importe_actual = camop1;

                        camopct = camopct / 1000;
                        camopct = Math.Round(camopct, 2);
                        lista_mes.coste_actual = camopct;

                        camop11 = camop11 / 1000;
                        camop11 = Math.Round(camop11, 2);
                        lista_mes.importe_anterior = camop11;

                        camopctt = camopctt / 1000;
                        camopctt = Math.Round(camopctt, 2);
                        lista_mes.coste_anterior = camopctt;





                        double bps = lista_mes.importe_actual - lista_mes.importe_anterior;
                        bps = Math.Round(bps, 2);
                        lista_mes.BPS = bps.ToString();

                        double calculo_grafico = lista_mes.coste_actual - lista_mes.coste_anterior;
                        calculo_grafico = Math.Round(calculo_grafico, 2);
                        lista_mes.calculo_grafico = calculo_grafico.ToString();



                        // double por = ((lista_mes.porcentaje_margen_actual * 100) / totap);
                        //  por = Math.Round(por, 2);
                        lista_mes.porcentajetorta = "0";


                    }
                    else
                    {




                        double gross_profit_actual = lista_mes.importe_actual - lista_mes.coste_actual;
                        double gross_margin_actual = gross_profit_actual / lista_mes.importe_actual;
                        gross_margin_actual = Math.Round(gross_margin_actual, 2);
                        lista_mes.porcentaje_margen_actual = gross_margin_actual;


                        lista_mes.importe_anterior = 0;
                        lista_mes.coste_anterior = 0;
                        lista_mes.porcentaje_margen_anterior = 0;


                        camop1 = camop1 / 1000;
                        camop1 = Math.Round(camop1, 2);
                        lista_mes.importe_actual = camop1;

                        camopct = camopct / 1000;
                        camopct = Math.Round(camopct, 2);
                        lista_mes.coste_actual = camopct;


                        double bps = lista_mes.importe_actual - lista_mes.importe_anterior;
                        bps = Math.Round(bps, 2);
                        lista_mes.BPS = bps.ToString();

                        double calculo_grafico = lista_mes.coste_actual - lista_mes.coste_anterior;
                        calculo_grafico = Math.Round(calculo_grafico, 2);
                        lista_mes.calculo_grafico = calculo_grafico.ToString();



                        //     double por = ((lista_mes.porcentaje_margen_actual * 100) / totap);
                        //   por = Math.Round(por, 2);
                        lista_mes.porcentajetorta = "0";
                    }




                }
                else
                {
                    lista_mes.importe_anterior = 0;
                    lista_mes.coste_anterior = 0;
                    lista_mes.porcentaje_margen_actual = 0;
                    lista_mes.importe_anterior = 0;
                    lista_mes.coste_anterior = 0;
                    lista_mes.porcentaje_margen_anterior = 0;
                    lista_mes.BPS = "0";
                    lista_mes.calculo_grafico = "0";
                    lista_mes.porcentajetorta = "0";
                }


            }




            return lst;

        }




        public List<Ranking_Margenes> devolver_mes_torta(int anioant, int anio, string mes, Compania info_compania, decimal idDB)
        {
            List<Ranking_Margenes> lst = new List<Ranking_Margenes>();
            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);
   
            List<Ranking_Margenes> lstg = new List<Ranking_Margenes>();

            double totap = 0;

            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[MargenBrutoTop5Mes]", conexion);
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
                lst = Conversor.listarankingmargen_brutotop5(dt.Select());
                totap = Conversor.totalrakinmargenbruto(dt.Select());
                conexion.Close();
            }


            foreach (var lista_mes in lst)
            {



                if (lista_mes.importe_actual > 0 && lista_mes.coste_actual > 0)
                {

                    int anioantant = anioant - 1;

                    Ranking_Margenes llenar = new Ranking_Margenes();

                    double camop1 = lista_mes.importe_actual;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importe_actual = camop1;

                    double camopct = lista_mes.coste_actual;
                    camopct = Math.Round(camopct, 2);
                    lista_mes.coste_actual = camopct;



                    Ranking_Margenes lstr = new Ranking_Margenes();

                    if (conexion.State != ConnectionState.Open)
                    {

                        conexion.Open();
                        SqlCommand sqlCmd1c = new SqlCommand("[MargenBrutoTop5Mes_x_producto]", conexion);
                        sqlCmd1c.CommandType = CommandType.StoredProcedure;
                        sqlCmd1c.CommandTimeout = 0;
                        sqlCmd1c.Parameters.AddWithValue("@iddbempresa", idDB);
                        sqlCmd1c.Parameters.AddWithValue("@companiaid", info_compania.IdCompaniaOdoo);
                        sqlCmd1c.Parameters.AddWithValue("@monedaid", info_compania.IdMonedaOdoo);
                        sqlCmd1c.Parameters.AddWithValue("@anioact", anioant);
                        sqlCmd1c.Parameters.AddWithValue("@anioant", anioantant);
                        sqlCmd1c.Parameters.AddWithValue("@mess", mes);
                        sqlCmd1c.Parameters.AddWithValue("@proidd", lista_mes.id);
                        SqlDataAdapter da1c = new SqlDataAdapter(sqlCmd1c);
                        DataTable dt1c = new DataTable();
                        da1c.Fill(dt1c);
                        lstr = Conversor.listarankingmargen_brutotop51(dt1c.Select().FirstOrDefault());
                        conexion.Close();

                    }




                    if (lstr.importe_actual > 0 && lstr.coste_actual > 0)
                    {

                        double camop11 = lstr.importe_actual;
                        camop11 = Math.Round(camop11, 2);
                        lista_mes.importe_anterior = camop11;

                        double camopctt = lstr.coste_actual ;
                        camopctt = Math.Round(camopctt, 2);
                        lista_mes.coste_anterior = camopctt;


                        double gross_profit_actual = lista_mes.importe_actual - lista_mes.coste_actual;
                        double gross_margin_actual = gross_profit_actual / lista_mes.importe_actual;
                        gross_margin_actual = Math.Round(gross_margin_actual, 2);
                        lista_mes.porcentaje_margen_actual = gross_margin_actual;

                        double gross_profit_anterior = lista_mes.importe_anterior - lista_mes.coste_anterior;
                        double gross_margin_anterior = gross_profit_anterior / lista_mes.importe_anterior;
                        gross_margin_anterior = Math.Round(gross_margin_anterior, 2);
                        lista_mes.porcentaje_margen_anterior = gross_margin_anterior;

              

                        camop11 = camop11 / 1000;
                        camop11 = Math.Round(camop11, 2);
                        lista_mes.importe_anterior = camop11;

                        camopctt = camopctt / 1000;
                        camopctt = Math.Round(camopctt, 2);
                        lista_mes.coste_anterior = camopctt;


                        double por = ((lista_mes.porcentaje_margen_actual * 100) / totap);
                        por = Math.Round(por, 2);
                        lista_mes.porcentajetorta = por.ToString();
                        llenar.porcentajetorta = por.ToString();
                        llenar.nombre = lista_mes.nombre;
                        llenar.id = lista_mes.id;


                    }
                    else
                    {




                        double gross_profit_actual = lista_mes.importe_actual - lista_mes.coste_actual;
                        double gross_margin_actual = gross_profit_actual / lista_mes.importe_actual;
                        gross_margin_actual = Math.Round(gross_margin_actual, 2);
                        lista_mes.porcentaje_margen_actual = gross_margin_actual;


                        lista_mes.importe_anterior = 0;
                        lista_mes.coste_anterior = 0;
                        lista_mes.porcentaje_margen_anterior = 0;


                        camop1 = camop1 / 1000;
                        camop1 = Math.Round(camop1, 2);
                        lista_mes.importe_actual = camop1;

                        camopct = camopct / 1000;
                        camopct = Math.Round(camopct, 2);
                        lista_mes.coste_actual = camopct;


             



                        double por = ((lista_mes.porcentaje_margen_actual * 100) / totap);
                        por = Math.Round(por, 2);
                        lista_mes.porcentajetorta = por.ToString();
                        llenar.porcentajetorta = por.ToString();
                        llenar.nombre = lista_mes.nombre;
                        llenar.id = lista_mes.id;

                    }


                    lstg.Add(llenar);

                }
                else
                {
                    lista_mes.importe_anterior = 0;
                    lista_mes.coste_anterior = 0;
                    lista_mes.porcentaje_margen_actual = 0;
                    lista_mes.importe_anterior = 0;
                    lista_mes.coste_anterior = 0;
                    lista_mes.porcentaje_margen_anterior = 0;
                    lista_mes.BPS = "0";
                    lista_mes.calculo_grafico = "0";
                    lista_mes.porcentajetorta = "0";
                }


            }




            return lstg;

        }



    }
}
