using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IndicadoresCore.Models.BC
{
    public class Conversor
    {
        double totalrr = 0;

        public static List<Lista_deudas> listadeudas(DataRow[] dr)
        {
            List<Lista_deudas> lst = new List<Lista_deudas>();
            foreach (var item in dr)
            {
                lst.Add(listadeudas(item));
            }
            return lst;
        }

        public static Lista_deudas listadeudas(DataRow dr)
        {

            string fecha1 = "";
            string fecha2 = "";
            DateTime v1 = Convert.ToDateTime(dr["servasigfhinicio"].ToString());
            if (v1 != null)
            {
                fecha1 = v1.ToString("dd/MM/yyyy HH:mm");
            }
            DateTime v2 = Convert.ToDateTime(dr["servasigfhfin"].ToString());
            if (v2 != null)
            {
                fecha2 = v2.ToString("dd/MM/yyyy HH:mm");
            }

            Lista_deudas obj = new Lista_deudas();
            obj.requiereservicioid = dr["RequiereServicioId"].ToString();
            obj.servasigid = dr["servasigid"].ToString();
            obj.deuda = Math.Round(Convert.ToDecimal(dr["Dueda"].ToString()), 2);
            obj.personanombres = dr["personanombres"].ToString();
            obj.personaapellidos = dr["personaapellidos"].ToString();
            obj.personadni = dr["personadni"].ToString();
            obj.personatelefono = dr["personatelefono"].ToString();
            obj.personacorreo = dr["personacorreo"].ToString();
            obj.servasigfhinicio = fecha1;
            obj.servasigfhfin = fecha2;
            return obj;
        }



        public static CampoVentas camposventas(DataRow dr)
        {
            CampoVentas obj = new CampoVentas();
            string ft1 = dr["acumulado_anio_actual"].ToString();
            string ft2 = dr["acumulado_mes_actual"].ToString();
            string ft3 = dr["acumulado_anio_anterior"].ToString();
            string ft4 = dr["acumulado_mes_anterior"].ToString();


            if (ft1 == "")
            {
                obj.acumulado_anio_actual = 0;
            }
            else
            {
                obj.acumulado_anio_actual = Convert.ToDouble(dr["acumulado_anio_actual"].ToString());
            }
            if (ft2 == "")
            {
                obj.acumulado_mes_actual = 0;
            }
            else
            {
                obj.acumulado_mes_actual = Convert.ToDouble(dr["acumulado_mes_actual"].ToString());
            }
            if (ft3 == "")
            {
                obj.acumulado_anio_anterior = 0;
            }
            else
            {
                obj.acumulado_anio_anterior = Convert.ToDouble(dr["acumulado_anio_anterior"].ToString());
            }
            if (ft4 == "")
            {
                obj.acumulado_mes_anterior = 0;
            }
            else
            {
                obj.acumulado_mes_anterior = Convert.ToDouble(dr["acumulado_mes_anterior"].ToString());
            }

            return obj;
        }




        public static CampoGross_margin_frofit camposgrossMarginProfit(DataRow dr)
        {



            CampoGross_margin_frofit obj = new CampoGross_margin_frofit();
            obj.acumulado_anio_actual_ganacia = Convert.ToDouble(dr["acumulado_anio_actual_ganancia"].ToString());
            obj.acumulado_anio_actual_costo = Convert.ToDouble(dr["acumulado_anio_actual_costo"].ToString());
            obj.acumulado_mes_actual_ganacia = Convert.ToDouble(dr["acumulado_mes_actual_ganancia"].ToString());
            obj.acumulado_mes_actual_costo = Convert.ToDouble(dr["acumulado_mes_actual_costo"].ToString());
            obj.acumulado_anio_anterior_ganacia = Convert.ToDouble(dr["acumulado_anio_anterior_ganancia"].ToString());
            obj.acumulado_anio_anterior_costo = Convert.ToDouble(dr["acumulado_anio_anterior_costo"].ToString());
            obj.acumulado_mes_anterior_ganacia = Convert.ToDouble(dr["acumulado_mes_anterior_ganancia"].ToString());
            obj.acumulado_mes_anterior_costo = Convert.ToDouble(dr["acumulado_mes_anterior_costo"].ToString());
         

            return obj;
        }


        public static CamposPrecioPromedioVentas camposPrecioPromedioVentas(DataRow dr)
        {



            CamposPrecioPromedioVentas obj = new CamposPrecioPromedioVentas();
            obj.acumulado_anio_actual_ventas_netas = Convert.ToDouble(dr["acumulado_anio_actual_ventas_netas"].ToString());
            obj.acumulado_anio_actual_cantidad_productos_vendidos = Convert.ToDouble(dr["acumulado_anio_actual_cantidad_productos_vendidos"].ToString());
            obj.acumulado_mes_actual_ventas_netas = Convert.ToDouble(dr["acumulado_mes_actual_ventas_netas"].ToString());
            obj.acumulado_mes_actual_productos_vendidos = Convert.ToDouble(dr["acumulado_mes_actual_productos_vendidos"].ToString());
            obj.acumulado_anio_anterior_ventas_netas = Convert.ToDouble(dr["acumulado_anio_anterior_ventas_netas"].ToString());
            obj.acumulado_anio_anterior_productos_vendidos = Convert.ToDouble(dr["acumulado_anio_anterior_productos_vendidos"].ToString());
            obj.acumulado_mes_anterior_ventas_netas = Convert.ToDouble(dr["acumulado_mes_anterior_ventas_netas"].ToString());
            obj.acumulado_mes_anterior_productos_vendidos = Convert.ToDouble(dr["acumulado_mes_anterior_productos_vendidos"].ToString());


            return obj;
        }









        public static List<Ranking> listaranking(DataRow[] dr)
        {
            List<Ranking> lst = new List<Ranking>();
            foreach (var item in dr)
            {
                lst.Add(listaranking(item));
            }
            return lst;
        }

        public static Ranking listaranking(DataRow dr)
        {
       
            Ranking obj = new Ranking();
            obj.idPosicion = Convert.ToDecimal(dr["ProdId"].ToString()); 
            obj.nombre = dr["Name"].ToString();
            obj.importeactual = Convert.ToDouble(dr["total"].ToString());
            return obj;
        }
        public static Ranking listaranking1(DataRow dr)
        {
            Ranking obj = new Ranking();
            if (dr!= null)
            {
                obj.idPosicion = Convert.ToDecimal(dr["ProdId"].ToString());
                obj.nombre = dr["Name"].ToString();
                obj.importeactual = Convert.ToDouble(dr["total"].ToString());
            }
            else
            {
                obj.idPosicion = 0;
                obj.nombre = "";
                obj.importeactual = 0;

            }
         
           
            return obj;
        }








        public static List<Ranking> listarankingregion(DataRow[] dr)
        {
            List<Ranking> lst = new List<Ranking>();
            foreach (var item in dr)
            {
                lst.Add(listarankingregion(item));
            }
            return lst;
        }

        public static Ranking listarankingregion(DataRow dr)
        {
        
            Ranking obj = new Ranking();
            obj.idPosicion = Convert.ToDecimal(dr["warehouse_id"].ToString());
            obj.nombre = dr["Name"].ToString();
            obj.importeactual = Convert.ToDouble(dr["totalimporteactual"].ToString());
            obj.importeanterior = Convert.ToDouble(dr["totalimporteanterior"].ToString());
            return obj;
        }





        public static  double totalrakinregion(DataRow[] dr)
        { double totalp = 0;
          
            foreach (var item in dr)
            {
                 double valop= totalrakinregion(item);
                totalp = valop + totalp;
            }
            return totalp;
        }

        public  static double totalrakinregion(DataRow dr)
        {
            double valor = 0;
             valor= Convert.ToDouble(dr["totalimporteactual"].ToString());

            return valor;
        }





        public static double totalrakintop5(DataRow[] dr)
        {
            double totalp = 0;

            foreach (var item in dr)
            {
                double valop = totalrakintop5(item);
                totalp = valop + totalp;
            }
            return totalp;
        }

        public static double totalrakintop5(DataRow dr)
        {
            double valor = 0;
            valor = Convert.ToDouble(dr["total"].ToString());

            return valor;
        }






        public static List<Ranking_Margenes> listarankingmargen_bruto(DataRow[] dr)
        {
            List<Ranking_Margenes> lst = new List<Ranking_Margenes>();
            foreach (var item in dr)
            {
                lst.Add(listarankingmargen_bruto(item));
            }
            return lst;
        }

        public static Ranking_Margenes listarankingmargen_bruto(DataRow dr)
        {
            Ranking_Margenes obj = new Ranking_Margenes();

            if (dr == null)
            {
                obj.id = 0;
                obj.nombre = "0";
                obj.importe_actual = 0;
                obj.coste_actual = 0;
            }
            else
            {
                obj.id = Convert.ToDecimal(dr["warehouse_id"].ToString());
                obj.nombre = dr["Name"].ToString();
                obj.importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());
                obj.coste_actual = Convert.ToDouble(dr["totalcosteactual"].ToString());

            }

            return obj;
        }





        public static Ranking_Margenes listarankingmargen_bruto1(DataRow dr)
        {
            Ranking_Margenes obj = new Ranking_Margenes();
           


            if (dr == null)
            {
                obj.id = 0;
                obj.nombre = "0";
                obj.importe_actual = 0;
                obj.coste_actual = 0;
            }
            else
            {
                obj.id = Convert.ToDecimal(dr["warehouse_id"].ToString());
                obj.nombre = dr["Name"].ToString();
                obj.importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());
                obj.coste_actual = Convert.ToDouble(dr["totalcosteactual"].ToString());

            }



            return obj;
        }





        public static double totalrakinmargenbruto(DataRow[] dr)
        {
            double totalp = 0;

            foreach (var item in dr)
            {
                double valop = totalrakinmargenbruto(item);
                totalp = valop + totalp;
            }
            return totalp;
        }

        public static double totalrakinmargenbruto(DataRow dr)
        {

            double gross_margin = 0;

            double importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());
            double coste_actual =  Convert.ToDouble(dr["totalcosteactual"].ToString());


            if (importe_actual>0  && coste_actual>0)
            {
                double gross_profit = importe_actual - coste_actual;
                 gross_margin = gross_profit / importe_actual;

            }
            else
            {
                gross_margin=0;
            }
          
       

            return gross_margin;
        }

















        public static List<Ranking_Margenes> listarankingmargen_brutotop5(DataRow[] dr)
        {
            List<Ranking_Margenes> lst = new List<Ranking_Margenes>();
            foreach (var item in dr)
            {
                lst.Add(listarankingmargen_brutotop5(item));
            }
            return lst;
        }

        public static Ranking_Margenes listarankingmargen_brutotop5(DataRow dr)
        {
            Ranking_Margenes obj = new Ranking_Margenes();

            if (dr==null)
            {
                obj.id = 0;
                obj.nombre = "0";
                obj.importe_actual = 0;
                obj.coste_actual = 0;
            }
            else
            {
                obj.id = Convert.ToDecimal(dr["ProdId"].ToString());
                obj.nombre = dr["Name"].ToString();
                obj.importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());
                obj.coste_actual = Convert.ToDouble(dr["totalcosteactual"].ToString());
            }

         
            return obj;
        }





        public static Ranking_Margenes listarankingmargen_brutotop51(DataRow dr)
        {
            Ranking_Margenes obj = new Ranking_Margenes();

            if (dr==null)
            {
                obj.id = 0;
                obj.nombre = "0";
                obj.importe_actual = 0;
                obj.coste_actual = 0;
            }
            else
            {
                obj.id = Convert.ToDecimal(dr["ProdId"].ToString());
                obj.nombre = dr["Name"].ToString();
                obj.importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());
                obj.coste_actual = Convert.ToDouble(dr["totalcosteactual"].ToString());

            }
          
            return obj;
        }







        public static List<Ranking> listarankinglinealperformance(DataRow[] dr)
        {
            List<Ranking> lst = new List<Ranking>();
            foreach (var item in dr)
            {
                lst.Add(listarankinglinealperformance(item));
            }
            return lst;
        }

        public static Ranking listarankinglinealperformance(DataRow dr)
        {

            Ranking obj = new Ranking();
            obj.idPosicion = Convert.ToDecimal(dr["Id"].ToString());
            obj.nombre = dr["Detalle"].ToString();
            obj.importeactual = Convert.ToDouble(dr["totalimporteactual"].ToString());
            obj.importeanterior = Convert.ToDouble(dr["totalimporteanterior"].ToString());
            return obj;
        }




        public static double totalrakinglinealperformance(DataRow[] dr)
        {
            double totalp = 0;

            foreach (var item in dr)
            {
                double valop = totalrakinglinealperformance(item);
                totalp = valop + totalp;
            }
            return totalp;
        }

        public static double totalrakinglinealperformance(DataRow dr)
        {
            double valor = 0;
            valor = Convert.ToDouble(dr["totalimporteactual"].ToString());

            return valor;
        }








        public static List<Ranking_Margenes> listarankingmargen_bruto_lineal(DataRow[] dr)
        {
            List<Ranking_Margenes> lst = new List<Ranking_Margenes>();
            foreach (var item in dr)
            {
                lst.Add(listarankingmargen_bruto_lineal(item));
            }
            return lst;
        }

        public static Ranking_Margenes listarankingmargen_bruto_lineal(DataRow dr)
        {
            Ranking_Margenes obj = new Ranking_Margenes();

            if (dr == null)
            {
                obj.id = 0;
                obj.nombre = "0";
                obj.importe_actual = 0;
                obj.coste_actual = 0;
            }
            else
            {
                obj.id = Convert.ToDecimal(dr["Id"].ToString());
                obj.nombre = dr["Detalle"].ToString();
                obj.importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());
                obj.coste_actual = Convert.ToDouble(dr["totalcosteactual"].ToString());

            }

            return obj;
        }


        public static Ranking_Margenes listarankingmargen_bruto_lineal1(DataRow dr)
        {
            Ranking_Margenes obj = new Ranking_Margenes();



            if (dr == null)
            {
                obj.id = 0;
                obj.nombre = "0";
                obj.importe_actual = 0;
                obj.coste_actual = 0;
            }
            else
            {
                obj.id = Convert.ToDecimal(dr["Id"].ToString());
                obj.nombre = dr["Detalle"].ToString();
                obj.importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());
                obj.coste_actual = Convert.ToDouble(dr["totalcosteactual"].ToString());

            }



            return obj;
        }





        public static List<portafolio_infoo> portafolio_info_lineal(DataRow[] dr)
        {
            List<portafolio_infoo> lst = new List<portafolio_infoo>();
            foreach (var item in dr)
            {
                lst.Add(portafolio_info_lineal(item));
            }
            return lst;
        }

        public static portafolio_infoo portafolio_info_lineal(DataRow dr)
        {
            portafolio_infoo obj = new portafolio_infoo();

            if (dr == null)
            {
                obj.Id = 0;
                obj.descripcion = "";
            }
            else
            {
                obj.Id = Convert.ToDecimal(dr["Id"].ToString());
                obj.descripcion = dr["Detalle"].ToString();
                obj.importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());
                obj.coste_actual = Convert.ToDouble(dr["totalcosteactual"].ToString());
                obj.cantidad_actual = Convert.ToDouble(dr["totalcantidadactual"].ToString());
            }
            return obj;
        }



        public static List<portafolio_infoo> portafolio_info_region(DataRow[] dr)
        {
            List<portafolio_infoo> lst = new List<portafolio_infoo>();
            foreach (var item in dr)
            {
                lst.Add(portafolio_info_region(item));
            }
            return lst;
        }

        public static portafolio_infoo portafolio_info_region(DataRow dr)
        {
            portafolio_infoo obj = new portafolio_infoo();

            if (dr == null)
            {
                obj.Id = 0;
                obj.descripcion = "";
            }
            else
            {
                obj.Id = Convert.ToDecimal(dr["warehouse_id"].ToString());
                obj.descripcion = dr["Name"].ToString();
                obj.importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());
                obj.coste_actual = Convert.ToDouble(dr["totalcosteactual"].ToString());
                obj.cantidad_actual = Convert.ToDouble(dr["totalcantidadactual"].ToString());
            }
            return obj;
        }



        public static List<portafolio_infoo> portafolio_info_top5(DataRow[] dr)
        {
            List<portafolio_infoo> lst = new List<portafolio_infoo>();
            foreach (var item in dr)
            {
                lst.Add(portafolio_info_top5(item));
            }
            return lst;
        }

        public static portafolio_infoo portafolio_info_top5(DataRow dr)
        {
            portafolio_infoo obj = new portafolio_infoo();

            if (dr == null)
            {
                obj.Id = 0;
                obj.descripcion = "";
            }
            else
            {
                obj.Id = Convert.ToDecimal(dr["ProdId"].ToString());
                obj.descripcion = dr["Name"].ToString();
                obj.importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());
                obj.coste_actual = Convert.ToDouble(dr["totalcosteactual"].ToString());
                obj.cantidad_actual = Convert.ToDouble(dr["totalcantidadactual"].ToString());
            }
            return obj;
        }




        public static double total_gross_margin(DataRow[] dr)
        {
            double totalp = 0;

            foreach (var item in dr)
            {
                double valop = total_gross_margin(item);
                totalp = valop + totalp;
            }
            return totalp;
        }

        public static double total_gross_margin(DataRow dr)
        {

            double gross_margin = 0;

            double importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());
            double coste_actual = Convert.ToDouble(dr["totalcosteactual"].ToString());


            if (importe_actual > 0 && coste_actual > 0)
            {
                double gross_profit = importe_actual - coste_actual;
                gross_margin = gross_profit / importe_actual;

            }
            else
            {
                gross_margin = 0;
            }



            return gross_margin;
        }




        public static double total_cantidad(DataRow[] dr)
        {
            double totalp = 0;

            foreach (var item in dr)
            {
                double valop = total_cantidad(item);
                totalp = valop + totalp;
            }
            return totalp;
        }

        public static double total_cantidad(DataRow dr)
        {

            double cantidadd = 0;

            double cantidad_actual = Convert.ToDouble(dr["totalcantidadactual"].ToString());
         


            if (cantidad_actual > 0 )
            {

                cantidadd = cantidad_actual;

            }
            else
            {
                cantidadd = 0;
            }



            return cantidadd;
        }



        public static double total_importe(DataRow[] dr)
        {
            double totalp = 0;

            foreach (var item in dr)
            {
                double valop = total_importe(item);
                totalp = valop + totalp;
            }
            return totalp;
        }

        public static double total_importe(DataRow dr)
        {

            double importee = 0;
            double importe_actual = Convert.ToDouble(dr["totalimporteactual"].ToString());


            if (importe_actual > 0)
            {

                importee = importe_actual;

            }
            else
            {
                importee = 0;
            }



            return importee;
        }






    }
}
