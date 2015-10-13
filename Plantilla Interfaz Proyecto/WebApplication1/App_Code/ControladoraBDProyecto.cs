using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebApplication1.App_Code
{
    public class ControladoraBDProyecto
    {

        private AccesoBaseDatos baseDatos;
        String conexion = "Server=eccibdisw; Initial Catalog= g4inge; Integrated Security=SSPI";        
        
        public ControladoraBDProyecto()
        {
            baseDatos = new AccesoBaseDatos();
        }

        public string insertarProyecto(EntidadProyecto proyecto)
        {
            string resultado = "Exito";
            int idP = 0;
            int idOf = 0;
            string consulta = "";
            SqlConnection sqlConnection = new SqlConnection(conexion);
            sqlConnection.Open();

            try
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO Proyecto(nombre,objetivo,fechaAsignacion,estado) VALUES (@nombre, @objetivo, @fechaAsignacion, @estado)", sqlConnection);
                cmd.Parameters.AddWithValue("@nombre", proyecto.getNombre());
                cmd.Parameters.AddWithValue("@objetivo", proyecto.getObjetivo());
                cmd.Parameters.AddWithValue("@fechaAsignacion", proyecto.getFecha());
                cmd.Parameters.AddWithValue("@estado", proyecto.getEstado());
                cmd.ExecuteNonQuery();

                consulta = "SELECT id FROM Proyecto WHERE nombre = '" + proyecto.getNombre() + "';";
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                while (reader.Read())
                {
                    idP = Convert.ToInt32((reader["id"].ToString()));
                }
            }
            catch (Exception e)
            {
                e.ToString();
                resultado = "Error al insertar, Error: " + e;
            }

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO OficinaUsuaria(representante,nombre,correo,idProyecto) VALUES (@representante, @nombre, @correo, @idProyecto)", sqlConnection);
                cmd.Parameters.AddWithValue("@representante", proyecto.getRep());
                cmd.Parameters.AddWithValue("@nombre", proyecto.getNomOf());
                cmd.Parameters.AddWithValue("@correo", proyecto.getCorreoOf());
                cmd.Parameters.AddWithValue("@idProyecto", idP);
                cmd.ExecuteNonQuery();
                consulta = "Select id from OficinaUsuaria where idProyecto = '" + idP + "'";
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                while (reader.Read())
                {
                    idOf = Convert.ToInt32((reader["id"].ToString()));
                }
            }
            catch (Exception e)
            {
                e.ToString();
                resultado = "Error al insertar, Error: " + e;
            }


            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO TelefonoOficina(numero,idCliente) VALUES (@numero, @idCliente)", sqlConnection);
                cmd.Parameters.AddWithValue("@numero", proyecto.getTelOf());
                cmd.Parameters.AddWithValue("@idCliente", idOf);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                e.ToString();
                resultado = "Error al insertar, Error: " + e;
            }

            return resultado;
        }
        public string modificarProyecto()
        {
            string resultado = "";

            return resultado;
        }
        public string eliminarProyecto()
        {
            string resultado = "";
            return resultado;
        }
        public string consultarProyecto()
        {
            string resultado = "";
            return resultado;
        }
        public string consultar_total_Proyecto()
        {
            string resultado = "";
            return resultado;
        }

    }
}