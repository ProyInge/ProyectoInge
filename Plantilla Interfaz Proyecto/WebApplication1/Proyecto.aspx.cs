using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.App_Code;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace WebApplication1
{
    public partial class Proyecto : System.Web.UI.Page
    {
        private ControladoraProyecto controladoraProyecto;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            controladoraProyecto = new ControladoraProyecto();

            if (Request.IsAuthenticated)
            {
                DataTable dtProyecto = controladoraProyecto.consultar_Total_Proyecto();
                DataView dvProyecto = dtProyecto.DefaultView;
                gridProyecto.DataSource = dvProyecto;
                gridProyecto.DataBind();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            string usuario = ((SiteMaster)this.Master).nombreUsuario;
            string perfil = controladoraProyecto.getPerfil(usuario);
            revisarPerfil(perfil); 
        }

        protected void revisarPerfil(string perfil) 
        {
            if (perfil.Equals("M"))
            {
                btnInsertar.Visible = false;
                btnAceptarInsertar.Visible = false;
                btnCancelarInsertar.Visible = false;
            }
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            btnEliminar.Disabled = true;
            btnModificar.Disabled = true;
            btnAceptarInsertar.Disabled = false;
            btnCancelarInsertar.Disabled = false;
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
            asignados.Disabled = false;
            disponibles.Disabled = false;
            lider.Disabled = false;
            btnTel2.Disabled = false;
            tel2.Disabled = false;

            nombreProyecto.Value = "";
            objetivo.Text = "";
            calendario.Value = "";
            nombreOficina.Value = "";
            representante.Value = "";
            correoOficina.Value = "";
            telefonoOficina.Value = "";
            asignados.Value = "";
            disponibles.Value = "";
            tel2.Value = "";
            barraEstado.Items.Clear();

            barraEstado.Items.Add("Pendiente");
            barraEstado.Items.Add("Asignado");
            barraEstado.Items.Add("En Ejecucion");
            barraEstado.Items.Add("Finalizado");
            barraEstado.Items.Add("Cerrado");

            List<string> lideres = controladoraProyecto.seleccionarLideres();
            int i = 0;
            while (i <= lideres.Count - 1)
            {
                lider.Items.Add(new ListItem(lideres.ElementAt(i)));
                i++;
            }
            
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nombreProyecto.Value))
            {
                btnInsertar.Disabled = true;
                btnEliminar.Disabled = true;
                btnAceptarInsertar.Visible = false;
                btnCancelarInsertar.Visible = false;
                btnGuardarModificar.Disabled = false;
                btnCancelarModificar.Disabled = false;
                btnGuardarModificar.Visible = true;
                btnCancelarModificar.Visible = true;

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
                asignados.Disabled = false;
                disponibles.Disabled = false;
                lider.Disabled = false;
                btnTel2.Disabled = false;
                tel2.Disabled = false;

                string est = barraEstado.Value;

                barraEstado.Items.Clear();

                if(est.Equals("Pendiente"))
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
            }
            else
            {
                textoAlerta.InnerHtml = "Seleccione un Proyecto a Modificar";
                alerta.Visible = true;
            }


        }

        protected void btnEliminar_Click(object sender, EventArgs e) 
        {
            if (!string.IsNullOrWhiteSpace(nombreProyecto.Value))
            {
                Object[] borrar = new Object[1];
                Object[] vacio2 = new Object[1];
                borrar[0] = nombreProyecto.Value;
                controladoraProyecto.ejecutarProyecto(4, borrar, vacio2);
                textoConfirmacion.InnerHtml = "Eliminado Correctamente!";
                alertaCorrecto.Visible = true;
            }
            else
            {
                textoAlerta.InnerHtml = "Seleccione un Proyecto a Eliminar";
                alerta.Visible = true;
            }
        }

        protected void btnCancelar_Insertar(object sender, EventArgs e)
        {
            btnEliminar.Disabled = false;
            btnModificar.Disabled = false;
            btnAceptarInsertar.Disabled = true;
            btnCancelarInsertar.Disabled = true;
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
            asignados.Disabled = true;
            disponibles.Disabled = true;
            lider.Disabled = true;
            btnTel2.Disabled = true;
            tel2.Disabled = true;
            alerta.Visible = false;
            alertaCorrecto.Visible = false;

            nombreProyecto.Value = "";
            objetivo.Text = "";
            barraEstado.Value = "";
            calendario.Value = "";
            nombreOficina.Value = "";
            representante.Value = "";
            correoOficina.Value = "";
            telefonoOficina.Value = "";
            asignados.Value = "";
            disponibles.Value = "";
            tel2.Value = "";
            lider.Items.Clear();
        }

        protected void btnAceptar_Insertar(object sender, EventArgs e)
        {

            if (
                        !string.IsNullOrWhiteSpace(nombreProyecto.Value) &&
                        !string.IsNullOrWhiteSpace(objetivo.Text) &&
                        !string.IsNullOrWhiteSpace(barraEstado.Value) &&
                        !string.IsNullOrWhiteSpace(calendario.Value) &&
                        !string.IsNullOrWhiteSpace(nombreOficina.Value) &&
                        !string.IsNullOrWhiteSpace(representante.Value) &&
                        !string.IsNullOrWhiteSpace(correoOficina.Value) &&
                        !string.IsNullOrWhiteSpace(telefonoOficina.Value) &&
                         !string.IsNullOrWhiteSpace(lider.Value)
                )
            {
                 int existe = revisarExistentes();
                 if (existe > 0)
                 {
                     string faltantes = "";
                     alertaCorrecto.Visible = false;

                     switch (existe)
                     {
                         case 1:
                             {
                                 faltantes = "Ya existe ese Nombre de Proyecto <br>";
                                 textoAlerta.InnerHtml = faltantes;
                                 alerta.Visible = true;
                                 break;
                             }

                         case 2:
                             {
                                 faltantes = "Ya existe ese Nombre de Oficina <br>";
                                 textoAlerta.InnerHtml = faltantes;
                                 alerta.Visible = true;
                                 break;
                             }

                         case 3:
                             {
                                 faltantes = "Ya existe ese Nombre de Proyecto <br> Y ese Nombre de Oficina ";
                                 textoAlerta.InnerHtml = faltantes;
                                 alerta.Visible = true;
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
                         dat[7] = telefonoOficina.Value;//tel 1
                         dat[8] = ll;//cedula lider
                        dat[9]="";//nombre lider
                        dat[10] = 0;//tel2.Value;//telefono 2
                        
                         controladoraProyecto.ejecutarProyecto(1, dat, vacio);

                         if (!string.IsNullOrWhiteSpace(tel2.Value))
                         {
                            controladoraProyecto.insertarTel2(tel2.Value, nombreOficina.Value);
                         }

                         alerta.Visible = false;
                         alertaCorrecto.Visible = true;

                         lider.Items.Clear();
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
                         alertaCorrecto.Visible = false;
                         revisarDatos();
                     }
                 }

            }
            else
            {
                alertaCorrecto.Visible = false;
                revisarDatos();
            }
        }

        protected void btnGuardar_Modificar(object sender, EventArgs e)
        {
            /*string obj = objetivo.Text;
            string nombreP = nombreProyecto.Value;
            string est = barraEstado.Value;
            string fechaP = calendario.Value;
            string nomOf = nombreOficina.Value;
            string rep = representante.Value;
            string email = correoOficina.Value;
            string telOf = telefonoOficina.Value;*/

          

            char est = barraEstado.Value[0];
            Object[] dat = new Object[8];
            dat[0] = nombreProyecto.Value; 

        }
        protected void btnCancelar_Modificar(object sender, EventArgs e)
        {
            btnInsertar.Disabled = false;
            btnEliminar.Disabled = false;
            btnGuardarModificar.Disabled = true;
            btnCancelarModificar.Disabled = true;
            btnGuardarModificar.Visible = false;
            btnCancelarModificar.Visible = false;
            btnAceptarInsertar.Visible = true;
            btnCancelarInsertar.Visible = true;
        }

        protected void revisarDatos()
        {
            string faltantes = "";


            if (string.IsNullOrWhiteSpace(nombreProyecto.Value))
            {
                faltantes = faltantes + "Escriba un Nombre de Proyecto <br>";
            }

            if (string.IsNullOrWhiteSpace(objetivo.Text))
            {
                faltantes = faltantes + "Escriba un Objetivo <br>";
            }

            if (string.IsNullOrWhiteSpace(barraEstado.Value))
            {
                faltantes = faltantes + "Seleccione un estado <br>";
            }

            if (string.IsNullOrWhiteSpace(calendario.Value))
            {
                faltantes = faltantes + "Seleccione una fecha <br>";
            }

            if (string.IsNullOrWhiteSpace(nombreOficina.Value))
            {
                faltantes = faltantes + "Escriba un Nombre de Oficina <br>";
            }

            if (string.IsNullOrWhiteSpace(representante.Value))
            {
                faltantes = faltantes + "Escriba un Representante de Oficina <br>";
            }

            if (string.IsNullOrWhiteSpace(correoOficina.Value))
            {
                faltantes = faltantes + "Escriba un Correo de Oficina <br>";
            }

            if (string.IsNullOrWhiteSpace(telefonoOficina.Value))
            {
                faltantes = faltantes + "Escriba un Telefono de Oficina <br>";
            }
            if (string.IsNullOrWhiteSpace(lider.Value))
            {
                faltantes = faltantes + "Seleccione un Lider de Proyecto <br>";
            }
            if (tel2.Value.Equals(telefonoOficina.Value))
            {
                faltantes = faltantes + "Los telefonos ingresados son iguales, por favor digite uno diferente <br>";
            }

            textoAlerta.InnerHtml = faltantes;
            alerta.Visible = true;

        }

        protected int revisarExistentes()
        {
            int resultado;
            resultado = controladoraProyecto.revisarExistentes(nombreProyecto.Value, nombreOficina.Value);
            return resultado;
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            DataTable dtProyecto = controladoraProyecto.consultar_Total_ProyectoFiltro(TextBox2.Text);
            DataView dvProyecto = dtProyecto.DefaultView;
            gridProyecto.DataSource = dvProyecto;
            gridProyecto.DataBind();

        }
        protected void gridProyecto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridProyecto, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
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
                    nombreProyecto.Value = nombreProy;
                    objetivo.Text = proy.getObjetivo();
                    string estado = (proy.getEstado()).ToString();
                    barraEstado.Items.Clear();
                    lider.Items.Clear();

                    //string fecha = proy.getFecha().ToString();

                    DateTime dt = proy.getFecha();
                    calendario.Value = dt.ToString("yyy-MM-dd",CultureInfo.InvariantCulture);

                

                    string liderP = (proy.getLider()).ToString();            

                    string nombreL = proy.getNombreLider();

                    lider.Items.Add(new ListItem(liderP+ " " +nombreL));
                    barraEstado.Items.Add(new ListItem(estado));

                    
                    //lider.Value = (proy.getLider()).ToString();
                    nombreOficina.Value = proy.getNomOf();
                    correoOficina.Value = proy.getCorreoOf();
                    telefonoOficina.Value = (proy.getTelOf()).ToString();
                    representante.Value = proy.getRep();
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click para seleccionar esta fila.";
                }
            }
        }
    }
}