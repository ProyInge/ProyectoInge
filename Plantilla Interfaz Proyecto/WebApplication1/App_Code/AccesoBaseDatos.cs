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


namespace WebApplication1.App_Code
{
    public class AccesoBaseDatos
    {
        /*En Initial Catalog se agrega la base de datos propia. Intregated Security es para utilizar Windows Authentication*/
        String conexion = "Server=dave-pc\\eccibdisw; Initial Catalog= g4inge; Integrated Security=SSPI";
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
            conSQL.Close();
        }

        public string Conexion
        {
            get{ return conexion; }
            set{ conexion = value; }
        }

        /**
         * Permite ejecutar una consulta SQL, los datos son devueltos en un SqlDataReader
         */
        public SqlDataReader ejecutarConsulta(String consulta)
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
         * Permite ejecutar una consulta SQL, los datos son devueltos en un DataTable
         */
        public DataTable ejecutarConsultaTabla(String consulta)
        {
            DataTable table = new DataTable();
            try { 
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