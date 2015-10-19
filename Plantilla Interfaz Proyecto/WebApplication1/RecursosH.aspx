<%@ Page Title="Recursos Humanos" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursosH.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <h1 style="margin-left: 20px; font-size: 50px;">Recursos Humanos</h1>


    <div class="btn-group">
        <button id="btnInsertar" runat="server" onserverclick="btnInsertar_Click" style="position:absolute; top:-10px; left:720px; width:100px; background-color: #0099CC; color: white;" type="button" class="btn">
            <span class="glyphicon glyphicon-plus"></span>
            Insertar
        </button>
    </div>
    <div class="btn-group">
        <button id="btnModificar" runat="server" onserverclick="btnModificar_Click" style="position:absolute; top:-10px; left:830px; width:100px; background-color: #0099CC; color: white" type="button" class="btn">
            <span class="glyphicon glyphicon-pencil"></span>
            Modificar
        </button>
    </div>

    <div class="btn-group">
        <button id="btnEliminar" runat="server" onserverclick="btnEliminar_Click" OnClientClick="Confirm()" style="position:absolute; top:-10px; left:940px; width:100px; background-color: #0099CC; color: white" type="button" class="btn">
            <span class="glyphicon glyphicon-minus"></span>
            Eliminar
        </button>
    </div>

    <a id="alerta" runat="server" style="margin-bottom: 30px" visible="false">
      <div class="alert alert-warning" style="margin-top: 45px;">
          <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <span class="glyphicon glyphicon-info-sign"></span><strong> WARNING!</strong><p id="textoAlerta" runat="server"> </p>
      </div>
    </a>

    <a id="alertaCorrecto" runat="server" visible="false">
      <div class="alert alert-success fade in">
          <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <span class="glyphicon glyphicon-info-ok-sign"></span><strong> SUCCESS!</strong><p id="textoConfirmacion" runat="server"> Proyecto Insertado!</p>
      </div>
    </a>

    <div style="max-height: 800px; max-width: 1000px; margin: 40px auto;">
        <div class="panel panel-primary" style="max-height: 800px; width: 450px; margin-right: 100px; float: left; overflow: hidden;">
            <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Información Personal</div>
            <div class="panel-body">

                <input id="idRH" runat="server" type="number" aria-describedby="cedula" visible="false" />

                <p>Cedula:</p>
                <input id="cedula" runat="server" style="margin: 4px" type="number" class="form-control" aria-describedby="cedula" />

                <p style="margin-top:14px;">Nombre:</p>
                <input id="nombre" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="nombre" />

                <p style="margin-top:14px;">Primer Apellido:</p>
                <input id="pApellido" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="pApellido" />

                <p style="margin-top:14px;">Segundo Apellido:</p>
                <input id="sApellido" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="sApellido" />

                <p style="margin-top:14px;">Telefono:</p>

                <input id="telefono1" runat="server" disabled="disabled" style="margin: 4px; margin-bottom: 0px;" type="number" class="form-control" aria-describedby="telefonoOficina" />
                <button id="btnTel2" runat="server" disabled="disabled" style="margin-left: 350px; margin-top: -56px; background-color: #24B8E0; color: white" type="button" class="btn btn-xs" data-toggle="collapse" data-target="#segundoTel"><span class="glyphicon glyphicon-earphone"></span><span class="glyphicon glyphicon-plus"></span></button>

                <div id="segundoTel" class="collapse" style="margin-top:-4px;">
                    <p>Telefono 2:</p>
                    <input id="telefono2" runat="server" disabled="disabled" style="margin: 4px;" type="number" class="form-control" aria-describedby="telefono2" />
                </div>

                <p style="margin-top:14px;">Correo:</p>
                <input id="correo" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="correo" />

            </div>
        </div>

        <div class="panel panel-primary" style="max-height: 800px; width: 450px; overflow: hidden;">
            <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Información de Usuario</div>
            <div class="panel-body">

                <p>Perfil:</p>
                <!--arriba derecha abajo izquierda -->
                <div class="col-xs-10" style="margin: 5px 5px 0px -10px; width: 100%;">
                    <select id="perfil" class="form-control" name="perfil" runat="server" aria-describedby="perfil">
                        <option value="" selected disabled>Seleccione</option>
                        <option value="Administrador">Administrador</option>
                        <option value="Miembro de Equipo">Miembro</option>
                    </select>
                </div>
                <br />
                <br />
                <p style="margin-top:14px;">Rol:</p>
                <div class="col-xs-10" style="margin: 0px 15px 0px -10px; width: 100%;">
                    <select id="rol" class="form-control" name="rol" runat="server" aria-describedby="rol">
                        <option value="" selected disabled>Seleccione</option>
                        <option value="Lider">Lider</option>
                        <option value="Tester">Tester</option>
                        <option value="Usuario">Usuario</option>
                    </select>
                </div>
                <br />
                <br />
                <p style="margin-top:14px;">Nombre de Usuario:</p>
                <input id="usuario" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="usuario" />

                <p style="margin-top:14px;">Contraseña:</p>
                <input id="contrasena1" runat="server" style="margin: 4px;" type="password" class="form-control" aria-describedby="contrasena" />

                <p id="repcontrasenalabel" runat="server" style="margin-top:14px;">Repita Contraseña:</p>
                <input id="contrasena2" runat="server" style="margin: 4px 4px 90px 4px;" type="password" class="form-control" aria-describedby="contrasena" />
            </div>
        </div>
    </div>
    <!--Div campos-->

    <div style="margin: 0% 0% 0% 75%; ">
        <div class="btn-group">
            <button id="btnAceptar" runat="server" onserverclick="btnAceptar_Click" style="margin: 20px 10px 0px 0px; width: 90px;" type="button" class="btn btn-success">
                Aceptar
            </button>
        </div>
        <div class="btn-group">
            <button id="btnCancelar" runat="server" onserverclick="btnCancelar_Click" style="margin-top: 20px; width: 90px;" type="button" class="btn btn-danger">
                Cancelar
            </button>
        </div>
    </div>

    <div style="margin: 50px auto;">
        <asp:GridView ID="gridRecursos" OnRowDataBound="gridRecursos_RowDataBound" OnSelectedIndexChanged="OnSelectedIndexChanged" runat="server" Style="margin: 40px auto; margin-left: 150px; height: 400px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
            <RowStyle BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
            <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" />
        </asp:GridView>
    </div>



</asp:Content>

