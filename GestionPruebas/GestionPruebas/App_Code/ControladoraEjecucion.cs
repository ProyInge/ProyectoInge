using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace GestionPruebas.App_Code
{
    public class ControladoraEjecucion
    {
        private ControladoraBDEjecucion controlBD;

        public ControladoraEjecucion()
        {
            controlBD = new ControladoraBDEjecucion();
        }

        public void modificarEjecucion(Object[] args)
        {
            EntidadEjecucion entidad = new EntidadEjecucion(args);
            controlBD.modificarEjecucion(entidad);
        }

        public List<EntidadEjecucion> consultarEjecuciones(string idProy, string idDise)
        {
            List<EntidadEjecucion> l = new List<EntidadEjecucion>();
            //Obtengo la tabla
            DataTable data = controlBD.consultarEjecuciones(idProy, idDise);

               foreach (DataRow row in data.Rows)
               {
                   int id = Int32.Parse(row["id"].ToString());
                   DateTime fecha = Convert.ToDateTime(row["fecha"].ToString());
                   string incidencias = row["incidencias"].ToString(); 
                   int cedResp = Int32.Parse(row["cedResp"].ToString());
                   string responsable = row["n"].ToString();
                   int idDi = Int32.Parse(idDise);
                   int idPr = Int32.Parse(idProy);

                   EntidadEjecucion entidad = new EntidadEjecucion(id, cedResp, responsable, fecha, incidencias, idDi, idPr);
                    
               }
               return l;
        }

    }
}