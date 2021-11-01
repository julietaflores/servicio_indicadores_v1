using IndicadoresCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using IndicadoresCore.Models.BC;
using IndicadoresCore.Models.Gastos;

namespace IndicadoresCore.Services
{
    public class Service : IService
    {
        private readonly DatabaseContext _dbContext;


        public Service(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }



        public async Task<Employee> Creacionn(Employee employee)
        {
            Task<Employee> employee1 = _dbContext.registraremployee(employee);
            return await employee1;
        }



        public async Task<Usuario> UsuarioCA(Usuario usuario)
        {
            Task<Usuario> usuario1 = _dbContext.usuarioca(usuario);
            return await usuario1;
        }

        public IQueryable<Usuario> GetAll_user()
        {
            return _dbContext.Usuario.AsQueryable();
        }


        public async Task<Usuario> ValidarLoginWeb1(string Usuario, string Clave)
        {
            Task<Usuario> usuario1 = _dbContext.verdatosusuario1(Usuario, Clave);
            return await usuario1;
        }

        public async Task<List<CategoriaCompania>> Lista_Menu(int idusuario, int companiaid)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            Task<List<CategoriaCompania>> categoriacompania = _dbContext.Lista_menu(idusuarioo, idcompania);
            return await categoriacompania;
        }

        public async Task<Detalle_receptor> DetalleCifrasNotables(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes,  int monedadestino)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            decimal idcategoricompania = Convert.ToDecimal(categoriacompaniaid);
            decimal idtablero = Convert.ToDecimal(tableroid);
            decimal moneda_destino = Convert.ToDecimal(monedadestino);
            Task<Detalle_receptor> detallecifrasnotabless = _dbContext.DetalleCifrasNotables(idusuarioo, idcompania, idcategoricompania , idtablero, anio, mes,  moneda_destino);
            return await detallecifrasnotabless;
        }


        public async Task<Performance> Performancelineal(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            decimal idcategoricompania = Convert.ToDecimal(categoriacompaniaid);
            decimal idtablero = Convert.ToDecimal(tableroid);
            decimal moneda_destino = Convert.ToDecimal(monedadestino);
            Task<Performance> detallecis = _dbContext.Performancelineal(idusuarioo, idcompania, idcategoricompania, idtablero, anio, mes, moneda_destino);
            return await detallecis;
        }

        public async Task<Performance> Performancetop5(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            decimal idcategoricompania = Convert.ToDecimal(categoriacompaniaid);
            decimal idtablero = Convert.ToDecimal(tableroid);
            decimal moneda_destino = Convert.ToDecimal(monedadestino);
            Task<Performance> detallecis = _dbContext.Performancetopcinco(idusuarioo, idcompania, idcategoricompania, idtablero, anio, mes, moneda_destino);
            return await detallecis;
        }


        public async Task<PerformanceRegion> Performanceregion(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            decimal idcategoricompania = Convert.ToDecimal(categoriacompaniaid);
            decimal idtablero = Convert.ToDecimal(tableroid);
            decimal moneda_destino = Convert.ToDecimal(monedadestino);
            Task<PerformanceRegion> detallecis = _dbContext.Performanceregion(idusuarioo, idcompania, idcategoricompania, idtablero, anio, mes, moneda_destino);
            return await detallecis;
        }



        public async Task<composicion_de_ventas> Composicion_ventas(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            decimal idcategoricompania = Convert.ToDecimal(categoriacompaniaid);
            decimal idtablero = Convert.ToDecimal(tableroid);

            Task<composicion_de_ventas> detallecis = _dbContext.composicion_ventas(idusuarioo, idcompania, idcategoricompania, idtablero, anio, mes);
            return await detallecis;
        }






        public async Task<Margen_bruto> Margenbruto_lineal1(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            decimal idcategoricompania = Convert.ToDecimal(categoriacompaniaid);
            decimal idtablero = Convert.ToDecimal(tableroid);
            decimal moneda_destino = Convert.ToDecimal(monedadestino);
            Task<Margen_bruto> detallecis = _dbContext.Margenbruto_lineal(idusuarioo, idcompania, idcategoricompania, idtablero, anio, mes, moneda_destino);
            return await detallecis;
        }



        public async Task<Margen_bruto> Margenbruto_region1(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            decimal idcategoricompania = Convert.ToDecimal(categoriacompaniaid);
            decimal idtablero = Convert.ToDecimal(tableroid);
            decimal moneda_destino = Convert.ToDecimal(monedadestino);
            Task<Margen_bruto> detallecis = _dbContext.Margenbruto_region(idusuarioo, idcompania, idcategoricompania, idtablero, anio, mes, moneda_destino);
            return await detallecis;
        }

        public async Task<Margen_bruto> Margenbruto_top51(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            decimal idcategoricompania = Convert.ToDecimal(categoriacompaniaid);
            decimal idtablero = Convert.ToDecimal(tableroid);
            decimal moneda_destino = Convert.ToDecimal(monedadestino);
            Task<Margen_bruto> detallecis = _dbContext.Margenbruto_top5(idusuarioo, idcompania, idcategoricompania, idtablero, anio, mes, moneda_destino);
            return await detallecis;
        }

        public async Task<composicion_de_margenes> Composicion_margenes(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            decimal idcategoricompania = Convert.ToDecimal(categoriacompaniaid);
            decimal idtablero = Convert.ToDecimal(tableroid);
           
            Task<composicion_de_margenes> detallecis = _dbContext.composicion_margenes(idusuarioo, idcompania, idcategoricompania, idtablero, anio, mes);
            return await detallecis;
        }



        public async Task<portaforlio> Contribucion_del_portafolio(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            decimal idcategoricompania = Convert.ToDecimal(categoriacompaniaid);
            decimal idtablero = Convert.ToDecimal(tableroid);
           
            Task<portaforlio> detallecis = _dbContext.Contribución_del_portafolio(idusuarioo, idcompania, idcategoricompania, idtablero, anio, mes);
            return await detallecis;
        }

        public async Task<Data_graficaICP> EvolucionCxCGrafica(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino)
        {
            decimal idusuarioo = Convert.ToDecimal(idusuario);
            decimal idcompania = Convert.ToDecimal(companiaid);
            decimal idcategoricompania = Convert.ToDecimal(categoriacompaniaid);
            decimal idtablero = Convert.ToDecimal(tableroid);

            Task<Data_graficaICP> detallecis = _dbContext.EvolucionCxCGrafica(idusuarioo, idcompania, idcategoricompania, idtablero, anio, mes, monedadestino);
            return await detallecis;
        }




        ////gastos
        //        public async Task<Gastos_tablero> Gastos(int idusuario, int anio, string mes, int compania, int monedadestino)
        //        {
        //            decimal idusuarioo = Convert.ToDecimal(idusuario);
        //            Task<Gastos_tablero> Gastos_detalle = _dbContext.Gastos_Indicadores(idusuarioo, anio, mes, compania, monedadestino);
        //            return await Gastos_detalle;
        //        }










    }
}
