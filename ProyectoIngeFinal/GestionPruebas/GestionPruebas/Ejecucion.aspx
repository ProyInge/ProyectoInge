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

        function ver()
        {
            var visual = document.querySelector('#<%=visualizar.ClientID %>');
            var archivo = document.querySelector('#<%=imagen.ClientID %>').files[0];
            var lector = new FileReader();

            lector.onloadend = function ()
            {
                visual.src = lector.result;
            }

            if (archivo)
            {
                lector.readAsDataURL(archivo);
            }
            else
            {
                visual.src = "";
            }
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
                <div style="float: left; width: 200px;">
                    <p>Tipo de No Conformidad:</p>
                    <select id="tipoNC" class="form-control" name="tipoNC" runat="server" disabled="disabled"
                        style="width: 95%; float: left; margin-bottom: 10px;" aria-describedby="comboTipoNC" >
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
                <div style="float: left; width: 136px; margin-left: 10px">
                    <p>Id Caso de Prueba:</p>
                    <select id="idCasoText" runat="server" disabled="disabled" type="text" class="form-control" aria-describedby="idCaso" style="width: 85%; float: right; margin: 0px 20px 10px 60px"/>
                </div>
                <div style="float: left; width: 200px; margin-left: 5px">
                    <p style="vertical-align: middle">Descripción:</p>
                    <textarea id="descripcionText" runat="server" disabled="disabled" rows="5" class="form-control" style="max-height: 120px; width: 90%; resize: none; overflow-y: scroll;"
                        />
                </div>
                <div style="float: left; width: 200px; margin-left: 10px">
                    <p>Justificación:</p>
                    <textarea id="justificacionText" runat="server" disabled="disabled" rows="5" class="form-control" style="max-height: 120px; width: 90%; resize: none; overflow-y: scroll;"
                        />
                </div>

                <div style="float: left; width: 200px;">
                    <p>Estado:</p>
                    <select id="ComboEstado" class="form-control" name="estado" runat="server" disabled="disabled"
                        style="width: 95%; float: left; margin-bottom: 10px;" aria-describedby="estado" >
                        <option value="" selected disabled>Seleccione un estado</option>
                        <option value="Satisfactoria">Satisfactoria</option>
                        <option value="Fallida">Fallida</option>
                        <option value="Cancelada">Cancelada</option>
                        <option value="Pendiente">Pendiente</option>
                    </select>
                </div>
                <div style="float: left; width: 215px;">
                    <p>Imagen:</p>
                    <!--boton cargar imagen-->
                    <div>
                        <input id="imagen" runat="server" type="file" accept="image/*" onchange="ver()" style="width: 100%; height: 100%; top: 0; right: 0; margin: 0; padding: 0; font-size: 10px; cursor: pointer;" />
                    </div>
                </div>

                <asp:Image ID="visualizar" runat="server" Height="225px" Width="225px" style="margin-top: 20px"/>

            </div>

            <button id="btn_agregarEntrada" runat="server" onserverclick="btn_agregarEntrada_Click" disabled="disabled" style="background-color: #0099CC; color: white; margin-left: 525px; margin-top: -200px" type="button" class="btn"><span class="glyphicon glyphicon-plus"></span>Agregar</button>

          
               

            <div style="margin: -50px 0 20px 30px;">
                <div style="overflow:auto; width: 815px; height: 100px; border: solid 1px black">
                    <asp:DataGrid
                        ID="gridNC"
                        ShowHeaderWhenEmpty="false"
                        OnRowDataBound="gridNC_RowDataBound"
                        AutoGenerateSelectButton="True"
                        CellPadding="7"
                        AutoGenerateColumns="false"
                        runat="server"
                        style="margin-left: -5px; margin-top: -5px; width: 800px; height:100px; border: solid 5px black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;border-collapse:collapse;"
                        >

                        <ItemStyle HorizontalAlign="Center" />
                        <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" Height="45px" />

                        <Columns>
                            <asp:BoundColumn
                                HeaderText="Tipo"
                                DataField="Tipo"
                                />

                            <asp:BoundColumn
                                HeaderText="Id Caso"
                                DataField="IdCaso" />

                            <asp:BoundColumn
                                HeaderText="Estado"
                                DataField="Estado" />

                            <asp:TemplateColumn
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5px"
                                HeaderText="  Modificar">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnModificarItemNC" OnClick="btnModificarItemNC_Command">
                                        <span aria-hidden="true" class="glyphicon glyphicon-pencil blueColor" style="font-size:20px"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn
                                HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5px"
                                HeaderText="  Eliminar">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnEliminarItemNC" OnClick="btnEliminarItemNC_Command">
                                                  <span runat="server" aria-hidden="true" class="glyphicon glyphicon-minus blueColor" style="font-size:20px"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                        </Columns>

                    </asp:DataGrid>
                </div>
            </div>
            <div>             

                <p style="width: 50%; float: left; margin-top: 30px; margin-left: 20px">Fecha de última ejecución:</p>
                <p style="width: 45%; float: right; margin-top: 40px">Responsable:</p>
                <input type="date" runat="server" id="calendario" class="form-control" style="float: left; width: 25%; margin: 5px 550px 10px 15px" disabled="disabled" required />
                <%--<select id="responsable" class="form-control" runat="server" style="width: 45%; float: right; margin-bottom: 10px;"
                    onchange="javascript:form.submit();" onserverchange="cambiaResponsableBox" required />--%>
                <select id="responsable" class="form-control" disabled="disabled" runat="server" style="width: 30%; float: right; margin-bottom: 10px; margin-right: 180px; margin-top: -45px" required />

                <p style="width: 100%; float: left; margin-left: 20px">Incidencias:</p>
                <textarea id="TextIncidencias" runat="server" rows="5" cols="500" style="float: left; max-height: 300px; width: 90%; resize: none; overflow-y: scroll; margin-bottom: 10px; margin-left: 20px"
                    disabled="disabled" name="incidencias" class="form-control" aria-describedby="incidencias" />
            </div>
        </div>
    </div>

    <div style="margin: 0% 0% 0% 75%;">
        <div class="btn-group">
            <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Enabled="false" ValidationGroup="Info" type="submit" Text="Aceptar" Style="margin: 20px 10px 0px 0px; width: 90px;" CssClass="btn btn-success" />
        </div>
        <div class="btn-group">
            <button id="btnCancelar" runat="server" onserverclick="btnCancelar_Click" disabled="disabled" style="margin-top: 20px; width: 90px;" type="button" class="btn btn-danger">
                Cancelar
            </button>
        </div>
    </div>
    <div>
        <asp:GridView ID="gridEjecuciones" OnRowDataBound="gridEjecuciones_RowDataBound" OnSelectedIndexChanged="OnSelectedIndexChanged" runat="server" Style="margin: 40px auto; margin-left: 150px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
            <RowStyle BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" Height="80px" />
            <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" Height="45px" />
        </asp:GridView>
    </div>

</asp:Content>
