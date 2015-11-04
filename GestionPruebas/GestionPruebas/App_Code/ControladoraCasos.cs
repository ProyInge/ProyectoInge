using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace GestionPruebas.App_Code
{
    public class ControladoraCasos
    {
        private ControladoraBDCasos controlBDCasos;

        public ControladoraCasos()
        {
            controlBDCasos = new ControladoraBDCasos();
        }
        /*
         * Descripción: Inserta un nuevo Caso de Prueba. Llama a la controladora de base de datos de Casos, la cual se encarga posteriormente de la consulta SQL.
         * Recibe: Los atributos del caso nuevo a ingresar.
         * Devuelve: una hilera de caracteres indicando si la insercion tuvo exito.
         */
        public string insertarCaso(string id, string proposito, string entrada, string resultadoEsperado, string flujoCentral, int idDise, int idProy)
        {

            EntidadCaso casoNuevo = new EntidadCaso(id, proposito, entrada, resultadoEsperado, flujoCentral, idDise, idProy);

            try
            {
                return controlBDCasos.insertarCaso(casoNuevo);
            }
            catch (SqlException ex)
            {
                return ex.Message;
                //throw ex;
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
        public int modificaCaso(string id, string proposito, string entrada, string resultadoEsperado, string flujoCentral, int idDise, int idProy)
        {
            EntidadCaso modCaso = new EntidadCaso(id, proposito, entrada, resultadoEsperado, flujoCentral, idDise, idProy);
            try
            {
                return 0;// controlBDCasos.modificaCaso(modCaso);
            }
            catch (SqlException ex)
            {
                //se devuelve el numero de la excepcion: 2627-violacion propiedad unica
                return ex.Number;
            }
        }



        public DataTable consultarCasos()
        {
            try
            {
                return controlBDCasos.consultarCasos();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public EntidadCaso consultaCaso(String id, String idDis)
        {
            try
            {
                return controlBDCasos.consultaCaso(id, idDis);
            }
            catch (SqlException e)
            {
                //return null;
                throw e;
            }
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
                return controlBDCasos.getPerfil(usuario);
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public string consultarReq(String id, String idDis)
        {
            return controlBDCasos.consultarReq(id, idDis);
        }
    }
}