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
        List <Object[]> lista_No_Conf= new List <Object[]>();
 
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

            ViewState["resp"] = responsable.Value;
            ViewState["fecha"] = calendario.Value;
            ViewState["incid"] = TextIncidencias.Value;

            if(idCasoText!=null){
                ViewState["tipoNC"] = tipoNC.Value;
                ViewState["idCaso"] = idCasoText.Value;
                ViewState["descrip"] = TextDescripcion.Value;
                ViewState["just"] = TextJustificacion.Value;
                ViewState["estado"] = ComboEstado.Value;
                //ViewState["ima"]=imagen;               
            }
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
        { 
            //**********---PARA Modificar----*********
            Object [] datos_nuevos= new Object[3];
            datos_nuevos[0] = responsable.Value;
            datos_nuevos[1] = calendario.Value;
            datos_nuevos[2] = TextIncidencias.Value;

            //contar la cantidad de filas que tiene el list y crear esa cantidad de entidades noConf           
            //por cada fila creo un objeto 

            Object[] noConf = new Object[6];
            lista_No_Conf.Add(noConf);

            //no conformidad anterior
            Object[] NC_anterior = new Object[6];
            NC_anterior[0] = ViewState["tipoNC"];
            NC_anterior[1] = ViewState["idCaso"];
            NC_anterior[2] = ViewState["descrip"];
            NC_anterior[3] = ViewState["just"];
            NC_anterior[4] =ViewState["estado"];

            //otros datos anteriores
            Object[] datos_anterior = new Object[3];
            datos_anterior[0]=ViewState["resp"];
            datos_anterior[1]=ViewState["fecha"];
            datos_anterior[2]= ViewState["incid"] ;  
        }

    }
}