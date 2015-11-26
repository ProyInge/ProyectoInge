using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace GestionPruebas.App_Code
{
    public class ControladoraBDReporte
    {
        private AccesoBaseDatos baseDatos;

        public ControladoraBDReporte()
        {
            baseDatos = new AccesoBaseDatos();
        }

    }
}