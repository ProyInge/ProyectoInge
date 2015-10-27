using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class ControladoraRH
    {
        //Controladora de base de datos para manejar el acceso a la base de datos
        private ControladoraBDRH controlBD;

        /**
         * Descripción: Constructor por defecto
         * Requiere: Nada
         * Retorna: La controladora construida
         */
        public ControladoraRH()
        {
            controlBD = new ControladoraBDRH();
        }

        /**
         * Requiere: el nombre del usuario y el password
         * Retorna: un entero como booleano.
         * Le pide a la controladora de la BD que confirme si el usuario y el password son validos
         */
        public int usuarioValido(string nombreUsuario, string contra)
        {
            try
            {
                return controlBD.usuarioValido(nombreUsuario, contra);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        /**
         * Requiere: hilera con el nombre usuario.
         * Retorna: hilera con el nombre completo.
         * Consulta la BD y devuelve el nombre completo (nombre y dos apellidos) del usuario.
         */
        public string getNombreCompleto(string nombreUsuario)
        {
            try
            {
                return controlBD.getNombreCompleto(nombreUsuario);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
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
        public int insertaRH(int cedula, string nombre, string pApellido, string sApellido, string correo, string nomUsuario, string contra, char perfil, int idProy, string rol, int telefono1, int telefono2, DateTime fecha)
        {
            EntidadRecursoH insRH = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo, nomUsuario, contra, perfil, idProy, rol, telefono1, telefono2, -1, fecha);
            try
            {
                return controlBD.insertaRH(insRH);
            }
            catch (SqlException ex)
            {
                throw ex;
                //return ex.Number;
            }
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
        public int modificaRH(int cedula, string nombre, string pApellido, string sApellido, string correo, string nomUsuario, string contra, char perfil, int idProy, string rol, int telefono1, int telefono2, int idrh, DateTime fecha)
        {
            EntidadRecursoH modRH = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo, nomUsuario, contra, perfil, idProy, rol, telefono1, telefono2, idrh, fecha);
            try
            {
                return controlBD.modificaRH(modRH);
            }
            catch (SqlException ex)
            {
                //throw ex;
                return ex.Number;
            }
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
            try
            {
                return controlBD.eliminaRH(cedula);
            }
            catch (SqlException ex)
            {
                return ex.Number;
            }
        }

        /**
         * Requiere: int cedula
         * Retorna EntidadRecursoH.
         * Consulta en la BD en la tabla RRHH la fila con la llave primaria cedula y la devuelve.
         */
        public EntidadRecursoH consultaRH(int cedula)
        {
            try
            {
                return controlBD.consultaRH(cedula);
            }
            catch (SqlException e)
            {
                //return null;
                throw e;
            }
        }

        /**
         * Requiere: String nomUsuario
         * Retorna EntidadRecursoH.
         * Consulta en la BD en la tabla RRHH la fila con el nombre de usuario dado y la devuelve.
         */
        public EntidadRecursoH consultaRH(string nomUsuario)
        {
            try
            {
                return controlBD.consultaRH(nomUsuario);
            }
            catch (SqlException e)
            {
                //return null;
                throw e;
            }
        }

        /**
         * Requiere: no aplica
         * Retorna: DataTable con la tabla
         * Consulta la tabla RRHH  y devuelve en un DataTable toda la tabla RRHH.
         */
        public DataTable consultaRRHH()
        {
            try
            {
                return controlBD.consultaRRHH();
            }
            catch (SqlException ex)
            {
                //return null;
                throw ex;
            }

        }

        /**
         * Requiere: string nombreUsuario
         * Retorna: no aplica.
         * Actualiza la BD poniendo la sesionActiva del usuario en 0.
         */
        public void cerrarSesion(string nombreUsuario)
        {
            controlBD.cerrarSesion(nombreUsuario);
        }

        /**
         * Requiere: int idProyecto
         * Retorna: DataTable
         * Consulta los miembros asociados al proyecto idProyecto.
         */
        public DataTable consultaMiembrosProy(int idProyecto)
        {
            return controlBD.consultaMiembrosProy(idProyecto);
        }

        /**
         * Requiere: string nombreUsuario
         * Retorna: int
         * Consulta la tabla RRHH y devuelve el ID de proyecto al que esta asociado el nombreUsuario.
         */
        public int getProyID(string nombreUsuario)
        {
            return controlBD.getProyID(nombreUsuario);
        }

        /**
         * Requiere: string usuario
         * Retorna: string
         * Consulta la tabla RRHH y devuelve el tipo de perfil del usuario.
         */
        public string getPerfil(string usuario)
        {
            try
            {
                return controlBD.getPerfil(usuario);
            } catch (SqlException ex)
            {
                return ex.Message;
            }
        }

    }
}