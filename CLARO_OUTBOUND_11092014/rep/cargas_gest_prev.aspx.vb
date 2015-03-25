Option Explicit On
Imports System.Data
Imports System.IO
Imports System.Drawing
Imports System.Linq
Imports System
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Globalization


Partial Class rep_rep_porta
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO
    Dim arr_cboServ() As String = {"SELECCIONAR", "GEST. PREVENTIVA", "GEST. INFORMATIVA"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            'txtCarga.Text = Now.ToString("yyyy-MM-dd")
            cboServ.DataSource = arr_cboServ : cboServ.DataBind()

            'Dim script As String = "$(document).ready(function () { $('[id*=btnBuscar]').click(); });"
            'ClientScript.RegisterStartupScript(Me.GetType, "load", script, True)

        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

        Dim tablaResult As New DataTable
        tablaResult.Columns.Add("ID")
        tablaResult.Columns.Add("D_CLIENTE")
        tablaResult.Columns.Add("D_NRO_DOCUMENTO")
        tablaResult.Columns.Add("SERVICEID")
        tablaResult.Columns.Add("ESTADO")
        tablaResult.Columns.Add("CARGA")
        Dim serviceId = 0
        Select Case cboServ.Text
            Case "GEST. PREVENTIVA"
                serviceId = 178
            Case "GEST. INFORMATIVA"
                serviceId = 189
        End Select
        Dim listaIds As DataTable = da.SP_OBTENER_CARGAS_GESTPREV(serviceId, txtCarga.Text)

        If listaIds.Rows.Count < 1 Then
            lblMsg.Text = "No se encontraron Registros"
            lblMsg.Visible = True
            grvReporte.DataSource = Nothing : grvReporte.DataBind()
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
                be.VAR_FECHA_INICIO = txtCarga.Text

                Dim dtHistorial As DataTable = da.SP_RESULTADO_PRESENCE_CARGA_GESTPREV(serviceId, cadenaIds)
                If dtHistorial.Rows.Count > 0 Then

                    Dim result = (From objSql In listaIds.AsEnumerable(), objMysql In dtHistorial.AsEnumerable()
                                  Where objSql.Field(Of Decimal)("ID").ToString = (objMysql.Field(Of Decimal)("ID").ToString) _
                        Select New With
                      {.ID = objSql.Field(Of Decimal)("ID"), _
                        .SERVICEID = objSql.Field(Of Decimal)("SERVICEID"), _
                        .ESTADO = objSql.Field(Of String)("ESTADO"), _
                        .CARGA = objSql.Field(Of Decimal)("CARGA"), _
                        .D_CLIENTE = objMysql.Field(Of String)("D_CLIENTE"), _
                        .D_NRO_DOCUMENTO = objMysql.Field(Of String)("D_NRO_DOCUMENTO") _
                         }).ToList()

                    For Each objSql In result.ToList
                        Dim row As DataRow = tablaResult.NewRow()
                        row("ID") = objSql.ID
                        row("SERVICEID") = objSql.SERVICEID
                        row("ESTADO") = objSql.ESTADO
                        row("CARGA") = objSql.CARGA
                        row("D_CLIENTE") = objSql.D_CLIENTE
                        row("D_NRO_DOCUMENTO") = objSql.D_NRO_DOCUMENTO
                        tablaResult.Rows.Add(row)
                    Next

                    grvReporte.DataSource = result
                    grvReporte.DataBind()
                    Session("tablaCarga") = result
                    lnkExportar.Visible = True
                    lblMsg.Text = "Cantidad de registros encontrados: " & tablaResult.Rows.Count
                    lblMsg.CssClass = "alert alert-success"
                Else
                    lblMsg.Text = "SIN REGISTROS VALIDOS PARA LAS FECHAS EN BUSQUEDA"
                    lblMsg.CssClass = "alert alert-danger"
                    grvReporte.DataSource = Nothing : grvReporte.DataBind()
                    lnkExportar.Visible = False
                    grvReporte.Visible = False
                End If
            Catch ex As Exception
                lblMsg.Text = "ERROR #500 ### = " & ex.Message
                lblMsg.CssClass = "alert alert-danger"
            End Try
        End If

    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
        ExportToExcel()
    End Sub
    Sub EXPORTAR()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        grvExport.DataSource = Session("tablaCarga")
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
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & mes & anio & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub ExportToExcel()
        'Dim dtexport As DataTable = Session("tablaCarga")
        'Dim row0 As Integer = dtexport.Rows(dtexport.Rows.Count() - 1)(6)
        'If row0 > 0 Then
        grvExport.DataSource = Session("tablaCarga")
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
            grvExport.DataSource = Session("tablaCarga")
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
        'End If

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
