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
        private AccesoBaseDatos baseDatos;
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
            string consultaU = "SELECT criterios, nivel, tipoPrueba, tecnica, ambiente, procedimiento, fecha, proposito, responsable, idProy"
                + " FROM Diseno d WHERE d.id=" + id + "; ";
            //Inicialice variables locales
            EntidadDiseno dise = null;
            string criterios = "";
            string nivel = "";
            string tipoPrueba = "";
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
                        tipoPrueba = SafeGetString(reader, 2);
                        tecnica = SafeGetString(reader, 3);
                        ambiente = SafeGetString(reader, 4);
                        procedimiento = SafeGetString(reader, 5);
                        fecha = SafeGetDate(reader, 6);
                        proposito = SafeGetString(reader, 7);
                        responsable = SafeGetInt32(reader, 8);
                        idProy = SafeGetInt32(reader, 9);
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                //Encapsulo los datos
                dise = new EntidadDiseno(id, criterios, nivel, tipoPrueba, tecnica, ambiente,
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
            String consulta = "SELECT id AS 'ID', proposito AS 'Propósito', nivel AS 'Nivel', tipoPrueba AS 'Tipo de Prueba'"
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