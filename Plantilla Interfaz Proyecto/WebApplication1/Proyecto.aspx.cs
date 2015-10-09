using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.App_Code;

namespace WebApplication1
{
    public partial class Proyecto : System.Web.UI.Page
    {
        private ControladoraProyecto controladoraProyecto;
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            btnEliminar.Disabled = true;
            btnModificar.Disabled = true;
            btnAceptarInsertar.Disabled = false;
            btnCancelarInsertar.Disabled = false;
            
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            btnInsertar.Disabled = true;
            btnEliminar.Disabled = true;
            btnAceptarInsertar.Visible = false;
            btnCancelarInsertar.Visible = false;
            btnGuardarModificar.Disabled = false; 
            btnCancelarModificar.Disabled = false;
            btnGuardarModificar.Visible = true;
            btnCancelarModificar.Visible = true;


        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnCancelar_Insertar(object sender, EventArgs e)
        {
            btnEliminar.Disabled = false;
            btnModificar.Disabled = false;
            btnAceptarInsertar.Disabled = true;
            btnCancelarInsertar.Disabled = true;
        }

        protected void btnAceptar_Insertar(object sender, EventArgs e)
        {
            /*string obj = objetivo.Text;
            string nombreP = nombreProyecto.Value;
            string est = barraEstado.Value;
            string fechaP = calendario.Value;
            string nomOf = nombreOficina.Value;
            string rep = representante.Value;
            string email = correoOficina.Value;
            string telOf = telefonoOficina.Value;*/
            char est = barraEstado.Value[0];
            Object[] dat = new Object[8];
            dat[0] = nombreProyecto.Value; 
           
        }
        protected void btnGuardar_Modificar(object sender, EventArgs e)
        {
            /*string obj = objetivo.Text;
            string nombreP = nombreProyecto.Value;
            string est = barraEstado.Value;
            string fechaP = calendario.Value;
            string nomOf = nombreOficina.Value;
            string rep = representante.Value;
            string email = correoOficina.Value;
            string telOf = telefonoOficina.Value;*/


            char est = barraEstado.Value[0];
            Object[] dat = new Object[8];
            dat[0] = nombreProyecto.Value; 

        }
        protected void btnCancelar_Modificar(object sender, EventArgs e)
        {
            btnInsertar.Disabled = false;
            btnEliminar.Disabled = false;
            btnGuardarModificar.Disabled = true;
            btnCancelarModificar.Disabled = true;
            btnGuardarModificar.Visible = false;
            btnCancelarModificar.Visible = false;
            btnAceptarInsertar.Visible = true;
            btnCancelarInsertar.Visible = true;
        }
        
    }
}