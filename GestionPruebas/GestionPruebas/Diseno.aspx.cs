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
    public partial class Diseno : Page
    {
        private ControladoraDiseno controlDiseno;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                controlDiseno = new ControladoraDiseno();
                if (!this.IsPostBack)
                {
                    inhabilitarCampos();
                    llenaCombo();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void llenaCombo()
        {
            DataTable dt = controlDiseno.consultaProyectos();
            int i = 0;
            foreach(DataRow row in dt.Rows)
            {
                DataColumn nombre = dt.Columns[0];
                proyecto.Items.Add(new ListItem(row[nombre].ToString(), ""+i));
                i++;
            }
        }

        protected void llenaReqs(int idDise)
        {
            int d = 0;
            DataTable reqDisp = controlDiseno.consultaReqDisponibles(idDise);
            DisponiblesChkBox.Items.Clear();
            foreach (DataRow r in reqDisp.Rows)
            {
                DisponiblesChkBox.Items.Add(new ListItem(r[0].ToString() + " - " +r[1].ToString(), "" + d));
                d++;
            }

            DataTable reqAsig = controlDiseno.consultaReqAsignados(idDise);
            AsignadosChkBox.Items.Clear();
            int a = 0;
            foreach (DataRow r in reqAsig.Rows)
            {
                AsignadosChkBox.Items.Add(new ListItem(r[0].ToString() + " - " + r[1].ToString(), "" + a));
                a++;
            }
        }

        protected void habilitarAdmReq(object sender, EventArgs e)
        {
            panelReq.Visible = true;
            panelDiseno.Visible = false;
            btnAceptarDiseno.Visible = false;
            btnCancelarDiseno.Visible = false;
            btnAceptarReq.Visible = true;
            btnCancelarReq.Visible = true;
            gridReq.Visible = true;
            gridDiseno.Visible = false;
        }

        protected void habilitarAdmDiseno(object sender, EventArgs e)
        {
            panelReq.Visible = false;
            panelDiseno.Visible = true;
            btnAceptarDiseno.Visible = true;
            btnCancelarDiseno.Visible = true;
            btnAceptarReq.Visible = false;
            btnCancelarReq.Visible = false;
            gridReq.Visible = false;
            gridDiseno.Visible = true;
        }

        protected void habilitarParaInsertar(object sender, EventArgs e)
        {
            btnAceptarReq.Enabled = true;
            btnCancelarReq.Disabled = false;
            btnAceptarDiseno.Enabled = true;
            btnCancelarDiseno.Enabled = true;
            volver.Enabled = false;
            habilitarCampos();
        }

        protected void habilitarParaModificar(object sender, EventArgs e)
        {
            habilitarCampos();
            btnAceptarDiseno.Text = "Guardar";
            volver.Enabled = false;
        }

        protected void cancelarReq(object sender, EventArgs e)
        {
            inhabilitarCampos();
            limpiarCampos();
        }

        protected void aceptarReq(object sender, EventArgs e)
        {
            string id = idReq.Value;
            string nom = nomReq.Value;
            controlDiseno.insertarReq(id, nom);
            string confirmado = "Requerimiento Insertado";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + confirmado + "')", true);
            inhabilitarCampos();
            btnAceptarReq.Enabled = false;
            btnCancelarReq.Disabled = true;
            volver.Enabled = true;

        }

        protected void habilitarCampos()
        {
            idReq.Disabled = false;
            nomReq.Disabled = false;
            proyecto.Disabled = false;
            derecha.Disabled = false;
            izquierda.Disabled = false;
            DisponiblesChkBox.Enabled = true;
            AsignadosChkBox.Enabled = true;
            proposito.Disabled = false;
            nivel.Disabled = false;
            tecnica.Disabled = false;
            procedimiento.Disabled = false;
            ambiente.Disabled = false;
            criterios.Disabled = false;
            calendario.Disabled = false;
            responsable.Disabled = false;
        }

        protected void inhabilitarCampos()
        {
            idReq.Disabled = true;
            nomReq.Disabled = true;
            proyecto.Disabled = true;
            derecha.Disabled = true;
            izquierda.Disabled = true;
            DisponiblesChkBox.Enabled = false;
            AsignadosChkBox.Enabled = false;
            proposito.Disabled = true;
            nivel.Disabled = true;
            tecnica.Disabled = true;
            procedimiento.Disabled = true;
            ambiente.Disabled = true;
            criterios.Disabled = true;
            calendario.Disabled = true;
            responsable.Disabled = true;
            volver.Enabled = true;
        }

        protected void limpiarCampos()
        {
            idReq.Value = "";
            nomReq.Value = "";
            proyecto.Value = "";
            DisponiblesChkBox.Items.Clear();
            AsignadosChkBox.Items.Clear();
            proposito.Value = "";
            nivel.Items.Clear();
            tecnica.Items.Clear();
            procedimiento.Value = "";
            ambiente.Value = "";
            criterios.Value = "";
            calendario.Value = "";
            responsable.Value = "";
        }


       /**
        * Descripcion: La acción que se realiza al presionar el boton de eliminar:
        * Elimina un recurso, seleccionado del grid de diseños de prueba, de la base de datos.
        * Recibe Object    @sender. No se utiliza
        *        EventArgs @e. No se utiliza
        * No devuelve nada.         
        */
       protected void btnEliminar_Click(object sender, EventArgs e)
        {
            btnEliminar.Disabled = false;
            btnAceptarDiseno.Enabled = true;
            btnCancelarDiseno.Enabled = true;
            btnCancelarDiseno.Visible = false;
            btnAceptarDiseno.Visible = false;

            //Revisa que se haya seleccionado un recurso del grid
            if (ViewState["idDiseno"] != null)
            {
                int idDise = (int)ViewState["idDiseno"];

                //Realiza la consulta que elimina recurso de la base de datos
                int resultado = controlDiseno.eliminaDiseno(idDise);

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
            else
            {
                string faltantes = "Debe seleccionar un diseño en la tabla primero.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);
            }
            btnAceptarDiseno.Visible = true;
            btnCancelarDiseno.Visible = true;
        }
    }
}