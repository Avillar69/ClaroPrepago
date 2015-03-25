<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="outSegClientes.aspx.vb" Inherits="rep_outSegClientes" %>
<%@ Register src="../DynamicData/FieldTemplates/UCTitulo1.ascx" tagname="UCTitulo1" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <div>

  

        <uc1:UCTitulo1 ID="UCTitulo11" runat="server" 
            Titulo="Carga Seg Clientes Top" />

  

</div>
<div>
<table align="left">
<tr>
<td>Archivo</td><td>:</td><td>
    <asp:FileUpload ID="FileUpload1" runat="server" />
    </td>
</tr>
<tr>
<td colspan="3" >
    <asp:Button ID="btnVisualizar" runat="server" Text="Visualizar" Width="150px" CssClass="btGen" /><br /><br />
    <asp:Button ID="btnGenerar" runat="server" Text="Generar" Width="150px" Visible="false" CssClass="btGen" />
    </td>
</tr>
</table>
</div>
<div><br /><br /><br /><br /><br /><br />
<asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
<br /><br />
</div>
<div>
   <asp:Panel ID="pn" runat="server" ScrollBars="Auto">
   
    <asp:GridView ID="grvCarga" runat="server" CssClass="gridview" 
    AutoGenerateColumns="true" AllowPaging="True" RowStyle-Wrap="false" PageSize="50">
    </asp:GridView>
   </asp:Panel>
</div>
<div>
   <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
           <asp:LinkButton ID="lnkExportar" runat="server" Visible="false">Exportar</asp:LinkButton>
    <asp:GridView ID="grvMostrar" runat="server" CssClass="gridview"  RowStyle-Wrap="false">
    </asp:GridView>
   </asp:Panel>
</div>
</asp:Content>

