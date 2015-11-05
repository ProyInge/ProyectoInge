<%@ Page Title="Diseño de Pruebas" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Diseno.aspx.cs" Inherits="GestionPruebas.Diseno" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="margin-left: 20px; font-size: 50px;">Diseño de Pruebas</h1>

    <h2 id="titFunc" runat="server" style="margin-left: 20px;">Seleccione una acción a ejecutar</h2>

    <div class="btn-group">
        <button id="btnInsertar" runat="server" onserverclick="habilitarParaInsertar" style="position: absolute; top: -10px; left: 650px; background-color: #0099CC; color: white; width:100px" type="button" class="btn"><span class="glyphicon glyphicon-plus"></span>Insertar</button>
    </div>

    <div class="btn-group">
        <button id="btnModificar" runat="server" onserverclick="habilitarParaModificar" style="position: absolute; top: -10px; left: 760px; background-color: #0099CC; color: white; width:100px" type="button" class="btn"><span class="glyphicon glyphicon-pencil"></span>Modificar</button>
    </div>

    <div class="btn-group">
        <button id="btnEliminar" runat="server" onclick=" MyFunction()" style="position: absolute; top: -10px; left: 870px; background-color: #0099CC; color: white; width:100px" type="button" class="btn"><span class="glyphicon glyphicon-minus"></span>Eliminar</button>
    </div>

    <div class="btn-group">
        <button id="btnConfirmar" runat="server" onserverclick="btnEliminar_Click" style="opacity: 0.0; position: absolute; top: -120px"></button>
    </div>

    <div id="panelDiseno" class="panel panel-primary" runat="server" style="height: 1100px; width: 950px; margin: auto; margin-top: 55px;">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Infomacion de Diseño</div>
        <div class="panel-body">

            <p style="margin: 5px">Proyecto:</p>

            <div class="col-xs-10" style="margin: 5px 5px 0px -10px; width: 300px">
                <asp:DropDownList ID="proyecto" CssClass="form-control" runat="server" OnSelectedIndexChanged="cambiaProyectoBox" AutoPostBack="True" />
            </div>

            <button id="admReq" class="btn btn-lg btn-primary" runat="server" style="background-color: #0099CC; margin-left: 150px;" 
                 onserverclick ="habilitarAdmReq">Administracion de Requerimientos</button>

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

                <div style="float: left; margin: auto;">
                    <button id="izquierda" onserverclick="btnIzquierda_Click" type="button" runat="server" disabled="disabled" style="background-color: #0099CC; color: white" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-left"></span></button>
                    <button id="derecha" onserverclick="btnDerecha_Click" type="button" runat="server" disabled="disabled" style="background-color: #0099CC; color: white" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-right"></span></button>
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

            <div style="float:left; width:45%;">
            <p>Propósito:</p>
                <textarea id="proposito" runat="server" rows="5" cols="500" class="form-control"  style="max-height: 300px; width: 100%; resize:none; overflow-y: scroll;" required />

                    <p>Ambiente:</p>
                <input id="ambiente" runat="server" disabled="disabled" type="text" class="form-control" aria-describedby="Ambiente" style="width: 100%;" />

                <p>Procedimiento:</p>
                <textarea id="procedimiento" runat="server" rows="5" cols="500" style="max-height: 300px; width: 100%; resize:none; overflow-y: scroll;" disabled="disabled" name="proc" class="form-control" aria-describedby="proc" required />

                <p>Criterios de Aceptación:</p>
                <textarea id="criterios" runat="server" rows="5" cols="500" class="form-control" style="max-height: 300px; width: 100%; resize:none; overflow-y: scroll;" />

                <p style="">Fecha de Asignación:</p>
                <input id="calendario" runat="server" class="form-control" style="width: 100%;" type="date" disabled="disabled" required />
            </div>
            <!--Div Izquierdo-->

            <div style="float:right; width:45%;">
                <p style="">Nivel de Prueba:</p>
                <div class="col-xs-10" style="width: 100%">
                <select id="nivel" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="nivel" required>
                    <option value="" selected disabled>Seleccione</option>
                    <option value="Unitaria">Unitaria</option>
                    <option value="De Integración">De Integración</option>
                    <option value="Del Sistema">Del Sistema</option>
                        <option value="De Aceptación">De Aceptación</option>
                </select>
            </div>

                <p style="">Técnica de Prueba:</p>
                <div class="col-xs-10" style="width: 100%">
                    <select id="tecnica" style="" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="nivel" required>
                    <option value="" selected disabled>Seleccione</option>
                    <option value="Caja Negra">Caja Negra</option>
                    <option value="Caja Blanca">Caja Blanca</option>
                    <option value="Exploratoria">Exploratoria</option>
                </select>
            </div>


                <p style="">Responsable:</p>
                <div class="col-xs-10" style="width: 100%">
                    <asp:DropDownList ID="responsable" CssClass="form-control" name="nivel" runat="server" Style="" OnSelectedIndexChanged="cambiaResponsableBox" AutoPostBack="True" required />
                </div>
            </div>
            <!--Div Derecho-->

        </div>
    </div>
    <!--PanelDiseno-->

    <asp:Button ID="btnAceptarDiseno" runat="server" type="submit" Text="Aceptar" Enabled="false" OnClick="btnAceptar_Insertar" CssClass="btn btn-success" style="margin-left:75%; margin-top:10px; width:90px;" />
    <button id="btnCancelarDiseno" runat="server" onserverclick="cancelarDiseno" type="button" disabled="disabled" class="btn btn-danger" style="margin-left:5px; margin-top:10px; width:90px;">Cancelar</button>

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
                    <asp:Button ID="btnCasos" runat="server" Style="background-color:#1db0a2; color: white;" CssClass="btn"
                        CausesValidation="false" CommandName="IrCasos" Text="Ver casos" CommandArgument='<%# Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div id="panelReq" class="panel panel-primary" style="height: 250px; width: 500px; margin-top: 55px; margin-left: 25px" runat="server" visible="false">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Administracion de Requerimientos</div>
        <div class="panel-body">

            <p>ID:</p>
            <span class="input-group"></span>
            <input id="idReq" runat="server" disabled="disabled" style="margin-bottom: 10px; width: 300px;" type="text" class="form-control" required />

            <p>Nombre:</p>
            <span class="input-group"></span>
            <input id="nomReq" runat="server" disabled="disabled" style="margin-bottom: 10px; width: 300px;" type="text" class="form-control" required />
        </div>

        <asp:Button ID="volver" runat="server" type="button" Text="Volver" CssClass="btn btn-sm btn-primary" Style="margin-left: 340px; margin-top: -200px" OnClick="habilitarAdmDiseno" />
    </div>

    <asp:Button ID="btnAceptarReq" runat="server" OnClick="aceptarReq" Visible="false" Enabled="false" type="submit" Text="Aceptar" CssClass="btn btn-success" Style="margin-left: 350px" />
    <button id="btnCancelarReq" runat="server" onserverclick="cancelarReq" disabled="disabled" visible="false" type="button" class="btn btn-danger">Cancelar</button>

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
