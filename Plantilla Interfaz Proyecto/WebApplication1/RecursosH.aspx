<%@ Page Title="Recursos Humanos" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursosH.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="margin-left: 20px; font-size: 50px;">Recursos Humanos</h1>

    <div class="btn-group">
        <button id="btnInsertar" runat="server" style="margin-left: 700px; background-color: #24B8E0; color: white" type="button" class="btn">Insertar</button>
    </div>

    <div class="btn-group">
        <button id="btnModificar" runat="server" style="margin: 15px; background-color: #24B8E0; color: white" type="button" class="btn">Modificar</button>
    </div>

    <div class="btn-group">
        <button id="btnEliminar" runat="server" style="margin-right: 100px; background-color: #24B8E0; color: white" type="button" class="btn">Eliminar</button>
    </div>

    <div class="panel panel-primary" style="max-height: 800px; max-width: 400px; margin-left: 100px; margin-top: 50px;">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Información Personal</div>
        <div class="panel-body">

            <p>Nombre:</p>
            <span class="input-group" id="nombreProyecto"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="nombreProyecto" />

            <p>Primer Apellido:</p>
            <span class="input-group" id="pApellido"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="pApellido" />

            <p>Segundo Apellido:</p>
            <span class="input-group" id="sApellido"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="sApellido" />


            <p>Teléfono:</p>
            <span class="input-group" id="tel1"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="tel1" />

            <p>Correo:</p>
            <span class="input-group" id="correo"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="correo" />

        </div>
    </div>

    <div class="panel panel-primary" style="max-height: 800px; max-width: 400px; margin-left: 600px; margin-top: -450px">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Información de Usuario</div>
        <div class="panel-body">

            <p>Perfil:</p>
            <div class="col-xs-10" style="margin: 5px 5px 0px -10px;"> <!--arriba derecha abajo izquierda -->
                <select id="perfil" class="form-control" name="perfil" runat="server" aria-describedby="perfil">
                    <option value="" selected disabled>Seleccione</option>
                    <option value="Administrador">Administrador</option>
                    <option value="Miembro de Equipo">Miembro</option>
                </select>
            </div>
            <br/><br/>
            <p style="margin-top: 5px;">Rol:</p>
            <div class="col-xs-10" style="margin: 0px 15px 0px -10px;">
                <select id="Rol" class="form-control" name="rol" runat="server" aria-describedby="rol">
                    <option value="" selected disabled>Seleccione</option>
                    <option value="Líder">Lider</option>
                    <option value="Tester">Tester</option>
                    <option value="Usuario">Usuario</option>
                </select>
            </div>
            <br/><br/>  
            <p style="margin: 8px 0px 0px 0px">Nombre de Usuario:</p>
            <span class="input-group" id="usuario"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="usuario" />

            <p style="margin: 8px 0px 0px -2px">Contraseña:</p>
            <span class="input-group" id="contrasena"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="contrasena" />

        </div>
    </div>

    <div class="btn-group">
        <button runat="server" onserverclick="Page_Load" style="margin-left: 820px" type="button" class="btn btn-success">Aceptar</button>
    </div>

    <div class="btn-group">
        <button style="margin: 4px" type="button" class="btn btn-danger">Cancelar</button>
    </div>


</asp:Content>
