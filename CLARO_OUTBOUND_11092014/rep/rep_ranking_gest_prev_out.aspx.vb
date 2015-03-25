Imports System.Data
Imports System.IO
Imports System.Drawing

Partial Class rep_rep_porta
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            txtInicio.Text = Now.ToString("yyyy-MM-dd")
            txtFin.Text = Now.ToString("yyyy-MM-dd")
            'cboLog.DataSource = arr_cboLog : cboLog.DataBind()
            'cboServ.DataSource = arr_cboServ : cboServ.DataBind()

            Dim script As String = "$(document).ready(function () { $('[id*=btnBuscar]').click(); });"
            ClientScript.RegisterStartupScript(Me.GetType, "load", script, True)

        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Try
            be.VAR_FECHA_INICIO = txtInicio.Text
            be.VAR_FECHA_FIN = txtFin.Text

            Dim dtHistorial As DataTable = da.SP_RANK_ASIGNACION_PREVENTIVA_OUT(be)
            'Dim dtDetalle As DataTable = da.SP_RANK_DET_RECUPERO_OUT(be)

            grvReporte.DataSource = dtHistorial
            grvReporte.DataBind()
            'Session("tablaExportar") = dtDetalle
            Session("tablaCarga") = dtHistorial
            lblMsg.Text = "Cantidad de registros encontrados: " & dtHistorial.Rows.Count
            lblMsg.CssClass = "alert alert-success"

        Catch ex As Exception
            lblMsg.Text = "ERROR #500 ### = " & ex.Message
            lblMsg.CssClass = "alert alert-danger"
        End Try
    End Sub

    'Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
    '    EXPORTAR()
    '    ExportToExcel()
    'End Sub
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
        Dim nombre As String = "ranking_gest_prev"
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
