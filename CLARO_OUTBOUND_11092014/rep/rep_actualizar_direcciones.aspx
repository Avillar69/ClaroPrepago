<%@ Page Title="Claro Outbound" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="rep_actualizar_direcciones.aspx.vb" Inherits="rep_rep_porta" %>



<%@ Register src="../DynamicData/FieldTemplates/UCTitulo1.ascx" tagname="UCTitulo1" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../jquery_smoothness/js/jquery-1.9.1.js" type="text/javascript"></script>
<link href="../jquery_smoothness/css/smoothness/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
<script src="../jquery_smoothness/js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<script src="../Scripts/jscript.js" type="text/javascript"></script>
<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/jscript.js" type="text/javascript"></script>
<script language="javascript">
    function AbrirVenta(ini, fin, cam, tip) { OpenPopup("../frmVenta/frmVentaHCpopup.aspx?ini=" + ini + "&fin=" + fin + "&cam=" + cam + "&tip=" + tip, 630, 700); }
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
<td>inicio</td><td>:</td><td><asp:TextBox ID="txtInicio" runat="server" Width="80px" ClientIDMode="Static" /></td>
</tr>
<tr>
<td>Fin</td><td>:</td><td><asp:TextBox ID="txtFin" runat="server" Width="80px"  ClientIDMode="Static" />
    <asp:Button ID="btnBuscar" runat="server" Text="Consultar" />
    </td>
</tr>
</table>
</div>
<div><br /><br /><br /><br /><br />
<asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
<br /><br />
</div>
<div>
   <asp:Panel ID="pn" runat="server" ScrollBars="Auto" Height="500px">
       <asp:LinkButton ID="lnkExportar" runat="server" Visible="false">Exportar Excel</asp:LinkButton>
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton 
           ID="lnkExportarCsv" runat="server" Visible="false">Exportar Csv</asp:LinkButton>
&nbsp;<asp:GridView ID="grvReporte" runat="server" CssClass="gridview"  
           AutoGenerateColumns="true" AllowPaging="True"
           RowStyle-Wrap="false" PageSize="50">
    </asp:GridView>
       <asp:GridView ID="grvReporte2" runat="server" CssClass="gridview" 
           RowStyle-Wrap="false">
       </asp:GridView>
       <br />
   </asp:Panel>
</div>
</asp:Content>
