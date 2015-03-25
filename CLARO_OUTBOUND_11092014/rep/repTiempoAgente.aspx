<%@ Page Title="Claro Seguimiento Clientes Top- Reporte" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="repTiempoAgente.aspx.vb" Inherits="frmMaestro_repTiempoAgente" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../UCform/ucTitulo.ascx" tagname="ucTitulo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../jquery_smoothness/js/jquery-1.9.1.js" type="text/javascript"></script>
<link href="../jquery_smoothness/css/smoothness/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script src="../jquery_smoothness/js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<script src="../Scripts/jscript.js" type="text/javascript"></script>

<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    
<script src="../Scripts/jscript.js" type="text/javascript"></script>

<script type="text/javascript">
    $(function () {
        $("#txtInicio").datepicker({
            dateFormat: 'yy-mm-dd',
            mINDate: 1,
            firstDay: 1,
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            dayNamesMin: ['dom', 'lun', 'mar', 'mie', 'jue', 'vie', 'sab']
        });
    });

    $(function () {
        $("#txtFin").datepicker({
            dateFormat: 'yy-mm-dd',
            mINDate: 1,
            firstDay: 1,
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            dayNamesMin: ['dom', 'lun', 'mar', 'mie', 'jue', 'vie', 'sab']
        });
    });


    function AbrirPopupAudio(direccionUrl1) {
        OpenPopup(direccionUrl1, 400, 120);
    }
 </script>

    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        .style2
        {
            font-family: Calibri;
        }
        .style3
        {
            font-size: medium;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="style1">
        <strong class="style2"><span class="style3">REPORTE DE TIEMPO DE GESTION 
        SEGUIMIENTO CLIENTES TOP</span></strong><br />
    </div>
   <div><input type="hidden" id="hdnVelocidad" name="hdnMax" value="1" runat="server" /></div>
   <div>
   <table>
   <tr><td>
   Fecha Inicio
   </td><td>:</td><td>
       <asp:TextBox ID="txtInicio" runat="server" Width="80px" ClientIDMode="Static"></asp:TextBox>
       <cc1:filteredtextboxextender ID="txtInicio_FilteredTextBoxExtender" 
           runat="server" Enabled="True" TargetControlID="txtInicio" 
           ValidChars="0123456789-">
       </cc1:filteredtextboxextender>
   </td>
   </tr>
   <tr>
   <td>Fecha Fin</td><td>:</td><td>
       <asp:TextBox ID="txtFin" runat="server" Width="80px"  ClientIDMode="Static" ></asp:TextBox>
       <cc1:filteredtextboxextender ID="idGrabacion_FilteredTextBoxExtender" 
           runat="server" Enabled="True" TargetControlID="txtFin" 
           ValidChars="0123456789-">
       </cc1:filteredtextboxextender>
&nbsp;<asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="100px" />
       </td>
   </tr>
   </table>
   </div>
   <div>
    <br />
    &nbsp;<asp:Button ID="btnExportar" runat="server" Text="Exportar" />
         &nbsp;&nbsp;
    <asp:Label ID="lblError" runat="server" ForeColor="Red" ></asp:Label><br />
       <asp:Panel runat="server" ID="pnExportar" runat="server" Visible="false">
         </asp:Panel>
</div>


   <div>
  <asp:Panel ID="pn1" runat="server" ScrollBars="Auto">
       <asp:GridView ID="grv" runat="server" 
           HeaderStyle-CssClass="GridView_HeaderStyle" 
           RowStyle-CssClass="GridView_RowStyle" 
           AlternatingRowStyle-CssClass="GridView_AlternatingRowStyle" 
           RowStyle-Wrap="false" AllowPaging="True"  PageSize="40" >
           <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
         
           <HeaderStyle CssClass="GridView_HeaderStyle" />
           <RowStyle CssClass="GridView_RowStyle" Wrap="False" />
       </asp:GridView>
       </asp:Panel>
   </div>


  










</asp:Content>

