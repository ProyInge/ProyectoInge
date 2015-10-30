using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionPruebas
{
    public partial class Casos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_agregarEntrada_click(object sender, EventArgs e)
        {
            string entradaNueva = (string)entradaDatos.Value;
            entradaNueva += " - "+ (string)estadoBox.Value;

            listEntradas.Items.Add(new ListItem(entradaNueva));

            
        }
    }
}