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

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_DTH(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("SERVICIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CODE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ID_CARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NOMBRE_BASE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ESTACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA_LLAMADA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA_VENTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA_EVALUACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBS_AGENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBS_CALIDAD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("LOGIN_VENTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("LOGIN_CALIDAD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("VECES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("GESTIONADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("BLOQUEO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("VS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHAALTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO_SERVICIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("SEGMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ESTADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO_CUENTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NOMBRES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("RUC_DNI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DIRECCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DISTRITO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PROVINCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELEF_REFERENCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PROM_FACTURACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DESC_CAMPANA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PLAN_TARIFARIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CREACION_CONTRATO_SISAC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DESC_PLAZO_ACUERDO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO_FACT_EMITIDAS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TOTAL_DEUDA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MONTO_PEND", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CANTDIASMOROSO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MONTO_VENC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO_RECIBOS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_MOTIVO_MOROSIDAD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_OFERTA_SI_PAGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_PROBLEMA_TECNICO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_COMPROMISO_PAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_SINO_SE_SOLUCIONO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_CICLO_FACT", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_PERIODO_FACT", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_TIENE_CORREO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_DIREC_NO_COINCIDE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_ACT_DIRECCION", Type.GetType("System.String"))

                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_DTH(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("SERVICIO") = dtScripting.Rows(j)("SERVICIO").ToString
                            dtHistorial.Rows(i)("CODE") = dtScripting.Rows(j)("CODE").ToString
                            dtHistorial.Rows(i)("ID_CARGA") = dtScripting.Rows(j)("ID_CARGA").ToString
                            dtHistorial.Rows(i)("NOMBRE_BASE") = dtScripting.Rows(j)("NOMBRE_BASE").ToString
                            dtHistorial.Rows(i)("ESTACION") = dtScripting.Rows(j)("ESTACION").ToString
                            dtHistorial.Rows(i)("FECHA_LLAMADA") = dtScripting.Rows(j)("FECHA_LLAMADA").ToString
                            dtHistorial.Rows(i)("FECHA_VENTA") = dtScripting.Rows(j)("FECHA_VENTA").ToString
                            dtHistorial.Rows(i)("FECHA_EVALUACION") = dtScripting.Rows(j)("FECHA_EVALUACION").ToString
                            dtHistorial.Rows(i)("OBS_AGENTE") = dtScripting.Rows(j)("OBS_AGENTE").ToString
                            dtHistorial.Rows(i)("OBS_CALIDAD") = dtScripting.Rows(j)("OBS_CALIDAD").ToString
                            dtHistorial.Rows(i)("LOGIN_VENTA") = dtScripting.Rows(j)("LOGIN_VENTA").ToString
                            dtHistorial.Rows(i)("LOGIN_CALIDAD") = dtScripting.Rows(j)("LOGIN_CALIDAD").ToString
                            dtHistorial.Rows(i)("VECES") = dtScripting.Rows(j)("VECES").ToString
                            dtHistorial.Rows(i)("GESTIONADO") = dtScripting.Rows(j)("GESTIONADO").ToString
                            dtHistorial.Rows(i)("BLOQUEO") = dtScripting.Rows(j)("BLOQUEO").ToString
                            dtHistorial.Rows(i)("VS") = dtScripting.Rows(j)("VS").ToString
                            dtHistorial.Rows(i)("FECHAALTA") = dtScripting.Rows(j)("FECHAALTA").ToString
                            dtHistorial.Rows(i)("NRO_SERVICIO") = dtScripting.Rows(j)("NRO_SERVICIO").ToString
                            dtHistorial.Rows(i)("SEGMENTO") = dtScripting.Rows(j)("SEGMENTO").ToString
                            dtHistorial.Rows(i)("ESTADO") = dtScripting.Rows(j)("ESTADO").ToString
                            dtHistorial.Rows(i)("NRO_CUENTA") = dtScripting.Rows(j)("NRO_CUENTA").ToString
                            dtHistorial.Rows(i)("NOMBRES") = dtScripting.Rows(j)("NOMBRES").ToString
                            dtHistorial.Rows(i)("RUC_DNI") = dtScripting.Rows(j)("RUC_DNI").ToString
                            dtHistorial.Rows(i)("DIRECCION") = dtScripting.Rows(j)("DIRECCION").ToString
                            dtHistorial.Rows(i)("DISTRITO") = dtScripting.Rows(j)("DISTRITO").ToString
                            dtHistorial.Rows(i)("PROVINCIA") = dtScripting.Rows(j)("PROVINCIA").ToString
                            dtHistorial.Rows(i)("DEPARTAMENTO") = dtScripting.Rows(j)("DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("TELEF_REFERENCIA") = dtScripting.Rows(j)("TELEF_REFERENCIA").ToString
                            dtHistorial.Rows(i)("PROM_FACTURACION") = dtScripting.Rows(j)("PROM_FACTURACION").ToString
                            dtHistorial.Rows(i)("DESC_CAMPANA") = dtScripting.Rows(j)("DESC_CAMPANA").ToString
                            dtHistorial.Rows(i)("PLAN_TARIFARIO") = dtScripting.Rows(j)("PLAN_TARIFARIO").ToString
                            dtHistorial.Rows(i)("CREACION_CONTRATO_SISAC") = dtScripting.Rows(j)("CREACION_CONTRATO_SISAC").ToString
                            dtHistorial.Rows(i)("DESC_PLAZO_ACUERDO") = dtScripting.Rows(j)("DESC_PLAZO_ACUERDO").ToString
                            dtHistorial.Rows(i)("NRO_FACT_EMITIDAS") = dtScripting.Rows(j)("NRO_FACT_EMITIDAS").ToString
                            dtHistorial.Rows(i)("TOTAL_DEUDA") = dtScripting.Rows(j)("TOTAL_DEUDA").ToString
                            dtHistorial.Rows(i)("MONTO_PEND") = dtScripting.Rows(j)("MONTO_PEND").ToString
                            dtHistorial.Rows(i)("CANTDIASMOROSO") = dtScripting.Rows(j)("CANTDIASMOROSO").ToString
                            dtHistorial.Rows(i)("MONTO_VENC") = dtScripting.Rows(j)("MONTO_VENC").ToString
                            dtHistorial.Rows(i)("NRO_RECIBOS") = dtScripting.Rows(j)("NRO_RECIBOS").ToString
                            dtHistorial.Rows(i)("CBO_ACT_MOTIVO_MOROSIDAD") = dtScripting.Rows(j)("CBO_ACT_MOTIVO_MOROSIDAD").ToString
                            dtHistorial.Rows(i)("CBO_ACT_OFERTA_SI_PAGA") = dtScripting.Rows(j)("CBO_ACT_OFERTA_SI_PAGA").ToString
                            dtHistorial.Rows(i)("CBO_ACT_PROBLEMA_TECNICO") = dtScripting.Rows(j)("CBO_ACT_PROBLEMA_TECNICO").ToString
                            dtHistorial.Rows(i)("DTP_COMPROMISO_PAGO") = dtScripting.Rows(j)("DTP_COMPROMISO_PAGO").ToString
                            dtHistorial.Rows(i)("CBO_ACT_SINO_SE_SOLUCIONO") = dtScripting.Rows(j)("CBO_ACT_SINO_SE_SOLUCIONO").ToString
                            dtHistorial.Rows(i)("CBO_ACT_CICLO_FACT") = dtScripting.Rows(j)("CBO_ACT_CICLO_FACT").ToString
                            dtHistorial.Rows(i)("CBO_ACT_PERIODO_FACT") = dtScripting.Rows(j)("CBO_ACT_PERIODO_FACT").ToString
                            dtHistorial.Rows(i)("CBO_ACT_TIENE_CORREO") = dtScripting.Rows(j)("CBO_ACT_TIENE_CORREO").ToString
                            dtHistorial.Rows(i)("CBO_ACT_DIREC_NO_COINCIDE") = dtScripting.Rows(j)("CBO_ACT_DIREC_NO_COINCIDE").ToString
                            dtHistorial.Rows(i)("TXT_ACT_DIRECCION") = dtScripting.Rows(j)("TXT_ACT_DIRECCION").ToString

                        End If
                    Next
                Next
                If dtHistorial.Rows.Count > 0 Then
                    Session("tablaExportar") = dtHistorial
                    Session("tablaExportar") = dtHistorial
                    grvReporte.DataSource = dtHistorial
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
