using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionPruebas
{
    public partial class Ejecucion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void habilitarInsertar(object sender, EventArgs e)
        {
            tipoNC.Disabled = false;
            idCasoText.Disabled = false;
            TextDescripcion.Disabled = false;
            TextJustificacion.Disabled = false;
            ComboEstado.Disabled = false;
            calendario.Disabled = false;
            TextIncidencias.Disabled = false;
            btnAceptar.Text = "Aceptar";
            btnAceptar.Enabled = true;
            btnCancelar.Disabled = false;
        }

        protected void habilitarModificar(object sender, EventArgs e)
        {
            tipoNC.Disabled = false;
            idCasoText.Disabled = false;
            TextDescripcion.Disabled = false;
            TextJustificacion.Disabled = false;
            ComboEstado.Disabled = false;
            calendario.Disabled = false;
            TextIncidencias.Disabled = false;
            btnAceptar.Text = "Guardar";
            btnAceptar.Enabled = true;
            btnCancelar.Disabled = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            inhabilitarCampos();
        }

        protected void limpiarCampos()
        {
            TextDescripcion.Value = "";
            TextJustificacion.Value = "";
            TextIncidencias.Value = "";
        }

        protected void inhabilitarCampos()
        {
            tipoNC.Disabled = true;
            idCasoText.Disabled = true;
            TextDescripcion.Disabled = true;
            TextJustificacion.Disabled = true;
            ComboEstado.Disabled = true;
            calendario.Disabled = true;
            TextIncidencias.Disabled = true;
            btnAceptar.Enabled = false;
            btnCancelar.Disabled = true;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        { }

        protected void btnAceptar_Click(object sender, EventArgs e)
        { }

    }
}