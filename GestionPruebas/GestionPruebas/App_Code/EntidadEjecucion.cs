 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class EntidadEjecucion
    {
        private int id;
        private int responsable;
        private string nombreResponsable {get; set;}
        private DateTime fecha;
        private string incidencias;
        private int idDise { get; set; }
        private int idProy { get; set; }
         

        public EntidadEjecucion(Object[] args)
        {
        
        }

        public EntidadEjecucion(int id, int responsable, string nomResp, DateTime fecha, string incidencias, int idDise, int idProy)
        {
            this.id = id;
            this.responsable = responsable;
            this.nombreResponsable = nomResp;
            this.fecha = fecha;
            this.incidencias = incidencias;
            this.idDise = idDise;
            this.idProy = idProy;
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