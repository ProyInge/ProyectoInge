using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace GestionPruebas.App_Code
{
    public class ControladoraDiseno
    {
        private ControladoraBDDiseno controlBD;

        public ControladoraDiseno()
        {
            controlBD = new ControladoraBDDiseno();
        }

        public void insertarReq(string id, string nombre)
        {

        }

        /**
         * Descripción: Realiza la consulta SQL de eliminación de undiseño de prueba de la base de datos, elimina de tablas Diseño
         * Recibe: Un valor entero que es identificador del diseño de prueba: @idDiseno
         * Devuelve un valor entero dependiendo del resultado de la consulta:
         * 0:  Eliminación correcta de tuplas en tabla Diseño
         * -1: Error eliminando de tabla Diseno
         */
        public int eliminaDiseno(int idDiseno)
        {
            try
            {
                return controlBD.eliminaDiseno(idDiseno);
            }
            catch (SqlException ex)
            {
                return ex.Number;
            }
        }

        public int eliminaRequerimiento(string idReq) {
            try
            {
                return controlBD.eliminaRequerimiento(idReq);
            }
            catch (SqlException ex)
            {
                return ex.Number;
            }
        }

        /**
         * Requiere: int id
         * Retorna EntidadDiseno.
         * Consulta en la BD en la tabla diseno la fila con el id de diseno dado usando la controladoraBD y la devuelve encapsulada.
         */
        public EntidadDiseno consultaDiseno(int id)
        {
            try
            {
                return controlBD.consultaDiseno(id);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /**
         * Requiere: no aplica
         * Retorna: DataTable con la tabla
         * Consulta la tabla diseno usando la controladoraBD y la devuelve en un DataTable.
         */
        public DataTable consultaDisenos()
        {
            try
            {
                return controlBD.consultaDisenos();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /**
         * Requiere: int id
         * Retorna string[].
         * Consulta en la BD en la tabla requerimiento la fila con el id de requerimiento dado usando la controladoraBD y la devuelve en un vector string.
         */
        public string[] consultaRequerimiento(string id)
        {
            try
            {
                return controlBD.consultaRequerimiento(id);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /**
         * Requiere: no aplica
         * Retorna: DataTable con la tabla requerimiento
         * Consulta la tabla requerimiento usando la controladoraBD y la devuelve en un DataTable.
         */
        public DataTable consultaRequerimientos()
        {
            try
            {
                return controlBD.consultaRequerimientos();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}