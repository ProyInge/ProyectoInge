using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class EntidadDiseno
    {
        private int id;
        private string criterios;
        private string nivel;
        private string tecnica;
        private string ambiente;
        private string procedimiento;
        private DateTime fecha;
        private string proposito;
        private int responsable;
        private int idProy;

        public int Id
        {
            get{ return id; }
            set{ id = value; }
        }

        public string Criterios
        {
            get{ return criterios; }
            set{ criterios = value; }
        }

        public string Nivel
        {
            get{ return nivel; }
            set{ nivel = value; }
        }

        public string Tecnica
        {
            get{ return tecnica; }
            set{ tecnica = value; }
        }

        public string Ambiente
        {
            get{ return ambiente; }
            set{ ambiente = value; }
        }

        public string Procedimiento
        {
            get{ return procedimiento; }
            set{ procedimiento = value; }
        }

        public DateTime Fecha
        {
            get{ return fecha; }
            set{ fecha = value; }
        }

        public string Proposito
        {
            get{ return proposito; }
            set{ proposito = value; }
        }

        public int Responsable
        {
            get{ return responsable; }
            set{ responsable = value; }
        }

        public int IdProy
        {
            get{ return idProy; }
            set{ idProy = value; }
        }

        public EntidadDiseno()
        {
            this.Id = -1;
            this.Criterios = "";
            this.Nivel = "";
            this.Tecnica = "";
            this.Ambiente = "";
            this.Procedimiento = "";
            this.Fecha = DateTime.Today;
            this.Proposito = "";
            this.Responsable = -1;
            this.IdProy = -1;
        }

        public EntidadDiseno(int id, string criterios, string nivel, string tecnica, string ambiente, string procedimiento, DateTime fecha, string proposito, int responsable, int idProy)
        {
            this.Id = id;
            this.Criterios = criterios;
            this.Nivel = nivel;
            this.Tecnica = tecnica;
            this.Ambiente = ambiente;
            this.Procedimiento = procedimiento;
            this.Fecha = fecha;
            this.Proposito = proposito;
            this.Responsable = responsable;
            this.IdProy = idProy;
        }

        public EntidadDiseno(Object[] datos)
        {
            this.Id = (int)datos[0];
            this.Criterios = (string)datos[1];
            this.Nivel = (string)datos[2];
            this.Tecnica = (string)datos[3];
            this.Ambiente = (string)datos[4];
            this.Procedimiento = (string)datos[5];
            this.Fecha = (DateTime)datos[6];
            this.Proposito = (string)datos[7];
            this.Responsable = (int)datos[8];
            this.IdProy = (int)datos[9];
        }
    }
}