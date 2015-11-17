<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ejecucion.aspx.cs" Inherits="GestionPruebas.Ejecucion" %>

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

    <h1 style="margin-left: 1px; font-size: 50px;">Modulo de Ejecucion de Pruebas</h1>

    <h2 id="titFunc" runat="server" style="margin-left: 20px;">Seleccione una acción a ejecutar</h2>

    <button id="btnConfirmar" runat="server" onserverclick="btnEliminar_Click" style="opacity: 0.0; position: absolute; top: -120px"></button>

    <div class="btn-group">
        <button id="btnInsertar" runat="server" onserverclick="btnInsertar_Click" style="position: absolute; top: -10px; left: 720px; width: 100px; background-color: #0099CC; color: white;" type="button" class="btn">
            <span class="glyphicon glyphicon-plus"></span>
            Insertar
        </button>
    </div>
    <div class="btn-group">
        <button id="btnModificar" runat="server" onserverclick="btnModificar_Click" style="position: absolute; top: -10px; left: 830px; width: 100px; background-color: #0099CC; color: white" type="button" class="btn">
            <span class="glyphicon glyphicon-pencil"></span>
            Modificar
        </button>
    </div>

    <div class="btn-group">
        <button id="btnEliminar" runat="server" onclick="MyFunction()" style="position: absolute; top: -10px; left: 940px; width: 100px; background-color: #0099CC; color: white" type="button" class="btn">
            <span class="glyphicon glyphicon-minus"></span>
            Eliminar
        </button>
    </div>

    <div style="max-height: 800px; max-width: 1000px; margin: 40px auto; margin-bottom: 0px;">
        <div class="panel panel-primary" style="max-height: 800px; width: 900px; margin-right: 100px; float: left; overflow: hidden;">
            <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Información Personal</div>
            <div class="panel-body">
                <p style="width: 40%; float: left;">Tipo de No Conformidad:</p>
                <p style="width: 40%; float: right;">Id Caso de Prueba:</p>

                <select id="comboTipoNC" class="form-control" name="nivel" runat="server" disabled="disabled"
                    style="width: 21%; float: left; margin-bottom: 10px;" aria-describedby="comboTipoNC" required>
                    <option value="" selected disabled>Seleccione un tipo NC</option>
                    <option value="Funcionalidad">Funcionalidad</option>
                    <option value="Validación">Validación</option>
                    <option value="Opciones que no funcionaban">Opciones que no funcionaban</option>
                    <option value="Error de usabilidad">Error de usabilidad</option>
                    <option value="Excepciones">Excepciones</option>
                    <option value="No correspondencia">No correspondencia</option>
                    <option value="Ortografía">Ortografía</option>
                </select>

                <input id="idCasoText" runat="server" disabled="disabled" type="text" class="form-control"
                aria-describedby="idCaso" style="float: right; width: 27%; margin: 0px 110px 10px 0px" />
                <br /><br /><br /><br />
                <p style="width: 50%; float: left;">Descripción:</p>
                <p style="width: 45%; float: right;">Justificación:</p>
                
                <textarea id="descripcion" runat="server" rows="5" class="form-control" style="float: left; 
                max-height: 300px; width: 50%; resize: none; overflow-y: scroll; margin-bottom: 10px;" required />

                <textarea id="justificacion" runat="server" rows="5" class="form-control" style="float: right; 
                max-height: 300px; width: 45%; resize: none; overflow-y: scroll; margin-bottom: 10px;" required />
                
                <p style="width: 50%; float: left;">Nivel de Prueba:</p>
                <p style="width: 45%; float: right;">Técnica de Prueba:</p>
                <select id="nivel" class="form-control" name="nivel" runat="server" disabled="disabled"
                    style="width: 45%; float: left; margin-bottom: 10px;" aria-describedby="nivel" required>
                    <option value="" selected disabled>Seleccione un nivel</option>
                    <option value="Unitaria">Unitaria</option>
                    <option value="De Integración">De Integración</option>
                    <option value="Del Sistema">Del Sistema</option>
                    <option value="De Aceptación">De Aceptación</option>
                </select>
                <select id="tecnica" style="width: 45%; float: right; margin-bottom: 10px;" class="form-control" name="tecnica"
                runat="server" disabled="disabled" aria-describedby="tecnica" required>
                    <option value="" selected disabled>Seleccione una técnica</option>
                    <option value="Caja Negra">Caja Negra</option>
                    <option value="Caja Blanca">Caja Blanca</option>
                    <option value="Exploratoria">Exploratoria</option>
                </select>

                <p style="width: 100%; float: left;">Ambiente:</p>
                <input id="ambiente" runat="server" disabled="disabled" type="text" class="form-control" aria-describedby="Ambiente" style="float: left; width: 45%; margin-bottom: 10px;" />

                <p style="width: 100%; float: left;">Procedimiento:</p>
                <textarea id="procedimiento" runat="server" rows="5" cols="500" style="float: left; max-height: 300px; width: 100%; resize: none; overflow-y: scroll; margin-bottom: 10px;" disabled="disabled" name="proc" class="form-control" aria-describedby="proc" />

                <p style="width: 100%; float: left;">Criterios de Aceptación:</p>
                <textarea id="criterios" runat="server" rows="5" cols="500" class="form-control" style="float: left; max-height: 300px; width: 100%; resize: none; overflow-y: scroll; margin-bottom: 10px;" />

                <p style="width: 50%; float: left;">Fecha de Asignación:</p>
                <p style="width: 45%; float: right;">Responsable:</p>
                <input id="calendario" runat="server" class="form-control" style="float: left; width: 45%; margin-bottom: 10px;" type="date" disabled="disabled" required />
                <%--<select id="responsable" class="form-control" runat="server" style="width: 45%; float: right; margin-bottom: 10px;"
                    onchange="javascript:form.submit();" onserverchange="cambiaResponsableBox" required />--%>


            </div>
        </div>
    </div>
    <!--Div campos-->

    <div style="margin: 0% 0% 0% 75%;">
        <div class="btn-group">
            <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" ValidationGroup="Info" type="submit" Text="Aceptar" Style="margin: 20px 10px 0px 0px; width: 90px;" CssClass="btn btn-success" />
        </div>
        <div class="btn-group">
            <button id="btnCancelar" runat="server" onserverclick="btnCancelar_Click" style="margin-top: 20px; width: 90px;" type="button" class="btn btn-danger">
                Cancelar
            </button>
        </div>
    </div>

</asp:Content>
