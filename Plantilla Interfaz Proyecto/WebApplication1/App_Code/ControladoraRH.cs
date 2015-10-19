﻿using System;
using System.Collections.Generic;
using System.Data;
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

        public int usuarioValido(string nombreUsuario, string contra)
        {
            try
            {
                return controlBD.usuarioValido(nombreUsuario, contra);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        public string getNombreCompleto(string nombreUsuario)
        {
            try
            {
                return controlBD.getNombreCompleto(nombreUsuario);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int insertaRH(int cedula, String nombre, String pApellido, String sApellido, String correo, String nomUsuario, String contra, char perfil, int idProy, String rol, int telefono1, int telefono2)
        {
            EntidadRecursoH insRH = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo, nomUsuario, contra, perfil, idProy, rol, telefono1, telefono2, -1);
            try
            {
                return controlBD.insertaRH(insRH);
            }
            catch (SqlException ex)
            {
                return ex.Number;
            }
        }

        public int modificaRH(int cedula, String nombre, String pApellido, String sApellido, String correo, String nomUsuario, String contra, char perfil, int idProy, String rol, int telefono1, int telefono2, int idrh)
        {
            EntidadRecursoH modRH = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo, nomUsuario, contra, perfil, idProy, rol, telefono1, telefono2, idrh);
            try
            {
                return controlBD.modificaRH(modRH);
            }
            catch (SqlException ex)
            {
                return ex.Number;
            }
        }

        public int eliminaRH(int cedula)
        {
            try
            {
                return controlBD.eliminaRH(cedula);
            }
            catch (SqlException ex)
            {
                return ex.Number;
            }
        }

        public EntidadRecursoH consultaRH(int cedula)
        {
            try
            {
                return controlBD.consultaRH(cedula);
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public EntidadRecursoH consultaRH(String nomUsuario)
        {
            try
            {
                return controlBD.consultaRH(nomUsuario);
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public DataTable consultaRRHH()
        {
            try
            {
                return controlBD.consultaRRHH();
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        public void cerrarSesion(string nombreUsuario)
        {
            controlBD.cerrarSesion(nombreUsuario);
        }

        public DataTable consultaMiembrosProy(int idProyecto)
        {
            return controlBD.consultaMiembrosProy(idProyecto);
        }

        public int getProyID(string nombreUsuario)
        {
            return controlBD.getProyID(nombreUsuario);
        }

        public string getPerfil(string usuario)
        {
            try
            {
                return controlBD.getPerfil(usuario);
            } catch (SqlException ex)
            {
                return ex.Message;
            }
        }

    }
}