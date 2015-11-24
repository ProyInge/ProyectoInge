using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace GestionPruebas.App_Code
{
    public class ControladoraBDEjecucion
    {
        private AccesoBaseDatos baseDatos;

        public ControladoraBDEjecucion()
        {
            baseDatos = new AccesoBaseDatos();
        }

        public void modificarEjecucion(EntidadEjecucion entidad)
        {

        }


        public DataTable consultarEjecuciones(string idProy, string idDise) 
        {

            string consulta = "SELECT e.id, e.fecha, e.incidencias, e.cedResp, CONCAT(u.pNombre, ' ', u.pApellido) AS 'n' FROM Ejecuciones e, Usuario u WHERE e.cedResp = u.cedula AND e.idProy = '" + idProy + "' AND e.idDise = " + idDise + ";";
            DataTable data = null;
            try
            {
                //Obtengo la tabla
                data = baseDatos.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            
            return data;

        }

    }
}