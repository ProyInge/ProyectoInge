using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnAceptar.Disabled = true;
            btnCancelar.Disabled = true;
            btnEliminar.Disabled = false;
            btnModificar.Disabled = false;
            btnInsertar.Disabled = false;
            btnAceptar.InnerHtml = "Aceptar";
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            btnEliminar.Disabled = true;
            btnModificar.Disabled = true;
            btnAceptar.Disabled = false;
            btnCancelar.Disabled = false;

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            btnInsertar.Disabled = true;
            btnEliminar.Disabled = true;
            btnAceptar.InnerHtml = "Guardar";
            btnAceptar.Disabled = false;
            btnCancelar.Disabled = false;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            btnInsertar.Disabled = true;
            btnModificar.Disabled = true;
            btnAceptar.Disabled = false;
            btnCancelar.Disabled = false;
        }
    }
}