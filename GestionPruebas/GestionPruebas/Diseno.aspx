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

</asp:Content>
