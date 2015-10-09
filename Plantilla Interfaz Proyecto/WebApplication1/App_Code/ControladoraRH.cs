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
        public bool insertaRH(int cedula, String nombre, String pApellido, String sApellido, String correo, String nomUsuario, String contra, char perfil, int idProy, String rol)
        {
            RecursoH insRH = new RecursoH(cedula, nombre, pApellido, sApellido, correo, nomUsuario, contra, perfil, idProy, rol);
            return true;
            //return controlBD.insertaRH(insRH);
        }

        public bool modificaRH(int cedula, String nombre, String pApellido, String sApellido, String correo, String nomUsuario, String contra, char perfil, int idProy, String rol)
        {
            RecursoH modRH = new RecursoH(cedula, nombre, pApellido, sApellido, correo, nomUsuario, contra, perfil, idProy, rol);
            //return controlBD.modificaRH(insRH);
            return true;
        }

        public bool eliminaRH(int cedula)
        {
            //return controlBD.eliminaRH(cedula);
            return true;
        }

    }
}