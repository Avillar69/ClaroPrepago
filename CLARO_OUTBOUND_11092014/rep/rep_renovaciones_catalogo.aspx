<%@ Page Title="Claro Outbound" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="rep_renovaciones_catalogo.aspx.vb" Inherits="rep_rep_porta" %>



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



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>

  

        <uc1:UCTitulo1 ID="UCTitulo11" runat="server" Titulo="Reporte Listado por Fecha" />

  

</div>
<div>
<table align="left">
<tr>
<td>MARCA</td><td>:</td><td>
    <asp:DropDownList ID="CBO_MARCA" runat="server" 
        Width="250px" AutoPostBack="True" >
    <asp:ListItem>SELECCIONAR</asp:ListItem>
    <asp:ListItem>ALCATEL</asp:ListItem>
    <asp:ListItem>APPLE</asp:ListItem>
    <asp:ListItem>AZUMI</asp:ListItem>
    <asp:ListItem>HUAWEI</asp:ListItem>
    <asp:ListItem>HTC</asp:ListItem>
    <asp:ListItem>LANIX</asp:ListItem>
    <asp:ListItem>LENOVO</asp:ListItem>
    <asp:ListItem>LG</asp:ListItem>
    <asp:ListItem>M4</asp:ListItem>
    <asp:ListItem>MOTOROLA</asp:ListItem>
    <asp:ListItem>NOKIA</asp:ListItem>
    <asp:ListItem>SAMSUNG</asp:ListItem>
    <asp:ListItem>SONY</asp:ListItem>
    <asp:ListItem>VERYKOOL</asp:ListItem>
    <asp:ListItem>ZTE</asp:ListItem>
    </asp:DropDownList>
    </td>
</tr>
<tr>
<td>EQUIPO</td><td>:</td><td><asp:DropDownList ID="CBO_EQUIPO" runat="server" 
        Width="250px" AutoPostBack="True"/></td>
</tr>
<tr>
<td>CAMARA</td><td>:</td><td><asp:TextBox ID="TXT_CAMARA" runat="server" 
        Width="250px"/></td>
</tr>
<tr>
<td>SO</td><td>:</td><td><asp:TextBox ID="TXT_SO" runat="server" Width="250px"/></td>
</tr>
<tr>
<td>TECNOLOGÍA</td><td>:</td><td><asp:TextBox ID="TXT_TECNOLOGIA" runat="server" 
        Width="250px"/></td>
</tr>
<tr>
<td>PLAN</td><td>:</td><td><asp:DropDownList ID="CBO_PLAN" runat="server" 
        Width="250px" AutoPostBack="True">
    <asp:ListItem>SELECCIONAR</asp:ListItem>
    <asp:ListItem>Grupo1 [30-40]</asp:ListItem>
    <asp:ListItem>Grupo2 [55-65]</asp:ListItem>
    <asp:ListItem>Grupo3 [69-85]</asp:ListItem>
    <asp:ListItem>Grupo4 [100-125]</asp:ListItem>
    <asp:ListItem>Grupo5 [130-155]</asp:ListItem>
    <asp:ListItem>Grupo6 [175-230]</asp:ListItem>
    <asp:ListItem>Grupo7 [255-320]</asp:ListItem>
    </asp:DropDownList>
    </td>
</tr>
<tr>
<td>PRECIO</td><td>:</td><td><asp:TextBox ID="TXT_PRECIO" runat="server" 
        Width="250px"/></td>
</tr>
</table>
</div>
<div><br /><br /><br /><br />
<br /><br />
</div>
</asp:Content>
