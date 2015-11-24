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

        public int insertarEjecucion(Object[] noConformidad, Object[] ejecucion)
        {
            EntidadEjecucion ent = new EntidadEjecucion();
            try
            {
                return controlBD.insertarEjecucion(ent);
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
                //return null; mbox,
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
    }
}