<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucTitulo.ascx.vb" Inherits="UCform_ucTitulo" %>
<style type="text/css">
    .Uctitulo
        {
        font-size: 13pt;
        font-family:Calibri;
        border-radius: 2pt;
        }
    
     .Ucleft
        { 
        text-align:left;
        }
        
        
      .bold
        { 
        font-weight:bold;
        }       
    </style>

<table cellSpacing="0" cellPadding="0" width="100%" border="0" align="left">
<tr>
<td width="100px">
<asp:Image ID="imgLogo" runat="server" Width="60px"  />
</td>
<td align="left"><asp:label id="lblTitulo"  runat="server" CssClass="Uctitulo  Ucleft bold" />
</td>
</tr>
<tr><td background="../pic/linea-punteada.gif" colSpan="2" height="1"></td></tr>
</table><br />