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


    }
}