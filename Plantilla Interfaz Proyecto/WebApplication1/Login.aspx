<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iniciar sesi&oacute;n</title>

    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="~/Content/Interfaz_Login.css" />
</head>
<body>
    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">Gesti&oacute;n de proyectos y pruebas</a>
            </div>
            <div id="navbar" class="collapse navbar-collapse">
                <ul class="nav navbar-nav">
                </ul>
            </div>
            <!--/.nav-collapse -->
        </div>
    </nav>
    <div class="container">

        <form class="form-signin" runat="server">
            <h2 class="form-signin-heading">Iniciar sesi&oacute;n</h2>
            <asp:TextBox ID="user" type="text" runat="server" class="form-control" placeholder="Nombre de usuario" required></asp:TextBox>
            <asp:TextBox ID="password" type="password" runat="server" class="form-control" placeholder="Contraseña" required></asp:TextBox>
            <asp:Button class="btn btn-lg btn-primary btn-block" ID="BtnLogin" runat="server" onclick="BtnLogin_Click" Text="Entrar" />  

        </form>

    </div>
</body>
</html>
