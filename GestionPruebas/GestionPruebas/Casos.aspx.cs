using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionPruebas.App_Code;
using System.Data;

namespace GestionPruebas
{
    public partial class Casos : System.Web.UI.Page
    {
        private  ControladoraCasos controlCasos;
        private string entradas;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Si ya está logueado:
            if (Request.IsAuthenticated)
            {
                
                //Inicializamos controladora
                controlCasos = new ControladoraCasos();

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
                    if (true)//esAdmin) // TODO
                    {
                        refrescaTabla();
                    }
               
                }
                
            }
            else
            {//En caso de que no esté logueado, redirija a login
                Response.Redirect("Login.aspx");
            }
        }

        private void refrescaTabla()
        {
            DataTable dtCaso = controlCasos.consultarCasos();
            DataView dvCaso = dtCaso.DefaultView;
            gridCasos.DataSource = dvCaso;
            gridCasos.DataBind();
        }

        /*
         * Descripción: Agrega en una lista temporal una entrada nueva a un caso.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btn_agregarEntrada_click(object sender, EventArgs e)
        {
            if (string.Equals(entradaDatos.Value,"") || string.Equals(estadoBox.Value,""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alert('Debe de ingresar una entrada y su tipo correspondiente')", true);
           
            }
            else
            {
                string entradaNueva = (string)entradaDatos.Value;
                entradaNueva += " - " + (string)estadoBox.Value;

                listEntradas.Items.Add(entradaNueva);

                entradaDatos.Value = "";
                estadoBox.Value = "";
            }
            
   
        }

        /*
         * Descripción: Se habilitan los campos para poder realizar la inserción de un caso de uso nuevo.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            //listaEntradas = new List<string>();
            habilitarCampos();
        }



        /*
         * Descripción: quita de la lista la entrada seleccionada en el listbox.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            string entradaAQuitar = (string)listEntradas.SelectedValue;
            listEntradas.Items.Remove(entradaAQuitar);
        }



        protected void btnLimpiarLista_Click(object sender, EventArgs e)
        {
            listEntradas.Items.Clear();
        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //Si va a insertar
            //if (!btnInsertar.Disabled)
            {
                foreach (ListItem entrada in listEntradas.Items)
                {
                    entradas += entrada.Value + ",";
                }

                string resultado = controlCasos.insertarCaso(idCaso.Value, proposito.Value, entradas, resultadoEsperado.Value, flujo.Value,0);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alert('"+resultado+"')", true);
           
                entradaDatos.Value = "";
                estadoBox.Value = "";
                idCaso.Value = "";
                proposito.Value = "";
                resultadoEsperado.Value = "";
                flujo.Value = "";
                listEntradas.Items.Clear();

                inhabilitarCampos();

            }
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
                listEntradas.Items.Clear();

                inhabilitarCampos();
                
            }
        }

        /*
         * Descripción: actualiza el listbox con las entradas que esten en listaEntradas.
         * Requiere: n/a
         * Retorna: n/a
         */ 


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