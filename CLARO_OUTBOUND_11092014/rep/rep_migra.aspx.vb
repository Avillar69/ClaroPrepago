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

            If ddlFinales.SelectedItem.ToString = "TODAS" Then
                be.tipo = 1
            ElseIf ddlFinales.SelectedItem.ToString = "VENTAS" Then
                be.tipo = 0
            Else
                lblMsg.Text = "Debe Seleccionar los finales"
            End If

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_MIGRACIONES(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("NOMBRES COMPLETOS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("RUC DNI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELEFONO BASE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PLAN TARIFARIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CICLO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA ITERACCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CLARO PUNTOS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("IMR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DISTRITO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PROVINCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DIRECCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("SEGMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELF PREP 1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELF PREP 2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELF PREP 3", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELF PREP 4", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELF PREP 5", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELF POST 1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELF POST 2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELF POST 3", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELF POST 4", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELF POST 5", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MOTIVO", Type.GetType("System.String"))

                dtHistorial.Columns.Add("SEGMENTO CLI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO RETENCION", Type.GetType("System.String"))

                dtHistorial.Columns.Add("MONTO AJUSTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PLANES TARIFARIOS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO PLAN", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CARTA PLAN", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("RUC_DNI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBLIGATORIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBS_AGENTE", Type.GetType("System.String"))

                dtHistorial.Columns.Add("MOTIVO DE BAJA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PROBLEMAS SERVICIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MALA ATENCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DESEA MAS DE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PROBLEMAS TECNICOS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OTRO OPERADOR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PRECIOS ALTOS DE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OFRECER PARA PERMANECER", Type.GetType("System.String"))

                Dim dtScripting As DataTable = da.SP_LISTAR_MIGRACIONES()

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("NOMBRES COMPLETOS") = dtScripting.Rows(j)("NOMBRES COMPLETOS").ToString
                            dtHistorial.Rows(i)("RUC DNI") = dtScripting.Rows(j)("RUC DNI").ToString
                            dtHistorial.Rows(i)("TELEFONO BASE") = dtScripting.Rows(j)("TELEFONO BASE").ToString
                            dtHistorial.Rows(i)("PLAN TARIFARIO") = dtScripting.Rows(j)("PLAN TARIFARIO").ToString
                            dtHistorial.Rows(i)("TIPO CLIENTE") = dtScripting.Rows(j)("TIPO CLIENTE").ToString
                            dtHistorial.Rows(i)("CICLO") = dtScripting.Rows(j)("CICLO").ToString
                            dtHistorial.Rows(i)("FECHA ITERACCION") = dtScripting.Rows(j)("FECHA ITERACCION").ToString
                            dtHistorial.Rows(i)("CLARO PUNTOS") = dtScripting.Rows(j)("CLARO PUNTOS").ToString
                            dtHistorial.Rows(i)("IMR") = dtScripting.Rows(j)("IMR").ToString
                            dtHistorial.Rows(i)("DISTRITO") = dtScripting.Rows(j)("DISTRITO").ToString
                            dtHistorial.Rows(i)("PROVINCIA") = dtScripting.Rows(j)("PROVINCIA").ToString
                            dtHistorial.Rows(i)("DEPARTAMENTO") = dtScripting.Rows(j)("DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("DIRECCION") = dtScripting.Rows(j)("DIRECCION").ToString
                            dtHistorial.Rows(i)("SEGMENTO") = dtScripting.Rows(j)("SEGMENTO").ToString
                            dtHistorial.Rows(i)("TELF PREP 1") = dtScripting.Rows(j)("TELF PREP 1").ToString
                            dtHistorial.Rows(i)("TELF PREP 2") = dtScripting.Rows(j)("TELF PREP 2").ToString
                            dtHistorial.Rows(i)("TELF PREP 3") = dtScripting.Rows(j)("TELF PREP 3").ToString
                            dtHistorial.Rows(i)("TELF PREP 4") = dtScripting.Rows(j)("TELF PREP 4").ToString
                            dtHistorial.Rows(i)("TELF PREP 5") = dtScripting.Rows(j)("TELF PREP 5").ToString
                            dtHistorial.Rows(i)("TELF POST 1") = dtScripting.Rows(j)("TELF POST 1").ToString
                            dtHistorial.Rows(i)("TELF POST 2") = dtScripting.Rows(j)("TELF POST 2").ToString
                            dtHistorial.Rows(i)("TELF POST 3") = dtScripting.Rows(j)("TELF POST 3").ToString
                            dtHistorial.Rows(i)("TELF POST 4") = dtScripting.Rows(j)("TELF POST 4").ToString
                            dtHistorial.Rows(i)("TELF POST 5") = dtScripting.Rows(j)("TELF POST 5").ToString
                            dtHistorial.Rows(i)("MOTIVO") = dtScripting.Rows(j)("MOTIVO").ToString
                            dtHistorial.Rows(i)("SEGMENTO CLI") = dtScripting.Rows(j)("SEGMENTO CLIENTE").ToString
                            dtHistorial.Rows(i)("TIPO RETENCION") = dtScripting.Rows(j)("TIPO RETENCION").ToString
                            dtHistorial.Rows(i)("MONTO AJUSTE") = dtScripting.Rows(j)("TXT_MONTO_AJUSTE").ToString
                            dtHistorial.Rows(i)("PLANES TARIFARIOS") = dtScripting.Rows(j)("CBO_PLANES_TARIFARIOS").ToString
                            dtHistorial.Rows(i)("TIPO PLAN") = dtScripting.Rows(j)("CBO_TIPO_PLAN").ToString
                            dtHistorial.Rows(i)("CARTA PLAN") = dtScripting.Rows(j)("TXT_CARTA_PLAN").ToString
                            dtHistorial.Rows(i)("CLIENTE") = dtScripting.Rows(j)("TXT_CLIENTE").ToString
                            dtHistorial.Rows(i)("RUC_DNI") = dtScripting.Rows(j)("TXT_RUC_DNI").ToString
                            dtHistorial.Rows(i)("OBLIGATORIO") = dtScripting.Rows(j)("OBS OBLIGATORIA").ToString
                            dtHistorial.Rows(i)("OBS_AGENTE") = dtScripting.Rows(j)("OBS_AGENTE").ToString
                            dtHistorial.Rows(i)("MOTIVO DE BAJA") = dtScripting.Rows(j)("MOTIVO DE BAJA").ToString
                            dtHistorial.Rows(i)("PROBLEMAS SERVICIO") = dtScripting.Rows(j)("PROBLEMAS SERVICIO").ToString
                            dtHistorial.Rows(i)("MALA ATENCION") = dtScripting.Rows(j)("MALA ATENCION").ToString
                            dtHistorial.Rows(i)("DESEA MAS DE") = dtScripting.Rows(j)("DESEA MAS DE").ToString
                            dtHistorial.Rows(i)("PROBLEMAS TECNICOS") = dtScripting.Rows(j)("PROBLEMAS TECNICOS").ToString
                            dtHistorial.Rows(i)("OTRO OPERADOR") = dtScripting.Rows(j)("OTRO OPERADOR").ToString
                            dtHistorial.Rows(i)("PRECIOS ALTOS DE") = dtScripting.Rows(j)("PRECIOS ALTOS DE").ToString
                            dtHistorial.Rows(i)("OFRECER PARA PERMANECER") = dtScripting.Rows(j)("OFRECER PARA PERMANECER").ToString

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
        Dim nombre As String = "Dynamicall"
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
