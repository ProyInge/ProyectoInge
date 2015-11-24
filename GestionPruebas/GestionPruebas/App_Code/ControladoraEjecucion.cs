using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data;

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

        public int insertarEjecucion(Object[] ejecucion) //Object[] noConformidad, 
        {
            EntidadEjecucion ejec = new EntidadEjecucion(ejecucion);
            //EntidadNoConformidad noConf = new EntidadNoConformidad(noConformidad);
            try
            {
                //return controlBD.insertarEjecucion(ejec, noConf);
                return controlBD.insertarEjecucion(ejec);
            }
            catch (SqlException e)
            {
                throw e;
                //return e.Number;
            }
        }

        public Object[] hacerResumen(int idEje)
        {
            try
            {
                return controlBD.hacerResumen(idEje);
            }
            catch (SqlException e)
            {
                throw e;
                //return null;
            }
        }

        public string consultarReq(int idEje)
        {
            try
            {
                return controlBD.consultarReq(idEje);
            }
            catch (SqlException e)
            {
                throw e;
                //return ""+e.Number;
            }
        }

        public EntidadEjecucion consultarEjecucion(int idEje)
        {
            try
            {
                return null;
                //return controlBD.consultarEjecucion(idEje);
            }
            catch (SqlException e)
            {
                throw e;
                //return ""+e.Number;
            }
        }

        public EntidadNoConformidad[] consultarNoConformidades(int idEje)
        {
            try
            {
                return null;
                //return controlBD.consultarNoConformidades(idEje);
            }
            catch (SqlException e)
            {
                throw e;
                //return ""+e.Number;
            }
        }
        public EntidadNoConformidad[] modif_NC(Object[] noConformidad_ant, Object[] noConformidad_nuev)
        {
            try
            {
                EntidadNoConformidad ent_NC_ant = new EntidadNoConformidad(noConformidad_ant);
                EntidadNoConformidad ent_NC_nuev = new EntidadNoConformidad(noConformidad_nuev);
                return null;
                //return controlBD.modifica_NC(ent_NC_ant, ent_NC_nuev);
            }
            catch (SqlException e)
            {
                throw e;
                //return ""+e.Number;
            }
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
                   string idPr = idProy;

                   EntidadEjecucion entidad = new EntidadEjecucion(id, cedResp, responsable, fecha, incidencias, idDi, idPr);
                    
               }
               return l;
        }

        public System.Data.DataTable consultarEjecucionesDt(string idProy, string idDise)
        {
            return controlBD.consultarEjecucionesDt(idProy, idDise);
        }

        public void eliminarEjecucion(string id)
        {
            controlBD.eliminarEjecucion(id);
        }

    }
}