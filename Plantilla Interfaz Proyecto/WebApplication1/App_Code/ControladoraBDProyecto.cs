using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;


namespace WebApplication1.App_Code
{
    public class ControladoraBDProyecto
    {

        
        private AccesoBaseDatos baseDatos;
        String conexion = "Server=eccibdisw; Initial Catalog= g4inge; Integrated Security=SSPI";        

        /* Descripcion: Constructor de la ControladoraBDProyecto
       * 
       * REQ: N/A
       * 
       * RET: N/A
       */

        public ControladoraBDProyecto()
        {
            baseDatos = new AccesoBaseDatos();
        }

        /* Descripcion: Inserta el proyecto en la base de datos junto con la oficina y sus telefonos
       * 
       * REQ: EntidadProyecto
       * 
       * RET: string
       */

        public string insertarProyecto(EntidadProyecto proyecto)
        {
            string resultado = "Exito";
            int idP = 0;
            int idOf = 0;
            string consulta = "";
            SqlConnection sqlConnection = new SqlConnection(conexion);

            try
            {

                sqlConnection.Open();
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
                reader.Close();
            }
            
            finally
            {
                sqlConnection.Close();
            }

            try
            {
                sqlConnection.Open();
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
                reader.Close();
            }

            finally
            {
                sqlConnection.Close();
            }


            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO TelefonoOficina(numero,idCliente) VALUES (@numero, @idCliente)", sqlConnection);
                cmd.Parameters.AddWithValue("@numero", proyecto.getTelOf());
                cmd.Parameters.AddWithValue("@idCliente", idOf);
                cmd.ExecuteNonQuery();
            }

            finally
            {
                sqlConnection.Close();
            }


            try
            {
                consulta = "Update Usuario Set idProy = '" + idP + "' where cedula = '" + proyecto.getLider() + "'";
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                reader.Close();
            }

            catch (Exception e)
            {
                e.ToString();
                resultado = "Error al insertar, Error: " + e;
            }

            return resultado;
        }

        /* Descripcion: Elimina el Proyecto y toda informacion asociada a ese Proyecto
       * 
       * REQ: string
       * 
       * RET: string
       */

        public string eliminarProyecto(string nomP) 
        {
            string resultado = "Exito";
            int idP = 0;
            string consulta = "Select id from Proyecto where nombre = '" + nomP + "'";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                while (reader.Read())
                {
                    idP = Convert.ToInt32((reader["id"].ToString()));
                }
                reader.Close();
                consulta = "Delete from Proyecto where id = '" + idP + "'";
                reader = baseDatos.ejecutarConsulta(consulta);
                reader.Close();
            }

            catch (Exception e)
            {
                throw e;
            }
            return resultado;
        }

        /* Descripcion: Devuelve todos los usuarios que sean lideres
       * 
       * REQ: N/A
       * 
       * RET: List<string>
       */

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
                reader.Close();
            }
            catch (SqlException e)
            {
                throw e;
            }

            return lista;
        }

        /* Descripcion: Revisa informacion repetida en la base de datos
       * 
       * REQ: string, string
       * 
       * RET: string
       */

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
                    reader.Close();
                }
                catch (SqlException e)
                {
                    throw e;
                }
                consulta = "Select nombre from OficinaUsuaria where nombre = '" + nomOf + "'";
            }

            return resultado;
        }

        /* Descripcion: Insertar el segundo Telefono a la oficina del Proyecto
       * 
       * REQ: string, string
       * 
       * RET: N/A
       */

        public void insertarTel2(string tel2, string of)
        {
            string consulta = "Select id from OficinaUsuaria where nombre = '" + of + "'";
            int idOf = -1;
            SqlConnection sqlConnection = new SqlConnection(conexion);
            try
            {
                
                sqlConnection.Open();
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                while (reader.Read())
                {
                    idOf = Convert.ToInt32((reader["id"].ToString()));
                }
                reader.Close();
                SqlCommand cmd = new SqlCommand("INSERT INTO TelefonoOficina(numero,idCliente) VALUES (@numero, @idCliente)", sqlConnection);
                cmd.Parameters.AddWithValue("@numero", tel2);
                cmd.Parameters.AddWithValue("@idCliente", idOf);
                cmd.ExecuteNonQuery();
            }
            
            finally
            {
                sqlConnection.Close();
            }
        }

        /* Descripcion: Consulta total de un proyecto por filtro 
       * 
       * REQ: string 
       * 
       * RET: DataTable
       */

        public DataTable consultar_Total_ProyectoFiltro(string nombreFiltro)
        {
            //realiza consulta de proyectos por filtro del nombre
            string consulta = "";
            DataTable data = new DataTable();
            //--cambio en a consulta--
            consulta = "SELECT  nombre as 'Nombre', objetivo as 'Objetivo', estado as 'Estado'  FROM Proyecto where nombre like '" + nombreFiltro + "%';";
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

        /* Descripcion: Consulta Total de los Proyectos
       * 
       * REQ: N/A
       * 
       * RET: DataTable
       */

        public DataTable consultar_Total_Proyecto()
        {
            string consulta = "";

            DataTable data = new DataTable();
            //--cambio en a consulta--
            consulta = "SELECT  nombre as 'Nombre', objetivo as 'Objetivo', estado as 'Estado'  FROM Proyecto;";
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

        /* Descripcion: Consultar un proyecto por su nombre
       * 
       * REQ: string
       * 
       * RET: EntidadProyecto
       */

        public EntidadProyecto consultar_Proyecto(string nombreP)
        {
            string consulta = "";

            Object[] datos = new Object[11];

            EntidadProyecto objPro = null;
            //SqlConnection sqlConnection = new SqlConnection(conexion);
            
            try
            {
                //sqlConnection.Open();
                //--cambio en a consulta--
                consulta = "select p.objetivo,p.estado, p.fechaAsignacion, o.nombre, o.representante, o.correo, u.cedula, CONCAT(u.pNombre,' ',u.pApellido,' ',u.sApellido) , tel.numero from Proyecto p, OficinaUsuaria o, TelefonoOficina tel, Usuario u where p.nombre = '" + nombreP + "' and p.id = o.idProyecto and tel.idCliente = o.id and u.idProy=p.id and u.rol = 'Lider'  ;";

                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                try
                {

                    if (reader.Read())
                    {
                        //cuando hay solo un tekefono
                        datos[0] = "";                  //nombre
                        datos[1] = reader.GetString(0); //objetivo
                        datos[2] = reader.GetString(1); //estado
                        datos[3] = reader.GetDateTime(2);  //fechaAsgnacion
                        datos[4] = reader.GetString(3); //nombreOficina
                        datos[5] = reader.GetString(4); // representante
                        datos[6] = reader.GetString(5); //cooreoOficina                   

                        datos[8] = reader.GetInt32(6);//cedula lider                      
                        datos[9] = reader.GetString(7);//nombreLider

                        datos[7] = reader.GetInt32(8);//Convert.ToInt32(reader.GetString(6)); //telOficina  
                        datos[10] = null;// reader.GetInt32(17);//tel2


                    }
                    if (reader.Read())
                    {
                        //cuando hay dos telefonos
                        datos[10] = reader.GetInt32(8);//tel2

                    }
                    objPro = new EntidadProyecto(datos);
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            
            catch (SqlException ex)
            {
                //throw ex;
                ex.ToString();
            }

            return objPro;
        }

        /* Descripcion: Devuelve el perfil de un usuario
       * 
       * REQ: string
       * 
       * RET: string
       */

        public string getPerfil(string usuario) 
        {
            string resultado = "";
            try
            {
                string consulta = "Select perfil from Usuario where nomUsuario = '" + usuario + "'";
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                while (reader.Read())
                {
                    resultado = reader.GetString(0);
                }
                reader.Close();
                
            }
            catch (Exception e)
            {
                e.ToString();
            }
                return resultado;
        }

        public List<EntidadRecursoH> getRecursosDisponibles()
        {
            SqlDataReader reader = null;
            List<EntidadRecursoH> recursos = new List<EntidadRecursoH>();

            using (SqlConnection sqlConnection = new SqlConnection(conexion))
        {

            string consulta = "SELECT cedula, pNombre, pApellido, sApellido, rol from Usuario WHERE not rol = 'Lider' AND not perfil = 'A' AND idProy IS NULL;";
                sqlConnection.Open();
                reader = baseDatos.ejecutarConsulta(consulta);



                try
                {
                    while (reader.Read())
                    {
                        int cedula = SafeGetInt32(reader, 0);
                        String nombre = SafeGetString(reader, 1);
                        String pApellido = SafeGetString(reader, 2);
                        String sApellido = SafeGetString(reader, 3);
                        String rol = SafeGetString(reader, 4);

                        EntidadRecursoH rh = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, rol);
                        recursos.Add(rh);
        }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

            }
            return recursos;
        }

        /* Descripcion: Devuelve los recursos disponibles
        * 
        * REQ: N/A
        * 
        * RET: SqlDataReader
        */

        public List<EntidadRecursoH> getRecursosAsignados(string nomP)
        {
            int idP = -1;
            SqlDataReader reader = null;
            string consulta = "SELECT id FROM Proyecto WHERE nombre = '" + nomP + "';";

            List<EntidadRecursoH> recursos = new List<EntidadRecursoH>();
            using (SqlConnection sqlConnection = new SqlConnection(conexion))
            {
                sqlConnection.Open();
                reader = baseDatos.ejecutarConsulta(consulta);
            while (reader.Read())
            {
                idP = Convert.ToInt32((reader["id"].ToString()));
            }

            consulta = "SELECT cedula, pNombre, pApellido, sApellido, rol from Usuario WHERE not rol = 'Lider' AND not perfil = 'A' And idProy = '" + idP + "';";
             
                reader = baseDatos.ejecutarConsulta(consulta);

                try
                {
                    while (reader.Read())
                    {
                        int cedula = SafeGetInt32(reader, 0);
                        String nombre = SafeGetString(reader, 1);
                        String pApellido = SafeGetString(reader, 2);
                        String sApellido = SafeGetString(reader, 3);
                        String rol = SafeGetString(reader, 4);

                        EntidadRecursoH rh = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, rol);
                        recursos.Add(rh);
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return recursos;
        }

        /* Descripcion: 
       * 
       * REQ: SqlDataReader, int
       * 
       * RET: static string
       */

        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        /* Descripcion: 
        * 
        * REQ: SqlDataReader,int
        * 
        * RET: static int
        */

        public static int SafeGetInt32(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            else
                return -1;
        }

        /* Descripcion: Asigna a un usuario el proyecto
       * 
       * REQ: string , string
       * 
       * RET: N/A
       */

        public void asignarProyectoAEmpleado(string cedula, string nombreProy)
        {
            try
            {
                string consulta = "SELECT id from Proyecto WHERE Nombre = '" + nombreProy + "'";
                var reader = baseDatos.ejecutarConsulta(consulta);
                reader.Read();
                int idProy = reader.GetInt32(0);
                reader.Close();

                consulta = "UPDATE usuario set idProy =" + idProy + "  WHERE cedula = " + cedula + ";";
                reader = baseDatos.ejecutarConsulta(consulta);
                reader.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        /* Descripcion: Cambia el estado del proyecto cuando un Miembro lo "elimina"
       * 
       * REQ: string
       * 
       * RET: N/A
       */

        public void cambiarEstado(string nombreP)
        {
            try
            {
                string consulta = "Update Proyecto Set estado ='Cerrado' where nombre = '" + nombreP + "'";
                var reader = baseDatos.ejecutarConsulta(consulta);
                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /* Descripcion: Consulta la tabla e informacion correspondiente a Miembro de Equipo
       * 
       * REQ: string
       * 
       * RET: EntidadProyecto
       */

        public EntidadProyecto consultarProyectoM(string nombreUsuario) 
        {

            string consulta;
            Object[] datos = new Object[11];

            EntidadProyecto objProy = null;
           // SqlConnection sqlConnection = new SqlConnection(conexion);
            
            try
            {
                //sqlConnection.Open();
                //--cambio en a consulta--
                consulta = "select p.nombre, p.objetivo,p.estado, p.fechaAsignacion ,o.nombre, o.representante, o.correo,l.cedula, CONCAT(l.pNombre,' ',l.pApellido,' ',l.sApellido), tel.numero from Proyecto p, Usuario u, Usuario l,OficinaUsuaria o, TelefonoOficina tel where u.nomUsuario='" + nombreUsuario + "' and l.idProy = p.id and l.idProy = u.idProy and l.idProy = o.idProyecto and tel.idCliente = o.id and l.rol = 'Lider'; ";

                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                try
                {
                    if (reader.Read())
                    {
                        //cuando hay solo un tekefono
                        datos[0] = reader.GetString(0);//nombre
                        datos[1] = reader.GetString(1); //objetivo
                        datos[2] = reader.GetString(2); //estado
                        datos[3] = reader.GetDateTime(3);  //fechaAsgnacion
                        datos[4] = reader.GetString(4); //nombreOficina
                        datos[5] = reader.GetString(5); // representante
                        datos[6] = reader.GetString(6); //cooreoOficina                   
                        datos[8] = reader.GetInt32(7);//cedula lider                      
                        datos[9] = reader.GetString(8);//nombreLider
                        datos[7] = reader.GetInt32(9);//telOficina  
                        datos[10] = null;//tel2

                    }
                    if (reader.Read())
                    {
                        //cuando hay dos telefonos
                        datos[10] = reader.GetInt32(9);//tel2

                    }
                    objProy = new EntidadProyecto(datos);
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    //throw ex;
                    ex.ToString();
                }
            }

            catch (SqlException ex)
            {
                //throw ex;
                ex.ToString();
            }

            return objProy;

        }
        public EntidadProyecto actualizaProyecto(Object[] nuevos, Object[] originales)
        {

            Object[] dato = new Object[11];
            EntidadProyecto nP = new EntidadProyecto(nuevos);
            EntidadProyecto en = null;

            string nombre = nP.getNombre();
            string objetivo = nP.getObjetivo();
            string estado = nP.getEstado();
            DateTime fecha = nP.getFecha();
            string nombreOf = nP.getNomOf();
            string representante = nP.getRep();
            string correoOf = nP.getCorreoOf();
            int telefonoOf = nP.getTelOf();
            int lider = nP.getLider();
            string nombreLider = nP.getNombreLider();
            int tel2 = nP.getTelOf2();

            string fec = fecha.ToString("yyy-MM-dd", CultureInfo.InvariantCulture);
            string nombreO = (String)originales[0];
            string lOrig = originales[1].ToString();


            int idP = traerId(nombreO);//obtiene el idProyecto          
                try
                {
                    string consulta = "Update Proyecto Set nombre ='" + nombre + "', objetivo ='" + objetivo + "',estado ='" + estado + "', fechaAsignacion= '" + fec + "'" +
                   " where nombre = '" + nombreO + "';";
                   //" where nombre = 'uuuuuuuuuuu';";
             
                    baseDatos.ejecutarConsulta(consulta);
                }
                catch(Exception e) {
                    throw e;
                }
                try
                {
                    string consulta2 = "Update OficinaUsuaria Set representante ='" + representante + "', nombre ='" + nombreOf + "',correo ='" + correoOf + "'" +
                    "where id= " + idP + "; ";
                    baseDatos.ejecutarConsulta(consulta2);
                }
                catch (Exception e)
                {
                    throw e;
                }
                try
                {
                    string consulta4 = "Update Usuario Set idProy =  null  where cedula = " + lOrig + ";";
                    baseDatos.ejecutarConsulta(consulta4);
                }
                catch (Exception e)
                {
                    throw e;
                }
                try
                {
                    string consulta3 = "Update Usuario Set idProy =" + idP +
                   "where cedula = " + lider + ";";
                    baseDatos.ejecutarConsulta(consulta3);
                }
                catch (Exception e)
                {
                    throw e;
                }
               
                /*string consulta3 = "Update TelefonoOficina Set numero ='" + telefonoOf + "'"+
                    "where idCliente= '" + idP + "'";
                baseDatos.ejecutarConsulta(consulta3);*/

                try
                {
                    string consulta5 = "select  p.objetivo,p.estado, p.fechaAsignacion, o.nombre, o.representante, o.correo, l.cedula, CONCAT(l.pNombre,' ',l.pApellido,' ',l.sApellido) , tel.numero from Proyecto p, OficinaUsuaria o, TelefonoOficina tel, Usuario l where p.nombre = '" + nombre + "' and p.id = o.idProyecto and tel.idCliente = o.id and l.idProy=p.id and l.rol='Lider';";
                    SqlDataReader reader = baseDatos.ejecutarConsulta(consulta5);
                    if (reader.Read())
                    {
                        //cuando hay solo un telefono
                        dato[0] = nombre;              //nombre
                        dato[1] = reader.GetString(0); //objetivo
                        dato[2] = reader.GetString(1); //estado
                        dato[3] = reader.GetDateTime(2);  //fechaAsgnacion
                        dato[4] = reader.GetString(3); //nombreOficina
                        dato[5] = reader.GetString(4); // representante
                        dato[6] = reader.GetString(5); //cooreoOficina                                       
                        dato[7] = reader.GetInt32(8); //telOficina 
                        dato[8] = reader.GetInt32(6);//cedula lider                      
                        dato[9] = reader.GetString(7);//nombreLider
                        dato[10] = null;//tel2
                    }
                    if (reader.Read())
                    {
                        //cuando hay dos telefonos
                        dato[10] = reader.GetInt32(8);//tel2
                    }
                en = new EntidadProyecto(dato);
                }
                catch (Exception ex)
                {
                    throw ex;
                }                  
            return en;
        }
        public int traerId(string nombreProy) {
            int idProy = 0;
            try
            {
                string consulta = "SELECT id from Proyecto WHERE Nombre = '" + nombreProy + "'";
                var reader = baseDatos.ejecutarConsulta(consulta);
                reader.Read();
                idProy = reader.GetInt32(0);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idProy;

        }

    }
}