using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class EntidadEjecucion
    {
        private string tipoNC;
        private string idCaso;
        private string descripcion;
        private string justificacion;
        private string estado;
        private byte[] imagen;
        private int responsable;
        private DateTime fecha;
        private string incidencias;
         

        public EntidadEjecucion(Object[] args)
        {
            tipoNC = args[0].ToString();
            idCaso = args[1].ToString();
            descripcion = args[2].ToString();
            justificacion = args[3].ToString();
            estado = args[4].ToString();
            imagen = (byte[])args[5];
            responsable = Convert.ToInt32(args[6]);
            fecha = Convert.ToDateTime(args[7]);
            incidencias = args[8].ToString();
        }

        public string TipoNC
        {
            get { return tipoNC; }
            set { tipoNC = value; }
        }

        public string IdCaso
        {
            get { return idCaso; }
            set { idCaso = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string Justificacion
        {
            get { return justificacion; }
            set { justificacion = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public byte[] Imagen
        {
            get { return imagen; }
            set { imagen = value; }
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