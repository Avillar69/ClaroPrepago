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

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_ENCUESTA(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("D_NOMBRES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FECHA_BAJA", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("D_DEPARTAMENTO", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("D_DESCRIPCION", Type.GetType("System.String"))

                'dtHistorial.Columns.Add("TXT_MOTIVO_NO_RECARGA", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_SABE_PROMOCIONES", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_ESCALA_CONSIDERA_SERVICIO", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_INCONVENIENTES_RECARGA", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_PROB_SENIAL_COBERTURA", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("TXT_OBSERVACION_MEJORA", Type.GetType("System.String"))

                'dtHistorial.Columns.Add("CBO_MOTIVO_SOLICITO_PORTA", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_HUBIERA_GUSTADO_PORTA", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("TXT_OTROS_PORTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_PORTO_PLAN_MISMO_CARGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_SINO_PORTO_PLAN", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_LLAMO_ATENCION_COMPETENCIA_PORTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ESTUVO_SATISFECHO_SERV", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_XQ_NO_ESTUVO_SATISFECHO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MALA_ATENCION_POST_PORTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OTROS_MALA_ATEN_PORTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_SE_ENTERO_ANTES_DE_MIGRAR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_REGRESARIA_A_CLARO", Type.GetType("System.String"))


                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_ENCUESTA_POSTPAGO(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then
                            dtHistorial.Rows(i)("D_NOMBRES") = dtScripting.Rows(j)("D_NOMBRES").ToString
                            dtHistorial.Rows(i)("D_TELEFONO") = dtScripting.Rows(j)("D_TELEFONO").ToString
                            dtHistorial.Rows(i)("D_FECHA_BAJA") = dtScripting.Rows(j)("D_FECHA_BAJA").ToString
                            'dtHistorial.Rows(i)("D_DEPARTAMENTO") = dtScripting.Rows(j)("D_DEPARTAMENTO").ToString
                            'dtHistorial.Rows(i)("D_DESCRIPCION") = dtScripting.Rows(j)("D_DESCRIPCION").ToString

                            'dtHistorial.Rows(i)("TXT_MOTIVO_NO_RECARGA") = dtScripting.Rows(j)("TXT_MOTIVO_NO_RECARGA").ToString
                            'dtHistorial.Rows(i)("CBO_SABE_PROMOCIONES") = dtScripting.Rows(j)("CBO_SABE_PROMOCIONES").ToString
                            'dtHistorial.Rows(i)("CBO_ESCALA_CONSIDERA_SERVICIO") = dtScripting.Rows(j)("CBO_ESCALA_CONSIDERA_SERVICIO").ToString
                            'dtHistorial.Rows(i)("CBO_INCONVENIENTES_RECARGA") = dtScripting.Rows(j)("CBO_INCONVENIENTES_RECARGA").ToString
                            'dtHistorial.Rows(i)("CBO_PROB_SENIAL_COBERTURA") = dtScripting.Rows(j)("CBO_PROB_SENIAL_COBERTURA").ToString
                            'dtHistorial.Rows(i)("TXT_OBSERVACION_MEJORA") = dtScripting.Rows(j)("TXT_OBSERVACION_MEJORA").ToString

                            'dtHistorial.Rows(i)("CBO_MOTIVO_SOLICITO_PORTA") = dtScripting.Rows(j)("CBO_MOTIVO_SOLICITO_PORTA").ToString
                            'dtHistorial.Rows(i)("CBO_HUBIERA_GUSTADO_PORTA") = dtScripting.Rows(j)("CBO_HUBIERA_GUSTADO_PORTA").ToString
                            'dtHistorial.Rows(i)("TXT_OTROS_PORTA") = dtScripting.Rows(j)("TXT_OTROS_PORTA").ToString
                            dtHistorial.Rows(i)("CBO_PORTO_PLAN_MISMO_CARGO") = dtScripting.Rows(j)("CBO_PORTO_PLAN_MISMO_CARGO").ToString
                            dtHistorial.Rows(i)("CBO_SINO_PORTO_PLAN") = dtScripting.Rows(j)("CBO_SINO_PORTO_PLAN").ToString
                            dtHistorial.Rows(i)("CBO_LLAMO_ATENCION_COMPETENCIA_PORTA") = dtScripting.Rows(j)("CBO_LLAMO_ATENCION_COMPETENCIA_PORTA").ToString
                            dtHistorial.Rows(i)("CBO_ESTUVO_SATISFECHO_SERV") = dtScripting.Rows(j)("CBO_ESTUVO_SATISFECHO_SERV").ToString
                            dtHistorial.Rows(i)("CBO_XQ_NO_ESTUVO_SATISFECHO") = dtScripting.Rows(j)("CBO_XQ_NO_ESTUVO_SATISFECHO").ToString
                            dtHistorial.Rows(i)("CBO_MALA_ATENCION_POST_PORTA") = dtScripting.Rows(j)("CBO_MALA_ATENCION_POST_PORTA").ToString
                            dtHistorial.Rows(i)("TXT_OTROS_MALA_ATEN_PORTA") = dtScripting.Rows(j)("TXT_OTROS_MALA_ATEN_PORTA").ToString
                            dtHistorial.Rows(i)("CBO_SE_ENTERO_ANTES_DE_MIGRAR") = dtScripting.Rows(j)("CBO_SE_ENTERO_ANTES_DE_MIGRAR").ToString
                            dtHistorial.Rows(i)("CBO_REGRESARIA_A_CLARO") = dtScripting.Rows(j)("CBO_REGRESARIA_A_CLARO").ToString

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
        Dim nombre As String = "Claro Encuesta Postpago"
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
