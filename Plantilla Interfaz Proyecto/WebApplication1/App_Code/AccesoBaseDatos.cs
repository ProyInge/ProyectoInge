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
        String conexion = "Server=eccibdisw; Initial Catalog=g4inge; Integrated Security=SSPI";

        /**
         * Constructor
         */
        public AccesoBaseDatos()
        {
        }

        /**
         * Permite ejecutar una consulta SQL, los datos son devueltos en un SqlDataReader
         */
        public SqlDataReader ejecutarConsulta(String consulta)
        {
            SqlConnection sqlConnection = new SqlConnection(conexion);
            try
            {
                sqlConnection.Open();
            } catch (SqlException ex)
            {
                throw ex;
            }
            

            SqlDataReader datos = null;
            SqlCommand comando = null;

            try
            {
                comando = new SqlCommand(consulta, sqlConnection);
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
            SqlConnection sqlConnection = new SqlConnection(conexion);
            sqlConnection.Open();

            SqlCommand comando = new SqlCommand(consulta, sqlConnection);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();

            dataAdapter.Fill(table);

            return table;
        }
    }
}