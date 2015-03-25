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
        LinkButton2.Visible = False
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_GESTION_PREV(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("D_SERVICE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CUST_ACCOUNT", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_ACCOUNT_DESC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TIPO_DOCUMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NRO_DOCUMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FEC_ACTIVACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_PROVINCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DISTRITO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DIRECCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CICLO_FACTURACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TIPO_DOC_EMITIDO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_RECIBO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FEC_EMISION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FEC_VENCIMIENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MONTO_RECIBO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FEC_ASIGNACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_PLAN_TARIFARIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NRO_SERVICIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_EST_ACT_SERVICIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ES_TITULAR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACCEDE_A_LLAMADA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_INCONVENIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_SE_AFILIA_X_CORREO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NOMBRE_COMPLETO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_DNI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_EMAIL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_DOMINIO_MAIL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_EMAIL_COMPLETO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_DIRECCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_TELEF_ALTER_1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_TELEF_ALTER_2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NOMBRE_TERCERO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_TELEF_TITULAR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO_CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_RESULTADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ESCENARIO_MOTIVO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OBSERVACIONES", Type.GetType("System.String"))
				dtHistorial.Columns.Add("TIPO VIA", Type.GetType("System.String"))
				dtHistorial.Columns.Add("NOMBRE VIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO VIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MANZ_BLOQ_EDIF", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NOM MANZANA", Type.GetType("System.String"))
				dtHistorial.Columns.Add("LOTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO LOTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO TIPO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DIRECCION", Type.GetType("System.String"))


				dtHistorial.Columns.Add("TIPO URB", Type.GetType("System.String"))
				dtHistorial.Columns.Add("NOMBRE URB", Type.GetType("System.String"))				
				dtHistorial.Columns.Add("ZONA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NOMBRE ZONE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("REFERENCIA_DETALLE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("REFERENCIA", Type.GetType("System.String"))

				dtHistorial.Columns.Add("DEPARTAMENTO", Type.GetType("System.String"))
				dtHistorial.Columns.Add("PROVINCIA", Type.GetType("System.String"))
				dtHistorial.Columns.Add("DISTRITO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("LLEGO_RECIBO", Type.GetType("System.String"))


                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_GESTION_PREV(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("D_SERVICE") = dtScripting.Rows(j)("D_SERVICE").ToString
                            dtHistorial.Rows(i)("D_CUST_ACCOUNT") = dtScripting.Rows(j)("D_CUST_ACCOUNT").ToString
                            dtHistorial.Rows(i)("D_ACCOUNT_DESC") = dtScripting.Rows(j)("D_ACCOUNT_DESC").ToString
                            dtHistorial.Rows(i)("D_CLIENTE") = dtScripting.Rows(j)("D_CLIENTE").ToString
                            dtHistorial.Rows(i)("D_TIPO_DOCUMENTO") = dtScripting.Rows(j)("D_TIPO_DOCUMENTO").ToString
                            dtHistorial.Rows(i)("D_NRO_DOCUMENTO") = dtScripting.Rows(j)("D_NRO_DOCUMENTO").ToString
                            dtHistorial.Rows(i)("D_FEC_ACTIVACION") = dtScripting.Rows(j)("D_FEC_ACTIVACION").ToString
                            dtHistorial.Rows(i)("D_DEPARTAMENTO") = dtScripting.Rows(j)("D_DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("D_PROVINCIA") = dtScripting.Rows(j)("D_PROVINCIA").ToString
                            dtHistorial.Rows(i)("D_DISTRITO") = dtScripting.Rows(j)("D_DISTRITO").ToString
                            dtHistorial.Rows(i)("D_DIRECCION") = dtScripting.Rows(j)("D_DIRECCION").ToString
                            dtHistorial.Rows(i)("D_CICLO_FACTURACION") = dtScripting.Rows(j)("D_CICLO_FACTURACION").ToString
                            dtHistorial.Rows(i)("D_TIPO_DOC_EMITIDO") = dtScripting.Rows(j)("D_TIPO_DOC_EMITIDO").ToString
                            dtHistorial.Rows(i)("D_RECIBO") = dtScripting.Rows(j)("D_RECIBO").ToString
                            dtHistorial.Rows(i)("D_FEC_EMISION") = dtScripting.Rows(j)("D_FEC_EMISION").ToString
                            dtHistorial.Rows(i)("D_FEC_VENCIMIENTO") = dtScripting.Rows(j)("D_FEC_VENCIMIENTO").ToString
                            dtHistorial.Rows(i)("D_MONTO_RECIBO") = dtScripting.Rows(j)("D_MONTO_RECIBO").ToString
                            dtHistorial.Rows(i)("D_FEC_ASIGNACION") = dtScripting.Rows(j)("D_FEC_ASIGNACION").ToString
                            dtHistorial.Rows(i)("D_PLAN_TARIFARIO") = dtScripting.Rows(j)("D_PLAN_TARIFARIO").ToString
                            dtHistorial.Rows(i)("D_NRO_SERVICIO") = dtScripting.Rows(j)("D_NRO_SERVICIO").ToString
                            dtHistorial.Rows(i)("D_EST_ACT_SERVICIO") = dtScripting.Rows(j)("D_EST_ACT_SERVICIO").ToString
                            dtHistorial.Rows(i)("CBO_ES_TITULAR") = dtScripting.Rows(j)("CBO_ES_TITULAR").ToString
                            dtHistorial.Rows(i)("CBO_ACCEDE_A_LLAMADA") = dtScripting.Rows(j)("CBO_ACCEDE_A_LLAMADA").ToString
                            dtHistorial.Rows(i)("TXT_INCONVENIENTE") = dtScripting.Rows(j)("TXT_INCONVENIENTE").ToString
                            dtHistorial.Rows(i)("CBO_SE_AFILIA_X_CORREO") = dtScripting.Rows(j)("CBO_SE_AFILIA_X_CORREO").ToString
                            dtHistorial.Rows(i)("TXT_NOMBRE_COMPLETO") = dtScripting.Rows(j)("TXT_NOMBRE_COMPLETO").ToString
                            dtHistorial.Rows(i)("TXT_DNI") = dtScripting.Rows(j)("TXT_DNI").ToString
                            dtHistorial.Rows(i)("TXT_EMAIL") = dtScripting.Rows(j)("TXT_EMAIL").ToString
                            dtHistorial.Rows(i)("CBO_DOMINIO_MAIL") = dtScripting.Rows(j)("CBO_DOMINIO_MAIL").ToString
                            dtHistorial.Rows(i)("TXT_EMAIL_COMPLETO") = dtScripting.Rows(j)("TXT_EMAIL_COMPLETO").ToString
                            dtHistorial.Rows(i)("TXT_DIRECCION") = dtScripting.Rows(j)("TXT_DIRECCION").ToString
                            dtHistorial.Rows(i)("TXT_TELEF_ALTER_1") = dtScripting.Rows(j)("TXT_TELEF_ALTER_1").ToString
                            dtHistorial.Rows(i)("TXT_TELEF_ALTER_2") = dtScripting.Rows(j)("TXT_TELEF_ALTER_2").ToString
                            dtHistorial.Rows(i)("TXT_NOMBRE_TERCERO") = dtScripting.Rows(j)("TXT_NOMBRE_TERCERO").ToString
                            dtHistorial.Rows(i)("TXT_TELEF_TITULAR") = dtScripting.Rows(j)("TXT_TELEF_TITULAR").ToString
                            dtHistorial.Rows(i)("CBO_TIPO_CONTACTO") = dtScripting.Rows(j)("CBO_TIPO_CONTACTO").ToString
                            dtHistorial.Rows(i)("CBO_RESULTADO") = dtScripting.Rows(j)("CBO_RESULTADO").ToString
                            dtHistorial.Rows(i)("CBO_ESCENARIO_MOTIVO") = dtScripting.Rows(j)("CBO_ESCENARIO_MOTIVO").ToString
                            dtHistorial.Rows(i)("TXT_OBSERVACIONES") = dtScripting.Rows(j)("TXT_OBSERVACIONES").ToString
							
							dtHistorial.Rows(i)("TIPO VIA") = dtScripting.Rows(j)("TIPO VIA").ToString
							dtHistorial.Rows(i)("NOMBRE VIA") = dtScripting.Rows(j)("NOMBRE VIA").ToString
							dtHistorial.Rows(i)("NRO VIA") = dtScripting.Rows(j)("NRO VIA").ToString
							dtHistorial.Rows(i)("MANZ_BLOQ_EDIF") = dtScripting.Rows(j)("MANZ_BLOQ_EDIF").ToString
							dtHistorial.Rows(i)("NOM MANZANA") = dtScripting.Rows(j)("NOM MANZANA").ToString
							dtHistorial.Rows(i)("LOTE") = dtScripting.Rows(j)("LOTE").ToString
							dtHistorial.Rows(i)("NRO LOTE") = dtScripting.Rows(j)("NRO LOTE").ToString
							dtHistorial.Rows(i)("TIPO") = dtScripting.Rows(j)("TIPO").ToString
							dtHistorial.Rows(i)("NRO TIPO") = dtScripting.Rows(j)("NRO TIPO").ToString
							dtHistorial.Rows(i)("TIPO URB") = dtScripting.Rows(j)("TIPO URB").ToString
							dtHistorial.Rows(i)("NOMBRE URB") = dtScripting.Rows(j)("NOMBRE URB").ToString
							dtHistorial.Rows(i)("ZONA") = dtScripting.Rows(j)("ZONA").ToString
							dtHistorial.Rows(i)("NOMBRE ZONE") = dtScripting.Rows(j)("NOMBRE ZONE").ToString
							dtHistorial.Rows(i)("REFERENCIA_DETALLE") = dtScripting.Rows(j)("REFERENCIA_DETALLE").ToString
							dtHistorial.Rows(i)("DEPARTAMENTO") = dtScripting.Rows(j)("DEPARTAMENTO").ToString
							dtHistorial.Rows(i)("PROVINCIA") = dtScripting.Rows(j)("PROVINCIA").ToString
							dtHistorial.Rows(i)("DISTRITO") = dtScripting.Rows(j)("DISTRITO").ToString
                            dtHistorial.Rows(i)("LLEGO_RECIBO") = dtScripting.Rows(j)("LLEGO_RECIBO").ToString
							dtHistorial.Rows(i)("DIRECCION") = dtScripting.Rows(j)("DIRECCION").ToString
                            dtHistorial.Rows(i)("REFERENCIA") = dtScripting.Rows(j)("REFERENCIA").ToString

                        End If
                    Next
                Next

                For i = 0 To dtHistorial.Rows.Count()

                Next

                If dtHistorial.Rows.Count > 0 Then
                    Session("tablaExportar") = dtHistorial
                    Session("tablaExportar") = dtHistorial
                    grvReporte.DataSource = dtHistorial
                    grvReporte.DataBind()
                    'EXPORTAR()
                    lnkExportar.Visible = True
                    LinkButton2.Visible = True
                    lblMsg.Text = "Cantidad de registros encontrados: " & dtHistorial.Rows.Count
                End If
            Else
                lblMsg.Text = "No hay datos con parametro de busqueda"

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
		grvReporte2.DataSource = Session("tablaExportar")
        grvReporte2.DataBind()
		
        Dim file As String = "Gestion_Preventiva_" & Now.ToString("yyyyMMddHHmmss")
        For i As Integer = 0 To grvReporte2.Rows.Count - 1
            Dim row As GridViewRow = grvReporte2.Rows(i)
            ''Apply text style to each Row
            row.Cells(7).Attributes.Add("class", "textmode")
            row.Cells(13).Attributes.Add("class", "textmode")
            row.Cells(14).Attributes.Add("class", "textmode")
            row.Cells(17).Attributes.Add("class", "textmode")
			row.Cells(25).Attributes.Add("class", "textmode")
			row.Cells(31).Attributes.Add("class", "textmode")
			row.Cells(50).Attributes.Add("class", "textmode")
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
