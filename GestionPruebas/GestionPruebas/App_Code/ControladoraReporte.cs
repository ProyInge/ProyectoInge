using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace GestionPruebas.App_Code
{
    public class ControladoraReporte
    {
        private ControladoraBDReporte controlBDCasos;

        public ControladoraReporte()
        {
            controlBDCasos = new ControladoraBDReporte();
        }
    }
}