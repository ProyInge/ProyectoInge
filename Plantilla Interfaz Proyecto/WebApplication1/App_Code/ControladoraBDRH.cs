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

        public bool insertaRH(EntidadRecursoH rh)
        {
            string consulta = "INSERT INTO Usuario (cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, rol)"
            + "values (" + rh.Cedula + "'" + rh.Nombre + "', '" + rh.PApellido + "', '" + rh.SApellido + "', '" + rh.Correo + "', '" + rh.NomUsuario + "', '"
            + rh.Contra + "', '" + rh.Perfil + "', '" + rh.Rol + "');";

            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            bool resultado = reader.HasRows;
            return resultado;
        }

        public bool modificaRH(EntidadRecursoH rh)
        {
            string consulta = "UPDATE Usuario"
            + "SET pNombre= '" + rh.Nombre + "', pApellido = '" + rh.PApellido + "', sApellido = '" + rh.SApellido + "', correo= '"
                + rh.Correo + "', nomUsuario= '" + rh.NomUsuario + "', contrasena = '" + rh.Contra + "', perfil= '" + rh.Perfil + "', rol = '" + rh.Rol + "' "
            + "WHERE cedula = " + rh.Cedula + ";";
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            bool resultado = reader.HasRows;
            return resultado;
        }

        public bool eliminaRH(EntidadRecursoH rh)
        {
            String consulta = "DELETE FROM Usuario WHERE cedula = "+rh.Cedula+"; ";
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            bool resultado = reader.HasRows;
            return resultado; 
        }

        public bool consultaRH(EntidadRecursoH rh)
        {
            String consulta = "SELECT ;
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            bool resultado = reader.HasRows;
            return resultado;
        }
    }
}