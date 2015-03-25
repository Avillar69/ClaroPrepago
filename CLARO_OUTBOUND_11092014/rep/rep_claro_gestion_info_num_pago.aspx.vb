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

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_GESTION_INFORMATIVA(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("D_CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TIPO_DOC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DOCUMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FEC_MIGRACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_COD_ANTERIOR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NUEVO_CODIGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CODIGO_PAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MONTO_PENDIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DEUDA_COD_ANTIGUO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NOMBRE_PLAN", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEF_SERVICIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DEBITO_AUTOMATICO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_SERV_TELEF_PLAN_ORIGEN", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEF_REFERENCIA_1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEF_REFERENCIA_2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FEC_ASIGNACION", Type.GetType("System.String"))

                dtHistorial.Columns.Add("CBO_ES_TITULAR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACCEDE_A_LLAMADA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NOMBRE_TERCERO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO_CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_RESULTADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ESCENARIO_MOTIVO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OBSERVACIONES", Type.GetType("System.String"))

                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_GESTION_INFORMATIVA(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then
                            dtHistorial.Rows(i)("D_CLIENTE") = dtScripting.Rows(j)("D_CLIENTE").ToString
                            dtHistorial.Rows(i)("D_TIPO_DOC") = dtScripting.Rows(j)("D_TIPO_DOC").ToString
                            dtHistorial.Rows(i)("D_DOCUMENTO") = dtScripting.Rows(j)("D_DOCUMENTO").ToString
                            dtHistorial.Rows(i)("D_FEC_MIGRACION") = dtScripting.Rows(j)("D_FEC_MIGRACION").ToString
                            dtHistorial.Rows(i)("D_COD_ANTERIOR") = dtScripting.Rows(j)("D_COD_ANTERIOR").ToString
                            dtHistorial.Rows(i)("D_NUEVO_CODIGO") = dtScripting.Rows(j)("D_NUEVO_CODIGO").ToString
                            dtHistorial.Rows(i)("D_CODIGO_PAGO") = dtScripting.Rows(j)("D_CODIGO_PAGO").ToString
                            dtHistorial.Rows(i)("D_MONTO_PENDIENTE") = dtScripting.Rows(j)("D_MONTO_PENDIENTE").ToString
                            dtHistorial.Rows(i)("D_DEUDA_COD_ANTIGUO") = dtScripting.Rows(j)("D_DEUDA_COD_ANTIGUO").ToString
                            dtHistorial.Rows(i)("D_NOMBRE_PLAN") = dtScripting.Rows(j)("D_NOMBRE_PLAN").ToString
                            dtHistorial.Rows(i)("D_TELEF_SERVICIO") = dtScripting.Rows(j)("D_TELEF_SERVICIO").ToString
                            dtHistorial.Rows(i)("D_DEBITO_AUTOMATICO") = dtScripting.Rows(j)("D_DEBITO_AUTOMATICO").ToString
                            dtHistorial.Rows(i)("D_SERV_TELEF_PLAN_ORIGEN") = dtScripting.Rows(j)("D_SERV_TELEF_PLAN_ORIGEN").ToString
                            dtHistorial.Rows(i)("D_TELEF_REFERENCIA_1") = dtScripting.Rows(j)("D_TELEF_REFERENCIA_1").ToString
                            dtHistorial.Rows(i)("D_TELEF_REFERENCIA_2") = dtScripting.Rows(j)("D_TELEF_REFERENCIA_2").ToString
                            dtHistorial.Rows(i)("D_FEC_ASIGNACION") = dtScripting.Rows(j)("D_FEC_ASIGNACION").ToString

                            dtHistorial.Rows(i)("CBO_ES_TITULAR") = dtScripting.Rows(j)("CBO_ES_TITULAR").ToString
                            dtHistorial.Rows(i)("CBO_ACCEDE_A_LLAMADA") = dtScripting.Rows(j)("CBO_ACCEDE_A_LLAMADA").ToString
                            dtHistorial.Rows(i)("TXT_NOMBRE_TERCERO") = dtScripting.Rows(j)("TXT_NOMBRE_TERCERO").ToString
                            dtHistorial.Rows(i)("CBO_TIPO_CONTACTO") = dtScripting.Rows(j)("CBO_TIPO_CONTACTO").ToString
                            dtHistorial.Rows(i)("CBO_RESULTADO") = dtScripting.Rows(j)("CBO_RESULTADO").ToString
                            dtHistorial.Rows(i)("CBO_ESCENARIO_MOTIVO") = dtScripting.Rows(j)("CBO_ESCENARIO_MOTIVO").ToString
                            dtHistorial.Rows(i)("TXT_OBSERVACIONES") = dtScripting.Rows(j)("TXT_OBSERVACIONES").ToString

                        End If
                    Next
                Next
                If dtHistorial.Rows.Count > 0 Then
                    grvReporte.DataSource = dtHistorial
                    Session("tablaExportar") = dtHistorial
                    grvReporte.DataBind()
                    lnkExportar.Visible = True
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
        Dim nombre As String = "ClaroGestionInformativa"
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
