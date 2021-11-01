using IndicadoresCore.Models;
using IndicadoresCore.Models.Gastos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.Services
{
    public interface IService
    {

        IQueryable<Usuario> GetAll_user();
        Task<Usuario> ValidarLoginWeb1(string nombre, string password);
        Task<Employee> Creacionn(Employee employee);
        Task<Usuario> UsuarioCA(Usuario usuario);
        Task<List<CategoriaCompania>> Lista_Menu(int idusuario, int companiaid);
        Task<Detalle_receptor> DetalleCifrasNotables(int idusuario, int companiaid, int categoriacompaniaid, int tableroid,  int anio, string mes,  int monedadestino);
        Task<Performance> Performancelineal(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino);
        Task<Performance> Performancetop5(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino);
        Task<PerformanceRegion> Performanceregion(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino);
        Task<composicion_de_ventas> Composicion_ventas(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes);


        Task<Margen_bruto> Margenbruto_lineal1(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino);
        Task<Margen_bruto> Margenbruto_region1(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino);
        Task<Margen_bruto> Margenbruto_top51(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino);
        Task<composicion_de_margenes> Composicion_margenes(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes);
        Task<portaforlio> Contribucion_del_portafolio(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes);
        Task<Data_graficaICP> EvolucionCxCGrafica(int idusuario, int companiaid, int categoriacompaniaid, int tableroid, int anio, string mes, int monedadestino);

        ////gastos
        //Task<Gastos_tablero> Gastos(int idusuario, int anio, string mes, int compania, int monedadestino);

    }
}
