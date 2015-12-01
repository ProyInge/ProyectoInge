using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class EntidadCaso
    {

        public String Id
        {
            get;
            set;
        }

        public String Proposito
        {
            get;
            set;
        }

        public String Entrada
        {
            get;
            set;
        }


        public String ResultadoEsperado
        {
            get;
            set;
        }

        public String FlujoCentral
        {
            get;
            set;
        }

        public int IdDise
        {
            get;
            set;
        }

        public int IdProy
        {
            get;
            set;
        }


        public EntidadCaso()
        {
            this.Id = "";
            this.Proposito = "";
            this.Entrada = "";
            this.ResultadoEsperado = "";
            this.FlujoCentral = "";
            this.IdDise = -1;
            this.IdProy = -1;
        }

        public EntidadCaso(string id, String proposito, String entrada, String resultadoEsperado, String flujoCentral, int idDise, int IdProy)
        {
            this.Id = id;
            this.Proposito = proposito;
            this.Entrada = entrada;
            this.ResultadoEsperado = resultadoEsperado;
            this.FlujoCentral = flujoCentral;
            this.IdDise = idDise;
            this.IdProy = IdProy;
        }

        public EntidadCaso(Object[] datos)
        {
            this.Id = (string)datos[0];
            this.Proposito = (string)datos[1];
            this.Entrada = (string)datos[2];
            this.ResultadoEsperado = (string)datos[3];
            this.FlujoCentral = (string)datos[4];
            this.IdDise = (int)datos[5];
            this.IdProy = (int)datos[6];
        }
    }
}