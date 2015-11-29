<%@ Page Title="Reportes" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="GestionPruebas.Reportes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="margin-left: 20px; font-size: 50px;">Reportes</h1>


    <div id="panelresu" runat="server" class="panel panel-primary" style="max-height: 800px; max-width: 900px; margin: 40px auto;" visible="false">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Resumen</div>

        <div class="row">
            <div class="col col-md-6">
                <div class="panel-body" style="max-width: 400px;">

                    <div style="">
                        <p>Proyecto 1</p>
                        <select id="proyecto1" class="form-control" runat="server"
                            onchange="javascript:form.submit();" onserverchange="cambiaProyecto1Box" />
                    </div>

                    <div style="margin-top:10px;">
                        <p id="labelDise1" runat="server">Diseno 1</p>
                        <select id="diseno1" class="form-control" runat="server"
                            onchange="javascript:form.submit();" onserverchange="cambiaDiseno1Box" />
                    </div>

                    <div style="margin-top:10px;">
                        <p id="labelEjec1" runat="server">Ejecución 1</p>
                        <select id="ejecucion1" class="form-control" runat="server"
                            onchange="javascript:form.submit();" onserverchange="cambiaEjecucion1Box" />
                    </div>

                </div>
            </div>

            <div class="col col-md-6">
                <div class="panel-body" style="max-width: 400px;">

                    <div style="">
                        <p>Proyecto 2</p>
                        <select id="proyecto2" class="form-control" runat="server"
                            onchange="javascript:form.submit();" onserverchange="cambiaProyecto2Box" />
                    </div>

                    <div style="margin-top:10px;">
                        <p id="labelDise2" runat="server">Diseno 2</p>
                        <select id="diseno2" class="form-control" runat="server"
                            onchange="javascript:form.submit();" onserverchange="cambiaDiseno2Box" />
                    </div>

                    <div style="margin-top:10px;">
                        <p id="labelEjec2" runat="server">Ejecución 2</p>
                        <select id="ejecucion2" class="form-control" runat="server"
                            onchange="javascript:form.submit();" onserverchange="cambiaEjecucion2Box" />
                    </div>

                </div>
            </div>
        </div>
    </div>


    <div id="panelconf" runat="server" class="panel panel-primary" style="max-height: 250px; max-width: 900px; margin: 40px auto;" visible="false">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Configuraciones del reporte</div>
        <p style="margin-top:14px;margin-left:20px;">Formato*</p>
        <select id="formato" class="form-control" name="rol" runat="server" aria-describedby="rol" style="margin: 40px; margin-top:0px;width: 200px;" required>
            <option value="" selected disabled>Seleccione</option>
            <option value="pdf">PDF</option>
            <option value="docx">Word</option>
        </select>
    </div>

    <div id="paneltipo" runat="server" class="panel panel-primary" style="max-height: 250px; max-width: 900px; margin: 40px auto;" visible="true">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Configuraciones del reporte</div>
        <p style="margin-top:14px;margin-left:20px;">Tipo*</p>
        <select id="tipo" class="form-control" name="rol" runat="server" aria-describedby="rol" style="margin: 40px; margin-top:0px; width: 300px;" required>
            <option value="" selected disabled>Seleccione</option>
            <option value="calidad">Calidad de Proyecto</option>
            <option value="noconformidad">No Conformidades de Proyecto</option>
            <option value="estado">Estado de Proyecto</option>
            <option value="progreso">Progreso de Proyecto</option>
            <option value="completo">Informe Completo de Proyecto</option>
        </select>
    </div>

    <div style="margin: 0% 0% 0% 72%;">
        <div class="btn-group">
            <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" type="submit" Text="Aceptar" Style="margin: 5px 10px 0px 0px; width: 90px;" CssClass="btn btn-success" />
        </div>
        <div class="btn-group">
            <button id="btnCancelar" runat="server" onserverclick="btnCancelar_Click" style="margin-top: 5px; width: 90px;" type="button" class="btn btn-danger">Cancelar</button>
        </div>
    </div>

    <script>
        function alerta(texto) {
            swal({ title: "Cuidado!", text: texto, type: "warning" });
        }

        function confirmacion(texto) {
            swal({ title: "Correcto!", text: texto, type: "success" });
        }
    </script>
</asp:Content>
