<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="WebApplication1.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Usuarios</title>

       <link rel="stylesheet" type="text/css" href="~/Content/Interfaz_Diseno.css"  />
    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap.css"  /> 

</head>
<body>

    <ul class="opciones">
    <li class="op-item"><a href="Inicio.aspx">Inicio</a></li>
    <li class="op-item"><a href="Default.aspx">Recursos Humanos</a></li>
    <li class="op-item"><a href="Proyecto.aspx">Proyecto</a></li>
	<li class="op-item"><a href="Usuarios.aspx">Usuarios</a></li>
</ul>

    <input type="checkbox" id="op-trigger" class="op-trigger" />
    <label for="op-trigger"></label>

    <div class="estilo">
    <form id="form1" runat="server">
    <h1 style="margin-left: 20px">Usuarios</h1>

    </form>
    </div>
</body>
</html>
