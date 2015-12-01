using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace GestionPruebas.App_Code
{
    public class ControladoraReporte
    {
        private ControladoraBDReporte controlBD;

        public ControladoraReporte()
        {
            controlBD = new ControladoraBDReporte();
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

        public DataTable consultaEjecuciones(int idDise)
        {
            try
            {
                return controlBD.consultaEjecuciones(idDise);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public EntidadEjecucion consultarEjecucion(int idEjec)
        {
            try
            {
                return controlBD.consultarEjecucion(idEjec);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable consultarNoConformidades(int idEjec)
        {
            try
            {
                return controlBD.consultarNoConformidades(idEjec);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable consultaHistoria(string idProy)
        {
            try
            {
                return controlBD.consultaHistoria(idProy);
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }

}