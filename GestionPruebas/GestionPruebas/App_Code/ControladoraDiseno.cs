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
         * Consulta la tabla proyecto usando la controladoraBD y la devuelve en un DataTable.
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

        /**
         * Requiere: no aplica
         * Retorna: DataTable con la tabla Proyecto que contiene el proyecto del usuario determinado
         * Consulta la tabla proyecto usando la controladoraBD y la devuelve en un DataTable.
         */
        public DataTable consultaProyecto(string usuario)
        {
            try
            {
                return controlBD.consultaProyecto(usuario);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /**
         * Requiere: no aplica
         * Retorna: DataTable con la tabla Usuario
         * Consulta la tabla usuario usando la controladoraBD y la devuelve en un DataTable.
         */
        public DataTable consultaRRHH()
        {
            try
            {
                return controlBD.consultaRRHH();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /**
         * Requiere: no aplica
         * Retorna: DataTable con los usuarios del proyecto dado
         * Consulta la tabla usuario usando la controladoraBD y la devuelve en un DataTable.
         */
        public DataTable consultaRRHH(int idProy)
        {
            try
            {
                return controlBD.consultaRRHH(idProy);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public int insertarDiseno(Object[] dis)
        {
            try
            {
                entidadDiseno = new EntidadDiseno(dis);
                return controlBD.insertarDiseno(entidadDiseno);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        
        public EntidadDiseno modificarDiseno(Object[] dis_Actual, Object[] dis_Nuevo)
        {
            try
            {
                entidadDiseno = new EntidadDiseno(dis_Actual);
                EntidadDiseno entidadDisenoN = new EntidadDiseno(dis_Nuevo);
                return controlBD.modificarDiseno(entidadDiseno, entidadDisenoN);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public void modificarReq(string idViejo, string nomViejo, string idNuevo, string nomNuevo)
        {
            try
            {
                controlBD.modificarReq(idViejo, nomViejo, idNuevo, nomNuevo);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public bool revisarReqExistente(string id)
        {
            return controlBD.revisarReqExistente(id);
        }

        public string obtenerRH( int cedula) {
            try
            {
                return controlBD.obtenerRH(cedula);
            }
            catch (SqlException e)
            {
                throw e;
            }

        }

        public void asignarReqs(List<string> listaA, List<string> listaD, int id)
        {
            controlBD.asignarReqs(listaA,listaD, id);
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
                return controlBD.getPerfil(usuario);
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }
    }
}