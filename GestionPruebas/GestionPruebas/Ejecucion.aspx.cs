using GestionPruebas.App_Code;
//using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GestionPruebas
{
    public partial class Ejecucion : System.Web.UI.Page
    {
        private string idDise = "-1";
        private string idProy = "-1";
        private ControladoraEjecucion controlEjecucion = new ControladoraEjecucion();

        private List <Object[]> lista_No_Conf= new List <Object[]>();
        List<EntidadEjecucion> listaEntidades = new List<EntidadEjecucion>();

        DataTable tablaNC = new DataTable();

 
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (Request.IsAuthenticated)
            {
                tablaNC.Columns.Add(new DataColumn("Tipo", typeof(string)));
                tablaNC.Columns.Add(new DataColumn("IdCaso", typeof(string)));
                tablaNC.Columns.Add(new DataColumn("Estado", typeof(string)));

                if (Request.QueryString["idDise"] != null)
                {
                    idDise = Request.QueryString["idDise"];
                }

                idProy = controlEjecucion.nombrarProy(idDise);
                TextProyecto.Value = idProy;

                listaEntidades = controlEjecucion.consultarEjecuciones(idProy, idDise);
                if (!this.IsPostBack)
                {
                    TextProyecto.Value = idProy;
                    TextDiseno.Value = idDise;
                    refrescaTabla();
                  
                }
                ItemsGrid.DataSource = tablaNC;
                ItemsGrid.DataBind();
            }
            else
            {
                Response.Redirect("Login.aspx");
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
            //Le da formato a toda la tabla
            foreach (GridViewRow row in gridEjecuciones.Rows)
            {
                //Formato de fila seleccionada
                if (row.RowIndex == gridEjecuciones.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#0099CC");
                    row.ToolTip = "Esta fila está seleccionada!";
                    row.ForeColor = ColorTranslator.FromHtml("#000000");
                    row.Attributes["onmouseout"] = "this.style.backgroundColor='#0099CC';";


                    ViewState["idEjecu"] = listaEntidades.ElementAt(row.RowIndex).Id;
                    titFunc.InnerText = "Consultar";

                    llenaCamposEjecucion(row.RowIndex);
                    cargarNoConformidades();

                }
                //Filas no seleccionadas
                else
                {
                    row.Attributes["onmouseout"] = "this.style.backgroundColor='#FFFFFF';";
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click para seleccionar esta fila.";
                }
            }

        }

        protected void llenaCamposEjecucion(int index)
        {
            EntidadEjecucion entidad = listaEntidades.ElementAt(index);
            TextIncidencias.Value = entidad.Incidencias;
            responsable.Value = entidad.NombreResponsable;
            calendario.Value = String.Format("{0:yyyy-MM-dd}", entidad.Fecha);
            System.Diagnostics.Debug.WriteLine(String.Format("{0:yyyy-MM-dd}", entidad.Fecha));
            responsable.Items.Clear();
            responsable.Items.Add(new ListItem(entidad.NombreResponsable + " ("+entidad.Responsable.ToString()+")", "1"));
        }

        private void cargarNoConformidades()
        {
            lista_No_Conf = controlEjecucion.consultarNoConformidades(ViewState["idEjecu"].ToString());
            tablaNC.Clear();
            DataRow dr;
            foreach (var nc in lista_No_Conf)
            {
                dr = tablaNC.NewRow();

                dr[0] = nc[4];
                dr[1] = nc[3];
                dr[2] = nc[7];

                tablaNC.Rows.Add(dr);
            }
            ItemsGrid.DataBind();
 
        }


        protected void habilitarInsertar(object sender, EventArgs e)
        {
            responsable.Disabled = false;
            tipoNC.Disabled = false;
            idCasoText.Disabled = false;
            descripcionText.Disabled = false;
            justificacionText.Disabled = false;
            ComboEstado.Disabled = false;
            calendario.Disabled = false;
            TextIncidencias.Disabled = false;
            btnAceptar.Text = "Aceptar";
            btnAceptar.Enabled = true;
            btnCancelar.Disabled = false;
            btnModificar.Disabled = true;
            btnEliminar.Disabled = true;

            responsable.Items.Clear();
            List<string> responsables = controlEjecucion.traerResp(idProy);
            int i = 0;
            while (i <= responsables.Count - 1)
            {
                responsable.Items.Add(new ListItem(responsables.ElementAt(i)));
                i++;
            }

            idCasoText.Items.Clear();
            List<string> casos = controlEjecucion.traerCasos(idDise);
            int j = 0;
            while (j <= casos.Count - 1)
            {
                idCasoText.Items.Add(new ListItem(casos.ElementAt(j)));
                j++;
            }
        }

        protected void habilitarModificar(object sender, EventArgs e)
        {
            tipoNC.Disabled = false;
            idCasoText.Disabled = false;
            descripcionText.Disabled = false;
            justificacionText.Disabled = false;
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
                ViewState["descrip"] = descripcionText.Value;
                ViewState["just"] = justificacionText.Value;
                ViewState["estado"] = ComboEstado.Value;
                //ViewState["ima"]=imagen;               
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            inhabilitarCampos();
            btnModificar.Disabled = false;
            btnEliminar.Disabled = false;
        }

        protected void limpiarCampos()
        {
            descripcionText.Value = "";
            justificacionText.Value = "";
            TextIncidencias.Value = "";
        }

        protected void inhabilitarCampos()
        {
            tipoNC.Disabled = true;
            idCasoText.Disabled = true;
            descripcionText.Disabled = true;
            justificacionText.Disabled = true;
            ComboEstado.Disabled = true;
            calendario.Disabled = true;
            TextIncidencias.Disabled = true;
            btnAceptar.Enabled = false;
            btnCancelar.Disabled = true;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //Si ya se selecciono una ejecucion del grid.
            if (ViewState["idEjecu"] != null){ 
                string idEjec = ViewState["idEjecu"].ToString();

                controlEjecucion.eliminarEjecucion(idEjec);
            }
            else
            {
                string error = "¡Debe seleccionar una ejecución primero!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + error + "')", true);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        { 
            if (btnAceptar.Text.Equals("Aceptar"))
            {
                Object[] ejec = new Object[5];
                List<EntidadNoConformidad> entidadConf = new List<EntidadNoConformidad>();

                ejec[0] = calendario.Value;
                ejec[1] = TextIncidencias.Value;
                string[] resp = responsable.Value.Split('(');
                string cedula = resp[1].Substring(0,9);
                ejec[2] = cedula;            
                ejec[3] = TextDiseno.Value;
                ejec[4] = TextProyecto.Value;

                lista_No_Conf = (List<Object[]>)(ViewState["lista_No_Conf"]); 

                int resultado = controlEjecucion.insertarEjecucion(ejec, lista_No_Conf);

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

            Object[] tup = new Object[6];
            tup[0] = 1;
            tup[1] = "1req1";
            tup[2] = "tipoN";
            tup[3] = "descripcionN" ;
            tup[4] = "justificacionN" ;
            tup[5] ="estadoN";
            lista_No_Conf.Add(tup);

            int cant_NC=lista_No_Conf.Count();
            for (int i = 0; i < cant_NC; i++) {
                controlEjecucion.modif_NC(lista_No_Conf[i]); 
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

        /*
         * Descripción: Agrega en una lista temporal una entrada nueva a una ejecucion.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btn_agregarEntrada_Click(object sender, EventArgs e)
        {
            if (tipoNC.SelectedIndex ==0 || string.Equals(idCasoText.Value, "") || string.Equals(descripcionText.Value, "")|| string.Equals(justificacionText.Value, "")||ComboEstado.SelectedIndex == 0)
            {

                string resultadoS = "Debe agregar una entrada con su tipo NC respectivo.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
            }
            else
            {
                Object[] entradas = new Object[7];
                entradas[0] = TextDiseno.Value;
                entradas[1] = idCasoText.Value;
                entradas[2] = tipoNC.Value;
                entradas[3] = descripcionText.Value;
                entradas[4] = justificacionText.Value;
                entradas[5] = ComboEstado.Value;
                entradas[6] = imagen.Value;

                if (ViewState["lista_No_Conf"] != null)
                {
                    lista_No_Conf = (List<Object[]>)ViewState["lista_No_Conf"];
                }
                
                lista_No_Conf.Add(entradas);

                DataTable dt;

                if (ViewState["TablaActual"] == null)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Tipo", typeof(string)));
                    dt.Columns.Add(new DataColumn("IdCaso", typeof(string)));
                    dt.Columns.Add(new DataColumn("Estado", typeof(string)));
                }
                else
                {
                    dt = (DataTable)ViewState["TablaActual"];
                }

                DataRow dr = null;
                dr = dt.NewRow();
                dr["Tipo"] = tipoNC.Value;
                dr["IdCaso"] = idCasoText.Value;
                dr["Estado"] = ComboEstado.Value;
                dt.Rows.Add(dr);
                //dr = dt.NewRow();
 
                //Store the DataTable in ViewState
                ViewState["TablaActual"] = dt;
                ItemsGrid.DataSource = dt;
                ItemsGrid.DataBind();

                //listEntradas.Items.Add(entradaNueva);
                //ItemsGrid.
                //LIMPIAR CAMPOS AQUI SI ES NECESARIO
            }

            ViewState["lista_No_Conf"] = lista_No_Conf; 
        }

        /*
         * Descripción: quita de la lista la entrada seleccionada en el listbox.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            //string entradaAQuitar = (string)listEntradas.SelectedValue;
            //listEntradas.Items.Remove(entradaAQuitar);
        }

        /*
         * Descripción: Elimina todas las entradas del listbox
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btnLimpiarLista_Click(object sender, EventArgs e)
        {
            //listEntradas.Items.Clear();
        }
    }
}