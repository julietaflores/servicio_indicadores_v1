using IndicadoresCore.Models;
using IndicadoresCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndicadoresCore.GraphQL
{
    public class Mutuation
    {
        private readonly IService _Service;
        public Mutuation(IService Service)
        {
            _Service = Service;
        }
        public async Task<Employee> Creacionn(Employee employee) => await _Service.Creacionn(employee);

        public async Task<Usuario> UsuarioCA(Usuario usuario) => await _Service.UsuarioCA(usuario);

    }
}
