Imports System.Data
Imports System.IO

Partial Class rep_rep_porta
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
        lnkExportar.Visible = False
        lnkExportarCsv.Visible = False
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Dim i As Integer = 0
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text
            Dim dtScripting As DataTable = da.SP_REPORTE_OUT_ENCUESTA(be)
            If dtScripting.Rows.Count > 0 Then
                grvReporte.DataSource = dtScripting
                grvReporte.DataBind()
            Else
                lblMsg.Text = "No hay datos con parametro de busqueda"
            End If

            If dtScripting.Rows.Count > 0 Then
                Session("tablaExportar") = dtScripting
                grvReporte.DataSource = dtScripting
                grvReporte.DataBind()
                lnkExportar.Visible = True
                lnkExportarCsv.Visible = True
                lblMsg.Text = "Cantidad de registros encontrados: " & dtScripting.Rows.Count
            End If


        Catch ex As Exception
            lblMsg.Text = "btnBuscar " & ex.Message
        End Try
    End Sub
    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
        If grvReporte.Rows.Count > 0 Then
            EXPORTAR()
        End If
    End Sub
    Sub EXPORTARBK()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        grvReporte2.DataSource = Nothing
        grvReporte2.DataSource = Session("tablaExportar")
        grvReporte2.DataBind()
        grvReporte2.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte2)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim nombre As String = "reporte_Encuensta_"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
	
	Sub EXPORTAR()
        Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"
        Response.ClearContent()
        Response.Write(style)
		grvReporte2.DataSource = Session("tablaExportar")
        grvReporte2.DataBind()
        Dim file As String = "reporte_Encuensta_" & Now.ToString("yyyyMMddHHmmss")
        For i As Integer = 0 To grvReporte2.Rows.Count - 1
            Dim row As GridViewRow = grvReporte2.Rows(i)
            ''Apply text style to each Row
            row.Cells(6).Attributes.Add("class", "textmode")
            row.Cells(7).Attributes.Add("class", "textmode")
            row.Cells(8).Attributes.Add("class", "textmode")
            row.Cells(17).Attributes.Add("class", "textmode")
            row.Cells(18).Attributes.Add("class", "textmode")
            row.Cells(19).Attributes.Add("class", "textmode")
        Next
        Response.AddHeader("content-disposition", "attachment; filename=" & file & ".xls")
        Response.ContentType = "application/vnd.ms-excel"
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        grvReporte2.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte2)

        pagina.RenderControl(htw)
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sw.ToString())
        Response.Flush()
        Response.End()
    End Sub
	

    Protected Sub grvReporte_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte.PageIndexChanging
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
    End Sub

    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID, ID_LOG, ID_FINAL, DESCRIPCION_FINAL, LOGIN, AGENTE, TELEFONO, TALKTIME, FECHA_GESTION, HORA_INICIO, HORA_FIN, TIEMPO_EN_SEG, IMPORTE_FACTURADO, IMPORTE_PENDIENTE, IMPORTE_PENDIENTE_SOLES, IMPORTE_DISPUTA, NOMBRE_DE_CARTERA, RUC/DNI, CUSTOMER_ID_(CUST_ACCOUNT), NRO_DE_DOCUMENTO, AFILIACION_A_CORREO, NOMBRE_COMPLETO_CLIENTE, AGENTE1, TIPIFICACION, ESCENARIO_DE_TIPIFICACION, RESULTADO_(CONTACTO/No_contacto), MOTIVO_NO_PAGO, OBSERVACIONES_(DETALLE_DE_GESTION), TELF_DE_CONTACTO, DEBITO")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "ID" Then campo = campo & " ,"
                If cabecera = "ID_LOG" Then campo = campo & " ,"
                If cabecera = "ID_FINAL" Then campo = campo & " ,"
                If cabecera = "DESCRIPCION_FINAL" Then campo = campo & " ,"
                If cabecera = "LOGIN" Then campo = campo & " ,"
                If cabecera = "AGENTE" Then campo = campo & " ,"
                If cabecera = "TELEFONO" Then campo = campo & " ,"
                If cabecera = "TALKTIME" Then campo = campo & " ,"
                If cabecera = "FECHA_GESTION" Then campo = campo & " ,"
                If cabecera = "HORA_INICIO" Then campo = campo & " ,"
                If cabecera = "HORA_FIN" Then campo = campo & " ,"
                If cabecera = "TIEMPO_EN_SEG" Then campo = campo & " ,"
                If cabecera = "IMPORTE_FACTURADO" Then campo = campo & " ,"
                If cabecera = "IMPORTE_PENDIENTE" Then campo = campo & " ,"
                If cabecera = "IMPORTE_PENDIENTE_SOLES" Then campo = campo & " ,"
                If cabecera = "IMPORTE_DISPUTA" Then campo = campo & " ,"
                If cabecera = "NOMBRE_DE_CARTERA" Then campo = campo & " ,"
                If cabecera = "RUC/DNI" Then campo = campo & " ,"
                If cabecera = "CUSTOMER_ID_(CUST_ACCOUNT)" Then campo = "'" & campo & ","
                If cabecera = "NRO_DE_DOCUMENTO" Then campo = "'" & campo & ","
                If cabecera = "AFILIACION_A_CORREO" Then campo = campo & " ,"
                If cabecera = "NOMBRE_COMPLETO_CLIENTE" Then campo = campo & " ,"
                If cabecera = "AGENTE1" Then campo = campo & " ,"
                If cabecera = "TIPIFICACION" Then campo = campo & " ,"
                If cabecera = "ESCENARIO_DE_TIPIFICACION" Then campo = campo & " ,"
                If cabecera = "RESULTADO_(CONTACTO/No_contacto)" Then campo = campo & " ,"
                If cabecera = "MOTIVO_NO_PAGO" Then campo = campo & " ,"
                If cabecera = "OBSERVACIONES_(DETALLE_DE_GESTION)" Then campo = campo & " ,"
                If cabecera = "TELF_DE_CONTACTO" Then campo = campo & " ,"
                If cabecera = "DEBITO" Then campo = campo & " ,"

                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Seguimiento_ClientesTop.csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()

    End Sub

    Protected Sub lnkExportarCsv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportarCsv.Click
        If grvReporte.Rows.Count > 0 Then
            ExportarTxt()
        End If
    End Sub
End Class