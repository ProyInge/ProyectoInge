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
            
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            //Do somthing
            string nombreUsuario = user.Text;
            string contra = password.Text;

            int respuesta = controladora.usuarioValido(nombreUsuario, contra);
            if(respuesta == 0)
            {
                //Response.Write("<script>window.localStorage.setItem('message', 'Hello World!');alert('entro');</script>");
                

                Response.Redirect("inicio.aspx"); 
            }
            else if(respuesta == -1)
            {
                Response.Write("<script>alert('datos incorrectos');</script>");
            }
            else if(respuesta == 1)
            {
                Response.Write("<script>alert('sesion activa');</script>");
            }
            
            
        } 
    }
}