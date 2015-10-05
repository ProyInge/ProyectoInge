<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Proyecto.aspx.cs" Inherits="WebApplication1.Proyecto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Proyecto</title>

    <link rel="stylesheet" type="text/css" href="~/Content/Interfaz_Diseno.css"  />
    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap.css"  /> 

</head>
<body>   
 
    <ul class="opciones">
    <li class="op-item"><a href="Inicio.aspx">Inicio</a></li>
    <li class="op-item"><a href="RecursosH.aspx">Recursos Humanos</a></li>
    <li class="op-item"><a href="Proyecto.aspx">Proyecto</a></li>
    </ul>

    <input type="checkbox" id="op-trigger" class="op-trigger" />
    <label for="op-trigger">
    </label>

    <div class="estilo">
    <form id="formProyecto" runat="server">

     <div class="navbar navbar-inverse navbar-fixed-top" style="position:absolute;">
      <ul class="nav navbar-nav navbar-right">
        <li><a style="color: white" href="#"><span class="glyphicon glyphicon-log-out"></span> Log Out</a></li>
      </ul>
     </div>

    <h1 style="margin-left: 20px; font-size:50px;">Proyecto</h1>

    <div class="btn-group">
    <button  style="margin-left: 700px; background-color:#24B8E0; color:white" type="button" class="btn">Insertar</button>
    </div>

    <div class="btn-group">
    <button  style="margin: 15px; background-color: #24B8E0; color:white" type="button" class="btn">Modificar</button>
    </div>

    <div class="btn-group">
    <button  style="margin-right: 200px; background-color: #24B8E0; color:white" type="button" class="btn">Eliminar</button>
    </div>


<div class="panel panel-primary" style="max-height: 800px; max-width: 400px; margin-left: 100px">
  <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color:#0BF1F1">Infomacion Proyecto</div>
  <div class="panel-body">

    <p>Nombre:</p>
    <span class="input-group" id="nombreProyecto"></span>
    <input style= "margin: 4px" type="text" class="form-control" aria-describedby="nombreProyecto" />

    <p>Objetivo:</p>
    <span class="input-group" id="objetivo"></span>
    <input style= "margin: 4px" type="text" class="form-control" aria-describedby="objetivo"/>

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

      <p style="margin:20px">Fecha de Asignación:</p>
      <form action="action_page.php">
          <input type="date" name="fecha" class="form-control" aria-describedby="fecha"/>
      </form>

  </div>
</div>

<div class="panel panel-primary" style="max-height: 500px; max-width: 400px; margin-left: 600px; margin-top: -385px">
  <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color:#0BF1F1">Infomacion de Oficina de Usuario</div>
  <div class="panel-body">

    <p style="margin:8px">Nombre de Oficina:</p>
    <span class="input-group" id="nombreOficina"></span>
    <input style= "margin: 4px" type="text" class="form-control" aria-describedby="nombreOficina"/>

    <p style="margin:8px">Representante:</p>
    <span class="input-group" id="representante"></span>
    <input style= "margin: 4px" type="text" class="form-control" aria-describedby="representante"/>

    <p style="margin:8px">Correo:</p>
    <span class="input-group" id="correoOficina"></span>
    <input style= "margin: 4px" type="text" class="form-control" aria-describedby="correoOficina"/>

    <p style="margin:8px">Telefono:</p>
    <span class="input-group" id="telefonoOficina"></span>
    <input style= "margin: 4px" type="text" class="form-control" aria-describedby="telefonoOficina"/>

  </div>
</div>

<div class="panel panel-primary"  style="max-height: 500px; max-width: 400px; margin-top: 50px; margin-left: 100px">
  <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color:#0BF1F1">Recursos Disponibles</div>
  <div class="panel-body">

      <div class="col-xs-10" style="margin: 5px;">
       <select class="form-control" name="recursos">
                <option value="">Recursos Disponibles</option>
       </select>
     </div>

  </div>
</div>

<a style="margin-left:520px; margin-top:-240px; margin-right: 0px; margin-bottom: 0px; background-color:#24B8E0; color:white" href="" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-right"></span></a>

<a style="margin-left:-55px;  margin-top:-115px; margin-right: 0px; margin-bottom: 0px; background-color:#24B8E0; color:white" href="" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-left"></span></a>

<div class="panel panel-primary"  style="max-height: 500px; max-width: 400px; margin-left: 600px; margin-top: -160px">
  <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color:#0BF1F1">Asignados</div>
  <div class="panel-body">

      <div class="col-xs-10" style="margin: 5px;">
       <select class="form-control" name="asignados">
                <option value="">Recursos Asignados</option>
       </select>
     </div>

  </div>
</div>

 <div class="btn-group">
    <button  style="margin-left: 820px" type="button" class="btn btn-success">Aceptar</button>
    </div>

    <div class="btn-group">
    <button  style="margin: 4px" type="button" class="btn btn-danger">Cancelar</button>
    </div>

    </form>
    </div>

</body>
</html>
