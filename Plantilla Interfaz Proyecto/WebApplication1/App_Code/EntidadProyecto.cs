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

        public EntidadProyecto(Object[] datos)
        {
            this.nombre = datos[0].ToString();
            this.objetivo = datos[1].ToString();
            string state = datos[2].ToString();
            this.estado = state[0];
            this.fecha = Convert.ToDateTime(datos[3]);
            this.nombreOf = datos[4].ToString();
            this.representante = datos[5].ToString();
            this.correoOf = datos[6].ToString();
            this.telefonoOf = Convert.ToInt32(datos[7].ToString());
        }

        public char getEstado()
        {
            return estado;
        }

        public string getNombre()
        {
            return nombre;
        }

        public string getObjetivo()
        {
            return objetivo;
        }

        public DateTime getFecha()
        {
            return fecha.Date;
        }

        public string getNomOf()
        {
            return nombreOf;
        }

        public string getRep()
        {
            return representante;
        }

        public string getCorreoOf()
        {
            return correoOf;
        }

        public int getTelOf()
        {
            return telefonoOf;
        }
    }
}