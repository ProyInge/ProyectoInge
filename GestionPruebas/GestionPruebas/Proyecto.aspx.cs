using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionPruebas.App_Code;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace GestionPruebas
{

    public partial class Proyecto : System.Web.UI.Page
    {
        private ControladoraProyecto controladoraProyecto;

        private List<EntidadRecursoH> recursosDisponibles;
        private List<EntidadRecursoH> recursosAsignados;


        /* Descripcion: Carga Pagina, revisa Perfil de Acceso y carga las tuplas en el Grid
         * 
         * REQ: object, EventArgs
         * 
         * RET: N/A
         */

        protected void Page_Load(object sender, EventArgs e)
        {
            controladoraProyecto = new ControladoraProyecto();

            if (Request.IsAuthenticated)
            {
                if (!this.IsPostBack)
                {
                    string nombUsuario = ((SiteMaster)this.Master).nombreUsuario;
                    string perfilUsuario = controladoraProyecto.getPerfil(nombUsuario);
                    if (perfilUsuario.Equals("A"))
                    {
                        refrescarTabla();

                    }
                    else
                    {
                        //consultar informacion del proyecto en el que esta el miembro
                        refrescarTabla();
                        filtro.Visible = false;
                        buscarP.Visible = false;
                        EntidadProyecto proyectoM = controladoraProyecto.consultarProyectoMiembro(nombUsuario);
                        llenaDatosProyecto(proyectoM);
                    }

                }



            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            string usuario = ((SiteMaster)this.Master).nombreUsuario;
            string perfil = controladoraProyecto.getPerfil(usuario);
            revisarPerfil(perfil);

            if (ViewState["recursosDisponibles"] != null)
            {
                recursosAsignados = (List<EntidadRecursoH>)ViewState["recursosAsignados"];
                recursosDisponibles = (List<EntidadRecursoH>)ViewState["recursosDisponibles"];
            }
        }

        /* Descripcion: Encargado de pasar los recursos disponibles a recursos asignados
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
                EntidadRecursoH ent = new EntidadRecursoH(recursosDisponibles[i]);
                recursosAsignados.Add(ent);
                recursosDisponibles.RemoveAt(i);
            }

            actualizarViewState();
            actualizarCheckBoxList();
        }

        /* Descripcion: Encargado de pasar los recursos asignados a recursos disponibles
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
                EntidadRecursoH ent = new EntidadRecursoH(recursosAsignados[i]);
                recursosDisponibles.Add(ent);
                recursosAsignados.RemoveAt(i);
            }

            actualizarViewState();
            actualizarCheckBoxList();
        }

        /* Descripcion: Se encarga de actualizar la vista de los recursos
        * 
        * REQ: N/A
        * 
        * RET: N/A
        */

        protected void actualizarViewState()
        {
            ViewState["recursosAsignados"] = recursosAsignados;
            ViewState["recursosDisponibles"] = recursosDisponibles;
        }

        /* Descripcion: Despliega los recursos de forma Nombre,Apellido,Apellido y Rol
        * 
        * REQ: EntidadRecursoH
        * 
        * RET: String
        */

        protected string getNombreBonito(EntidadRecursoH e)
        {
            return e.Nombre + " " + e.PApellido + " " + e.SApellido + " - " + e.Rol;
        }

        /* Descripcion: Actualiza los recursos que existen cuando se presiona algun boton
        * 
        * REQ: N/A
        * 
        * RET: N/A
        */

        protected void actualizarCheckBoxList()
        {
            int c = 0;
            DisponiblesChkBox.Items.Clear();
            foreach (var r in recursosDisponibles)
            {
                DisponiblesChkBox.Items.Add(new ListItem(getNombreBonito(r), "rec" + c.ToString()));
                c++;
            }

            AsignadosChkBox.Items.Clear();
            foreach (var r in recursosAsignados)
            {
                AsignadosChkBox.Items.Add(new ListItem(getNombreBonito(r), "rec" + c.ToString()));
                c++;
            }
        }

        /* Descripcion: Revisa el Perfil que posee el usuario para determinar que mostrar en pantalla
        * 
        * REQ: string
        * 
        * RET: N/A
        */

        protected void revisarPerfil(string perfil)
        {
            if (perfil.Equals("M"))
            {
                btnInsertar.Visible = false;
                btnAceptarInsertar.Visible = false;
                btnCancelarInsertar.Visible = false;
            }
        }

        /* Descripcion: Caso en que se seleccione el boton de Insertar, inhabilita y habilita los botones para el caso
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            //alerta.Visible = false;
            //alertaCorrecto.Visible = false;

            btnEliminar.Disabled = true;
            btnModificar.Disabled = true;
            btnAceptarInsertar.Enabled = true;
            btnCancelarInsertar.Disabled = false;
            habilitarCampos();

            limpiarCampos();

            barraEstado.Items.Add("Pendiente");
            barraEstado.Items.Add("Asignado");
            barraEstado.Items.Add("En Ejecucion");
            barraEstado.Items.Add("Finalizado");
            barraEstado.Items.Add("Cerrado");

            lider.Items.Clear();

            List<string> lideres = controladoraProyecto.seleccionarLideres();

            int i = 0;
            while (i <= lideres.Count - 1)
            {
                lider.Items.Add(new ListItem(lideres.ElementAt(i)));
                i++;
            }

            recursosAsignados = new List<EntidadRecursoH>();
            recursosDisponibles = controladoraProyecto.getRecursosDisponibles();

            actualizarViewState();
            actualizarCheckBoxList();

        }

        /* Descripcion: Caso en que se seleccione el boton de modificar, habilita y inhabilita recursos para el caso
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //alerta.Visible = false;
            //alertaCorrecto.Visible = false;

            if (!string.IsNullOrWhiteSpace(nombreProyecto.Value))
            {
                ViewState["nombreProyectoActual"] = nombreProyecto.Value;
                string datLider = lider.Value;
                string[] words = datLider.Split(' ');
                ViewState["liderActual"] = words[0];
                ViewState["Tel1"] = telefonoOficina.Value;
                ViewState["Tel2"] = tel2.Value;

                btnInsertar.Disabled = true;
                btnEliminar.Disabled = true;
                btnAceptarInsertar.Visible = false;
                btnCancelarInsertar.Visible = false;
                btnGuardarModificar.Enabled = true;
                btnCancelarModificar.Disabled = false;
                btnGuardarModificar.Visible = true;
                btnCancelarModificar.Visible = true;

                habilitarCampos();

                string est = barraEstado.Value;

                barraEstado.Items.Clear();

                if (est.Equals("Pendiente"))
                {
                    barraEstado.Items.Add(est);
                    barraEstado.Items.Add("Asignado");
                    barraEstado.Items.Add("En Ejecucion");
                    barraEstado.Items.Add("Finalizado");
                    barraEstado.Items.Add("Cerrado");
                }

                if (est.Equals("Asignado"))
                {
                    barraEstado.Items.Add(est);
                    barraEstado.Items.Add("Pendiente");
                    barraEstado.Items.Add("En Ejecucion");
                    barraEstado.Items.Add("Finalizado");
                    barraEstado.Items.Add("Cerrado");
                }

                if (est.Equals("En Ejecucion"))
                {
                    barraEstado.Items.Add(est);
                    barraEstado.Items.Add("Asignado");
                    barraEstado.Items.Add("Pendiente");
                    barraEstado.Items.Add("Finalizado");
                    barraEstado.Items.Add("Cerrado");
                }

                if (est.Equals("Finalizado"))
                {
                    barraEstado.Items.Add(est);
                    barraEstado.Items.Add("Asignado");
                    barraEstado.Items.Add("En Ejecucion");
                    barraEstado.Items.Add("Pendiente");
                    barraEstado.Items.Add("Cerrado");
                }

                if (est.Equals("Cerrado"))
                {
                    barraEstado.Items.Add(est);
                    barraEstado.Items.Add("Asignado");
                    barraEstado.Items.Add("En Ejecucion");
                    barraEstado.Items.Add("Finalizado");
                    barraEstado.Items.Add("Pendiente");
                }

                List<string> lideres = controladoraProyecto.seleccionarLideres();

                int i = 0;
                while (i <= lideres.Count - 1)
                {
                    lider.Items.Add(new ListItem(lideres.ElementAt(i)));
                    i++;
                }

            }
            else
            {
                string faltante = "Seleccione un Proyecto a Modificar";
                //textoAlerta.InnerHtml = "Seleccione un Proyecto a Modificar";
                //alerta.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltante + "')", true);

            }


        }

        /* Descripcion: Acciona la alerta de verificar si desea eliminar algo
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //alerta.Visible = false;
            //alertaCorrecto.Visible = false;
            string eliminado = "";

            string usuario = ((SiteMaster)this.Master).nombreUsuario;
            string perfil = controladoraProyecto.getPerfil(usuario);

            switch (perfil)
            {
                case "M":
                    {
                        if (!string.IsNullOrWhiteSpace(nombreProyecto.Value))
                        {
                            Object[] borrar = new Object[1];
                            Object[] vacio2 = new Object[1];
                            borrar[0] = nombreProyecto.Value;
                            controladoraProyecto.cambiarEstado(nombreProyecto.Value);
                            //textoConfirmacion.InnerHtml = "Eliminado Correctamente!";
                            //alertaCorrecto.Visible = true;
                            eliminado = "Proyecto Cancelado!";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + eliminado + "')", true);

                            //alertaCorrecto.Visible = true;

                        }
                        else
                        {
                            //textoAlerta.InnerHtml = "Seleccione un Proyecto a Eliminar";
                            //alerta.Visible = true;
                            eliminado = "Seleccione un Proyecto a Eliminar";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + eliminado + "')", true);
                        }
                        break;
                    }

                case "A":
                    {
                        if (!string.IsNullOrWhiteSpace(nombreProyecto.Value))
                        {
                            Object[] borrar = new Object[1];
                            Object[] vacio2 = new Object[1];
                            borrar[0] = nombreProyecto.Value;
                            controladoraProyecto.ejecutarProyecto(4, borrar, vacio2);
                            //textoConfirmacion.InnerHtml = "Eliminado Correctamente!";
                            //alertaCorrecto.Visible = true;
                            limpiarCampos();
                            eliminado = "Eliminado Correctamente!";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + eliminado + "')", true);
                            refrescarTabla();
                            //alertaCorrecto.Visible = true;

                        }
                        else
                        {
                            //textoAlerta.InnerHtml = "Seleccione un Proyecto a Eliminar";
                            //alerta.Visible = true;
                            eliminado = "Seleccione un Proyecto a Eliminar";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + eliminado + "')", true);

                        }
                        break;
                    }
            }

        }

        /* Descripcion: Encargado de pasar los recursos disponibles a recursos asignados
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void btnCancelar_Insertar(object sender, EventArgs e)
        {
            btnEliminar.Disabled = false;
            btnModificar.Disabled = false;
            btnAceptarInsertar.Enabled = false;
            btnCancelarInsertar.Disabled = true;
            inhablitarCampos();
            //alerta.Visible = false;
            //alertaCorrecto.Visible = false;

            limpiarCampos();

            // limpia checkboxlists
            recursosAsignados = new List<EntidadRecursoH>();
            recursosDisponibles = controladoraProyecto.getRecursosDisponibles();
            actualizarViewState();
            actualizarCheckBoxList();

            tel2.Value = "";
            lider.Items.Clear();
        }

        /* Descripcion: Caso de Insertar un Proyecto, verifica cierta informacion de entrada
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void btnAceptar_Insertar(object sender, EventArgs e)
        {
                int existe = revisarExistentes();
                if (existe > 0)
                {
                    string faltantes = "";
                    //alertaCorrecto.Visible = false;

                    switch (existe)
                    {
                        case 1:
                            {
                                faltantes = "Ya existe ese Nombre de Proyecto\\n";
                                //textoAlerta.InnerHtml = faltantes;
                                //alerta.Visible = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);

                                break;
                            }

                        case 2:
                            {
                                faltantes = "Ya existe ese Nombre de Oficina \\n";
                                //textoAlerta.InnerHtml = faltantes;
                                //alerta.Visible = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);

                                break;
                            }

                        case 3:
                            {
                                faltantes = "Ya existe ese Nombre de Proyecto \\n Y ese Nombre de Oficina ";
                                //textoAlerta.InnerHtml = faltantes;
                                //alerta.Visible = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);
                                break;
                            }
                    }
                }
                else
                {
                    if ((!string.IsNullOrWhiteSpace(tel2.Value) && !(tel2.Value.Equals(telefonoOficina.Value))) || (string.IsNullOrWhiteSpace(tel2.Value)))
                    {

                        Object[] dat = new Object[11];
                        Object[] vacio = new Object[1];
                        string l = lider.Value;
                        string ll = l.Substring(0, 9);
                        dat[0] = nombreProyecto.Value;
                        dat[1] = objetivo.Text;
                        dat[2] = barraEstado.Value;
                        dat[3] = calendario.Value;
                        dat[4] = nombreOficina.Value;
                        dat[5] = representante.Value;
                        dat[6] = correoOficina.Value;
                        dat[8] = ll;//cedula lider
                        dat[9] = "";//nombre lider

                        if ((string.IsNullOrWhiteSpace(telefonoOficina.Value)))
                        {
                            //dat[10] = 0;//tel2.Value;//telefono 2
                            dat[7] = 0;//tel 1
                        }
                        else
                        {
                            dat[7] = telefonoOficina.Value;//tel 1
                        }

                        controladoraProyecto.ejecutarProyecto(1, dat, vacio);

                        if (!string.IsNullOrWhiteSpace(tel2.Value))
                        {
                            controladoraProyecto.insertarTel2(tel2.Value, nombreOficina.Value);
                        }

                        //alerta.Visible = false;
                        string confirmado = "Proyecto Insertado!";

                        //alertaCorrecto.Visible = true;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + confirmado + "')", true);

                        // codigo de emma
                        foreach (var r in recursosAsignados)
                        {
                            controladoraProyecto.asignarProyectoAEmpleado(nombreProyecto.Value, r);
                        }

                        //alerta.Visible = false;
                        //alertaCorrecto.Visible = true;


                        btnEliminar.Disabled = false;
                        btnModificar.Disabled = false;
                        btnAceptarInsertar.Enabled = false;
                        btnCancelarInsertar.Disabled = true;
                        inhablitarCampos();
                    }
                    else
                    {
                        //alertaCorrecto.Visible = false;
                        revisarDatos();
                    }
                }
            refrescarTabla();
        }

        /* Descripcion: 
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void btnGuardar_Modificar(object sender, EventArgs e)
        {

            if ((!string.IsNullOrWhiteSpace(tel2.Value) && !(tel2.Value.Equals(telefonoOficina.Value))) || (string.IsNullOrWhiteSpace(tel2.Value)))
            {
                //crea objeto con el nombre del proyecto que se obtuvo antes de los cambio
                Object[] datosOriginales = new Object[11];
                datosOriginales[0] = ViewState["nombreProyectoActual"].ToString();
                datosOriginales[1] = ViewState["liderActual"].ToString();
                datosOriginales[2] = ViewState["Tel1"].ToString();
                datosOriginales[3] = ViewState["Tel2"].ToString();

                //separa los datos del lider (cedula nombre)
                string datLider = lider.Value;
                string[] words = datLider.Split(' ');

                //objeto que tiene los datos
                Object[] datos = new Object[11];
                datos[0] = nombreProyecto.Value;    //nombre           
                datos[1] = objetivo.Text;           //objetivo
                datos[2] = barraEstado.Value;       //estado
                datos[3] = calendario.Value;        //fechaAsgnacion
                datos[4] = nombreOficina.Value;     //nombreOficina
                datos[5] = representante.Value;     //representante
                datos[6] = correoOficina.Value;     //cooreoOficina
                if (string.IsNullOrWhiteSpace(telefonoOficina.Value))
                {
                    datos[7] = 0;
                }
                else
                {
                    datos[7] = telefonoOficina.Value;   //telOficina 
                }
                datos[8] = words[0];     // lider.Value;//cedula lider                      
                datos[9] = words[1];                //nombreLider

                if (!string.IsNullOrWhiteSpace(tel2.Value))
                {
                    datos[10] = tel2.Value;         //tel2
                }
                else
                {
                    datos[10] = 0;                  //tel2
                }

                EntidadProyecto en = controladoraProyecto.actProy(datos, datosOriginales);

                string usuario = ((SiteMaster)this.Master).nombreUsuario;
                string perfil = controladoraProyecto.getPerfil(usuario);
                refrescarTabla();

                string confirmado = "";
                confirmado = "Modifcaciones Guardadas!";

                if (recursosAsignados != null)
                {
                    foreach (var r in recursosAsignados)
                    {
                        controladoraProyecto.asignarProyectoAEmpleado(nombreProyecto.Value, r);
                    }
                }

                if (recursosDisponibles != null)
                {
                    foreach (var r in recursosDisponibles)
                    {
                        controladoraProyecto.asignarProyectoAEmpleado("", r);
                    }
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + confirmado + "')", true);

                btnInsertar.Disabled = false;
                btnEliminar.Disabled = false;
                inhablitarCampos();
                btnGuardarModificar.Visible = false;
                btnCancelarModificar.Visible = false;
                btnAceptarInsertar.Visible = true;
                btnCancelarInsertar.Visible = true;
            }
            else
            {
                revisarDatos();
            }
        }

        /* Descripcion: Limpia y Cancela la modificacion
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void btnCancelar_Modificar(object sender, EventArgs e)
        {
            btnInsertar.Disabled = false;
            btnEliminar.Disabled = false;
            btnGuardarModificar.Enabled = false;
            btnCancelarModificar.Disabled = true;
            btnGuardarModificar.Visible = false;
            btnCancelarModificar.Visible = false;
            btnAceptarInsertar.Visible = true;
            btnCancelarInsertar.Visible = true;

            inhablitarCampos();
            //alerta.Visible = false;
            //alertaCorrecto.Visible = false;

                limpiarCampos();
                refrescarTabla();
        }

        /* Descripcion: Alertas de informacion invalida o incompleta
        * 
        * REQ: N/A
        * 
        * RET: N/A
        */

        protected void revisarDatos()
        {
            string faltantes = "";

            if (tel2.Value.Equals(telefonoOficina.Value))
            {
                faltantes = faltantes + "Los telefonos ingresados son iguales, por favor digite uno diferente\\n";
            }

            //textoAlerta.InnerHtml = faltantes;
            //alerta.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);

        }

        /* Descripcion: Accion que verifica informacion repetida
        * 
        * REQ: N/A
        * 
        * RET: N/A
        */

        protected int revisarExistentes()
        {
            int resultado;
            resultado = controladoraProyecto.revisarExistentes(nombreProyecto.Value, nombreOficina.Value);
            return resultado;
        }

        /* Descripcion: Filtra la informacion que se busca en el Grid
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            DataTable dtProyecto = controladoraProyecto.consultar_Total_ProyectoFiltro(filtro.Text);
            DataView dvProyecto = dtProyecto.DefaultView;
            gridProyecto.DataSource = dvProyecto;
            gridProyecto.DataBind();

        }

        /* Descripcion: Metodo complementario de filtrar la informacion del Grid
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void seleccion(object sender, EventArgs e)
        {
            //traer al grid los proyectos que tienen el nombre similar al que se ingresó en el filtro de busqueda
            DataTable dtProyecto = controladoraProyecto.consultar_Total_ProyectoFiltro(filtro.Text);
            DataView dvProyecto = dtProyecto.DefaultView;
            gridProyecto.DataSource = dvProyecto;
            gridProyecto.DataBind();

        }

        /* Descripcion: Metodo que reconoce la seleccion de una tupla en el Grid
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void gridProyecto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == gridProyecto.SelectedIndex)
                {
                    e.Row.ToolTip = "Esta fila está seleccionada!";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='#0099CC';";
                    e.Row.ForeColor = ColorTranslator.FromHtml("#000000");
                    e.Row.BackColor = ColorTranslator.FromHtml("#0099CC");
                }
                else
                {
                    e.Row.ToolTip = "Click para seleccionar esta fila.";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                }
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridProyecto, "Select$" + e.Row.RowIndex);
            }
        }

        /* Descripcion: Funcion Complementaria para seleccionar y mostrar la informacion de la tupla selccionada
        * 
        * REQ: object, EventArgs
        * 
        * RET: N/A
        */

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //cuando se da click sobre una de las filas del grid de proyectos
            //llama a la controladora de proyecto indicando que se debe consultar el proyecto seleccionado del grid
            foreach (GridViewRow row in gridProyecto.Rows)
            {
                if (row.RowIndex == gridProyecto.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#0099CC");
                    row.ToolTip = "Esta fila está seleccionada!";
                    row.ForeColor = ColorTranslator.FromHtml("#000000");
                    row.Attributes["onmouseout"] = "this.style.backgroundColor='#0099CC';";
                    string nombreProy = row.Cells[0].Text;
                    EntidadProyecto proy = controladoraProyecto.consultarProyecto(nombreProy);

                    //carga en los campos los valores que trae la entidad de Proyecto
                    nombreProyecto.Value = nombreProy;
                    objetivo.Text = proy.getObjetivo();
                    string estado = proy.getEstado();
                    barraEstado.Items.Clear();
                    lider.Items.Clear();
                    DateTime dt = proy.getFecha();
                    calendario.Value = dt.ToString("yyy-MM-dd", CultureInfo.InvariantCulture);
                    //concatena la cedula y el nombre del lider
                    string liderP = (proy.getLider()).ToString();
                    string nombreL = proy.getNombreLider();
                    lider.Items.Add(new ListItem(liderP + " " + nombreL));
                    barraEstado.Items.Add(new ListItem(estado));
                    nombreOficina.Value = proy.getNomOf();
                    correoOficina.Value = proy.getCorreoOf();
                    if (proy.getTelOf() != 0)
                    {
                        telefonoOficina.Value = (proy.getTelOf()).ToString();
                    }
                    else
                    {
                        telefonoOficina.Value = "";
                    }
                    //si hay un segundo telefono lo carga tambien, sino solo muestra el primero y no habilita el boton de mostrar el segundo.
                    int num = proy.getTelOf2();
                    if (num != 0)
                    {
                        tel2.Value = (num).ToString();
                        btnTel2.Disabled = false;
                        tel2.Visible = true;
                    }
                    else
                    {
                        btnTel2.Disabled = true;
                    }
                    representante.Value = proy.getRep();
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click para seleccionar esta fila.";
                }
            }

            btnInsertar.Disabled = false;
            btnModificar.Disabled = false;
            btnEliminar.Disabled = false;
            btnAceptarInsertar.Enabled = false;
            btnCancelarInsertar.Disabled = true;
            btnGuardarModificar.Visible = false;
            btnCancelarModificar.Visible = false;

            inhablitarCampos();

            recursosAsignados = controladoraProyecto.getRecursosAsignados(nombreProyecto.Value);
            recursosDisponibles = controladoraProyecto.getRecursosDisponibles();

            actualizarViewState();
            actualizarCheckBoxList();
        }

        /* Descripcion: Llena los datos del proyecto en las casillas correspondientes
       * 
       * REQ:EntidadProyecto
       * 
       * RET: N/A
       */

        public void llenaDatosProyecto(EntidadProyecto proy)
        {

            try
            {
                nombreProyecto.Value = proy.getNombre();
                objetivo.Text = proy.getObjetivo();
                string estado = (proy.getEstado()).ToString();
                barraEstado.Items.Clear();
                lider.Items.Clear();
                DateTime dt = proy.getFecha();
                calendario.Value = dt.ToString("yyy-MM-dd", CultureInfo.InvariantCulture);

                //concatena la cedula y el nombre del lider
                string liderP = (proy.getLider()).ToString();
                string nombreL = proy.getNombreLider();
                lider.Items.Add(new ListItem(liderP + " " + nombreL));

                barraEstado.Items.Add(new ListItem(estado));
                nombreOficina.Value = proy.getNomOf();
                correoOficina.Value = proy.getCorreoOf();
                if (proy.getTelOf() != 0)
                {
                    telefonoOficina.Value = (proy.getTelOf()).ToString();
                }
                else
                {
                    telefonoOficina.Value = "";
                }
                //si hay un segundo telefono lo carga tambien, sino solo muestra el primero y no habilita el boton de mostrar el segundo.
                int num = proy.getTelOf2();
                if (num != 0)
                {
                    tel2.Value = (num).ToString();
                    btnTel2.Disabled = false;
                    tel2.Visible = true;
                }
                else
                {
                    btnTel2.Disabled = true;
                }
                representante.Value = proy.getRep();
            }
            catch
            {

            }

        }

        /* Descripcion: Refresca la tabla de consultar
       * 
       * REQ: n/A
       * 
       * RET: N/A
       */

        public void refrescarTabla()
        {
            string nombUsuario = ((SiteMaster)this.Master).nombreUsuario;
            string perfilUsuario = controladoraProyecto.getPerfil(nombUsuario);

            if (perfilUsuario.Equals("A"))
            {
                DataTable dtProyecto = controladoraProyecto.consultar_Total_Proyecto();
                DataView dvProyecto = dtProyecto.DefaultView;
                gridProyecto.DataSource = dvProyecto;
                gridProyecto.DataBind();
            }
            else
            {
                EntidadProyecto entidad = controladoraProyecto.consultarProyectoMiembro(nombUsuario);
                DataTable dtProyecto = controladoraProyecto.consultar_Total_ProyectoFiltro(entidad.getNombre());
                DataView dvProyecto = dtProyecto.DefaultView;
                gridProyecto.DataSource = dvProyecto;
                gridProyecto.DataBind();
            }
        }

        protected void inhablitarCampos()
        {
            nombreProyecto.Disabled = true;
            objetivo.ReadOnly = true;
            barraEstado.Disabled = true;
            calendario.Disabled = true;
            nombreOficina.Disabled = true;
            representante.Disabled = true;
            correoOficina.Disabled = true;
            telefonoOficina.Disabled = true;
            izquierda.Disabled = true;
            derecha.Disabled = true;
            AsignadosChkBox.Enabled = false;
            DisponiblesChkBox.Enabled = false;
            lider.Disabled = true;
            btnTel2.Disabled = true;
            tel2.Disabled = true;
        }

        protected void limpiarCampos()
        {
            nombreProyecto.Value = "";
            objetivo.Text = "";
            calendario.Value = "";
            nombreOficina.Value = "";
            representante.Value = "";
            correoOficina.Value = "";
            telefonoOficina.Value = "";
            AsignadosChkBox.Items.Clear();
            DisponiblesChkBox.Items.Clear();
            tel2.Value = "";
            barraEstado.Items.Clear();
            lider.Items.Clear();
        }

        protected void habilitarCampos()
        {
            nombreProyecto.Disabled = false;
            objetivo.ReadOnly = false;
            barraEstado.Disabled = false;
            calendario.Disabled = false;
            nombreOficina.Disabled = false;
            representante.Disabled = false;
            correoOficina.Disabled = false;
            telefonoOficina.Disabled = false;
            izquierda.Disabled = false;
            derecha.Disabled = false;
            AsignadosChkBox.Enabled = true;
            DisponiblesChkBox.Enabled = true;
            lider.Disabled = false;
            btnTel2.Disabled = false;
            tel2.Disabled = false;
        }

    }
}