<%@ Page Title="Claro Outbound" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="rep_claro_3play_hfc.aspx.vb" Inherits="rep_rep_porta" %>
<%@ Register src="../DynamicData/FieldTemplates/UCTitulo1.ascx" tagname="UCTitulo1" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script src="../jquery_smoothness/js/jquery-1.9.1.js" type="text/javascript"></script>
<link href="../jquery_smoothness/css/smoothness/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script src="../jquery_smoothness/js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<script src="../Scripts/jscript.js" type="text/javascript"></script>
<link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>

 <script type="text/javascript">
    $(document).ready(function () {
        $("#<%=btnBuscar.ClientID %>").click(function () {
            var btn = $(this)
            btn.button('loading')
        });

        $("#<%=btnProcesar.ClientID %>").click(function () {
            var btn = $(this)
            btn.button('loading')
        });
    });
     </script>


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
 </script>
 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <uc1:UCTitulo1 ID="UCTitulo11" runat="server" Titulo="Reporte Listado por Fecha" />
    </div>

<div>
<table align="left">
<tr>
<td> FECHA INICIO</td><td>:</td>
<td><asp:TextBox ID="txtInicio" runat="server" CssClass="form-control input-group input-group-sm" Width="120px" ClientIDMode="Static" /></td>
<td>&nbsp; <asp:Button CssClass="btn btn-primary btn-sm" ID="btnBuscar" data-loading-text="Buscando..." runat="server" Text="Consultar" /></td>
</tr>
<tr>
<td>FECH FIN</td><td>:</td>
<td><asp:TextBox ID="txtFin" runat="server"  CssClass="form-control input-group input-group-sm" Width="120px" ClientIDMode="Static" /></td>
</tr>
</table>
</div>



<div>
<br /><br />
<br /><br />

<asp:Label ID="lblMsg" runat="server" />
<br /><br />

        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnProcesar" data-loading-text="Procesando..." Visible="false" runat="server" Text="Obtener Reg. Unicos" />
        <asp:LinkButton ID="lnkExportar1" runat="server" class="btn btn-default btn-sm" Visible="false" OnClick="ExportToExcel1" >
        <span class="glyphicon glyphicon-download-alt"> </span> &nbsp;Exportar Reg. Unicos
       </asp:LinkButton>

       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       
       <asp:LinkButton ID="lnkExportar2" runat="server" class="btn btn-default btn-sm" Visible="false" OnClick="ExportToExcel2" >
       <span class="glyphicon glyphicon-download-alt"></span> &nbsp; Exportar Reg. Detallado
       </asp:LinkButton>
       <br />

   <asp:Panel ID="pn" runat="server" ScrollBars="Auto" Height="300px">

        <asp:GridView ID="grvReporte" runat="server" CssClass="rounded_corners" 
               AutoGenerateColumns="true" AllowPaging="True"  RowStyle-Wrap="false" 
               PageSize="50">
        </asp:GridView>
        <asp:GridView ID="grvExport" runat="server" CssClass="rounded_corners">
       </asp:GridView>

   </asp:Panel>
</div>


</asp:Content>
