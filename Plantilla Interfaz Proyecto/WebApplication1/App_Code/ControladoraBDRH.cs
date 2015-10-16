using System;
using System.Collections.Generic;
using System.Data;
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

        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        public static int SafeGetInt32(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            else
                return -1;
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
            bool resultado2 =false;
            string modificaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                + " values (" + rh.Cedula + ", " + rh.Telefono1+ ");";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(modificaTel);
                if (reader.RecordsAffected > 0)
                {
                    resultado2 = true;
                }
                else
                {
                    resultado2 = false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado|resultado2;
        }

        public bool modificaRH(EntidadRecursoH rh)
        {
            string consulta = "UPDATE Usuario "
            + " SET pNombre= '" + rh.Nombre + "', pApellido = '" + rh.PApellido + "', sApellido = '" + rh.SApellido + "', correo= '" + rh.Correo 
            //+ "', nomUsuario= '" + rh.NomUsuario
            + "', contrasena = '" + rh.Contra + "', perfil= '" + rh.Perfil + "', rol = '" + rh.Rol + "' "
            + " WHERE cedula = " + rh.Cedula + ";";
            bool resultado1 = false;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.RecordsAffected > 0)
                {
                    resultado1 = true;
                }
                else
                {
                    resultado1 = false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            bool resultado2 = false;
            string modificaTel = " UPDATE TelefonoUsuario "
                + " SET numero = "+rh.Telefono1
                + " WHERE cedula = "+rh.Cedula+"; ";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(modificaTel);
                if (reader.RecordsAffected > 0)
                {
                    resultado2 = true;
                }
                else
                {
                    resultado2 = false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            
            return resultado1|resultado2;
        }

        public bool eliminaRH(int cedula)
        {
            String consulta = "DELETE FROM Usuario WHERE cedula = " + cedula + "; ";
            bool resultado = false;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.RecordsAffected > 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }

        public EntidadRecursoH consultaRH(int cedula)
        {
            String consultaU = "SELECT cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, idProy, rol"
            //String consultaU = "SELECT * "
                + " FROM Usuario u WHERE u.cedula =" + cedula + "; ";
            String consultaT = "SELECT numero "
                + " FROM telefonoUsuario t WHERE t.cedula =" + cedula + "; ";
            EntidadRecursoH rh = null;
            String nombre="";
            String pApellido="";
            String sApellido="";
            String correo="";
            String usuario="";
            String contrasena="";
            char perfil=' ';
            int idProy =-1;
            String rol="";
            int telefono1 = 0;
            int telefono2 = 0;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consultaU);
                try
                {
                    reader.Read();
                    nombre = SafeGetString(reader, 1);
                    pApellido = SafeGetString(reader, 2);
                    sApellido = SafeGetString(reader, 3);
                    correo = SafeGetString(reader, 4);
                    usuario = SafeGetString(reader, 5);
                    contrasena = SafeGetString(reader, 6);
                    perfil = SafeGetString(reader, 7).ElementAt(0);
                    idProy = SafeGetInt32(reader, 8);
                    rol = SafeGetString(reader, 9);

                }
                catch (SqlException ex)
                {
                    String a = ex.ToString();
                }

                SqlDataReader readerT = baseDatos.ejecutarConsulta(consultaT);
                try
                {
                    if (readerT.Read())
                    {
                        telefono1 = reader.GetInt32(0);
                    }
                    if(readerT.Read())
                    {
                        telefono2 = reader.GetInt32(0);
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                rh = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo,
                        usuario, contrasena, perfil, idProy, rol, telefono1, telefono2);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return rh;
        }

        public DataTable consultaRRHH()
        {
            String consulta = "SELECT cedula AS 'Cedula', pNombre AS 'Nombre', pApellido AS 'Primer Apellido', sApellido AS 'Segundo Apellido'"
            //String consulta = "SELECT * "
                + " FROM Usuario; ";
            DataTable data = new DataTable();
            try
            {
                data = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;
        }
    }
}