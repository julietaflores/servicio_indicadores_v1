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

namespace IndicadoresCore.Models.Grafico_ICP.Graficos
{ 
    public class Graficos_icp
    {
        ClaseConexion ClaseConexiond = new ClaseConexion();
        const string ingreso_miles= "ingreso_miles";
        const string cantidad_miles = "cantidad_miles";
        const string ppp = "ppp";
        const string cpp= "cpp";
        const string ebitda_miles = "ebitda_miles";

        public Data_graficaICP ICP_graficos(int anioant, int anio, string mes, Compania info_compania, decimal idDB)
        {
            List<DatosGrafico_icp> lst_datos = new List<DatosGrafico_icp>();
            portafolio_datos lst_datos_l = new portafolio_datos();
            Data_graficaICP resp_data = new Data_graficaICP();

            SqlConnection conexion = new SqlConnection(ClaseConexiond.con);
            //Anual

            if (conexion.State != ConnectionState.Open)
            {

                conexion.Open();
                for (int i = 1; i <= 12; i++) {
                    if (i < 10)
                    {
                        mes = "0" + i;
                    }
                    else
                    {
                        mes = i.ToString();
                    }
                    SqlCommand sqlCmd = new SqlCommand("[Graficos_icp]", conexion);
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

                    DataRow[] dr =  dt.Select();
                    DatosGrafico_icp n = new DatosGrafico_icp();


                    foreach (var item in dr)
                    {
                        DataRow ob = item;
                        resp_data.ingreso_miles.Add(Convert.ToDouble(ob["ingreso_miles"].ToString()));
                        resp_data.cantidad_miles.Add(Convert.ToDouble(ob["cantidad_miles"].ToString()));
                        resp_data.ppp.Add(Convert.ToDouble(ob["ppp"].ToString()));
                        resp_data.cpp.Add(Convert.ToDouble(ob["cpp"].ToString()));
                        resp_data.ebitda_miles.Add(Convert.ToDouble(ob["ebitda_miles"].ToString()));
                    }
                  
                }
                conexion.Close();


            }


           

            

            return resp_data;

        }





    }
}
