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

        /**
         * Descripción: Constructor por defecto
         * Requiere: Nada
         * Retorna: La controladora construida
         */
        public ControladoraBDDiseno()
        {
            baseDatos = new AccesoBaseDatos();
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
                SqlDataReader reader = baseDatos.ejecutarConsulta(consultaU);
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
        public DataTable consultaDisenos()
        {
            //La consulta debe quedar con las columnas en formato adecuado para que se muestren en el grid
            string consulta = "SELECT id AS 'ID', proposito AS 'Propósito', nivel AS 'Nivel'"
                + " FROM Diseno; ";
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
         * Requiere: int id
         * Retorna string[].
         * Consulta en la BD en la tabla requerimiento la fila con el id de requerimiento dado y la devuelve en un vector string.
         */
        public string[] consultaRequerimiento(string id)
        {//Hace la consulta de todos los campos
            string consultaU = "SELECT nombre"
                + " FROM Requerimiento WHERE id='" + id + "'; ";
            //Inicialice variables locales
            string nombre = "";

            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consultaU);
                try
                {
                    if (reader.Read())
                    {//Si pudo leer, obtenga los datos de forma segura
                        nombre = SafeGetString(reader, 0);
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return (new string[] {id, nombre});
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
                data = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;
        }

        public int eliminaDiseno(int id)
        {
            string consulta = " DELETE FROM Diseno WHERE id = " + id + "; ";
            int resultado = -1;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
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
    }
}