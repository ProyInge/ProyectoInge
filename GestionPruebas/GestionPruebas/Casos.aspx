<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Casos.aspx.cs" Inherits="GestionPruebas.Casos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        <%--function MyFunction() {
            swal({ title: "¿Eliminar Recurso?", text: "Se borrara sus datos asociados", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "Si, Borrar", cancelButtonText: "No, Cancelar", closeOnConfirm: true, closeOnCancel: true },
            function (isConfirm) {
                if (isConfirm) {
                    $get('<%=btnConfirmar.ClientID %>').click();
                }
            });
        }--%>

        function alerta(texto) {
            swal({ title: "¡Cuidado!", text: texto, type: "warning" });
        }

        function confirmacion(texto) {
            swal({ title: "¡Correcto!", text: texto, type: "success" });
        }
    </script>

    <h1 style="margin-left: 20px; font-size: 50px;">Modulo de Casos de Prueba</h1>

    <h2 id="titFunc" runat="server" style="margin-left: 20px;">Seleccione una acción a ejecutar</h2>

     <button id="volver" runat="server" type="button" class="btn btn-lg btn-primary" style="margin-left: 700px; margin-top: -60px" onserverclick="habilitarAdmD">Administracion de Diseño</button>


    <div style="max-width: 1000px; margin: 40px auto;">

        <div class="panel panel-primary" style="max-height: 800px; max-width: 900px; margin-left: 100px; margin-top: 0px">
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
    </div>


    <div class="btn-group">
        <button id="btnInsertar" runat="server" onserverclick="btnInsertar_Click" style="position: absolute; top: -10px; left: 650px; background-color: #0099CC; color: white" type="button" class="btn"><span class="glyphicon glyphicon-plus"></span>Insertar</button>
    </div>

    <div class="btn-group">
        <button id="btnModificar" runat="server" onserverclick="btnModificar_Click" style="position: absolute; top: -10px; left: 760px; background-color: #0099CC; color: white" type="button" class="btn"><span class="glyphicon glyphicon-pencil"></span>Modificar</button>
    </div>

    <div class="btn-group">
        <button id="btnEliminar" runat="server" onclick="MyFunction()" style="position: absolute; top: -10px; left: 880px; background-color: #0099CC; color: white" type="button" class="btn"><span class="glyphicon glyphicon-minus"></span>Eliminar</button>
    </div>

    <div class="btn-group">
        <button id="btnConfirmar" runat="server" onserverclick="btnEliminarCaso_Click" style="opacity: 0.0; position: absolute; top: -120px"></button>
    </div>

    <div style="max-height: 800px; max-width: 1000px; margin: 40px auto;">
        <div class="panel panel-primary" style="max-height: 850px; max-width: 900px; margin-left: 100px; margin-top: 40px; height:450px ">
            <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Informaci&oacute;n de Caso</div>
            <div class="row">
                <div class="col col-md-6">
                    <div class="panel-body" style="max-width: 400px;">

                        <p>Campos Obligatorios(*)</p>

                        <p>ID de caso:*</p>
                        <input id="idCaso" runat="server" style="margin: 4px; width: 350px;" maxlength="50" type="text" class="form-control" aria-describedby="nombre" required />


                        <p style="margin-top: 14px;">Entrada de Datos:*</p>
                        <input id="entradaDatos" runat="server" style="margin: 4px; width: 350px;" type="text" class="form-control" aria-describedby="nombre" />

                        <div class="row" style="width: 350px">
                            <div class="col col-md-6">
                                <select id="estadoBox" class="form-control" name="estado" runat="server" aria-describedby="estado" style="position: absolute; margin-left: 4px; margin-top: 4px">
                                    <option value="" selected disabled>Seleccione</option>
                                    <option value="Válido">V&aacute;lido</option>
                                    <option value="Inválido">Inv&aacute;lido</option>
                                    <option value="No aplica">No aplica</option>
                                </select>

                            </div>
                            <div class="col col-md-6">
                                <button id="btn_agregarEntrada" runat="server" onserverclick="btn_agregarEntrada_click" style="background-color: #0099CC; color: white; margin-left: 88px; margin-top: 4px" type="button" class="btn"><span class="glyphicon glyphicon-plus"></span>Agregar</button>
                            </div>


                        </div>

                        <div>
                            <asp:ListBox ID="listEntradas" runat="server" Style="margin: 10px auto; margin-left: 4px; height: 100px; width: 350px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 2px; overflow: hidden;"></asp:ListBox>
                        </div>

                        <div class="btn-group">
                            <button id="btnQuitar" runat="server" onserverclick="btnQuitar_Click" style="position: absolute; top: -10px; left: 4px; background-color: #0099CC; color: white" type="button" class="btn"><span class="glyphicon glyphicon-minus"></span>Quitar de la lista</button>
                        </div>
                        <div class="btn-group">
                            <button id="btnLimpiarLista" runat="server" onserverclick="btnLimpiarLista_Click" style="position: absolute; top: -10px; left: 150px; background-color: #0099CC; color: white" type="button" class="btn"><span class="glyphicon glyphicon-minus"></span>Limpiar lista</button>
                        </div>
                    </div>
                </div>
                <div class="col col-md-6">
                    <div class="panel-body" style="max-width: 400px;">

                        <p>Prop&oacute;sito:*</p>
                        <textarea id="proposito" runat="server" rows="5" cols="48" maxlength="200" disabled="disabled" style="resize: none; overflow-y: scroll; max-height: 300px; max-width: 350px; margin: 4px" required />

                        <p style="margin-top: 14px;">Resultado esperado:*</p>
                        <input id="resultadoEsperado" runat="server" maxlength="100" disabled="disabled" style="margin: 4px; width: 350px;" type="text" class="form-control" aria-describedby="pApellido" required />

                        <p style="margin-top: 14px;">Flujo central:*</p>
                        <textarea id="flujo" runat="server" rows="4" cols="48" maxlength="200" disabled="disabled" style="resize: none; overflow-y: scroll; max-height: 300px; max-width: 350px;" required />

                    </div>
                </div>

            </div>

        </div>

    </div>
    <div style="margin: 0% 0% 0% 75%;">
        <div class="btn-group">
            <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" type="submit" Text="Aceptar" Style="margin: 5px 10px 0px 0px; width: 90px;" CssClass="btn btn-success" />
        </div>
        <div class="btn-group">
            <button id="btnCancelar" runat="server" onserverclick="btnCancelar_Click" style="margin-top: 5px; width: 90px;" type="button" class="btn btn-danger">Cancelar</button>
        </div>
    </div>
    <div>
        <asp:GridView ID="gridCasos" OnRowDataBound="gridCasos_RowDataBound" OnSelectedIndexChanged="OnSelectedIndexChanged" runat="server" Style="margin: 40px auto; margin-left: 150px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
            <RowStyle BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" Height="80px" />
            <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" Height="45px" />
        </asp:GridView>
    </div>

    <script>

        function MyFunction() {
            swal({ title: "¿Eliminar caso de prueba?", text: "Se borrará el caso de prueba", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "Sí, borrar", cancelButtonText: "No, cancelar", closeOnConfirm: true, closeOnCancel: true },
            function (isConfirm) {
                if (isConfirm) {
                    $get('<%=btnConfirmar.ClientID %>').click();
                }
            });
        }

        function alerta(texto) {
            swal({ title: "Cuidado!", text: texto, type: "warning" });
        }

        function confirmacion(texto) {
            swal({ title: "Correcto!", text: texto, type: "success" });
        }
    </script>
</asp:Content>
