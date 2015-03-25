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

            If CBO_SERVICIO.SelectedIndex = 1 Then
                be.servicio = "176"

            ElseIf CBO_SERVICIO.SelectedIndex = 2 Then
                be.servicio = "191"

            Else
                be.servicio = ""

            End If

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_ONTOP_PREVENTIVA(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("D_IT", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_SERVICE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CODIGO_ID", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CODIGO_BSCS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_RAZON_SOCIAL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_RUC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_ETAPA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TRAMO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_PROVINCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DISTRITO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_IDFAC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_ESTADO_DOC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TIPO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DEBITO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NRO_DOCUMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_EMISION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_VENCIMIENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_ANTIGUEDAD_CTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_ANTIGUERDAD_DOC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MONEDA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MONTO_FAC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_SALDO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_SALDO_SOLES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MONTO_DISPUTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CLIENTE_TOP", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TRAMO_X_DOC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEF1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEF2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_RESULTADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPIFICACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MOTIVO_NO_PAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ESCENARIOS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_COMPROMISO_PAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OBS_AGENTE", Type.GetType("System.String"))



                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_ONTOP_PREVENTIVA(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then
                            dtHistorial.Rows(i)("D_IT") = dtScripting.Rows(j)("D_IT").ToString
                            dtHistorial.Rows(i)("D_SERVICE") = dtScripting.Rows(j)("D_SERVICE").ToString
                            dtHistorial.Rows(i)("D_CODIGO_ID") = dtScripting.Rows(j)("D_CODIGO_ID").ToString
                            dtHistorial.Rows(i)("D_CODIGO_BSCS") = dtScripting.Rows(j)("D_CODIGO_BSCS").ToString
                            dtHistorial.Rows(i)("D_RAZON_SOCIAL") = dtScripting.Rows(j)("D_RAZON_SOCIAL").ToString
                            dtHistorial.Rows(i)("D_RUC") = dtScripting.Rows(j)("D_RUC").ToString
                            dtHistorial.Rows(i)("D_ETAPA") = dtScripting.Rows(j)("D_ETAPA").ToString
                            dtHistorial.Rows(i)("D_TRAMO") = dtScripting.Rows(j)("D_TRAMO").ToString
                            dtHistorial.Rows(i)("D_DEPARTAMENTO") = dtScripting.Rows(j)("D_DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("D_PROVINCIA") = dtScripting.Rows(j)("D_PROVINCIA").ToString
                            dtHistorial.Rows(i)("D_DISTRITO") = dtScripting.Rows(j)("D_DISTRITO").ToString
                            dtHistorial.Rows(i)("D_IDFAC") = dtScripting.Rows(j)("D_IDFAC").ToString
                            dtHistorial.Rows(i)("D_ESTADO_DOC") = dtScripting.Rows(j)("D_ESTADO_DOC").ToString
                            dtHistorial.Rows(i)("D_TIPO") = dtScripting.Rows(j)("D_TIPO").ToString
                            dtHistorial.Rows(i)("D_DEBITO") = dtScripting.Rows(j)("D_DEBITO").ToString
                            dtHistorial.Rows(i)("D_NRO_DOCUMENTO") = dtScripting.Rows(j)("D_NRO_DOCUMENTO").ToString
                            dtHistorial.Rows(i)("D_EMISION") = dtScripting.Rows(j)("D_EMISION").ToString
                            dtHistorial.Rows(i)("D_VENCIMIENTO") = dtScripting.Rows(j)("D_VENCIMIENTO").ToString
                            dtHistorial.Rows(i)("D_ANTIGUEDAD_CTA") = dtScripting.Rows(j)("D_ANTIGUEDAD_CTA").ToString
                            dtHistorial.Rows(i)("D_ANTIGUERDAD_DOC") = dtScripting.Rows(j)("D_ANTIGUERDAD_DOC").ToString
                            dtHistorial.Rows(i)("D_MONEDA") = dtScripting.Rows(j)("D_MONEDA").ToString
                            dtHistorial.Rows(i)("D_MONTO_FAC") = dtScripting.Rows(j)("D_MONTO_FAC").ToString
                            dtHistorial.Rows(i)("D_SALDO") = dtScripting.Rows(j)("D_SALDO").ToString
                            dtHistorial.Rows(i)("D_SALDO_SOLES") = dtScripting.Rows(j)("D_SALDO_SOLES").ToString
                            dtHistorial.Rows(i)("D_MONTO_DISPUTA") = dtScripting.Rows(j)("D_MONTO_DISPUTA").ToString
                            dtHistorial.Rows(i)("D_CLIENTE_TOP") = dtScripting.Rows(j)("D_CLIENTE_TOP").ToString
                            dtHistorial.Rows(i)("D_TRAMO_X_DOC") = dtScripting.Rows(j)("D_TRAMO_X_DOC").ToString
                            dtHistorial.Rows(i)("D_TELEF1") = dtScripting.Rows(j)("D_TELEF1").ToString
                            dtHistorial.Rows(i)("D_TELEF2") = dtScripting.Rows(j)("D_TELEF2").ToString
                            dtHistorial.Rows(i)("CBO_RESULTADO") = dtScripting.Rows(j)("CBO_RESULTADO").ToString
                            dtHistorial.Rows(i)("CBO_TIPIFICACION") = dtScripting.Rows(j)("CBO_TIPIFICACION").ToString
                            dtHistorial.Rows(i)("CBO_MOTIVO_NO_PAGO") = dtScripting.Rows(j)("CBO_MOTIVO_NO_PAGO").ToString
                            dtHistorial.Rows(i)("CBO_ESCENARIOS") = dtScripting.Rows(j)("CBO_ESCENARIOS").ToString
                            dtHistorial.Rows(i)("DTP_COMPROMISO_PAGO") = dtScripting.Rows(j)("DTP_COMPROMISO_PAGO").ToString
                            dtHistorial.Rows(i)("TXT_OBS_AGENTE") = dtScripting.Rows(j)("TXT_OBS_AGENTE").ToString

                        End If
                    Next
                Next
                If dtHistorial.Rows.Count > 0 Then
                    grvReporte.DataSource = dtHistorial
                    Session("tablaExportar") = dtHistorial
                    grvReporte.DataBind()
                    'EXPORTAR()
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
        Dim nombre As String = "Claro Ontop Preventiva"
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
