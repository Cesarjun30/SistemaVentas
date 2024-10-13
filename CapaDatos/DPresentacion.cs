using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DPresentacion
    {
        private int _Idpresentacion;
        private string _Nombre, _Descripcion, _TextoBuscar;

        public int Idpresentacion { get => _Idpresentacion; set => _Idpresentacion = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public string TextoBuscar { get => _TextoBuscar; set => _TextoBuscar = value; }


        public DPresentacion()
        {


        }

        public DPresentacion(int idpresentacion, string nombre, string descripcion, string textobuscar) { 
                
            this.Idpresentacion = idpresentacion;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.TextoBuscar = textobuscar;

        }




        // Metodo Insertar

        public string Insertar(DPresentacion Presentacion)
        {

            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {

                // Codigo para insertar
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                // Establecer el Comando para ejecutar sentencias en SQL server

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinsertar_presentacion";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                /* Parametros para iDPresentacion */

                SqlParameter ParIdpresentacion = new SqlParameter();
                ParIdpresentacion.ParameterName = "@idpresentacion";
                ParIdpresentacion.SqlDbType = SqlDbType.Int;
                ParIdpresentacion.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdpresentacion);

                /* Parametros para Nombre */

                SqlParameter ParNombre = new SqlParameter();
                ParNombre.ParameterName = "@nombre";
                ParNombre.SqlDbType = SqlDbType.VarChar;
                ParNombre.Size = 50;
                ParNombre.Value = Presentacion.Nombre;
                SqlCmd.Parameters.Add(ParNombre);

                /* Parametros para Nombre */

                SqlParameter ParDescripcion = new SqlParameter();
                ParDescripcion.ParameterName = "@descripcion";
                ParDescripcion.SqlDbType = SqlDbType.VarChar;
                ParDescripcion.Size = 256;
                ParDescripcion.Value = Presentacion.Descripcion;
                SqlCmd.Parameters.Add(ParDescripcion);

                // Ejecutamos Comando 

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se 'Ingreso'  el resgistro ";


            }

            catch (Exception ex)
            {

                rpta = ex.Message;
            }
            finally
            {

                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;

        }

        // Metodo Editar

        public string Editar(DPresentacion Presentacion)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {

                // Codigo para Editar
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                // Establecer el Comando para ejecutar sentencias en SQL server

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speditar_presentacion";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                /* Parametros para idpresentacion */

                SqlParameter ParIdpresentacion = new SqlParameter();
                ParIdpresentacion.ParameterName = "@idpresentacion";
                ParIdpresentacion.SqlDbType = SqlDbType.Int;
                ParIdpresentacion.Value = Presentacion.Idpresentacion;
                SqlCmd.Parameters.Add(ParIdpresentacion);

                /* Parametros para Nombre */

                SqlParameter ParNombre = new SqlParameter();
                ParNombre.ParameterName = "@nombre";
                ParNombre.SqlDbType = SqlDbType.VarChar;
                ParNombre.Size = 50;
                ParNombre.Value = Presentacion.Nombre;
                SqlCmd.Parameters.Add(ParNombre);

                /* Parametros para Descripcion */

                SqlParameter ParDescripcion = new SqlParameter();
                ParDescripcion.ParameterName = "@descripcion";
                ParDescripcion.SqlDbType = SqlDbType.VarChar;
                ParDescripcion.Size = 256;
                ParDescripcion.Value = Presentacion.Descripcion;
                SqlCmd.Parameters.Add(ParDescripcion);

                // Ejecutamos Comando 

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se 'Actualizo' el resgistro ";


            }

            catch (Exception ex)
            {

                rpta = ex.Message;
            }
            finally
            {

                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;

        }

        // Metodo Eliminar

        public string Eliminar(DPresentacion Presentacion)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {

                // Codigo para Eliminar
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                // Establecer el Comando para ejecutar sentencias en SQL server

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speliminar_presentacion";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                /* Parametros para idpresentacion */

                SqlParameter ParIdpresentacion = new SqlParameter();
                ParIdpresentacion.ParameterName = "@idpresentacion";
                ParIdpresentacion.SqlDbType = SqlDbType.Int;
                ParIdpresentacion.Value = Presentacion.Idpresentacion;
                SqlCmd.Parameters.Add(ParIdpresentacion);



                // Ejecutamos Comando 

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se 'Elimino' el resgistro ";


            }

            catch (Exception ex)
            {

                rpta = ex.Message;
            }
            finally
            {

                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;

        }


        // Metodo Mostrar

        public DataTable Mostrar()
        {

            DataTable DtResultado = new DataTable("presentacion");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_presentacion";
                SqlCmd.CommandType = CommandType.StoredProcedure;


                /*Comando para llenar el DataTable*/

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;

        }




        // Metodo BuscarNombre 

        public DataTable BuscarNombre(DPresentacion Presentacion)
        {

            DataTable DtResultado = new DataTable("presentacion");
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_presentacion_nombre";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = Presentacion.TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                /*Comando para llenar el DataTable*/

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex)
            {

                DtResultado = null;
            }

            return DtResultado;

        }




        /*Fin*/
    }


}
