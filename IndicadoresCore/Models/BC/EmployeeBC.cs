using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IndicadoresCore.Models;


namespace IndicadoresCore.Models.BC
{
    public class EmployeeBC
    {

        public EmployeeBC() : base()
        {
        }

        public EmployeeBC(string cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public Boolean Actualizar(ref Employee BEObj, Boolean isTransaccion = false)
        {
            string strSql = string.Empty;
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            bool bolOk = false;
            try
            {
                string TipoEstadoa = BEObj.TipoEstado.ToString();
                switch (BEObj.TipoEstado)
                {
                    case TipoEstado.Modificar:
                        strSql = @"update Employee set 
Name=@Name,
Designation=@Designation,
Estado=@Estado
where (Id=@Id)";
                        break;
                    case TipoEstado.Insertar:
                        strSql = @"insert into Employee values (@Id, @Name, @Designation,@Estado)";
                        break;

                }

                if (isTransaccion)
                    conx = dbConexion;
                else
                {
                    conx.Conectar();
                    conx.ComenzarTransaccion();
                }
                if (BEObj.TipoEstado != TipoEstado.SinAccion)
                {
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@Id", BEObj.Id);
                    conx.AsignarParametro("@Name", BEObj.Name);
                    conx.AsignarParametro("@Designation", BEObj.Designation);
                    conx.AsignarParametro("@Estado", BEObj.Estado);


                }
                conx.EjecutarComando();

                if (!isTransaccion)
                {
                    conx.ConfirmarTransaccion();
                    conx.Desconectar();
                }

                bolOk = true;
            }
            catch (Exception ex)
            {
                if (!isTransaccion)
                {
                    conx.CancelarTransaccion();
                    conx.Desconectar();
                }
                throw ex;
            }
            return bolOk;
        }






        public List<Employee> CargarBE(DataRow[] dr)
        {
            List<Employee> lst = new List<Employee>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public Employee CargarBE(DataRow dr)
        {
            Employee obj = new Employee();

            obj.Id = Convert.ToInt32(dr["Id"].ToString());
            obj.Name = dr["Name"].ToString();
            obj.Designation = dr["Designation"].ToString();
            obj.Estado = Convert.ToBoolean(dr["Estado"].ToString());

            return obj;
        }


        public async Task<Employee> buscarxid(int id)
        {
            Employee obj = new Employee();
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
               
                string sql = String.Format(@"select u.* from Employee u where u.Id={0}", Convert.ToInt32(id));
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

    }
}
