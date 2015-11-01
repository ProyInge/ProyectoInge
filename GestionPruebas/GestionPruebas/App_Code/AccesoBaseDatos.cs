using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

// Namespace de acceso a base de datos
using System.Data;
using System.Data.SqlClient;

// Namespace para mostrar mensajes
using System.Web;


namespace GestionPruebas.App_Code
{
    public class AccesoBaseDatos
    {
        /*En Initial Catalog se agrega la base de datos propia. Intregated Security es para utilizar Windows Authentication*/
        //string conexion = "Server=JEFFRY; Initial Catalog= g4inge; Integrated Security=SSPI";
        //string conexion = "Server=DANIEL\\LOCAL; Initial Catalog= g4inge; Integrated Security=SSPI";
        //string conexion = "Server=DESKTOP-FRM9QAR\\SQLEXPRESS; Initial Catalog= g4inge; Integrated Security=SSPI";
        //string conexion = "Server=gabopc\\eccibdisw; Initial Catalog= g4inge; Integrated Security=SSPI";
        //string conexion = "Server=eccibdisw; Initial Catalog= g4inge; Integrated Security=SSPI";
        string conexion = "Server=dave-pc\\eccibdisw; Initial Catalog= g4inge; Integrated Security=SSPI";

        SqlConnection conSQL;

        /**
         * Constructor
         */
        public AccesoBaseDatos()
        {
            conSQL = new SqlConnection(Conexion);
            try
            {
                conSQL.Open();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /**
         * Destructor
         */
        ~AccesoBaseDatos()
        {
            try
            {
                conSQL.Close();
            }
            catch
            {

            }
        }

        public string Conexion
        {
            get { return conexion; }
            set { conexion = value; }
        }

        /**
         * Permite ejecutar una consulta SQL, los datos son devueltos en un SqlDataReader
         */
        public SqlDataReader ejecutarConsulta(string consulta)
        {

            SqlDataReader datos = null;
            SqlCommand comando = null;

            try
            {
                comando = new SqlCommand(consulta, conSQL);
                datos = comando.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return datos;
        }

        /**
         * Permite ejecutar una consulta SQL con parámetros, los datos son devueltos en un SqlDataReader
         */
        public SqlDataReader ejecutarConsulta(string consulta, Object[] args)
        {

            SqlDataReader datos = null;
            SqlCommand comando = null;

            try
            {

                comando = new SqlCommand(consulta, conSQL);
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].GetType() == typeof(string))
                    {
                        string arg = (string)args[i];
                        SqlParameter param = comando.Parameters.Add("@" + i, System.Data.SqlDbType.VarChar);
                        param.Value = arg;
                    }
                    else if (args[i].GetType() == typeof(DateTime))
                    {
                        DateTime arg = (DateTime)args[i];
                        if (arg.TimeOfDay != TimeSpan.Zero)
                        {
                            SqlParameter param = comando.Parameters.Add("@" + i, System.Data.SqlDbType.DateTime);
                            param.Value = arg;
                        } else
                        {
                            SqlParameter param = comando.Parameters.Add("@" + i, System.Data.SqlDbType.Date);
                            param.Value = arg;
                        }
                        
                    }
                    else if (args[i].GetType() == typeof(int))
                    {
                        int arg = (int)args[i];
                        SqlParameter param = comando.Parameters.Add("@" + i, System.Data.SqlDbType.Int);
                        param.Value = arg;
                    }
                    else if (args[i].GetType() == typeof(char))
                    {
                        char arg = (char)args[i];
                        SqlParameter param = comando.Parameters.Add("@" + i, System.Data.SqlDbType.Char);
                        param.Value = arg;
                    }
                }
                datos = comando.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }


            return datos;
        }

        /**
         * Permite ejecutar una consulta SQL, los datos son devueltos en un DataTable
         */
        public DataTable ejecutarConsultaTabla(String consulta)
        {
            DataTable table = new DataTable();
            try
            {
                SqlCommand comando = new SqlCommand(consulta, conSQL);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
                dataAdapter.Fill(table);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return table;

        }
    }
}