<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="WebApplication1.Inicio" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="margin-left: 20px; font-size:50px;">Inicio</h1>

    <fieldset id="box" style="margin-left: 100px; margin-top: 80px; margin-bottom: 10px; margin-right: 0px; height:100px; width: 490px; border: 2px solid #0099CC; background: #0099CC">
        <a style="font-size: 30px; color: white; text-decoration: none;" href="RecursosH.aspx">Recursos Humanos</a>
    </fieldset>

    <div id="box" style="margin-bottom: 0px; margin-left: 600px; margin-top: -110px; margin-right: 0px; height: 160px; border: 2px solid #0099CC; background: #0099CC">
        <a href="Proyecto.aspx" style="font-size: 30px; color: white; text-decoration: none">Proyectos</a>
    </div>

    <div id="box" style="margin-bottom: 0px; margin-left: 540px; margin-top: -45px; margin-right: 0px; height: 50px; width: 50px; border: 2px solid #0099CC; background: #0099CC">
    </div>

    <div id="box" style="margin-bottom: 0px; margin-left: 100px; margin-top: -50px; margin-right: 0px; height: 300px; width:430px; border: 2px solid #0099CC; background: #0099CC">
        <a href="MiembrosProyecto.aspx" style="font-size: 30px; color: white; text-decoration: none">Miembros de Proyecto</a>
    </div>

    <div id="box" style="margin-bottom: 0px; margin-left: 540px; margin-top: -240px; margin-right: 0px; height: 240px; width:370px; border: 2px solid #0099CC; background:#0099CC">
        <a href="#" style="font-size: 30px; color: white; text-decoration: none"></a>
    </div>


</asp:Content>
