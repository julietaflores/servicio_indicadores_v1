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
using IndicadoresCore.Models.Gastos;
using System.Runtime.ExceptionServices;
using IndicadoresCore.Models.tabla_9_primero_indicadores;
using IndicadoresCore.Models.margen_bruto_regiones;
using IndicadoresCore.Models.margen_bruto_top5;
using IndicadoresCore.Models.margen_bruto_lineal;

using IndicadoresCore.Models.performance_top5;
using IndicadoresCore.Models.performance_region;
using IndicadoresCore.Models.performance_lineal;
using IndicadoresCore.Models.contribucion_de_portaforlio;

using IndicadoresCore.Models.contribucion_de_portaforlio.linea;
using IndicadoresCore.Models.contribucion_de_portaforlio.region;
using IndicadoresCore.Models.contribucion_de_portaforlio.top5;
using IndicadoresCore.Models.Grafico_ICP.Graficos;

namespace IndicadoresCore.Models
{
    public class DatabaseContext : DbContext
    {
    
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

    
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Employee> Employee { get; set; }



        public async Task<Usuario> verdatosusuario1(string UserName, string Password)
        {
            Usuario obj = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            MonedaCompaniaBC monedaEmpresaBC = new MonedaCompaniaBC();
            MesBC mesBC = new MesBC();
            CompaniaBC companiaBC = new CompaniaBC();
            IdiomaBC idiomabc = new IdiomaBC();
            AuxiliarBC auxiliarBC = new AuxiliarBC();
            string password_encriptado = Encrypt(Password);
            obj = await usuarioBC.verdatosusuario11(UserName,password_encriptado );
            if (obj.idUsuario > 0)
            {
                obj.passwordd = Decrypt(password_encriptado);
                obj.Companiaa = companiaBC.listaCompaniaxUser(obj.idUsuario,obj.CodIdioma);
                obj.idioma = idiomabc.datosidioma_x_usuarioid(obj.idUsuario);
    
     


                lista_mes lis_mes = new lista_mes();
                Auxiliar auxiliar1 = new Auxiliar();
                auxiliar1 = auxiliarBC.imformacion_lang(2, obj.CodIdioma);
                lis_mes.descripcion_mes = auxiliar1;
                lis_mes.info_mes = mesBC.listames_lang(obj.CodIdioma);
                obj.Mess = lis_mes;




                lista_anio lis_Anio = new lista_anio();
                Auxiliar auxiliar2 = new Auxiliar();
                auxiliar2 = auxiliarBC.imformacion_lang(3, obj.CodIdioma);
                lis_Anio.descripcion_anio = auxiliar2;
                obj.Anioo = lis_Anio;


            }
            return obj;
        }


        public async Task<Usuario> usuarioca(Usuario usuario)
        {
            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            IdiomaBC idiomabc = new IdiomaBC();
            usuario.fechacreacionusuario = DateTime.Now;
            usuario.passwordd = Encrypt(usuario.passwordd);
            bool ff = usuarioBC.Actualizar(ref usuario);

            if (usuario.TipoEstado == TipoEstado.Insertar)
            {
                usuario.passwordd = Decrypt(usuario.passwordd);
                usuario1 = await verdatosusuario1(usuario.usuario, usuario.passwordd);
            }
            else
            {
                if (usuario.TipoEstado == TipoEstado.Modificar)
                {
                    usuario1 = await usuarioBC.buscarxid(usuario.idUsuario);
                    if (usuario1.idUsuario > 0)
                    {
                        usuario1.passwordd = Decrypt(usuario1.passwordd);
                        usuario1.idioma = idiomabc.datosidioma_x_usuarioid(usuario.idUsuario);
                    }
                }
            }

            return usuario1;
        }

        public string Encrypt(string ToEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(ToEncrypt);

            string Key = "Bantic";
        
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                hashmd5.Clear();
      
            TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
            tDes.Key = keyArray;
            tDes.Mode = CipherMode.ECB;
            tDes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tDes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tDes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Decrypt(string cypherString)
        {
            byte[] keyArray;
            byte[] toDecryptArray = Convert.FromBase64String(cypherString.Replace(' ', '+'));

            string key = "Bantic";
        
                MD5CryptoServiceProvider hashmd = new MD5CryptoServiceProvider();
                keyArray = hashmd.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd.Clear();
           
         
            TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
            tDes.Key = keyArray;
            tDes.Mode = CipherMode.ECB;
            tDes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tDes.CreateDecryptor();
            try
            {
                byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
                tDes.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //registrra employtee
        public async Task<Employee> registraremployee(Employee employee)
        {
            Employee employee1 = new Employee();
            EmployeeBC employeeBC = new EmployeeBC();
            bool ff = employeeBC.Actualizar(ref employee);
            employee1 = await employeeBC.buscarxid(employee.Id);
            return employee1;
        }


        public async Task<List<CategoriaCompania>> Lista_menu(decimal idusuario, decimal companiaid)
        {
            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);
            List<CategoriaCompania> obj = new List<CategoriaCompania>();
            CategoriaCompaniaBC categoria_RolBC = new CategoriaCompaniaBC();
            obj = categoria_RolBC.listadatoscategoriacompania_lista_menu(idusuario, usuario1.CodIdioma, companiaid);
            return obj;
        }



        public async Task<Detalle_receptor> DetalleCifrasNotables(decimal idusuario, decimal idcompania, decimal categoriacompaniaid, decimal tableroid,  int anio, string mes,  decimal monedadestino)
        {
            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);


            List<Indicador> indicadors = new List<Indicador>();
            IndicadorBC indicadorBC = new IndicadorBC();
            indicadors = indicadorBC.datosindicador_nuevo_menu_lang(idusuario, idcompania, categoriacompaniaid, tableroid, usuario1.CodIdioma);
  

            Detalle_receptor detalle_Receptor = new Detalle_receptor();
            TableroBC tableroBC = new TableroBC();
            detalle_Receptor.tablero = tableroBC.datostableroid(tableroid, usuario1.CodIdioma);
            List<Devolucion> devolucions = new List<Devolucion>();
            int anioant = anio - 1;

            // datos de la compania q selecciono
            CompaniaBC companiaBCc = new CompaniaBC();
            Compania info_compania = companiaBCc.datosempresacompaniaid(idcompania);


            //datos de la moneda destino
            MonedaCompaniaBC monedacompaniaBC1 = new MonedaCompaniaBC();
            MonedaCompania moneda_destinoo = monedacompaniaBC1.datoscompaniamoneda(info_compania.IdCompania, monedadestino);


            DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa111(info_compania.IdEmpresa);


            foreach (var list in indicadors)
            {
                decimal idindicadorr = list.idIndicador;
                switch (idindicadorr)
                {
                    case 1:
                        Cantidad_1 cantidad_1 = new Cantidad_1();
                        Devolucion devolucion1 = cantidad_1.devolver_cantidad_1(usuario1, anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        devolucions.Add(devolucion1);


                        break;
                    case 2:
                        Ventas_2 ventas_2 = new Ventas_2();
                        Devolucion devolucion2 = ventas_2.devolver_ventas_2(usuario1, anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        devolucions.Add(devolucion2);

                        break;
                    case 3:
                        Precio_Promedio_3 precio_Promedio_3 = new Precio_Promedio_3();
                        Devolucion devolucion3 = precio_Promedio_3.devolver_Precio_Promedio_3(usuario1, anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        devolucions.Add(devolucion3);

                        break;
                    case 4:
                        Gross_Profit_4 gross_Profit_4 = new Gross_Profit_4();
                        Devolucion devolucion4 = gross_Profit_4.devolver_gross_profit_4(usuario1, anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        devolucions.Add(devolucion4);

                        break;
                    case 5:
                        Gross_Margin_5 gross_Margin_5 = new Gross_Margin_5();
                        Devolucion devolucion5 = gross_Margin_5.devolver_gross_margin_5(usuario1, anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        devolucions.Add(devolucion5);


                        break;
                    case 6:




                        break;
                    case 7:



                        break;
                    case 8:



                        break;
                    case 9:



                        break;
                }
            }




            detalle_Receptor.Lista = devolucions;
            return detalle_Receptor;

        }



        public async Task<Performance> Performancelineal(decimal idusuario, decimal idcompania, decimal categoriacompaniaid, decimal tableroid, int anio, string mes, decimal monedadestino)
        {

            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);

            List<Indicador> indicadors = new List<Indicador>();
            IndicadorBC indicadorBC = new IndicadorBC();
            indicadors = indicadorBC.datosindicador_nuevo_menu_lang(idusuario, idcompania, categoriacompaniaid, tableroid, usuario1.CodIdioma);


            Performance performance = new Performance();
            TableroBC tableroBC = new TableroBC();
            performance.tablero = tableroBC.datostableroid(tableroid, usuario1.CodIdioma);
            int anioant = anio - 1;





            // datos de la compania q selecciono
            CompaniaBC companiaBCc = new CompaniaBC();
            Compania info_compania = companiaBCc.datosempresacompaniaid(idcompania);


            //datos de la moneda destino
            MonedaCompaniaBC monedacompaniaBC1 = new MonedaCompaniaBC();
            MonedaCompania moneda_destinoo = monedacompaniaBC1.datoscompaniamoneda(info_compania.IdCompania, monedadestino);


            DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa111(info_compania.IdEmpresa);



            for (int nn = 1; nn <= 2; nn++)
            {
                switch (nn)
                {
                    case 1:

                        lineal_mes_performance lineal_Mes_Performancee = new lineal_mes_performance();
                        List<Ranking> lst = new List<Ranking>();
                        lst = lineal_Mes_Performancee.devolver_mes_lineal(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Listames = lst;
                        break;

                    case 2:

                        lineal_anual_performance lineal_Anual_Performancee = new lineal_anual_performance();
                        List<Ranking> lst1 = new List<Ranking>();
                        lst1 = lineal_Anual_Performancee.devolver_anual_lineal(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Listaanual = lst1;
                        break;

                }

            }
            return performance;
        }



        public async Task<Performance> Performancetopcinco(decimal idusuario, decimal idcompania, decimal categoriacompaniaid, decimal tableroid, int anio, string mes, decimal monedadestino)
        {


            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);


            List<Indicador> indicadors = new List<Indicador>();
            IndicadorBC indicadorBC = new IndicadorBC();
            indicadors = indicadorBC.datosindicador_nuevo_menu_lang(idusuario, idcompania, categoriacompaniaid, tableroid, usuario1.CodIdioma);



            Performance performance = new Performance();
            TableroBC tableroBC = new TableroBC();
            performance.tablero = tableroBC.datostableroid(tableroid, usuario1.CodIdioma);
            int anioant = anio - 1;



            // datos de la compania q selecciono
            CompaniaBC companiaBCc = new CompaniaBC();
            Compania info_compania = companiaBCc.datosempresacompaniaid(idcompania);


            //datos de la moneda destino
            MonedaCompaniaBC monedacompaniaBC1 = new MonedaCompaniaBC();
            MonedaCompania moneda_destinoo = monedacompaniaBC1.datoscompaniamoneda(info_compania.IdCompania, monedadestino);


            DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa111(info_compania.IdEmpresa);





            for (int nn = 1; nn <= 1; nn++)
            {
                switch (nn)
                {
                    case 1:
                        top5_mes_performance mes_top55_performance = new top5_mes_performance();
                        List<Ranking> lst = new List<Ranking>();
                        lst = mes_top55_performance.devolver_mes_top5(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Listames = lst;

                        top5_anual_performance acumulado_Top5_performance = new top5_anual_performance();
                        List<Ranking> lst1 = new List<Ranking>();
                        lst1 = acumulado_Top5_performance.devolver_anual_top5(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Listaanual = lst1;
                        break;


                }

            }
            return performance;
        }






        public async Task<PerformanceRegion> Performanceregion(decimal idusuario, decimal idcompania, decimal categoriacompaniaid, decimal tableroid, int anio, string mes, decimal monedadestino)
        {

            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);

            PerformanceRegion performance = new PerformanceRegion();
            TableroBC tableroBC = new TableroBC();
            performance.tablero = tableroBC.datostableroid(tableroid, usuario1.CodIdioma);
            int anioant = anio - 1;


            // datos de la compania q selecciono
            CompaniaBC companiaBCc = new CompaniaBC();
            Compania info_compania = companiaBCc.datosempresacompaniaid(idcompania);


            //datos de la moneda destino
            MonedaCompaniaBC monedacompaniaBC1 = new MonedaCompaniaBC();
            MonedaCompania moneda_destinoo = monedacompaniaBC1.datoscompaniamoneda(info_compania.IdCompania, monedadestino);


            DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa111(info_compania.IdEmpresa);



            for (int nn = 1; nn <= 1; nn++)
            {
                switch (nn)
                {
                    case 1:
                        region_mes_performance region_mes_perfor = new region_mes_performance();
                        List<Ranking> lst = new List<Ranking>();
                        lst = region_mes_perfor.devolver_mes_region(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Listames = lst;

                        region_anual_performance region_acumulado_perfor = new region_anual_performance();
                        List<Ranking> lst1 = new List<Ranking>();
                        lst1 = region_acumulado_perfor.devolver_acumulado_region(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Listaanual = lst1;
                        break;
                }

            }
            return performance;
        }











        public async Task<composicion_de_ventas> composicion_ventas(decimal idusuario, decimal idcompania, decimal categoriacompaniaid, decimal tableroid, int anio, string mes)
        {

            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);

            composicion_de_ventas composicion_ventas = new composicion_de_ventas();
            TableroBC tableroBC = new TableroBC();
            composicion_ventas.tablero = tableroBC.datostableroid(tableroid, usuario1.CodIdioma);
            List<Performance_indicadores> devolucions = new List<Performance_indicadores>();
            int anioant = anio - 1;





            List<Indicador> indicadors = new List<Indicador>();
            IndicadorBC indicadorBC = new IndicadorBC();
            indicadors = indicadorBC.datosindicador_nuevo_menu_lang(idusuario, idcompania, categoriacompaniaid, tableroid, usuario1.CodIdioma);


            // datos de la compania q selecciono
            CompaniaBC companiaBCc = new CompaniaBC();
            Compania info_compania = companiaBCc.datosempresacompaniaid(idcompania);


            //datos de la moneda destino
            MonedaCompaniaBC monedacompaniaBC1 = new MonedaCompaniaBC();
            MonedaCompania moneda_destinoo = monedacompaniaBC1.datoscompaniamoneda(info_compania.IdCompania, info_compania.IdMonedaOdoo);


            DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa111(info_compania.IdEmpresa);



            foreach (var list in indicadors)
            {

                Performance_indicadores Indicadores1 = new Performance_indicadores();
                Indicador indicadorf = new Indicador();
                indicadorf = indicadorBC.datosindicador_lang(list.idIndicador, usuario1.CodIdioma);
                Indicadores1.indicador = indicadorf;



                switch (list.idIndicador)
                {
                    case 10:
                        top5_mes_performance mes_top55 = new top5_mes_performance();
                        List<Ranking> lst = new List<Ranking>();
                        lst = mes_top55.devolver_mes_top5(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        Indicadores1.Lista_mes = lst;

                        top5_anual_performance acumulado_Top5 = new top5_anual_performance();
                        List<Ranking> lst1 = new List<Ranking>();
                        lst1 = acumulado_Top5.devolver_anual_top5(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        Indicadores1.Lista_anual = lst1;
                        devolucions.Add(Indicadores1);
                        break;

                    case 11:
                        region_mes_performance mes_regionn = new region_mes_performance();
                        List<Ranking> lstrm = new List<Ranking>();
                        lstrm = mes_regionn.devolver_mes_region_torta(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        Indicadores1.Lista_mes = lstrm;


                        region_anual_performance acumulado_Region = new region_anual_performance();
                        List<Ranking> lst1ra = new List<Ranking>();
                        lst1ra = acumulado_Region.devolver_acumulado_region_torta(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        Indicadores1.Lista_anual = lst1ra;

                        devolucions.Add(Indicadores1);
                        break;


                    case 14:

                        lineal_mes_performance mes_lineal = new lineal_mes_performance();
                        List<Ranking> lmlm = new List<Ranking>();
                        lmlm = mes_lineal.devolver_mes_lineal_torta(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        Indicadores1.Lista_mes = lmlm;


                        lineal_anual_performance acumulado_lineal = new lineal_anual_performance();
                        List<Ranking> lala = new List<Ranking>();
                        lala = acumulado_lineal.devolver_anual_lineal_torta(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        Indicadores1.Lista_anual = lala;

                        devolucions.Add(Indicadores1);
                        break;

                }
            }
            composicion_ventas.lista = devolucions;
            return composicion_ventas;

        }







        public async Task<Margen_bruto> Margenbruto_lineal(decimal idusuario, decimal idcompania, decimal categoriacompaniaid, decimal tableroid, int anio, string mes, decimal monedadestino)
        {




            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);


            List<Indicador> indicadors = new List<Indicador>();
            IndicadorBC indicadorBC = new IndicadorBC();
            indicadors = indicadorBC.datosindicador_nuevo_menu_lang(idusuario, idcompania, categoriacompaniaid, tableroid, usuario1.CodIdioma);


            Margen_bruto performance = new Margen_bruto();
            TableroBC tableroBC = new TableroBC();
            performance.tablero = tableroBC.datostableroid(tableroid, usuario1.CodIdioma);
            int anioant = anio - 1;


            // datos de la compania q selecciono
            CompaniaBC companiaBCc = new CompaniaBC();
            Compania info_compania = companiaBCc.datosempresacompaniaid(idcompania);


            //datos de la moneda destino
            MonedaCompaniaBC monedacompaniaBC1 = new MonedaCompaniaBC();
            MonedaCompania moneda_destinoo = monedacompaniaBC1.datoscompaniamoneda(info_compania.IdCompania, monedadestino);


            DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa111(info_compania.IdEmpresa);


            //DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            //DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa11(usuario1.IDRolUsuario);

            //MonedaEmpresaBC monedaEmpresaBC1 = new MonedaEmpresaBC();
            //MonedaEmpresa moneda_destinoo = monedaEmpresaBC1.datosempresamoneda(dBEmpresac.IdEmpresa, monedadestino);

            //CompaniaBC companiaBCc = new CompaniaBC();
            //Compania info_compania = companiaBCc.datosempresacompania(dBEmpresac.IdEmpresa, idcompania);


            for (int nn = 1; nn <= 1; nn++)
            {
                switch (nn)
                {
                    case 1:
                        lineal_mes_margen_bruto mes_lineal_mb = new lineal_mes_margen_bruto();
                        List<Ranking_Margenes> lst = new List<Ranking_Margenes>();
                        lst = mes_lineal_mb.devolver_mes_mg_lineal(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Lista_mes = lst;

                        lineal_anual_margen_bruto anual_lineal_mb = new lineal_anual_margen_bruto();
                        List<Ranking_Margenes> lst1 = new List<Ranking_Margenes>();
                        lst1 = anual_lineal_mb.devolver_anual_mg_lineal(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Lista_anual = lst1;
                        break;
                }
            }
            return performance;
        }




        public async Task<Margen_bruto> Margenbruto_top5(decimal idusuario, decimal idcompania, decimal categoriacompaniaid, decimal tableroid, int anio, string mes, decimal monedadestino)
        {

            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);


            List<Indicador> indicadors = new List<Indicador>();
            IndicadorBC indicadorBC = new IndicadorBC();
            indicadors = indicadorBC.datosindicador_nuevo_menu_lang(idusuario, idcompania, categoriacompaniaid, tableroid, usuario1.CodIdioma);


            Margen_bruto performance = new Margen_bruto();
            TableroBC tableroBC = new TableroBC();
            performance.tablero = tableroBC.datostableroid(tableroid, usuario1.CodIdioma);
            int anioant = anio - 1;

            // datos de la compania q selecciono
            CompaniaBC companiaBCc = new CompaniaBC();
            Compania info_compania = companiaBCc.datosempresacompaniaid(idcompania);


            //datos de la moneda destino
            MonedaCompaniaBC monedacompaniaBC1 = new MonedaCompaniaBC();
            MonedaCompania moneda_destinoo = monedacompaniaBC1.datoscompaniamoneda(info_compania.IdCompania, monedadestino);


            DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa111(info_compania.IdEmpresa);

            //DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            //DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa11(usuario1.IDRolUsuario);

            //MonedaEmpresaBC monedaEmpresaBC1 = new MonedaEmpresaBC();
            //MonedaEmpresa moneda_destinoo = monedaEmpresaBC1.datosempresamoneda(dBEmpresac.IdEmpresa, monedadestino);

            //CompaniaBC companiaBCc = new CompaniaBC();
            //Compania info_compania = companiaBCc.datosempresacompania(dBEmpresac.IdEmpresa, idcompania);


            for (int nn = 1; nn <= 1; nn++)
            {
                switch (nn)
                {
                    case 1:
                        mes_top5 mes_top55 = new mes_top5();
                        List<Ranking_Margenes> lst = new List<Ranking_Margenes>();
                        lst = mes_top55.devolver_mes(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Lista_mes = lst;

                        acumulado_top5 acumulado_Top5 = new acumulado_top5();
                        List<Ranking_Margenes> lst1 = new List<Ranking_Margenes>();
                        lst1 = acumulado_Top5.devolver_top5_acumulado(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Lista_anual = lst1;
                        break;


                }

            }

            return performance;
        }



        public async Task<Margen_bruto> Margenbruto_region(decimal idusuario, decimal idcompania, decimal categoriacompaniaid, decimal tableroid, int anio, string mes, decimal monedadestino)
        {
            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);



            List<Indicador> indicadors = new List<Indicador>();
            IndicadorBC indicadorBC = new IndicadorBC();
            indicadors = indicadorBC.datosindicador_nuevo_menu_lang(idusuario, idcompania, categoriacompaniaid, tableroid, usuario1.CodIdioma);


            Margen_bruto performance = new Margen_bruto();
            TableroBC tableroBC = new TableroBC();
            performance.tablero = tableroBC.datostableroid(tableroid, usuario1.CodIdioma);
            int anioant = anio - 1;



            // datos de la compania q selecciono
            CompaniaBC companiaBCc = new CompaniaBC();
            Compania info_compania = companiaBCc.datosempresacompaniaid(idcompania);


            //datos de la moneda destino
            MonedaCompaniaBC monedacompaniaBC1 = new MonedaCompaniaBC();
            MonedaCompania moneda_destinoo = monedacompaniaBC1.datoscompaniamoneda(info_compania.IdCompania, monedadestino);


            DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa111(info_compania.IdEmpresa);


            for (int nn = 1; nn <= 1; nn++)
            {
                switch (nn)
                {
                    case 1:
                        mes_region mes_regionn = new mes_region();
                        List<Ranking_Margenes> lst = new List<Ranking_Margenes>();
                        lst = mes_regionn.devolver_mes_region(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Lista_mes = lst;


                        acumulado_region acumulado_Region = new acumulado_region();
                        List<Ranking_Margenes> lst1 = new List<Ranking_Margenes>();
                        lst1 = acumulado_Region.devolver_acumulado_region(anioant, anio, mes, info_compania, dBEmpresac.idDB, moneda_destinoo);
                        performance.Lista_anual = lst1;
                        break;
                }
            }

            return performance;
        }

        public async Task<composicion_de_margenes> composicion_margenes(decimal idusuario, decimal idcompania, decimal categoriacompaniaid, decimal tableroid, int anio, string mes)
        {

            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);

            composicion_de_margenes composicion_margenes = new composicion_de_margenes();
            TableroBC tableroBC = new TableroBC();
            composicion_margenes.tablero = tableroBC.datostableroid(tableroid, usuario1.CodIdioma);
            List<Margen_bruto_indicadores> devolucions = new List<Margen_bruto_indicadores>();
            int anioant = anio - 1;



            List<Indicador> listadatosindricaresxta = new List<Indicador>();
            IndicadorBC indicadorBC = new IndicadorBC();
            listadatosindricaresxta = indicadorBC.datosindicador_nuevo_menu_lang(idusuario, idcompania, categoriacompaniaid, tableroid, usuario1.CodIdioma);

            // datos de la compania q selecciono
            CompaniaBC companiaBCc = new CompaniaBC();
            Compania info_compania = companiaBCc.datosempresacompaniaid(idcompania);


        
            DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa111(info_compania.IdEmpresa);

           
            foreach (var list in listadatosindricaresxta)
            {

                Margen_bruto_indicadores margen_Bruto_Indicadores1 = new Margen_bruto_indicadores();
                Indicador indicadorf = new Indicador();
                indicadorf = indicadorBC.datosindicador_lang(list.idIndicador, usuario1.CodIdioma);
                margen_Bruto_Indicadores1.indicador = indicadorf;


                switch (list.idIndicador)
                {
                    case 12:
                        mes_top5 mes_top55 = new mes_top5();
                        List<Ranking_Margenes> lst = new List<Ranking_Margenes>();
                        lst = mes_top55.devolver_mes_torta(anioant, anio, mes, info_compania, dBEmpresac.idDB);
                        margen_Bruto_Indicadores1.Lista_mes = lst;

                        acumulado_top5 acumulado_Top5 = new acumulado_top5();
                        List<Ranking_Margenes> lst1 = new List<Ranking_Margenes>();
                        lst1 = acumulado_Top5.devolver_top5_acumulado_torta(anioant, anio, mes, info_compania, dBEmpresac.idDB);
                        margen_Bruto_Indicadores1.Lista_anual = lst1;

                        devolucions.Add(margen_Bruto_Indicadores1);


                        break;


                    case 13:



                        mes_region mes_regionn = new mes_region();
                        List<Ranking_Margenes> lstrm = new List<Ranking_Margenes>();
                        lstrm = mes_regionn.devolver_mes_region_torta(anioant, anio, mes, info_compania, dBEmpresac.idDB);
                        margen_Bruto_Indicadores1.Lista_mes = lstrm;


                        acumulado_region acumulado_Region = new acumulado_region();
                        List<Ranking_Margenes> lst1ra = new List<Ranking_Margenes>();
                        lst1ra = acumulado_Region.devolver_acumulado_region_torta(anioant, anio, mes, info_compania, dBEmpresac.idDB);
                        margen_Bruto_Indicadores1.Lista_anual = lst1ra;

                        devolucions.Add(margen_Bruto_Indicadores1);
                        break;


                    case 15:


                        lineal_mes_margen_bruto mes_lineal_mb = new lineal_mes_margen_bruto();
                        List<Ranking_Margenes> lstmes_l_mb = new List<Ranking_Margenes>();
                        lstmes_l_mb = mes_lineal_mb.devolver_mes_mg_lineal_torta(anioant, anio, mes, info_compania, dBEmpresac.idDB);
                        margen_Bruto_Indicadores1.Lista_mes = lstmes_l_mb;

                        lineal_anual_margen_bruto anual_lineal_mb = new lineal_anual_margen_bruto();
                        List<Ranking_Margenes> lst1_anual_l_mb = new List<Ranking_Margenes>();
                        lst1_anual_l_mb = anual_lineal_mb.devolver_anual_mg_lineal_torta(anioant, anio, mes, info_compania, dBEmpresac.idDB);
                        margen_Bruto_Indicadores1.Lista_anual = lst1_anual_l_mb;

                        devolucions.Add(margen_Bruto_Indicadores1);
                        break;
                }
            }
            composicion_margenes.lista = devolucions;
            return composicion_margenes;

        }



        public async Task<portaforlio> Contribución_del_portafolio(decimal idusuario, decimal idcompania, decimal categoriacompaniaid, decimal tableroid, int anio, string mes)
        {

            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);

            portaforlio portaforlioo = new portaforlio();
            TableroBC tableroBC = new TableroBC();
            portaforlioo.tablero = tableroBC.datostableroid(tableroid, usuario1.CodIdioma);
            List<portafolio_descripcion> devolucions = new List<portafolio_descripcion>();
            int anioant = anio - 1;



            List<Indicador> listadatosindricaresxta = new List<Indicador>();
            IndicadorBC indicadorBC = new IndicadorBC();
            listadatosindricaresxta = indicadorBC.datosindicador_nuevo_menu_lang(idusuario, idcompania, categoriacompaniaid, tableroid, usuario1.CodIdioma);

            // datos de la compania q selecciono
            CompaniaBC companiaBCc = new CompaniaBC();
            Compania info_compania = companiaBCc.datosempresacompaniaid(idcompania);



            DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa111(info_compania.IdEmpresa);


           
            foreach (var list in listadatosindricaresxta)
            {

                portafolio_descripcion infoo = new portafolio_descripcion();
                Indicador indicadorf = new Indicador();
                indicadorf = indicadorBC.datosindicador_lang(list.idIndicador, usuario1.CodIdioma);
                infoo.indicador = indicadorf;


                switch (list.idIndicador)
                {
                    case 16:
                        List<portafolio_datos> portafolio_Datos1 = new List<portafolio_datos>();
                        Lineal_cp_total lineal_total = new Lineal_cp_total();
                        portafolio_Datos1 = lineal_total.devolver_lineal_cp(anioant, anio, mes, info_compania, dBEmpresac.idDB);
                        infoo.Lista_informacion = portafolio_Datos1;
                        devolucions.Add(infoo);

                        break;


                    case 17:
                        List<portafolio_datos> portafolio_datos2 = new List<portafolio_datos>();
                        Region_cp_total region_total = new Region_cp_total();
                        portafolio_datos2 = region_total.devolver_region_cp(anioant, anio, mes, info_compania, dBEmpresac.idDB);
                        infoo.Lista_informacion = portafolio_datos2;
                        devolucions.Add(infoo);

                        break;

                    case 18:
                        List<portafolio_datos> portafolio_datos3 = new List<portafolio_datos>();
                        Top5_cp_total top5_total = new Top5_cp_total();
                        portafolio_datos3 = top5_total.devolver_top5_cp(anioant, anio, mes, info_compania, dBEmpresac.idDB);
                        infoo.Lista_informacion = portafolio_datos3;
                        devolucions.Add(infoo);

                        break;

                }
            }
            portaforlioo.portafolio_Descripcions = devolucions;
            return portaforlioo;

        }

        public async Task<Data_graficaICP> EvolucionCxCGrafica(decimal idusuario, decimal idcompania, decimal categoriacompaniaid, decimal tableroid, int anio, string mes, int monedadestino)
        {

            Usuario usuario1 = new Usuario();
            UsuarioBC usuarioBC = new UsuarioBC();
            usuario1 = await usuarioBC.buscarxid(idusuario);

            portaforlio portaforlioo = new portaforlio();
            TableroBC tableroBC = new TableroBC();
            portaforlioo.tablero = tableroBC.datostableroid(tableroid, usuario1.CodIdioma);
            List<portafolio_descripcion> devolucions = new List<portafolio_descripcion>();
            int anioant = anio - 1;

            CompaniaBC companiaBCc = new CompaniaBC();
            Compania info_compania = companiaBCc.datosempresacompaniaid(idcompania);

            MonedaCompaniaBC monedacompaniaBC1 = new MonedaCompaniaBC();
            MonedaCompania moneda_destinoo = monedacompaniaBC1.datoscompaniamoneda(info_compania.IdCompania, monedadestino);

            List<Indicador> listadatosindricaresxta = new List<Indicador>();
            IndicadorBC indicadorBC = new IndicadorBC();
            listadatosindricaresxta = indicadorBC.datosindicador_nuevo_menu_lang(idusuario, idcompania, categoriacompaniaid, tableroid, usuario1.CodIdioma);

               DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
            DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa111(info_compania.IdEmpresa);


            Data_graficaICP respGrafico = new Data_graficaICP();
            Graficos_icp lineal_total = new Graficos_icp();
            respGrafico = lineal_total.ICP_graficos(anioant, anio, mes, info_compania, dBEmpresac.idDB);
            //infoo.Lista_informacion = portafolio_Datos1;
           // devolucions.Add(infoo);

            foreach (var list in listadatosindricaresxta)
            {

                portafolio_descripcion infoo = new portafolio_descripcion();
                Indicador indicadorf = new Indicador();
                indicadorf = indicadorBC.datosindicador_lang(list.idIndicador, usuario1.CodIdioma);
                infoo.indicador = indicadorf;


                switch (list.idIndicador)
                {
                    case 35:
                        //List<portafolio_datos> portafolio_Datos1 = new List<portafolio_datos>();
                        //Graficos_icp lineal_total = new Graficos_icp();
                        //portafolio_Datos1 = lineal_total.ICP_graficos(anioant, anio, mes, info_compania, dBEmpresac.idDB);
                        //infoo.Lista_informacion = portafolio_Datos1;
                        //devolucions.Add(infoo);

                        break;

                }
            }
            return respGrafico;

        }




        //public async Task<Gastos_tablero> Gastos_Indicadores(decimal idusuario, int anio, string mes, int idcompania, int monedadestino)
        //{
        //    Usuario usuario1 = new Usuario();
        //    UsuarioBC usuarioBC = new UsuarioBC();
        //    usuario1 = await usuarioBC.buscarxid(idusuario);

        //    Gastos_tablero Gastos_I = new Gastos_tablero();

        //    TableroBC tableroBC = new TableroBC();
        //    Gastos_I.tablero = tableroBC.datostableroid(Convert.ToDecimal(11), usuario1.CodIdioma);
        //    int anioant = anio - 1;
        //    DBEmpresaBC dBEmpresaBC = new DBEmpresaBC();
        //    DBEmpresa dBEmpresac = dBEmpresaBC.listadebasexEmpresa11(usuario1.IDRolUsuario);


        //    MonedaEmpresaBC monedaEmpresaBC1 = new MonedaEmpresaBC();
        //    MonedaEmpresa moneda_destinoo = monedaEmpresaBC1.datosempresamoneda(dBEmpresac.IdEmpresa, monedadestino);

        //    CompaniaBC companiaBCc = new CompaniaBC();
        //    Compania info_compania = companiaBCc.datosempresacompania(dBEmpresac.IdEmpresa, idcompania);

        //    IndicadorBC indicadorBC = new IndicadorBC();
        //    List<Indicador> listadatosindricaresxta = indicadorBC.listadatosindricaresxtablero(11, usuario1.CodIdioma);
        //    List<Gastos_li_MA> devolucion = new List<Gastos_li_MA>();


        //    foreach (var list in listadatosindricaresxta)
        //    {

        //        Gastos_li_MA infoo = new Gastos_li_MA();
        //        Indicador indicadorf = new Indicador();
        //        indicadorf = indicadorBC.datosindicador_lang(list.idIndicador, usuario1.CodIdioma);
        //        infoo.Indicadordatos = indicadorf;





        //        devolucion.Add(infoo);


        //    }

        //    Gastos_I.informacion = devolucion;
        //    return Gastos_I;
        //}
















    }
}
