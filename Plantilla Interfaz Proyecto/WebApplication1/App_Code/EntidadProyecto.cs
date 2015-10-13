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
        string fecha;
        string nombreOf;
        string representante;
        string correoOf;
        string telefonoOf;

        public EntidadProyecto(Object[] datos)
        {
            nombre = datos[0].ToString();
            objetivo = datos[1].ToString();

        }

        
    }
}