using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace IndicadoresCore.Models
{
    public class ClaseConexion
    {
        public string con = "Server=wdb4.my-hosting-panel.com;Initial Catalog=bantican_Indicadores;Persist Security Info=False;User ID=bantican_user;Password=Bantic2021!*;";
        private System.Data.SqlClient.SqlConnection conx = new System.Data.SqlClient.SqlConnection();
        private System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        private System.Data.SqlClient.SqlDataReader lec;
        private System.Data.SqlClient.SqlDataAdapter dap = new System.Data.SqlClient.SqlDataAdapter();
        private DataSet ds = new DataSet();
        private System.Data.SqlClient.SqlTransaction trans;
        private string cadconx;
        private static DbProviderFactory factory = null;

        public ClaseConexion(string NombreCadena)
        {
            Configurar("cadenaCnx");
        }

        public ClaseConexion()
        {
            Configurar("Negocio");
        }

        private void Configurar(string cadenaCnx)
        {
            try
            {
                cadconx = con;
                factory = System.Data.SqlClient.SqlClientFactory.Instance;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void Conectar()
        {
            try
            {
                if (conx != null)
                {
                    this.conx = (System.Data.SqlClient.SqlConnection)factory.CreateConnection();

                    this.conx.ConnectionString = cadconx;
                }
                if (this.conx.State == ConnectionState.Closed || this.conx.State == ConnectionState.Broken)
                    this.conx.Open();
            }
            catch (Exception ex)
            {
                Desconectar();
                //Conectar();
                throw ex;
            }
        }


        public void Desconectar()
        {
            if (!this.conx.State.Equals(ConnectionState.Closed))
            {
                this.conx.Dispose();
                this.conx.Close();
                System.Data.SqlClient.SqlConnection.ClearAllPools();
            }
        }


        public void CrearComando(string sql, System.Data.CommandType tipoConsulta = CommandType.Text)
        {
            this.cmd = (System.Data.SqlClient.SqlCommand)factory.CreateCommand();
            this.cmd.Connection = this.conx;
            this.cmd.CommandType = tipoConsulta;
            this.cmd.CommandText = sql;
            if (this.trans != null)
                this.cmd.Transaction = this.trans;
        }

        public void AsignarParametro(string nombre, object valor, ParameterDirection direccion = ParameterDirection.Input, int size = 0)
        {
            if (valor == null)
                this.cmd.Parameters.AddWithValue(nombre, DBNull.Value);

            else
                switch (direccion)
                {
                    case ParameterDirection.InputOutput:
                    case ParameterDirection.Output:
                    case ParameterDirection.ReturnValue:
                        {
                            //SqlParameter ret = this.cmd.Parameters.AddWithValue(nombre, valor);
                            //ret.Direction = direccion;
                            //if (size > 0)
                            //ret.Size = size;
                            this.cmd.Parameters.AddWithValue(nombre, valor);
                            this.cmd.Parameters[nombre].Direction = direccion;
                            this.cmd.Parameters[nombre].Size = size;
                            break;
                        }

                    default:
                        {
                            this.cmd.Parameters.AddWithValue(nombre, valor);
                            break;
                        }
                }
        }

        public Object ObtenerParametro(string nombre)
        {
            Object valor = null;
            valor = this.cmd.Parameters[nombre].Value;
            return valor;
        }

        public void EjecutarComando()
        {
            this.cmd.CommandTimeout = 0;
            this.cmd.ExecuteNonQuery();
        }

        public void ComenzarTransaccion(IsolationLevel tipoTransaccion = IsolationLevel.Unspecified)
        {
            if (this.trans == null)
            {
                this.trans = this.conx.BeginTransaction(tipoTransaccion);
            }
        }
        public void CancelarTransaccion()
        {
            if (this.trans != null)
            {
                this.trans.Rollback();
            }
        }

        public void ConfirmarTransaccion()
        {
            if (this.trans != null)
            {
                this.trans.Commit();
            }
        }

        public DataRow ObtenerFila(string sql)
        {
            DataRow dr = null;
            try
            {
                if (this.trans == null)
                {
                    Conectar();
                }
                this.dap = new System.Data.SqlClient.SqlDataAdapter(sql, this.conx);
                ds = new DataSet();
                this.dap.Fill(ds);
                if (this.trans == null)
                {
                    Desconectar();
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                }
            }
            catch (Exception ex)
            {
                if (this.trans == null)
                {
                    Desconectar();
                }
                throw ex;
            }
            return dr;
        }

        public DataRow[] ObtenerFilas(string sql)
        {
            DataRow[] dr = null;
            try
            {
                if (this.trans == null)
                {
                    Conectar();
                }
                this.dap = new System.Data.SqlClient.SqlDataAdapter(sql, this.conx);
                ds = new DataSet();
                this.dap.Fill(ds);
                if (this.trans == null)
                {
                    Desconectar();
                }
                dr = ds.Tables[0].Select("");
            }
            catch (Exception ex)
            {
                if (this.trans == null)
                {
                    Desconectar();
                }
                throw ex;
            }
            return dr;
        }

        public DataTable ObtenerTabla(string sql)
        {
            DataTable dr = null;
            try
            {
                if (this.trans == null)
                {
                    Conectar();
                }
                this.dap = new System.Data.SqlClient.SqlDataAdapter(sql, this.conx);
                ds = new DataSet();
                this.dap.Fill(ds);
                if (this.trans == null)
                {
                    Desconectar();
                }
                dr = ds.Tables[0];
            }
            catch (Exception ex)
            {
                if (this.trans == null)
                {
                    Desconectar();
                }
                throw ex;
            }
            return dr;
        }

        public DataTable ObtenerTablaSP(string sql, Boolean bolCommand = true)
        {
            DataTable dr = null;
            try
            {
                if (this.trans == null)
                {
                    Conectar();
                }
                this.dap = new System.Data.SqlClient.SqlDataAdapter(sql, this.conx);
                if (bolCommand)
                {
                    this.dap.SelectCommand = this.cmd;
                }
                ds = new DataSet();
                this.dap.Fill(ds);
                if (this.trans == null)
                {
                    Desconectar();
                }
                dr = ds.Tables[0];
            }
            catch (Exception ex)
            {
                if (this.trans == null)
                {
                    Desconectar();
                }
                throw ex;
            }
            return dr;
        }

        public DataSet ObtenerDataSet(string sql, Boolean bolCommand = true)
        {
            try
            {
                if (this.trans == null)
                {
                    Conectar();
                }
                this.dap = new System.Data.SqlClient.SqlDataAdapter(sql, this.conx);
                if (bolCommand)
                {
                    this.dap.SelectCommand = this.cmd;
                }
                ds = new DataSet();
                this.dap.Fill(ds);
                if (this.trans == null)
                {
                    Desconectar();
                }
            }
            catch (Exception ex)
            {
                if (this.trans == null)
                {
                    Desconectar();
                }
                throw ex;
            }
            return ds;
        }

        public Object ObtenerValor(string sql)
        {
            Object dr = null;
            try
            {
                if (this.trans == null)
                {
                    Conectar();
                }
                this.CrearComando(sql);
                dr = this.cmd.ExecuteScalar();
                if (this.trans == null)
                {
                    Desconectar();
                }
            }
            catch (Exception ex)
            {
                if (this.trans == null)
                {
                    Desconectar();
                }
                throw ex;
            }
            return dr;
        }

        public object EjecutarScalar()
        {
            this.cmd.CommandTimeout = 0;
            return this.cmd.ExecuteScalar();
        }


    }
}
