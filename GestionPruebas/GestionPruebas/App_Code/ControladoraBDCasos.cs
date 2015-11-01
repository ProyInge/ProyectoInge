using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class ControladoraBDCasos
    {
        //Clase que controla el acceso a la base de datos

        private AccesoBaseDatos baseDatos;

        public AccesoBaseDatos BaseDatos
        {
            get { return baseDatos; }
            set { baseDatos = value; }
        }

        /*
         * Descripción: Constructor por defecto de la controladora.
         * Requiere: Nada.
         * Retorna: La controladora nueva.
         */
        public ControladoraBDCasos()
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

        /*
         * Descripción: Inserta un caso en la BD.
         * Recibe: el objeto Entidad Caso.
         * Retorna: n/a.
         */
        public string insertarCaso(EntidadCaso caso)
        {
            string resultado = "Exito";
            string consulta = "";

            consulta = "INSERT INTO CasoPrueba (id,proposito,entrada,resultadoEsperado,flujoCentral,idDise) VALUES (@0, @1, @2, @3, @4, @5);";
            Object[] args = new Object[6];
            args[0] = caso.Id;
            args[1] = caso.Proposito;
            args[2] = caso.Entrada;
            args[3] = caso.ResultadoEsperado;
            args[4] = caso.FlujoCentral;
            args[5] = caso.IdDise;

            try
            {

                SqlDataReader res = baseDatos.ejecutarConsulta(consulta, args);
                res.Close();
            }
            catch (SqlException ex)
            {
                resultado = "Error al insertar. Error 1: " + ex.Message;
            }
            
            return resultado;
        }
    }
}