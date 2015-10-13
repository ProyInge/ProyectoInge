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

        public bool usuarioValido(string nombreUsuario, string contra)
        {
            string consulta = "SELECT nomUsuario FROM Usuario WHERE contrasena = '" + contra.Trim() + "'AND nomUsuario = '" + nombreUsuario.Trim() + "';";
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            bool resultado = reader.HasRows;
            return resultado;
        }

        public string getNombreCompleto(string nombreUsuario)
        {
            string consulta = "SELECT CONCAT(pNombre, ' ', pApellido, ' ', sApellido) FROM Usuario WHERE nomUsuario = '" + nombreUsuario.Trim() + "';";
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            reader.Read();
            string nombre = reader.GetString(0);

            return nombre;
        }

        public void cerrarSesion(string nombreUsuario)
        {

        }

    }
}