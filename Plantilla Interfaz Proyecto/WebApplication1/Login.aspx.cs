using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebApplication1.App_Code;
using System.Web.Security;

namespace WebApplication1
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
            if(controladora.usuarioValido(nombreUsuario, contra))
            {
                FormsAuthentication.Authenticate(nombreUsuario, contra);
                FormsAuthentication.RedirectFromLoginPage(nombreUsuario, true);
            }
            else
            {
                Response.Write("<script>alert('datos incorrectos');</script>");
            }
            
            
        } 
    }
}