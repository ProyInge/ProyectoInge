using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GestionPruebas.App_Code;
using System.Web.Security;

namespace GestionPruebas
{

    public partial class Login : System.Web.UI.Page
    {
        protected ControladoraRH controladora = new ControladoraRH();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                Response.Redirect("Inicio.aspx");
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            //Do somthing
            string nombreUsuario = user.Text;
            string contra = password.Text;
            int res = controladora.usuarioValido(nombreUsuario, contra);
            switch(res)
            {
                case 0:
                    FormsAuthentication.Authenticate(nombreUsuario,contra);
                    FormsAuthentication.RedirectFromLoginPage(nombreUsuario, true);
                    break;
                case 1:
                    lblModalTitle.Text = "Error de autenticación";
                    lblModalBody.Text = "Ya está activa una sesión para este usuario. Cierre sesión en el otro dispositivo e intente de nuevo.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                    break;
                case -1:
                    lblModalTitle.Text = "Error de autenticación";
                    lblModalBody.Text = "El nombre de usuario y/o contraseña son inválidos.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                    break;
            }
                     
        } 
    }
}