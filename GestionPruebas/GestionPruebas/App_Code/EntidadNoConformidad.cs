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
        private int id;
        private int idEjecu;
        private int idDise { get; set; }
        private string idCaso { get; set; }
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

        public EntidadNoConformidad(int id, int idEjecu, int idDise, string idCaso, string tipo, string descripcion, string justificacion, string estado, byte[] imagen)
        {
            this.id = id;
            this.idEjecu = idEjecu;
            this.idDise = idDise;
            this.idCaso = idCaso;
            this.tipo = tipo;
            this.descripcion = descripcion;
            this.justificacion = justificacion;
            this.estado = estado;
            this.imagen = imagen;
        }

        public EntidadNoConformidad(Object[] datos)
        {           
            idDise = Convert.ToInt32(datos[0]);
            idCaso = datos[1].ToString();
            tipo = datos[2].ToString();
            descripcion = datos[3].ToString();
            justificacion = datos[4].ToString();
            estado = datos[5].ToString();
            imagen = ObjectToByteArray(datos[6]);
        }
        public EntidadNoConformidad(Object[] datos, int val)
        {
            id = Convert.ToInt32(datos[0]);
            idEjecu= Convert.ToInt32(datos[1]);
            idDise = Convert.ToInt32(datos[2]);
            idCaso = datos[3].ToString();
            tipo = datos[4].ToString();
            descripcion = datos[5].ToString();
            justificacion = datos[6].ToString();
            estado = datos[7].ToString();
            //imagen = ObjectToByteArray(datos[8]);
        }

            
       



        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IdEjecu
        {
            get { return idEjecu; }
            set { idEjecu = value; }
        }


        public int IdDise {
            get { return idDise; }
            set { idDise = value; }
        }
        public string IdCaso
        {
            get { return idCaso; }
            set { idCaso = value; }
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