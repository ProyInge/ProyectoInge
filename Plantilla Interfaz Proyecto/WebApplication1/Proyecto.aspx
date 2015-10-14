<%@ Page Title="Proyecto" EnableEventValidation="false" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Proyecto.aspx.cs" Inherits="WebApplication1.Proyecto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
 
    <h1 style="margin-left: 20px; font-size:50px;">Proyecto</h1>

    <div class="btn-group">
    <button id="btnInsertar" runat="server" onserverclick="btnInsertar_Click" style="margin-left: 600px; background-color:#0099CC; color:white" type="button" class="btn"><span class="glyphicon glyphicon-plus"></span> Insertar</button>
    </div>

    <div class="btn-group">
    <button id="btnModificar" runat="server"  onserverclick="btnModificar_Click" style="margin: 15px; background-color: #0099CC; color:white" type="button" class="btn"><span class="glyphicon glyphicon-pencil"></span> Modificar</button>
    </div>

    <div class="btn-group">
    <button  id="btnEliminar" runat="server" style="margin-right: 100px; background-color: #0099CC; color:white" type="button" class="btn"><span class="glyphicon glyphicon-minus"></span> Eliminar</button>
    </div>

    <a id="alerta" runat="server" visible="false">
      <div class="alert alert-warning">
          <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <span class="glyphicon glyphicon-info-sign"></span><strong> WARNING!</strong><p id="textoAlerta" runat="server"> </p>
      </div>
    </a>

    <a id="alertaCorrecto" runat="server" visible="false">
      <div class="alert alert-success fade in">
          <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <span class="glyphicon glyphicon-info-ok-sign"></span><strong> SUCCESS!</strong> Proyecto Insertado!
      </div>
    </a>

<div class="panel panel-primary" style="max-height: 800px; max-width: 400px; margin-left: 100px">
  <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color:#0BF1F1">Infomacion Proyecto</div>
  <div class="panel-body">

    <p>Nombre:</p>
    <span class="input-group"></span>
    <input  id="nombreProyecto" runat="server" disabled="disabled" style= "margin: 4px" type="text" class="form-control" aria-describedby="nombreProyecto" />

    <p>Objetivo:</p>
    <span class="input-group"></span>
      <div  style = "margin: 4px">
    <asp:TextBox CssClass="form-control"  runat="server" ReadOnly="true" ID="objetivo" />
    </div>

     <p>Estado:</p>
     <div class="col-xs-10" style="margin: 5px;">
       <select id="barraEstado" class="form-control" name="estado" runat="server" disabled="disabled" aria-describedby="estado">
                <option value="" selected disabled>Seleccione</option>
                <option value="pendiente">Pendiente</option>
                <option value="asignado">Asignado</option>
                <option value="ejecucion">En Ejecucion</option>
                <option value="finalizado">Finalizado</option>
                <option value="cerrado">Cerrado</option>
       </select>
     </div>

      <p style="margin:20px">Fecha de Asignación:</p>
      <form action="action_page.php">
          <input id="calendario" runat="server" type="date" name="fecha" disabled="disabled" class="form-control" aria-describedby="fecha"/>
      </form>

      <p style="margin: 5px">Lider:</p>
     <div class="col-xs-10" style="margin: 5px;">
       <select id="lider" class="form-control" name="lider" runat="server" disabled="disabled" aria-describedby="lider">
       </select>
     </div>

  </div>
</div>

<div class="panel panel-primary" style="max-height: 500px; max-width: 400px; margin-left: 600px; margin-top: -460px">
  <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color:#0BF1F1">Infomacion de Oficina de Usuario</div>
  <div class="panel-body">

    <p style="margin:8px">Nombre de Oficina:</p>
    <span class="input-group"></span>
    <input  id="nombreOficina" runat="server" disabled="disabled" style= "margin: 4px" type="text" class="form-control" aria-describedby="nombreOficina"/>

    <p style="margin:8px">Representante:</p>
    <span class="input-group"></span>
    <input id="representante" runat="server" disabled="disabled" style= "margin: 4px" type="text" class="form-control" aria-describedby="representante"/>

    <p style="margin:8px">Correo:</p>
    <span class="input-group"></span>
    <input  id="correoOficina" runat="server" disabled="disabled" style = "margin: 4px" type="text" class="form-control" aria-describedby="correoOficina"/>

    <p style="margin:8px">Telefono:</p>
    <span class="input-group"></span>
    <input id="telefonoOficina" runat="server" disabled="disabled" style= "margin: 4px" type="number" class="form-control" aria-describedby="telefonoOficina"/>

  </div>
</div>

<div class="panel panel-primary"  style="max-height: 500px; max-width: 400px; margin-top: 50px; margin-left: 100px">
  <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color:#0BF1F1">Recursos Disponibles</div>
  <div class="panel-body">

      <div class="col-xs-10" style="margin: 5px;">
       <select id="disponibles" class="form-control" name="recursos" runat="server" disabled="disabled">
                <option value="">Recursos Disponibles</option>
       </select>
     </div>

  </div>
</div>

<a  id="derecha" runat="server" disabled="disabled" style="margin-left:520px; margin-top:-240px; margin-right: 0px; margin-bottom: 0px; background-color:#24B8E0; color:white" href="" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-right"></span></a>

<a  id="izquierda" runat="server" disabled="disabled" style="margin-left:-55px;  margin-top:-115px; margin-right: 0px; margin-bottom: 0px; background-color:#24B8E0; color:white" href="" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-left"></span></a>

<div class="panel panel-primary"  style="max-height: 500px; max-width: 400px; margin-left: 600px; margin-top: -160px">
  <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color:#0BF1F1">Asignados</div>
  <div class="panel-body">

       <div class="col-xs-10" style="margin: 5px;">
       <select id="asignados" class="form-control" name="asignados" runat="server" disabled="disabled">
                <option value="">Recursos Asignados</option>
       </select>
     </div>

  </div>
</div>

 <div class="btn-group">
    <button  id="btnAceptarInsertar" runat="server" onserverclick="btnAceptar_Insertar" disabled="disabled" style="margin-left: 820px" type="button" class="btn btn-success">Aceptar</button>
    </div>

    <div class="btn-group">
    <button  id="btnCancelarInsertar" runat="server" onserverclick="btnCancelar_Insertar" disabled="disabled" style="margin: 4px" type="button" class="btn btn-danger">Cancelar</button>
    </div>

     <div class="btn-group">
    <button  id="btnGuardarModificar" runat="server"  onserverclick="btnGuardar_Modificar" Visible="false" disabled="disabled" style="margin-left: 820px; margin-bottom: 10px" type="button" class="btn btn-success">Guardar</button>
    </div>

        <div class="btn-group">
    <button  id="btnCancelarModificar" runat="server" onserverclick="btnCancelar_Modificar" Visible="false" disabled="disabled" style="margin:0px 4px 10px 4px" type="button" class="btn btn-danger">Cancelar</button>
    </div>


    <fieldset id="box" style="margin-left: 300px; margin-top: 80px; margin-bottom: 0px; margin-right: 0px">
        <div class="container">
            <h2 style="color:white">Lista</h2>
            <table class="table" style="color:white">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Nombre</th>
                        <th>Estado</th>
                    </tr>
                </thead>
                <tbody>

                </tbody>
            </table>
        </div>

    </fieldset>

</asp:Content>
