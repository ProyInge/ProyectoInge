using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebApplication1.App_Code
{
    public class ControladoraRH
    {
        private ControladoraBDRH controlBD;

        public ControladoraRH()
        {
            controlBD = new ControladoraBDRH();
        }

        public int usuarioValido(string nombreUsuario, string contra)
        {
            int res = controlBD.usuarioValido(nombreUsuario, contra);
            if(res == 0)
            {
                FormsAuthentication.SetAuthCookie(nombreUsuario, true);
            }
            return res;
        }

        public void cerrarSesion(string nombreUsuario)
        {
            controlBD.cerrarSesion(nombreUsuario);
            FormsAuthentication.SignOut();
        }
    }
}