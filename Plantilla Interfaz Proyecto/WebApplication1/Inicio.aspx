<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="WebApplication1.Inicio" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        

    <h1 style="margin-left: 20px">Inicio</h1>

   <fieldset id="box" style="margin-left: 300px; margin-top: 80px; margin-bottom:0px; margin-right:0px">

       <a style="font-size:30px; color:white; text-decoration: none;" href="RecursosH.aspx">Recursos Humanos</a>

    </fieldset>

    <div id="box" style="margin-bottom: 0px; margin-left: 700px; margin-top:-300px; margin-right:0px; height: 400px">

     <a href="Proyecto.aspx" style="font-size:30px; color:white; text-decoration: none">Proyectos</a>
       
    </div>

</asp:Content>