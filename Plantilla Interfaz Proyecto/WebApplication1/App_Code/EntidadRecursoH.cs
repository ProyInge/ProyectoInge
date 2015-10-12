using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.App_Code
{
    public class EntidadRecursoH
    {
        int cedula;
        String nombre;
        String pApellido;
        String sApellido;
        String correo;
        String nomUsuario;
        String contra;
        char perfil;
        int idProy;
        String rol;

        public int Cedula
        {
            get { return cedula; }
            set { cedula = value; }
        }

        public string Nombre
        {
            get{ return nombre; }
            set{ nombre = value; }
        }

        public string PApellido
        {
            get{ return pApellido; }
            set{ pApellido = value; }
        }

        public string SApellido
        {
            get{ return sApellido; }
            set{ sApellido = value; }
        }

        public string Correo
        {
            get{ return correo; }
            set{ correo = value; }
        }

        public string NomUsuario
        {
            get{ return nomUsuario; }
            set{ nomUsuario = value; }
        }

        public string Contra
        {
            get{ return contra; }
            set{ contra = value; }
        }

        public char Perfil
        {
            get{ return perfil; }
            set{ perfil = value; }
        }

        public int IdProy
        {
            get{ return idProy; }
            set{ idProy = value; }
        }

        public string Rol
        {
            get{ return rol; }
            set{ rol = value; }
        }

        public EntidadRecursoH(int cedula, String nombre, String pApellido, String sApellido, String correo, String nomUsuario, String contra, char perfil, int idProy, String rol)
        {
            this.Cedula = cedula;
            this.Nombre = nombre;
            this.PApellido = pApellido;
            this.SApellido = sApellido;
            this.Correo = correo;
            this.NomUsuario = nomUsuario;
            this.Contra = contra;
            this.Perfil = perfil;
            this.IdProy = idProy;
            this.Rol = rol;
        }

        public EntidadRecursoH(Object[] data)
        {
            this.Cedula = (int) data[0];
            this.Nombre = (String) data[1];
            this.PApellido = (String) data[2];
            this.SApellido =(String) data[3];
            this.Correo = (String) data[4];
            this.NomUsuario = (String) data[5];
            this.Contra = (String) data[6];
            this.Perfil = (char) data[7];
            this.IdProy = (int) data[8];
            this.Rol = (String) data[9];
        }

    }
}