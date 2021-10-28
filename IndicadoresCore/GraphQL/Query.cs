
using IndicadoresCore.Models;
using IndicadoresCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndicadoresCore.Models.Gastos;

namespace IndicadoresCore.GraphQL
{
    public class Query
    {
        private readonly IService _Service;
        public Query(IService Service)
        {
            _Service = Service;
        }
        public IQueryable<Usuario> Usuario => _Service.GetAll_user();


        public Task<Usuario> Validarlogin(string usuario, string clave) => _Service.ValidarLoginWeb1(usuario, clave);
        public Task<List<CategoriaCompania>> Lista_Menu(int idusuario, int companiaid) => _Service.Lista_Menu(idusuario, companiaid);

        public Task<Detalle_receptor> DetalleCifrasNotables(int idusuario,int companiaid,int categoriacompaniaid,int tableroid, int anio, string mes, int monedadestino) => _Service.DetalleCifrasNotables(idusuario, companiaid,categoriacompaniaid,tableroid,anio, mes, monedadestino);

        public Task<Performance> Performancelineal(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino) => _Service.Performancelineal(idusuario, companiaid, categoriacompaniaid, tableroid, anio, mes, monedadestino);

        public Task<Performance> performancetop5(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino) => _Service.Performancetop5(idusuario, companiaid, categoriacompaniaid, tableroid, anio, mes, monedadestino);
        public Task<PerformanceRegion> performanceregion(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino) => _Service.Performanceregion(idusuario, companiaid, categoriacompaniaid, tableroid, anio, mes, monedadestino);
        public Task<composicion_de_ventas> Composicion_ventas(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes) => _Service.Composicion_ventas(idusuario, companiaid, categoriacompaniaid, tableroid, anio, mes);



        public Task<Margen_bruto> Margenbruto_lineal(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino) => _Service.Margenbruto_lineal1(idusuario, companiaid, categoriacompaniaid, tableroid, anio, mes, monedadestino);
        public Task<Margen_bruto> Margenbruto_region(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino) => _Service.Margenbruto_region1(idusuario, companiaid, categoriacompaniaid, tableroid, anio, mes, monedadestino);
        public Task<Margen_bruto> Margenbruto_top5(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino) => _Service.Margenbruto_top51(idusuario, companiaid, categoriacompaniaid, tableroid, anio, mes, monedadestino);
        public Task<composicion_de_margenes> Composicion_margenes(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes) => _Service.Composicion_margenes(idusuario, companiaid, categoriacompaniaid, tableroid, anio, mes);
        public Task<portaforlio> Contribucion_del_portafolio(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes) => _Service.Contribucion_del_portafolio(idusuario, companiaid, categoriacompaniaid, tableroid, anio, mes);

        ////Gastos
        //public Task<Gastos_tablero> Gastos(int idusuario, int anio, string mes, int compania, int monedadestino) => _Service.Gastos(idusuario, anio, mes, compania, monedadestino);

    }
}
