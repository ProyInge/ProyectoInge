using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.App_Code
{
    public class ControladoraBDRH
    {
        private AccesoBaseDatos baseDatos;

        public ControladoraBDRH()
        {
             baseDatos = new AccesoBaseDatos();
        }

        public int usuarioValido(string nombreUsuario, string contra)
        {
            int resultado  = -1;
            string consulta = "EXEC iniciarSesion @nombre='" + nombreUsuario.Trim() + "', @contra='" + contra.Trim() + "';";
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            if(reader.Read())
            {
                resultado = reader.GetInt32(0);
            }
            return resultado;
        }

        public void cerrarSesion(string nombreUsuario)
        {
            string consulta = "EXEC cerrarSesion @nombre='" + nombreUsuario.Trim() + "';";
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
        }

    }
}