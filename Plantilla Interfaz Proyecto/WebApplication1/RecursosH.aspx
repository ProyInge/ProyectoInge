<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursosH.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1 style="margin-left:20px">Recursos Humanos</h1>
    </div>

<div class="container">
 <ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#home">Inicio</a></li>
    <li><a data-toggle="tab" href="#insertar">Insertar</a></li>
    <li><a data-toggle="tab" href="#consultar">Consultar</a></li>
  </ul>

  <div class="tab-content">
    <div id="home" class="tab-pane fade in active">
      <p>Pagina de Inicio</p>
    </div>

    <div id="insertar" class="tab-pane fade">
      <p>Inserte un Recurso Humano.</p>

      <div class="input-group">

        
     <span class="input-group" id="usuario"></span>
    <input style="margin: 4px" type="text" class="form-control" placeholder="Usuario" aria-describedby="usuario">
           
     <span class="input-group" id="contrasena"></span>
    <input style="margin: 4px" type="text" class="form-control" placeholder="Contraseña" aria-describedby="contrasena">

    <span class="input-group" id="cedula"></span>
    <input style="margin: 4px" type="text" class="form-control" placeholder="Cedula" aria-describedby="cedula">

    <span class="input-group" id="nombre"></span>
    <input  style="margin: 4px" type="text" class="form-control" placeholder="Nombre" aria-describedby="nombre">

    <span class="input-group" id="apellidos"></span>
    <input   style="margin: 4px" type="text" class="form-control" placeholder="Apellidos" aria-describedby="apellidos">

    <span class="input-group" id="correo"></span>
    <input  style="margin: 4px" type="text" class="form-control" placeholder="Correo" aria-describedby="correo">

    <div class="btn-group">
    <button  style="margin: 4px" type="button" class="btn btn-success">Insertar</button>
    </div>

    <div class="btn-group">
    <button  style="margin: 4px" type="button" class="btn btn-danger">Cancelar</button>
    </div>

    </div>
   </div>

    <div id="consultar" class="tab-pane fade">
      <p>Consulte un Recurso Humano</p>

      <table class="table">
    <thead>
      <tr>
        <th>Cedula</th>
        <th>Nombre</th>
        <th>Apellido</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>000000000</td>
        <td>Daniel</td>
        <td>Muñoz</td>
      </tr>
    </tbody>
  </table>

         <div class="btn-group">
    <button type="button" class="btn btn-primary">Modificar</button>
    </div>

    <div class="btn-group">
    <button type="button" class="btn btn-primary">Eliminar</button>
    </div>

   </div>
 </div>
</div>


</asp:Content>