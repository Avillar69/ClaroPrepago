﻿<%@ Page Title="Claro Outbound" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="cargas_gest_prev.aspx.vb" Inherits="rep_rep_porta" %>



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
     });
     </script>
    <style type="text/css">
        .style1
        {
            height: 32px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>


        <uc1:UCTitulo1 ID="UCTitulo11" runat="server" Titulo="Obtener Cargas Presence" />

</div>
<div>

<table>
<tr><td><p>Servicio</p></td><td>:</td><td><asp:DropDownList ID="cboServ" runat="server" CssClass="btn btn-default btn-sm dropdown-toggle" ></asp:DropDownList> &nbsp;</td>
<td><asp:Button ID="btnBuscar" data-loading-text="Buscando..." CssClass="btn btn-primary btn-sm btn-block" runat="server" Text="Consultar" OnClick="btnBuscar_Click"/></td></tr>
<tr><td class="style1"><p>Carga</p></td><td class="style1">:</td>
    <td class="style1"><asp:TextBox ID="txtCarga" runat="server"  
            CssClass="form-control input-sm"  ClientIDMode="Static" /></td></tr>
<tr><td>&nbsp;</td><td>:</td><td>&nbsp;</td></tr>
</table>

</div>
<div>
<br />
<asp:Label ID="lblMsg" runat="server" Width="100%" />

    <br />
       <asp:LinkButton ID="lnkExportar" runat="server" class="btn btn-default btn-sm" Visible="false" OnClick="ExportToExcel" > <span class="glyphicon glyphicon-download-alt"></span>&nbsp;Exportar Detalle </asp:LinkButton>
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <br />
<asp:Panel ID="pn" runat="server" ScrollBars="Auto" Height="150px">
       <asp:GridView ID="grvReporte" runat="server" RowStyle-Wrap="false" CssClass="rounded_corners">
       </asp:GridView>
       <asp:GridView ID="grvExport" runat="server" HeaderStyle-BackColor="#4B6C9E" HeaderStyle-ForeColor="White"
        RowStyle-BackColor="#E2E9F1" AlternatingRowStyle-BackColor="#F0F1F3" AlternatingRowStyle-ForeColor="#000">
       </asp:GridView>
   </asp:Panel>
   
</div>

</asp:Content>



