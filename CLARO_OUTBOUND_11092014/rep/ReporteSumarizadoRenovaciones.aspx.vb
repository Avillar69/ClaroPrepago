Imports System.Data
Imports System.IO

Partial Class rep_ReporteSumarizadoRenovaciones
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO
    Dim be2 As New BE_CLARO_MIGRA

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtInicio.Text = Now.ToString("yyyy-MM-dd")
            txtFin.Text = Now.ToString("yyyy-MM-dd")
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        lnkExportar.Visible = False
        lnkExpoExcel.Visible = False
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text
            'dtHistorial.Columns.Add("ID", Type.GetType("System.String"))

            Dim dtHistorial As DataTable = da.SP_REPORTE_SUMARIZADO_RENOVACIONES(be)
            If dtHistorial.Rows.Count > 0 Then

                'dtHistorial.Rows(i)("ID") = dtScripting.Rows(j)("ID").ToString

                grvReporte.DataSource = dtHistorial
                Session("tablaExportarReno") = dtHistorial
                grvReporte.DataBind()
                lnkExportar.Visible = True
                lnkExpoExcel.Visible = True
                lblMsg.Text = "Cantidad de registros encontrados: " & dtHistorial.Rows.Count
            Else
                lblMsg.Text = "No hay datos con parametro de busqueda"
            End If


        Catch ex As Exception
            lblMsg.Text = "btnBuscar " & ex.Message
        End Try

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
        grvReporte3.DataSource = Nothing
        grvReporte3.DataSource = Session("tablaExportarReno")
        grvReporte3.DataBind()
        grvReporte3.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte3)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim mes As String = Request("Mes")
        Dim anio As String = Request("Ano")
        Dim nombre As String = "RetencionesOut"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=ReporteSumarizadoRenovaciones.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Sub ExportarTxt()
        Dim var_ruta As String = ""
        Dim dt As DataTable = Session("tablaExportarReno")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("FECHAHORA| IDBASE| IDCALLCENTER| CALLCENTER| MSISDN| IDCAMPANA| IDTIPIFICACION| NIVEL1| NIVEL2| NIVEL3| NIVEL4| IDAGENTE| COMENTARIO| OTROS2| OTROS3| NCONTACTOS")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                campo = Replace(campo, "&nbsp;", "")
                campo = Replace(campo, "á", "a")
                campo = Replace(campo, "é", "e")
                campo = Replace(campo, "í", "i")
                campo = Replace(campo, "ó", "o")
                campo = Replace(campo, "ú", "u")
                campo = Replace(campo, "ñ", "n")
                If cabecera = "FECHAHORA" Then campo = campo & "|"
                If cabecera = "IDBASE" Then campo = campo & "|"
                If cabecera = "IDCALLCENTER" Then campo = campo & "|"
                If cabecera = "CALLCENTER" Then campo = campo & "|"
                If cabecera = "MSISDN" Then campo = campo & "|"
                If cabecera = "IDCAMPANA" Then campo = campo & "|"
                If cabecera = "IDTIPIFICACION" Then campo = campo & "|"
                If cabecera = "NIVEL1" Then campo = campo & "|"
                If cabecera = "NIVEL2" Then campo = campo & "|"
                If cabecera = "NIVEL3" Then campo = campo & "|"
                If cabecera = "NIVEL4" Then campo = campo & "|"
                If cabecera = "IDAGENTE" Then campo = campo & "|"
                If cabecera = "COMENTARIO" Then campo = campo & "|"
                If cabecera = "OTROS2" Then campo = campo & "|"
                If cabecera = "OTROS3" Then campo = campo & "|"
                If cabecera = "NCONTACTOS" Then campo = campo & "|"

                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i


        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Sumarizado_Reno_" & Now.ToString("yyyyMMdd") & ".txt")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)

        Response.Write(str.ToString())
        Response.End()
    End Sub

    Protected Sub grvReporte_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte.PageIndexChanging
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportarReno")
        grvReporte.DataBind()
    End Sub

    Protected Sub lnkExpoExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExpoExcel.Click
        If grvReporte.Rows.Count > 0 Then
            EXPORTAR()
        End If
    End Sub

End Class
