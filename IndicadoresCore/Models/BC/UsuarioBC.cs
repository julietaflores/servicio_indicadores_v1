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
    public class UsuarioBC
    {

        public UsuarioBC() : base()
        {
        }

        public UsuarioBC(string cadConx)
        {

        }

  

        public List<Usuario> CargarBE(DataRow[] dr)
        {
            List<Usuario> lst = new List<Usuario>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public Usuario CargarBE(DataRow dr)
        {
            Usuario obj = new Usuario();

            obj.idUsuario = Convert.ToDecimal(dr["idUsuario"].ToString());
            obj.nombreUsuario = dr["nombreUsuario"].ToString();
            obj.usuario = dr["usuario"].ToString();
            obj.passwordd = dr["passwordd"].ToString();
            obj.fechacreacionusuario = dr.Field<DateTime?>("fechaCreacionUsuario");
            obj.CodIdioma = Convert.ToDecimal(dr["CODIdioma"].ToString());
            obj.Estado = Convert.ToBoolean(dr["Estado"].ToString());
            obj.IdEmpresa = Convert.ToDecimal(dr["IdEmpresa"].ToString());
            return obj;
        }


        public ClaseConexion dbConexion { get; set; }




        public Boolean Actualizar(ref Usuario BEObj, Boolean isTransaccion = false)
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
                        strSql = @"update Usuario set 
nombreUsuario=@nombreUsuario,
usuario=@usuario,
passwordd= @passwordd,
fechaCreacionUsuario= @fechaCreacionUsuario,
CODIdioma=@CODIdioma,
Estado=@Estado
where (idUsuario=@idUsuario)";
                        break;
                    case TipoEstado.Insertar:
                        strSql = @"insert into Usuario values (@nombreUsuario, @usuario, @passwordd,@fechaCreacionUsuario,@CODIdioma,@Estado)";
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
                    conx.AsignarParametro("@nombreUsuario", BEObj.nombreUsuario);
                    conx.AsignarParametro("@usuario", BEObj.usuario);
                    conx.AsignarParametro("@fechaCreacionUsuario", BEObj.fechacreacionusuario);
                    conx.AsignarParametro("@CODIdioma", BEObj.CodIdioma);
                    conx.AsignarParametro("@Estado", BEObj.Estado);
                    conx.AsignarParametro("@passwordd", BEObj.passwordd);


                    if (BEObj.TipoEstado == TipoEstado.Modificar)
                    {
                    
                        conx.AsignarParametro("@idUsuario", BEObj.idUsuario);
                    }



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



        public async Task<Usuario> buscarxid(decimal usuarioid)
        {
            Usuario obj = new Usuario();
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
                string sql = String.Format(@"select u.* from Usuario u where u.idUsuario={0} and  u.Estado=1", Convert.ToInt32(usuarioid));
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



        public async Task<Usuario> verdatosusuario11(string UserName, string Password)
        {
            Usuario obj = new Usuario();
            ClaseConexion conx = new ClaseConexion("cadenaCnx");
            try
            {
              
                string sql = String.Format(@"select u.* from Usuario u where u.usuario= '{0}' and u.passwordd='{1}' and u.Estado=1", UserName, Password);
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
