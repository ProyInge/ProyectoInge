﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{

    [Serializable]
    public class EntidadRecursoH
    {
        int idRH;
        int cedula;
        string nombre;
        string pApellido;
        string sApellido;
        string correo;
        string nomUsuario;
        string contra;
        int telefono1;
        int telefono2;
        char perfil;
        int idProy;
        string rol;
        DateTime fechaModif;

        /*
         * Setters y Getters de la clase. Nótese que se usa la notación de propiedad que posee C#
        */
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
        public int Telefono1
        {
            get{ return telefono1; }
            set{ telefono1 = value; }
        }
        public int Telefono2
        {
            get{ return telefono2; }
            set{ telefono2 = value; }
        }
        public int IdRH
        {
            get{ return idRH; }
            set{ idRH = value; }
        }

        public DateTime FechaModif
        {
            get{ return fechaModif; }
            set{ fechaModif = value; }
        }


        /*
         * Descripcion: Constructor por defecto.
         * No recibe nada.
         * Devuelve la entidadRH construida
         */
        public EntidadRecursoH()
        {
            this.Cedula = -1;
            this.Nombre = "";
            this.PApellido = "";
            this.SApellido = "";
            this.Correo = "";
            this.NomUsuario = "";
            this.Contra = "";
            this.Perfil = ' ';
            this.IdProy = -1;
            this.Rol = "";
            this.Telefono1 = -1;
            this.Telefono2 = -1;
            this.idRH = -1;
            this.FechaModif = DateTime.Now;
        }

        /*
         * Descripcion: Constructor completo de la clase.
         * Recibe: Todos los atributos de un RH.
         * Devuelve la entidadRH construida
         */
        public EntidadRecursoH(int cedula, string nombre, string pApellido, string sApellido, string correo, string nomUsuario, string contra, char perfil, int idProy, string rol, int telefono1, int telefono2, int idrh, DateTime fecha)
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
            this.Telefono1 = telefono1;
            this.Telefono2 = telefono2;
            this.IdRH = idrh;
            this.FechaModif = fecha;
        }

        /*
         * Descripcion: Constructor abreviado de la clase.
         * Recibe: Cédula, nombre, primer apellido, segundo apellido y rol
         * Devuelve la entidadRH construida
         */
        public EntidadRecursoH(int cedula, string nombre, string pApellido, string sApellido,  string rol)
        {
            this.Cedula = cedula;
            this.Nombre = nombre;
            this.PApellido = pApellido;
            this.SApellido = sApellido;
            this.Correo = "";
            this.NomUsuario = "";
            this.Contra = "";
            this.Perfil = ' ';
            this.IdProy = -1;
            this.Rol = rol;
            this.Telefono1 = -1;
            this.Telefono2 = -1;
            this.idRH = -1;
            this.FechaModif = DateTime.Now;
        }

        /*
         * Descripcion: Constructor de copia.
         * Recibe: La entidad a copiar
         * Devuelve la entidadRH construida
         */
        public EntidadRecursoH(EntidadRecursoH e)
        {
            this.idRH = e.IdRH;
            this.Cedula = e.cedula;
            this.Nombre = e.nombre;
            this.PApellido = e.pApellido;
            this.SApellido = e.sApellido;
            this.Correo = e.correo;
            this.NomUsuario = e.NomUsuario;
            this.Contra = e.Contra;
            this.Perfil = e.Perfil;
            this.IdProy = e.IdProy;
            this.Rol = e.rol;
            this.Telefono1 = e.Telefono1;
            this.Telefono2 = e.Telefono2;
            this.FechaModif = e.FechaModif;
        }

        /*
         * Descripcion: Constructor con vector de objetos.
         * Recibe: Un vector de objetos con la información a utilizar
         * Devuelve la entidadRH construida
         */
        public EntidadRecursoH(Object[] data)
        {
            this.Cedula = (int) data[0];
            this.Nombre = (string) data[1];
            this.PApellido = (string) data[2];
            this.SApellido =(string) data[3];
            this.Correo = (string) data[4];
            this.NomUsuario = (string) data[5];
            this.Contra = (string) data[6];
            this.Perfil = (char) data[7];
            this.IdProy = (int) data[8];
            this.Rol = (string) data[9];
            this.Telefono1 = (int) data[10];
            this.Telefono2 = (int)data[11];
            this.IdRH = (int)data[12];
            this.FechaModif = (DateTime)data[13];
        }

    }
}