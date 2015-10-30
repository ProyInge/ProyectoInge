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
        private EntidadDiseno entidadDiseno;

        public ControladoraDiseno()
        {
            controlBD = new ControladoraBDDiseno();
            entidadDiseno = new EntidadDiseno();
        }

        /** 
         * Descripción: Manda los parametros a insertar de Requerimientos a la BD
         * Recibe dos string que son los atributos de la tabla
         * RET: N/A
         */

        public void insertarReq(string id, string nombre)
        {
            controlBD.insertarReq(id, nombre);
        }

        public void modificarReq(string idViejo,string nomViejo, string idNuevo, string nomNuevo)
        {
            controlBD.modificarReq(idViejo,nomViejo,idNuevo,nomNuevo);
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
        public DataTable consultaDisenos(int idProy)
        {
            try
            {
                return controlBD.consultaDisenos(idProy);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /**
         * Requiere: int id
         * Retorna string[].
         * * Consulta en la BD los requerimientos Disponibles para el diseño dado.
         */
        public DataTable consultaReqDisponibles(int idDise)
        {
            try
            {
                return controlBD.consultaReqDisponibles(idDise);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /**
         * Requiere: int id
         * Retorna string[].
         * Consulta en la BD los requerimientos asignados al diseño dado.
         */
        public DataTable consultaReqAsignados(int idDise)
        {
            try
            {
                return controlBD.consultaReqAsignados(idDise);
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

        /**
         * Requiere: no aplica
         * Retorna: DataTable con la tabla Proyecto
         * Consulta la tabla proyecto usando la controladoraBDProyecto y la devuelve en un DataTable.
         */
        public DataTable consultaProyectos()
        {
            try
            {
                return controlBD.consultaProyectos();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}