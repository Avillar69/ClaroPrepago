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
        lnkExportar2.Visible = False
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
		Dim dtScripting As New DataTable
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            dtScripting = da.SP_LISTAR_CLARO_SEG_CLIE_ESP(be)

            If dtScripting.Rows.Count > 0 Then
                Session("tablaExportar") = dtScripting
                grvReporte.DataSource = dtScripting
                grvReporte.DataBind()
                'EXPORTAR()
                lnkExportar2.Visible = True
                lblMsg.Text = "TOTAL DE REGISTROS : " & dtScripting.Rows.Count
                lblMsg.CssClass = "alert alert-success"
            Else
                lnkExportar2.Visible = False
                lblMsg.Text = "SIN REGISTROS VALIDOS PARA LAS FECHAS EN BUSQUEDA"
                lblMsg.CssClass = "alert alert-danger"
            End If
        Catch ex As Exception
            'lblMsg.Text = "[ERROR #500 ####] : " & ex.Message
			'lblMsg.Text = "[ERROR #500 ####] : " & ex.Messge & dtScripting.Rows(0)(0);
            lblMsg.CssClass = "alert alert-danger"
        End Try
    End Sub

    'Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
    '    If grvReporte.Rows.Count > 0 Then
    '        EXPORTAR()
    '    End If
    'End Sub
    Sub EXPORTAR1()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        'grvExport.DataSource = Nothing
        'grvExport.DataBind()
        'grvExport.EnableViewState = False

        grvExport.DataSource = Nothing
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
        Dim nombre As String = "Seguimiento_Clientes_Esp_" & Now.ToString("yyyyMMddHHmmss")
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Sub EXPORTAR2()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        grvExport.DataSource = Nothing
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
        Dim nombre As String = "Seguimiento_Clientes_Esp_" & Now.ToString("yyyyMMddHHmmss")
        'Response.ContentType = "application/vnd.ms-excel"
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Sub ExportarTxt(ByVal tipo As Integer)
        Dim nombre As String
        Dim dt As DataTable
        If Tipo = 1 Then
            dt = Session("tablaExportar")
            nombre = "Detalle_3_Play_DTH_" & Now.ToString("yyyyMMddHHmmss")
        Else
            dt = Session("tablaCarga")
            nombre = "Unico_3_Play_DTH_" & Now.ToString("yyyyMMddHHmmss")
        End If

        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID_LOG,FECHA,LOADID,ID,ID_FINAL,FINAL,LOGIN,TELEFONO,TALKTIME,ACWTIME,D_SEMANA,D_COD_CLIENTE,D_NOMBRE_CLI,D_FEC_NAC,D_NRO_DOCUMENTO,D_DIRECCION,D_DISTRITO,D_PROVINCIA,D_DEPARTAMENTO,D_TELEFONO_01,D_TELEFONO_02,D_TELEFONO_03,D_TELEFONO_04,D_TELEFONO_05,D_CAMPANIA,D_CF_TOTAL,D_NRO_SOT,D_FEC_ACTIVACION,D_MATERIAL_DES,D_PLAN_TARIFARIO,CBO_PRODUCTO,CBO_TIPO,CBO_TIPO_LLAMADA,CBO_SUBTIPO_LLAMADA,CBO_PROB_TECNICO,CBO_NO_CONFORME,CBO_INFORMACION,CBO_SOLICITUD_PEN,CBO_TV,CBO_RECIBO,CBO_ESTADO_PAGO,CBO_ESTADO_RECIBO,TXT_CORREO,CBO_AFILIA_RECIBO_ELEC,TXT_PORQUE_NO,CBO_REALIZA_DESCARTE_ADM,CBO_DETECTA_PROB_ADM,CBO_NO_DETEC_PROB_ADM,CBO_FRENTE_A_EQ,CBO_NO_FRENTE_A_EQ,TXT_CODIGO,TXT_NOM_CLI,TXT_NRO_NOM_CONTACTO,CBO_SERVI_AFECTADO,TXT_CAMBIO_EQ,TXT_HORARIO_LLAMADA,DTP_FEC_INSTALACION,TXT_OBSERVACION,TXT_PROB_TEC_CORREO")
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
                'Dim cabecera As String = dt.Columns(j).ColumnName.ToString
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
                If cabecera = "D_FEC_NAC" Then campo = campo & " ,"
                If cabecera = "D_NRO_DOCUMENTO" Then campo = campo & " ,"
                If cabecera = "D_DIRECCION" Then campo = campo & " ,"
                If cabecera = "D_DISTRITO" Then campo = campo & " ,"
                If cabecera = "D_PROVINCIA" Then campo = campo & " ,"
                If cabecera = "D_DEPARTAMENTO" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_01" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_02" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_03" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_04" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_05" Then campo = campo & " ,"
                If cabecera = "D_CAMPANIA" Then campo = campo & " ,"
                If cabecera = "D_CF_TOTAL" Then campo = campo & " ,"
                If cabecera = "D_NRO_SOT" Then campo = campo & " ,"
                If cabecera = "D_FEC_ACTIVACION" Then campo = campo & " ,"
                If cabecera = "D_MATERIAL_DES" Then campo = campo & " ,"
                If cabecera = "D_PLAN_TARIFARIO" Then campo = campo & " ,"
                If cabecera = "CBO_PRODUCTO" Then campo = campo & " ,"
                If cabecera = "CBO_TIPO" Then campo = campo & " ,"
                If cabecera = "CBO_TIPO_LLAMADA" Then campo = campo & " ,"
                If cabecera = "CBO_SUBTIPO_LLAMADA" Then campo = campo & " ,"
                If cabecera = "CBO_PROB_TECNICO" Then campo = campo & " ,"
                If cabecera = "CBO_NO_CONFORME" Then campo = campo & " ,"
                If cabecera = "CBO_INFORMACION" Then campo = campo & " ,"
                If cabecera = "CBO_SOLICITUD_PEN" Then campo = campo & " ,"
                If cabecera = "CBO_TV" Then campo = campo & " ,"
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
        Response.AddHeader("content-disposition", "attachment;filename= " & nombre & ".csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()

    End Sub

    Protected Sub grvMostrar_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte.PageIndexChanging
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
    End Sub


    Protected Sub ExportToExcel2(sender As Object, e As System.EventArgs) Handles lnkExportar2.Click
        EXPORTAR1()
    End Sub
End Class
