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
        AccesoBaseDatos baseD;
        public ControladoraBDEjecucion()
        {
            baseD = new AccesoBaseDatos();
        }

        public void modificarEjecucion(EntidadEjecucion entidad)
        {

        }

        public int insertarEjecucion(EntidadEjecucion ent)
        {
            try
            {
                string query = "INSERT INTO ejecucion ";
                SqlDataReader dr = baseD.ejecutarConsulta(query);
                if (dr.RecordsAffected > 0)
                {
                    //Todo bien, todo sano
                    return 0;
                }
            }
            catch (SqlException e)
            {
                return e.Number;
            }

            return -1;
        }
    }
}