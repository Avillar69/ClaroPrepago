Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Data
Public Class DA_Sistemas
    Dim cn As New Conexion
    Dim cnx As New SqlConnection(cn.CNX_DNINO)
    Dim cnxErp As New SqlConnection(cn.CNX_ERP)

    Public Function LISTA_DE_BASE() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("SELECT ID_BASE,CAST(ID_BASE AS VARCHAR)+' - '+NOMBRE_BASE'NOMBRE_BASE' FROM scripting.BASE WHERE ID_CAMPANYA=140 ORDER BY 1", cnx)
            cmd.CommandTimeout = 600000
            cnx.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("LISTA_DE_BASE " & ex.Message)
        Finally
            cnx.Close()
        End Try
        Return dt
    End Function
    Public Function FALABELLA_CONSULTA_BASE(ByVal base As Integer) As DataTable
        Dim dt As New DataTable
        Try
            Dim da As New SqlDataAdapter("REPORTES.FALABELLA_CONSULTA_BASE " & base & "", cnx)
            da.SelectCommand.CommandTimeout = 600000

            da.Fill(dt)
        Catch ex As Exception
            MsgBox("LISTA_DE_BASE " & ex.Message)
        Finally
            cnx.Close()
        End Try
        Return dt
    End Function
    Public Function SP_LOGUEO_REPORTES(ByVal documento As String, ByVal contrasenia As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim da As New SqlDataAdapter("BD_ERP.RRHH.SP_LOGUEO_REPORTES", cnxErp)
            da.SelectCommand.CommandType = CommandType.StoredProcedure
            da.SelectCommand.Parameters.Add("@NRO_DOCUMENTO", SqlDbType.VarChar, 15).Value = documento
            da.SelectCommand.Parameters.Add("@CONTRASENIA", SqlDbType.VarChar, 15).Value = contrasenia
            da.SelectCommand.Parameters.Add("@CAMPANYA", SqlDbType.VarChar, 20).Value = "TODOS"
            da.SelectCommand.CommandTimeout = 600000
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LOGUEO_REPORTES " & ex.Message)
        Finally
            cnx.Close()
        End Try
        Return dt
    End Function
End Class
