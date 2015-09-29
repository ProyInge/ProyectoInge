<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Inicio" %>

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
    <li class="op-item"><a href="Default.aspx">Inicio</a></li>
    <li class="op-item"><a href="RecursosH.aspx">Recursos Humanos</a></li>
    <li class="op-item"><a href="Proyectos.aspx">Proyecto</a></li>
	<li class="op-item"><a href="Usuarios.aspx">Usuarios</a></li>
</ul>

    <input type="checkbox" id="op-trigger" class="op-trigger" />
    <label for="op-trigger"></label>

    <div class="estilo">
    <form id="form1" runat="server">
    <h1 style="margin-left: 20px">Inicio</h1>

        <a href="RecursosH.aspx"><button style="margin-top:80px" type="button" class="btn btn-warning btn-lg btn-block">Recursos Humanos</button></a>
        <a href="Proyectos.aspx"><button style="margin-top:50px" type="button" class="btn btn-warning btn-lg btn-block">Proyectos</button></a>
        <a href="Usuarios.aspx"><button style="margin-top:50px" type="button" class="btn btn-warning btn-lg btn-block">Usuarios</button></a>

    </form>
    </div>
</body>
</html>
