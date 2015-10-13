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
            try
            {
                return controlBD.usuarioValido(nombreUsuario, contra);
            } catch(SqlException)
            {
                return false;
            }
            
        }

  public string getNombreCompleto(string nombreUsuario)
        {
            return controlBD.getNombreCompleto(nombreUsuario);
        }
        public bool insertaRH(int cedula, String nombre, String pApellido, String sApellido, String correo, String nomUsuario, String contra, char perfil, int idProy, String rol)
        {
            EntidadRecursoH insRH = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, correo, nomUsuario, contra, perfil, idProy, rol);
            try
            {
                return controlBD.insertaRH(insRH);
            } catch(SqlException ex)
            {
                return false;
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
                return false;
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
                return false;
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

        public bool asociaProyecto(int cedula, int idProy)
        {
            //return controlBD.asociaProyecto(int cedula, int idProy);
            return true;
        }

        public List<EntidadRecursoH> consultaRRHH()
        {
            try
            {
                return controlBD.consultaRRHH();
            } catch(SqlException ex)
            {
                return null;
            }
            
        }

    }
}