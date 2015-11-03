<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Diseno.aspx.cs" Inherits="GestionPruebas.Diseno" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="margin-left: 20px; font-size: 50px;">Diseño de Pruebas</h1>

    <div class="btn-group">
        <button id="btnInsertar" runat="server" onserverclick="habilitarParaInsertar" style="position: absolute; top: -10px; left: 650px; background-color: #0099CC; color: white" type="button" class="btn"><span class="glyphicon glyphicon-plus"></span>Insertar</button>
    </div>

    <div class="btn-group">
        <button id="btnModificar" runat="server" onserverclick="habilitarParaModificar" style="position: absolute; top: -10px; left: 760px; background-color: #0099CC; color: white" type="button" class="btn"><span class="glyphicon glyphicon-pencil"></span>Modificar</button>
    </div>

    <div class="btn-group">
        <button id="btnEliminar" runat="server" onclick=" MyFunction()" style="position: absolute; top: -10px; left: 880px; background-color: #0099CC; color: white" type="button" class="btn"><span class="glyphicon glyphicon-minus"></span>Eliminar</button>
    </div>

    <div class="btn-group">
        <button id="btnConfirmar" runat="server" onserverclick="btnEliminar_Click" style="opacity: 0.0; position: absolute; top: -120px"></button>
    </div>

    <div id="panelDiseno" class="panel panel-primary" runat="server" style="height: 1100px; width: 950px; margin-top: 55px; margin-left: 25px">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Infomacion de Diseño</div>
        <div class="panel-body">

            <p style="margin: 5px">Proyecto:</p>

            <div class="col-xs-10" style="margin: 5px 5px 0px -10px; width: 300px">
                <asp:DropDownList id="proyecto" CssClass="form-control"  runat="server" OnSelectedIndexChanged="cambiaProyectoBox" AutoPostBack="True" />
            </div>

            <asp:Button ID="admReq" CssClass="btn btn-lg btn-primary" runat="server" Style="background-color: #0099CC; margin-left: 150px;" Text="Administracion de Requerimientos" OnClick="habilitarAdmReq"></asp:Button>

            <div class="panel panel-primary" style="height: 200px; width: 400px; margin-top: 80px">
                <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Requerimientos Disponibles</div>
                <div class="panel-body">
                    <div class="col-xs-10" style="margin: 5px;">
                        <div class="panel" style="border: 2px solid #ccc; width: 300px; height: 100px; overflow-y: scroll;">
                            <asp:CheckBoxList ID="DisponiblesChkBox" runat="server" Enabled="false"></asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>

            <button id="derecha" onserverclick="btnDerecha_Click" type="button" runat="server" disabled="disabled" style="margin-left: 420px; margin-top: -300px; margin-right: 0px; margin-bottom: 0px; background-color: #0099CC; color: white" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-right"></span></button>

            <button id="izquierda" onserverclick="btnIzquierda_Click" type="button" runat="server" disabled="disabled" style="margin-left: -55px; margin-top: -180px; margin-right: 0px; margin-bottom: 0px; background-color: #0099CC; color: white" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-left"></span></button>

            <div class="panel panel-primary" style="height: 200px; width: 400px; margin-left: 490px; margin-top: -240px">
                <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Requerimientos Asignados</div>

                <div class="panel-body">
                    <div class="col-xs-10" style="margin: 5px;">
                        <div class="panel" style="border: 2px solid #ccc; width: 300px; height: 100px; overflow-y: scroll;">
                            <asp:CheckBoxList ID="AsignadosChkBox" runat="server" Enabled="false"></asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>

            <p>Propósito:</p>
            <textarea id="proposito" runat="server" rows="5" cols="500" style="max-height: 300px; max-width: 400px;" />


            <p style="margin-top: -130px; margin-left: 490px;">Nivel de Prueba:</p>
            <div class="col-xs-10" style="width: 270px; margin-left: 490px">
                <select id="nivel" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="nivel">
                    <option value="" selected disabled>Seleccione</option>
                    <option value="Unitaria">Unitaria</option>
                    <option value="De Integración">De Integración</option>
                    <option value="Del Sistema">Del Sistema</option>
                    <option value="De Acepatación">De Aceptación</option>
                </select>
            </div>

            <p style="margin-top: 130px; margin-right: 100px">Técnica de Prueba:</p>
            <div class="col-xs-10">
                <select id="tecnica" style="width: 240px" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="nivel">
                    <option value="" selected disabled>Seleccione</option>
                    <option value="Caja Negra">Caja Negra</option>
                    <option value="Caja Blanca">Caja Blanca</option>
                    <option value="Exploratoria">Exploratoria</option>
                </select>
            </div>

            <div style="position: absolute; top: 790px; left: 760px">
                <p>Ambiente:</p>
                <span class="input-group"></span>
                <input id="ambiente" runat="server" disabled="disabled" type="text" class="form-control" aria-describedby="Ambiente" />
            </div>

            <p>Procedimiento:</p>
            <textarea id="procedimiento" runat="server" rows="5" cols="500" style="max-height: 300px; max-width: 780px;" disabled="disabled" name ="proc" class="form-control" aria-describedby="proc" required />

            <p>Criterios de Aceptación:</p>
            <textarea id="criterios" runat="server" rows="5" cols="500" style="max-height: 300px; max-width: 780px;" />

            
           <p style="position: absolute; top: 1150px;" >Fecha de Asignación:</p>
           <input id="calendario" runat="server" style="width:250px; position: absolute; top: 1180px; left: 285px" type="date" disabled="disabled" />


            <p style="position: absolute; top: 1150px; left: 745px" >Responsable:</p> 
            <div class="col-xs-10">
                <asp:DropDownList id="responsable" CssClass="form-control" name="nivel" runat="server" style="width:250px; position: absolute; top: 35px; left: 495px" OnSelectedIndexChanged="cambiaResponsableBox" AutoPostBack="True" />
            </div>


        </div>

    </div>

    <asp:Button ID="btnAceptarDiseno" runat="server" type="submit" Text="Aceptar" Enabled="false"  OnClick="btnAceptar_Insertar" CssClass="btn btn-success" Style="position: absolute; top: 1298px; left: 990px"/>
    <button id="btnCancelarDiseno" runat="server" onserverclick="cancelarDiseno" type="button" disabled="disabled" class="btn btn-danger" style="position: absolute; top: 1298px; left: 1070px">Cancelar</button>

    <asp:GridView ID="gridDiseno" OnRowDataBound="gridDiseno_RowDataBound" OnSelectedIndexChanged="seleccionaGridDis" runat="server" Style="margin: 40px auto; margin-left: 150px; height: 400px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
        <RowStyle BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
        <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <HeaderStyle HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" />
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

    <asp:Button id="btnAceptarReq" runat="server" onclick="aceptarReq" Visible="false" Enabled="false" type="submit" Text="Aceptar" CssClass="btn btn-success" Style="margin-left: 350px" />
    <button id="btnCancelarReq" runat="server" onserverclick="cancelarReq" disabled="disabled" visible="false" type="button" class="btn btn-danger">Cancelar</button>

    <asp:GridView ID="gridReq" OnRowDataBound="gridReq_RowDataBound" OnSelectedIndexChanged="seleccionaGridReq" runat="server" Visible="false" Style="margin: 40px auto; margin-left: 150px; height: 400px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
        <RowStyle BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
        <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <HeaderStyle HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" />
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
