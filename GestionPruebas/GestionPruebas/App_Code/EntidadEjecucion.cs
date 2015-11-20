 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class EntidadEjecucion
    {
        private int id;
        private string idCaso;
        private int responsable;
        private DateTime fecha;
        private string incidencias;
         

        public EntidadEjecucion(Object[] args)
        {
            id = Convert.ToInt32(args[0]);
            idCaso = args[1].ToString();
            responsable = Convert.ToInt32(args[2]);
            fecha = Convert.ToDateTime(args[3]);
            incidencias = args[4].ToString();
        }

        public string IdCaso
        {
            get { return idCaso; }
            set { idCaso = value; }
        }

        public int Responsable
        {
            get { return responsable; }
            set { responsable = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Incidencias
        {
            get { return incidencias; }
            set { incidencias = value; }
        }

    }
}