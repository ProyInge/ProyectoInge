﻿using System;
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

        public int usuarioValido(string nombreUsuario, string contra)
        {
            int res = -1;
            string consulta = "EXEC iniciarSesion @nombre='" + nombreUsuario.Trim() + "', @contra='" + contra.Trim() + "';";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.HasRows)
                {
                    reader.Read();
                    res = reader.GetInt32(0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return res;
        }

        public string getNombreCompleto(string nombreUsuario)
        {
            string consulta = "SELECT CONCAT(pNombre, ' ', pApellido, ' ', sApellido) FROM Usuario WHERE nomUsuario = '" + nombreUsuario.Trim() + "';";
            string nombre = "";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.HasRows)
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
            string consulta = "EXEC cerrarSesion @nombre = '" + nombreUsuario + "';";
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

        public int insertaRH(EntidadRecursoH rh)
        {
            string consulta = "INSERT INTO Usuario (cedula, pNombre, pApellido, sApellido, correo, nomUsuario, contrasena, perfil, rol)"
            + "values (" + rh.Cedula + ",'" + rh.Nombre + "', '" + rh.PApellido + "', '" + rh.SApellido + "', '" + rh.Correo + "', '" + rh.NomUsuario + "', '"
            + rh.Contra + "', '" + rh.Perfil + "', '" + rh.Rol + "');";
            int resultado = -1;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                /*Si se hizo bien el insertar de usuario se hace el de telefono, de otro modo no se hace nada*/
                if (reader.RecordsAffected > 0)
                {                   
                    string consultaTel = "";
                    /*Si no hay telefonos que insertar se finaliza la insercion a la base*/
                    if (rh.Telefono1 == -1 && rh.Telefono2 == -1)
                    {
                        resultado = 0;
                    }
                    else
                    {
                        /*Se revisan los casos de cuales telefonos hay que insertar*/
                        if (rh.Telefono2 == -1)
                        {
                            consultaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                            + " values (" + rh.Cedula + ", " + rh.Telefono1 + ");";
                        }
                        else if (rh.Telefono1 == -1)
                        {
                            consultaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                            + " values (" + rh.Cedula + ", " + rh.Telefono2 + ");";
                        }
                        else
                        {
                            consultaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                            + " values (" + rh.Cedula + ", " + rh.Telefono1 + "), (" + rh.Cedula + ", " + rh.Telefono2 + "); ";
                        }

                        try
                        {
                            reader = baseDatos.ejecutarConsulta(consultaTel);
                            if (reader.RecordsAffected > 0)
                            {
                                resultado = 0;
                            }
                            else
                            {
                                /*resultado = -2 indica que hubo error al insertar el o los telefonos*/
                                resultado = -2;
                            }
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }
                    }

                }
                //Si no se insertó nada se devuelve -1
                else
                {
                    resultado = -1;
                }

            }
            catch (SqlException ex)
            {
                string a = ex.ToString();
                resultado = ex.Number;
            }
            
            return resultado;
        }

        public int modificaRH(EntidadRecursoH rh)
        {
            string consulta = "UPDATE Usuario "
            + " SET pNombre= '" + rh.Nombre + "', pApellido = '" + rh.PApellido + "', sApellido = '" + rh.SApellido + "', correo= '" + rh.Correo
            + "', nomUsuario= '" + rh.NomUsuario
            + "', contrasena = '" + rh.Contra + "', perfil= '" + rh.Perfil + "', rol = '" + rh.Rol + "' "
            + " WHERE cedula = " + rh.Cedula + ";";
            int resultado = -1;

            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.RecordsAffected > 0)
                {
                    //si se realiza la modificacion correcta del usuario se hace el cambio de los telefonos

                    string borraTel = " DELETE FROM  TelefonoUsuario WHERE cedula = " + rh.Cedula + "; ";
                    string insertaTel = "";
                    /*Se revisan los casos para ver cual(es) telefono(s) insertar*/
                    if (rh.Telefono1 != -1 && rh.Telefono2 == -1)
                    {
                        insertaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                        + " values (" + rh.Cedula + ", " + rh.Telefono1 + ");";
                    }
                    else if (rh.Telefono1 == -1 && rh.Telefono2 != -1)
                    {
                        insertaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                        + " values (" + rh.Cedula + ", " + rh.Telefono2 + ");";
                    }
                    else if (rh.Telefono2 != -1 && rh.Telefono2 != -1)
                    {
                        insertaTel = " INSERT INTO TelefonoUsuario (cedula, numero) "
                        + " values (" + rh.Cedula + ", " + rh.Telefono1 + "), (" + rh.Cedula + ", " + rh.Telefono2 + "); ";
                    }

                    try
                    {
                        //se borran todos los numeros relacionados a la persona, utlizando su numero de cedula
                        reader = baseDatos.ejecutarConsulta(borraTel);
                        if (insertaTel != "")
                        {
                            try
                            {
                                //se insertan los nuevos numeros 
                                reader = baseDatos.ejecutarConsulta(insertaTel);
                                if (reader.RecordsAffected > 0)
                                {
                                    //se devuelve 0 si tanto el usuario como los telefonos se modificaron correctamente
                                    resultado = 0;
                                }
                                else
                                {
                                    //Si hubo un error al modificar los telefonos
                                    resultado = -2;
                                }
                            }
                            catch (SqlException ex)
                            {
                                throw ex;
                            }
                        }
                    }

                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                }
                //si no se modificó el usuario correctamente se devuelve -1
                else
                {
                    resultado = -1;
                }

            }
            catch (SqlException ex)
            {
                resultado = ex.Number;
            }

            return resultado;
        }

        public int eliminaRH(int cedula)
        {
            String consulta = "DELETE FROM Usuario WHERE cedula = " + cedula + "; ";
            int resultado = -1;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.RecordsAffected > 0)
                {
                    resultado = 0;
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
            String nombre = "";
            String pApellido = "";
            String sApellido = "";
            String correo = "";
            String usuario = "";
            String contrasena = "";
            char perfil = ' ';
            int idProy = -1;
            String rol = "";
            int telefono1 = -1;
            int telefono2 = -1;
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
                    throw ex;
                }

                SqlDataReader readerT = baseDatos.ejecutarConsulta(consultaT);
                try
                {
                    if (readerT.Read())
                    {
                        telefono1 = SafeGetInt32(readerT, 0);
                    }
                    if (readerT.Read())
                    {
                        telefono2 = SafeGetInt32(readerT, 0);
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

        public DataTable consultaMiembrosProy(int idProy)
        {
            String consulta = "SELECT pNombre AS 'Nombre', pApellido AS 'Primer Apellido', sApellido AS 'Segundo Apellido', correo AS 'E-mail' FROM Usuario WHERE idProy = "+idProy+";";
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

        public int getProyID(string nombreUsuario)
        {
            int idProy;
            SqlDataReader reader = baseDatos.ejecutarConsulta("SELECT idProy FROM Usuario WHERE nomUsuario = '"+nombreUsuario+"';");
            reader.Read();    
            idProy = reader.GetInt32(0);
            return idProy;
            
        }

        public string getPerfil(string usuario)
        {
            string resultado = "";
            try { 
                string consulta = "Select perfil from Usuario where nomUsuario = '" + usuario + "'";
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.Read())
                {
                    resultado = reader.GetString(0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }
    }
}