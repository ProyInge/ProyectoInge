using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace GestionPruebas.App_Code
{
    public class ControladoraCasos
    {
        private ControladoraBDCasos controlBDCasos;

        /*
         * Descripción: Inserta un nuevo Caso de Prueba.
         */
        public string insertarCaso(int id, string proposito, string tipoEntrada, string nombreEntrada, string resultadoEsperado, string flujoCentral, int idDise)
        {

            EntidadCasos casoNuevo = new EntidadCasos(id, proposito, tipoEntrada, nombreEntrada, resultadoEsperado, flujoCentral, idDise);

            try
            {
                return controlBDCasos.insertarCaso(casoNuevo);
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
            
        }

        /**
         * Descripción: Realiza la consulta SQL de modificación de un caso de prueba en la base de datos, modifica tabla CasoPrueba
         * Recibe los atributos del caso de prueba a modificar
         * Devuelve un valor entero dependiendo del resultado de la consulta:
         * 0:  Actualización correcta de ambas tablas
         * -1: Error actualizando en tabla casoPrueba
         * 2627: Error de atributo duplicado (id de caso).
         */
        public int modificaCaso(int id, string proposito, string tipoEntrada, string nombreEntrada, string resultadoEsperado, string flujoCentral, int idDise)
        {
            EntidadCasos modCaso = new EntidadCasos(id, proposito, tipoEntrada, resultadoEsperado, flujoCentral, idDise);
            try
            {
                return controlBDCasos.modificaRH(modCaso);
            }
            catch (SqlException ex)
            {
                //throw ex;
                return ex.Number;
            }
        }


    }
}