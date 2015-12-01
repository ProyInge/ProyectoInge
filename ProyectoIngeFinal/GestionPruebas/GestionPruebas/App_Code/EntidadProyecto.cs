using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class EntidadProyecto
    {
        string nombre;
        string objetivo;
        string estado;
        DateTime fecha;
        string nombreOf;
        string representante;
        string correoOf;
        int telefonoOf;
        int lider;
        string nombreLider;
        int tel2;

        /* Descripcion: Constructor de la Entidad de Proyecto
        * 
        * REQ: Object
        * 
        * RET: N/A
        */

        public EntidadProyecto(Object[] datos)
        {
            this.nombre = (string) datos[0];
            this.objetivo = (string)datos[1];
            this.estado = (string)datos[2];
            this.fecha = Convert.ToDateTime(datos[3]);
            this.nombreOf = (string)datos[4];
            this.representante = (string)datos[5];
            this.correoOf = (string)datos[6];
            this.telefonoOf = Convert.ToInt32(datos[7]);
            this.lider = Convert.ToInt32(datos[8]);
            this.nombreLider = (string)datos[9];
            this.tel2 = Convert.ToInt32(datos[10]);
        }

        public EntidadProyecto(string nombre, string objetivo, string estado, string fecha, string nombreOf, string representante, string correoOf,  string telefonoOf, string lider, string nombreLider, string tel2)
        {
            this.nombre = nombre;
            this.objetivo = objetivo;
            this.estado = estado;
            this.fecha = Convert.ToDateTime(fecha);
            this.nombreOf = nombreOf;
            this.representante = representante;
            this.correoOf = correoOf;
            this.telefonoOf = Convert.ToInt32(telefonoOf);
            this.lider = Convert.ToInt32(lider);
            this.nombreLider = nombreLider;
            this.tel2 = Convert.ToInt32(tel2);
        }

        /* Descripcion: Devuleve el estado del proyecto en la Entidad
        * 
        * REQ: N/A
        * 
        * RET: string
        */

        public string getEstado()
        {
            return estado;
        }

        /* Descripcion: Devuelve el nombre del proyecto en la entidad
        * 
        * REQ: N/A
        * 
        * RET: string
        */

        public string getNombre()
        {
            return nombre;
        }

        /* Descripcion: Devuelve el objetivo del proyecto en la entidad
       * 
       * REQ: N/A
       * 
       * RET: string
       */

        public string getObjetivo()
        {
            return objetivo;
        }

        /* Descripcion: Devuelve la fecha de asignacion del proyecto en la entidad
       * 
       * REQ: N/A
       * 
       * RET: DateTime
       */

        public DateTime getFecha()
        {
            return fecha.Date;
        }

        /* Descripcion: Devuelve el nombre de oficina del proyecto en la entidad
       * 
       * REQ: N/A
       * 
       * RET: string
       */

        public string getNomOf()
        {
            return nombreOf;
        }

        /* Descripcion: Devuelve el representante de oficina del proyecto en la entidad
        * 
        * REQ: N/A
        * 
        * RET: string
        */

        public string getRep()
        {
            return representante;
        }

        /* Descripcion: Devuelve el correo de la oficina del proyecto en la entidad
       * 
       * REQ: N/A
       * 
       * RET: string
       */

        public string getCorreoOf()
        {
            return correoOf;
        }

        /* Descripcion: Devuelve el telefono de la oficina del proyecto en la entidad
       * 
       * REQ: N/A
       * 
       * RET: string
       */

        public int getTelOf()
        {
            return telefonoOf;
        }

        /* Descripcion: Devuelve el lider del proyecto en la entidad
       * 
       * REQ: N/A
       * 
       * RET: string
       */

        public int getLider()
        {
            return lider;
        }

        /* Descripcion: Devuelve el nombre del lider del proyecto en la entidad
       * 
       * REQ: N/A
       * 
       * RET: string
       */

        public string getNombreLider()
        {
            return nombreLider;
        }

        /* Descripcion: Devuelve el telefono2 de la oficina del proyecto en la entidad
       * 
       * REQ: N/A
       * 
       * RET: string
       */

        public int getTelOf2()
        {
            return tel2;
        }
    }
}