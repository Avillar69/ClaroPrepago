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
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_DEBITO_AUTO(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("D_CELULAR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NOMBRES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_SERVICIO_CLARO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_ENTIDAD_BANCARIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TIPO_TARJETA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MONTO_TOPE_MAX", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FEC_RECHAZO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MOTIVO_RECHAZO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MONTO_RECHAZADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MONTO_TOTAL_FAC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MONTO_DEBITADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FEC_VENCIMIENTO_FAC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_EJECUTO_PAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_FEC_COMPROMISO_PAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_SE_APERSONARA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_FEC_SE_APERSONARA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MOTIVO_NO_SE_APERSONARA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_NO_CONFIRMA_FEC_PAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ENTREGA_NRO_CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NRO_CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_PROGRAMA_DIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_PROGRAMA_HORA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_DESAFILIACION_DNI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OBSERVACIONES", Type.GetType("System.String"))

                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_DEBITO_AUTO(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("D_CELULAR") = dtScripting.Rows(j)("D_CELULAR").ToString
                            dtHistorial.Rows(i)("D_NOMBRES") = dtScripting.Rows(j)("D_NOMBRES").ToString
                            dtHistorial.Rows(i)("D_SERVICIO_CLARO") = dtScripting.Rows(j)("D_SERVICIO_CLARO").ToString
                            dtHistorial.Rows(i)("D_ENTIDAD_BANCARIA") = dtScripting.Rows(j)("D_ENTIDAD_BANCARIA").ToString
                            dtHistorial.Rows(i)("D_TIPO_TARJETA") = dtScripting.Rows(j)("D_TIPO_TARJETA").ToString
                            dtHistorial.Rows(i)("D_MONTO_TOPE_MAX") = dtScripting.Rows(j)("D_MONTO_TOPE_MAX").ToString
                            dtHistorial.Rows(i)("D_FEC_RECHAZO") = dtScripting.Rows(j)("D_FEC_RECHAZO").ToString
                            dtHistorial.Rows(i)("D_MOTIVO_RECHAZO") = dtScripting.Rows(j)("D_MOTIVO_RECHAZO").ToString
                            dtHistorial.Rows(i)("D_MONTO_RECHAZADO") = dtScripting.Rows(j)("D_MONTO_RECHAZADO").ToString
                            dtHistorial.Rows(i)("D_MONTO_TOTAL_FAC") = dtScripting.Rows(j)("D_MONTO_TOTAL_FAC").ToString
                            dtHistorial.Rows(i)("D_MONTO_DEBITADO") = dtScripting.Rows(j)("D_MONTO_DEBITADO").ToString
                            dtHistorial.Rows(i)("D_FEC_VENCIMIENTO_FAC") = dtScripting.Rows(j)("D_FEC_VENCIMIENTO_FAC").ToString
                            dtHistorial.Rows(i)("CBO_EJECUTO_PAGO") = dtScripting.Rows(j)("CBO_EJECUTO_PAGO").ToString
                            dtHistorial.Rows(i)("DTP_FEC_COMPROMISO_PAGO") = dtScripting.Rows(j)("DTP_FEC_COMPROMISO_PAGO").ToString
                            dtHistorial.Rows(i)("CBO_SE_APERSONARA") = dtScripting.Rows(j)("CBO_SE_APERSONARA").ToString
                            dtHistorial.Rows(i)("DTP_FEC_SE_APERSONARA") = dtScripting.Rows(j)("DTP_FEC_SE_APERSONARA").ToString
                            dtHistorial.Rows(i)("CBO_MOTIVO_NO_SE_APERSONARA") = dtScripting.Rows(j)("CBO_MOTIVO_NO_SE_APERSONARA").ToString
                            dtHistorial.Rows(i)("CBO_NO_CONFIRMA_FEC_PAGO") = dtScripting.Rows(j)("CBO_NO_CONFIRMA_FEC_PAGO").ToString
                            dtHistorial.Rows(i)("CBO_ENTREGA_NRO_CONTACTO") = dtScripting.Rows(j)("CBO_ENTREGA_NRO_CONTACTO").ToString
                            dtHistorial.Rows(i)("TXT_NRO_CONTACTO") = dtScripting.Rows(j)("TXT_NRO_CONTACTO").ToString
                            dtHistorial.Rows(i)("DTP_PROGRAMA_DIA") = dtScripting.Rows(j)("DTP_PROGRAMA_DIA").ToString
                            dtHistorial.Rows(i)("DTP_PROGRAMA_HORA") = dtScripting.Rows(j)("DTP_PROGRAMA_HORA").ToString
                            dtHistorial.Rows(i)("TXT_DESAFILIACION_DNI") = dtScripting.Rows(j)("TXT_DESAFILIACION_DNI").ToString
                            dtHistorial.Rows(i)("TXT_OBSERVACIONES") = dtScripting.Rows(j)("TXT_OBSERVACIONES").ToString

                        End If
                    Next
                Next
                If dtHistorial.Rows.Count > 0 Then
                    Session("tablaExportar") = dtHistorial
                    Session("tablaExportar") = dtHistorial
                    grvReporte.DataSource = dtHistorial
                    grvReporte.DataBind()
                    lnkExportar.Visible = True
                    lnkExportarCsv.Visible = True
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
    Sub EXPORTAR()
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
        Dim nombre As String = "Claro_Débito_Automatico"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID_LOG, FECHA, CARGA, SERVICIO, ID, ID_FINAL, FINAL, LOGIN, TELEFONO, TALKTIME, ACWTIME, D_CELULAR, D_NOMBRES, D_SERVICIO_CLARO, D_ENTIDAD_BANCARIA, D_TIPO_TARJETA, D_MONTO_TOPE_MAX, D_FEC_RECHAZO, D_MOTIVO_RECHAZO, D_MONTO_RECHAZADO, D_MONTO_TOTAL_FAC, D_MONTO_DEBITADO, D_FEC_VENCIMIENTO_FAC, CBO_EJECUTO_PAGO, DTP_FEC_COMPROMISO_PAGO, CBO_SE_APERSONARA, DTP_FEC_SE_APERSONARA, CBO_MOTIVO_NO_SE_APERSONARA, CBO_NO_CONFIRMA_FEC_PAGO, CBO_ENTREGA_NRO_CONTACTO, TXT_NRO_CONTACTO, DTP_PROGRAMA_DIA, DTP_PROGRAMA_HORA, TXT_DESAFILIACION_DNI, TXT_OBSERVACIONES")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "ID_LOG" Then campo = campo & " ,"
                If cabecera = "FECHA" Then campo = campo & " ,"
                If cabecera = "CARGA" Then campo = campo & " ,"
                If cabecera = "SERVICIO" Then campo = campo & " ,"
                If cabecera = "ID" Then campo = campo & " ,"
                If cabecera = "ID_FINAL" Then campo = campo & " ,"
                If cabecera = "FINAL" Then campo = campo & " ,"
                If cabecera = "LOGIN" Then campo = campo & " ,"
                If cabecera = "TELEFONO" Then campo = campo & " ,"
                If cabecera = "TALKTIME" Then campo = campo & " ,"
                If cabecera = "ACWTIME" Then campo = campo & " ,"
                If cabecera = "D_CELULAR" Then campo = campo & " ,"
                If cabecera = "D_NOMBRES" Then campo = campo & " ,"
                If cabecera = "D_SERVICIO_CLARO" Then campo = campo & " ,"
                If cabecera = "D_ENTIDAD_BANCARIA" Then campo = campo & " ,"
                If cabecera = "D_TIPO_TARJETA" Then campo = campo & " ,"
                If cabecera = "D_MONTO_TOPE_MAX" Then campo = campo & " ,"
                If cabecera = "D_FEC_RECHAZO" Then campo = campo & " ,"
                If cabecera = "D_MOTIVO_RECHAZO" Then campo = campo & " ,"
                If cabecera = "D_MONTO_RECHAZADO" Then campo = campo & " ,"
                If cabecera = "D_MONTO_TOTAL_FAC" Then campo = campo & " ,"
                If cabecera = "D_MONTO_DEBITADO" Then campo = campo & " ,"
                If cabecera = "D_FEC_VENCIMIENTO_FAC" Then campo = campo & " ,"
                If cabecera = "CBO_EJECUTO_PAGO" Then campo = campo & " ,"
                If cabecera = "DTP_FEC_COMPROMISO_PAGO" Then campo = campo & " ,"
                If cabecera = "CBO_SE_APERSONARA" Then campo = campo & " ,"
                If cabecera = "DTP_FEC_SE_APERSONARA" Then campo = campo & " ,"
                If cabecera = "CBO_MOTIVO_NO_SE_APERSONARA" Then campo = campo & " ,"
                If cabecera = "CBO_NO_CONFIRMA_FEC_PAGO" Then campo = campo & " ,"
                If cabecera = "CBO_ENTREGA_NRO_CONTACTO" Then campo = campo & " ,"
                If cabecera = "TXT_NRO_CONTACTO" Then campo = campo & " ,"
                If cabecera = "DTP_PROGRAMA_DIA" Then campo = campo & " ,"
                If cabecera = "DTP_PROGRAMA_HORA" Then campo = campo & " ,"
                If cabecera = "TXT_DESAFILIACION_DNI" Then campo = campo & " ,"
                If cabecera = "TXT_OBSERVACIONES" Then campo = campo & " ,"

                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Claro_Débito_Automatico.csv")
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

    Protected Sub grvReporte_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte.PageIndexChanging
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
    End Sub

End Class
