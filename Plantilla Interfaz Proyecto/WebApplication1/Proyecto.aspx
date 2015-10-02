<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Proyecto.aspx.cs" Inherits="WebApplication1.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
    <label for="op-trigger"></label>

    <div class="estilo">
    <form id="formProyecto" runat="server">
    <h1 style="margin-left: 20px">Administración de Proyectos</h1>

    <div class="btn-group">
    <button  style="margin-left: 900px" type="button" class="btn btn-primary">Insertar</button>
    </div>

    <div class="btn-group">
    <button  style="margin: 15px" type="button" class="btn btn-primary">Modificar</button>
    </div>

    <div class="btn-group">
    <button  style="margin-right: 200px" type="button" class="btn btn-primary">Eliminar</button>
    </div>


<div class="panel panel-primary" style="max-height: 800px; max-width: 400px">
  <div class="panel-heading">Infomacion Proyecto</div>
  <div class="panel-body">

    <span class="input-group" id="nombreProyecto"></span>
    <input style= "margin: 4px" type="text" class="form-control" placeholder="Nombre" aria-describedby="nombreProyecto" />

    <span class="input-group" id="objetivo"></span>
    <input style= "margin: 4px" type="text" class="form-control" placeholder="Objetivo" aria-describedby="objetivo"/>

     <div class="col-xs-10" style="margin: 5px;">
       <select class="form-control" name="estado">
                <option value="">Estado</option>
                <option value="pendiente">Pendiente</option>
                <option value="asignado">Asignado</option>
                <option value="ejecucion">En Ejecucion</option>
                <option value="finalizado">Finalizado</option>
                <option value="cerrado">Cerrado</option>
       </select>
     </div>

      <form action="action_page.php">
          <input type="date" name="fecha"/>
      </form>


  </div>
</div>

<div class="panel panel-primary" style="max-height: 500px; max-width: 400px; margin-left: 450px; margin-top: -246px">
  <div class="panel-heading">Infomacion de Oficina de Usuario</div>
  <div class="panel-body">

    <span class="input-group" id="nombreOficina"></span>
    <input style= "margin: 4px" type="text" class="form-control" placeholder="Nombre de Oficina" aria-describedby="nombreOficina"/>

    <span class="input-group" id="representante"></span>
    <input style= "margin: 4px" type="text" class="form-control" placeholder="Representante" aria-describedby="representante"/>

    <span class="input-group" id="correoOficina"></span>
    <input style= "margin: 4px" type="text" class="form-control" placeholder="Correo" aria-describedby="correoOficina"/>

    <span class="input-group" id="telefonoOficina"></span>
    <input style= "margin: 4px" type="text" class="form-control" placeholder="Telefono" aria-describedby="telefonoOficina"/>

  </div>
</div>

<div class="panel panel-primary"  style="max-height: 500px; max-width: 400px; margin-top: 50px;">
  <div class="panel-heading">Recursos Disponibles</div>
  <div class="panel-body">

      <div class="col-xs-10" style="margin: 5px;">
       <select class="form-control" name="recursos">
                <option value="">Recursos</option>
       </select>
     </div>

  </div>
</div>

<div class="panel panel-primary"  style="max-height: 500px; max-width: 400px; margin-left: 450px; margin-top: -136px">
  <div class="panel-heading">Asignados</div>
  <div class="panel-body">

      <div class="col-xs-10" style="margin: 5px;">
       <select class="form-control" name="asignados">
                <option value="">Asignados</option>
       </select>
     </div>

  </div>
</div>

 <div class="btn-group">
    <button  style="margin-left: 680px" type="button" class="btn btn-success">Aceptar</button>
    </div>

    <div class="btn-group">
    <button  style="margin: 4px" type="button" class="btn btn-danger">Cancelar</button>
    </div>

    </form>
    </div>
</body>
</html>
