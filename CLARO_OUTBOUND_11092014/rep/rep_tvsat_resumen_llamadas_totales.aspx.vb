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

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_TVSAT_RESUMEN_LLAMADAS_TOTALES(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("D_TELEFONO_01", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OBSERVACIONES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA_LLAMADA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NOM_CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_COD_CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_COD_RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_ESTADOPAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_PLAN_GENERAL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CODINSSRV", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CAPA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_GENERA_COMPROMISO_RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MOTIVO_NO_RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_ACT_FEC_RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_ACT_CELU_CLARO", Type.GetType("System.String"))

                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_TVSAT_RESUMEN_LLAM_TOTAL(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("D_TELEFONO_01") = dtScripting.Rows(j)("D_TELEFONO_01").ToString
                            dtHistorial.Rows(i)("TXT_OBSERVACIONES") = dtScripting.Rows(j)("TXT_OBSERVACIONES").ToString
                            dtHistorial.Rows(i)("FECHA_LLAMADA") = dtScripting.Rows(j)("FECHA_LLAMADA").ToString
                            dtHistorial.Rows(i)("D_NOM_CLIENTE") = dtScripting.Rows(j)("D_NOM_CLIENTE").ToString
                            dtHistorial.Rows(i)("D_COD_CLIENTE") = dtScripting.Rows(j)("D_COD_CLIENTE").ToString
                            dtHistorial.Rows(i)("D_DEPARTAMENTO") = dtScripting.Rows(j)("D_DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("D_COD_RECARGA") = dtScripting.Rows(j)("D_COD_RECARGA").ToString
                            dtHistorial.Rows(i)("D_ESTADOPAGO") = dtScripting.Rows(j)("D_ESTADOPAGO").ToString
                            dtHistorial.Rows(i)("D_PLAN_GENERAL") = dtScripting.Rows(j)("D_PLAN_GENERAL").ToString
                            dtHistorial.Rows(i)("D_CODINSSRV") = dtScripting.Rows(j)("D_CODINSSRV").ToString
                            dtHistorial.Rows(i)("CAPA") = dtScripting.Rows(j)("CAPA").ToString
                            dtHistorial.Rows(i)("CBO_ACT_GENERA_COMPROMISO_RECARGA") = dtScripting.Rows(j)("CBO_ACT_GENERA_COMPROMISO_RECARGA").ToString
                            dtHistorial.Rows(i)("CBO_MOTIVO_NO_RECARGA") = dtScripting.Rows(j)("CBO_MOTIVO_NO_RECARGA").ToString
                            dtHistorial.Rows(i)("DTP_ACT_FEC_RECARGA") = dtScripting.Rows(j)("DTP_ACT_FEC_RECARGA").ToString
                            dtHistorial.Rows(i)("TXT_ACT_CELU_CLARO") = dtScripting.Rows(j)("TXT_ACT_CELU_CLARO").ToString

                        End If
                    Next
                Next
                If dtHistorial.Rows.Count > 0 Then
                    grvReporte.DataSource = dtHistorial
                    grvReporte.DataBind()
                    EXPORTAR()
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
        grvReporte.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim mes As String = Request("Mes")
        Dim anio As String = Request("Ano")
        Dim nombre As String = "TvSat_Resumen_LLamadas_Total"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
