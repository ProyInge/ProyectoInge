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
        private string nombreResponsable;
        private DateTime fecha;
        private string incidencias;
        private int idDise;
        private string idProy;

        public EntidadEjecucion()
        {

        }

        public EntidadEjecucion(Object[] datos)
        {
            //id = Convert.ToInt32(datos[0]);
            fecha = Convert.ToDateTime(datos[0]);
            Incidencias = datos[1].ToString();
            responsable = Convert.ToInt32(datos[2]);                     
            idDise = Convert.ToInt32(datos[3]);
            idProy = datos[4].ToString();
        }
        public EntidadEjecucion(Object[] datos, int val)
        {
            id = Convert.ToInt32(datos[0]);
            fecha = Convert.ToDateTime(datos[1]);
            Incidencias = datos[2].ToString();
            responsable = Convert.ToInt32(datos[3]);
            idDise = Convert.ToInt32(datos[4]);
            idProy = datos[5].ToString();
        }

        public EntidadEjecucion(int id, int responsable, string nomResp, DateTime fecha, string incidencias, int idDise, string idProy)
        {
            this.id = id;
            this.responsable = responsable;
            this.nombreResponsable = nomResp;
            this.fecha = fecha;
            this.incidencias = incidencias;
            this.idDise = idDise;
            this.idProy = idProy;
        }

        public string NombreResponsable
        {
            get { return nombreResponsable; }
            set { nombreResponsable = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
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

        public int IdDise
        {
            get { return idDise; }
            set { idDise = value; }
        }

        public string IdProy
        {
            get { return idProy; }
            set { idProy = value; }
        }

    }
}