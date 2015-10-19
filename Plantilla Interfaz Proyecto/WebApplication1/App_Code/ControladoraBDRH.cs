using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.App_Code
{
    public class ControladoraBDRH
    {
        private AccesoBaseDatos baseDatos;

        public ControladoraBDRH()
        {
            baseDatos = new AccesoBaseDatos();
        }

        public int usuarioValido(string nombreUsuario, string contra)
        {
            int res = -1;
            string consulta = "EXEC iniciarSesion @nombre='" + nombreUsuario.Trim() + "', @contra='" + contra.Trim() + "';";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.HasRows)
                {
                    reader.Read();
                    res = reader.GetInt32(0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return res;
        }

        public string getNombreCompleto(string nombreUsuario)
        {
            string consulta = "SELECT CONCAT(pNombre, ' ', pApellido, ' ', sApellido) FROM Usuario WHERE nomUsuario = '" + nombreUsuario.Trim() + "';";
            string nombre = "";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.HasRows)
                {
                    reader.Read();
                    nombre = reader.GetString(0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return nombre;
        }

        public void cerrarSesion(string nombreUsuario)
        {
            string consulta = "EXEC cerrarSesion @nombre = '" + nombreUsuario + "';";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        public static int SafeGetInt32(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            else
                return -1;
        }

        /*Realiza la consulta SQL de inserción de un nuevo recurso humano a la base de datos, inserta en tablas Usuario y telefonoUsuario
         *Recibe una entidad recurso humano @rh con la informacón a insertar
         *Devuelve un valor entero dependiendo del resultado de la consulta:
         *0: Inserción correcta en ambas tablas
         *-1: Error insertando en tabla Usuario
         *-2: Error insertando en tabla telefonoUsuario
         *2627: Error de atributo duplicado (cedula o nombre de usuario).
         */
        public int insertaRH(EntidadRecursoH rh)
        {
            //se crea la consulta como un string para luego utlizarla en el metodo ejecutaConsulta(string)
            string consulta = "INSERT INTO Usuario (cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, rol)"
            + "values (" + rh.Cedula + ",'" + rh.Nombre + "', '" + rh.PApellido + "', '" + rh.SApellido + "', '" + rh.Correo + "', '" + rh.NomUsuario + "', '"
            + rh.Contra + "', '" + rh.Perfil + "', '" + rh.Rol + "');";
            int resultado = -1;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                /*Si se hizo bien el insertar de usuario se hace el de telefono, de otro modo no se hace nada y se devuelve -1*/
                if (reader.RecordsAffected > 0)
                {                   
                    string consultaTel = "";
                    /*Si no hay telefonos que insertar se finaliza la insercion a la base*/
                    if (rh.Telefono1 == -1 && rh.Telefono2 == -1)
                    {
                        resultado = 0;
                    }
                    else                    
                    {    /*Se revisan los casos de cuales telefonos hay que insertar*/
                        //solo primer telefono
                        if (rh.Telefono2 == -1)
                        {
                            consultaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                            + " values (" + rh.Cedula + ", " + rh.Telefono1 + ");";
                        }//solo segundo telefono
                        else if (rh.Telefono1 == -1)
                        {
                            consultaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                            + " values (" + rh.Cedula + ", " + rh.Telefono2 + ");";
                        }//los dos telefonos
                        else
                        {
                            consultaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                            + " values (" + rh.Cedula + ", " + rh.Telefono1 + "), (" + rh.Cedula + ", " + rh.Telefono2 + "); ";
                        }

                        try
                        {//se intenta insertar los telefonos a la tabla telefonoUsuario
                            reader = baseDatos.ejecutarConsulta(consultaTel);
                            if (reader.RecordsAffected > 0)
                            {
                                //si se insertó correctamente se devuelve 0
                                resultado = 0;
                            }
                            else
                            {
                                /*resultado = -2 indica que hubo error al insertar el o los telefonos*/
                                resultado = -2;
                            }
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }
                    }

                }
                //Si no se insertó nada se devuelve -1
                else
                {
                    resultado = -1;
                }

            }
            catch (SqlException ex)
                //en caso de una excepcion SQL se devuelve el numero de excepcion
            {
                throw ex;
            }
            
            return resultado;
        }

        /*Realiza la consulta SQL de modificación de un recurso humano en la base de datos, modifica tablas Usuario y telefonoUsuario
         *Recibe una entidad recurso humano @rh con la informacón a actualizar
         *Devuelve un valor entero dependiendo del resultado de la consulta:
         *0:  Actualización correcta de ambas tablas
         *-1: Error actualizando en tabla Usuario
         *-2: Error insertando en tabla telefonoUsuario
         *2627: Error de atributo duplicado (cedula o nombre de usuario).
         */
        public int modificaRH(EntidadRecursoH rh)
        {
            //se crea la consulta como un string para luego utlizarla en el metodo ejecutaConsulta(string)
            string consulta = "UPDATE Usuario "
            + " SET pNombre= '" + rh.Nombre + "', pApellido = '" + rh.PApellido + "', sApellido = '" + rh.SApellido + "', correo= '" + rh.Correo
            + "', nomUsuario= '" + rh.NomUsuario 
            + "', contrasena = '" + rh.Contra + "', perfil= '" + rh.Perfil + "', rol = '" + rh.Rol + "', cedula = " + rh.Cedula 
            + " WHERE idRH = " + rh.IdRH + ";";
            int resultado = -1;

            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.RecordsAffected > 0)
                {
                    //si se realiza la modificacion correcta del usuario se hace el cambio de los telefonos

                    string borraTel = " DELETE FROM  TelefonoUsuario WHERE cedula = " + rh.Cedula + "; ";
                    string insertaTel = "";
                    /*Se revisan los casos para ver cual(es) telefono(s) insertar*/
                    if (rh.Telefono1 != -1 && rh.Telefono2 == -1)
                    {
                        insertaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                        + " values (" + rh.Cedula + ", " + rh.Telefono1 + ");";
                    }
                    else if (rh.Telefono1 == -1 && rh.Telefono2 != -1)
                    {
                        insertaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                        + " values (" + rh.Cedula + ", " + rh.Telefono2 + ");";
                    }
                    else if (rh.Telefono2 != -1 && rh.Telefono2 != -1)
                    {
                        insertaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                        + " values (" + rh.Cedula + ", " + rh.Telefono1 + "), (" + rh.Cedula + ", " + rh.Telefono2 + "); ";
                    }

                    try
                    {
                        //se borran todos los numeros relacionados a la persona, utlizando su numero de cedula
                        reader = baseDatos.ejecutarConsulta(borraTel);
                        if (insertaTel != "")
                        {
                            try
                            {
                                //se insertan los nuevos numeros 
                                reader = baseDatos.ejecutarConsulta(insertaTel);
                                if (reader.RecordsAffected > 0)
                                {
                                    //se devuelve 0 si tanto el usuario como los telefonos se modificaron correctamente
                                    resultado = 0;
                                }
                                else
                                {
                                    //Si hubo un error al modificar los telefonos
                                    resultado = -2;
                                }
                            }
                            catch (SqlException ex)
                            {
                                throw ex;
                            }
                        }
                    }

                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                }
                //si no se modificó el usuario correctamente se devuelve -1
                else
                {
                    resultado = -1;
                }

            }
            //en caso de una excepcion SQL se devuelve el numero de excepcion
            catch (SqlException ex)
            {
                throw ex;
            }

            return resultado;
        }

        /*Realiza la consulta SQL de eliminación de un recurso humano de la base de datos, elimina de tablas Usuario y telefonoUsuario
         *Recibe un valor entero que es el numero de cedula: @cedula
         *Devuelve un valor entero dependiendo del resultado de la consulta:
         *0:  Eliminación correcta de tuplas en ambas tablas
         *-1: Error eliminando de tabla Usuario
         */
        public int eliminaRH(int cedula)
        {
            //Se crean las consultas como string para luego utilizarlas en el metodo jecutarConsulta(string)
            String consulta = "DELETE FROM Usuario WHERE cedula = " + cedula + "; ";

            string borraTel = " DELETE FROM  TelefonoUsuario WHERE cedula = " + cedula + "; ";
            
            int resultado = -1;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                //si se eliminó correctamente el recurso humano elimina de tabla telefonos
                if (reader.RecordsAffected > 0)                   
                {
                    try
                    {
                        //se borran todos los numeros relacionados a la persona, utlizando su numero de cedula
                        reader = baseDatos.ejecutarConsulta(borraTel);
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    resultado = 0;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }

        public EntidadRecursoH consultaRH(int cedula)
        {
            String consultaU = "SELECT cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, idProy, rol, idRH"
                //String consultaU = "SELECT * "
                + " FROM Usuario u WHERE u.cedula =" + cedula + "; ";
            String consultaT = "SELECT numero "
                + " FROM telefonoUsuario t WHERE t.cedula =" + cedula + "; ";
            EntidadRecursoH rh = null;
            String nombre = "";
            String pApellido = "";
            String sApellido = "";
            String correo = "";
            String usuario = "";
            String contrasena = "";
            char perfil = ' ';
            int idProy = -1;
            String rol = "";
            int telefono1 = -1;
            int telefono2 = -1;
            int idrh = -1;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consultaU);
                try
                {
                    reader.Read();
                    nombre = SafeGetString(reader, 1);
                    pApellido = SafeGetString(reader, 2);
                    sApellido = SafeGetString(reader, 3);
                    correo = SafeGetString(reader, 4);
                    usuario = SafeGetString(reader, 5);
                    contrasena = SafeGetString(reader, 6);
                    perfil = SafeGetString(reader, 7).ElementAt(0);
                    idProy = SafeGetInt32(reader, 8);
                    rol = SafeGetString(reader, 9);
                    idrh = SafeGetInt32(reader, 10);

                }
                catch (SqlException ex)
                {
                    throw ex;
                }

                SqlDataReader readerT = baseDatos.ejecutarConsulta(consultaT);
                try
                {
                    if (readerT.Read())
                    {
                        telefono1 = SafeGetInt32(readerT, 0);
                    }
                    if (readerT.Read())
                    {
                        telefono2 = SafeGetInt32(readerT, 0);
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                rh = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo,
                        usuario, contrasena, perfil, idProy, rol, telefono1, telefono2, idrh);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return rh;
        }

        public EntidadRecursoH consultaRH(String nomUsuario)
        {
            String consultaU = "SELECT cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, idProy, rol, idrh"
                //String consultaU = "SELECT * "
                + " FROM Usuario u WHERE u.nomUsuario ='" + nomUsuario + "'; ";
            EntidadRecursoH rh = null;
            int cedula = -1;
            String nombre = "";
            String pApellido = "";
            String sApellido = "";
            String correo = "";
            String usuario = "";
            String contrasena = "";
            char perfil = ' ';
            int idProy = -1;
            String rol = "";
            int telefono1 = -1;
            int telefono2 = -1;
            int idrh = -1;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consultaU);
                try
                {
                    reader.Read();
                    cedula = SafeGetInt32(reader, 0);
                    nombre = SafeGetString(reader, 1);
                    pApellido = SafeGetString(reader, 2);
                    sApellido = SafeGetString(reader, 3);
                    correo = SafeGetString(reader, 4);
                    usuario = SafeGetString(reader, 5);
                    contrasena = SafeGetString(reader, 6);
                    perfil = SafeGetString(reader, 7).ElementAt(0);
                    idProy = SafeGetInt32(reader, 8);
                    rol = SafeGetString(reader, 9);
                    idrh = SafeGetInt32(reader, 10); 
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                String consultaT = "SELECT numero "
                    + " FROM telefonoUsuario t WHERE t.cedula =" + cedula + "; ";
                SqlDataReader readerT = baseDatos.ejecutarConsulta(consultaT);
                try
                {
                    if (readerT.Read())
                    {
                        telefono1 = SafeGetInt32(readerT, 0);
                    }
                    if (readerT.Read())
                    {
                        telefono2 = SafeGetInt32(readerT, 0);
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                rh = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo,
                        usuario, contrasena, perfil, idProy, rol, telefono1, telefono2, idrh);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return rh;
        }

        public DataTable consultaRRHH()
        {
            String consulta = "SELECT cedula AS 'Cedula', pNombre AS 'Nombre', pApellido AS 'Primer Apellido', sApellido AS 'Segundo Apellido'"
                //String consulta = "SELECT * "
                + " FROM Usuario; ";
            DataTable data = new DataTable();
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

        public DataTable consultaMiembrosProy(int idProy)
        {
            String consulta = "SELECT pNombre AS 'Nombre', pApellido AS 'Primer Apellido', sApellido AS 'Segundo Apellido', correo AS 'E-mail' FROM Usuario WHERE idProy = "+ idProy +";";
            DataTable data = new DataTable();
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

        public int getProyID(string nombreUsuario)
        {
            int idProy = -1;
            try
            {  
                SqlDataReader reader = baseDatos.ejecutarConsulta("SELECT idProy FROM Usuario WHERE nomUsuario = '" + nombreUsuario + "' And idProy IS NOT NULL;");
                if (reader.HasRows)
                {
                    reader.Read();
                    idProy = reader.GetInt32(0);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return idProy;
            
        }

        public string getPerfil(string usuario)
        {
            string resultado = "";
            try { 
                string consulta = "Select perfil from Usuario where nomUsuario = '" + usuario + "'";
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.Read())
                {
                    resultado = reader.GetString(0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }
    }
}