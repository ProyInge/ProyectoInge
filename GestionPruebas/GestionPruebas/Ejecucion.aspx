<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ejecucion.aspx.cs" Inherits="GestionPruebas.Ejecucion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        function MyFunction() {
            swal({ title: "¿Eliminar Ejecución?", text: "Se borrarán sus datos asociados", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "Si, Borrar", cancelButtonText: "No, Cancelar", closeOnConfirm: true, closeOnCancel: true },
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
        <button id="btnInsertar" runat="server" onserverclick="habilitarInsertar" style="position: absolute; top: 150px; left: 720px; width: 100px; background-color: #0099CC; color: white;" type="button" class="btn">
            <span class="glyphicon glyphicon-plus"></span>
            Insertar
        </button>
    </div>
    <div class="btn-group">
        <button id="btnModificar" runat="server" onserverclick="habilitarModificar" style="position: absolute; top: 150px; left: 830px; width: 100px; background-color: #0099CC; color: white" type="button" class="btn">
            <span class="glyphicon glyphicon-pencil"></span>
            Modificar
        </button>
    </div>

    <div class="btn-group">
        <button id="btnEliminar" runat="server" onclick="MyFunction()" style="position: absolute; top: 150px; left: 940px; width: 100px; background-color: #0099CC; color: white" type="button" class="btn">
            <span class="glyphicon glyphicon-minus"></span>
            Eliminar
        </button>
    </div>

    <div class="panel panel-primary" style="max-height: 800px; max-width: 500px; margin-left: 4px; margin-top: 30px">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Resumen</div>

        <div class="row">
            <div class="col col-md-6">
                <div class="panel-body" style="max-width: 400px;">
                    <p style="margin-left: 10px;">Proyecto:</p>
                    <input id="TextProyecto" runat="server" disabled="disabled" style="width: 175px; margin-left: 10px;" type="text" class="form-control" aria-describedby="proyecto" />

                    <div style="margin-left: 235px; margin-top: -64px">
                        <p>Diseño:</p>
                        <input id="TextDiseno" runat="server" disabled="disabled" style="width: 175px;" type="text" class="form-control" aria-describedby="diseno" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="max-height: 800px; max-width: 1300px; margin: 40px auto; margin-bottom: 0px; margin-left: 4px">
        <div class="panel panel-primary" style="max-height: 800px; width: 1230px; margin-right: 100px; overflow: hidden;">
            <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Información de Ejecución</div>
            <div class="panel-body">
                <div style ="float:left; width:200px;">
                    <p> Tipo de No Conformidad:</p>
                    <select id="tipoNC" class="form-control" name="tipoNC" runat="server" disabled="disabled"
                    style="width: 95%; float: left; margin-bottom: 10px;" aria-describedby="comboTipoNC" required>
                        <option value="" selected disabled>Seleccione un tipo NC</option>
                        <option value="Funcionalidad">Funcionalidad</option>
                        <option value="Validación">Validación</option>
                        <option value="Opciones que no funcionaban">Opciones que no funcionaban</option>
                        <option value="Error de usabilidad">Error de usabilidad</option>
                        <option value="Excepciones">Excepciones</option>
                        <option value="No correspondencia">No correspondencia</option>
                        <option value="Ortografía">Ortografía</option>
                </select>
                </div>
                <div style ="float:left; width:136px; margin-left:10px">
                    <p>Id Caso de Prueba:</p>
                    <select id="idCasoText" runat="server" disabled="disabled" type="text" class="form-control" aria-describedby="idCaso" style="width: 85%; float: right; margin: 0px 20px 10px 60px" required />
                </div>
                <div style ="float:left; width:200px; margin-left:5px">
                    <p style="vertical-align:middle">Descripción:</p>
                    <textarea id="descripcionText" runat="server" disabled="disabled" rows="5" class="form-control" style="max-height: 120px; 
                    width: 90%; resize: none; overflow-y: scroll;" required />
                </div>
                <div style ="float:left; width:200px; margin-left:10px">
                    <p >Justificación:</p>
                    <textarea id="justificacionText" runat="server" disabled="disabled" rows="5" class="form-control" style="max-height: 120px;
                     width: 90%; resize: none; overflow-y: scroll;" required />
                </div>

                <div style ="float:left; width:200px;">
                    <p> Estado:</p>
                    <select id="ComboEstado" class="form-control" name="estado" runat="server" disabled="disabled"
                    style="width: 95%; float: left; margin-bottom: 10px;" aria-describedby="estado" required>
                        <option value="" selected disabled>Seleccione un estado</option>
                        <option value="Satisfactoria">Satisfactoria</option>
                        <option value="Fallida">Fallida</option>
                        <option value="Cancelada">Cancelada</option>
                        <option value="Pendiente">Pendiente</option>
                </select>
                </div>                                    
                <div style ="float:left; width:215px;">
                    <p>Imagen:</p>
                  <!--boton cargar imagen-->
                   <div>
                        <input type="file" accept="image/*" style="width: 100%; height: 100%; top: 0; right: 0; margin: 0; padding: 0; font-size: 10px; cursor: pointer;" />
                    </div>
                 </div>

                <div>
                    <asp:ListBox ID="listEntradas" runat="server" Style="margin-left: 4px; margin-top: 30px; height: 300px; width: 500px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 2px; overflow: hidden;"></asp:ListBox>
                    <button id="btnAgregarEntrada" runat="server" onserverclick="btn_agregarEntrada_Click" style="position:absolute; top:710px; left: 650px; background-color: #0099CC; color: white;" type="button" class="btn"><span class="glyphicon glyphicon-plus"></span>Agregar</button>
                    <button id="btnQuitar" runat="server" onserverclick="btnQuitar_Click" style="position: absolute; top: 750px; left: 650px; background-color: #0099CC; color: white" type="button" class="btn btn-group"><span class="glyphicon glyphicon-minus"></span>Quitar de la lista</button>
                    <button id="btnLimpiarLista" runat="server" onserverclick="btnLimpiarLista_Click" style="position: absolute; top: 790px; left: 650px; background-color: #0099CC; color: white" type="button" class="btn btn-group"><span class="glyphicon glyphicon-minus"></span>Limpiar lista</button>
                    <%--<button id="btn" runat="server" style="position: absolute; top: 840px; left: 650px; background-color: #0099CC; color: white" type="button" class="btn btn-group"><span class="glyphicon glyphicon-pencil"></span>Modificar</button>--%>
                </div>
                
                                            
                <p style="width: 50%; float: left;">Fecha de última ejecución:</p>
                <p style="width: 45%; float: right;">Responsable:</p>
                <input id="calendario" runat="server" class="form-control" style="float: left; width: 25%; margin-bottom: 10px;" type="date" disabled="disabled" required />
                <%--<select id="responsable" class="form-control" runat="server" style="width: 45%; float: right; margin-bottom: 10px;"
                    onchange="javascript:form.submit();" onserverchange="cambiaResponsableBox" required />--%>
                <select id="responsable" class="form-control" disabled="disabled" runat="server" style="width: 30%; float: right; margin-bottom: 10px;margin-right:180px;" required />

                <p style="width: 100%; float: left;">Incidencias:</p>
                <textarea id="TextIncidencias" runat="server" rows="5" cols="500" style="float: left; max-height: 300px; width: 100%; resize: none; overflow-y: scroll; margin-bottom: 10px;"
                 disabled="disabled" name="incidencias" class="form-control" aria-describedby="incidencias" />
            </div>
        </div>
    </div>
   
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
        <div>
    <asp:GridView ID="gridEjecuciones" OnRowDataBound="gridEjecuciones_RowDataBound" OnSelectedIndexChanged="OnSelectedIndexChanged" runat="server" Style="margin: 40px auto; 
    margin-left: 150px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
        <RowStyle BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" Height="80px" />
        <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <HeaderStyle HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" Height="45px" />
    </asp:GridView>
    </div>

</asp:Content>
