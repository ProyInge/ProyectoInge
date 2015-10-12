using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnAceptar.Disabled = true;
            btnCancelar.Disabled = true;
            btnEliminar.Disabled = false;
            btnModificar.Disabled = false;
            btnInsertar.Disabled = false;
            btnAceptar.InnerHtml = "Aceptar";
            cedula.Disabled = true;
            nombre.Disabled = true;
            pApellido.Disabled = true;
            sApellido.Disabled = true;
            telefono.Disabled = true;
            correo.Disabled = true;
            Rol.Disabled = true;
            perfil.Disabled = true;
            usuario.Disabled = true;
            contrasena.Disabled = true;
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            btnEliminar.Disabled = true;
            btnModificar.Disabled = true;
            btnAceptar.Disabled = false;
            btnCancelar.Disabled = false;
            cedula.Disabled = false;
            nombre.Disabled = false;
            pApellido.Disabled = false;
            sApellido.Disabled = false;
            telefono.Disabled = false;
            correo.Disabled = false;
            Rol.Disabled = false;
            perfil.Disabled = false;
            usuario.Disabled = false;
            contrasena.Disabled = false;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            btnInsertar.Disabled = true;
            btnEliminar.Disabled = true;
            btnAceptar.InnerHtml = "Guardar";
            btnAceptar.Disabled = false;
            btnCancelar.Disabled = false;
            cedula.Disabled = false;
            nombre.Disabled = false;
            pApellido.Disabled = false;
            sApellido.Disabled = false;
            telefono.Disabled = false;
            correo.Disabled = false;
            Rol.Disabled = false;
            perfil.Disabled = false;
            usuario.Disabled = false;
            contrasena.Disabled = false;

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            btnInsertar.Disabled = true;
            btnModificar.Disabled = true;
            btnAceptar.Disabled = false;
            btnCancelar.Disabled = false;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if(!btnInsertar.Disabled)
            { //Inserción
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                cedula.Disabled = true;
                nombre.Disabled = true;
                pApellido.Disabled = true;
                sApellido.Disabled = true;
                telefono.Disabled = true;
                correo.Disabled = true;
                Rol.Disabled = true;
                perfil.Disabled = true;
                usuario.Disabled = true;
                contrasena.Disabled = true;
            } else if(!btnModificar.Disabled)
            { //Modificación
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                btnAceptar.InnerHtml = "Aceptar";
                cedula.Disabled = true;
                nombre.Disabled = true;
                pApellido.Disabled = true;
                sApellido.Disabled = true;
                telefono.Disabled = true;
                correo.Disabled = true;
                Rol.Disabled = true;
                perfil.Disabled = true;
                usuario.Disabled = true;
                contrasena.Disabled = true;
            } else if(!btnEliminar.Disabled)
            { //Eliminación
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                cedula.Disabled = true;
                nombre.Disabled = true;
                pApellido.Disabled = true;
                sApellido.Disabled = true;
                telefono.Disabled = true;
                correo.Disabled = true;
                Rol.Disabled = true;
                perfil.Disabled = true;
                usuario.Disabled = true;
                contrasena.Disabled = true;
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
                cedula.Disabled = true;
                nombre.Disabled = true;
                pApellido.Disabled = true;
                sApellido.Disabled = true;
                telefono.Disabled = true;
                correo.Disabled = true;
                Rol.Disabled = true;
                perfil.Disabled = true;
                usuario.Disabled = true;
                contrasena.Disabled = true;
            }
            else if (!btnModificar.Disabled)
            { //Cancelar modificación
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                btnAceptar.InnerHtml = "Aceptar";
                cedula.Disabled = true;
                nombre.Disabled = true;
                pApellido.Disabled = true;
                sApellido.Disabled = true;
                telefono.Disabled = true;
                correo.Disabled = true;
                Rol.Disabled = true;
                perfil.Disabled = true;
                usuario.Disabled = true;
                contrasena.Disabled = true;
            }
            else if (!btnEliminar.Disabled)
            { //Cancelar inserción
                btnAceptar.Disabled = true;
                btnCancelar.Disabled = true;
                btnEliminar.Disabled = false;
                btnModificar.Disabled = false;
                btnInsertar.Disabled = false;
                cedula.Disabled = true;
                nombre.Disabled = true;
                pApellido.Disabled = true;
                sApellido.Disabled = true;
                telefono.Disabled = true;
                correo.Disabled = true;
                Rol.Disabled = true;
                perfil.Disabled = true;
                usuario.Disabled = true;
                contrasena.Disabled = true;
            }
        }
    }
}