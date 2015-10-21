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
        /**
         *Controladora de recursos humanos, permite efectuar las acciones relacionadas al módulo de recursos humanos.
         */
        private ControladoraRH controlRH;

        /**
         * Descripcion: Se cargan los elementos necesarios de la vista luego de cada acción en el servidor y en el inicio
         * Recibe: un object @sender que determina el objeto que envía a cargar la página. Este no se usa.
         *         un EventArgs @e, que determina la acción o evento realizado.
         * No devuelve nada.
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            //Si ya está logueado:
            if (Request.IsAuthenticated)
            {
                //Inicializamos controladora
                controlRH = new ControladoraRH();
                //Si es la primera vez que se carga la página:
                if (!this.IsPostBack)
                {
                    //Obtiene usuario logueado
                    String usuarioS = ((SiteMaster)this.Master).nombreUsuario;
                    //Revisa su perfil
                    bool esAdmin = revisarPerfil(usuarioS, true);
                    //Alistamos campos y grid
                    btnEliminar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnInsertar.Disabled = false;
                    btnAceptar.Disabled = true;
                    btnCancelar.Disabled = true;
                    contrasena2.Disabled = true;
                    repcontrasenalabel.Visible = false;
                    contrasena2.Visible = false;
                    contrasena1.Style.Value = "margin: 4px 4px 167px 4px;";
                    cedula.Disabled = true;
                    deshabilitaCampos();
                    if (esAdmin)
                    {
                        refrescaTabla();
                    }
                }
                btnAceptar.InnerHtml = "Aceptar";
            }
            else
            {//En caso de que no esté logueado, redirija a login
                Response.Redirect("Login.aspx");
            }
        }

        /**
         * Descripcion: se revisa el perfil de la persona que inició sesión en el sistema.
         * Si es miembro no se le muestra el grid, ni los botones de eliminar e insertar,
         * se llenan los campos con su información personal.
         * Si es Administrador se muestra todo, es decir, no se cambia nada.
         * Recibe: un string @usuario que es el nombre de usuario de la persona en el sistema.
         *         un booleano @esInicio que determina si se llama a la función desde la primera carga o desde alguna otra parte de la interfaz
         * Devuelve verdadero si es administrador o falso si es un miembro de equipo.
         */
        protected bool revisarPerfil(string usuario, bool esInicio)
        {
            //Obtiene el perfil de la controladora
            String perfilS = controlRH.getPerfil(usuario);
            //Si es miembro de equipo:
            if (perfilS.Equals("M"))
            {
                //Si el método se llama desde la primera carga de la página, controle lo necesario
                if (esInicio)
                {
                    btnEliminar.Visible = false;
                    btnInsertar.Visible = false;
                    gridRecursos.Visible = false;
                    EntidadRecursoH miembro = controlRH.consultaRH(usuario);
                    llenaCampos(miembro);
                }
                //No es administrador
                return false;
            }
            else
            {
                //Es administrador
                return true;
            }
        }

        /** 
         * Descripción: Cambia el estado de todos los campos de texto y combo box para que no
         * puedan modificarse ni ingresar datos.
         * No recibe nada.
         * No devuelve nada.
         */
        protected void deshabilitaCampos()
        {
            cedula.Disabled = true;
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
            contrasena1.Disabled = true;
            contrasena2.Disabled = true;
        }

        /** 
         * Descripción: Vacía los campos de texto y combo box .
         * No recibe nada.
         * No devuelve nada.
         */
        protected void limpiaCampos()
        {
            cedula.Value = "";
            nombre.Value = "";
            pApellido.Value = "";
            sApellido.Value = "";
            telefono1.Value = "";
            telefono2.Value = "";
            correo.Value = "";
            usuario.Value = "";
            contrasena1.Value = "";
            contrasena2.Value = "";
            rol.SelectedIndex = 0;
            perfil.SelectedIndex = 0;
        }


        /**
         * Descripcion: llena los campos de texto y combobox con la informacion de un recurso hummno.
         * Recibe: Una entidad recurso humano @rec que contiene la información para llenar los campos.
         * No devuelve nada.
         */
        protected void llenaCampos(EntidadRecursoH rec)
        {
            //Se guarda el id del último usuario consultado
            ViewState["idrh"] = rec.IdRH;
            //Se llenan los campos
            cedula.Value = "" + rec.Cedula;
            nombre.Value = rec.Nombre;
            pApellido.Value = rec.PApellido;
            sApellido.Value = rec.SApellido;
            correo.Value = rec.Correo;
            usuario.Value = rec.NomUsuario;
            contrasena1.Value = rec.Contra;
            //Si no hay telefono asociado deja los campos en blanco
            telefono1.Value = (rec.Telefono1 != -1) ? rec.Telefono1.ToString() : "";
            telefono2.Value = (rec.Telefono2 != -1) ? rec.Telefono2.ToString() : "";
            //Dependiendo del perfil selecciona una opcion del combobox perfil
            switch (rec.Perfil)
            {
                case ' ':
                    //No se seleccionó perfil
                    perfil.SelectedIndex = 0;
                    break;
                case 'A':
                    perfil.SelectedIndex = 1;
                    break;
                case 'M':
                    perfil.SelectedIndex = 2;
                    break;
                default:
                    perfil.SelectedIndex = 0;                    
                    break;
            }
            //Dependiendo del rol selecciona una opcion del combobox rol
            switch (rec.Rol)
            {
                case "":
                    //No se seleccionó rol
                    rol.SelectedIndex = 0;
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
                    break;
            }
        }

        /**
         * Descripcion: Llena el grid con la información básica de los recursos humanos en la base de datos
         * No recibe nada.
         * No devuelve nada.
         */
        protected void refrescaTabla()
        {
            DataTable dtRecursos = new DataTable();
            try
            {
                //Realiza la consulta de selección de todos los recursos humanos con la controladora y guarda esa información en un DataTable
                dtRecursos = controlRH.consultaRRHH();
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + "Error leyendo tabla. Revise su conexión con la base de datos" + "')", true);
            }

            
            //Crea una vista para llenar el grid
            DataView dvRecursos = dtRecursos.DefaultView;
            //Liga el grid con la información de la vista
            gridRecursos.DataSource = dvRecursos;
            gridRecursos.DataBind();
        }

        /**
         * Descripcion: Da formato a cada fila cuando se le liga la información a la misma.
         * Recibe: objeto @sender. No se utiliza
         *         EventArgs @e. Determina los datos de la fila actual
         * No devuelve nada.               
         */ 
        protected void gridRecursos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Si el tipo de la fila es de datos
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Le da formato a la fila seleccionada
                if (e.Row.RowIndex == gridRecursos.SelectedIndex)
                {
                    e.Row.ToolTip = "Esta fila está seleccionada!";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='#0099CC';";
                    e.Row.ForeColor = ColorTranslator.FromHtml("#000000");
                    e.Row.BackColor = ColorTranslator.FromHtml("#0099CC");
                }
                //Le da formato a las demás filas
                else
                {
                    e.Row.ToolTip = "Click para seleccionar esta fila.";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                }
                //Determina formato general y acción al hacer click sobre la fila
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridRecursos, "Select$" + e.Row.RowIndex);
            }
        }

        /**
         * Descripcion: Carga los campos y combobox con la información del recurso humano seleccionado en el grid.
         * Utiliza la cedula y realiza una consulta SQL mediante la controladora de BD para obtener la información
         * completa del recurso. 
         * Recibe: object @sender. No se utiliza
         *         EventArgs @e. No se utiliza
         */
        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Le da formato a toda la tabla
            foreach (GridViewRow row in gridRecursos.Rows)
            {
                //Formato de fila seleccionada
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
                    llenaCampos(recursoSel);
                    deshabilitaCampos();
                    btnInsertar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnEliminar.Disabled = false;
                    btnAceptar.Disabled = true;
                    btnCancelar.Disabled = true;
                    btnTel2.Disabled = false;
                }
                //Filas no seleccionadas
                else
                {
                    row.Attributes["onmouseout"] = "this.style.backgroundColor='#FFFFFF';";
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click para seleccionar esta fila.";
                }
            }
        }

        /**
         * Descripcion: Habilita y limpia los campos de texto y combobox para ingresar la información
         * de un nuevo Recurso Humano.
         * Recibe object    @sender. No se utiliza
         *        EventArgs @e. No se utiliza
         * No devuelve nada.         
        */
        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            //Habilita campos
            gridRecursos.SelectedIndex = -1;
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
            contrasena1.Disabled = false;
            contrasena2.Disabled = false;
            contrasena2.Visible = true;
            repcontrasenalabel.Visible = true;
            contrasena1.Style.Value = "margin: 4px;";
            limpiaCampos();
        }

        /**
         * Descripcion: Habilita los campos de texto y combobox para modificar la información de un Recurso Humano.
         * Dependiendo del perfil del usuario se le habilitan diferentes campos.
         * Recibe object    @sender. No se utiliza
         *        EventArgs @e. No se utiliza
         * No devuelve nada.         
        */
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //Revisa que los campos no esten vacíos
            if (
                ViewState["idrh"]!=null &&
                !string.IsNullOrWhiteSpace(cedula.Value) &&
                !string.IsNullOrWhiteSpace(nombre.Value) &&
                !string.IsNullOrWhiteSpace(perfil.Value) &&
                !string.IsNullOrWhiteSpace(usuario.Value) &&
                !string.IsNullOrWhiteSpace(contrasena1.Value)
               )
            {
                //Revisa si el usuario es administrador o miembro
                String usuarioS = ((SiteMaster)this.Master).nombreUsuario;
                bool esAdmin = revisarPerfil(usuarioS, false);
                //Si no es administrador no puede modificar el perfil de usuario
                if (!esAdmin)
                {   
                    perfil.Disabled = true;
                }
                else
                {
                    perfil.Disabled = false;
                }
                //Se habilitan campos y botones de guardar y cancelar cambios
                btnInsertar.Disabled = true;
                btnEliminar.Disabled = true;
                btnModificar.Disabled = false;
                btnAceptar.InnerHtml = "Guardar";
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
                usuario.Disabled = false;
                contrasena1.Disabled = false;
                contrasena2.Disabled = false;
                contrasena2.Visible = true;
                repcontrasenalabel.Visible = true;
                contrasena1.Style.Value = "margin: 4px;";
            }
            else
            {
                //Para modificar un recurso debe primero seleccionarse del grid, se le muestra un mensaje al usuario indicandolo.
                String faltantes = "Debe seleccionar un recurso en la tabla primero.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);
            }

        }

        /**
         * Descripcion: La acción que se realiza al presionar el boton de eliminar:
         * Elimina un recurso, seleccionado del grid de recursos humanos, de la base de datos.
         * Recibe Object    @sender. No se utiliza
         *        EventArgs @e. No se utiliza
         * No devuelve nada.         
        */
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            btnEliminar.Disabled = false;
            btnAceptar.Disabled = false;
            btnCancelar.Disabled = false;
            btnCancelar.Visible = false;
            btnAceptar.Visible = false;
            
            //Revisa que se haya seleccionado un recurso del grid
            if (!string.IsNullOrWhiteSpace(cedula.Value))
            {
                char[] charsToTrim = { '-', ' ', '/' };
                int cedulaE;
                bool parsedCed = int.TryParse(cedula.Value.Trim(charsToTrim), out cedulaE);
                if (!parsedCed)
                {
                    //Incorrecto formato de cédula
                }
                //Realiza la consulta que elimina recurso de la base de datos
                int resultado = controlRH.eliminaRH(cedulaE);

                String resultadoS;
                switch (resultado)
                {
                    //0: todo correcto
                    case 0:
                        resultadoS = "Se eliminó la información correctamente";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + resultadoS + "')", true);
                        break;
                    //Error en eliminación de usuario
                    case -1:
                        resultadoS = "Error al eliminar la información de la persona (no se afectó ningún registro)";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                        break;
                    //Error SQL inesperado
                    default:
                        resultadoS = "Error al eliminar los datos, intente de nuevo";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                        break;
                }
                gridRecursos.SelectedIndex = -1;
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                deshabilitaCampos();
                refrescaTabla();
            }
            //Si el usuario no seleccionó un recurso del grid se le muestra un mensaje de alerta
            else
            {
                String faltantes= "Debe seleccionar un recurso en la tabla primero.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);
            }
            btnAceptar.Visible = true;
            btnCancelar.Visible = true;
        }

        /**
         * Descripcion: Controla la accion del boton de aceptar, dependiendo si se está eliminando o insertando un recurso humano.
         * Recibe object    @sender
         *        EventArgs @e
         * No devuelve nada.         
        */
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!btnInsertar.Disabled) //Si se está insertando un nuevo recurso humano
            {//Inserción
                if (//Revisa si la información está completa
                    !string.IsNullOrWhiteSpace(cedula.Value) &&
                    !string.IsNullOrWhiteSpace(nombre.Value) &&
                    !string.IsNullOrWhiteSpace(pApellido.Value) &&
                    !string.IsNullOrWhiteSpace(sApellido.Value) &&
                    !string.IsNullOrWhiteSpace(telefono1.Value) &&
                    !string.IsNullOrWhiteSpace(correo.Value) &&
                    !string.IsNullOrWhiteSpace(perfil.Value) &&
                    !string.IsNullOrWhiteSpace(rol.Value) &&
                    !string.IsNullOrWhiteSpace(usuario.Value) &&
                    !string.IsNullOrWhiteSpace(contrasena1.Value) &&
                    !string.IsNullOrWhiteSpace(contrasena2.Value) &&
                    parseInt(cedula.Value) > 0 && cedula.Value.Length == 9 &&
                    (parseInt(telefono1.Value) > 0 && telefono1.Value.Length == 8 || parseInt(telefono2.Value) > 0 && telefono2.Value.Length == 8)
                )
                {
                    //Se guarda cada uno de los datos de los campos de texto y combobox
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
                    String contrasena1S = contrasena1.Value;
                    //Se realiza la sentencia de insercion de un nuevo recurso humano con la informacion guardada, por medio de la controladora
                    int resultado = controlRH.insertaRH(cedulaI, nombreS, pApellidoS, sApellidoS, correoS, usuarioS, contrasena1S, perfilC, -1, rolS, pTelefono, sTelefono);
                    String resultadoS = "";
                    string resultadoS0 = "";
                    //Se revisa el estado de la consulta realizada
                    switch (resultado)
                    {
                        //0: todo correcto
                        case 0:
                            resultadoS0 = "Se insertó la información correctamente";
                            EntidadRecursoH insertado = controlRH.consultaRH(cedulaI);
                            llenaCampos(insertado);
                            break;
                        //Error en insercion de usuario
                        case -1:
                            resultadoS = "Error al insertar una nueva persona";
                            break;
                        //Error en insercion de telefono
                        case -2:
                            resultadoS = "Error al insertar los teléfonos";
                            break;
                        //2627 violacion propiedad unica
                        case 2627:
                            resultadoS = "Ya existe una persona con el número de cédula o el nombre de Usuario ingresado";
                            break;
                    }
                    //Dependiendo del resultado se le muestra un mensaje al usuario:
                    //Si se hizo todo correctamente
                    if (resultado == 0)
                    {   
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + resultadoS0 + "')", true);
                    }
                    //Si hubo algun error
                    else
                    { 
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                    }

                    //Se inhabilitan campos. Se devuelve el estado de inicio de los botones.
                    btnAceptar.Disabled = true;
                    btnCancelar.Disabled = true;
                    btnEliminar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnInsertar.Disabled = false;
                    repcontrasenalabel.Visible = false;
                    contrasena2.Visible = false;
                    contrasena2.Disabled = true;
                    contrasena1.Style.Value = "margin: 4px 4px 167px 4px;";
                    gridRecursos.SelectedIndex = -1;
                    deshabilitaCampos(); 
                    refrescaTabla();
                }
                else
                {
                    //Se muestra un mensaje luego de revisar los datos
                    revisarDatos();
                }
            }
            else if (!btnModificar.Disabled)
            {//Modificación
                if (//Revisa si la información está completa
                    ViewState["idrh"]!=null &&
                    !string.IsNullOrWhiteSpace(cedula.Value) &&
                    !string.IsNullOrWhiteSpace(nombre.Value) &&
                    !string.IsNullOrWhiteSpace(pApellido.Value) &&
                    !string.IsNullOrWhiteSpace(sApellido.Value) &&
                    !string.IsNullOrWhiteSpace(telefono1.Value) &&
                    !string.IsNullOrWhiteSpace(correo.Value) &&
                    !string.IsNullOrWhiteSpace(perfil.Value) &&
                    !string.IsNullOrWhiteSpace(rol.Value) &&
                    !string.IsNullOrWhiteSpace(usuario.Value) &&
                    !string.IsNullOrWhiteSpace(contrasena1.Value) &&
                    !string.IsNullOrWhiteSpace(contrasena2.Value) &&
                    parseInt(cedula.Value) > 0 && cedula.Value.Length == 9 &&
                    (parseInt(telefono1.Value) > 0 && telefono1.Value.Length == 8 || parseInt(telefono2.Value) > 0 && telefono2.Value.Length == 8)
                )
                {
                    //Si la información está completa se guarda cada uno de los datos ingresados en variables locales
                    char[] charsToTrim = { '-', ' ', '/' };
                    int idRHI = (int)ViewState["idrh"];
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
                    String contrasena1S = contrasena1.Value;
                    //Se realiza la consulta SQL de actualizacion con la información ingresada, conectandose a la controladora
                    int resultado = controlRH.modificaRH(cedulaI, nombreS, pApellidoS, sApellidoS, correoS, usuarioS, contrasena1S, perfilC, -1, rolS, pTelefono, sTelefono, idRHI);
                    String resultadoS = "";
                    string resultadoS0 = "";
                    //Se revisa estado de la consulta
                    switch (resultado)
                    {
                        //0: todo correcto
                        case 0:
                            resultadoS0 = "Se modificó la información correctamente";
                            break;
                        //error en modificacion de usuario
                        case -1:
                            resultadoS = "Error al modificar la información de la persona";
                            break;
                        //error en modificacion de telefono
                        case -2:
                            resultadoS = "Error al modificar los teléfonos";
                            break;
                        //2627 violacion propiedad unica
                        case 2627:
                            resultadoS = "El nombre de usuario ingresado no está disponible";
                            break;
                        //Error inesperado SQL
                        default:
                            resultadoS = "Error al modificar los datos, intente de nuevo ";
                            break;
                    }
                    //Se muestra un mensaje indicando que todo se realizó correctamente
                    if (resultado == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "confirmacion('" + resultadoS0 + "')", true);
                    }
                    //Se muestra un mensaje inidicando que hubo un error
                    else
                    {   
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + resultadoS + "')", true);
                    }
                    
                    //Se inhabilitan campos y se devuelven botones a su estado de inicio
                    btnAceptar.InnerHtml = "Aceptar";
                    btnAceptar.Disabled = true;
                    btnCancelar.Disabled = true;
                    btnEliminar.Disabled = false;
                    btnModificar.Disabled = false;
                    btnInsertar.Disabled = false;
                    contrasena2.Visible = false;
                    repcontrasenalabel.Visible = false;
                    contrasena2.Disabled = true;
                    contrasena1.Style.Value = "margin: 4px 4px 167px 4px;";
                    deshabilitaCampos();
                    refrescaTabla();
                    btnTel2.Disabled = false;
                }
                else
                {
                    //Se muestra un mensaje luego de revisar los datos
                    revisarDatos();
                }
            }
        }

        /**
         * Descripcion: Controla la accion del boton de cancelar, dependiendo de si se esta insertando o modificando
         * Recibe: @sender
         *         @e
         * No devuelve nada.
         */ 
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!btnInsertar.Disabled)
            { //Cancelar inserción
                //Inhabilita y limpia los campos y devuelve botones a estado inicial

                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                contrasena2.Visible = false;
                repcontrasenalabel.Visible = false;
                contrasena2.Disabled = true;
                contrasena1.Style.Value = "margin: 4px 4px 167px 4px;";
                deshabilitaCampos();
                limpiaCampos();
            }
            else if (!btnModificar.Disabled)
            { //Cancelar modificación
                //Inhabilita y limpia los campos y devuelve botones a estado inicial, desaparece guardar y aparece aceptar
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                btnAceptar.InnerHtml = "Aceptar";
                contrasena2.Visible = false;
                repcontrasenalabel.Visible = false;
                contrasena2.Disabled = true;
                contrasena1.Style.Value = "margin: 4px 4px 167px 4px;";
                deshabilitaCampos();
                limpiaCampos();
            }
        }
        /**
         * Descripción: Revisa que se hayan ingresado todos los datos requeridos por la aplicación, 
         * además de revisar que las contraseñas sean iguales.
         * Se tiene un string para mostrar al usuario los campos que debe llenar y se muestra mediante
         * un mensaje de alerta.
         * No recibe nada.
         * No devuelve nada.
         * 
         */
        protected void revisarDatos()
        {
            string faltantes = "Falta llenar los siguientes campos: \\n";

            if (string.IsNullOrWhiteSpace(cedula.Value))
            {
                faltantes = faltantes + "Número de Cédula \\n";
            }
            if (parseInt(cedula.Value) < 0 || cedula.Value.Length != 9)
            {
                faltantes += "*La cédula debe contener 9 dígitos y no debe incluir guiones \\n";
            }
            if (string.IsNullOrWhiteSpace(nombre.Value))
            {
                faltantes = faltantes + "Nombre \\n";
            }

            if (string.IsNullOrWhiteSpace(pApellido.Value))
            {
                faltantes = faltantes + "Primer Apellido \\n";
            }

            if (string.IsNullOrWhiteSpace(sApellido.Value))
            {
                faltantes = faltantes + "Segundo Apellido \\n";
            }

            if (string.IsNullOrWhiteSpace(telefono1.Value))
            {
                faltantes = faltantes + "Número de teléfono \\n";
            }
            if ( (parseInt(telefono1.Value) < 0 || telefono1.Value.Length != 8) || ( (!string.IsNullOrWhiteSpace(telefono2.Value)) && telefono2.Value.Length != 8) )
            {
                faltantes += "*El telefono debe contener 8 digitos y no puede contener guiones \\n";
            }

            if (string.IsNullOrWhiteSpace(correo.Value))
            {
                faltantes = faltantes + "Correo electrónico \\n";
            }

            if (string.IsNullOrWhiteSpace(usuario.Value))
            {
                faltantes = faltantes + "Nombre de Usuario \\n";
            }

            if (string.IsNullOrWhiteSpace(perfil.Value))
            {
                faltantes = faltantes + "Perfil de acceso \\n";
            }
            if (string.IsNullOrWhiteSpace(rol.Value))
            {
                faltantes = faltantes + "Rol en proyecto \\n";
            }
            if (string.IsNullOrWhiteSpace(contrasena1.Value))
            {
                faltantes = faltantes + "Contraseña \\n";
            }
            if (string.IsNullOrWhiteSpace(contrasena2.Value))
            {
                faltantes = faltantes + "Debe repetir su contraseña \\n";
            }
            if (!contrasena1.Value.Equals(contrasena2.Value))
            {
                faltantes = faltantes + "Las contraseñas ingresadas no son iguales, por favor intente de nuevo \\n";
            }

            //textoAlerta.InnerHtml = faltantes;
            //alerta.Visible = true;

            //Se muestra al usuario los campos faltantes
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltantes + "')", true);
        }

        protected int parseInt(string valor){
            int parsedInt;
            char[] charsToTrim = { '-', ' ', '/' };
            bool parsed = int.TryParse(valor.Trim(charsToTrim), out parsedInt);
            if(!parsed){
                parsedInt = -1;
            }
            return parsedInt;
        }

    }
}