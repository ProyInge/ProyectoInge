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
using System.IO;

namespace GestionPruebas
{
    public partial class Ejecucion : System.Web.UI.Page
    {
        private string idDise = "-1";
        private string idProy = "-1";
        private ControladoraEjecucion controlEjecucion = new ControladoraEjecucion();

        private List <Object[]> lista_No_Conf= new List <Object[]>();
        private List<Object[]> listaNC_Eliminar = new List<Object[]>();
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
                else
                {
                    if(ViewState["idEjecu"] != null)
                    {
                        cargarNoConformidades();
                        llenarTabla();
                    }
                }
                gridNC.DataSource = tablaNC;
                gridNC.DataBind();
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
                    gridNC.Enabled = false;

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
            btnInsertar.Disabled = false;
            btnModificar.Disabled = false;
            btnEliminar.Disabled = false;

        }

        protected void btnModificarItemNC_Command(object sender, EventArgs e)
        {
            DataGridItem item = (DataGridItem)((LinkButton)sender).NamingContainer;
            int i = 0;
            int res = -1;
            foreach (var drv in gridNC.Items)
            {
                if(drv == item)
                {
                    res = i;
                    break;
                }
                i++;
            }
            ViewState["indexNC"] = res;
            cargarNoConformidad();
            btn_agregarEntrada.InnerText = "Guardar";
            llenarTabla();
        }

        protected void btnEliminarItemNC_Command(object sender, EventArgs e)
        {
            DataGridItem item = (DataGridItem)((LinkButton)sender).NamingContainer;
            int i = 0;
            int res = -1;
            foreach (var drv in gridNC.Items)
            {
                if (drv == item)
                {
                    res = i;
                    break;
                }
                i++;
            }

            if (ViewState["listaNC_Eliminar"] != null)
            {
                listaNC_Eliminar = (List<Object[]>)ViewState["listaNC_Eliminar"];
            }


            lista_No_Conf = (List<Object[]>)(ViewState["lista_No_Conf_N"]);
            Object[] NC_eliminar = lista_No_Conf[res];

            if (NC_eliminar[0] != null) 
            {
                System.Diagnostics.Debug.WriteLine("ID de NC"+NC_eliminar[0]);
                listaNC_Eliminar.Add(NC_eliminar);
                lista_No_Conf.RemoveAt(res);
            }
            else
            {
                lista_No_Conf.RemoveAt(res);
            }

            ViewState["listaNC_Eliminar"] = listaNC_Eliminar;
            ViewState["lista_No_Conf_N"] = lista_No_Conf;
            llenarTabla(lista_No_Conf);
            

        }

        protected void llenaCamposEjecucion(int index)
        {
            EntidadEjecucion entidad = listaEntidades.ElementAt(index);
            TextIncidencias.Value = entidad.Incidencias;
            responsable.Value = entidad.NombreResponsable;
            calendario.Value = String.Format("{0:yyyy-MM-dd}", entidad.Fecha);
            responsable.Items.Clear();
            responsable.Items.Add(new ListItem(entidad.NombreResponsable + " ("+entidad.Responsable.ToString()+")", "1"));
        }

        private void cargarNoConformidad()
        {
            Object[] noconf = lista_No_Conf.ElementAt((int)ViewState["indexNC"]);
            descripcionText.Value = (string)noconf[5];
            justificacionText.Value = (string)noconf[6];

            tipoNC.Items.Clear();
            tipoNC.Items.Add(new ListItem((string)noconf[4]));
            string tipoNC_Act= ((string)noconf[4]);
            switch (tipoNC_Act)
            {
                case "Funcionalidad":
                    tipoNC.Items.Add(new ListItem("Validación"));
                    tipoNC.Items.Add(new ListItem("Opciones que no funcionaban"));
                    tipoNC.Items.Add(new ListItem("Error de Usabilidad"));
                    tipoNC.Items.Add(new ListItem("Excepciones"));
                    tipoNC.Items.Add(new ListItem("No correspondencia"));
                    tipoNC.Items.Add(new ListItem("Ortografía"));
                    break;
                case "Validación":
                    tipoNC.Items.Add(new ListItem("Funcionalidad"));
                    tipoNC.Items.Add(new ListItem("Opciones que no funcionaban"));
                    tipoNC.Items.Add(new ListItem("Error de Usabilidad"));
                    tipoNC.Items.Add(new ListItem("Excepciones"));
                    tipoNC.Items.Add(new ListItem("No correspondencia"));
                    tipoNC.Items.Add(new ListItem("Ortografía"));
                    break;
                case "Opciones que no funcionaban":
                    tipoNC.Items.Add(new ListItem("Validación"));
                    tipoNC.Items.Add(new ListItem("Funcionalidad"));
                    tipoNC.Items.Add(new ListItem("Error de Usabilidad"));
                    tipoNC.Items.Add(new ListItem("Excepciones"));
                    tipoNC.Items.Add(new ListItem("No correspondencia"));
                    tipoNC.Items.Add(new ListItem("Ortografía"));
                    break;
                case "Error de Usabilidad":
                    tipoNC.Items.Add(new ListItem("Validación"));
                    tipoNC.Items.Add(new ListItem("Opciones que no funcionaban"));
                    tipoNC.Items.Add(new ListItem("Funcionalidad"));
                    tipoNC.Items.Add(new ListItem("Excepciones"));
                    tipoNC.Items.Add(new ListItem("No correspondencia"));
                    tipoNC.Items.Add(new ListItem("Ortografía"));
                    break;
                case "Excepciones":
                    tipoNC.Items.Add(new ListItem("Validación"));
                    tipoNC.Items.Add(new ListItem("Opciones que no funcionaban"));
                    tipoNC.Items.Add(new ListItem("Error de Usabilidad"));
                    tipoNC.Items.Add(new ListItem("Funcionalidad"));
                    tipoNC.Items.Add(new ListItem("No correspondencia"));
                    tipoNC.Items.Add(new ListItem("Ortografía"));
                    break;
                case "No correspondencia":
                    tipoNC.Items.Add(new ListItem("Validación"));
                    tipoNC.Items.Add(new ListItem("Opciones que no funcionaban"));
                    tipoNC.Items.Add(new ListItem("Error de Usabilidad"));
                    tipoNC.Items.Add(new ListItem("Excepciones"));
                    tipoNC.Items.Add(new ListItem("Funcionalidad"));
                    tipoNC.Items.Add(new ListItem("Ortografía"));
                    break;
                case "Ortografía":
                    tipoNC.Items.Add(new ListItem("Validación"));
                    tipoNC.Items.Add(new ListItem("Opciones que no funcionaban"));
                    tipoNC.Items.Add(new ListItem("Error de Usabilidad"));
                    tipoNC.Items.Add(new ListItem("Excepciones"));
                    tipoNC.Items.Add(new ListItem("No correspondencia"));
                    tipoNC.Items.Add(new ListItem("Funcionalidad"));
                    break;
            }


            idCasoText.Items.Clear();
            idCasoText.Items.Add(new ListItem((string)noconf[3]));
            string idCasoAct= ((string)noconf[3]);
            List<string> casos = controlEjecucion.traerCasos(idDise);
            int j = 0;
            while (j <= casos.Count - 1)
            {
                if (casos.ElementAt(j)!=idCasoAct) {
                   idCasoText.Items.Add(new ListItem(casos.ElementAt(j)));
                }
                
                j++;
            }


            ComboEstado.Items.Clear();
            ComboEstado.Items.Add(new ListItem((string)noconf[7]));
            string estAct= ((string)noconf[7]);
            switch (estAct) {
                case "Satisfactoria":
                    ComboEstado.Items.Add(new ListItem("Fallida"));
                    ComboEstado.Items.Add(new ListItem("Cancelada"));
                    ComboEstado.Items.Add(new ListItem("Pendiente"));
                    break;
                case "Fallida":
                    ComboEstado.Items.Add(new ListItem("Satisfactoria"));
                    ComboEstado.Items.Add(new ListItem("Cancelada"));
                    ComboEstado.Items.Add(new ListItem("Pendiente"));
                    break;
                case "Cancelada":
                    ComboEstado.Items.Add(new ListItem("Satisfactoria"));
                    ComboEstado.Items.Add(new ListItem("Fallida"));
                    ComboEstado.Items.Add(new ListItem("Pendiente"));
                    break;
                case "Pendiente":
                    ComboEstado.Items.Add(new ListItem("Satisfactoria"));
                    ComboEstado.Items.Add(new ListItem("Fallida"));
                    ComboEstado.Items.Add(new ListItem("Cancelada"));
                    break;
            }

          // using (var ms = new MemoryStream((byte[])noconf[8]))
            //{
               // var img = System.Drawing.Image.FromStream(ms);
           // }

            //byte[] bytes = (byte[])(noconf[8]);
            //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            //visualizar.ImageUrl = "data:image/jpg;base64," + base64String;
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
            gridNC.DataBind();
            ViewState["lista_No_Conf"] = lista_No_Conf;

        }


        protected void habilitarInsertar(object sender, EventArgs e)
        {
            gridNC.Enabled = true;
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
            btn_agregarEntrada.Disabled = false;

            titFunc.InnerText = "Insertar";

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

            ViewState["idEjecu"] = null;
            ViewState["lista_No_Conf_N"] = null;
            ViewState["lista_No_Conf"] = null;
            refrescaTabla();
            TextIncidencias.Value = "";
            calendario.Value = "";
            tablaNC.Clear();
            gridNC.DataBind();
        }

        protected void habilitarModificar(object sender, EventArgs e)
        {
            if (ViewState["idEjecu"] != null)
            {
                gridNC.Enabled = true;
                btn_agregarEntrada.Disabled = false;
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
                btnEliminar.Disabled = true;
                btnInsertar.Disabled = true;

                titFunc.InnerText = "Modificar";

                cargarNoConformidades();
                lista_No_Conf = (List<Object[]>)(ViewState["lista_No_Conf"]);
                ViewState["lista_No_Conf_N"] = lista_No_Conf;

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
                responsable.Disabled = false;

                if (idCasoText != null)
                {
                    ViewState["tipoNC"] = tipoNC.Value;
                    ViewState["idCaso"] = idCasoText.Value;
                    ViewState["descrip"] = descripcionText.Value;
                    ViewState["just"] = justificacionText.Value;
                    ViewState["estado"] = ComboEstado.Value;
                    //ViewState["ima"]=imagen;               
                }
            }
            else
            {
                string error = "¡Debe seleccionar una ejecución primero!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + error + "')", true);
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            inhabilitarCampos();
            btnModificar.Disabled = false;
            btnEliminar.Disabled = false;
            btnInsertar.Disabled = false;
            ViewState["lista_No_Conf_N"] = null;
            calendario.Value = "";
            ViewState["idEjecu"] = null;

            ViewState["lista_No_Conf"] = null;
            refrescaTabla();
            TextIncidencias.Value = "";
            calendario.Value = "";
            tablaNC.Clear();
            gridNC.DataBind();
            gridNC.Enabled = false;
            titFunc.InnerText = "Seleccione una acción a ejecutar";


            btn_agregarEntrada.Disabled = true;
        }

        protected void limpiarCampos()
        {
            descripcionText.Value = "";
            justificacionText.Value = "";
            TextIncidencias.Value = "";
        }

        protected void limpiarTuplas()
        {
            descripcionText.Value = "";
            justificacionText.Value = "";
            tipoNC.Value = "";
            ComboEstado.Value = "";
            idCasoText.Value = "";
        }

        protected void inhabilitarCampos()
        {
            tipoNC.Disabled = true;
            idCasoText.Disabled = true;
            descripcionText.Disabled = true;
            responsable.Disabled = true;
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

                limpiarCampos();
                limpiarTuplas();
                inhabilitarCampos();
                ViewState["idEjecu"] = null;
                ViewState["lista_No_Conf_N"] = null;
                ViewState["lista_No_Conf"] = null;
                refrescaTabla();
                tablaNC.Clear();
                gridNC.DataBind();
                calendario.Value = "";

                string si = "¡Ejecucion Borrada!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + si + "')", true);
            }
            else
            {
                string error = "¡Debe seleccionar una ejecución primero!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + error + "')", true);
            }
            refrescaTabla();
            titFunc.InnerText = "Seleccione una acción a ejecutar";
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //insertar
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

                lista_No_Conf = (List<Object[]>)(ViewState["lista_No_Conf_N"]); 
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
                //hacer update a las tuplas
                lista_No_Conf = (List<Object[]>)(ViewState["lista_No_Conf_N"]);

                //objeto con valores nuevos
                Object[] ejecN = new Object[6];
                ejecN[0] = ViewState["idEjecu"];
                ejecN[1] = calendario.Value;
                ejecN[2] = TextIncidencias.Value;
                string respoN = responsable.Value;
                string[] respN = respoN.Split('(');
                string cedulaN = respN[1].Substring(0, 9);
                ejecN[3] = cedulaN;
                ejecN[4] = TextDiseno.Value;
                ejecN[5] = TextProyecto.Value;

                int resultado = controlEjecucion.modif_Ejec(ejecN, lista_No_Conf);
                if (resultado > 0)
                {
                    string resultadoS = "Modificacion Realizada!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + resultadoS + "')", true);
                }
                else
                {
                    string resultadoS = "Error";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                }
                
                ////********* Se eliminan las NC  ***********

                if (ViewState["listaNC_Eliminar"] != null)
                {
                    listaNC_Eliminar = (List<Object[]>)ViewState["listaNC_Eliminar"];
                    foreach (var elim in listaNC_Eliminar)
                    {
                        controlEjecucion.eliminarNC((int)elim[0]);
                    }

                    listaNC_Eliminar.Clear();
                    ViewState["listaNC_Eliminar"] = listaNC_Eliminar;
                }

            }

            llenarTabla();
            inhabilitarCampos();
            refrescaTabla();
            btnInsertar.Disabled = false;
            btnEliminar.Disabled = false;
            btnModificar.Disabled = false;
            btn_agregarEntrada.Disabled = true;
            titFunc.InnerText = "Seleccione una acción a ejecutar";
        }

        /*
         * Descripción: Agrega en una lista temporal una entrada nueva a una ejecucion.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btn_agregarEntrada_Click(object sender, EventArgs e)
        {
            
            //para insertar
            if (btn_agregarEntrada.InnerText.Equals("<span class=\"glyphicon glyphicon-plus\"></span>Agregar"))
            {
                if (tipoNC.SelectedIndex == 0 || string.Equals(idCasoText.Value, "") || string.Equals(descripcionText.Value, "") || string.Equals(justificacionText.Value, "") || ComboEstado.SelectedIndex == 0)
                {
                    string resultadoS = "Debe agregar una entrada con su tipo NC respectivo.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                }
                else
                {
                    Object[] entradas = new Object[9];
                    entradas[0] = -1;
                    entradas[1] = ViewState["idEjecu"];
                    entradas[2] = TextDiseno.Value;
                    entradas[3] = idCasoText.Value;
                    entradas[4] = tipoNC.Value;
                    entradas[5] = descripcionText.Value;
                    entradas[6] = justificacionText.Value;
                    entradas[7] = ComboEstado.Value;
                    entradas[8] = imagen.Value;

                    if (ViewState["lista_No_Conf_N"] == null)
                    {
                        if (ViewState["lista_No_Conf"] != null)
                        {
                            lista_No_Conf = (List<Object[]>)(ViewState["lista_No_Conf"]);//obtiene lista logica con las tuplas de la base de datos
                            ViewState["lista_No_Conf_N"] = lista_No_Conf;
                        }
                        else {
                            lista_No_Conf = new List<Object[]>();
                        }
                    }
                    else {
                        lista_No_Conf = (List<Object[]>)(ViewState["lista_No_Conf_N"]);//obtiene lista logica con las tuplas de la base de datos
                    }
                    lista_No_Conf.Add(entradas);

                    //DataTable dt;

                    //if (ViewState["TablaActual"] == null)
                    //{
                    //    dt = new DataTable();
                    //    dt.Columns.Add(new DataColumn("Tipo", typeof(string)));
                    //    dt.Columns.Add(new DataColumn("IdCaso", typeof(string)));
                    //    dt.Columns.Add(new DataColumn("Estado", typeof(string)));
                    //}
                    //else
                    //{
                    //    dt = (DataTable)ViewState["TablaActual"];
                    //}

                    //DataRow dr = null;
                    //dr = dt.NewRow();
                    //dr["Tipo"] = tipoNC.Value;
                    //dr["IdCaso"] = idCasoText.Value;
                    //dr["Estado"] = ComboEstado.Value;
                    //dt.Rows.Add(dr);
                    ////dr = dt.NewRow();

                    ////Store the DataTable in ViewState
                    //ViewState["TablaActual"] = dt;
                    //gridNC.DataSource = dt;
                    //gridNC.DataBind();

                    //listEntradas.Items.Add(entradaNueva);
                    //ItemsGrid.
                    //LIMPIAR CAMPOS AQUI SI ES NECESARIO
                    ViewState["lista_No_Conf_N"] = lista_No_Conf;
                    llenarTabla();
                }

                

                //llenarTabla();


            }//para modificar
            else {
                if (ViewState["lista_No_Conf_N"] == null)
                {
                    if (ViewState["lista_No_Conf"] != null)
                    {
                        lista_No_Conf = (List<Object[]>)(ViewState["lista_No_Conf"]);//obtiene lista logica con las tuplas de la base de datos
                        ViewState["lista_No_Conf_N"] = lista_No_Conf;
                    }
                }
                lista_No_Conf = (List<Object[]>)(ViewState["lista_No_Conf_N"]);//obtiene lista logica
                Object[] tup = lista_No_Conf[(int)ViewState["indexNC"]];//salva el objeto que va a eliminar
                lista_No_Conf.RemoveAt((int)ViewState["indexNC"]);//elimina el objeto que esta modificando de  la lista logica

                //se asignan los valores que pueden haber cambiado los demas se dejan igual        
                tup[3] = idCasoText.Value;
                tup[4] = tipoNC.Value;
                tup[5] = descripcionText.Value;
                tup[6] = justificacionText.Value;
                tup[7] = ComboEstado.Value;
                tup[8] = imagen.Value;

                lista_No_Conf.Add(tup);
                ViewState["lista_No_Conf_N"] = lista_No_Conf; 
                llenarTabla();
                
                btn_agregarEntrada.InnerText = ("Agregar");
           }

            limpiarTuplas();
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
     
        /*
      * Descripción: carga la lista con los dtos que hay en el viewState o la lista logica, que son los que habian al inicio mas los que se han agregado o modificado a  la lista logica
      * Requiere: object, EventArgs
      * Retorna: n/a
      */
        public void llenarTabla() {
            if (ViewState["lista_No_Conf_N"] == null)
            {
                if (ViewState["lista_No_Conf"]!=null) {
                    lista_No_Conf = (List<Object[]>)(ViewState["lista_No_Conf"]);
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
                    gridNC.DataBind();
                }
            }
            else
            {
                lista_No_Conf = (List<Object[]>)(ViewState["lista_No_Conf_N"]);
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
                gridNC.DataBind();
            }             

        }

        public void llenarTabla(List<Object[]> lista_temporal)
        {
            tablaNC.Clear();
            DataRow dr;
            foreach (var nc in lista_temporal)
            {
                dr = tablaNC.NewRow();
                dr[0] = nc[4];
                dr[1] = nc[3];
                dr[2] = nc[7];

                tablaNC.Rows.Add(dr);
            }
            gridNC.DataBind();
        }
    }
}