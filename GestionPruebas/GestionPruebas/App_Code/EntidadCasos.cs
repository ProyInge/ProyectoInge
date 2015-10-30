using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class EntidadCasos
    {

        private int id;
        private String proposito;
        private String tipoEntrada;
        private String nombreEntrada;
        private String resultadoEsperado;
        private String flujoCentral;
        private int idDise;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Proposito
        {
            get { return proposito; }
            set { proposito = value; }
        }

        public String TipoEntrada
        {
            get { return tipoEntrada; }
            set { tipoEntrada = value; }
        }

        public String NombreEntrada
        {
            get { return nombreEntrada; }
            set { nombreEntrada = value; }
        }

        public String ResultadoEsperado
        {
            get { return resultadoEsperado; }
            set { resultadoEsperado = value; }
        }

        public String FlujoCentral
        {
            get { return flujoCentral; }
            set { flujoCentral = value; }
        }

        public int IdDise
        {
            get { return idDise; }
            set { idDise = value; }
        }


        public EntidadCasos()
        {
            this.Id = -1;
            this.Proposito = "";
            this.NombreEntrada = "";
            this.TipoEntrada = "";
            this.ResultadoEsperado = "";
            this.FlujoCentral = "";
            this.IdDise = -1;
        }

        public EntidadCasos(int id, String proposito, String tipoEntrada, String nombreEntrada, String resultadoEsperado, String flujoCentral, int idDise)
        {
            this.Id = id;
            this.Proposito = proposito;
            this.NombreEntrada = nombreEntrada;
            this.TipoEntrada = tipoEntrada;
            this.ResultadoEsperado = resultadoEsperado;
            this.FlujoCentral = flujoCentral;
            this.IdDise = idDise;
        }

        public EntidadCasos(Object[] datos)
        {
            this.Id = (int)datos[0];
            this.Proposito = (string)datos[1];
            this.TipoEntrada = (string)datos[2];
            this.NombreEntrada = (string)datos[3];
            this.ResultadoEsperado = (string)datos[4];
            this.FlujoCentral = (string)datos[5];
            this.IdDise = (int)datos[6];

        }
    }
}