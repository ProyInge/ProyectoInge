using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.App_Code;

namespace WebApplication1
{
    public partial class MiembrosProyecto : System.Web.UI.Page
    {
        private ControladoraRH controlRH = new ControladoraRH();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ticket.Name + "')", true);
                int idProy = controlRH.getProyID(ticket.Name);
                refrescaTablaMiembros(idProy);

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }



        protected void refrescaTablaMiembros(int idProyecto)
        {
            DataTable dtMiembros;
            try
            {
                dtMiembros = controlRH.consultaMiembrosProy(idProyecto);
            }
            catch
            {
                dtMiembros = null;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "ERROR LEYENDO TABLA" + "');", true);
            }

            DataView dvRecursos = dtMiembros.DefaultView;

            gridMiembros.DataSource = dvRecursos;
            gridMiembros.DataBind();
        }
    }


}