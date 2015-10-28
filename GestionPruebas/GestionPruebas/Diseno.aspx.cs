using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionPruebas.App_Code;

namespace GestionPruebas
{
    public partial class Diseno : Page
    {
        //private ControladoraDiseno controlDiseno;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void habilitarAdmReq(object sender, EventArgs e)
        {
            panelReq.Visible = true;
            panelDiseno.Visible = false;
        }

         /**
         * Descripcion: La acción que se realiza al presionar el boton de eliminar:
         * Elimina un recurso, seleccionado del grid de diseños de prueba, de la base de datos.
         * Recibe Object    @sender. No se utiliza
         *        EventArgs @e. No se utiliza
         * No devuelve nada.         
        */
      /* protected void btnEliminar_Click(object sender, EventArgs e)
        {
            btnEliminar.Disabled = false;
            btnAceptarDiseno.Enabled = true;
            btnCancelarDiseno.Enabled = true;
            btnCancelarDiseno.Visible = false;
            btnAceptarDiseno.Visible = false;

            //Revisa que se haya seleccionado un recurso del grid
            if (ViewState["idDiseno"] != null &&
                !string.IsNullOrWhiteSpace(.Value) &&
                !string.IsNullOrWhiteSpace(.Value) &&
                !string.IsNullOrWhiteSpace(.Value) &&
                !string.IsNullOrWhiteSpace(.Value) &&
                !string.IsNullOrWhiteSpace(.Value))
            {
                int idDise = (int)ViewState["idDiseno"];

                //Realiza la consulta que elimina recurso de la base de datos
                int resultado = controlDiseno.eliminaRH(idDise);

                string resultadoS;
                switch (resultado)
                {
                    //0: todo correcto
                    case 0:
                        resultadoS = "Se eliminó la información correctamente";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + resultadoS + "')", true);
                        break;
                    //Error en eliminación de usuario
                    case -1:
                        resultadoS = "Error al eliminar la información del diseño (no se afectó ningún registro)";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                        break;
                    //Error SQL inesperado
                    default:
                        resultadoS = "Error al eliminar los datos, intente de nuevo";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                        break;
                }
                gridDiseno.SelectedIndex = -1;
                btnAceptarDiseno.Enabled = false;
                btnCancelarDiseno.Enabled = false;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                //limpiaCampos();
                //deshabilitaCampos();
                //refrescaTabla();
            }
            //Si el usuario no seleccionó un recurso del grid se le muestra un mensaje de alerta
            else{
                string faltantes = "Debe seleccionar un diseño en la tabla primero.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);
            }
            btnAceptarDiseno.Visible = true;
            btnCancelarDiseno.Visible = true;
        }*/
    }
}