<%@ Page Language="C#"  EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Diseno.aspx.cs" Inherits="GestionPruebas.Diseno" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="margin-left: 20px; font-size: 50px;">Diseño de Pruebas</h1>

    <div class="btn-group">
        <button id="btnInsertar" runat="server" style="position: absolute; top: -10px; left: 650px; background-color: #0099CC; color: white" type="button" class="btn"><span class="glyphicon glyphicon-plus"></span>Insertar</button>
    </div>

    <div class="btn-group">
        <button id="btnModificar" runat="server" style="position: absolute; top: -10px; left: 760px; background-color: #0099CC; color: white" type="button" class="btn"><span class="glyphicon glyphicon-pencil"></span>Modificar</button>
    </div>

    <div class="btn-group">

    <button  id="btnEliminar" runat="server" style="position:absolute; top:-10px; left: 880px; background-color: #0099CC; color:white" type="button" class="btn"><span class="glyphicon glyphicon-minus"></span> Eliminar</button>

    </div>

    <!DOCTYPE html>
<html lang="en">
<head>
  <title>Bootstrap Case</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
</head>
<body>

<div class="container">
  <ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#diseno">Administración de Diseños</a></li>
    <li><a data-toggle="tab" href="#req">Administración de Requerimientos</a></li>
  </ul>

  <div class="tab-content">
    <div id="diseno" class="tab-pane fade in active">
      <div class="panel panel-primary" style="max-height: 1400px; max-width: 1100px; margin-left: 50px; margin-top: 40px">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Infomacion de Diseño</div>
        <div class="panel-body">

            <p style="margin: 5px">Proyecto:</p>
            <div class="col-xs-10" style=" margin: 5px 5px 0px -10px; max-width:300px">
                <select id="Select1" class="form-control" name="Proyecto" runat="server" disabled="disabled" aria-describedby="Proyecto">
                </select>
            </div>
            <div class="panel panel-primary" style="max-height: 500px; max-width: 400px; margin-top: 80px; margin-left: 30px">
                <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Requerimientos Disponibles</div>
                <div class="panel-body">
                <div class="col-xs-10" style="margin: 5px;">
                    <div class="panel" style="border: 2px solid #ccc; width: 300px; height: 100px; overflow-y: scroll;">
                        <asp:CheckBoxList ID="DisponiblesChkBox" runat="server" Enabled="false"></asp:CheckBoxList>
                        </div>
                    </div>

                </div>
            </div>

            <button id="derecha" type="button" runat="server"  disabled="disabled" style="margin-left: 450px; margin-top: -300px; margin-right: 0px; margin-bottom: 0px; background-color: #24B8E0; color: white" href="" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-right"></span></button>

            <button id="izquierda" type="button" runat="server"  disabled="disabled" style="margin-left: -55px; margin-top: -180px; margin-right: 0px; margin-bottom: 0px; background-color: #24B8E0; color: white" href="" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-left"></span></button>

            <div class="panel panel-primary" style="max-height: 500px; max-width: 400px; margin-left: 520px; margin-top: -242px">
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
            <span class="input-group"></span>
            <input id="proposito" runat="server" disabled="disabled" style="margin-bottom:10px;  max-width:400px; height: 100px" type="text" class="form-control" aria-describedby="Proposito" />

              <p id="p" style="margin-top:-140px; margin-left:500px;">Procedimiento:</p>
            <span class="input-group"></span>
            <input id="Text2" runat="server" disabled="disabled" style="margin-left:500px; max-width:400px; height: 100px" type="text" class="form-control" aria-describedby="Proposito" />


              <p id="n" style="margin-top:20px";>Nivel de Prueba:</p>
            <div class="col-xs-10" style="max-width:250px" >
                <select id="nivelPrueba" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="nivel">
                    <option value="" selected disabled>Seleccione</option>
                    <option value="Unitaria">Unitaria</option>
                    <option value="De Integración">De Integración</option>
                    <option value="Del Sistema">Del Sistema</option>
                    <option value="De Acepatación">De Acepatación</option>
                </select>
            </div>

              <p id="c" style="margin-top:-25px; margin-left:500px; ">Criterios de Aceptación:</p>
            <span class="input-group"></span>
            <input id="criterios" runat="server" disabled="disabled" style="margin-left:500px; margin-bottom:0px; max-width:400px; height: 100px" type="text" class="form-control" aria-describedby="criterios" />


            <p id="x" style="margin-top:-70px;">Técnica de Prueba:</p>
            <div class="col-xs-10">
                <select id="tecnicaPrueba" style="max-width:240px" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="nivel">
                    <option value="" selected disabled>Seleccione</option>
                    <option value="Caja Negra">Caja Negra</option>
                    <option value="Caja Blanca">Caja Blanca</option>
                    <option value="Exploratoria">Exploratoria</option>
                </select>
            </div>

            <p id="r" style="margin-top:80px;margin-left:500px;">Responsable:</p>
            <div class="col-xs-10">
                <select id="responsable" style="max-width:440px; margin-top:10px;margin-left:485px" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="responsable">
                    <option value="" selected disabled>Seleccione</option>
                </select>
            </div>
           <p id="h" style="margin-top:20px;">Tipo de Prueba:</p>
            <div class="col-xs-10" style="max-width:260px" >
                <select id="tipoPrueba" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="nivel">
                    <option value="" selected disabled>Seleccione</option>
                    <option value="Funcional">Funcional</option>
                    <option value="Interfaz de Usuario">Interfaz de Usuario</option>
                    <option value="Rendimiento">Rendimiento</option>
                    <option value="Stress">Stress</option>
                     <option value="Volumen">Volumen</option>
                    <option value="Configuración">Configuración</option>
                    <option value="Instalación">Instalación</option>
                </select>
            </div>

            <p id="f" style="margin-top:10px; margin-left:500px;">Fecha de Asignación:</p>
                <input id="calendario" style="margin-top:0px; margin-left:500px; max-width:300px" runat="server" type="date" name="fecha" disabled="disabled" class="form-control" aria-describedby="fecha" />
           
             <p id="y" style="margin-top:10px;">Ambiente de Prueba:</p>
            <span class="input-group" ></span>
            <input id="Text1" runat="server" disabled="disabled" style="margin-left:14px; max-width:250px" type="text" class="form-control" aria-describedby="Ambiente" />

            
        </div>
    </div>
    </div>
       <asp:Button ID="btnAceptarInsertar" runat="server"  type="submit" Text="Aceptar" CssClass="btn btn-success"  style="position:absolute; top:1220px; left:1200px"/>
      <asp:Button ID="btnCancelar" runat="server"  type="submit" Text="Cancelar" CssClass="btn btn-danger"  style="position:absolute; top:1220px; left:1290px"/>

    <div id="req" class="tab-pane fade">
     
        <div class="tab-content">
            <div id="requer" class="tab-pane fade in active">
            <div class="panel panel-primary" style="max-height: 1400px; max-width: 1100px; margin-left: 50px; margin-top: 40px">
            <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Infomacion de Requerimientos</div>
            <div class="panel-body">

         
            

            <p>ID:</p>
            <span class="input-group"></span>
            <input id="Text3" runat="server" disabled="disabled" style="margin-bottom:10px;  max-width:200px;" type="text" class="form-control" aria-describedby="Proposito" />

            <p>Nombre:</p>
            <span class="input-group"></span>
            <input id="Text7" runat="server" disabled="disabled" style="margin-bottom:10px;  max-width:200px;" type="text" class="form-control" aria-describedby="Proposito" />

<%--<asp:GridView ID="GridView1" OnRowDataBound="gridProyecto_RowDataBound" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged" Style="margin: 40px auto; margin-left: 150px; height: 400px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">--%>

            <div>
                <asp:GridView ID="gridReq" runat="server"  Style="margin: 40px auto; margin-left: 150px; height: 400px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">

                    <RowStyle BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <HeaderStyle HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" />
                </asp:GridView>
            </div>
    
            
    </div>
    </div>
    </div>

            </div>
         <asp:Button ID="Button1" runat="server"  type="submit" Text="Aceptar" CssClass="btn btn-success"  style="position:absolute; top:420px; left:1200px"/>
      <asp:Button ID="Button2" runat="server"  type="submit" Text="Cancelar" CssClass="btn btn-danger"  style="position:absolute; top:420px; left:1290px"/>
    </div>
     
      
 
 
</div>
</body>
</html>

</asp:Content>
