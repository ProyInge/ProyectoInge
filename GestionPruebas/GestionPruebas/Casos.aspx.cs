using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionPruebas.App_Code;
using System.Data;
using System.Drawing;

namespace GestionPruebas
{
    public partial class Casos : System.Web.UI.Page
    {
        private  ControladoraCasos controlCasos;
        private string entradas;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Si ya está logueado:
            if (Request.IsAuthenticated)
            {
                
                //Inicializamos controladora
                controlCasos = new ControladoraCasos();

                entradas = "";

                //Si es la primera vez que se carga la página:
                if (!this.IsPostBack)
                {
                    //Obtiene usuario logueado
                    string usuarioS = ((SiteMaster)this.Master).nombreUsuario;
                    //Revisa su perfil
                    bool esAdmin = revisarPerfil(usuarioS, true);
                    btnEliminar.Disabled = true;
                    inhabilitarCampos();
                    refrescaTabla();
                    
               
                }
                
            }
            else
            {//En caso de que no esté logueado, redirija a login
                Response.Redirect("Login.aspx");
            }
        }

        private void refrescaTabla()
        {
            DataTable dtCaso = controlCasos.consultarCasos();
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alert('Debe de ingresar una entrada y su tipo correspondiente')", true);
           
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
            habilitarCampos();
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


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //Si va a insertar
            if (!btnInsertar.Disabled)
            {
                foreach (ListItem entrada in listEntradas.Items)
                {
                    entradas += entrada.Value + ",";
                }

                string resultado = controlCasos.insertarCaso(idCaso.Value, proposito.Value, entradas, resultadoEsperado.Value, flujo.Value,0,0);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alert('"+resultado+"')", true);
           
                entradaDatos.Value = "";
                estadoBox.Value = "";
                idCaso.Value = "";
                proposito.Value = "";
                resultadoEsperado.Value = "";
                flujo.Value = "";
                listEntradas.Items.Clear();

                inhabilitarCampos();

            }
        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            
            //Si es una inserción
            if (!btnInsertar.Disabled)
            {
                entradaDatos.Value = "";
                estadoBox.Value = "";
                idCaso.Value = "";
                proposito.Value = "";
                resultadoEsperado.Value = "";
                flujo.Value = "";
                listEntradas.Items.Clear();

                inhabilitarCampos();
                
            }
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
            btnEliminar.Disabled = true;
            btnModificar.Disabled = true;
            btnInsertar.Disabled = false;
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
        }



        /*
        * Descripción: habilita los campos del form.
        * Requiere: n/a
        * Retorna: n/a
        */
        protected void habilitarCampos()
        {
            btnModificar.Disabled = true;
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

                    idCaso.Value = row.Cells[0].Text;
                    int id = parseInt(idCaso.Value);
                    
                    EntidadCaso casoSel = controlCasos.consultaCaso(id);
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

            String entrada = caso.Entrada;
            // todo 

            String res = caso.ResultadoEsperado;
            resultadoEsperado.Value = res;

            String flujoCentral = caso.FlujoCentral;
            flujo.Value = flujoCentral;

            int idDise = caso.IdDise;
            diseno.Value = idDise.ToString();

            int idProy = caso.IdProy;
            TextProyecto.Value = idProy.ToString();
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
    }
}