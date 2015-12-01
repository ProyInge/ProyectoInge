<%@ Page Title="Recursos Humanos" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecursosH.aspx.cs" Inherits="GestionPruebas.RecursosH" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        function MyFunction() {
            swal({ title: "¿Eliminar Recurso?", text: "Se borrara sus datos asociados", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "Si, Borrar", cancelButtonText: "No, Cancelar", closeOnConfirm: true, closeOnCancel: true },
            function (isConfirm) {
                if (isConfirm) {
                    $get('<%=btnConfirmar.ClientID %>').click();
                }
            });
        }

        function alerta(texto) {
            swal({ title: "¡Cuidado!", text: texto, type: "warning" });
        }

        function confirmacion(texto) {
            swal({ title: "¡Correcto!", text: texto, type: "success" });
        }
    </script>

    <h1 style="margin-left: 20px; font-size: 50px;">Modulo de Recursos Humanos</h1>

    <h2 id="titFunc" runat="server" style="margin-left: 20px;">Seleccione una acción a ejecutar</h2>

    <button id="btnConfirmar" runat="server" onserverclick="btnEliminar_Click" style="opacity:0.0; position:absolute; top:-120px "></button>

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
        <button id="btnEliminar" runat="server" Onclick="MyFunction()" style="position:absolute; top:-10px; left:940px; width:100px; background-color: #0099CC; color: white" type="button" class="btn">
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

    <div style="max-height: 800px; max-width: 1000px; margin: 40px auto; margin-bottom: 0px;">
        <div class="panel panel-primary" style="max-height: 800px; width: 450px; margin-right: 100px; float: left; overflow: hidden;">
            <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Información Personal</div>
            <div class="panel-body">

                <p>Cedula*</p>
                <input id="cedula" runat="server" style="margin: 4px" type="text"  class="form-control" aria-describedby="cedula" pattern="\d{1}-\d{4}-\d{4}" title="La cédula debe contener el siguiente formato: #-####-####" autofocus required/>

                <p style="margin-top:14px;">Nombre*</p>
                <input id="nombre" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="nombre" required/>

                <p style="margin-top:14px;">Primer Apellido*</p>
                <input id="pApellido" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="pApellido" required />

                <p style="margin-top:14px;">Segundo Apellido*</p>
                <input id="sApellido" runat="server" style="margin: 4px" type="text" class="form-control" aria-describedby="sApellido" required/>

                <p style="margin-top:14px;">Correo</p>
                <input id="correo" runat="server" style="margin: 4px" type="email" class="form-control" aria-describedby="correo" autofocus />

                <p style="margin-top:14px;">Teléfono Personal</p>
                <input id="telefono1" runat="server" disabled="disabled" style="margin: 4px; margin-bottom: 0px; width:87%;" type="text" class="form-control" aria-describedby="telefono1" pattern="\d{4}-\d{4}" title="El teléfono debe contener el siguiente formato: ####-####" autofocus/>
                <button id="btnTel2" runat="server" disabled="disabled" style="margin-left: 92%; margin-top: -56px; background-color: #24B8E0; color: white" type="button" class="btn btn-xs" data-toggle="collapse" data-target="#segundoTel"><span class="glyphicon glyphicon-earphone"></span><span class="glyphicon glyphicon-plus"></span></button>
                <div id="segundoTel" class="collapse" style="margin-top:-4px;">
                    <p>Otro teléfono</p>
                    <input id="telefono2" runat="server" disabled="disabled" style="margin: 4px;" type="text" class="form-control" aria-describedby="telefono2" pattern="\d{4}-\d{4}" title="El teléfono debe contener el siguiente formato: ####-####" autofocus  />
                </div>

            </div>
        </div>

        <div class="panel panel-primary" style="max-height: 800px; width: 450px; overflow: hidden;">
            <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Información de Usuario</div>
            <div class="panel-body">

                <p>Perfil*</p>
                <!--arriba derecha abajo izquierda -->
                <div class="col-xs-10" style="margin: 5px 5px 0px -10px; width: 107%;">
                    <select id="perfil" class="form-control" name="perfil" runat="server" aria-describedby="perfil" required>
                        <option value="" selected disabled>Seleccione</option>
                        <option value="Administrador">Administrador</option>
                        <option value="Miembro de Equipo">Miembro</option>
                    </select>
                </div>
                <br />
                <br />
                <p style="margin-top:14px;">Rol*</p>
                <div class="col-xs-10" style="margin: 0px 15px 0px -10px; width: 107%;">
                    <select id="rol" class="form-control" name="rol" runat="server" aria-describedby="rol" required>
                        <option value="" selected disabled>Seleccione</option>
                        <option value="Lider">Lider</option>
                        <option value="Tester">Tester</option>
                        <option value="Usuario">Usuario</option>
                    </select>
                </div>
                <br />
                <br />
                <p style="margin-top:14px;">Nombre de Usuario*</p>
                <input id="usuario" runat="server" style="margin: 4px;" type="text" class="form-control" aria-describedby="usuario" required/>

                <p style="margin-top:14px;">Contraseña*</p>
                <input id="contrasena1" runat="server" style="margin: 4px 4px 135px 4px;" type="text" class="form-control" aria-describedby="contrasena" autofocus required />

                <p>Campos Obligatorios(*)</p>

            </div>
        </div>
    </div>
    <!--Div campos-->

    <div style="margin: 0% 0% 0% 75%; ">
        <div class="btn-group">
            <asp:Button id="btnAceptar" runat="server" onclick="btnAceptar_Click" validationgroup="Info" type="submit" Text="Aceptar"  style="margin: 20px 10px 0px 0px; width: 90px;" CssClass="btn btn-success"/>
        </div>
        <div class="btn-group">
            <button id="btnCancelar" runat="server" onserverclick="btnCancelar_Click" style="margin-top: 20px; width: 90px;" type="button" class="btn btn-danger">
                Cancelar
            </button>
        </div>
    </div>

    <div style="margin: 50px auto;">
        <asp:GridView ID="gridRecursos" OnRowDataBound="gridRecursos_RowDataBound" OnSelectedIndexChanged="OnSelectedIndexChanged" runat="server" Style="margin: 40px auto; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
            <RowStyle Height="35px" BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
            <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle Height="45px" HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" />
        </asp:GridView>
    </div>



</asp:Content>
