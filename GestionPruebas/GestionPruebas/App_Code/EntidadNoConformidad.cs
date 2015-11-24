using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace GestionPruebas
{
    public class EntidadNoConformidad
    {
        private string idEjecucion;
        private string tipo;
        private string descripcion;
        private string justificacion;
        private string estado;
        private byte[] imagen;

        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public EntidadNoConformidad(Object[] args){
            idEjecucion = args[0].ToString();
            tipo = args[1].ToString();
            descripcion = args[2].ToString();
            justificacion = args[3].ToString();
            estado = args[4].ToString();
            imagen = ObjectToByteArray(args[5]);
        }

        public string IdEjecucion
        {
            get { return idEjecucion; }
            set { idEjecucion = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
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

    }
}