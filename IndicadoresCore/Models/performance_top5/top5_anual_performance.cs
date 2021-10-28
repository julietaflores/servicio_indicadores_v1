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
using IndicadoresCore.Models.tabla_9_primero_indicadores;


namespace IndicadoresCore.Models.performance_top5
{
    public class top5_anual_performance
    {
        
        ClaseConexion ClaseConexiond = new ClaseConexion();



        public List<Ranking> devolver_anual_top5(int anioant, int anio, string mes, Compania info_compania, decimal idDB, MonedaCompania moneda_destino)
        {
            List<Ranking> lst = new List<Ranking>();
            double totap = 0;
            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);



            if (conexion.State != ConnectionState.Open)
            {

                conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("[Performacetop5anioactual]", conexion);
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
                lst = Conversor.listaranking(dt.Select());
                totap = Conversor.totalrakintop5(dt.Select());
                conexion.Close();
            }
           

            foreach (var lista_mes in lst)
            {
                Ranking_Lista detalle_Receptor = new Ranking_Lista();
                List<Devolucion> devolucions = new List<Devolucion>();


                if (lista_mes.importeactual > 0)
                {
                
                    int anioantant = anioant - 1;

                    double camop1 = lista_mes.importeactual * moneda_destino.Rate;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importeactual = camop1;
                    double por = ((lista_mes.importeactual * 100) / totap);
                    por = Math.Round(por, 2);
                    lista_mes.porcentajetorta = por.ToString();


                    camop1 = camop1 / 1000;
                    camop1 = Math.Round(camop1, 2);
                    lista_mes.importeactual = camop1;
                    Ranking lstr = new Ranking();
                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                        SqlCommand sqlCmd1 = new SqlCommand("[Performacetop5anioanterior_x_producto]", conexion);
                        sqlCmd1.CommandType = CommandType.StoredProcedure;
                        sqlCmd1.CommandTimeout = 0;
                        sqlCmd1.Parameters.AddWithValue("@iddbempresa", idDB);
                        sqlCmd1.Parameters.AddWithValue("@companiaid", info_compania.IdCompaniaOdoo);
                        sqlCmd1.Parameters.AddWithValue("@monedaid", info_compania.IdMonedaOdoo);
                        sqlCmd1.Parameters.AddWithValue("@anioact", anioant);
                        sqlCmd1.Parameters.AddWithValue("@anioant", anioantant);
                        sqlCmd1.Parameters.AddWithValue("@mess", mes);
                        sqlCmd1.Parameters.AddWithValue("@proid", lista_mes.idPosicion);
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        lstr = Conversor.listaranking1(dt1.Select().FirstOrDefault());
                        conexion.Close();
                    }

                       

                    if (lstr.importeactual > 0)
                    {
                        double camop11 = lstr.importeactual * moneda_destino.Rate;
                        camop11 = Math.Round(camop11, 2);
                        camop11 = camop11 / 1000;
                        camop11 = Math.Round(camop11, 2);
                        lista_mes.importeanterior = camop11;
                       

                    }
                    else
                    {
                        double camop11 = 0 * moneda_destino.Rate;
                        camop11 = Math.Round(camop11, 2);
                        lista_mes.importeanterior = camop11;
                      

                    }

                }
                else
                {
                    lista_mes.importeactual = 0;
                    lista_mes.importeanterior = 0;
                    lista_mes.porcentajetorta = "0";
                }


                for (int nn = 1; nn <= 3; nn++)
                {
                    switch (nn)
                    {
                        case 1:
                            Cantidad_1 cantidad_1 = new Cantidad_1();

                            Devolucion devolucion1 = cantidad_1.devolver_cantidad_mesanual(anioant, anio, mes, info_compania, idDB,  lista_mes.idPosicion);
                            devolucions.Add(devolucion1);
                            break;
                        case 2:
                            Ventas_2 ventas_2 = new Ventas_2();
                            Devolucion devolucion2 = ventas_2.devolver_ventas_mesanual(anioant, anio, mes, info_compania,idDB,  lista_mes.idPosicion);
                            devolucions.Add(devolucion2);
                            break;
                        case 3:
                            Precio_Promedio_3 importeactual_Promedio_3 = new Precio_Promedio_3();
                            Devolucion devolucion3 = importeactual_Promedio_3.devolver_Precio_Promedio_mesanual(anioant, anio, mes, info_compania, idDB, lista_mes.idPosicion);
                            devolucions.Add(devolucion3);
                            break;
                    }
                }
                detalle_Receptor.Lista = devolucions;
                lista_mes.detalle_Receptor = detalle_Receptor;



            }



            return lst;

        }

    }
}
