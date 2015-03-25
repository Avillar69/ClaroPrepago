
<%@ Application Language="VB" %>
<%@ Import Namespace="System.Data" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta al iniciarse la aplicación
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta al cerrarse la aplicación
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta al producirse un error no controlado
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Dim dtCarga As New DataTable
        Session("tablaCarga") = dtCarga

        Dim dtExportar As New DataTable
        Session("tablaExportar") = dtExportar
        
       
       
        Session("ids") = ""
        Session("usuario") = ""
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta cuando finaliza una sesión. 
        ' Nota: el evento Session_End se desencadena sólo cuando el modo sessionstate
        ' se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
        ' o SQLServer, el evento no se genera.
    End Sub
       
</script>