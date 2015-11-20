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
        public int insertarCaso(string id, string proposito, string entrada, string resultadoEsperado, string flujoCentral, int idDise, int idProy)
        {

            EntidadCaso casoNuevo = new EntidadCaso(id, proposito, entrada, resultadoEsperado, flujoCentral, idDise, idProy);

            try
            {
                return controlBDCasos.insertarCaso(casoNuevo);
            }
            catch (SqlException ex)
            {
                return ex.Number;
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
        public int modificaCaso(string id, string proposito, string entrada, string resultadoEsperado, string flujoCentral, int idDise, int idProy, string idV, int idDiseV)
        {
            EntidadCaso modCaso = new EntidadCaso(id, proposito, entrada, resultadoEsperado, flujoCentral, idDise, idProy);
            try
            {
                return controlBDCasos.modificaCaso(modCaso, idV, idDiseV);
            }
            catch (SqlException ex)
            {
                //se devuelve el numero de la excepcion: 2627-violacion propiedad unica
                return ex.Number;
            }
        }

        /*
         * Descripción: Consulta todos los casos de prueba asociados a un diseño
         * Requiere: identificador del diseño asociado al caso de prueba
         * Retorna: DataTable con los casos asociados a @idDise
         */ 
        public DataTable consultarCasos(string idDise)
        {
            try
            {
                return controlBDCasos.consultarCasos(idDise);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /*
        * Descripción: Consulta un caso de prueba especifico asociado a un diseño
        * Requiere: identificador del caso de prueba y del diseño asociado al caso a consultar
        * Retorna: DataTable con los casos asociados a @idDise
        */ 
        public EntidadCaso consultaCaso(string id, string idDis)
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

        /*
         * Descripción: Consulta todos los requerimientos asociados a un diseño
         * Requiere: identificador del diseño asociado a los requerimientos
         * Retorna: hilera con lista de requerimientos con los requerimientos asociados
         */ 
        public string consultarReq(string idDis)
        {
            return controlBDCasos.consultarReq(idDis);
        }

        /*
         * Descripción: Consulta información de un diseño de prueba asociados a un proyecto para mostralo en la interfaz de casos. 
         * Requiere: identificador del diseño asociado al caso de prueba
         * Retorna: Objeto con la informacion del resumen
         */ 
        public Object[] hacerResumen(string idDiseno)
        {
            return controlBDCasos.hacerResumen(idDiseno);
        }

        /*
         * Descripción: Elimina un caso de prueba asociados a un diseño
         * Requiere: identificador del caso de prueba y del diseño asociado al caso de prueba
         * Retorna: int que indica el resultado de la consulta con los casos asociados a @idDise
         */        
        internal int eliminarCaso(string idCaso, string idDise)
        {
            return controlBDCasos.eliminarCaso(idCaso, idDise);
        }
    }
}