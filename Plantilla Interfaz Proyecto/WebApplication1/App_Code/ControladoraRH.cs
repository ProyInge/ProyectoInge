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

        public bool usuarioValido(string nombreUsuario, string contra)
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
        public bool insertaRH(int cedula, String nombre, String pApellido, String sApellido, String correo, String nomUsuario, String contra, char perfil, int idProy, String rol)
        {
            EntidadRecursoH insRH = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo, nomUsuario, contra, perfil, idProy, rol);
            try
            {
                return controlBD.insertaRH(insRH);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public bool modificaRH(int cedula, String nombre, String pApellido, String sApellido, String correo, String nomUsuario, String contra, char perfil, int idProy, String rol)
        {
            EntidadRecursoH modRH = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo, nomUsuario, contra, perfil, idProy, rol);
            try
            {
                return controlBD.modificaRH(modRH);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public bool eliminaRH(int cedula)
        {
            try
            {
                return controlBD.eliminaRH(cedula);
            }
            catch (SqlException ex)
            {
                throw ex;
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
                throw ex;
            }
        }

        public bool asociaProyecto(int cedula, int idProy)
        {
            //return controlBD.asociaProyecto(int cedula, int idProy);
            return true;
        }

        public DataTable consultaRRHH()
        {
            try
            {
                return controlBD.consultaRRHH();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        public void cerrarSesion(string nombreUsuario)
        {
            controlBD.cerrarSesion(nombreUsuario);
        }

    }
}