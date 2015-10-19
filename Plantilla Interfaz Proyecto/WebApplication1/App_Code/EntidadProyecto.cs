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
            this.nombre = datos[0].ToString();
            this.objetivo = datos[1].ToString();
            //string state = datos[2].ToString();
            this.estado = datos[2].ToString();
            //this.estado = state[0];
            this.fecha = Convert.ToDateTime(datos[3]);
            this.nombreOf = datos[4].ToString();
            this.representante = datos[5].ToString();
            this.correoOf = datos[6].ToString();
            this.telefonoOf = Convert.ToInt32(datos[7]);
            this.lider = Convert.ToInt32(datos[8]);
            this.nombreLider = datos[9].ToString();
            this.tel2 = Convert.ToInt32(datos[10]);
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