using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class ControladoraBDDiseno
    {
        AccesoBaseDatos baseDatos;
        public ControladoraBDDiseno()
        {
            baseDatos = new AccesoBaseDatos();
        }
    }
}