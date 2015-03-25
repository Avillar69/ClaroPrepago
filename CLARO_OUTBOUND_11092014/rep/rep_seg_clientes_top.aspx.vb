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

            Dim dtGeneral As New DataTable()
            dtGeneral.Columns.Add("ID_LOG", Type.GetType("System.String"))
            dtGeneral.Columns.Add("FECHA_GESTION", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("HORA INI", Type.GetType("System.String"))
            dtGeneral.Columns.Add("HORA_FIN", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("TIEMPO EN SEG.", Type.GetType("System.String"))
            dtGeneral.Columns.Add("LOADID", Type.GetType("System.String"))
            dtGeneral.Columns.Add("ID2", Type.GetType("System.String"))
            dtGeneral.Columns.Add("ID_FINAL", Type.GetType("System.String"))
            dtGeneral.Columns.Add("FINAL", Type.GetType("System.String"))
            dtGeneral.Columns.Add("LOGIN", Type.GetType("System.String"))
            dtGeneral.Columns.Add("TELEFONO", Type.GetType("System.String"))
            dtGeneral.Columns.Add("TALKTIME", Type.GetType("System.String"))
            dtGeneral.Columns.Add("ACWTIME", Type.GetType("System.String"))

            dtGeneral.Columns.Add("ID", Type.GetType("System.String"))
            dtGeneral.Columns.Add("NOMBRE_DE_CARTERA", Type.GetType("System.String"))
            dtGeneral.Columns.Add("RUC/DNI", Type.GetType("System.String"))
            dtGeneral.Columns.Add("CUSTOMER_ID_(CUST_ACCOUNT)", Type.GetType("System.String"))
            dtGeneral.Columns.Add("NRO_DE_DOCUMENTO", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("PLAN TARIFARIO ", Type.GetType("System.String"))
            dtGeneral.Columns.Add("AFILIACION_A_CORREO", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("MONTO GESTIONADO", Type.GetType("System.String"))
            dtGeneral.Columns.Add("NOMBRE_COMPLETO_CLIENTE", Type.GetType("System.String"))
            dtGeneral.Columns.Add("AGENTE", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("TIPO DE GESTION ( CALL / CAMPO / IVR )", Type.GetType("System.String"))
            dtGeneral.Columns.Add("TIPIFICACION", Type.GetType("System.String"))
            dtGeneral.Columns.Add("ESCENARIO_DE_TIPIFICACION", Type.GetType("System.String"))
            dtGeneral.Columns.Add("RESULTADO_(CONTACTO/No_contacto)", Type.GetType("System.String"))
            dtGeneral.Columns.Add("MOTIVO_NO_PAGO", Type.GetType("System.String"))
            dtGeneral.Columns.Add("OBSERVACIONES_(DETALLE_DE_GESTION)", Type.GetType("System.String"))
            dtGeneral.Columns.Add("TELF_DE_CONTACTO", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("GESTOR", Type.GetType("System.String"))
            dtGeneral.Columns.Add("DEBITO", Type.GetType("System.String"))

            'Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_SEG_CLI_TOP(be)

            'Dim IDS As String = ""
            'For a = 0 To dtHistorial.Rows.Count - 1
            'IDS = "" & dtHistorial.Rows(a)("ID").ToString

            'IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)
            Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_CLIENTE_TOP(be)
            If dtScripting.Rows.Count > 0 Then

                'IDS = IDS & "" & dtHistorial.Rows(a)("ID").ToString
                'For i = 0 To dtHistorial.Rows.Count - 1
                'For b = 0 To dtScripting.Rows.Count - 1
                'Dim IDhIS As String = dtHistorial.Rows(b)("ID").ToString.Trim

                'For j = 0 To dtScripting.Rows.Count - 1
                'Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                'If IDScrip = IDhIS Then
                'Dim dr = dtGeneral.NewRow

                'dr(0) = dtHistorial.Rows(a)("ID").ToString
                'dr(1) = dtHistorial.Rows(a)("ID LOG").ToString
                'dr(2) = dtHistorial.Rows(a)("HORA FIN").ToString
                'dr(3) = dtHistorial.Rows(a)("LOADID").ToString
                'dr(4) = dtHistorial.Rows(a)("ID").ToString
                'dr(5) = dtHistorial.Rows(a)("ID_FINAL").ToString
                'dr(6) = dtHistorial.Rows(a)("FINAL").ToString
                'dr(7) = dtHistorial.Rows(a)("LOGIN").ToString
                'dr(8) = dtHistorial.Rows(a)("TELEFONO").ToString
                'dr(9) = dtHistorial.Rows(a)("TALKTIME").ToString
                'dr(10) = dtHistorial.Rows(a)("ACWTIME").ToString
                'dr(11) = dtScripting.Rows(b)("ID").ToString
                'dr(12) = dtScripting.Rows(b)("NOMBRE DE CARTERA").ToString
                'dr(13) = dtScripting.Rows(b)("RUC/DNI").ToString
                'dr(14) = dtScripting.Rows(b)("CUSTOMER ID ( CUST ACCOUNT )").ToString
                'dr(15) = dtScripting.Rows(b)("NRO DE DOCUMENTO").ToString
                'dr(16) = dtScripting.Rows(b)("AFILIACION A CORREO").ToString
                'dr(17) = dtScripting.Rows(b)("NOMBRE COMPLETO CLIENTE").ToString
                'dr(18) = dtScripting.Rows(b)("AGENTE").ToString
                'dr(19) = dtScripting.Rows(b)("TIPIFICACION").ToString
                'dr(20) = dtScripting.Rows(b)("ESCENARIO DE TIPIFICACION").ToString
                'dr(21) = dtScripting.Rows(b)("RESULTADO ( CONTACTO / No contacto )").ToString
                'dr(22) = dtScripting.Rows(b)("MOTIVO NO PAGO").ToString
                'dr(23) = dtScripting.Rows(b)("OBSERVACIONES ( DETALLE DE GESTION )").ToString
                'dr(24) = dtScripting.Rows(b)("TELF. DE CONTACTO").ToString
                'dr(25) = dtScripting.Rows(b)("DEBITO").ToString



                'dtGeneral.Rows.Add(dr)
                'End If
                'Next
                'Next
            Else
                lblMsg.Text = "No hay datos con parametro de busqueda"

            End If
            'Next
            If dtScripting.Rows.Count > 0 Then
                Session("tablaExportar") = dtScripting
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
        Dim nombre As String = "Seguimiento_ClientesTop"
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
        Dim file As String = "Seguimiento_ClientesTop_" & Now.ToString("yyyyMMddHHmmss")
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