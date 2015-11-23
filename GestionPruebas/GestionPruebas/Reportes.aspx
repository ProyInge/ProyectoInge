<%@ Page Title="Reportes" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="GestionPruebas.Reportes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="margin-left: 20px; font-size: 50px;">Reportes</h1>

     <button id="volver" runat="server" type="button" class="btn btn-lg btn-primary" style="margin-left: 700px; margin-top: -60px" onserverclick="volverEj">Ejecuciones</button>


    <div class="panel panel-primary" style="max-height: 800px; max-width: 900px; margin: auto; margin-top: 0px">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Resumen</div>

        <div class="row">
            <div class="col col-md-6">
                <div class="panel-body" style="max-width: 400px;">

                    <p style="margin-top: 14px;">Proyecto:</p>
                    <input id="TextProyecto" runat="server" style="width: 350px;" disabled="disabled" type="text" class="form-control" aria-describedby="nombre" />

                    <div style="margin-left: 475px; margin-top: -64px">
                        <p>Tipo</p>
                        <input id="nivelPrueba" runat="server" style="width: 350px;" disabled="disabled" type="text" class="form-control" aria-describedby="tipoPrueba" />

                    </div>

                    <div style="margin-top: 30px;">
                        <p>Propósito:</p>
                        <textarea id="propositoDiseno" runat="server" style="width: 350px;" disabled class="form-control" aria-describedby="proposito" rows="5" cols="48" />

                    </div>

                    <div style="margin-top: -163px; margin-left: 475px;">
                        <p>Requerimientos Seleccionados:</p>
                        <textarea id="TextReq" runat="server" class="form-control" disabled rows="5" cols="48" style="max-width: 350px; width: 350px; resize: none; overflow-y: scroll;" />
                    </div>

                </div>
            </div>
        </div>
    </div>


    <div class="panel panel-primary" style="max-height: 250px; max-width: 900px; margin: 40px auto; ">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Configuraciones del reporte</div>
        <p style="margin-top:14px;margin-left:20px;">Tipo*</p>
        <select id="tipo" class="form-control" name="rol" runat="server" aria-describedby="rol" style="margin: 40px; margin-top:0px; width: 200px;" required>
            <option value="" selected disabled>Seleccione</option>
            <option value="calidad">Calidad de Proyecto</option>
            <option value="estado">Estado de Proyecto</option>
            <option value="resumen">Resumen de Proyecto</option>
        </select>
        <p style="margin-top:14px;margin-left:20px;">Formato*</p>
        <select id="formato" class="form-control" name="rol" runat="server" aria-describedby="rol" style="margin: 40px; margin-top:0px;width: 200px;" required>
            <option value="" selected disabled>Seleccione</option>
            <option value="pdf">PDF</option>
            <option value="doc">DOC</option>
        </select>
    </div>

    <div style="margin: 0% 0% 0% 72%;">
        <div class="btn-group">
            <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" type="submit" Text="Descargar" Style="margin: 5px 10px 0px 0px; width: 90px;" CssClass="btn btn-success" />
        </div>
        <div class="btn-group">
            <button id="btnCancelar" runat="server" onserverclick="btnCancelar_Click" style="margin-top: 5px; width: 90px;" type="button" class="btn btn-danger">Cancelar</button>
        </div>
    </div>

    <script>

        <%--function MyFunction() {
            swal({ title: "¿Eliminar caso de prueba?", text: "Se borrará el caso de prueba", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "Sí, borrar", cancelButtonText: "No, cancelar", closeOnConfirm: true, closeOnCancel: true },
            function (isConfirm) {
                if (isConfirm) {
                    $get('<%=btnConfirmar.ClientID %>').click();
                }
            });
        }--%>

        function alerta(texto) {
            swal({ title: "Cuidado!", text: texto, type: "warning" });
        }

        function confirmacion(texto) {
            swal({ title: "Correcto!", text: texto, type: "success" });
        }
    </script>
</asp:Content>
