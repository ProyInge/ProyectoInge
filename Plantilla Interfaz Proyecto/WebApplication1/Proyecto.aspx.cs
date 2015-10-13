using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.App_Code;

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

            }
            else
            {
                Response.Redirect("Login.aspx");
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
            
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            btnInsertar.Disabled = true;
            btnEliminar.Disabled = true;
            btnAceptarInsertar.Visible = false;
            btnCancelarInsertar.Visible = false;
            btnGuardarModificar.Disabled = false; 
            btnCancelarModificar.Disabled = false;
            btnGuardarModificar.Visible = true;
            btnCancelarModificar.Visible = true;


        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            
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
        }

        protected void btnAceptar_Insertar(object sender, EventArgs e)
        {
            /*string obj = objetivo.Text;
           string nombreP = nombreProyecto.Value;
           string est = barraEstado.Value;
           string fechaP = calendario.Value;
           string nomOf = nombreOficina.Value; ESTO NO SE OCUPA
           string rep = representante.Value;
           string email = correoOficina.Value;
           string telOf = telefonoOficina.Value;
           char est = barraEstado.Value[0];*/

            if (
                        !string.IsNullOrWhiteSpace(nombreProyecto.Value) &&
                        !string.IsNullOrWhiteSpace(objetivo.Text) &&
                        !string.IsNullOrWhiteSpace(barraEstado.Value) &&
                        !string.IsNullOrWhiteSpace(calendario.Value) &&
                        !string.IsNullOrWhiteSpace(nombreOficina.Value) &&
                        !string.IsNullOrWhiteSpace(representante.Value) &&
                        !string.IsNullOrWhiteSpace(correoOficina.Value) &&
                        !string.IsNullOrWhiteSpace(telefonoOficina.Value)
                )
            {

                Object[] dat = new Object[8];
                Object[] vacio = new Object[1];
                dat[0] = nombreProyecto.Value;
                dat[1] = objetivo.Text;
                dat[2] = barraEstado.Value;
                dat[3] = calendario.Value;
                dat[4] = nombreOficina.Value;
                dat[5] = representante.Value;
                dat[6] = correoOficina.Value;
                dat[7] = telefonoOficina.Value;
                controladoraProyecto.ejecutarProyecto(1, dat, vacio);
            }
            else
            {
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

            textoAlerta.InnerHtml = faltantes;
            alerta.Visible = true;

        }
    }
}