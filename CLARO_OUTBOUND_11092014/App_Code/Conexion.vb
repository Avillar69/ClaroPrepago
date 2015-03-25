Public Class Conexion
    Public Function CNX_MYSQL() As String
        Dim cn As String = "datasource=192.168.150.35;username=reportes;password=r3p0rt3sd1n;database=BD_SCRIPTING_CLARO"
        Return cn
    End Function

    Public Function CNX_MYSQLDN() As String
        Dim cn As String = "datasource=192.168.150.35;username=reportes;password=r3p0rt3sd1n;database=BD_DNINOWEB"
        Return cn
    End Function
  
    Public Function CNX_DNINO() As String
        Dim cn As String = "Data Source=192.168.151.253\SQLDYN;Initial Catalog=BD_REPORTE_GENERAL;uid=sa; pwd=aph0l0Xd1n"
        Return cn
    End Function
    Public Function CNX_DNINO2() As String
        Dim cn As String = "Data Source=192.168.150.7\SQLDYN;Initial Catalog=BD_CLARO_OUTBOUND;uid=sa; pwd=aph0l0Xd1n"
        Return cn
    End Function
    Public Function CNX_ERP() As String
        'Dim cn As String = "Data Source=192.168.151.253\SQLDYN;Initial Catalog=BD_ERP;uid=sa; pwd=aph0l0Xd1n"
        Dim cn As String = "Data Source=192.168.150.7\SQLDYN;Initial Catalog=BD_ERP;uid=sa; pwd=aph0l0Xd1n"
        Return cn
    End Function
    Public Function CNX_MYSQL_CARGAREG() As String
        Dim cn As String = "datasource=192.168.150.35;username=root;password=rootd1n;database=BD_CARGAR_REGISTROS"
        Return cn
    End Function

End Class
