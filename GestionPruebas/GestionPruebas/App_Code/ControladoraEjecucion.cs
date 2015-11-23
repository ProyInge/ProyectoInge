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

        public int insertarEjecucion(Object[] args)
        {
            EntidadEjecucion ent = new EntidadEjecucion(args);
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
                return null;
                //return controlBD.consultarNoConformidades(idEje);
            }
            catch (SqlException e)
            {
                throw e;
                //return ""+e.Number;
            }
        }
    }
}