using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1.App_Code
{
    public class ControladoraBDProyecto
    {

        private AccesoBaseDatos baseDatos;
        String conexion = "Server=eccibdisw; Initial Catalog= g4inge; Integrated Security=SSPI";        
        //String conexion = "Server=DESKTOP-FRM9QAR\\SQLEXPRESS; Initial Catalog= eccibdisw; Integrated Security=SSPI";

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


            try
            {
                consulta = "Update Usuario Set idProy = '" + idP + "' where cedula = '" + proyecto.getLider() + "'";
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
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

        public string eliminarProyecto(string nomP) 
        {
            string resultado = "Exito";
            int idP = 0;
            string consulta = "Select id from Proyecto where nombre = '" + nomP + "'";
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            while (reader.Read())
            {
                idP = Convert.ToInt32((reader["id"].ToString()));
            }
            consulta = "Delete from Proyecto where id = '" + idP + "'";
            reader = baseDatos.ejecutarConsulta(consulta);
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

        public List<string> traerLideres()
        {
            string consulta = "SELECT CONCAT(cedula, ' ' ,pNombre, ' ', pApellido, ' ', sApellido) FROM Usuario WHERE rol = 'Lider' And idProy IS NULL";
            List<string> lista = new List<string>();
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        lista.Add(reader.GetString(0));
                        i++;
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return lista;
        }

        public int revisarExistentes(string nomP, string nomOf)
        {
            int resultado = 0;

            string consulta = "Select nombre from Proyecto where nombre = '" + nomP + "'";
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (i == 0)
                        {
                            resultado = 1;
                        }
                        if (resultado == 0 && i == 1)
                        {
                            resultado = 2;
                        }
                        if (resultado == 1 && i == 1)
                        {
                            resultado = 3;
                        }
                    }
                }
                catch (SqlException e)
                {
                    throw e;
                }
                consulta = "Select nombre from OficinaUsuaria where nombre = '" + nomOf + "'";
            }

            return resultado;
        }

        public void insertarTel2(string tel2, string of)
        {
            string consulta = "Select id from OficinaUsuaria where nombre = '" + of + "'";
            int idOf = -1;

            try
            {
                SqlConnection sqlConnection = new SqlConnection(conexion);
                sqlConnection.Open();
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                while (reader.Read())
                {
                    idOf = Convert.ToInt32((reader["id"].ToString()));
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO TelefonoOficina(numero,idCliente) VALUES (@numero, @idCliente)", sqlConnection);
                cmd.Parameters.AddWithValue("@numero", tel2);
                cmd.Parameters.AddWithValue("@idCliente", idOf);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable consultar_Total_ProyectoFiltro(string nombreFiltro)
        {
            //realiza consulta de proyectos por filtro del nombre

            string consulta = "";

            DataTable data = new DataTable();

            consulta = "SELECT  nombre as 'Nombre', objetivo as 'Objetivo', estado as 'Estado'  FROM Proyecto where nombre like '" + nombreFiltro + "%';";
            //SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            try
            {
                data = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return data;
        }
        public DataTable consultar_Total_Proyecto()
        {
            //string resultado = "Exito";
            string consulta = "";

            //SqlConnection sqlConnection = new SqlConnection(conexion);
            //sqlConnection.Open();
            //List<EntidadProyecto> listaProy = new List<EntidadProyecto>();
            DataTable data = new DataTable();

            consulta = "SELECT  nombre as 'Nombre', objetivo as 'Objetivo', estado as 'Estado'  FROM Proyecto;";
            //SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            try
            {
                data = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return data;
        }
        public EntidadProyecto consultar_Proyecto(string nombreP)
        {
            string resultado = "Exito";
            string consulta = "";

            Object[] datos = new Object[11];

            string res = "";

            EntidadProyecto objPro = null;
            SqlConnection sqlConnection = new SqlConnection(conexion);
            sqlConnection.Open();
            try
            {
                //consulta = "SELECT  objetivo   FROM Proyecto  WHERE nombre= "+ "'"+nombreP+ "';";
                consulta = "select p.objetivo,p.estado, p.fechaAsignacion, o.nombre, o.representante, o.correo, tel.numero, u.cedula, u.pNombre from Proyecto p, OficinaUsuaria o, TelefonoOficina tel, Usuario u where p.nombre = '"+ nombreP + "' and p.id = o.idProyecto and tel.idCliente = o.id and u.idProy=p.id;";

                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                try
                {
                    while (reader.Read())
                    {
                        datos[0] = "";                  //nombre
                        datos[1] = reader.GetString(0); //objetivo
                        datos[2] = reader.GetString(1); //estado
                        datos[3] =  reader.GetDateTime(2);  //fechaAsgnacion
                        datos[4] = reader.GetString(3); //nombreOficina
                        datos[5] = reader.GetString(4); // representante
                        datos[6] = reader.GetString(5); //cooreoOficina
                        datos[7] = reader.GetInt32(6);//Convert.ToInt32(reader.GetString(6)); //telOficina                      
                        datos[8] = reader.GetInt32(7);//cedula lider                      
                        datos[9] = reader.GetString(8);//nombreLider
                        datos[10] = 0;//reader.GetInt32(9);//tel2

                       objPro = new EntidadProyecto(datos);
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                e.ToString();
                resultado = "Error al consultar, Error: " + e;
                throw e;
            }

            return objPro;
        }

        public string getPerfil(string usuario) 
        {
            string resultado = "";

            string consulta = "Select perfil from Usuario where nomUsuario = '" + usuario + "'";
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            while (reader.Read())
            {
                resultado = reader.GetString(0);
            }
            return resultado;
        }

    }
}