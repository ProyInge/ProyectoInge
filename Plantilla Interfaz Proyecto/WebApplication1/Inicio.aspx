<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="WebApplication1.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio</title>

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
    <form id="formInicio" runat="server" action="#box">

        <div class="navbar navbar-inverse navbar-fixed-top" style="position:absolute;">
         <ul class="nav navbar-nav navbar-right">
           <li><a id="log_out" style="color:white" href="#"><span class="glyphicon glyphicon-log-out"></span> Log Out</a></li>
         </ul>
        </div>

    <h1 style="margin-left: 20px">Inicio</h1>

   <fieldset id="box" style="margin-left: 300px; margin-top: 80px; margin-bottom:0px; margin-right:0px">

       <a style="font-size:30px; color:white; text-decoration: none;" href="RecursosH.aspx">Recursos Humanos</a>

    </fieldset>

    <div id="box" style="margin-bottom: 0px; margin-left: 700px; margin-top:-300px; margin-right:0px; height: 400px">

     <a href="Proyecto.aspx" style="font-size:30px; color:white; text-decoration: none">Proyectos</a>
       
    </div>

    </form>
    </div>

</body>
</html>