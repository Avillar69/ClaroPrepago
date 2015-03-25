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

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_CANCELACIONES(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("RUC DNI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELEFONO BASE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PLAN TARIFARIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ACCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA EXPIRACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MOTIVO CANCELCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CICLO", Type.GetType("System.String"))
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
                dtHistorial.Columns.Add("NOMBRES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_TELEFONO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MOTIVO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO_RETENCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MONTO AJUSTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PLANES TARIFARIOS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO PLAN", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CARTA PLAN", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBS OBLIGATORIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBS_AGENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MOTIVO2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PROB SERV", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MALA ATENCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DESEA MAS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PROB TEC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OTRO OPERADOR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PRECIOS ALTOS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OFRECER PARA PERMANECER", Type.GetType("System.String"))

                Dim dtScripting As DataTable = da.SP_LISTAR_CANCELACIONES()

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("RUC DNI") = dtScripting.Rows(j)("RUC DNI").ToString
                            dtHistorial.Rows(i)("TELEFONO BASE") = dtScripting.Rows(j)("TELEFONO BASE").ToString
                            dtHistorial.Rows(i)("PLAN TARIFARIO") = dtScripting.Rows(j)("PLAN TARIFARIO").ToString
                            dtHistorial.Rows(i)("ACCION") = dtScripting.Rows(j)("ACCION").ToString
                            dtHistorial.Rows(i)("TIPO CLIENTE") = dtScripting.Rows(j)("TIPO CLIENTE").ToString
                            dtHistorial.Rows(i)("FECHA EXPIRACION") = dtScripting.Rows(j)("FECHA EXPIRACION").ToString
                            dtHistorial.Rows(i)("MOTIVO CANCELCION") = dtScripting.Rows(j)("MOTIVO CANCELCION").ToString
                            dtHistorial.Rows(i)("CICLO") = dtScripting.Rows(j)("CICLO").ToString
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
                            dtHistorial.Rows(i)("NOMBRES") = dtScripting.Rows(j)("NOMBRES").ToString
                            dtHistorial.Rows(i)("TXT_TELEFONO") = dtScripting.Rows(j)("TXT_TELEFONO").ToString
                            dtHistorial.Rows(i)("MOTIVO") = dtScripting.Rows(j)("MOTIVO").ToString
                            dtHistorial.Rows(i)("MONTO AJUSTE") = dtScripting.Rows(j)("MONTO AJUSTE").ToString
                            dtHistorial.Rows(i)("PLANES TARIFARIOS") = dtScripting.Rows(j)("PLANES TARIFARIOS").ToString
                            dtHistorial.Rows(i)("TIPO PLAN") = dtScripting.Rows(j)("TIPO PLAN").ToString
                            dtHistorial.Rows(i)("CARTA PLAN") = dtScripting.Rows(j)("CARTA PLAN").ToString
                            dtHistorial.Rows(i)("OBS OBLIGATORIA") = dtScripting.Rows(j)("OBS OBLIGATORIA").ToString
                            dtHistorial.Rows(i)("OBS_AGENTE") = dtScripting.Rows(j)("OBS_AGENTE").ToString
                            dtHistorial.Rows(i)("MOTIVO2") = dtScripting.Rows(j)("MOTIVO2").ToString
                            dtHistorial.Rows(i)("PROB SERV") = dtScripting.Rows(j)("PROB SERV").ToString
                            dtHistorial.Rows(i)("MALA ATENCION") = dtScripting.Rows(j)("MALA ATENCION").ToString
                            dtHistorial.Rows(i)("DESEA MAS") = dtScripting.Rows(j)("DESEA MAS").ToString
                            dtHistorial.Rows(i)("PROB TEC") = dtScripting.Rows(j)("PROB TEC").ToString
                            dtHistorial.Rows(i)("OTRO OPERADOR") = dtScripting.Rows(j)("OTRO OPERADOR").ToString
                            dtHistorial.Rows(i)("PRECIOS ALTOS") = dtScripting.Rows(j)("PRECIOS ALTOS").ToString
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
