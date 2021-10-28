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
    public class DBEmpresaBC
    {
        public List<DBEmpresa> CargarBE(DataRow[] dr)
        {
            List<DBEmpresa> lst = new List<DBEmpresa>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public DBEmpresa CargarBE(DataRow dr)
        {
            DBEmpresa obj = new DBEmpresa();

            obj.idDB = Convert.ToDecimal(dr["IdDB"].ToString());
            obj.UrlBase = dr["UrlBase"].ToString();
            obj.NombreDB = dr["NombreDB"].ToString();
            obj.Usuario = dr["Usuario"].ToString();
            obj.Password = dr["Password"].ToString();
            obj.Version = dr["Version"].ToString();
            obj.IdEmpresa = Convert.ToDecimal(dr["IdEmpresa"].ToString());

            return obj;
        }



        public DBEmpresa listadebasexEmpresa11(decimal idrol)
        {
            DBEmpresa obj = new DBEmpresa();
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select top 1 db.* from RolUsuario rr inner join DBEmpresa db on db.IdEmpresa= rr.IDEmpresa where idRol={0}", Convert.ToInt32(idrol));
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


        public DBEmpresa listadebasexEmpresa111(decimal idempresa)
        {
            DBEmpresa obj = new DBEmpresa();
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select top 1 db.* from DBEmpresa db where db.IdEmpresa={0}", Convert.ToInt32(idempresa));
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
