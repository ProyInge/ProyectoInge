<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GestionPruebas.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iniciar sesi&oacute;n</title>

    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="~/Content/Interfaz_Login.css" />
    <script src="http://code.jquery.com/jquery-latest.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
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
                <a class="navbar-brand" href="#" style="color:white">Gesti&oacute;n de Proyectos y Pruebas</a>
            </div>
            <div id="navbar" class="collapse navbar-collapse">
                <ul class="nav navbar-nav">
                </ul>
            </div>
            <!--/.nav-collapse -->
        </div>
    </nav>
    <div class="container">

        <fieldset id="box">
            <form class="form-signin" runat="server">
                <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
                <h2 class="form-signin-heading" style="margin-top: 20px; color: white">Iniciar sesi&oacute;n</h2>
                <asp:TextBox ID="user" type="text" runat="server" Style="margin-top: 20px" CssClass="form-control" placeholder="Nombre de usuario" required></asp:TextBox>
                <asp:TextBox ID="password" type="password" runat="server" Style="margin-top: 20px" CssClass="form-control" placeholder="Contraseña" required></asp:TextBox>
                <asp:Button CssClass="btn btn-lg btn-primary btn-block" ID="BtnLogin" runat="server" Style="margin-top: 40px" OnClick="BtnLogin_Click" Text="Entrar" />


                <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                        <h4 class="modal-title">
                                            <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="modal-footer">
                                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </form>
        </fieldset>

    </div>




</body>
</html>
