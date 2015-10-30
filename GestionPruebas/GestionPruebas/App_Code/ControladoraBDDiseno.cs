using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace GestionPruebas.App_Code
{
    public class ControladoraBDDiseno
    {
        //Clase que controla el acceso a la base de datos
        private AccesoBaseDatos baseDatos;

        public AccesoBaseDatos BaseDatos
        {
            get{ return baseDatos; }
            set{ baseDatos = value; }
        }

        /**
         * Descripción: Constructor por defecto
         * Requiere: Nada
         * Retorna: La controladora construida
         */
        public ControladoraBDDiseno()
        {
            BaseDatos = new AccesoBaseDatos();
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
         * Descripción: Obtiene el campo fecha de la tabla de forma segura (revisando si es null o no antes de leerlo)
         * Recibe un SqlDataReader con el que se obtiene el campo y el índice de la columna a consultar
         * Devuelve un valor DateTime dependiendo del resultado de la consulta. La fecha actual si el campo está nulo
         */
        public static DateTime SafeGetDateTime(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            else
                return DateTime.Now;
        }

        /** 
         * Descripción: Obtiene el campo fecha de la tabla de forma segura (revisando si es null o no antes de leerlo)
         * Recibe un SqlDataReader con el que se obtiene el campo y el índice de la columna a consultar
         * Devuelve un valor DateTime dependiendo del resultado de la consulta. La fecha actual si el campo está nulo
         */
        public static DateTime SafeGetDate(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            else
                return DateTime.Today;
        }

        /** 
         * Descripción: Inserta un Requerimiento en la BD
         * Recibe dos strings que son los atributos
         * RET: N/A
         */

        public void insertarReq(string id, string nomReq)
        {
            string resultado = "";
            string consulta = "";

            try
            {
                consulta = "INSERT INTO Requerimiento VALUES (@0, @1);";
                Object[] args = new Object[2];
                args[0] = id;
                args[1] = nomReq;
                SqlDataReader res = baseDatos.ejecutarConsulta(consulta, args);
                res.Close();
            }
            catch (SqlException ex)
            {
                resultado = "Error al insertar, Error 1: " + ex.Message;
            }
        }

        public void modificarReq(string idViejo, string nomViejo, string idNuevo, string nomNuevo)
        {
            string consulta = "UPDATE Requerimiento Set id = '" + idNuevo + "', nombre = '" + nomNuevo + "' WHERE id = '" + idViejo + "';";
            SqlDataReader res = baseDatos.ejecutarConsulta(consulta);
            res.Close();
        }

        /**
         * Requiere: int id
         * Retorna EntidadDiseno.
         * Consulta en la BD en la tabla diseno la fila con el id de diseno dado y la devuelve encapsulada.
         */
        public EntidadDiseno consultaDiseno(int id)
        {//Hace la consulta de todos los campos
            string consultaU = "SELECT criterios, nivel, tecnica, ambiente, procedimiento, fecha, proposito, responsable, idProy"
                + " FROM Diseno d WHERE d.id=" + id + "; ";
            //Inicialice variables locales
            EntidadDiseno dise = null;
            string criterios = "";
            string nivel = "";
            string tecnica = "";
            string ambiente = "";
            string procedimiento = "";
            DateTime fecha = DateTime.Today;
            string proposito = "";
            int responsable = -1;
            int idProy = -1;

            try
            {
                SqlDataReader reader = BaseDatos.ejecutarConsulta(consultaU);
                try
                {
                    if (reader.Read())
                    {//Si pudo leer, obtenga los datos de forma segura
                        criterios = SafeGetString(reader, 0);
                        nivel = SafeGetString(reader, 1);
                        tecnica = SafeGetString(reader, 2);
                        ambiente = SafeGetString(reader, 3);
                        procedimiento = SafeGetString(reader, 4);
                        fecha = SafeGetDate(reader, 5);
                        proposito = SafeGetString(reader, 6);
                        responsable = SafeGetInt32(reader, 7);
                        idProy = SafeGetInt32(reader, 8);
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                //Encapsulo los datos
                dise = new EntidadDiseno(id, criterios, nivel, tecnica, ambiente,
                        procedimiento, fecha, proposito, responsable, idProy);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return dise;
        }

        /**
         * Requiere: no aplica
         * Retorna: DataTable con la tabla
         * Consulta la tabla diseno y la devuelve en un DataTable.
         */
        public DataTable consultaDisenos(int idProy)
        {
            //La consulta debe quedar con las columnas en formato adecuado para que se muestren en el grid
            string consulta = "SELECT id AS 'ID', proposito AS 'Propósito', nivel AS 'Nivel'"
                + " FROM Diseno WHERE idProy = " + idProy + "; ";
            DataTable data = new DataTable();
            try
            {
                //Obtengo la tabla
                data = BaseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;
        }

        /**
         * Requiere: int id
         * Retorna string[].
         * Consulta en la BD en la tabla requerimiento la fila con el id de requerimiento dado y la devuelve en un vector string.
         */
        public DataTable consultaReqDisponibles(int idDise)
        {//Hace la consulta de todos los campos
            string consulta = "SELECT r.id, r.nombre"
                            + " FROM Requerimiento r LEFT OUTER JOIN DisenoRequerimiento d"
                            + " WHERE d.idDise is null AND d.idReq=r.id AND d.idDise != "+idDise+"; ";
            DataTable res = new DataTable();
            try
            {
                res = BaseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return res;
        }

        /**
         * Requiere: int id
         * Retorna string[].
         * Consulta en la BD en la tabla requerimiento la fila con el id de requerimiento dado y la devuelve en un vector string.
         */
        public DataTable consultaReqAsignados(int idDise)
        {//Hace la consulta de todos los campos
            string consulta = "SELECT r.id, r.nombre"
                            + " FROM Requerimiento r, DisenoRequerimiento d"
                            + " WHERE d.idDise=" + idDise + " AND d.idReq=r.id; ";
            //Inicialice variables locales
            DataTable res = new DataTable();
            try
            {
                res = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return res;
        }

        /**
         * Requiere: no aplica
         * Retorna: DataTable con la tabla
         * Consulta la tabla requerimiento y la devuelve en un DataTable.
         */
        public DataTable consultaRequerimientos()
        {
            //La consulta debe quedar con las columnas en formato adecuado para que se muestren en el grid
            string consulta = "SELECT id AS 'ID', nombre AS 'Nombre'"
                + " FROM Requerimiento; ";
            DataTable data = new DataTable();
            try
            {
                //Obtengo la tabla
                data = BaseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;
        }

        /**
         * Descripción: Realiza la consulta SQL de eliminación de undiseño de prueba de la base de datos, elimina de tabla Diseño
         * Recibe: Un valor entero que es el identificador del diseño: @id
         * Devuelve un valor entero dependiendo del resultado de la consulta:
         * 0:  Eliminación correcta de tuplas en ambas tablas
         * -1: Error eliminando de tabla Usuario
         */
        public int eliminaDiseno(int id)
        {
            string consulta = " DELETE FROM Diseno WHERE id = " + id + "; ";
            int resultado = -1;
            try
            {
                SqlDataReader reader = BaseDatos.ejecutarConsulta(consulta);
                //Si se eliminó correctamente el diseño de prueba se devuelve un cero
                if (reader.RecordsAffected > 0)
                {
                    reader.Close();                    
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
         * Descripción: Realiza la consulta SQL de eliminación de un requerimiento de la base de datos, elimina de la tabla Requerimiento
         * Recibe: Un valor entero que es el identificador del requerimiento a eliminar: @idReq
         * Devuelve un valor entero dependiendo del resultado de la consulta:
         * 0:  Eliminación correcta de tuplas en ambas tablas
         * -1: Error eliminando de tabla Usuario
         */
        public int eliminaRequerimiento(string idReq) {
            string consulta = " DELETE FROM Requerimiento WHERE id = " + idReq + "; ";
            int resultado = -1;
            try
            {
                SqlDataReader reader = BaseDatos.ejecutarConsulta(consulta);
                //Si se eliminó correctamente el requerimiento de un diseño de prueba se devuelve un cero
                if (reader.RecordsAffected > 0)
                {
                    reader.Close();
                    resultado = 0;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }

        /** Descripcion: Consulta total de un proyecto por filtro 
         * REQ: string 
         * RET: DataTable
         */
        public DataTable consultaProyectos()
        {
            string consulta = "";

            DataTable data = new DataTable();
            //--cambio en a consulta--
            consulta = "SELECT  nombre, id FROM Proyecto;";
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
    }
}