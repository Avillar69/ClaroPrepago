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
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_RECUPERO_TFI_POST(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("D_TELEFONO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CUSTCODE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NOMBRES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CUSTOMER_ID", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FACTURA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FEC_EMISION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FEC_VENCIMIENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MONTO_ORIGINAL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MONTO_PENDIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MOTIVO_NO_PAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_GENERA_COMPROMISO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_APLICA_BENEFICIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_FEC_RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MOTIVO_NO_PAGARA", Type.GetType("System.String"))
		dtHistorial.Columns.Add("TXT_OBSERVACIONES", Type.GetType("System.String"))

                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_RECUPERO_TFI_POST(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("D_TELEFONO") = dtScripting.Rows(j)("D_TELEFONO").ToString
                            dtHistorial.Rows(i)("D_CUSTCODE") = dtScripting.Rows(j)("D_CUSTCODE").ToString
                            dtHistorial.Rows(i)("D_NOMBRES") = dtScripting.Rows(j)("D_NOMBRES").ToString
                            dtHistorial.Rows(i)("D_CUSTOMER_ID") = dtScripting.Rows(j)("D_CUSTOMER_ID").ToString
                            dtHistorial.Rows(i)("D_FACTURA") = dtScripting.Rows(j)("D_FACTURA").ToString
                            dtHistorial.Rows(i)("D_FEC_EMISION") = dtScripting.Rows(j)("D_FEC_EMISION").ToString
                            dtHistorial.Rows(i)("D_FEC_VENCIMIENTO") = dtScripting.Rows(j)("D_FEC_VENCIMIENTO").ToString
                            dtHistorial.Rows(i)("D_MONTO_ORIGINAL") = dtScripting.Rows(j)("D_MONTO_ORIGINAL").ToString
                            dtHistorial.Rows(i)("D_MONTO_PENDIENTE") = dtScripting.Rows(j)("D_MONTO_PENDIENTE").ToString
                            dtHistorial.Rows(i)("CBO_MOTIVO_NO_PAGO") = dtScripting.Rows(j)("CBO_MOTIVO_NO_PAGO").ToString
                            dtHistorial.Rows(i)("CBO_GENERA_COMPROMISO") = dtScripting.Rows(j)("CBO_GENERA_COMPROMISO").ToString
                            dtHistorial.Rows(i)("CBO_APLICA_BENEFICIO") = dtScripting.Rows(j)("CBO_APLICA_BENEFICIO").ToString
                            dtHistorial.Rows(i)("DTP_FEC_RECARGA") = dtScripting.Rows(j)("DTP_FEC_RECARGA").ToString
                            dtHistorial.Rows(i)("CBO_MOTIVO_NO_PAGARA") = dtScripting.Rows(j)("CBO_MOTIVO_NO_PAGARA").ToString
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
        grvReporte.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim nombre As String = "Recupero_TFI_Postpago"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
