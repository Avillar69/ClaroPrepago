Imports System.Data
Imports System.IO

Partial Class rep_re_general_migraciones
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO
    Dim be2 As New BE_CLARO_MIGRA

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtInicio.Text = Now.ToString("yyyy-MM-dd")
            txtFin.Text = Now.ToString("yyyy-MM-dd")
        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        'lnkExportar.Visible = False
        lnkExpoExcel.Visible = False
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_MIGRACIONES_GENERAL_VENTAS(be)
            If dtHistorial.Rows.Count > 0 Then

                'dtHistorial.Columns.Add("ID", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DESC_FINAL_BO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA EVALUACION", Type.GetType("System.String"))

                dtHistorial.Columns.Add("LOGIN CALIDAD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CODIGO CALIDAD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NOMBRES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("APELLIDOS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO DOCUMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MSISDN", Type.GetType("System.String"))
                
                dtHistorial.Columns.Add("PLAN TARIFARIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PROMEDIO RECARGAS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PLAN OFRECER 1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PLAN OFRECER 2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CALL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FEC ACTIVACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NIVEL1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NIVEL2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NIVEL3", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NIVEL4", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CARGO FIJO MENSUAL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CICLO FACTURACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NUMERO A MIGRAR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("LUGAR DE NACIMIENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA DE NACIMIENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DIRECCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DISTRITO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELEFONO DE REFERENCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PLAN POSTPAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PROVINCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CORREO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBSERVACION CALIDAD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBSERVACION AGENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBSERVACION BACKOFFICE", Type.GetType("System.String"))

                Dim dtScripting As DataTable = da.SP_LISTAR_MIGRACIONES_GENERAL()

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim


                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim
                        Dim qCode As String = dtScripting.Rows(j)("CODIGO CALIDAD").ToString.Trim

                        If IDScrip = IDhIS Then
                            'dtHistorial.Rows(i)("ID") = dtScripting.Rows(j)("ID").ToString
                            dtHistorial.Rows(i)("DESC_FINAL_BO") = dtScripting.Rows(j)("DESC_FINAL_BO").ToString
                            dtHistorial.Rows(i)("FECHA EVALUACION") = dtScripting.Rows(j)("FECHA EVALUACION").ToString

                            dtHistorial.Rows(i)("LOGIN CALIDAD") = dtScripting.Rows(j)("LOGIN CALIDAD").ToString

                            Dim dtDescCali As DataTable = da.SP_CODIGOS_PRESENCE(294)

                            For k = 0 To dtDescCali.Rows.Count - 1
                                Dim idCode As String = dtDescCali.Rows(k)("QCODE").ToString.Trim
                                If idCode = qCode Then

                                    dtHistorial.Rows(i)("CODIGO CALIDAD") = dtDescCali.Rows(k)("DESCRIPTION").ToString

                                End If
                            Next
                            dtHistorial.Rows(i)("NOMBRES") = dtScripting.Rows(j)("NOMBRES").ToString
                            dtHistorial.Rows(i)("APELLIDOS") = dtScripting.Rows(j)("APELLIDOS").ToString
                            dtHistorial.Rows(i)("NRO DOCUMENTO") = dtScripting.Rows(j)("NRO DOCUMENTO").ToString
                            dtHistorial.Rows(i)("MSISDN") = dtScripting.Rows(j)("MSISDN").ToString

                            dtHistorial.Rows(i)("PLAN TARIFARIO") = dtScripting.Rows(j)("PLAN TARIFARIO").ToString
                            dtHistorial.Rows(i)("PROMEDIO RECARGAS") = dtScripting.Rows(j)("PROMEDIO RECARGAS").ToString
                            dtHistorial.Rows(i)("PLAN OFRECER 1") = dtScripting.Rows(j)("PLAN OFRECER 1").ToString
                            dtHistorial.Rows(i)("PLAN OFRECER 2") = dtScripting.Rows(j)("PLAN OFRECER 2").ToString
                            dtHistorial.Rows(i)("CALL") = dtScripting.Rows(j)("CALL").ToString
                            dtHistorial.Rows(i)("FEC ACTIVACION") = dtScripting.Rows(j)("FEC ACTIVACION").ToString
                            dtHistorial.Rows(i)("NIVEL1") = dtScripting.Rows(j)("NIVEL1").ToString
                            dtHistorial.Rows(i)("NIVEL2") = dtScripting.Rows(j)("NIVEL2").ToString
                            dtHistorial.Rows(i)("NIVEL3") = dtScripting.Rows(j)("NIVEL3").ToString
                            dtHistorial.Rows(i)("NIVEL4") = dtScripting.Rows(j)("NIVEL4").ToString
                            dtHistorial.Rows(i)("CARGO FIJO MENSUAL") = dtScripting.Rows(j)("CARGO FIJO MENSUAL").ToString
                            dtHistorial.Rows(i)("CICLO FACTURACION") = dtScripting.Rows(j)("CICLO FACTURACION").ToString
                            dtHistorial.Rows(i)("NUMERO A MIGRAR") = dtScripting.Rows(j)("NUMERO A MIGRAR").ToString
                            dtHistorial.Rows(i)("LUGAR DE NACIMIENTO") = dtScripting.Rows(j)("LUGAR DE NACIMIENTO").ToString
                            dtHistorial.Rows(i)("FECHA DE NACIMIENTO") = dtScripting.Rows(j)("FECHA DE NACIMIENTO").ToString
                            dtHistorial.Rows(i)("DIRECCION") = dtScripting.Rows(j)("DIRECCION").ToString
                            dtHistorial.Rows(i)("DISTRITO") = dtScripting.Rows(j)("DISTRITO").ToString
                            dtHistorial.Rows(i)("TELEFONO DE REFERENCIA") = dtScripting.Rows(j)("TELEFONO DE REFERENCIA").ToString
                            dtHistorial.Rows(i)("PLAN POSTPAGO") = dtScripting.Rows(j)("PLAN POSTPAGO").ToString
                            dtHistorial.Rows(i)("DEPARTAMENTO") = dtScripting.Rows(j)("DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("PROVINCIA") = dtScripting.Rows(j)("PROVINCIA").ToString
                            dtHistorial.Rows(i)("CORREO") = dtScripting.Rows(j)("CORREO").ToString
                            dtHistorial.Rows(i)("OBSERVACION CALIDAD") = dtScripting.Rows(j)("OBSERVACION CALIDAD").ToString
                            dtHistorial.Rows(i)("OBSERVACION AGENTE") = dtScripting.Rows(j)("OBSERVACION AGENTE").ToString
                            dtHistorial.Rows(i)("OBSERVACION BACKOFFICE") = dtScripting.Rows(j)("OBSERVACION BACKOFFICE").ToString

                        End If
                    Next
                Next
                grvReporte.DataSource = dtHistorial
                Session("tablaExportar") = dtHistorial
                grvReporte.DataBind()
                'lnkExportar.Visible = True
                lnkExpoExcel.Visible = True
                lblMsg.Text = "Cantidad de registros encontrados: " & dtHistorial.Rows.Count
            Else
                lblMsg.Text = "No hay datos con parametro de busqueda"

            End If
        Catch ex As Exception
            lblMsg.Text = "btnBuscar " & ex.Message
        End Try
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
        Dim mes As String = Request("Mes")
        Dim anio As String = Request("Ano")
        Dim nombre As String = "RetencionesOut"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=ReporteRenovaciones.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("FECHAHORA| IDBASE| IDAGENTE| NCONTACTOS| ID| IDCALLCENTER| CALLCENTER| MSISDN| IDCAMPANA| IDTIPIFICACION| NIVEL1| NIVEL2| NIVEL2| COMENTARIO")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "FECHAHORA" Then campo = campo & " |"
                If cabecera = "IDBASE" Then campo = campo & " |"
                If cabecera = "IDAGENTE" Then campo = campo & " |"
                If cabecera = "NCONTACTOS" Then campo = campo & " |"
                If cabecera = "ID" Then campo = campo & " |"
                If cabecera = "IDCALLCENTER" Then campo = campo & " |"
                If cabecera = "CALLCENTER" Then campo = campo & " |"
                If cabecera = "MSISDN" Then campo = campo & " |"
                If cabecera = "IDCAMPANA" Then campo = campo & " |"
                If cabecera = "IDTIPIFICACION" Then campo = campo & " |"
                If cabecera = "NIVEL1" Then campo = campo & " |"
                If cabecera = "NIVEL2" Then campo = campo & " |"
                If cabecera = "NIVEL3" Then campo = campo & " |"
                If cabecera = "COMENTARIO" Then campo = campo & " |"

                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Tipif_CALL_06_" & Now.ToString("yyyyMMdd") & ".txt")
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
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
    End Sub

    Protected Sub lnkExpoExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExpoExcel.Click
        If grvReporte.Rows.Count > 0 Then
            EXPORTAR()
        End If
    End Sub

End Class
