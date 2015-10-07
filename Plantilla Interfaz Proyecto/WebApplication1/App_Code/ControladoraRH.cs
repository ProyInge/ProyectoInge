using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.App_Code
{
    public class ControladoraRH
    {
        private ControladoraBDRH controlBD;

        public ControladoraRH()
        {
            controlBD = new ControladoraBDRH();
        }

        public bool usuarioValido(string nombreUsuario, string contra)
        {
            return controlBD.usuarioValido(nombreUsuario, contra);
        }
    }
}