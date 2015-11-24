using GestionPruebas.App_Code;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionPruebas.App_Code;

namespace GestionPruebas
{
    public partial class Ejecucion : System.Web.UI.Page
    {
        private string idDise = "-1";
        private string idProy = "-1";
        private ControladoraEjecucion controlEjecucion = new ControladoraEjecucion();

        List <Object[]> lista_No_Conf= new List <Object[]>();
        List<EntidadEjecucion> listaEntidades = new List<EntidadEjecucion>();
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idDise"] != null)
            {
                idDise = Request.QueryString["idDise"];
            }
            if (Request.QueryString["idProy"] != null)
            {
                idProy = Request.QueryString["idProy"];
            }

            if (Request.IsAuthenticated)
            {
                if (!this.IsPostBack)
                {
                    refrescaTabla();
                    listaEntidades = controlEjecucion.consultarEjecuciones(idProy, idDise);
                }    
            }
        }

        /*
         * Refresca el grid consultando a la base de datos, por medio de la controladora.
         * Requiere: n/a
         * Retorna: n/a
         */
        private void refrescaTabla()
        {
            System.Data.DataTable dtEjecu = controlEjecucion.consultarEjecucionesDt(idProy, idDise);
            System.Data.DataView dvEjecu = dtEjecu.DefaultView;
            gridEjecuciones.DataSource = dvEjecu;
            gridEjecuciones.DataBind();
        }

        protected void gridEjecuciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Si el tipo de la fila es de datos
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Le da formato a la fila seleccionada
                if (e.Row.RowIndex == gridEjecuciones.SelectedIndex)
                {
                    e.Row.ToolTip = "Esta fila está seleccionada!";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='#0099CC';";
                    e.Row.ForeColor = ColorTranslator.FromHtml("#000000");
                    e.Row.BackColor = ColorTranslator.FromHtml("#0099CC");
                }
                //Le da formato a las demás filas
                else
                {
                    e.Row.ToolTip = "Click para seleccionar esta fila.";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                }
                //Determina formato general y acción al hacer click sobre la fila
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridEjecuciones, "Select$" + e.Row.RowIndex);
            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            /*//Le da formato a toda la tabla
            foreach (GridViewRow row in gridCasos.Rows)
            {
                //Formato de fila seleccionada
                if (row.RowIndex == gridCasos.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#0099CC");
                    row.ToolTip = "Esta fila está seleccionada!";
                    row.ForeColor = ColorTranslator.FromHtml("#000000");
                    row.Attributes["onmouseout"] = "this.style.backgroundColor='#0099CC';";

                    string id = row.Cells[0].Text;



                    EntidadCaso casoSel = controlCasos.consultaCaso(id, idDise);
                    //string req = controlCasos.consultarReq(id, idDise);
                    titFunc.InnerText = "Consultar";
                    llenaCampos(casoSel);

                }
                //Filas no seleccionadas
                else
                {
                    row.Attributes["onmouseout"] = "this.style.backgroundColor='#FFFFFF';";
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click para seleccionar esta fila.";
                }
            }

            inhabilitarCampos();
            btnAceptar.Enabled = false;
            btnCancelar.Disabled = true;
            btnModificar.Disabled = false;
            btnEliminar.Disabled = false;
            btnInsertar.Disabled = false;*/
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
            if (btnAceptar.Text.Equals("Aceptar"))
            {
                Object[] ejec = new Object[5];

                ejec[0] = calendario.Value;
                ejec[1] = TextIncidencias.Value;
                ejec[2] = responsable.Value;            
                ejec[3] = TextDiseno.Value;
                ejec[4] = TextProyecto.Value;

                int resultado = controlEjecucion.insertarEjecucion(ejec);

                if (resultado == 0)
                {
                    string resultadoS = "Ejecucion Insertada!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + resultadoS + "')", true);
                }
                else
                {
                    string resultadoS = "Error";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                }
            }
            else
            {
            //**********---PARA Modificar----*********
                Object[] datos_nuevos = new Object[3];
            datos_nuevos[0] = responsable.Value;
            datos_nuevos[1] = calendario.Value;
            datos_nuevos[2] = TextIncidencias.Value;

            //contar la cantidad de filas que tiene el list y crear esa cantidad de entidades noConf           
            //por cada fila creo un objeto 

            
            int cant_NC=lista_No_Conf.Count();
            for (int i = 0; i < cant_NC; i++) {
                //controlEjecucion.modif_NC(lista_No_Conf[i]); 
            }


            //no conformidad anterior
            Object[] NC_anterior = new Object[6];
            NC_anterior[0] = ViewState["tipoNC"];
            NC_anterior[1] = ViewState["idCaso"];
            NC_anterior[2] = ViewState["descrip"];
            NC_anterior[3] = ViewState["just"];
                NC_anterior[4] = ViewState["estado"];

            //otros datos anteriores
            Object[] datos_anterior = new Object[3];
                datos_anterior[0] = ViewState["resp"];
                datos_anterior[1] = ViewState["fecha"];
                datos_anterior[2] = ViewState["incid"];
            }
        }

    }
}