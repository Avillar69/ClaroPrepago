<%@ Page Title="Claro Outbound" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="rep_migraciones_consulta.aspx.vb" Inherits="rep_re_general_migraciones" %>



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
        $("#DTP_FEC_NAC").datepicker({
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
<td>Consulta por</td><td>:</td><td>
    <asp:DropDownList ID="cbo_ConsultaPor" runat="server">
        <asp:ListItem>SELECCIONAR</asp:ListItem>
        <asp:ListItem>ID FUENTE</asp:ListItem>
        <asp:ListItem>TELEFONO</asp:ListItem>
    </asp:DropDownList>
&nbsp;&nbsp;
    <asp:TextBox ID="txtParametro" runat="server" 
        Width="120px"  ClientIDMode="Static" />
    &nbsp;&nbsp;
    <asp:Button ID="btnBuscar" runat="server" Text="Consultar" />
    &nbsp;&nbsp;&nbsp;
    </td>
</tr>
</table>
</div>
<div><br /><br />
<asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
<br /><br />
</div>
<div>
   <asp:Panel ID="pn" runat="server" ScrollBars="Auto" Height="800px">
    <asp:GridView ID="grvResultado" runat="server" CssClass="gridview" 
           RowStyle-Wrap="false" AutoGenerateColumns="False" Width="522px">
        <Columns>
            <asp:CommandField ButtonType="Image" SelectImageUrl="~/pic/edit.gif"
                 ShowSelectButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="TELEFONO" HeaderText="TELEFONO" />
            <asp:BoundField DataField="FECHA_LLAMADA" HeaderText="FECHA" />
            <asp:BoundField DataField="LOGIN_VENTA" HeaderText="LOGIN" />
            <asp:BoundField DataField="DESC_CALLCENTER" HeaderText="CALL" />
        </Columns>
        <RowStyle Wrap="False" />
    </asp:GridView>
          <br />
    <table>
     <tr><td>Id Fuente</td><td><asp:TextBox runat="server" ID="txtId" 
        BackColor="#E9E6E6" ReadOnly="True" /></td></tr>
    <tr><td>Plan Postpago contratado</td><td width="180px"><asp:TextBox runat="server" 
             ID="TXT_PLAN_POSTPAGO_CONTRATADO" Height="21px" Width="300px"/></td></tr>
    <tr><td>Cargo Fijo Mensual</td><td width="180px"><asp:TextBox runat="server" 
             ID="TXT_CARGO_FIJO_MENSUAL" Height="21px" Width="300px" /></td></tr>
    <tr><td>Ciclo facturación</td><td width="180px"><asp:TextBox runat="server" 
             ID="TXT_CICLO_FACTURACION" Height="21px" Width="300px" /></td></tr>
    <tr><td>Nombres</td><td width="180px"><asp:TextBox runat="server" 
             ID="TXT_NOMBRES" Height="21px" Width="300px" /></td></tr>
    <tr><td>Apellidos</td><td width="180px"><asp:TextBox runat="server" 
             ID="TXT_APELLIDOS" Height="21px" Width="300px" /></td></tr>
    <tr><td>DNI</td><td width="180px"><asp:TextBox runat="server" 
             ID="TXT_NRO_DNI" Height="21px" Width="300px" /></td></tr>
    <tr><td>Numero a Migrar</td><td width="180px"><asp:TextBox runat="server" 
             ID="TXT_NRO_MIGRAR" Height="21px" Width="300px" /></td></tr>
    <tr><td>Fecha de Nacimiento</td><td width="180px"><asp:TextBox runat="server"
             ID="DTP_FEC_NAC" Width="80px" ClientIDMode="Static" /></td></tr>
    <tr><td>Lugar de Nacimiento</td><td width="180px"><asp:TextBox runat="server" 
             ID="TXT_LUGAR_NAC" Height="21px" Width="300px" /></td></tr>
    <tr><td>Telefono de Referencia</td><td width="180px"><asp:TextBox runat="server" 
             ID="TXT_TELEFONO_REF" Height="21px" Width="300px" /></td></tr>
    <tr><td>E-mail</td><td width="180px"><asp:TextBox runat="server" 
             ID="TXT_EMAIL" Height="21px" Width="300px" /></td></tr>
    <tr><td>Departamento</td><td width="180px">
        <asp:DropDownList runat="server" 
             ID="cboDepartamento" Height="21px" Width="300px" AutoPostBack="True" /></td></tr>
    <tr><td>Provincia</td><td width="180px">
        <asp:DropDownList runat="server" 
             ID="cboProvincia" Height="21px" Width="300px" AutoPostBack="True" /></td></tr>
    <tr><td>Distrito</td><td width="180px"><asp:DropDownList runat="server" 
             ID="cboDistrito" Height="21px" Width="300px" /></td></tr>
    <tr><td>Dirección</td><td width="180px">
        <asp:TextBox runat="server" 
             ID="TXT_DIRECCION" Height="37px" Width="316px" TextMode="MultiLine" /></td></tr>
    <tr><td>FINAL 1</td><td width="180px">
        <asp:DropDownList runat="server" 
             ID="CBO_NIVEL_1" Height="21px" Width="300px" AutoPostBack="True" /></td></tr>
    <tr><td>FINAL 2</td><td width="180px">
        <asp:DropDownList runat="server" 
             ID="CBO_NIVEL_2" Height="21px" Width="300px" AutoPostBack="True" /></td></tr>
    <tr><td>FINAL 3</td><td width="180px">
        <asp:DropDownList runat="server" 
             ID="CBO_NIVEL_3" Height="21px" Width="300px" AutoPostBack="True" /></td></tr>
    <tr><td>FINAL 4</td><td width="180px"><asp:DropDownList runat="server" 
             ID="CBO_NIVEL_4" Height="21px" Width="300px" /></td></tr>
    <tr><td>FINAL CALIDAD</td><td width="180px"><asp:DropDownList runat="server" 
             ID="cboFinalCalidad" Height="21px" Width="300px" /></td></tr>
    <tr><td>FINAL BACKOFFICE</td><td width="180px"><asp:DropDownList runat="server" 
             ID="cboFinalBackoffice" Height="21px" Width="300px" /></td></tr>
    <tr><td>OBSERVACIONES</td><td width="180px">
        <asp:TextBox runat="server" 
             ID="OBS_BACKOFFICE" Height="40px" Width="319px" TextMode="MultiLine"/></td></tr>
    <tr><td valign="top">Ultimo Resultado</td><td valign="top" colspan="4">
        <asp:GridView ID="grvUltimoResultado" runat="server" AutoGenerateColumns="true"><RowStyle HorizontalAlign="Center" Wrap="false" />
       </asp:GridView></td></tr>
    <tr><td valign="top" align="center" colspan="5">
    <asp:Button ID="btnGuardar" runat="server" CssClass="ButtonStyle" 
        Text="Guardar" /></td></tr>
    </table>

    <div>
      <asp:Panel ID="pnHistorial" runat="server" ScrollBars="Auto">
        <asp:GridView ID="grvHistorial" runat="server" AutoGenerateColumns="true" CssClass="gridview" >
        <HeaderStyle HorizontalAlign="Center" Wrap="false"/>
        <RowStyle HorizontalAlign="Center" Wrap="false" />
        </asp:GridView>
        </asp:Panel>
    </div>
      </asp:Panel>

</div>

</asp:Content>
