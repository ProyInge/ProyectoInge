using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.App_Code
{
    public class EntidadProyecto
    {
        string nombre;
        string objetivo;
        char estado;
        DateTime fecha;
        string nombreOf;
        string representante;
        string correoOf;
        int telefonoOf;

        public EntidadProyecto(string nombreP,string obj, char est, DateTime fechaP, string nomOf, string rep, string email, int telOf)
        {
            nombre = nombreP;
            objetivo = obj;
            estado = est;
            fecha = fechaP;
            nombreOf = nomOf;
            representante = rep;
            correoOf = email;
            telefonoOf = telOf;
        }

        public char getEstado()
        {
            return estado;
        }
    }
}