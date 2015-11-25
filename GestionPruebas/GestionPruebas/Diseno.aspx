<%@ Page Title="Diseño de Pruebas" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Diseno.aspx.cs" Inherits="GestionPruebas.Diseno" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="margin-left: 20px; font-size: 50px;">Modulo de Diseño de Pruebas</h1>

    <h2 id="titFunc" runat="server" style="margin-left: 20px;">Seleccione una acción a ejecutar</h2>

    <div class="btn-group">
        <button id="btnInsertar" runat="server" onserverclick="habilitarParaInsertar" style="position: absolute; top: -10px; left: 650px; background-color: #0099CC; color: white; width: 100px" type="button" class="btn"><span class="glyphicon glyphicon-plus"></span>Insertar</button>
    </div>

    <div class="btn-group">
        <button id="btnModificar" runat="server" onserverclick="habilitarParaModificar" style="position: absolute; top: -10px; left: 760px; background-color: #0099CC; color: white; width: 100px" type="button" class="btn"><span class="glyphicon glyphicon-pencil"></span>Modificar</button>
    </div>

    <div class="btn-group">
        <button id="btnEliminar" runat="server" onclick=" MyFunction()" style="position: absolute; top: -10px; left: 870px; background-color: #0099CC; color: white; width: 100px" type="button" class="btn"><span class="glyphicon glyphicon-minus"></span>Eliminar</button>
    </div>

    <div class="btn-group">
        <button id="btnConfirmar" runat="server" onserverclick="btnEliminar_Click" style="opacity: 0.0; position: absolute; top: -120px"></button>
    </div>

    <div id="panelDiseno" class="panel panel-primary" runat="server" style="height: 1100px; width: 950px; margin: auto; margin-top: 55px;">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Infomacion de Diseño</div>
        <div class="panel-body">

            <p>Campos Obligatorios(*)</p>

            <p style="margin: 5px">Proyecto:*</p>

            <div class="col-xs-10" style="margin: 5px 5px 0px -10px; width: 300px">
                <select ID="proyecto" Class="form-control" runat="server" 
                    onchange="javascript:form.submit();" onserverchange="cambiaProyectoBox" required />
            </div>

            <button id="admReq" class="btn btn-lg btn-primary" runat="server" style="float: right; background-color: #0099CC; margin-left: 150px;"
                onserverclick="habilitarAdmReq">
                Administracion de Requerimientos</button>

            <div style="margin: 80px auto; margin-bottom: 0px; height: 220px;">

                <div class="panel panel-primary" style="height: 200px; width: 390px; float: left;">
                    <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Requerimientos Disponibles</div>
                    <div class="panel-body">
                        <div class="col-xs-10" style="margin: 5px;">
                            <div class="panel" style="border: 2px solid #ccc; width: 300px; height: 100px; overflow-y: scroll;">
                                <asp:CheckBoxList ID="DisponiblesChkBox" runat="server" Enabled="false"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                </div>

                <div style="float: left; margin: 50px 43px; width: 40px;">
                    <button id="derecha" onserverclick="btnDerecha_Click" type="button" runat="server" disabled="disabled" style="margin: 5px 0px; background-color: #0099CC; color: white" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-right"></span></button>
                    <button id="izquierda" onserverclick="btnIzquierda_Click" type="button" runat="server" disabled="disabled" style="margin: 5px 0px; background-color: #0099CC; color: white" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-left"></span></button>
                </div>

                <div class="panel panel-primary" style="height: 200px; width: 390px; float: right;">
                    <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Requerimientos Asignados</div>
                    <div class="panel-body">
                        <div class="col-xs-10" style="margin: 5px;">
                            <div class="panel" style="border: 2px solid #ccc; width: 300px; height: 100px; overflow-y: scroll;">
                                <asp:CheckBoxList ID="AsignadosChkBox" runat="server" Enabled="false"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <!--Div requerimientos-->

            <p style="width: 100%; float: left;">Propósito:*</p>
            <textarea id="proposito" runat="server" rows="5" cols="500" class="form-control" style="float: left; max-height: 300px; width: 100%; resize: none; overflow-y: scroll; margin-bottom: 10px;" required />

            <p style="width: 50%; float: left;">Nivel de Prueba:*</p>
            <p style="width: 45%; float: right;">Técnica de Prueba:*</p>
            <select id="nivel" class="form-control" name="nivel" runat="server" disabled="disabled" 
                style="width: 45%; float: left; margin-bottom: 10px;" aria-describedby="nivel" required>
                <option value="" selected disabled>Seleccione un nivel</option>
                <option value="Unitaria">Unitaria</option>
                <option value="De Integración">De Integración</option>
                <option value="Del Sistema">Del Sistema</option>
                <option value="De Aceptación">De Aceptación</option>
            </select>
            <select id="tecnica" style="width: 45%; float: right; margin-bottom: 10px;" class="form-control" name="tecnica" runat="server" disabled="disabled" aria-describedby="tecnica" required>
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

            <p style="width: 50%; float: left;">Fecha de Asignación:*</p>
            <p style="width: 45%; float: right;">Responsable:*</p>
            <input id="calendario" runat="server" class="form-control" style="float: left; width: 45%; margin-bottom: 10px;" type="date" disabled="disabled" required />
            <select ID="responsable" Class="form-control" runat="server" Style="width: 45%; float: right; margin-bottom: 10px;"
                onchange="javascript:form.submit();" onserverchange="cambiaResponsableBox" required />

            <asp:ValidationSummary ID="ResumenValida" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="dropdowns" />
        </div>
    </div>
    <!--PanelDiseno-->

    <asp:Button ID="btnAceptarDiseno" runat="server" type="submit" Text="Aceptar" Enabled="false" OnClick="btnAceptar_Insertar" 
        CssClass="btn btn-success" Style="margin-left: 75%; margin-top: 10px; width: 90px;" ValidationGroup="dropdowns" />
    <button id="btnCancelarDiseno" runat="server" onserverclick="cancelarDiseno" type="button" disabled="disabled" class="btn btn-danger" style="margin-left: 5px; margin-top: 10px; width: 90px;">Cancelar</button>

    <asp:GridView ID="gridDiseno" OnRowDataBound="gridDiseno_RowDataBound" OnSelectedIndexChanged="seleccionaGridDis" OnRowCommand="gridDiseno_RowCommand" AutoGenerateColumns="false"
        runat="server" Style="margin: 40px auto; margin-left: 150px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
        <RowStyle Height="45px" BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
        <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <HeaderStyle Height="55px" HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" />
            <asp:BoundField DataField="Propósito" HeaderText="Propósito" ReadOnly="true" />
            <asp:BoundField DataField="Nivel" HeaderText="Nivel" ReadOnly="true" />
            <asp:TemplateField HeaderText="Acción">
                <ItemTemplate>
                    <asp:Button ID="btnCasos" runat="server" Style="background-color: #1db0a2; color: white;" CssClass="btn"
                        CausesValidation="false" CommandName="IrCasos" Text="Ver casos" CommandArgument='<%# Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ejecuciones">
                <ItemTemplate>
                    <asp:Button ID="btnEjecucion" runat="server" Style="background-color: #1db0a2; color: white;" CssClass="btn"
                        CausesValidation="false" CommandName="IrEjecucion" Text="Ver ejecucion" CommandArgument='<%# Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div id="panelReq" class="panel panel-primary" style="height: 250px; width: 540px; margin: auto; margin-top: 55px;" runat="server" visible="false">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Administracion de Requerimientos</div>
        <div class="panel-body">
            <p>ID:*</p>
            <span class="input-group"></span>
            <input id="idReq" runat="server" disabled="disabled" style="margin-bottom: 10px; width: 300px;" type="text" class="form-control" required />

            <p>Nombre:*</p>
            <span class="input-group"></span>
            <input id="nomReq" runat="server" disabled="disabled" style="margin-bottom: 10px; width: 300px;" type="text" class="form-control" required />
        </div>

        <button id="volver" runat="server" type="button" class="btn btn-sm btn-primary" style="margin-left: 340px; margin-top: -200px" onserverclick="habilitarAdmDiseno">Administracion de Diseño</button>

        <p style="margin-top:-25px; margin-left: 15px">Campos Obligatorios(*)</p>

    </div>

    <div style="margin-top: 15px">
    <asp:Button ID="btnAceptarReq" runat="server" OnClick="aceptarReq" Visible="false" Enabled="false" type="submit" Text="Aceptar" CssClass="btn btn-success" Style="margin-left: 670px" />
    <button id="btnCancelarReq" runat="server" onserverclick="cancelarReq" disabled="disabled" visible="false" type="button" class="btn btn-danger">Cancelar</button>
    </div>
    <asp:GridView ID="gridReq" OnRowDataBound="gridReq_RowDataBound" OnSelectedIndexChanged="seleccionaGridReq" runat="server" Visible="false" Style="margin: 40px auto; margin-left: 150px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
        <RowStyle Height="35px" BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
        <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <HeaderStyle Height="45px" HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" />
    </asp:GridView>

    <script>
        function MyFunction() {
            swal({ title: "Quiere Eliminar?", text: "Se borrara la Informacion", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "Si,Borrar", cancelButtonText: "No, Cancelar", closeOnConfirm: true, closeOnCancel: true },
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
