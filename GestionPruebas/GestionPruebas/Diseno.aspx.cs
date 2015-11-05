using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionPruebas.App_Code;
using System.Data;
using System.Drawing;
using System.Globalization;

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
                    llenaProys();
                    llenaResps();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void llenaProys()
        {
            proyecto.Items.Clear();
            DataTable dt = controlDiseno.consultaProyectos();
            int[] ids = new int[dt.Rows.Count + 1];
            ids[0] = -1;
            proyecto.Items.Add(new ListItem("Seleccione un Proyecto", "" + 0));
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                DataColumn id = dt.Columns[1];
                ids[i] = parseInt(row[id].ToString());
                DataColumn nombre = dt.Columns[0];
                proyecto.Items.Add(new ListItem(row[nombre].ToString(), "" + i));
                i++;
            }
            ViewState["idsproys"] = ids;
        }

        protected void cambiaProyectoBox(object sender, EventArgs e)
        {
            int[] ids = (int[])ViewState["idsproys"];
            if (proyecto.SelectedIndex != 0)
            {
                ViewState["idproy"] = ids[proyecto.SelectedIndex];
                refrescaGridDis((int)ViewState["idproy"]);
                llenaResps(ids[proyecto.SelectedIndex]);
            } else
            {
                ViewState["idproy"] = null;
                DataTable empty = new DataTable();
                DataView emptyV = empty.DefaultView;
                gridDiseno.DataSource = emptyV;
                gridDiseno.DataBind();
            }
        }


        protected void llenaResps()
        {
            responsable.Items.Clear();
            DataTable dt = controlDiseno.consultaRRHH();
            int[] ceds = new int[dt.Rows.Count + 1];
            ceds[0] = -1;
            responsable.Items.Add(new ListItem("Seleccione un Recurso", "" + 0));
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                DataColumn ced = dt.Columns[1];
                ceds[i] = parseInt(row[ced].ToString());
                DataColumn nombre = dt.Columns[0];
                responsable.Items.Add(new ListItem(row[nombre].ToString(), "" + i));
                i++;
            }
            ViewState["ceds"] = ceds;
        }

        protected void llenaResps(int idProy)
        {
            responsable.Items.Clear();
            DataTable dt = controlDiseno.consultaRRHH(idProy);
            int[] ceds = new int[dt.Rows.Count + 1];
            ceds[0] = -1;
            responsable.Items.Add(new ListItem("Seleccione un Recurso", "" + 0));
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                DataColumn ced = dt.Columns[1];
                ceds[i] = parseInt(row[ced].ToString());
                DataColumn nombre = dt.Columns[0];
                responsable.Items.Add(new ListItem(row[nombre].ToString(), "" + i));
                i++;
            }
            ViewState["ceds"] = ceds;
           
        }

        protected void cambiaResponsableBox(object sender, EventArgs e)
        {
            int[] ceds = (int[])ViewState["ceds"];
            if (proyecto.SelectedIndex != 0)
            {
                ViewState["ced"] = ceds[responsable.SelectedIndex];
                refrescaGridDis((int)ViewState["idproy"]);
            }
            else
            {
                ViewState["ced"] = null;
            }
        }

        protected void refrescaGridDis(int idProy)
        {
            DataTable dt = new DataTable();
            try
            {
                //Realiza la consulta de selección de todos los recursos humanos con la controladora y guarda esa información en un DataTable
                dt = controlDiseno.consultaDisenos(idProy);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + "Error leyendo tabla. Revise su conexión con la base de datos" + "')", true);
            }

            //Crea una vista para llenar el grid
            DataView dv = dt.DefaultView;
            //Liga el grid con la información de la vista
            gridDiseno.DataSource = dv;
            gridDiseno.DataBind();
        }

        protected void refrescaGridReq()
        {
            DataTable dt = new DataTable();
            try
            {
                //Realiza la consulta de selección de todos los recursos humanos con la controladora y guarda esa información en un DataTable
                dt = controlDiseno.consultaRequerimientos();
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + "Error leyendo tabla. Revise su conexión con la base de datos" + "')", true);
            }

            //Crea una vista para llenar el grid
            DataView dv = dt.DefaultView;
            //Liga el grid con la información de la vista
            gridReq.DataSource = dv;
            gridReq.DataBind();
        }

        protected void llenaReqs(int idDise)
        {
            int d = 0;
            DataTable reqDisp = controlDiseno.consultaReqDisponibles(idDise);
            DisponiblesChkBox.Items.Clear();
            foreach (DataRow r in reqDisp.Rows)
            {
                DisponiblesChkBox.Items.Add(new ListItem(r[0].ToString() + " - " + r[1].ToString(), "" + d));
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

        protected void llenaReqs()
        {
            AsignadosChkBox.Items.Clear();
            int d = 0;
            DataTable reqs = controlDiseno.consultaRequerimientos();
            DisponiblesChkBox.Items.Clear();
            foreach (DataRow r in reqs.Rows)
            {
                DisponiblesChkBox.Items.Add(new ListItem(r[0].ToString() + " - " + r[1].ToString(), "" + d));
                d++;
            }
        }

        protected void llenaCampos(EntidadDiseno dise)
        {
            ViewState["idDiseno"] = dise.Id;
            ViewState["idDiseAct"] = ViewState["idDiseno"];
            llenaReqs(dise.Id);
            proposito.Value = dise.Proposito;
            ambiente.Value = dise.Ambiente;
            procedimiento.Value = dise.Procedimiento;
            criterios.Value = dise.Criterios;
            calendario.Value = dise.Fecha.ToString("yyy-MM-dd", CultureInfo.InvariantCulture);
            int[] ceds = (int[])ViewState["ceds"];
            for (int i = 0; i < (ceds.Length); i++)
            {
                if (ceds[i] == dise.Responsable)
                {
                    responsable.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < tecnica.Items.Count; i++)
            {
                if (tecnica.Items[i].Value == dise.Tecnica)
                {
                    tecnica.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < nivel.Items.Count; i++)
            {
                if (nivel.Items[i].Value == dise.Nivel)
                {
                    nivel.SelectedIndex = i;
                    break;
                }
            }
        }

        protected void llenaCamposReq(string id, string nombre)
        {
            ViewState["idReq"] = id;
            idReq.Value = id;
            nomReq.Value = nombre;
        }

        protected void habilitarAdmReq(object sender, EventArgs e)
        {
            refrescaGridReq();
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
            limpiarCampos();
            llenarComboBox();
            btnAceptarDiseno.Text = "Aceptar";
            btnAceptarReq.Text = "Aceptar";
            btnAceptarReq.Enabled = true;
            btnCancelarReq.Disabled = false;
            btnAceptarDiseno.Enabled = true;
            btnCancelarDiseno.Disabled = false;
            btnModificar.Disabled = true;
            btnEliminar.Disabled = true;
            volver.Enabled = false;
            llenaReqs();
            habilitarCampos();
            admReq.Disabled = false;
            titFunc.InnerText = "Insertar";
        }

        protected void habilitarParaModificar(object sender, EventArgs e)
        {
            proyecto.Enabled = false;
            //modifica requerimiento
            if (!(idReq.Value.Equals("")) || !(proposito.Value.Equals("")))
            {
                titFunc.InnerText = "Modificar";
                btnAceptarDiseno.Text = "Guardar";
                btnAceptarReq.Text = "Guardar";
                btnInsertar.Disabled = true;
                btnEliminar.Disabled = true;
                habilitarCampos();
                btnAceptarDiseno.Enabled = true;
                btnCancelarDiseno.Disabled = false;
                btnAceptarReq.Enabled = true;
                btnCancelarReq.Disabled = false;
            volver.Enabled = false;
                admReq.Disabled = true;

                ViewState["idReq"] = idReq.Value;
                ViewState["nomReq"] = nomReq.Value;
            }
            else
            {
                string advertencia = "Seleccione un Dato a Modificar";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + advertencia + "')", true);
            }


            //modifica diseño 
            if ( !string.IsNullOrWhiteSpace(proposito.Value) && !string.IsNullOrWhiteSpace(nivel.Value) && !string.IsNullOrWhiteSpace(tecnica.Value)
              && !string.IsNullOrWhiteSpace(ambiente.Value) && !string.IsNullOrWhiteSpace(calendario.Value) && !string.IsNullOrWhiteSpace(responsable.Text))
            {
  
            }

            }

        protected void cancelarReq(object sender, EventArgs e)
        {
            btnAceptarReq.Enabled = false;
            btnCancelarReq.Disabled = true;
            btnInsertar.Disabled = false;
            btnModificar.Disabled = false;
            btnEliminar.Disabled = false;
            inhabilitarCampos();
            limpiarCampos();
            titFunc.InnerText = "Seleccione una acción a ejecutar";
        }

        protected void cancelarDiseno(object sender, EventArgs e)
        {
            btnEliminar.Disabled = false;
            btnInsertar.Disabled = false;
            btnModificar.Disabled = false;
            inhabilitarCampos();
            limpiarCampos();
            btnCancelarDiseno.Disabled = true;
            btnAceptarDiseno.Enabled = false;
            titFunc.InnerText = "Seleccione una acción a ejecutar";

            nivel.Items.Add("Unitaria");
            nivel.Items.Add("De Integración");
            nivel.Items.Add("Del Sistema");
            nivel.Items.Add("De Aceptación");

            tecnica.Items.Add("Caja Blanca");
            tecnica.Items.Add("Caja Negra");
            tecnica.Items.Add("Exploratoria");


        }

        protected void aceptarReq(object sender, EventArgs e)
        {
            if (btnAceptarReq.Text.Equals("Aceptar"))
                {
                    if (controlDiseno.revisarReqExistente(idReq.Value) == false)
                    {
                    string id = idReq.Value;
                    string nom = nomReq.Value;
                    controlDiseno.insertarReq(id, nom);
                    string confirmado = "Requerimiento Insertado";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + confirmado + "')", true);

                    inhabilitarCampos();
                    btnAceptarReq.Enabled = false;
                    btnCancelarReq.Disabled = true;
                    btnEliminar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnInsertar.Disabled = false;
                    volver.Enabled = true;
                    titFunc.InnerText = "Seleccione una acción a ejecutar";
                    refrescaGridReq();
                }
                 else
                 {
                   string advertencia = "Este ID de Requerimiento ya Existe!";
                   Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + advertencia + "')", true);
                 }
            }
           

           if(btnAceptarReq.Text.Equals("Guardar"))
           {
               if(ViewState["idReq"].Equals(idReq.Value) || controlDiseno.revisarReqExistente(idReq.Value) == false)
               {
                   modificarReq();

                   inhabilitarCampos();
                   btnAceptarReq.Enabled = false;
                   btnCancelarReq.Disabled = true;
                   btnEliminar.Disabled = false;
                   btnModificar.Disabled = false;
                   btnInsertar.Disabled = false;
                   volver.Enabled = true;
                   titFunc.InnerText = "Seleccione una acción a ejecutar";
                   refrescaGridReq();
               }
               else
               {
                   string advertencia = "Este ID de Requerimiento ya Existe!";
                   Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + advertencia + "')", true);
               }
                
           }
        }
           

        protected void habilitarCampos()
        {
            idReq.Disabled = false;
            nomReq.Disabled = false;
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
            responsable.Enabled = true;
        }

        protected void inhabilitarCampos()
        {
            idReq.Disabled = true;
            nomReq.Disabled = true;
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
            responsable.Enabled = false;
            volver.Enabled = true;
            admReq.Disabled = false;
        }

        protected void limpiarCampos()
        {
            ViewState["idDiseno"] = null;
            ViewState["ced"] = null;
            ViewState["idReq"] = null;
            idReq.Value = "";
            nomReq.Value = "";
            DisponiblesChkBox.Items.Clear();
            AsignadosChkBox.Items.Clear();
            proposito.Value = "";
            nivel.Items.Clear();
            tecnica.Items.Clear();
            procedimiento.Value = "";
            ambiente.Value = "";
            criterios.Value = "";
            calendario.Value = "";
            responsable.SelectedIndex = 0;
            AsignadosChkBox.Items.Clear();
            DisponiblesChkBox.Items.Clear();
        }
        protected void llenarComboBox() {
            //llena combobox de nivel de prueba          
            nivel.Items.Add("Unitaria");
            nivel.Items.Add("De Integración");
            nivel.Items.Add("Del Sistema");
            nivel.Items.Add("De Acepatación");

            //llenar comboBox de tecnica de prueba
           tecnica.Items.Add("Caja Negra");
            tecnica.Items.Add("Caja Blanca");
            tecnica.Items.Add("Exploratoria");

        }

        /**
         * Descripcion: Da formato a cada fila cuando se le liga la información a la misma.
         * Recibe: objeto @sender. No se utiliza
         *         EventArgs @e. Determina los datos de la fila actual
         * No devuelve nada.               
         */
        protected void gridDiseno_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Si el tipo de la fila es de datos
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Le da formato a la fila seleccionada
                if (e.Row.RowIndex == gridDiseno.SelectedIndex)
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
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridDiseno, "Select$" + e.Row.RowIndex);
            }
        }

        /**
         * Descripcion: Carga los campos y combobox con la información del diseño seleccionado en el grid.
         * Utiliza la cedula y realiza una consulta SQL mediante la controladora de BD para obtener la información
         * completa del recurso. 
         * Recibe: object @sender. No se utiliza
         *         EventArgs @e. No se utiliza
         */
        protected void seleccionaGridDis(object sender, EventArgs e)
        {
            //Le da formato a toda la tabla
            foreach (GridViewRow row in gridDiseno.Rows)
            {
                //Formato de fila seleccionada
                if (row.RowIndex == gridDiseno.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#0099CC");
                    row.ToolTip = "Esta fila está seleccionada!";
                    row.ForeColor = ColorTranslator.FromHtml("#000000");
                    row.Attributes["onmouseout"] = "this.style.backgroundColor='#0099CC';";

                    //id.Value = row.Cells[0].Text;
                    int id = parseInt(row.Cells[0].Text);

                    EntidadDiseno disenoSel = controlDiseno.consultaDiseno(id);
                    llenaCampos(disenoSel);
                    inhabilitarCampos();
                    btnInsertar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnEliminar.Disabled = false;
                    btnAceptarDiseno.Enabled = false;
                    btnCancelarDiseno.Disabled = true;
                    titFunc.InnerText = "Consultar";
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

        /**
         * Descripcion: Da formato a cada fila cuando se le liga la información a la misma.
         * Recibe: objeto @sender. No se utiliza
         *         EventArgs @e. Determina los datos de la fila actual
         * No devuelve nada.               
         */
        protected void gridReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Si el tipo de la fila es de datos
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Le da formato a la fila seleccionada
                if (e.Row.RowIndex == gridReq.SelectedIndex)
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
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridReq, "Select$" + e.Row.RowIndex);
            }
        }

        /**
         * Descripcion: Carga los campos y combobox con la información del requerimiento seleccionado en el grid.
         * Utiliza la cedula y realiza una consulta SQL mediante la controladora de BD para obtener la información
         * completa del recurso. 
         * Recibe: object @sender. No se utiliza
         *         EventArgs @e. No se utiliza
         */
        protected void seleccionaGridReq(object sender, EventArgs e)
        {
            //Le da formato a toda la tabla
            foreach (GridViewRow row in gridReq.Rows)
            {
                //Formato de fila seleccionada
                if (row.RowIndex == gridReq.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#0099CC");
                    row.ToolTip = "Esta fila está seleccionada!";
                    row.ForeColor = ColorTranslator.FromHtml("#000000");
                    row.Attributes["onmouseout"] = "this.style.backgroundColor='#0099CC';";

                    //id.Value = row.Cells[0].Text;
                    string id = row.Cells[0].Text;
                    string nombre = row.Cells[1].Text;
                    
                    llenaCamposReq(id, nombre);
                    inhabilitarCampos();
                    btnInsertar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnEliminar.Disabled = false;
                    btnAceptarDiseno.Enabled = false;
                    btnCancelarDiseno.Disabled = true;
                    titFunc.InnerText = "Consultar";
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

        protected void modificarReq()
        {
            string idViejo = (string)ViewState["idReq"];
            string nomViejo = (string)ViewState["nomReq"];
            controlDiseno.modificarReq(idViejo, nomViejo, idReq.Value, nomReq.Value);
            string confirmado = "Modificaciones Guardadas!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + confirmado + "')", true);
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
            if (panelDiseno.Visible)
            {
                btnEliminar.Disabled = false;
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
                btnCancelarDiseno.Disabled = true;
                    btnEliminar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnInsertar.Disabled = false;
                    limpiarCampos();
                    inhabilitarCampos();
                    refrescaGridDis((int)ViewState["idproy"]);
                }
                //Si el usuario no seleccionó un recurso del grid se le muestra un mensaje de alerta
                else
                {
                    string faltantes = "Debe seleccionar un diseño en la tabcla primero.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);
                }
                btnAceptarDiseno.Visible = true;
                btnCancelarDiseno.Visible = true;
            }
            else
            {
                btnEliminar.Disabled = false;
                btnCancelarDiseno.Visible = false;
                btnAceptarDiseno.Visible = false;

                //Revisa que se haya seleccionado un recurso del grid
                if (ViewState["idReq"] != null)
                {
                    string idReq = (string)ViewState["idReq"];

                    //Realiza la consulta que elimina recurso de la base de datos
                    int resultado = controlDiseno.eliminaRequerimiento(idReq);

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
                    btnCancelarDiseno.Disabled = true;
                    btnEliminar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnInsertar.Disabled = false;
                    limpiarCampos();
                    inhabilitarCampos();
                    refrescaGridReq();
                }
                //Si el usuario no seleccionó un recurso del grid se le muestra un mensaje de alerta
                else
                {
                    string faltantes = "Debe seleccionar un requerimiento en la tabla primero.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);
                }
                btnAceptarDiseno.Visible = true;
                btnCancelarDiseno.Visible = true;
            }
        }

        protected void btnAceptar_Insertar(object sender, EventArgs e)
        {

            if (btnInsertar.Disabled == false)
            {
                Object[] dis = new Object[10];
                dis[0] = 0;
                dis[1] = criterios.Value;
                dis[2] = nivel.Value;
                dis[3] = tecnica.Value;
                dis[4] = ambiente.Value;
                dis[5] = procedimiento.Value;
                dis[6] = calendario.Value;
                dis[7] = proposito.Value;
                dis[8] = ViewState["ced"];
                dis[9] = ViewState["idproy"];


                int resultado = controlDiseno.insertarDiseno(dis);

                // int resultado = 1;
                string resultadoS = "";
                switch (resultado)
                {
                    //0: todo correcto
                    case 1:
                        resultadoS = "Se insertó la información correctamente";

                        break;
                    //Error en insercion de diseño
                    case -1:
                        resultadoS = "Error al insertar un nuevo diseño";
                        break;
                }
                if (resultado == 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + resultadoS + "')", true);
                    //Se inhabilitan campos. Se devuelve el estado de inicio de los botones.

                }
                //Si hubo algun error
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                }

            }
            else {
                if (btnModificar.Disabled == false) {
                    Object[] dis_Actual = new Object[10];
                    dis_Actual[0] = ViewState["idDiseAct"];
                    dis_Actual[1] =ViewState["criterios"];
                    dis_Actual[2] = ViewState["nivel"];
                    dis_Actual[3] = ViewState["tecnica"];
                    dis_Actual[4] = ViewState["ambiente"];
                    dis_Actual[5] =ViewState["procedimiento"];
                    dis_Actual[6] = ViewState["calendario"];
                    dis_Actual[7] = ViewState["proposito"];
                    dis_Actual[8] = ViewState["ced"];
                    dis_Actual[9] = ViewState["idproy"];

                    Object[] dis_Nuevo = new Object[10];
                    dis_Nuevo[0] = 0;
                    dis_Nuevo[1] = criterios.Value;
                    dis_Nuevo[2] = nivel.Value;
                    dis_Nuevo[3] = tecnica.Value;
                    dis_Nuevo[4] = ambiente.Value;
                    dis_Nuevo[5] = procedimiento.Value;
                    dis_Nuevo[6] = calendario.Value;
                    dis_Nuevo[7] = proposito.Value;
                    dis_Nuevo[8] = ViewState["ced"];
                    dis_Nuevo[9] = ViewState["idproy"];

                    EntidadDiseno  resultado = controlDiseno.modificarDiseno(dis_Actual, dis_Nuevo);

                    string confirmado = "";
                    confirmado = "Modifcaciones Guardadas!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + confirmado + "')", true);

                    //asigna los nuevos valores
                    procedimiento.Value = resultado.Procedimiento;
                    nivel.Items.Clear();
                    if (resultado.Nivel=="Unitaria") {
                        nivel.Items.Add(resultado.Nivel);
                        nivel.Items.Add("De Integración");
                        nivel.Items.Add("Del Sistema");
                        nivel.Items.Add("De Aceptación");
                    }
                    if (resultado.Nivel == "De Integración")
                    {
                        nivel.Items.Add(resultado.Nivel);
                        nivel.Items.Add("Unitaria");
                        nivel.Items.Add("Del Sistema");
                        nivel.Items.Add("De Aceptación");
                    }
                    if (resultado.Nivel == "Del Sistema")
                    {
                        nivel.Items.Add(resultado.Nivel);
                        nivel.Items.Add("Unitaria");
                        nivel.Items.Add("De Integración");
                        nivel.Items.Add("De Aceptación");
                    }
                    if (resultado.Nivel == "De Aceptación")
                    {
                        nivel.Items.Add(resultado.Nivel);
                        nivel.Items.Add("Unitaria");
                        nivel.Items.Add("De Integración");
                        nivel.Items.Add("Del Sistema");
                    }

                    tecnica.Items.Clear();
                    if (resultado.Tecnica=="Caja Negra") {
                        tecnica.Items.Add(resultado.Tecnica);
                        tecnica.Items.Add("Caja Blanca");
                        tecnica.Items.Add("Exploratoria");
                    }
                    if (resultado.Tecnica == "Caja Blanca")
                    {
                        tecnica.Items.Add(resultado.Tecnica);
                        tecnica.Items.Add("Caja Negra");
                        tecnica.Items.Add("Exploratoria");
                    }
                    if (resultado.Tecnica == "Exploratoria")
                    {
                        tecnica.Items.Add(resultado.Tecnica);
                        tecnica.Items.Add("Caja Blanca");
                        tecnica.Items.Add("Caja Negra");
                    }

                    ambiente.Value = resultado.Ambiente;
                    procedimiento.Value = resultado.Procedimiento;
                    criterios.Value = resultado.Criterios;

                    //si se cambio el responsable
                   if (ViewState["ced"] ==null) {
                        responsable.Items.Clear();
                        responsable.Items.Add(controlDiseno.obtenerRH(resultado.Responsable));
                    }
                    DateTime dt = resultado.Fecha;
                    calendario.Value = dt.ToString("yyy-MM-dd", CultureInfo.InvariantCulture);


                }

            }
           
            refrescaGridDis((int) ViewState["idproy"]);

            List<string> listaA = new List<string>();
            List<string> listaD = new List<string>();

            for (int i = 0; i < AsignadosChkBox.Items.Count; i++)
            {
                string ent = AsignadosChkBox.Items[i].ToString();
                string[] nuevo = ent.Split('-');
                listaA.Add(nuevo[0]);
            }

            for (int i = 0; i < DisponiblesChkBox.Items.Count; i++)
            {
                string ent = DisponiblesChkBox.Items[i].ToString();
                string[] nuevo = ent.Split('-');
                listaD.Add(nuevo[0]);
            }

            int idDiseno = -1;
            if (ViewState["idDiseno"] != null)
            {
                idDiseno = (int)ViewState["idDiseno"];
            }
            controlDiseno.asignarReqs(listaA, listaD,idDiseno);
        }

        protected int parseInt(string valor)
        {
            int parsedInt;
            string trimmed = valor;
            bool parsed = int.TryParse(trimmed.Replace("-", ""), out parsedInt);
            if (!parsed)
            {
                parsedInt = -1;
            }
            return parsedInt;
        }

        /* Descripcion: Encargado de pasar los requerimientos disponibles a requerimientos asignados
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void btnDerecha_Click(object sender, EventArgs e)
        {
            List<int> l = new List<int>();
            int cont = 0;
            foreach (ListItem item in DisponiblesChkBox.Items)
            {
                if (item.Selected)
                {
                    l.Add(cont);
                }
                cont++;
            }

            l.Reverse();
            foreach (var i in l)
            {
                string ent = DisponiblesChkBox.Items[i].ToString();
                AsignadosChkBox.Items.Add(ent);
                DisponiblesChkBox.Items.RemoveAt(i);

                
            }

        }

        /* Descripcion: Encargado de pasar los requerimientos asignados a requerimientos disponibles
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void btnIzquierda_Click(object sender, EventArgs e)
        {

            List<int> l = new List<int>();
            int cont = 0;
            foreach (ListItem item in AsignadosChkBox.Items)
            {
                if (item.Selected)
                {
                    l.Add(cont);
                }
                cont++;
            }

            l.Reverse();
            foreach (var i in l)
            {
                string ent = AsignadosChkBox.Items[i].ToString();
                DisponiblesChkBox.Items.Add(ent);
                AsignadosChkBox.Items.RemoveAt(i);
            }
        }
    }
}