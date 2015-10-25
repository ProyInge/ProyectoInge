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

    <div class="container" style="margin-top:70px">
  <ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#diseno" style="color:#3D3D3D">Administración De Diseño</a></li>
    <li><a data-toggle="tab" href="#req" style="color:#3D3D3D">Administracion de Requerimientos</a></li>
  </ul>

  <div class="tab-content">
    <div id="diseno" class="tab-pane fade in active">

        <div class="panel panel-primary" style="height: 870px; width: 950px; margin-top: 40px">
        <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Infomacion de Diseño</div>
        <div class="panel-body">

            <p style="margin: 5px">Proyecto:</p>

            <div class="col-xs-10" style=" margin: 5px 5px 0px -10px; width:300px">
                 <select id="proyecto" class="form-control" name="Proyecto" runat="server" disabled="disabled" aria-describedby="Proyecto"></select>
            </div>

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

     <button id="derecha" type="button" runat="server"  disabled="disabled" style="margin-left: 420px; margin-top: -300px; margin-right: 0px; margin-bottom: 0px; background-color: #24B8E0; color: white" href="" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-right"></span></button>

     <button id="izquierda" type="button" runat="server"  disabled="disabled" style="margin-left: -55px; margin-top: -180px; margin-right: 0px; margin-bottom: 0px; background-color: #24B8E0; color: white" href="" class="btn btn-lg"><span class="glyphicon glyphicon-chevron-left"></span></button>

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
            <span class="input-group"></span>
            <input id="proposito" runat="server" disabled="disabled" style="margin-bottom:10px;  max-width:400px; height: 100px" type="text" class="form-control" aria-describedby="Proposito" />

            <p style="margin-top:-140px; margin-left:490px;">Procedimiento:</p>
            <span class="input-group"></span>
            <input id="procedimiento" runat="server" disabled="disabled" style="margin-left:490px; max-width:400px; height: 100px" type="text" class="form-control" aria-describedby="Proposito" />


            <p style="margin-top:20px";>Nivel de Prueba:</p>
            <div class="col-xs-10" style="margin: 4px; width: 270px" >
                <select id="nivel" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="nivel">
                    <option value="" selected disabled>Seleccione</option>
                    <option value="Unitaria">Unitaria</option>
                    <option value="De Integración">De Integración</option>
                    <option value="Del Sistema">Del Sistema</option>
                    <option value="De Acepatación">De Acepatación</option>
                </select>
            </div>

            <p style="margin-top:-30px; margin-left:490px; ">Criterios de Aceptación:</p>
            <span class="input-group"></span>
            <input id="criterios" runat="server" disabled="disabled" style="margin-left:490px; width:400px; height: 100px" type="text" class="form-control" aria-describedby="criterios" />


            <p style="margin-top:-50px; margin-right:100px">Técnica de Prueba:</p>
            <div class="col-xs-10" style="margin-top: 5px">
                <select id="tecnica" style="width:240px" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="nivel">
                    <option value="" selected disabled>Seleccione</option>
                    <option value="Caja Negra">Caja Negra</option>
                    <option value="Caja Blanca">Caja Blanca</option>
                    <option value="Exploratoria">Exploratoria</option>
                </select>
            </div>

            <p style="margin-top:40px; margin-left: 485px; margin-right:100px">Responsable:</p>
                <div class="col-xs-10">
                    <select id="responsable" style="width:300px; margin-top:5px;margin-left:490px" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="responsable"></select>
                </div>

           <p style=" margin-top:-25px">Tipo de Prueba:</p>
            <div class="col-xs-10" style="width:260px; margin-top:-35px">
                <select id="tipo" class="form-control" name="nivel" runat="server" disabled="disabled" aria-describedby="nivel">
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

            <div style="margin-left:490px; margin-top:60px">
            <p style="margin-right:100px">Fecha de Asignacion:</p>
                <input id="calendario" style="margin:5px; width:300px" runat="server" type="date" name="fecha" disabled="disabled" class="form-control" aria-describedby="fecha" />
            </div>

            <div style="margin-top: -80px">
            <p>Ambiente:</p>
            <span class="input-group" ></span>
            <input id="ambiente" runat="server" disabled="disabled" style="margin-left:14px; width:250px" type="text" class="form-control" aria-describedby="Ambiente" />
            </div>
            
        </div>
    </div>

      <asp:Button ID="btnAceptarDiseno" runat="server"  type="submit" Text="Aceptar" CssClass="btn btn-success"  style="position:absolute; top:1200px; left:890px"/>
      <asp:Button ID="btnCancelarDiseno" runat="server"  type="submit" Text="Cancelar" CssClass="btn btn-danger"  style="position:absolute; top:1200px; left:970px"/>
	
        <asp:GridView ID="gridDiseno" runat="server"  Style="margin: 40px auto; margin-left: 150px; height: 400px; width: 800px; border: 1px solid black; -webkit-border-radius: 8px; border-radius: 8px; overflow: hidden;">
                    <RowStyle BackColor="White" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <FooterStyle BackColor="#3D3D3D" ForeColor="White" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <HeaderStyle HorizontalAlign="Center" BackColor="#3D3D3D" Font-Bold="True" ForeColor="Cyan" VerticalAlign="Middle" Font-Size="Medium" />
        </asp:GridView>

    </div>
        

    <div id="req" class="tab-pane fade">

            <div class="panel panel-primary" style="height:230px; width:500px; margin-left: 10px; margin-top: 40px">
            <div class="panel-heading" style="border-color: #3D3D3D; background-color: #3D3D3D; color: #0BF1F1">Infomacion de Requerimientos</div>
            <div class="panel-body">         

            <p>ID:</p>
            <span class="input-group"></span>
            <input id="idReq" runat="server" disabled="disabled" style="margin-bottom:10px;  max-width:200px;" type="text" class="form-control" aria-describedby="Proposito" />

            <p>Nombre:</p>
            <span class="input-group"></span>
            <input id="nomReq" runat="server" disabled="disabled" style="margin-bottom:10px;  max-width:200px;" type="text" class="form-control" aria-describedby="Proposito" />          
                 </div>
             </div>

      <asp:Button ID="btnAceptarReq" runat="server"  type="submit" Text="Aceptar" CssClass="btn btn-success"  style="position:absolute; top:550px; left:430px"/>
      <asp:Button ID="btnCancelarReq" runat="server"  type="button" Text="Cancelar" CssClass="btn btn-danger"  style="position:absolute; top:550px; left:520px"/>
    
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





</asp:Content>
