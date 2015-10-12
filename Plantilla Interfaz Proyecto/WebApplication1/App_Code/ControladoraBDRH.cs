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
            string consulta = "UPDATE Usuario "
            + " SET pNombre= '" + rh.Nombre + "', pApellido = '" + rh.PApellido + "', sApellido = '" + rh.SApellido + "', correo= '" + rh.Correo 
            + "', nomUsuario= '" + rh.NomUsuario + "', contrasena = '" + rh.Contra + "', perfil= '" + rh.Perfil + "', rol = '" + rh.Rol + "' "
            + " WHERE cedula = " + rh.Cedula + ";";
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            bool resultado = reader.HasRows;
            return resultado;
        }

        public bool eliminaRH(int cedula)
        {
            String consulta = "DELETE FROM Usuario WHERE cedula = " + cedula + "; ";
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            bool resultado = reader.HasRows;
            return resultado;
        }

        public EntidadRecursoH consultaRH(int cedula)
        {
            //String consulta = "SELECT cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, rol"
            String consulta = "SELECT * "
                + " FROM Usuario WHERE cedula =" + cedula + "; ";
            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            EntidadRecursoH rh = null;
            try
            {
                reader.Read();
                rh = new EntidadRecursoH(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetChar(7), reader.GetInt32(8), reader.GetString(9));
            }
            catch (SqlException ex)
            {
                string mensajeError = ex.ToString();
                /*MessageBox.Show(mensajeError);*/
            }
            return rh;
        }
    }
}