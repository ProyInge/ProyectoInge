using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebApplication1.App_Code;

namespace WebApplication1
{

    public partial class Login : System.Web.UI.Page
    {
        protected ControladoraRH controladora = new ControladoraRH();

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            //Do somthing
            string nombreUsuario = user.Text;
            string contra = password.Text;
            if(controladora.usuarioValido(nombreUsuario, contra))
            {
                Response.Write("<script>alert('entro');</script>");
            }
            else
            {
                Response.Write("<script>alert('datos incorrectos');</script>");
            }
            
            
        } 
    }
}