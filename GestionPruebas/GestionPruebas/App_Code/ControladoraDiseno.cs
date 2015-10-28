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

        /**
         * Descripción: Realiza la consulta SQL de eliminación de undiseño de prueba de la base de datos, elimina de tablas Diseño y [...]
         * Recibe: Un valor entero que es identificador del diseño de prueba: @idDiseno
         * Devuelve un valor entero dependiendo del resultado de la consulta:
         * 0:  Eliminación correcta de tuplas en tablas [...]
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

        public DataTable consultaDisenos(int id)
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
    }
}