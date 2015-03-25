<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>










<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link href="css/estilo.css"  rel="stylesheet" type="text/css" />
<script src="jquery_smoothness/js/jquery-1.9.1.js" type="text/javascript"></script>
<script src="jquery_smoothness/js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
<link href="jquery_smoothness/css/smoothness/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jscript.js" type="text/javascript"></script>

    <link href="css/login.css"  rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="pic/peru.ico" type="image/png" />

<script language="javascript">
    function AbrirVersion() { OpenPopup("frmVersion.aspx", 200, 200); }
</script>


    <script type="text/javascript" language="javascript">
        function cerrar() {
            window.close();
        }
    </script>

    <title>:::DynamiCALL 2015:::</title>
</head>
<body>
    <form id="form1" runat="server">
  
    <div class="login_form">
                <center><br />
                    <table width="300px" cellspacing="5">
                        <tr>
                            <td colspan="2" align="center"><img src="pic/logo.jpg" alt="" style="width:200px"/></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="titulo black center">REPORTES CLARO OUTBOUND</td>
                        </tr>
                        <tr>
                            <td>
                                <label class="bold">Usuario:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtusuario" runat="server" CssClass="aspcontrol bold center textbox padding_user" 
                                                        TabIndex="0" Font-Size="Medium" Width="200px"> </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="bold">Contraseña:</label></td>
                            <td>
                                <asp:TextBox ID="txtContrasenia" runat="server" CssClass="aspcontrol bold center textbox padding_user" 
                                                        TabIndex="0" Font-Size="Medium" Width="200px" TextMode="Password" > </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="center"><br />
                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
                                CssClass="aspcontrol aspbutton bold btAgregar" />
                                <asp:Button ID="btnCerrar" runat="server" Text="Salir" OnClientClick="cerrar();" 
                                CssClass="aspcontrol aspbutton bold btEliminar" />                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="center">
                               
                                
                                


                             
                               
                                
                                


                            </td>
                        </tr>
                    </table>
                </center>
            </div>
       

 <div>

    

 </div>
 <div>
    
     <asp:Label ID="lblmsg" runat="server" ForeColor="Red" ></asp:Label>
 </div>




    </form>
</body>
</html>