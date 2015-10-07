<%@ Page Title="Recursos Humanos" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursosH.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="margin-left: 20px; font-size: 50px;">Recursos Humanos</h1>

    <div class="btn-group">
        <button id="btnInsertar" runat="server"  style="margin-left: 700px; background-color: #24B8E0; color: white" type="button" class="btn">Insertar</button>
    </div>

    <div class="btn-group">
        <button id="btnModificar" runat="server" style="margin: 15px; background-color: #24B8E0; color: white" type="button" class="btn">Modificar</button>
    </div>

    <div class="btn-group">
        <button id="btnEliminar" runat="server" style="margin-right: 100px; background-color: #24B8E0; color: white" type="button" class="btn">Eliminar</button>
    </div>


    <div class="panel panel-primary" style="max-height: 800px; max-width: 400px; margin-left: 100px">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Infomacion Proyecto</div>
        <div class="panel-body">

            <p>Nombre:</p>
            <span class="input-group" id="nombreProyecto"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="nombreProyecto" />

            <p>Objetivo:</p>
            <span class="input-group" id="objetivo"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="objetivo" />

            <p>Estado:</p>
            <div class="col-xs-10" style="margin: 5px;">
                <select class="form-control" name="estado" aria-describedby="estado">
                    <option value="" selected disabled>Seleccione</option>
                    <option value="pendiente">Pendiente</option>
                    <option value="asignado">Asignado</option>
                    <option value="ejecucion">En Ejecucion</option>
                    <option value="finalizado">Finalizado</option>
                    <option value="cerrado">Cerrado</option>
                </select>
            </div>

            <p style="margin: 20px">Fecha de Asignación:</p>
            <form action="action_page.php">
                <input type="date" name="fecha" class="form-control" aria-describedby="fecha" />
            </form>

        </div>
    </div>

    <div class="panel panel-primary" style="max-height: 500px; max-width: 400px; margin-left: 600px; margin-top: -385px">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Infomacion de Oficina de Usuario</div>
        <div class="panel-body">

            <p style="margin: 8px">Nombre de Oficina:</p>
            <span class="input-group" id="nombreOficina"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="nombreOficina" />

            <p style="margin: 8px">Representante:</p>
            <span class="input-group" id="representante"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="representante" />

            <p style="margin: 8px">Correo:</p>
            <span class="input-group" id="correoOficina"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="correoOficina" />

            <p style="margin: 8px">Telefono:</p>
            <span class="input-group" id="telefonoOficina"></span>
            <input style="margin: 4px" type="text" class="form-control" aria-describedby="telefonoOficina" />

        </div>
    </div>

    <div class="btn-group">
        <button runat="server" onserverclick="Page_Load" style="margin-left: 820px" type="button" class="btn btn-success">Aceptar</button>
    </div>

    <div class="btn-group">
        <button style="margin: 4px" type="button" class="btn btn-danger">Cancelar</button>
    </div>


</asp:Content>
