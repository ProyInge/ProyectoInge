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
            catch(SqlException e)
            {
                return e.Number;
            }
            
        }
    }
}