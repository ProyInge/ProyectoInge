using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class EntidadCaso
    {

        private String id;
        private String proposito;
        private String entrada;
        private String resultadoEsperado;
        private String flujoCentral;
        private int idDise;

        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Proposito
        {
            get { return proposito; }
            set { proposito = value; }
        }

        public String Entrada
        {
            get { return entrada; }
            set { entrada = value; }
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


        public EntidadCaso()
        {
            this.Id = "";
            this.Proposito = "";
            this.Entrada = "";
            this.ResultadoEsperado = "";
            this.FlujoCentral = "";
            this.IdDise = -1;
        }

        public EntidadCaso(string id, String proposito, String entrada, String resultadoEsperado, String flujoCentral, int idDise)
        {
            this.Id = id;
            this.Proposito = proposito;
            this.Entrada = entrada;
            this.ResultadoEsperado = resultadoEsperado;
            this.FlujoCentral = flujoCentral;
            this.IdDise = idDise;
        }

        public EntidadCaso(Object[] datos)
        {
            this.Id = (string)datos[0];
            this.Proposito = (string)datos[1];
            this.Entrada = (string)datos[2];
            this.ResultadoEsperado = (string)datos[3];
            this.FlujoCentral = (string)datos[4];
            this.IdDise = (int)datos[5];

        }
    }
}