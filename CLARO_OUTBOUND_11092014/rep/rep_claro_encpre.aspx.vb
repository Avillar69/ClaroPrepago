Imports System.Data
Imports System.IO
Imports System.Drawing

Partial Class rep_rep_claro_encpre
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtInicio.Text = Now.ToString("yyyy-MM-dd")
            txtFin.Text = Now.ToString("yyyy-MM-dd")
            Session("tablaExportar") = Nothing
        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Session("tablaExportar") = Nothing
        Session("tablaCarga") = Nothing
        Session("ids") = Nothing
        lnkExportar1.Visible = False

        grvReporte.DataSource = Nothing
        grvREPORTE.DataSource = Nothing
        grvReporte.DataBind()
        grvREPORTE.DataBind()
        lblMsg.Text = ""
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            Dim dt As DataTable = da.SP_REPORTE_DETALLE_RESPUESTA(be)
            If dt.Rows.Count > 0 Then
                lblMsg.Text = "TOTAL DE REGISTROS : " & dt.Rows.Count
                lblMsg.CssClass = "alert alert-success"
                lnkExportar1.Visible = True
                grvREPORTE.DataSource = dt
                grvReporte.DataBind()
                Session("tablaExportar") = dt
            Else
                lblMsg.Text = "SIN REGISTROS VALIDOS PARA LAS FECHAS EN BUSQUEDA"
                lblMsg.CssClass = "alert alert-danger"
            End If



        Catch ex As Exception
            lblMsg.Text = "[ERROR #500 ####] : " & ex.Message
            lblMsg.CssClass = "alert alert-danger"
        End Try
    End Sub


    Sub EXPORTAR1()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        grvReporte.DataSource = Nothing
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
        grvReporte.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim mes As String = Request("Mes")
        Dim anio As String = Request("Ano")
        Dim nombre As String = "Tv Recupero 3"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

   


    'Protected Sub lnkExportarCsv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportarCsv.Click
    '    If grvReporte.Rows.Count > 0 Then
    '        ExportarTxt()
    '    End If
    'End Sub


    Protected Sub ExportToExcel1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar1.Click
        EXPORTAR1()
    End Sub
End Class
