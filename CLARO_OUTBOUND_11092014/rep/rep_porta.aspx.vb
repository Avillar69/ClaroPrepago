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

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_PORT(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("TELEFONO MOVIL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CANTIDAD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OPERADOR CEDENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MODALIDAD TELEFONO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CONTACTO CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO DOCUMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO DOCUMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NOMBRE CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MOTIVO SP", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBSERVACION SP", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA PROGRAMACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBSERVACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NRO_LLAMADAS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO_CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO_NO_CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA ENVIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA GESTION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("SOLICITUD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO SEC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DESPACHO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("VENTA EFECTIVA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OPERADOR RECEPTOR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ESTADO SP", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO MENSAJE SP", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA_REGISTRO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PUNTO VENTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ID SOLICITUD PORTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("SUSTENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBS_AGENTE", Type.GetType("System.String"))

                Dim dtScripting As DataTable = da.SP_LISTAR_PORT()

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim



                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("TELEFONO MOVIL") = dtScripting.Rows(j)("TELEFONO MOVIL").ToString
                            dtHistorial.Rows(i)("CANTIDAD") = dtScripting.Rows(j)("CANTIDAD").ToString
                            dtHistorial.Rows(i)("TIPO") = dtScripting.Rows(j)("TIPO").ToString
                            dtHistorial.Rows(i)("OPERADOR CEDENTE") = dtScripting.Rows(j)("OPERADOR CEDENTE").ToString
                            dtHistorial.Rows(i)("MODALIDAD TELEFONO") = dtScripting.Rows(j)("MODALIDAD TELEFONO").ToString
                            dtHistorial.Rows(i)("CONTACTO CLIENTE") = dtScripting.Rows(j)("CONTACTO CLIENTE").ToString
                            dtHistorial.Rows(i)("TIPO DOCUMENTO") = dtScripting.Rows(j)("TIPO DOCUMENTO").ToString
                            dtHistorial.Rows(i)("NRO DOCUMENTO") = dtScripting.Rows(j)("NRO DOCUMENTO").ToString
                            dtHistorial.Rows(i)("NOMBRE CLIENTE") = dtScripting.Rows(j)("NOMBRE CLIENTE").ToString
                            dtHistorial.Rows(i)("MOTIVO SP") = dtScripting.Rows(j)("MOTIVO SP").ToString
                            dtHistorial.Rows(i)("OBSERVACION SP") = dtScripting.Rows(j)("OBSERVACION SP").ToString
                            dtHistorial.Rows(i)("FECHA PROGRAMACION") = dtScripting.Rows(j)("FECHA PROGRAMACION").ToString
                            dtHistorial.Rows(i)("OBSERVACION") = dtScripting.Rows(j)("OBSERVACION").ToString
                            dtHistorial.Rows(i)("TXT_NRO_LLAMADAS") = dtScripting.Rows(j)("TXT_NRO_LLAMADAS").ToString
                            dtHistorial.Rows(i)("CBO_TIPO") = dtScripting.Rows(j)("CBO_TIPO").ToString
                            dtHistorial.Rows(i)("CBO_TIPO_CONTACTO") = dtScripting.Rows(j)("CBO_TIPO_CONTACTO").ToString
                            dtHistorial.Rows(i)("CBO_TIPO_NO_CONTACTO") = dtScripting.Rows(j)("CBO_TIPO_NO_CONTACTO").ToString
                            dtHistorial.Rows(i)("FECHA ENVIO") = dtScripting.Rows(j)("FECHA ENVIO").ToString
                            dtHistorial.Rows(i)("FECHA GESTION") = dtScripting.Rows(j)("FECHA GESTION").ToString
                            dtHistorial.Rows(i)("SOLICITUD") = dtScripting.Rows(j)("SOLICITUD").ToString
                            dtHistorial.Rows(i)("NRO SEC") = dtScripting.Rows(j)("NRO SEC").ToString
                            dtHistorial.Rows(i)("DESPACHO") = dtScripting.Rows(j)("DESPACHO").ToString
                            dtHistorial.Rows(i)("VENTA EFECTIVA") = dtScripting.Rows(j)("VENTA EFECTIVA").ToString
                            dtHistorial.Rows(i)("OPERADOR RECEPTOR") = dtScripting.Rows(j)("OPERADOR RECEPTOR").ToString
                            dtHistorial.Rows(i)("ESTADO SP") = dtScripting.Rows(j)("ESTADO SP").ToString
                            dtHistorial.Rows(i)("TIPO MENSAJE SP") = dtScripting.Rows(j)("TIPO MENSAJE SP").ToString
                            dtHistorial.Rows(i)("FECHA_REGISTRO") = dtScripting.Rows(j)("FECHA_REGISTRO").ToString
                            dtHistorial.Rows(i)("PUNTO VENTA") = dtScripting.Rows(j)("PUNTO VENTA").ToString
                            dtHistorial.Rows(i)("ID SOLICITUD PORTA") = dtScripting.Rows(j)("ID SOLICITUD PORTA").ToString
                            dtHistorial.Rows(i)("SUSTENTO") = dtScripting.Rows(j)("SUSTENTO").ToString
                            dtHistorial.Rows(i)("OBS_AGENTE") = dtScripting.Rows(j)("OBS_AGENTE").ToString


                        End If
                    Next
                Next
                grvReporte.DataSource = dtHistorial
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
