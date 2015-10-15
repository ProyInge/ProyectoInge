<%@ Page Title="Recursos Humanos" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursosH.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="margin-left: 20px; font-size: 50px;">Recursos Humanos</h1>

    <div class="btn-group">
        <button id="btnInsertar" runat="server" onserverclick="btnInsertar_Click" style="margin-left: 672px; background-color: #0099CC; color: white; top: 0px; left: -14px;" type="button" class="btn">
            <span class="glyphicon glyphicon-plus"></span>
            Insertar
        </button>
    </div>

    <div class="btn-group">
        <button id="btnModificar" runat="server" onserverclick="btnModificar_Click" style="margin: 0px 15px 0px 15px; background-color: #0099CC; color: white" type="button" class="btn">
            <span class="glyphicon glyphicon-pencil"></span>
            Modificar</button>
    </div>

    <div class="btn-group">
        <button id="btnEliminar" runat="server" onserverclick="btnEliminar_Click" style="margin-right: 100px; background-color: #0099CC; color: white" type="button" class="btn">
            <span class="glyphicon glyphicon-minus"></span>
            Eliminar
        </button>
    </div>

    <div>
        <div class="panel panel-primary" style="max-height: 800px; max-width: 400px; margin-left: 100px; margin-top: 30px;">
            <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Información Personal</div>
            <div class="panel-body">

                <p>Cedula:</p>
                <input id="cedula" runat="server" style="margin: 4px" type="number" class="form-control" aria-describedby="cedula" />

                <p>Nombre:</p>
                <input id="nombre" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="nombre" />

                <p>Primer Apellido:</p>
                <input id="pApellido" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="pApellido" />

                <p>Segundo Apellido:</p>
                <input id="sApellido" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="sApellido" />

                <p style="margin: 8px">Telefono:</p>
                <span class="input-group"></span>
                <input id="telefono1" runat="server" disabled="disabled" style="margin: 4px; margin-bottom: 0px; width: 300px" type="number" class="form-control" aria-describedby="telefonoOficina" />

                <button id="btnTel2" runat="server" disabled="disabled" style="margin-left: 330px; margin-top: -60px; background-color: #24B8E0; color: white" type="button" class="btn btn-xs" data-toggle="collapse" data-target="#segundoTel"><span class="glyphicon glyphicon-earphone"></span><span class="glyphicon glyphicon-plus"></span></button>

                <div id="segundoTel" class="collapse">
                    <p style="margin-top: -5px">Telefono 2:</p>
                    <span class="input-group"></span>
                    <input id="telefono2" runat="server" disabled="disabled" style="margin-top: -10px" type="number" class="form-control" aria-describedby="telefono2" />
                </div>

                <p>Correo:</p>
                <input id="correo" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="correo" />

            </div>
        </div>

        <div class="panel panel-primary" style="max-height: 800px; max-width: 400px; margin-left: 600px; margin-top: -500px">
            <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Información de Usuario</div>
            <div class="panel-body">

                <p>Perfil:</p>
                <!--arriba derecha abajo izquierda -->
                <div class="col-xs-10" style="margin: 5px 5px 0px -10px;">
                    <select id="perfil" class="form-control" name="perfil" runat="server" aria-describedby="perfil">
                        <option value="" selected disabled>Seleccione</option>
                        <option value="Administrador">Administrador</option>
                        <option value="Miembro de Equipo">Miembro</option>
                    </select>
                </div>
                <br />
                <br />
                <p style="margin-top: 5px;">Rol:</p>
                <div class="col-xs-10" style="margin: 0px 15px 0px -10px;">
                    <select id="rol" class="form-control" name="rol" runat="server" aria-describedby="rol">
                        <option value="" selected disabled>Seleccione</option>
                        <option value="Lider">Lider</option>
                        <option value="Tester">Tester</option>
                        <option value="Usuario">Usuario</option>
                    </select>
                </div>
                <br />
                <br />
                <p style="margin: 8px 0px 0px 0px">Nombre de Usuario:</p>
                <input id="usuario" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="usuario" />

                <p style="margin: 8px 0px 0px -2px">Contraseña:</p>
                <input id="contrasena" runat="server" style="margin: 4px 4px 130px 4px" type="text" class="form-control" aria-describedby="contrasena" />
            </div>
        </div>

        <div class="btn-group">
            <button id="btnAceptar" runat="server" onserverclick="btnAceptar_Click" style="margin-left: 880%; margin-top: 20px; width: 90px;" type="button" class="btn btn-success">
                Aceptar
            </button>
        </div>

        <div class="btn-group">
            <button id="btnCancelar" runat="server" onserverclick="btnCancelar_Click" style="margin-left: 900%; margin-top: 20px; width: 90px;" type="button" class="btn btn-danger">
                Cancelar
            </button>
        </div>

        <div>
            <asp:GridView ID="gridRecursos" OnRowDataBound="gridRecursos_RowDataBound" OnSelectedIndexChanged="OnSelectedIndexChanged" runat="server" Style="margin: 40px auto; margin-left: 150px; height: 400px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
                <RowStyle BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
                <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <HeaderStyle HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" />
            </asp:GridView>
        </div>

    </div>


</asp:Content>
