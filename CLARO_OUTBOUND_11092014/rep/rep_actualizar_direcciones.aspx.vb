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
            Dim dtHistorial As DataTable = da.SP_LISTA_ACT_DIRECCIONES_X_FECHA(be.inicio, be.fin)
            If dtHistorial.Rows.Count > 0 Then
                grvReporte.DataSource = dtHistorial
                grvReporte.DataBind()
                Session("tablaExportar") = dtHistorial
                lnkExportar.Visible = True
                lnkExportarCsv.Visible = True
            Else
                'lnkExportar1.Visible = False
                'lnkExportar2.Visible = False
                lblMsg.Text = "SIN REGISTROS VALIDOS PARA LAS FECHAS EN BUSQUEDA"
                lblMsg.CssClass = "alert alert-danger"

            End If

            dtHistorial.Dispose()
            'Else
            'lblMsg.Text = "No hay datos con parametro de busqueda"

            'End If
            ''Next
            'If dtScripting.Rows.Count > 0 Then
            '    Session("tablaExportar") = dtScripting
            '    Session("tablaExportar") = dtScripting
            '    grvReporte.DataSource = dtScripting
            '    grvReporte.DataBind()
            '    lnkExportar.Visible = True
            '    lnkExportarCsv.Visible = True
            ''    lblMsg.Text = "Cantidad de registros encontrados: " & dtScripting.Rows.Count
            'End If

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

    Sub ExportarTxt(ByVal tipo As Integer)
        Dim nombre As String
        Dim dt As DataTable
        If tipo = 1 Then
            dt = Session("tablaExportar")
            nombre = "Detalle_3_Play_HFC_" & Now.ToString("yyyyMMddHHmmss")
        Else
            dt = Session("tablaCarga")
            nombre = "Unico_3_Play_HFC_" & Now.ToString("yyyyMMddHHmmss")
        End If

        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID_LOG,FECHA,LOADID,ID,ID_FINAL,FINAL,LOGIN,TELEFONO,TALKTIME,ACWTIME,D_SEMANA,D_COD_CLIENTE,D_NOMBRE_CLI,D_SOLUCION,D_DIRECCION,D_DISTRITO,D_TELEFONO_01,D_TELEFONO_02,D_TELEFONO_03,D_TELEFONO_04,D_TELEFONO_05,D_PROYECTO,D_NRO_DOCUMENTO,D_SERVICIO,D_REFERENCIA,D_DEPARTAMENTO,D_FEC_INSTALACION,D_PROVINCIA,TXT_TITULAR_USUARIO,TXT_TELEF_ADICIONAL,CBO_PRODUCTO,CBO_TIPO,CBO_TIPO_LLAMADA,CBO_SUBTIPO_LLAMADA,CBO_PROB_TECNICO,CBO_NO_CONFORME,CBO_INFORMACION,CBO_SOLICITUD_PEN,CBO_INTERNET,CBO_TELEFONIA,CBO_TV,CBO_INTER_TELEF,CBO_INTER_TV,CBO_INTER_TV_TELEF,CBO_RECIBO,CBO_ESTADO_PAGO,CBO_ESTADO_RECIBO,TXT_CORREO,CBO_AFILIA_RECIBO_ELEC,TXT_PORQUE_NO,CBO_REALIZA_DESCARTE_ADM,CBO_DETECTA_PROB_ADM,CBO_NO_DETEC_PROB_ADM,CBO_FRENTE_A_EQ,CBO_NO_FRENTE_A_EQ,TXT_CODIGO,TXT_NOM_CLI,TXT_NRO_NOM_CONTACTO,CBO_SERVI_AFECTADO,TXT_CAMBIO_EQ,TXT_HORARIO_LLAMADA,DTP_FEC_INSTALACION,TXT_OBSERVACION,TXT_PROB_TEC_CORREO")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim campo As String
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                If dt.Rows(i)(j).ToString Is DBNull.Value OrElse dt.Rows(i)(j).ToString Is Nothing OrElse dt.Rows(i)(j).ToString.Equals("") Then
                    campo = ""
                Else
                    campo = dt.Rows(i)(j).ToString()
                End If
                'Dim campo As String = dt.Rows(i)(j).ToString
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
                If cabecera = "D_SEMANA" Then campo = campo & " ,"
                If cabecera = "D_COD_CLIENTE" Then campo = campo & " ,"
                If cabecera = "D_NOMBRE_CLI" Then campo = campo & " ,"
                If cabecera = "D_SOLUCION" Then campo = campo & " ,"
                If cabecera = "D_DIRECCION" Then campo = campo & " ,"
                If cabecera = "D_DISTRITO" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_01" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_02" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_03" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_04" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_05" Then campo = campo & " ,"

                If cabecera = "D_PROYECTO" Then campo = campo & " ,"
                If cabecera = "D_NRO_DOCUMENTO" Then campo = campo & " ,"
                If cabecera = "D_SERVICIO" Then campo = campo & " ,"
                If cabecera = "D_REFERENCIA" Then campo = campo & " ,"
                If cabecera = "D_DEPARTAMENTO" Then campo = campo & " ,"
                If cabecera = "D_FEC_INSTALACION" Then campo = campo & " ,"
                If cabecera = "D_PROVINCIA" Then campo = campo & " ,"
                If cabecera = "TXT_TITULAR_USUARIO" Then campo = campo & " ,"
                If cabecera = "TXT_TELEF_ADICIONAL" Then campo = campo & " ,"
                If cabecera = "CBO_PRODUCTO" Then campo = campo & " ,"
                If cabecera = "CBO_TIPO" Then campo = campo & " ,"
                If cabecera = "CBO_TIPO_LLAMADA" Then campo = campo & " ,"
                If cabecera = "CBO_SUBTIPO_LLAMADA" Then campo = campo & " ,"
                If cabecera = "CBO_PROB_TECNICO" Then campo = campo & " ,"
                If cabecera = "CBO_NO_CONFORME" Then campo = campo & " ,"

                If cabecera = "CBO_INFORMACION" Then campo = campo & " ,"
                If cabecera = "CBO_SOLICITUD_PEN" Then campo = campo & " ,"
                If cabecera = "CBO_INTERNET" Then campo = campo & " ,"
                If cabecera = "CBO_TELEFONIA" Then campo = campo & " ,"
                If cabecera = "CBO_TV" Then campo = campo & " ,"
                If cabecera = "CBO_INTER_TELEF" Then campo = campo & " ,"
                If cabecera = "CBO_INTER_TV" Then campo = campo & " ,"
                If cabecera = "CBO_INTER_TV_TELEF" Then campo = campo & " ,"
                If cabecera = "CBO_RECIBO" Then campo = campo & " ,"
                If cabecera = "CBO_ESTADO_PAGO" Then campo = campo & " ,"
                If cabecera = "CBO_ESTADO_RECIBO" Then campo = campo & " ,"
                If cabecera = "TXT_CORREO" Then campo = campo & " ,"
                If cabecera = "CBO_AFILIA_RECIBO_ELEC" Then campo = campo & " ,"
                If cabecera = "TXT_PORQUE_NO" Then campo = campo & " ,"
                If cabecera = "CBO_REALIZA_DESCARTE_ADM" Then campo = campo & " ,"
                If cabecera = "CBO_DETECTA_PROB_ADM" Then campo = campo & " ,"
                If cabecera = "CBO_NO_DETEC_PROB_ADM" Then campo = campo & " ,"
                If cabecera = "CBO_FRENTE_A_EQ" Then campo = campo & " ,"
                If cabecera = "CBO_NO_FRENTE_A_EQ" Then campo = campo & " ,"
                If cabecera = "TXT_CODIGO" Then campo = campo & " ,"
                If cabecera = "TXT_NOM_CLI" Then campo = campo & " ,"
                If cabecera = "TXT_NRO_NOM_CONTACTO" Then campo = campo & " ,"
                If cabecera = "CBO_SERVI_AFECTADO" Then campo = campo & " ,"
                If cabecera = "TXT_CAMBIO_EQ" Then campo = campo & " ,"
                If cabecera = "TXT_HORARIO_LLAMADA" Then campo = campo & " ,"
                If cabecera = "DTP_FEC_INSTALACION" Then campo = campo & " ,"
                If cabecera = "TXT_OBSERVACION" Then campo = campo & " ,"
                If cabecera = "TXT_PROB_TEC_CORREO" Then campo = campo
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & nombre & ".csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()

    End Sub
End Class