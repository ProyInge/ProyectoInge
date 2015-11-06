using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionPruebas.App_Code;
using System.Data;
using System.Drawing;
using System.Diagnostics;

namespace GestionPruebas
{
    public partial class Casos : System.Web.UI.Page
    {
        private  ControladoraCasos controlCasos;
        private string entradas;
        private string idDise = "-1";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idDise"] != null)
            {
                idDise = Request.QueryString["idDise"];
            }

            //Si ya está logueado:
            if (Request.IsAuthenticated)
            {
                
                //Inicializamos controladora
                controlCasos = new ControladoraCasos();
                hacerResumen(idDise);

                entradas = "";

                //Si es la primera vez que se carga la página:
                if (!this.IsPostBack)
                {
                    //Obtiene usuario logueado
                    string usuarioS = ((SiteMaster)this.Master).nombreUsuario;
                    //Revisa su perfil
                    bool esAdmin = revisarPerfil(usuarioS, true);
                    //btnEliminar.Disabled = true;
                    inhabilitarCampos();

                    if(esAdmin)
                    {
                        refrescaTabla();
                    }
                    
                }

                
                
            }
            else
            {//En caso de que no esté logueado, redirija a login
                Response.Redirect("Login.aspx");
            }
        }

        private void refrescaTabla()
        {
            DataTable dtCaso = controlCasos.consultarCasos(idDise);
            DataView dvCaso = dtCaso.DefaultView;
            gridCasos.DataSource = dvCaso;
            gridCasos.DataBind();
        }

        /*
         * Descripción: Agrega en una lista temporal una entrada nueva a un caso.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btn_agregarEntrada_click(object sender, EventArgs e)
        {
            if (string.Equals(entradaDatos.Value,"") || string.Equals(estadoBox.Value,""))
            {

                string resultadoS = "Debe insertar una entrada con su estado respectivo.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
            }
            else
            {
                string entradaNueva = (string)entradaDatos.Value;
                entradaNueva += " - " + (string)estadoBox.Value;

                listEntradas.Items.Add(entradaNueva);

                entradaDatos.Value = "";
                estadoBox.Value = "";
            }
            
   
        }

        /*
         * Descripción: Se habilitan los campos para poder realizar la inserción de un caso de uso nuevo.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            btnModificar.Disabled = true;
            btnEliminar.Disabled = true;
            limpiarCampos();
            titFunc.InnerText = "Insertar";
            btnAceptar.Text = "Aceptar";
            habilitarCampos();

        }

        /*
        * Descripción: Se habilitan los campos para poder realizar la modificación de un caso de uso nuevo.
        * Requiere: n/a
        * Retorna: n/a
        */
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            titFunc.InnerText = "Modificar";
            btnAceptar.Text = "Guardar";
            habilitarCampos();
            btnInsertar.Disabled = true;
            btnEliminar.Disabled = true;
            ViewState["idCasoV"] = idCaso.Value;           
        }

        /*
        * Descripción: Se habilitan los campos para poder realizar la eliminación de un caso de uso nuevo.
        * Requiere: n/a
        * Retorna: n/a
        */
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            titFunc.InnerText = "Eliminar";
        }

        protected void btnEliminarCaso_Click(object sender, EventArgs e)
        {
            string eliminado = "";
            if (!string.IsNullOrWhiteSpace(idCaso.Value))
            {
                int res = controlCasos.eliminarCaso(idCaso.Value, idDise);
                if(res == 1)
                {
                    eliminado = "Caso de prueba eliminado!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + eliminado + "')", true);
                }
            }
            else
            {
                eliminado = "Seleccione un Caso de prueba a eliminar";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + eliminado + "')", true);
            }
        }
        

        /*
         * Descripción: quita de la lista la entrada seleccionada en el listbox.
         * Requiere: n/a
         * Retorna: n/a
         */
        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            string entradaAQuitar = (string)listEntradas.SelectedValue;
            listEntradas.Items.Remove(entradaAQuitar);
        }



        protected void btnLimpiarLista_Click(object sender, EventArgs e)
        {
            listEntradas.Items.Clear();
        }


        /*
         */ 
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //Si va a insertar
            if (!btnInsertar.Disabled)
            {
                for(var i = 0; i < listEntradas.Items.Count-1; i++)
                {
                    entradas += listEntradas.Items[i] + ", ";
                }
                /* modificacion por emmanuel
                foreach (ListItem entrada in listEntradas.Items)
                {
                    entradas += entrada.Value + ",";
                }*/
               
                string id_caso = idCaso.Value;
                string propositoCaso = proposito.Value;
                string resultado_esperado = resultadoEsperado.Value;
                string flujoCaso = flujo.Value;
                string id_Dise = Request.QueryString["idDise"];

                int idDise = Int32.Parse(id_Dise);

                int resultado = controlCasos.insertarCaso(id_caso, propositoCaso, entradas, resultado_esperado, flujoCaso, idDise, 0);
                string resultadoS;
                switch (resultado)
                {
                    case 1:
                        resultadoS = "Se insertó la información correctamente";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + resultadoS + "')", true);
                        entradaDatos.Value = "";
                        estadoBox.Value = "";
                        idCaso.Value = "";
                        proposito.Value = "";
                        resultadoEsperado.Value = ""; 
                        flujo.Value = "";
                        listEntradas.Items.Clear();
                        titFunc.InnerText = "Seleccione una acción a ejecutar";
                        inhabilitarCampos();
                        btnEliminar.Disabled = false;
                        btnModificar.Disabled = false;
                        break;
                    case 2627:
                        resultadoS = "Ya existe un caso de prueba con este ID";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                        break;
                }
                
                refrescaTabla();

            }
            else if(!btnModificar.Disabled)
            {//Modificación
                if ( idCaso.Value != null && idCaso.Value!="" )
                {//Modifica si se seleccionó previamente un recurso
                    string id_caso = idCaso.Value;
                    string propositoCaso = proposito.Value;
                    string resultado_esperado = resultadoEsperado.Value;
                    string flujoCaso = flujo.Value;
                    string entradas = "";

                    foreach (ListItem entrada in listEntradas.Items)
                    {
                        entradas += entrada.Value + ",";
                    }

                    //Se realiza la consulta SQL de actualizacion con la información ingresada, conectandose a la controladora
                    int idDiseV = parseInt(Request.QueryString["idDise"]);
                    string idViejo = (string)ViewState["idCasoV"];
                    int resultado = controlCasos.modificaCaso(id_caso, propositoCaso, entradas, resultado_esperado, flujoCaso, 0, 0, idViejo, idDiseV);
                    string resultadoS = "";
                    string resultadoS0 = "";
                    //Se revisa estado de la consulta
                    switch (resultado)
                    {
                        //0: todo correcto
                        case 0:
                            resultadoS0 = "Se modificó la información correctamente";
                            break;
                        //error en modificacion de usuario
                        case -1:
                            resultadoS = "Error al modificar la información de la persona";
                            break;
                        //error en modificacion de telefono
                        case 1:
                            resultadoS0 = "No se modificó la información correctamente";
                            break;
                        //2627 violacion propiedad unica
                        case 2627:
                            resultadoS = "El id de caso no está disponible";
                            break;
                        //Error inesperado SQL
                        default:
                            resultadoS = "Error al modificar los datos, intente de nuevo ";
                            break;
                    }
                    //Se muestra un mensaje indicando que todo se realizó correctamente
                    if (resultado == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + resultadoS0 + "')", true);
                        //Se inhabilitan campos y se devuelven botones a su estado de inicio
                        btnAceptar.Text = "Aceptar";
                        btnAceptar.Enabled = false;
                        btnCancelar.Disabled = true;
                        btnEliminar.Disabled = false;
                        btnModificar.Disabled = false;
                        btnInsertar.Disabled = false;
                        inhabilitarCampos();
                        refrescaTabla();
                    }
                    //Se muestra un mensaje inidicando que hubo un error
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                    }

                }
                else
                {
                    string faltantes = "Debe seleccionar un recurso en la tabla primero.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);
                }
            }
        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {           
            limpiarCampos();
            listEntradas.Items.Clear();
            titFunc.InnerText = "Seleccione una acción a ejecutar";
            inhabilitarCampos();
            btnModificar.Disabled = false;
            btnEliminar.Disabled = false;
            btnInsertar.Disabled = false;
        }

        /*
         * Descripción: actualiza el listbox con las entradas que esten en listaEntradas.
         * Requiere: n/a
         * Retorna: n/a
         */ 


        /*
         * Descripción: inhabilita los campos del form.
         * Requiere: n/a
         * Retorna: n/a
         */ 
        protected void inhabilitarCampos()
        {
            //btnEliminar.Disabled = true;
            //btnModificar.Disabled = true;
            //btnInsertar.Disabled = false;
            btnAceptar.Enabled = false;
            btnCancelar.Disabled = true;
            btn_agregarEntrada.Disabled = true;
            entradaDatos.Disabled = true;
            idCaso.Disabled = true;
            listEntradas.Enabled = false;
            proposito.Disabled = true;
            resultadoEsperado.Disabled = true;
            flujo.Disabled = true;
            estadoBox.Disabled = true;
            btnQuitar.Disabled = true;
            btnLimpiarLista.Disabled = true;
            TextProyecto.Disabled = true;
            TextReq.Disabled = true;
        }



        /*
        * Descripción: habilita los campos del form.
        * Requiere: n/a
        * Retorna: n/a
        */
        protected void habilitarCampos()
        {
            btnAceptar.Enabled = true;
            btnCancelar.Disabled = false;
            btn_agregarEntrada.Disabled = false;
            entradaDatos.Disabled = false;
            idCaso.Disabled = false;
            listEntradas.Enabled = true;
            proposito.Disabled = false;
            resultadoEsperado.Disabled = false;
            flujo.Disabled = false;
            estadoBox.Disabled = false;
            btnQuitar.Disabled = false;
            btnLimpiarLista.Disabled = false;
        }

        protected void gridCasos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Si el tipo de la fila es de datos
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Le da formato a la fila seleccionada
                if (e.Row.RowIndex == gridCasos.SelectedIndex)
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
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridCasos, "Select$" + e.Row.RowIndex);
            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Le da formato a toda la tabla
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

                    /*deshabilitaCampos();
                    btnInsertar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnEliminar.Disabled = false;
                    btnAceptar.Enabled = false;
                    btnCancelar.Disabled = true;
                    btnTel2.Disabled = false;*/
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

        protected void llenaCampos(EntidadCaso caso)
        {
            //Se guarda el id del último usuario consultado
            ViewState["idcaso"] = caso.Id;

            String idC = caso.Id;
            idCaso.Value = idC;
            
            String prop = caso.Proposito;
            proposito.Value = prop;

            String en = caso.Entrada;
            var elements = en.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

            listEntradas.Items.Clear();
            foreach (string s in elements)
            {
                listEntradas.Items.Add(s);
            }

            String res = caso.ResultadoEsperado;
            resultadoEsperado.Value = res;

            String flujoCentral = caso.FlujoCentral;
            flujo.Value = flujoCentral;

            int idDise = caso.IdDise;
            //TextDiseno.Value = idDise.ToString();

            int idProy = caso.IdProy;
            //TextProyecto.Value = idProy.ToString();
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

        /**
         * Descripcion: se revisa el perfil de la persona que inició sesión en el sistema.
         * Si es miembro no se le muestra el grid, ni los botones de eliminar e insertar,
         * se llenan los campos con su información personal.
         * Si es Administrador se muestra todo, es decir, no se cambia nada.
         * Recibe: un string @usuario que es el nombre de usuario de la persona en el sistema.
         *         un booleano @esInicio que determina si se llama a la función desde la primera carga o desde alguna otra parte de la interfaz
         * Devuelve verdadero si es administrador o falso si es un miembro de equipo.
         */
        protected bool revisarPerfil(string usuario, bool esInicio)
        {
            //Obtiene el perfil de la controladora
            string perfilS = controlCasos.getPerfil(usuario);
            //Si es miembro de equipo:
            if (perfilS.Equals("M"))
            {
                //Si el método se llama desde la primera carga de la página, controle lo necesario
                if (esInicio)
                {
                    /*btnEliminar.Visible = false;
                    btnInsertar.Visible = false;
                    gridRecursos.Visible = false;
                    EntidadRecursoH miembro = controlRH.consultaRH(usuario);
                    llenaCampos(miembro);*/
                }
                //No es administrador
                return false;
            }
            else
            {
                //Es administrador
                return true;
            }
        }

        protected void hacerResumen(string idDiseno)
        {
            Object[] resumen = controlCasos.hacerResumen(idDiseno);
            TextProyecto.Value = resumen[0].ToString();
            nivelPrueba.Value = resumen[1].ToString();
            propositoDiseno.Value = resumen[2].ToString();
            TextReq.Value = controlCasos.consultarReq(idDiseno);
        }

        protected void limpiarCampos() {
            idCaso.Value = "";
            entradaDatos.Value = "";
            object s = new object();
            EventArgs e = new EventArgs();
            btnLimpiarLista_Click(s, e);
            proposito.Value = "";
            resultadoEsperado.Value = "";
            flujo.Value = "";

        }

        
            /*
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
*/

         protected void habilitarAdmD(object sender, EventArgs e) {
             Response.Redirect("Diseno.aspx?idDise=" + idDise);
         }

    }
}