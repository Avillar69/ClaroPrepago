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
        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        lnkExportar.Visible = False
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_TV_RETENCIONES(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("LLAMADA DEL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("INTENTO LLAMADA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO LILNEA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MOTIVO BAJA PREPAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MOTIVO BAJA POSTPAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELEF MINUTOS SALVAVIDAS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP CODIGO CLI SGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP NRO INCIDENCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP TELEF CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP COD RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP FEC ACTIVACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP OPCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP TRASLADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP ACTIV CANALES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP INST DECO DIGI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP INST DECO HD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP ATEN TECNICA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP CAMBIO TITULAR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP REPOR EQUIPO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PREP OBSERVACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST NRO SERV SIAC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST NRO ITERACCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST TELEF CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST FEC ACTIVACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST DSCTO 50", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST DSCTO MOVIE CITY", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST DSCTO HBO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST ATEN TECNICA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST CAMBIO TITULAR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST REPO EQUIPO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST DESCTO OCC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST MAS 6 MESES 1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST MAS 6 MESES 2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("POST OBSERVACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ATEN PRIO COD CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ATEN PRIO NOMBRE CLI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ATEN PRIO DESCIO PROB", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ATEN PRIO DIRECCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ATEN PRIO REFERENCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ATEN PRIO TELEF CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ATEN PRIO PERSONA CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ATEN PRIO HORARIO VISITA", Type.GetType("System.String"))

                Dim dtScripting As DataTable = da.SP_LISTAR_TV_RETENCIONES()

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then
                            dtHistorial.Rows(i)("LLAMADA DEL") = dtScripting.Rows(j)("LLAMADA DEL").ToString
                            dtHistorial.Rows(i)("INTENTO LLAMADA") = dtScripting.Rows(j)("INTENTO LLAMADA").ToString
                            dtHistorial.Rows(i)("CLIENTE") = dtScripting.Rows(j)("CLIENTE").ToString
                            dtHistorial.Rows(i)("TIPO LILNEA") = dtScripting.Rows(j)("TIPO LILNEA").ToString
                            dtHistorial.Rows(i)("MOTIVO BAJA PREPAGO") = dtScripting.Rows(j)("MOTIVO BAJA PREPAGO").ToString
                            dtHistorial.Rows(i)("MOTIVO BAJA POSTPAGO") = dtScripting.Rows(j)("MOTIVO BAJA POSTPAGO").ToString
                            dtHistorial.Rows(i)("TELEF MINUTOS SALVAVIDAS") = dtScripting.Rows(j)("TELEF MINUTOS SALVAVIDAS").ToString
                            dtHistorial.Rows(i)("PREP CODIGO CLI SGA") = dtScripting.Rows(j)("PREP CODIGO CLI SGA").ToString
                            dtHistorial.Rows(i)("PREP NRO INCIDENCIA") = dtScripting.Rows(j)("PREP NRO INCIDENCIA").ToString
                            dtHistorial.Rows(i)("PREP DEPARTAMENTO") = dtScripting.Rows(j)("PREP DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("PREP TELEF CONTACTO") = dtScripting.Rows(j)("PREP TELEF CONTACTO").ToString
                            dtHistorial.Rows(i)("PREP COD RECARGA") = dtScripting.Rows(j)("PREP COD RECARGA").ToString
                            dtHistorial.Rows(i)("PREP FEC ACTIVACION") = dtScripting.Rows(j)("PREP FEC ACTIVACION").ToString
                            dtHistorial.Rows(i)("PREP OPCION") = dtScripting.Rows(j)("PREP OPCION").ToString
                            dtHistorial.Rows(i)("PREP TRASLADO") = dtScripting.Rows(j)("PREP TRASLADO").ToString
                            dtHistorial.Rows(i)("PREP ACTIV CANALES") = dtScripting.Rows(j)("PREP ACTIV CANALES").ToString
                            dtHistorial.Rows(i)("PREP INST DECO DIGI") = dtScripting.Rows(j)("PREP INST DECO DIGI").ToString
                            dtHistorial.Rows(i)("PREP INST DECO HD") = dtScripting.Rows(j)("PREP INST DECO HD").ToString
                            dtHistorial.Rows(i)("PREP ATEN TECNICA") = dtScripting.Rows(j)("PREP ATEN TECNICA").ToString
                            dtHistorial.Rows(i)("PREP CAMBIO TITULAR") = dtScripting.Rows(j)("PREP CAMBIO TITULAR").ToString
                            dtHistorial.Rows(i)("PREP REPOR EQUIPO") = dtScripting.Rows(j)("PREP REPOR EQUIPO").ToString
                            dtHistorial.Rows(i)("PREP OBSERVACION") = dtScripting.Rows(j)("PREP OBSERVACION").ToString
                            dtHistorial.Rows(i)("POST NRO SERV SIAC") = dtScripting.Rows(j)("POST NRO SERV SIAC").ToString
                            dtHistorial.Rows(i)("POST NRO ITERACCION") = dtScripting.Rows(j)("POST NRO ITERACCION").ToString
                            dtHistorial.Rows(i)("POST DEPARTAMENTO") = dtScripting.Rows(j)("POST DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("POST TELEF CONTACTO") = dtScripting.Rows(j)("POST TELEF CONTACTO").ToString
                            dtHistorial.Rows(i)("POST FEC ACTIVACION") = dtScripting.Rows(j)("POST FEC ACTIVACION").ToString
                            dtHistorial.Rows(i)("POST DSCTO 50") = dtScripting.Rows(j)("POST DSCTO 50").ToString
                            dtHistorial.Rows(i)("POST DSCTO MOVIE CITY") = dtScripting.Rows(j)("POST DSCTO MOVIE CITY").ToString
                            dtHistorial.Rows(i)("POST DSCTO HBO") = dtScripting.Rows(j)("POST DSCTO HBO").ToString
                            dtHistorial.Rows(i)("POST ATEN TECNICA") = dtScripting.Rows(j)("POST ATEN TECNICA").ToString
                            dtHistorial.Rows(i)("POST CAMBIO TITULAR") = dtScripting.Rows(j)("POST CAMBIO TITULAR").ToString
                            dtHistorial.Rows(i)("POST REPO EQUIPO") = dtScripting.Rows(j)("POST REPO EQUIPO").ToString
                            dtHistorial.Rows(i)("POST DESCTO OCC") = dtScripting.Rows(j)("POST DESCTO OCC").ToString
                            dtHistorial.Rows(i)("POST MAS 6 MESES 1") = dtScripting.Rows(j)("POST MAS 6 MESES 1").ToString
                            dtHistorial.Rows(i)("POST MAS 6 MESES 2") = dtScripting.Rows(j)("POST MAS 6 MESES 2").ToString
                            dtHistorial.Rows(i)("POST OBSERVACION") = dtScripting.Rows(j)("POST OBSERVACION").ToString
                            dtHistorial.Rows(i)("ATEN PRIO COD CLIENTE") = dtScripting.Rows(j)("ATEN PRIO COD CLIENTE").ToString
                            dtHistorial.Rows(i)("ATEN PRIO NOMBRE CLI") = dtScripting.Rows(j)("ATEN PRIO NOMBRE CLI").ToString
                            dtHistorial.Rows(i)("ATEN PRIO DESCIO PROB") = dtScripting.Rows(j)("ATEN PRIO DESCIO PROB").ToString
                            dtHistorial.Rows(i)("ATEN PRIO DIRECCION") = dtScripting.Rows(j)("ATEN PRIO DIRECCION").ToString
                            dtHistorial.Rows(i)("ATEN PRIO REFERENCIA") = dtScripting.Rows(j)("ATEN PRIO REFERENCIA").ToString
                            dtHistorial.Rows(i)("ATEN PRIO TELEF CONTACTO") = dtScripting.Rows(j)("ATEN PRIO TELEF CONTACTO").ToString
                            dtHistorial.Rows(i)("ATEN PRIO PERSONA CONTACTO") = dtScripting.Rows(j)("ATEN PRIO PERSONA CONTACTO").ToString
                            dtHistorial.Rows(i)("ATEN PRIO HORARIO VISITA") = dtScripting.Rows(j)("ATEN PRIO HORARIO VISITA").ToString

                        End If
                    Next
                Next
                grvReporte.DataSource = dtHistorial
                Session("tablaExportar") = dtHistorial
                grvReporte.DataBind()
                lnkExportar.Visible = True
                lblMsg.Text = "Cantidad de registros encontrados: " & dtHistorial.Rows.Count
            Else
                lblMsg.Text = "No hay datos con parametro de busqueda"

            End If
        Catch ex As Exception
            lblMsg.Text = "btnBuscar " & ex.Message
        End Try
    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
        EXPORTAR()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub grvReporte_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte.PageIndexChanging
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
    End Sub
End Class
