using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.App_Code;

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
                }

                controlRH = new ControladoraRH();
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
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
                List<EntidadRecursoH> recursosL = controlRH.consultaRRHH();
                if (recursosL != null)
                {
                    for (int i = 0; i < recursosL.Count; i++)
                    {
                        System.Web.UI.HtmlControls.HtmlTableRow r = new System.Web.UI.HtmlControls.HtmlTableRow();
                        System.Web.UI.HtmlControls.HtmlTableCell c1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        c1.InnerText = recursosL[i].Cedula.ToString();
                        r.Cells.Add(c1);
                        System.Web.UI.HtmlControls.HtmlTableCell c2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        c2.InnerText = recursosL[i].Nombre;
                        r.Cells.Add(c2);
                        System.Web.UI.HtmlControls.HtmlTableCell c3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        c3.InnerText = recursosL[i].PApellido;
                        r.Cells.Add(c3);
                        System.Web.UI.HtmlControls.HtmlTableCell c4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        c4.InnerText = recursosL[i].SApellido;
                        r.Cells.Add(c4);
                        System.Web.UI.HtmlControls.HtmlTableCell c5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        c5.InnerText = ""; c5.Visible = false;
                        r.Cells.Add(c5);
                        r.ID = ""+i;
                        r.Attributes["onserverclick"] = "selectRow";
                        r.Attributes["runat"] = "server";

                        gridRecursos.Rows.Add(r);
                    }
                }
                else
                {
                    String resultadoS = "ERROR LEYENDO TABLA USUARIO";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + resultadoS + "');", true);
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void selectRow(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlTableRow row = (System.Web.UI.HtmlControls.HtmlTableRow) sender;
            row.BgColor = "#FFFFFF";
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