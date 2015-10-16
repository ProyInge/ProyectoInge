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
                if (!this.IsPostBack)
                {
                    btnEliminar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnInsertar.Disabled = false;
                    btnAceptar.Disabled = true;
                    btnCancelar.Disabled = true;
                    deshabilitaCampos();
                }

                controlRH = new ControladoraRH();
                btnAceptar.InnerHtml = "Aceptar";
                cedula.Disabled = true;
                refrescaTabla();

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void deshabilitaCampos()
        {
            nombre.Disabled = true;
            btnTel2.Disabled = true;
            pApellido.Disabled = true;
            sApellido.Disabled = true;
            telefono1.Disabled = true;
            telefono2.Disabled = true;
            correo.Disabled = true;
            rol.Disabled = true;
            perfil.Disabled = true;
            usuario.Disabled = true;
            contrasena.Disabled = true;
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
                if (e.Row.RowIndex == gridRecursos.SelectedIndex)
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

                    char[] charsToTrim = { '-', ' ', '/' };
                    cedula.Value = row.Cells[0].Text;
                    int cedulaC;
                    bool parsedCed = int.TryParse(cedula.Value.Trim(charsToTrim), out cedulaC);
                    if (!parsedCed)
                    {
                        //Incorrecto formato de cédula
                    }
                    EntidadRecursoH recursoSel = controlRH.consultaRH(cedulaC);
                    nombre.Value = recursoSel.Nombre;
                    pApellido.Value = recursoSel.PApellido;
                    sApellido.Value = recursoSel.SApellido;
                    correo.Value = recursoSel.Correo;
                    usuario.Value = recursoSel.NomUsuario;
                    contrasena.Value = recursoSel.Contra;
                    telefono1.Value = (recursoSel.Telefono1 != -1) ? recursoSel.Telefono1.ToString() : "";
                    telefono2.Value = (recursoSel.Telefono2 != -1) ? recursoSel.Telefono2.ToString() : "";
                    switch (recursoSel.Perfil)
                    { 
                        case ' ':
                            perfil.SelectedIndex = 0;
                            //No se seleccionó rol
                            break;
                        case 'A':
                            perfil.SelectedIndex = 1;
                            break;
                        case 'M':
                            perfil.SelectedIndex = 2;
                            break;
                        default:
                            perfil.SelectedIndex = 0;
                            //??
                            break;
                    }
                    switch (recursoSel.Rol)
                    {
                        case "":
                            rol.SelectedIndex = 0;
                            //No se seleccionó rol
                            break;
                        case "Lider":
                            rol.SelectedIndex = 1;
                            break;
                        case "Tester":
                            rol.SelectedIndex = 2;
                            break;
                        case "Usuario":
                            rol.SelectedIndex = 3;
                            break;
                        default:
                            rol.SelectedIndex = 0;
                            //??
                            break;
                    }
                    deshabilitaCampos();
                }
                else
                {
                    row.Attributes["onmouseout"] = "this.style.backgroundColor='#FFFFFF';";
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
            btnTel2.Disabled = false;
            cedula.Disabled = false;
            nombre.Disabled = false;
            pApellido.Disabled = false;
            sApellido.Disabled = false;
            telefono1.Disabled = false;
            telefono2.Disabled = false;
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
            btnTel2.Disabled = false;
            cedula.Disabled = true;
            nombre.Disabled = false;
            pApellido.Disabled = false;
            sApellido.Disabled = false;
            telefono1.Disabled = false;
            telefono2.Disabled = false;
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
            if (!btnInsertar.Disabled)
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

                int pTelefono;
                bool parsedTel1 = int.TryParse(telefono1.Value.Trim(charsToTrim), out pTelefono);
                if (!parsedTel1)
                {
                    //Incorrecto formato de telefono
                }

                int sTelefono;
                bool parsedTel2 = int.TryParse(telefono2.Value.Trim(charsToTrim), out sTelefono);
                if (!parsedTel2)
                {
                    sTelefono = -1;
                }

                String rolS = rol.Value;
                char perfilC = ' ';
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
                bool resultado = controlRH.insertaRH(cedulaI, nombreS, pApellidoS, sApellidoS, correoS, usuarioS, contrasenaS, perfilC, -1, rolS, pTelefono, sTelefono);
                String resultadoS;
                if (resultado)
                {
                    resultadoS = "INSERCIÓN CORRECTA";
                }
                else
                {
                    resultadoS = "ERROR EN INSERCIÓN";
                }
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + resultadoS + "');", true);
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                deshabilitaCampos();
                refrescaTabla();
            }
            else if (!btnModificar.Disabled)
            { //Modificación
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                btnAceptar.InnerHtml = "Aceptar";

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

                int pTelefono;
                bool parsedTel1 = int.TryParse(telefono1.Value.Trim(charsToTrim), out pTelefono);
                if (!parsedTel1)
                {
                    pTelefono = -1;
                }

                int sTelefono;
                bool parsedTel2 = int.TryParse(telefono2.Value.Trim(charsToTrim), out sTelefono);
                if (!parsedTel2)
                {
                    sTelefono = -1;
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
                bool resultado = controlRH.modificaRH(cedulaI, nombreS, pApellidoS, sApellidoS, correoS, usuarioS, contrasenaS, perfilC, -1, rolS, pTelefono, sTelefono);
                String resultadoS;
                if (resultado)
                {
                    resultadoS = "MODIFICACIÓN CORRECTA";
                }
                else
                {
                    resultadoS = "ERROR EN MODIFICACIÓN";
                }
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + resultadoS + "');", true);
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                deshabilitaCampos();
                refrescaTabla();
            }
            else if (!btnEliminar.Disabled)
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
                deshabilitaCampos();
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
                deshabilitaCampos();
            }
            else if (!btnModificar.Disabled)
            { //Cancelar modificación
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                btnAceptar.InnerHtml = "Aceptar";
                deshabilitaCampos();
            }
            else if (!btnEliminar.Disabled)
            { //Cancelar inserción
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                deshabilitaCampos();
            }
        }
    }
}