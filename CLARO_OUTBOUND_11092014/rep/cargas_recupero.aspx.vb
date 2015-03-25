Imports System.Data
Imports System.IO
Imports System.Drawing

Partial Class rep_rep_porta
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO
    Dim arr_cboServ() As String = {"SELECCIONAR", "REC. FISICA", "REC. ELECTRONICO", "AFIL. CORREO"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            txtInicio.Text = Now.ToString("yyyy-MM-dd")
            txtFin.Text = Now.ToString("yyyy-MM-dd")
            cboServ.DataSource = arr_cboServ : cboServ.DataBind()

            'Dim script As String = "$(document).ready(function () { $('[id*=btnBuscar]').click(); });"
            'ClientScript.RegisterStartupScript(Me.GetType, "load", script, True)

        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

        If cboServ.SelectedIndex = 0 Then

            lblMsg.Text = "Error : Seleccione un servicio"
            lblMsg.CssClass = "alert alert-danger"
            lblMsg.Visible = True
            Exit Sub
        End If

        Dim listaIds As DataTable = da.SP_OBTENER_CARGAS_RECUPERO(txtInicio.Text, txtFin.Text, cboServ.SelectedIndex)
        Dim serviceId = 0

        If listaIds.Columns(0).ColumnName = "ERROR" Then
            lblMsg.Text = "Error : " + listaIds(0)(0).ToString
            lblMsg.CssClass = "alert alert-danger"
            lblMsg.Visible = True
        ElseIf listaIds.Rows.Count = 0 Then
            lblMsg.CssClass = "alert alert-danger"
            lblMsg.Text = "No se encontraron Registros"
            lblMsg.Visible = True
        Else
            Dim cadenaIds As String = ""
            Dim cont As Integer = 0
            For Each item As DataRow In listaIds.Rows
                If cont = listaIds.Rows.Count - 1 Then
                    cadenaIds = cadenaIds + item("ID").ToString
                Else
                    cadenaIds = cadenaIds + item("ID").ToString + ","
                End If
                cont = cont + 1
            Next
            lnkExportar.Visible = False
            grvReporte.DataSource = Nothing
            grvReporte.DataBind()
            lblMsg.Text = ""
            Try
                be.VAR_SERVICEID = cboServ.SelectedIndex
                be.VAR_FECHA_INICIO = txtInicio.Text
                be.VAR_FECHA_FIN = txtFin.Text
                Select Case cboServ.Text
                    Case "REC. FISICA"
                        serviceId = 273
                    Case "REC. ELECTRONICO"
                        serviceId = 274
                    Case "AFIL. CORREO"
                        serviceId = 275
                End Select
                Dim dtHistorial As DataTable = da.SP_RESULTADO_PRESENCE_CARGA_RECUPERO(serviceId, cadenaIds)
                If dtHistorial.Rows.Count > 0 Then
                    grvReporte.DataSource = dtHistorial
                    grvReporte.DataBind()
                    Session("tablaCarga") = dtHistorial
                    lnkExportar.Visible = True
                    lblMsg.Text = "Cantidad de registros encontrados: " & dtHistorial.Rows.Count
                    lblMsg.CssClass = "alert alert-success"
                Else
                    lblMsg.Text = "SIN REGISTROS VALIDOS PARA LAS FECHAS EN BUSQUEDA"
                    lblMsg.CssClass = "alert alert-danger"
                    grvReporte.DataSource = Nothing : grvReporte.DataBind()
                    lnkExportar.Visible = False
                End If
            Catch ex As Exception
                lblMsg.Text = "ERROR #500 ### = " & ex.Message
                lblMsg.CssClass = "alert alert-danger"
            End Try
        End If

    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
        ExportarTxt()
    End Sub
    Sub EXPORTAR()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        grvExport.DataSource = Session("tablaExportar")
        grvExport.DataBind()
        grvExport.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvExport)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim mes As String = Request("Mes")
        Dim anio As String = Request("Ano")
        Dim nombre As String = "Dynamicall"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Dim dtexport As DataTable = Session("tablaCarga")
        Dim row0 As Integer = dtexport.Rows(dtexport.Rows.Count() - 1)(6)
        If row0 > 0 Then
            grvExport.DataSource = Session("tablaExportar")
            grvExport.DataBind()

            Dim nombre = "Report_Ranking " & Now.ToString("yyyyMMddHHmmss")
            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=" & nombre & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentType = "application/vnd.ms-excel"
            Using sw As New StringWriter()
                Dim hw As New HtmlTextWriter(sw)

                'To Export all pages
                grvExport.AllowPaging = False
                grvExport.DataSource = Session("tablaExportar")
                grvExport.DataBind()

                grvExport.HeaderRow.BackColor = Color.White
                For Each cell As TableCell In grvExport.HeaderRow.Cells
                    cell.BackColor = grvExport.HeaderStyle.BackColor
                Next
                For Each row As GridViewRow In grvExport.Rows
                    'row.BackColor = grvExport.HeaderStyle.BackColor
                    row.BackColor = Color.White
                    For Each cell As TableCell In row.Cells
                        If row.RowIndex Mod 2 = 0 Then
                            cell.BackColor = grvExport.RowStyle.BackColor
                            'cell.BackColor = Color.White
                        Else
                            cell.BackColor = grvExport.RowStyle.BackColor
                            cell.BackColor = Color.White
                        End If
                        cell.CssClass = "textmode"
                    Next
                Next

                grvExport.RenderControl(hw)
                'style to format numbers to string
                Dim style As String = "<style> .textmode { } </style>"
                Response.Write(style)
                Response.Output.Write(sw.ToString())
                Response.Flush()
                Response.[End]()
            End Using
        End If

    End Sub

    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaCarga")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("SOURCEID, PHONE,")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "SOURCEID" Then campo = campo & " ,"
                If cabecera = "PHONE" Then campo = "'" + campo & "' ,"
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=CargaBase" + cboServ.Text + ".csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()

    End Sub

    Protected Sub ExportToExcelRes(sender As Object, e As EventArgs)
        Dim nombre = "Report_Ranking_Res " & Now.ToString("yyyyMMddHHmmss")
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            grvReporte.AllowPaging = False
            grvReporte.DataSource = Session("tablaCarga")
            grvReporte.DataBind()

            grvReporte.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In grvReporte.HeaderRow.Cells
                cell.BackColor = grvExport.HeaderStyle.BackColor
                cell.ForeColor = Color.White
            Next
            For Each row As GridViewRow In grvExport.Rows
                'row.BackColor = grvExport.HeaderStyle.BackColor
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = grvExport.RowStyle.BackColor
                        'cell.BackColor = Color.White
                    Else
                        'cell.BackColor = grvExport.RowStyle.BackColor
                        cell.BackColor = Color.White
                    End If
                    cell.CssClass = "textmode"
                Next
            Next
            grvReporte.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub


End Class
