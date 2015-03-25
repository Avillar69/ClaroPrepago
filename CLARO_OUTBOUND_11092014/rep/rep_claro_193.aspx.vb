Imports System.Data
Imports System.IO

Partial Class rep_rep_porta
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO
    Dim num As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtInicio.Text = Now.ToString("yyyy-MM-dd")
            txtFin.Text = Now.ToString("yyyy-MM-dd")
            Session("tablaExportar") = Nothing
        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim tablaResult As New DataTable
        tablaResult.Columns.Add("ID")
        tablaResult.Columns.Add("FECHA_LLAMADA")
        tablaResult.Columns.Add("SERVICEID")
        tablaResult.Columns.Add("LOGIN")
        tablaResult.Columns.Add("QCODE")
        tablaResult.Columns.Add("QCODEDESCRIPTION")
        tablaResult.Columns.Add("TALKTIME")
        tablaResult.Columns.Add("ACWTIME")
        tablaResult.Columns.Add("D_TELEFONO")
        tablaResult.Columns.Add("D_MONTO")
        tablaResult.Columns.Add("D_PORTACION")
        tablaResult.Columns.Add("D_CEDENTE")
        tablaResult.Columns.Add("D_TIPO")
        tablaResult.Columns.Add("D_TIPO_DOC")
        tablaResult.Columns.Add("D_NRO_DOC")
        tablaResult.Columns.Add("D_CLIENTE")
        tablaResult.Columns.Add("FECHA_COMPROMISO")
        tablaResult.Columns.Add("OBSERVACIONES")

        Dim listaIds As DataTable = da.SP_HISTORIAL_CLARO_193(txtInicio.Text, txtFin.Text)

        If listaIds.Rows.Count < 1 Then
            lblMsg.Text = "No se encontraron Registros"
            lblMsg.Visible = True
            grvReporte.DataSource = Nothing : grvReporte.DataBind()
        Else
            lnkExportar.Visible = False
            grvReporte.DataSource = Nothing
            grvReporte.DataBind()
            lblMsg.Text = ""
            Try
                Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_193_SQL(txtInicio.Text, txtFin.Text)
                If dtHistorial.Rows.Count > 0 Then
               
                   
                    'Dim result = (From objSql In dtHistorial.AsEnumerable(), objMysql In listaIds.AsEnumerable()
                    '           Where objSql.Field(Of Decimal)("SOURCEID").ToString = (objMysql.Field(Of Decimal)("ID").ToString) _
                    Dim results = (From table1 In dtHistorial Join table2 In listaIds _
                    On table1("SOURCEID").ToString Equals table2("SOURCEID").ToString _
                       Select New With _
                     {.ID = table2("SOURCEID"), _
                      .FECHA_LLAMADA = table2("FECHA_LLAMADA"), _
                      .SERVICEID = table1("SERVICEID"), _
                      .TALKTIME = table1("TALKTIME"), _
                      .ACWTIME = table1("ACWTIME"), _
                      .D_TELEFONO = table2("D_TELEFONO"), _
                      .LOGIN = table1("LOGIN"), _
                      .QCODE = table1("QCODE"), _
                      .QCODEDESCRIPTION = table1("QCODEDESCRIPTION"), _
                      .D_MONTO = table2("D_MONTO"), _
                      .D_PORTACION = table2("D_PORTACION"), _
                      .D_CEDENTE = table2("D_CEDENTE"), _
                      .D_TIPO = table2("D_TIPO"), _
                      .D_TIPO_DOC = table2("D_TIPO_DOC"), _
                      .D_NRO_DOC = table2("D_NRO_DOC"), _
                      .D_CLIENTE = table2("D_CLIENTE"), _
                      .FECHA_COMPROMISO = table2("FECHA_COMPROMISO"), _
                      .OBSERVACIONES = table2("OBSERVACIONES")})

                    For Each objSql In results.ToList
                        Dim row As DataRow = tablaResult.NewRow()
                        row("ID") = objSql.ID
                        row("FECHA_LLAMADA") = objSql.FECHA_LLAMADA
                        row("SERVICEID") = objSql.SERVICEID
                        row("LOGIN") = objSql.LOGIN
                        row("QCODE") = objSql.QCODE
                        row("QCODEDESCRIPTION") = objSql.QCODEDESCRIPTION
                        row("TALKTIME") = objSql.TALKTIME
                        row("ACWTIME") = objSql.ACWTIME
                        row("D_TELEFONO") = objSql.D_TELEFONO
                        row("D_MONTO") = objSql.D_MONTO
                        row("D_PORTACION") = objSql.D_PORTACION
                        row("D_CEDENTE") = objSql.D_CEDENTE
                        row("D_TIPO") = objSql.D_TIPO
                        row("D_TIPO_DOC") = objSql.D_TIPO_DOC
                        row("D_NRO_DOC") = objSql.D_NRO_DOC
                        row("D_CLIENTE") = objSql.D_CLIENTE
                        row("FECHA_COMPROMISO") = objSql.FECHA_COMPROMISO
                        row("OBSERVACIONES") = objSql.OBSERVACIONES
                        tablaResult.Rows.Add(row)
                    Next

                    Dim X As String = tablaResult.Rows.Count

                    grvReporte.DataSource = tablaResult
                    grvReporte.DataBind()


                    Session("tablaExportar") = results
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
        grvReporte2.EnableViewState = False
        grvReporte2.DataSource = Session("tablaExportar")
        grvReporte2.DataBind()

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte2)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim nombre As String = "Gestion_Preventiva"
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
        'grvReporte2.DataSource = Session("tablaExportar")
        '      grvReporte2.DataBind()
		
        Dim file As String = "Gestion_193_" & Now.ToString("yyyyMMddHHmmss")
        For i As Integer = 0 To grvReporte.Rows.Count - 1
            Dim row As GridViewRow = grvReporte.Rows(i)
            ''Apply text style to each Row
            'row.Cells(7).Attributes.Add("class", "textmode")
            'row.Cells(13).Attributes.Add("class", "textmode")
            'row.Cells(14).Attributes.Add("class", "textmode")
            'row.Cells(17).Attributes.Add("class", "textmode")
            'row.Cells(25).Attributes.Add("class", "textmode")
            'row.Cells(31).Attributes.Add("class", "textmode")
            'row.Cells(50).Attributes.Add("class", "textmode")
        Next
        Response.AddHeader("content-disposition", "attachment; filename=" & file & ".xls")
        Response.ContentType = "application/vnd.ms-excel"
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        grvReporte.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte)

        pagina.RenderControl(htw)
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sw.ToString())
        Response.Flush()
        Response.End()
    End Sub
	
    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID_LOG, FECHA, LOADID, ID, ID_FINAL, FINAL, LOGIN, TELEFONO, TALKTIME, ACWTIME, D_SERVICE, D_CUST_ACCOUNT, D_ACCOUNT_DESC, D_CLIENTE, D_TIPO_DOCUMENTO, D_NRO_DOCUMENTO, D_FEC_ACTIVACION, D_DEPARTAMENTO, D_PROVINCIA, D_DISTRITO, D_DIRECCION, D_CICLO_FACTURACION, D_TIPO_DOC_EMITIDO, D_RECIBO, D_FEC_EMISION, D_FEC_VENCIMIENTO, D_MONTO_RECIBO, D_FEC_ASIGNACION, D_PLAN_TARIFARIO, D_NRO_SERVICIO, D_EST_ACT_SERVICIO, CBO_ES_TITULAR, CBO_ACCEDE_A_LLAMADA, TXT_INCONVENIENTE, CBO_SE_AFILIA_X_CORREO, TXT_NOMBRE_COMPLETO, TXT_DNI, TXT_EMAIL, CBO_DOMINIO_MAIL, TXT_EMAIL_COMPLETO, TXT_DIRECCION, TXT_TELEF_ALTER_1, TXT_TELEF_ALTER_2, TXT_NOMBRE_TERCERO, TXT_TELEF_TITULAR, CBO_TIPO_CONTACTO, CBO_RESULTADO, CBO_ESCENARIO_MOTIVO, TXT_OBSERVACIONES, DIRECCION, REFERENCIA")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "ID_LOG" Then campo = campo & " ,"
                If cabecera = "FECHA" Then campo = campo & " ,"
                If cabecera = "LOADID" Then campo = campo & " ,"
                If cabecera = "ID" Then campo = campo & " ,"
                If cabecera = "ID_FINAL" Then campo = campo & " ,"
                If cabecera = "FINAL" Then campo = campo & " ,"
                If cabecera = "LOGIN" Then campo = campo & " ,"
                If cabecera = "TELEFONO" Then campo = campo & " ,"
                If cabecera = "TALKTIME" Then campo = campo & " ,"
                If cabecera = "ACWTIME" Then campo = campo & " ,"
                If cabecera = "D_SERVICE" Then campo = campo & " ,"
                If cabecera = "D_CUST_ACCOUNT" Then campo = campo & " ,"
                If cabecera = "D_ACCOUNT_DESC" Then campo = campo & " ,"
                If cabecera = "D_CLIENTE" Then campo = campo & " ,"
                If cabecera = "D_TIPO_DOCUMENTO" Then campo = campo & " ,"
                If cabecera = "D_NRO_DOCUMENTO" Then campo = "'" & campo & ","
                If cabecera = "D_FEC_ACTIVACION" Then campo = campo & " ,"
                If cabecera = "D_DEPARTAMENTO" Then campo = campo & " ,"
                If cabecera = "D_PROVINCIA" Then campo = campo & " ,"
                If cabecera = "D_DISTRITO" Then campo = campo & " ,"
                If cabecera = "D_DIRECCION" Then campo = campo & " ,"
                If cabecera = "D_CICLO_FACTURACION" Then campo = campo & " ,"
                If cabecera = "D_TIPO_DOC_EMITIDO" Then campo = campo & " ,"
                If cabecera = "D_RECIBO" Then campo = "'" & campo & ","
                If cabecera = "D_FEC_EMISION" Then campo = campo & " ,"
                If cabecera = "D_FEC_VENCIMIENTO" Then campo = campo & " ,"
                If cabecera = "D_MONTO_RECIBO" Then campo = campo & " ,"
                If cabecera = "D_FEC_ASIGNACION" Then campo = campo & " ,"
                If cabecera = "D_PLAN_TARIFARIO" Then campo = campo & " ,"
                If cabecera = "D_NRO_SERVICIO" Then campo = campo & " ,"
                If cabecera = "D_EST_ACT_SERVICIO" Then campo = campo & " ,"
                If cabecera = "CBO_ES_TITULAR" Then campo = campo & " ,"
                If cabecera = "CBO_ACCEDE_A_LLAMADA" Then campo = campo & " ,"
                If cabecera = "TXT_INCONVENIENTE" Then campo = campo & " ,"
                If cabecera = "CBO_SE_AFILIA_X_CORREO" Then campo = campo & " ,"
                If cabecera = "TXT_NOMBRE_COMPLETO" Then campo = campo & " ,"
                If cabecera = "TXT_DNI" Then campo = campo & " ,"
                If cabecera = "TXT_EMAIL" Then campo = campo & " ,"
                If cabecera = "CBO_DOMINIO_MAIL" Then campo = campo & " ,"
                If cabecera = "TXT_EMAIL_COMPLETO" Then campo = campo & " ,"
                If cabecera = "TXT_DIRECCION" Then campo = campo & " ,"
                If cabecera = "TXT_TELEF_ALTER_1" Then campo = campo & " ,"
                If cabecera = "TXT_TELEF_ALTER_2" Then campo = campo & " ,"
                If cabecera = "TXT_NOMBRE_TERCERO" Then campo = campo & " ,"
                If cabecera = "TXT_TELEF_TITULAR" Then campo = campo & " ,"
                If cabecera = "CBO_TIPO_CONTACTO" Then campo = campo & " ,"
                If cabecera = "CBO_RESULTADO" Then campo = campo & " ,"
                If cabecera = "CBO_ESCENARIO_MOTIVO" Then campo = campo & " ,"
                If cabecera = "TXT_OBSERVACIONES" Then campo = campo & " ,"
                If cabecera = "DIRECCION" Then campo = campo & " ,"
                If cabecera = "REFERENCIA" Then campo = campo & " ,"

                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Gestion_Preventiva.csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()

    End Sub

    Protected Sub grvReporte2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte2.PageIndexChanging
        'grvReporte.PageIndexChanging()
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        If grvReporte.Rows.Count > 0 Then
            ExportarTxt()
        End If
    End Sub
End Class
