Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class rep_cargar
    Inherits System.Web.UI.Page
    Dim be As New BE_CLARO
    Dim da As New DA_claro
    Dim dtCarga As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub btnVisualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizar.Click
        Session("tablaExportar") = Nothing
        Dim ficha As String = Now.ToString("yyyyMMddHHmmss")
        Dim ruta As String = "C:\ClaroCargas\" & ficha & ".xls"
        Try
            If FileUpload1.PostedFile IsNot Nothing Then
                FileUpload1.PostedFile.SaveAs(ruta)
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
        CargarExcel(ruta)
    End Sub
    Sub CargarExcel(ByVal SLibro As String)

        lblMsg.Text = ""
        dtCarga = Nothing
        Dim cs As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                           "Data Source=" & SLibro & ";" & _
                           "Extended Properties=""Excel 8.0;HDR=YES"""
        Try


            Dim cn As New OleDbConnection(cs)
            If Not System.IO.File.Exists(SLibro) Then
                MsgBox("No se encontró el Libro: " & _
                        SLibro, MsgBoxStyle.Critical, _
                        "Ruta inválida")
                Exit Sub
            End If

            Dim dAdapter As New OleDbDataAdapter("Select * From [HOJA1$]", cs)
            Dim dt As New DataTable
            dAdapter.Fill(dt)
            dtCarga = dt
            Session("tablaCarga") = dtCarga


            lblMsg.Text = "Cantidad de Registros : " & dt.Rows.Count

            ' mostrar los 50 primeros
            Dim dAdapter1 As New OleDbDataAdapter("Select top 50 * From [HOJA1$]", cs)
            Dim dt1 As New DataTable
            dAdapter1.Fill(dt1)

            grvCarga.DataSource = dt1
            grvCarga.DataBind()

            If dt1.Rows.Count > 0 Then btnGenerar.Visible = True Else btnGenerar.Visible = False

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Sub GUARDAR()

        Dim c_bien As Integer = 0
        Dim c_no_bien As Integer = 0
        Dim c_error As Integer = 0

        Dim dt As DataTable = Session("tablaCarga")
        For i = 0 To dt.Rows.Count - 1
            be.VAR_FECHA_CARGA = dt.Rows(i)("F_CARGA").ToString
            be.VAR_CICLO = dt.Rows(i)("CICLO").ToString
            be.VAR_NOMBRE_BASE = dt.Rows(i)("BASE").ToString
            be.VAR_CUENTA = dt.Rows(i)("CUENTA").ToString
            be.VAR_EMAIL = dt.Rows(i)("EMAIL").ToString
            be.VAR_TELCELULAR = dt.Rows(i)("TELCELULAR").ToString
            be.VAR_D_ESTADO_SP = dt.Rows(i)("ESTADO").ToString
            be.VAR_ID_LLAMADA = dt.Rows(i)("ID_DE_LA_LLAMADA").ToString

            Dim ms As String = da.SP_GRABAR_RECUPERO_EMAIL_GRABACIONES(be)

            Select Case ms
                Case "0"
                    c_no_bien += 1
                Case "1"
                    c_bien += 1
                    Dim c As DataRow = dt.NewRow
                Case Else
                    c_error += 1
            End Select
        Next

        grvCarga.DataSource = dt
        grvCarga.DataBind()

        lblMsg.Text = "Archivos Correcto " & c_bien & ",  Archivos no subidos " & c_no_bien & ",  Archivos con errores " & c_error
        btnGenerar.Visible = False
        LIMPIAR()


    End Sub
    Sub LIMPIAR()
        Session("tablaCarga") = Nothing
        Dim dt As DataTable = Session("tablaCarga")
        grvCarga.DataSource = dt
        grvCarga.DataSource = Nothing
        grvCarga.DataBind()
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        GUARDAR()
    End Sub

End Class
