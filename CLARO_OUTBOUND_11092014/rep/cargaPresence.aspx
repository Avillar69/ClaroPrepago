<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="cargaPresence.aspx.vb" Inherits="frm_cargaPresence" %>

<%@ Register src="~/DynamicData/FieldTemplates/UCTitulo1.ascx" tagname="ucTitulo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">

   

     function isNumberKey(evt) {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 48 || charCode > 57))
             return true;

         return false;

     }
     function AcceptNumber(evt) {
         var nav4 = window.Event ? true : false;
         var key = nav4 ? evt.which : evt.keyCode;
         return (key <= 13 || (key >= 48 && key <= 57) || key == 46);
     }
     $(function () {
         $("#txt_Fecha_Emision_Cot").datepicker({
             dateFormat: 'yy-mm-dd',

             firstDay: 1,
             monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
             dayNamesMin: ['dom', 'lun', 'mar', 'mie', 'jue', 'vie', 'sab']
         });
     });
    


 </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <table>
            <tr>
            <td>Esquemas</td><td>:</td><td><asp:TextBox ID="txtEsquema" runat="server" Width="300px" /> </td>
            </tr>
            <tr>
            <td>Tabla</td><td>:</td><td><asp:TextBox ID="txtTabla" runat="server" Width="300px" /> </td>
            </tr>
            <tr>
            <td colspan="3" align="right"><asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="110px" /> </td>
            </tr>
            <tr>
            <td>Archivo</td><td>:</td><td>
            <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
            </td>
            </tr>
             <tr>
            <td colspan="3" align="right"><asp:Button ID="btnMostrarExcel" runat="server" Text="Mostrar Excel" Width="110px" /> </td>
            </tr>
       </table>
            
    </div>
    <div>
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
    </div>
    <div>
    <br /><br />

        <asp:DataList ID="DataList1" runat="server"   >
            <ItemTemplate>
               <table>
            <tr>
                <td><asp:Label ID="lblCampo" runat="server" Text='<%# Eval("COLUMNA") %>' Width="200px" /></td>
                 <td width="50px"></td>
                 <td><asp:Label ID="lblCruce" runat="server" /></td>
                <td><asp:DropDownList ID="cboCruce" runat="server" Width="350" DataSourceID="SqlDataSource1" 
                    DataTextField="CAMPO" DataValueField="CAMPO"  /></td>
            </tr>
            
        </table>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div><asp:Button ID="btnGenerar" runat="server" Text="Generar Base" 
            Width="110px" /> </div>
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server">
        </asp:SqlDataSource>
    </div>
</asp:Content>

