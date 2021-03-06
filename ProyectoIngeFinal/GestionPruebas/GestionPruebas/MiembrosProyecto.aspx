﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiembrosProyecto.aspx.cs" MasterPageFile="~/Site.Master" Inherits="GestionPruebas.MiembrosProyecto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="margin-left: 20px; font-size: 50px;">Miembros de Equipo</h1>

    <div>
        <asp:GridView ID="gridMiembros" runat="server" Style="margin: 40px auto; margin-left: 150px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
            <RowStyle Height = "35px" BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
            <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle Height = "45px" HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" />
        </asp:GridView>
    </div>


</asp:Content>
