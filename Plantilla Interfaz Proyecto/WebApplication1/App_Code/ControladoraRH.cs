﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.App_Code
{
    public class ControladoraRH
    {
        private AccesoBaseDatos baseDatos;

        public ControladoraRH()
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
    }
}