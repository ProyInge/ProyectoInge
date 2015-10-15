using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.App_Code;
using System.Drawing;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        private ControladoraRH controlRH;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                if(!this.IsPostBack)
                {
                    btnEliminar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnInsertar.Disabled = false;
                    btnAceptar.Disabled = true;
                    btnCancelar.Disabled = true;
                }

                controlRH = new ControladoraRH();
                btnAceptar.InnerHtml = "Aceptar";
                cedula.Disabled = true;
                nombre.Disabled = true;
                pApellido.Disabled = true;
                sApellido.Disabled = true;
                telefono.Disabled = true;
                correo.Disabled = true;
                rol.Disabled = true;
                perfil.Disabled = true;
                usuario.Disabled = true;
                contrasena.Disabled = true;
                refrescaTabla();

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void refrescaTabla()
        {
            DataTable dtRecursos;
            try
            {
                dtRecursos = controlRH.consultaRRHH();
            }
            catch
            {
                dtRecursos = null;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "ERROR LEYENDO TABLA" + "');", true);
            }

            DataView dvRecursos = dtRecursos.DefaultView;

            gridRecursos.DataSource = dvRecursos;
            gridRecursos.DataBind();
        }
        protected void gridRecursos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if(e.Row.RowIndex==gridRecursos.SelectedIndex)
                {
                    e.Row.ToolTip = "Esta fila está seleccionada!";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='#0099CC';";
                    e.Row.ForeColor = ColorTranslator.FromHtml("#000000");
                    e.Row.BackColor = ColorTranslator.FromHtml("#0099CC");
                } else {
                    e.Row.ToolTip = "Click para seleccionar esta fila.";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                }
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridRecursos, "Select$" + e.Row.RowIndex);
            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridRecursos.Rows)
            {
                if (row.RowIndex == gridRecursos.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#0099CC");
                    row.ToolTip = "Esta fila está seleccionada!";
                    row.ForeColor = ColorTranslator.FromHtml("#000000");
                    row.Attributes["onmouseout"] = "this.style.backgroundColor='#0099CC';";
                    cedula.Value = row.Cells[0].Text;
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click para seleccionar esta fila.";
                }
            }
        }


        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            btnEliminar.Disabled = true;
            btnModificar.Disabled = true;
            btnInsertar.Disabled = false;
            btnAceptar.Disabled = false;
            btnCancelar.Disabled = false;
            cedula.Disabled = false;
            nombre.Disabled = false;
            pApellido.Disabled = false;
            sApellido.Disabled = false;
            telefono.Disabled = false;
            correo.Disabled = false;
            rol.Disabled = false;
            perfil.Disabled = false;
            usuario.Disabled = false;
            contrasena.Disabled = false;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            btnInsertar.Disabled = true;
            btnEliminar.Disabled = true;
            btnModificar.Disabled = false;
            btnAceptar.InnerHtml = "Guardar";
            btnAceptar.Disabled = false;
            btnCancelar.Disabled = false;
            cedula.Disabled = false;
            nombre.Disabled = false;
            pApellido.Disabled = false;
            sApellido.Disabled = false;
            telefono.Disabled = false;
            correo.Disabled = false;
            rol.Disabled = false;
            perfil.Disabled = false;
            usuario.Disabled = false;
            contrasena.Disabled = false;

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            btnInsertar.Disabled = true;
            btnModificar.Disabled = true;
            btnEliminar.Disabled = false;
            btnAceptar.Disabled = false;
            btnCancelar.Disabled = false;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if(!btnInsertar.Disabled)
            { //Inserción
                char[] charsToTrim = { '-', ' ', '/' };
                int cedulaI;
                bool parsedCed = int.TryParse(cedula.Value.Trim(charsToTrim), out cedulaI);
                if (!parsedCed)
                {
                    //Incorrecto formato de cedula
                }
                String nombreS = nombre.Value;
                String pApellidoS = pApellido.Value;
                String sApellidoS = sApellido.Value;
                String correoS = correo.Value;
                int telefonoI;
                bool parsedTel = int.TryParse(telefono.Value.Trim(charsToTrim), out telefonoI);
                if(!parsedTel)
                {
                    //Incorrecto formato de telefono
                }

                String rolS = rol.Value;
                char perfilC = 'M';
                switch (perfil.SelectedIndex)
                {
                    case 0:
                        //No se seleccionó rol
                        break;
                    case 1:
                        perfilC = 'A';
                        break;
                    case 2:
                        perfilC = 'M';
                        break;
                    default:
                        //??
                        break;
                }
                String usuarioS = usuario.Value;
                String contrasenaS = contrasena.Value;
                bool resultado = controlRH.insertaRH(cedulaI, nombreS, pApellidoS, sApellidoS, correoS, usuarioS, contrasenaS, perfilC, -1, rolS );
                String resultadoS;
                if(resultado)
                {
                    resultadoS = "INSERCIÓN CORRECTA";
                } else
                {
                    resultadoS = "ERROR EN INSERCIÓN";
                }
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + resultadoS + "');", true);
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                refrescaTabla();
            } else if(!btnModificar.Disabled)
            { //Modificación
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                btnAceptar.InnerHtml = "Aceptar";
            } else if(!btnEliminar.Disabled)
            { //Eliminación
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                char[] charsToTrim = { '-', ' ', '/' };
                int cedulaE;
                bool parsedCed = int.TryParse(cedula.Value.Trim(charsToTrim), out cedulaE);
                if (!parsedCed)
                {
                    //Incorrecto formato de telefono
                }
                bool resultado;
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + cedulaE + "');", true);
                resultado = controlRH.eliminaRH(cedulaE);

                String resultadoS;
                if (resultado)
                {
                    resultadoS = "ELIMINACIÓN CORRECTA";
                }
                else
                {
                    resultadoS = "ERROR EN ELIMINACIÓN";
                }
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + resultadoS + "');", true);
                refrescaTabla();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!btnInsertar.Disabled)
            { //Cancelar inserción
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
            }
            else if (!btnModificar.Disabled)
            { //Cancelar modificación
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                btnAceptar.InnerHtml = "Aceptar";
            }
            else if (!btnEliminar.Disabled)
            { //Cancelar inserción
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
            }
        }
    }
}