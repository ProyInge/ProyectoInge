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


        public List<EntidadEjecucion> consultarEjecuciones(string idCaso, string idDise) 
        {
            List<EntidadEjecucion> l = new List<EntidadEjecucion>();

            string consulta = "SELECT e.id, e.fecha, e.incidencias, e.cedResp, CONCAT(u.pNombre, ' ', u.pApellido) AS 'n' FROM Ejecuciones e, Usuario u WHERE e.cedResp = u.cedula AND e.idCaso = '"+idCaso+"' AND e.idDise = "+idDise+";";
            DataTable data = new DataTable();
            try
            {
                /*//Obtengo la tabla
                data = baseDatos.ejecutarConsultaTabla(consulta);

                foreach (DataRow row in data.Rows)
                {
                    int id = Int32.Parse(row["id"].ToString());
                    string idCaso = row["id"].ToString();     
                    int responsable Int32.Parse(row["cedResp"].ToString());;
                    string nombreResponsable;         
                    DateTime fecha;
                    string incidencias;    
                }*/
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            

            return l;

        }
    }
}