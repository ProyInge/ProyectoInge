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
            string consulta = "EXEC iniciarSesion @nombre='" + nombreUsuario.Trim() + "', @contra='" + contra.Trim() + "';";
            bool resultado = false;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                resultado = reader.HasRows;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            
            return resultado;
        }
        
        public string getNombreCompleto(string nombreUsuario)
        {
            string consulta = "SELECT CONCAT(pNombre, ' ', pApellido, ' ', sApellido) FROM Usuario WHERE nomUsuario = '" + nombreUsuario.Trim() + "';";
            string nombre = "";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if(reader.HasRows)
                {
                    reader.Read();
                    nombre = reader.GetString(0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return nombre;
        }

        public void cerrarSesion(string nombreUsuario)
        {
            string consulta = "EXEC cerrarSesion @nombre = '"+nombreUsuario+"';";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public bool insertaRH(EntidadRecursoH rh)
        {
            string consulta = "INSERT INTO Usuario (cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, rol)"
            + "values (" + rh.Cedula + ",'" + rh.Nombre + "', '" + rh.PApellido + "', '" + rh.SApellido + "', '" + rh.Correo + "', '" + rh.NomUsuario + "', '"
            + rh.Contra + "', '" + rh.Perfil + "', '" + rh.Rol + "');";
            bool resultado = false;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if(reader.RecordsAffected>0)
                {
                    resultado = true;
                } else
                {
                    resultado = false;
                }
                
            } catch(SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }

        public bool modificaRH(EntidadRecursoH rh)
        {
            string consulta = "UPDATE Usuario "
            + " SET pNombre= '" + rh.Nombre + "', pApellido = '" + rh.PApellido + "', sApellido = '" + rh.SApellido + "', correo= '" + rh.Correo 
            + "', nomUsuario= '" + rh.NomUsuario + "', contrasena = '" + rh.Contra + "', perfil= '" + rh.Perfil + "', rol = '" + rh.Rol + "' "
            + " WHERE cedula = " + rh.Cedula + ";";
            bool resultado = false;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                resultado = reader.HasRows;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            
            
            return resultado;
        }

        public bool eliminaRH(int cedula)
        {
            String consulta = "DELETE FROM Usuario WHERE cedula = " + cedula + "; ";
            bool resultado = false;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                resultado = reader.HasRows;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }

        public EntidadRecursoH consultaRH(int cedula)
        {
            //String consulta = "SELECT cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, rol"
            String consulta = "SELECT * "
                + " FROM Usuario WHERE cedula =" + cedula + "; ";
            EntidadRecursoH rh = null;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                try
                {
                    reader.Read();

                    rh = new EntidadRecursoH(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                        reader.GetString(5), reader.GetString(6), reader.GetString(7).ElementAt(0), reader.GetInt32(8), reader.GetString(9));
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return rh;
        }

        public List<EntidadRecursoH> consultaRRHH()
        {
            String consulta = "SELECT cedula, pNombre, pApellido, sApellido"
            //String consulta = "SELECT * "
                + " FROM Usuario; ";
            List<EntidadRecursoH> rhL = new List<EntidadRecursoH>();
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                try
                {
                    while(reader.Read()) {
                        EntidadRecursoH rh = new EntidadRecursoH(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "",
                            "", "", ' ', -1, "");
                        rhL.Add(rh);
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return rhL;
        }
    }
}