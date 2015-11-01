﻿using System;
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
         * Descripción: Inserta un nuevo Caso de Prueba. Llama a la controladora de base de datos de Casos, la cual se encarga posteriormente de la consulta SQL.
         * Recibe: Los atributos del caso nuevo a ingresar.
         * Devuelve: una hilera de caracteres indicando si la insercion tuvo exito.
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
            /*EntidadCasos modCaso = new EntidadCasos(id, proposito, tipoEntrada, resultadoEsperado, flujoCentral, idDise);
            try
            {
                return controlBDCasos.modificaRH(modCaso);
            }
            catch (SqlException ex)
            {
                //throw ex;
                return ex.Number;
            }*/
            return 0;
        }


    }
}