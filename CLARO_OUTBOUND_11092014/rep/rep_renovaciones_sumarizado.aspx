<%@ Page Title="Claro Outbound" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="rep_renovaciones_sumarizado.aspx.vb" Inherits="rep_rep_porta" %>



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
    &nbsp;
    <asp:Button ID="btnBuscar" runat="server" Text="Consultar" />
    &nbsp;
    </td>
</tr>
</table>
</div>
<div><br /><br /><br /><br />
<asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
<br /><br />
</div>
<div>
   <asp:Panel ID="pn" runat="server" ScrollBars="Auto" Height="300px">
       <asp:LinkButton ID="lnkExportar" runat="server" Visible="false">Exportar Txt</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:LinkButton ID="lnkExpoExcel" runat="server" Visible="false">Exportar Excel</asp:LinkButton>
    <asp:GridView ID="grvReporte" runat="server" CssClass="gridview"  
           RowStyle-Wrap="false" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="COD_GESTION" HeaderText="COD_GESTION" />
            <asp:BoundField DataField="NRO_DOCUMENTO" HeaderText="NRO_DOCUMENTO" />
            <asp:BoundField DataField="NOMBRE_CAMPANIA" HeaderText="NOMBRE_CAMPANIA" />
            <asp:BoundField DataField="NOMBRE CLIENTE" HeaderText="NOMBRE CLIENTE" />
            <asp:BoundField DataField="GENERO" HeaderText="GENERO" />
            <asp:BoundField DataField="ANIONACIMIENTO" HeaderText="ANIONACIMIENTO" />
            <asp:BoundField DataField="COD_AREA" HeaderText="COD_AREA" />
            <asp:BoundField DataField="FONO_PARTICULAR" HeaderText="FONO_PARTICULAR" />
            <asp:BoundField DataField="FONO_COMERCIAL" HeaderText="FONO_COMERCIAL" />
            <asp:BoundField DataField="FONO_CELULAR" HeaderText="FONO_CELULAR" />
            <asp:BoundField DataField="DIRECCION" HeaderText="DIRECCION" />
            <asp:BoundField DataField="CIUDAD" HeaderText="CIUDAD" />
            <asp:BoundField DataField="LOCALIDAD" HeaderText="LOCALIDAD" />
            <asp:BoundField DataField="INICIO_LLAMADA" HeaderText="INICIO_LLAMADA" />
            <asp:BoundField DataField="FIN_LLAMADA" HeaderText="FIN_LLAMADA" />
            <asp:BoundField DataField="TELEFONO" HeaderText="TELEFONO" />
            <asp:BoundField DataField="NRO_LLAMADAS" HeaderText="NRO_LLAMADAS" />
            <asp:BoundField DataField="DURACION" HeaderText="DURACION" />
            <asp:BoundField DataField="LOGIN" HeaderText="LOGIN" />
            <asp:BoundField DataField="MOTIVO" HeaderText="MOTIVO" />
            <asp:BoundField DataField="SUBMOTIVO" HeaderText="SUBMOTIVO" />
            <asp:BoundField DataField="ID" HeaderText="SOURCEID" />
            <asp:BoundField DataField="SERVICEID" HeaderText="SERVICEID" />
            <asp:BoundField DataField="FINAL" HeaderText="FINAL" />
            <asp:BoundField DataField="OBS_AGENTE" HeaderText="OBS_AGENTE" />
            <asp:BoundField DataField="CODIGO_CARGA" HeaderText="CODIGO_CARGA" />
            <asp:BoundField DataField="COMODIN" HeaderText="COMODIN" />
            <asp:BoundField DataField="ADICIONALES" HeaderText="ADICIONALES" />
            <asp:BoundField HeaderText="ID" />
            <asp:BoundField DataField="CUENTA_CON_MAIL" HeaderText="CUENTA_CON_MAIL" />
            <asp:BoundField DataField="MESES_PERMANENCIA" HeaderText="MESES_PERMANENCIA" />
        </Columns>
        <RowStyle Wrap="False" />
    </asp:GridView>
       <br />
       <asp:GridView ID="grvReporte2" runat="server" CssClass="gridview"
           RowStyle-Wrap="false">
       </asp:GridView>
   </asp:Panel>
</div>
</asp:Content>
