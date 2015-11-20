using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionPruebas.App_Code;
using System.Data;
using System.Drawing;
using System.Diagnostics;

namespace GestionPruebas
{
    public partial class Reportes : System.Web.UI.Page
    {
        private  ControladoraCasos controlCasos;
        private string idEje = "-1";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idEje"] != null)
            {
                idEje = Request.QueryString["idEje"];
            }

            //Si ya está logueado:
            if (Request.IsAuthenticated)
            {
                
                //Inicializamos controladora
                controlCasos = new ControladoraCasos();
                hacerResumen(idEje);

                //Si es la primera vez que se carga la página:
                if (!this.IsPostBack)
                {               
                }                
            }
            else
            {//En caso de que no esté logueado, redirija a login
                Response.Redirect("Login.aspx");
            }
        }

        /*
         * Desc: Se encarga de realizar las operaciones de creacion de reporte
         * Requiere: n/a
         * Retorna. n/a
         */ 
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            
        }

        protected void volverEj(object sender, EventArgs e)
        {
            Response.Redirect("Ejecucion.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {           

        }



        protected int parseInt(string valor)
        {
            int parsedInt;
            string trimmed = valor;
            bool parsed = int.TryParse(trimmed.Replace("-", ""), out parsedInt);
            if (!parsed)
            {
                parsedInt = -1;
            }
            return parsedInt;
        }

        /**
         * Descripcion: se revisa el perfil de la persona que inició sesión en el sistema.
         * Si es miembro no se le muestra el grid, ni los botones de eliminar e insertar,
         * se llenan los campos con su información personal.
         * Si es Administrador se muestra todo, es decir, no se cambia nada.
         * Recibe: un string @usuario que es el nombre de usuario de la persona en el sistema.
         *         un booleano @esInicio que determina si se llama a la función desde la primera carga o desde alguna otra parte de la interfaz
         * Devuelve verdadero si es administrador o falso si es un miembro de equipo.
         */
        protected bool revisarPerfil(string usuario, bool esInicio)
        {
            //Obtiene el perfil de la controladora
            string perfilS = controlCasos.getPerfil(usuario);
            //Si es miembro de equipo:
            if (perfilS.Equals("M"))
            {
                //Si el método se llama desde la primera carga de la página, controle lo necesario
                if (esInicio)
                {
                    /*btnEliminar.Visible = false;
                    btnInsertar.Visible = false;
                    gridRecursos.Visible = false;
                    EntidadRecursoH miembro = controlRH.consultaRH(usuario);
                    llenaCampos(miembro);*/
                }
                //No es administrador
                return false;
            }
            else
            {
                //Es administrador
                return true;
            }
        }

        protected void hacerResumen(string idDiseno)
        {
        }


    }
}