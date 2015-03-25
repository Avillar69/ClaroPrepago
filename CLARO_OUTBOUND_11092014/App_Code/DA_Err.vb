Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data

Public Class DA_Err
    Dim cn As New Conexion
    Dim cnxMySql As New MySqlConnection(cn.CNX_MYSQL)
    Dim beerr As New BE_Err

    Public Function SP_MANTENIMIENTO_RES_ERR(ByVal be As BE_Err) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_MANTENIMIENTO_RES_ERR", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ORIGEN", MySqlDbType.VarChar, 50).Value = be.ORIGEN
                .Add("VAR_IP", MySqlDbType.VarChar, 20).Value = be.IP
                .Add("VAR_VS", MySqlDbType.VarChar, 20).Value = be.VS
                .Add("VAR_PLATAFORMA", MySqlDbType.VarChar, 20).Value = be.PLATAFORMA
                .Add("VAR_SERVICIO", MySqlDbType.VarChar, 100).Value = be.SERVICIO
                .Add("VAR_VALOR", MySqlDbType.VarChar, 1000).Value = be.VALOR
                .Add("VAR_NRO_USUARIO", MySqlDbType.VarChar, 300).Value = be.NRO_USUARIO
                .Add("VAR_USUARIO", MySqlDbType.VarChar, 300).Value = be.USUARIO
                .Add("VAR_HOST", MySqlDbType.VarChar, 100).Value = be.HOST
                .Add("VAR_SITIO", MySqlDbType.VarChar, 300).Value = be.SITIO
                .Add("VAR_NRO_LINEA", MySqlDbType.VarChar, 100).Value = be.NRO_LINEA
                .Add("VAR_MSG_1", MySqlDbType.VarChar, 1000).Value = be.MSG_1
                .Add("VAR_MSG_2", MySqlDbType.VarChar, 1000).Value = be.MSG_2
                .Add("VAR_MSG_3", MySqlDbType.VarChar, 1000).Value = be.MSG_3
                .Add("VAR_TIPO_ERR", MySqlDbType.VarChar, 350).Value = be.TIPO_ERR
                .Add("VAR_OBS", MySqlDbType.VarChar, 1500).Value = be.OBS
                .Add("VAR_TIPO", MySqlDbType.Int32).Value = be.TIPO
            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Shared Function GetIPAddress() As String
        Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
        Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If String.IsNullOrEmpty(sIPAddress) Then
            Return context.Request.ServerVariables("REMOTE_ADDR")
        Else
            Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
            Return ipArray(0)
        End If
    End Function

End Class
