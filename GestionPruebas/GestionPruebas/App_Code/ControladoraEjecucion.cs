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

        public int insertarEjecucion(Object[] ejecucion,List<Object[]> noConformidad) 
        {
            EntidadEjecucion ejec = new EntidadEjecucion(ejecucion);
            List<EntidadNoConformidad> listaConf = new List<EntidadNoConformidad>();

            for (int i = 0; i < noConformidad.Count; i++)
            {
                EntidadNoConformidad conf = new EntidadNoConformidad(noConformidad.ElementAt(i));
                listaConf.Add(conf);
            }

            try
            {
                return controlBD.insertarEjecucion(ejec, listaConf);
                //return controlBD.insertarEjecucion(ejec);
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
        public EntidadNoConformidad[] modif_NC(Object[] noConformidad)
        {
            try
            {
                EntidadNoConformidad ent_NC = new EntidadNoConformidad(noConformidad);
                return null;
                //return controlBD.modifica_NC(ent_NC);
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
                   l.Add(entidad);
               }
               return l;
        }

        public List<Object[]> consultarNoConformidades(string idEjecucion)
        {
            List<Object[]> l = new List<Object[]>();
            DataTable data = controlBD.consultarNoConformidades(idEjecucion);

            foreach (DataRow row in data.Rows)
            {
                Object[] o = new Object[9];
                o[0] = row["idTupla"];
                o[1] = row["idEjecucion"];
                o[2] = row["idDise"];
                o[3] = row["idCaso"];
                o[4] = row["tipo"];
                o[5] = row["descripcion"];
                o[6] = row["justificacion"];
                o[7] = row["estado"];
                o[8] = row["imagen"];
                l.Add(o);
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

        public List<string> traerResp(string idProy)
        {
            return controlBD.traerResp(idProy);
        }

        public List<string> traerCasos(string idDise)
        {
            return controlBD.traerCasos(idDise);
        }

        public string nombrarProy(string idDise)
        {
            return controlBD.nombrarProy(idDise);
        }

    }
}