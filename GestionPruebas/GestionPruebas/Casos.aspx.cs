using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionPruebas.App_Code;

namespace GestionPruebas
{
    public partial class Casos : System.Web.UI.Page
    {
        private List<string> listaEntradas; 
        private  ControladoraCasos controlCasos;
        private string entradas;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Si ya está logueado:
            if (Request.IsAuthenticated)
            {
                
                //Inicializamos controladora
                controlCasos = new ControladoraCasos();

                listaEntradas = new List<string>();

                entradas = "";

                //Si es la primera vez que se carga la página:
                if (!this.IsPostBack)
                {
                    //Obtiene usuario logueado
                    string usuarioS = ((SiteMaster)this.Master).nombreUsuario;
                    //Revisa su perfil
                    //bool esAdmin = revisarPerfil(usuarioS, true);
                    btnEliminar.Disabled = true;
                    inhabilitarCampos();
                    /*
                    if (esAdmin)
                    {
                        Poner codigo de admin aqui
                    }
                }
                     */
                    btnAceptar.Text = "Aceptar";
                }
                
            }
            else
            {//En caso de que no esté logueado, redirija a login
                Response.Redirect("Login.aspx");
            }
        }

        /*
         * Descripción: Agrega en una lista temporal una entrada nueva a un caso.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btn_agregarEntrada_click(object sender, EventArgs e)
        {
            string entradaNueva = (string)entradaDatos.Value;
            entradaNueva += " - "+ (string)estadoBox.Value;

            listaEntradas.Add(entradaNueva);

            actualizarListaEntradas();

            entradaDatos.Value = "";
            estadoBox.Value = "";

            
        }

        /*
         * Descripción: Se habilitan los campos para poder realizar la inserción de un caso de uso nuevo.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            habilitarCampos();
        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        /*
         * Descripción: quita de la lista la entrada seleccionada en el listbox.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            string entradaAQuitar = (string)listEntradas.SelectedValue;
            listaEntradas.Remove(entradaAQuitar);
            actualizarListaEntradas();
        }



        protected void btnLimpiarLista_Click(object sender, EventArgs e)
        {
            listaEntradas.Clear();
            actualizarListaEntradas();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            
            //Si es una inserción
            if (!btnInsertar.Disabled)
            {
                entradaDatos.Value = "";
                estadoBox.Value = "";
                idCaso.Value = "";
                proposito.Value = "";
                resultadoEsperado.Value = "";
                flujo.Value = "";
                listaEntradas.Clear();

                actualizarListaEntradas();

                inhabilitarCampos();
                
            }
        }

        /*
         * Descripción: actualiza el listbox con las entradas que esten en listaEntradas.
         * Requiere: n/a
         * Retorna: n/a
         */ 
        protected void actualizarListaEntradas()
        {

            listEntradas.Items.Clear();

            foreach (string entrada in listaEntradas)
            {
                listEntradas.Items.Add(new ListItem(entrada));
            }
        }


        /*
         * Descripción: inhabilita los campos del form.
         * Requiere: n/a
         * Retorna: n/a
         */ 
        protected void inhabilitarCampos()
        {
            btnEliminar.Disabled = true;
            btnModificar.Disabled = true;
            btnInsertar.Disabled = false;
            btnAceptar.Enabled = false;
            btnCancelar.Disabled = true;
            btn_agregarEntrada.Disabled = true;
            entradaDatos.Disabled = true;
            idCaso.Disabled = true;
            listEntradas.Enabled = false;
            proposito.Disabled = true;
            resultadoEsperado.Disabled = true;
            flujo.Disabled = true;
            estadoBox.Disabled = true;
            btnQuitar.Disabled = true;
            btnLimpiarLista.Disabled = true;
        }



        /*
        * Descripción: habilita los campos del form.
        * Requiere: n/a
        * Retorna: n/a
        */
        protected void habilitarCampos()
        {
            btnModificar.Disabled = true;
            btnAceptar.Enabled = true;
            btnCancelar.Disabled = false;
            btn_agregarEntrada.Disabled = false;
            entradaDatos.Disabled = false;
            idCaso.Disabled = false;
            listEntradas.Enabled = true;
            proposito.Disabled = false;
            resultadoEsperado.Disabled = false;
            flujo.Disabled = false;
            estadoBox.Disabled = false;
            btnQuitar.Disabled = false;
            btnLimpiarLista.Disabled = false;
        }
    }
}