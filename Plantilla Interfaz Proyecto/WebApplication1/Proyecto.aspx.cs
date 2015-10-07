using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Proyecto : System.Web.UI.Page
    {


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
            string obj = objetivo.Text;
            string nombre = nombreProyecto.Value;
        }
        
    }
}