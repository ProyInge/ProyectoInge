using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class ControladoraBDRH
    {
        //Clase que permite una fácil interacción con la base de datos
        private AccesoBaseDatos baseDatos;

        /**
         * Descripción: Constructor por defecto
         * Requiere: Nada
         * Retorna: La controladora construida
         */
        public ControladoraBDRH()
        {
            baseDatos = new AccesoBaseDatos();
        }

        /**
         * Requiere: el nombre del usuario y el password
         * Retorna: un entero como booleano.
         * Le pide a la controladora de la BD que confirme si el usuario y el password son validos
         */
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
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return res;
        }

        /**
         * Requiere: hilera con el nombre usuario.
         * Retorna: hilera con el nombre completo.
         * Consulta la BD y devuelve el nombre completo (nombre y dos apellidos) del usuario.
         */
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
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return nombre;
        }

        /**
         * Requiere: string nombreUsuario
         * Retorna: no aplica.
         * Actualiza la BD poniendo la sesionActiva del usuario en 0.
         */
        public void cerrarSesion(string nombreUsuario)
        {
            string consulta = "EXEC cerrarSesion @nombre = '" + nombreUsuario + "';";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /** 
         * Descripción: Obtiene el campo String de la tabla de forma segura (revisando si es null o no antes de leerlo)
         * Recibe un SqlDataReader con el que se obtiene el campo y el índice de la columna a consultar
         * Devuelve un valor String dependiendo del resultado de la consulta. String.empty si el campo está nulo
         */ 
        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        /** 
         * Descripción: Obtiene el campo entero de la tabla de forma segura (revisando si es null o no antes de leerlo)
         * Recibe un SqlDataReader con el que se obtiene el campo y el índice de la columna a consultar
         * Devuelve un valor entero dependiendo del resultado de la consulta. -1 si el campo está nulo
         */
        public static int SafeGetInt32(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            else
                return -1;
        }

        /**
         * Descripción: Realiza la consulta SQL de inserción de un nuevo recurso humano a la base de datos, inserta en tablas Usuario y telefonoUsuario
         * Recibe una entidad recurso humano @rh con la informacón a insertar
         * Devuelve un valor entero dependiendo del resultado de la consulta:
         * 0: Inserción correcta en ambas tablas
         * -1: Error insertando en tabla Usuario
         * -2: Error insertando en tabla telefonoUsuario
         * 2627: Error de atributo duplicado (cedula o nombre de usuario).
         */
        public int insertaRH(EntidadRecursoH rh)
        {
            //Se crea la consulta como un string para luego utlizarla en el metodo ejecutaConsulta(string)
            string consulta = "INSERT INTO Usuario (cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, rol)"
            + "values                              (@0,     @1,      @2,        @3,        @4,     @5,         @6,         @7,     @8);";
            Object[] args = new Object[9];
            args[0] = rh.Cedula;
            args[1] = rh.Nombre;
            args[2] = rh.PApellido;
            args[3] = rh.SApellido;
            args[4] = rh.Correo;
            args[5] = rh.NomUsuario;
            args[6] = rh.Contra;
            args[7] = rh.Perfil;
            args[8] = rh.Rol;
            int resultado = -1;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta, args);
                //Si se hizo bien el insertar de usuario se hace el de telefono, de otro modo no se hace nada y se devuelve -1
                if (reader.RecordsAffected > 0)
                {
                    reader.Close();   
                    string consultaTel = "";
                    //Si no hay telefonos que insertar se finaliza la insercion a la base
                    if (rh.Telefono1 == -1 && rh.Telefono2 == -1)
                    {
                        resultado = 0;
                    }
                    else                    
                    {   //Se revisan los casos de cuales telefonos hay que insertar
                        //Solo primer telefono válido
                        if (rh.Telefono2 == -1)
                        {
                            consultaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                            + " values (" + rh.Cedula + ", " + rh.Telefono1 + ");";
                        }//Solo segundo telefono válido
                        else if (rh.Telefono1 == -1)
                        {
                            consultaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                            + " values (" + rh.Cedula + ", " + rh.Telefono2 + ");";
                        }//Los dos telefonos válidos
                        else
                        {
                            consultaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                            + " values (" + rh.Cedula + ", " + rh.Telefono1 + "), (" + rh.Cedula + ", " + rh.Telefono2 + "); ";
                        }

                        try
                        {//Se intenta insertar los telefonos a la tabla telefonoUsuario
                            reader = baseDatos.ejecutarConsulta(consultaTel);
                            if (reader.RecordsAffected > 0)
                            {
                                //Si se insertó correctamente se devuelve 0
                                resultado = 0;
                            }
                            else
                            {
                                //resultado = -2 indica que hubo error al insertar el o los telefonos
                                resultado = -2;
                            }
                            reader.Close();
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
                //En caso de una excepcion SQL se tira a la capa superior para ser tratada
            {
                throw ex;
            }
            
            return resultado;
        }

        /**
         * Descripción: Realiza la consulta SQL de modificación de un recurso humano en la base de datos, modifica tablas Usuario y telefonoUsuario
         * Recibe una entidad recurso humano @rh con la informacón a actualizar
         * Devuelve un valor entero dependiendo del resultado de la consulta:
         * 0:  Actualización correcta de ambas tablas
         * -1: Error actualizando en tabla Usuario
         * -2: Error insertando en tabla telefonoUsuario
         * 2627: Error de atributo duplicado (cedula o nombre de usuario).
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
                    reader.Close();
                    //Si se realiza la modificacion correcta del usuario se hace el cambio de los telefonos

                    string borraTel = " DELETE FROM  TelefonoUsuario WHERE cedula = " + rh.Cedula + "; ";
                    string insertaTel = "";
                    //Se revisan los casos para ver cual(es) telefono(s) insertar
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
                        //Se borran todos los numeros relacionados a la persona, utlizando su numero de cedula
                        reader = baseDatos.ejecutarConsulta(borraTel);
                        reader.Close();
                        if (insertaTel != "")
                        {
                            try
                            {
                                //Se insertan los nuevos numeros 
                                reader = baseDatos.ejecutarConsulta(insertaTel);
                                if (reader.RecordsAffected > 0)
                                {
                                    //Se devuelve 0 si tanto el usuario como los telefonos se modificaron correctamente
                                    resultado = 0;
                                }
                                else
                                {
                                    //Si hubo un error al modificar los telefonos
                                    resultado = -2;
                                }
                                reader.Close();
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
                //Si no se modificó el usuario correctamente se devuelve -1
                else
                {
                    resultado = -1;
                }

            }
            //En caso de una excepcion SQL se tira para tratarla en la capa superior
            catch (SqlException ex)
            {
                throw ex;
            }

            return resultado;
        }

        /**
         * Descripción: Realiza la consulta SQL de eliminación de un recurso humano de la base de datos, elimina de tablas Usuario y telefonoUsuario
         * Recibe: Un valor entero que es el numero de cedula: @cedula
         * Devuelve un valor entero dependiendo del resultado de la consulta:
         * 0:  Eliminación correcta de tuplas en ambas tablas
         * -1: Error eliminando de tabla Usuario
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
                //Si se eliminó correctamente el recurso humano elimina de tabla telefonos
                if (reader.RecordsAffected > 0)                   
                {
                    reader.Close();
                    try
                    {
                        //Se borran todos los numeros relacionados a la persona, utlizando su numero de cedula
                        reader = baseDatos.ejecutarConsulta(borraTel);
                        reader.Close();
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

        /**
         * Requiere: int cedula
         * Retorna EntidadRecursoH.
         * Consulta en la BD en la tabla RRHH la fila con la llave primaria cedula y la devuelve.
         */
        public EntidadRecursoH consultaRH(int cedula)
        {
            //Hace la consulta de todos los campos
            String consultaU = "SELECT cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, idProy, rol, idRH"
                + " FROM Usuario u WHERE u.cedula =" + cedula + "; ";
            String consultaT = "SELECT numero "
                + " FROM telefonoUsuario t WHERE t.cedula =" + cedula + "; ";
            //Inicialice variables locales
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
                    if(reader.Read())
                    {//Si pudo leer, obtenga los datos de forma segura
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
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

                //Consultamos los teléfonos
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
                    readerT.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                //Encapsulo los datos
                rh = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo,
                        usuario, contrasena, perfil, idProy, rol, telefono1, telefono2, idrh);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return rh;
        }

        /**
         * Requiere: String nomUsuario
         * Retorna EntidadRecursoH.
         * Consulta en la BD en la tabla RRHH la fila con el nombre de usuario dado y la devuelve.
         */
        public EntidadRecursoH consultaRH(String nomUsuario)
        {
            //Consulta la tupla con el usuario seleccionado
            String consultaU = "SELECT cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, idProy, rol, idrh"
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
                    //Si leyó, llene los campos
                    if (reader.Read()) { 
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
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                //Una vez consultada la información, uso la cédula obtenida para traer los números
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
                    readerT.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                //Encapsulo los datos
                rh = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo,
                        usuario, contrasena, perfil, idProy, rol, telefono1, telefono2, idrh);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return rh;
        }

        /**
         * Requiere: no aplica
         * Retorna: DataTable con la tabla
         * Consulta la tabla RRHH  y devuelve en un DataTable toda la tabla RRHH.
         */
        public DataTable consultaRRHH()
        {
            //La consulta debe quedar con las columnas en formato adecuado para que se muestren en el grid
            String consulta = "SELECT cedula AS 'Cedula', pNombre AS 'Nombre', pApellido AS 'Primer Apellido', sApellido AS 'Segundo Apellido'"
                + " FROM Usuario; ";
            DataTable data = new DataTable();
            try
            {
                //Obtengo la tabla
                data = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;
        }

        /**
         * Requiere: int idProyecto
         * Retorna: DataTable
         * Consulta los miembros asociados al proyecto idProyecto.
         */
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

        /**
         * Requiere: string nombreUsuario
         * Retorna: int
         * Consulta la tabla RRHH y devuelve el ID de proyecto al que esta asociado el nombreUsuario.
         */
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
                    reader.Close();
                }
                reader.Close();
            }
            catch(Exception e)
            {
                throw e;
            }
            return idProy;
            
        }

        /**
         * Requiere: string usuario
         * Retorna: string
         * Consulta la tabla RRHH y devuelve el tipo de perfil del usuario.
         */
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
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }
    }
}