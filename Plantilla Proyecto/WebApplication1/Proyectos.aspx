<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Proyectos.aspx.cs" Inherits="WebApplication1.Inicio" %>

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
    <li class="op-item"><a href="Default.aspx">Inicio</a></li>
    <li class="op-item"><a href="RecursosH.aspx">Recursos Humanos</a></li>
    <li class="op-item"><a href="Proyectos.aspx">Proyectos</a></li>
	<li class="op-item"><a href="Usuarios.aspx">Usuarios</a></li>
</ul>

    <input type="checkbox" id="op-trigger" class="op-trigger" />
    <label for="op-trigger"></label>

    <div class="estilo">
    <form id="formProy" runat="server">
    <h1 style="margin-left: 20px">Proyectos</h1>

    </form>
    </div>
</body>
</html>
