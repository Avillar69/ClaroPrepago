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

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_RECUPERO_FISICA(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("TXT_OBSERVACIONES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CUSTID", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CICLO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_RAZ_SOCIAL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NOMBRE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DNI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DIRECCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_REFERENCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_PROVINCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DISTRITO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_MOVIL_CLARO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_REFERENCIA_1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_REFERENCIA_2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_PLAN", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACEPTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_EMAIL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_DOMINIO_MAIL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_EMAIL_COMPLETO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_MOTIVO_NO_ACT", Type.GetType("System.String")) '
                dtHistorial.Columns.Add("CBO_TIPO_VIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NOMBRE_VIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NRO_VIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MANZANA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NRO_LETRA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_LOTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NRO_LOTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NRO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_DIRECCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_URBANIZACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NOMBRE_URB", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ZONA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NOMBRE_ZONA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_REFERENCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_REFERENCIA_COMPLETA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_PROVINCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_DISTRITO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO_CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_RESULTADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_TELEF_REFERENCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA_EVALUACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("LOGIN_CALIDAD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBS_CALIDAD", Type.GetType("System.String"))

                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_HISTORIAL_RECUPERA_FISICA(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then
                            dtHistorial.Rows(i)("TXT_OBSERVACIONES") = dtScripting.Rows(j)("TXT_OBSERVACIONES").ToString
                            dtHistorial.Rows(i)("D_CUSTID") = dtScripting.Rows(j)("D_CUSTID").ToString
                            dtHistorial.Rows(i)("D_CICLO") = dtScripting.Rows(j)("D_CICLO").ToString
                            dtHistorial.Rows(i)("D_RAZ_SOCIAL") = dtScripting.Rows(j)("D_RAZ_SOCIAL").ToString
                            dtHistorial.Rows(i)("D_NOMBRE") = dtScripting.Rows(j)("D_NOMBRE").ToString
                            dtHistorial.Rows(i)("D_DNI") = dtScripting.Rows(j)("D_DNI").ToString
                            dtHistorial.Rows(i)("D_DIRECCION") = dtScripting.Rows(j)("D_DIRECCION").ToString
                            dtHistorial.Rows(i)("D_REFERENCIA") = dtScripting.Rows(j)("D_REFERENCIA").ToString
                            dtHistorial.Rows(i)("D_DEPARTAMENTO") = dtScripting.Rows(j)("D_DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("D_PROVINCIA") = dtScripting.Rows(j)("D_PROVINCIA").ToString
                            dtHistorial.Rows(i)("D_DISTRITO") = dtScripting.Rows(j)("D_DISTRITO").ToString
                            dtHistorial.Rows(i)("D_MOVIL_CLARO") = dtScripting.Rows(j)("D_MOVIL_CLARO").ToString
                            dtHistorial.Rows(i)("D_REFERENCIA_1") = dtScripting.Rows(j)("D_REFERENCIA_1").ToString
                            dtHistorial.Rows(i)("D_REFERENCIA_2") = dtScripting.Rows(j)("D_REFERENCIA_2").ToString
                            dtHistorial.Rows(i)("D_PLAN") = dtScripting.Rows(j)("D_PLAN").ToString
                            dtHistorial.Rows(i)("CBO_ACEPTA") = dtScripting.Rows(j)("CBO_ACEPTA").ToString
                            dtHistorial.Rows(i)("TXT_EMAIL") = dtScripting.Rows(j)("TXT_EMAIL").ToString
                            dtHistorial.Rows(i)("CBO_DOMINIO_MAIL") = dtScripting.Rows(j)("CBO_DOMINIO_MAIL").ToString
                            dtHistorial.Rows(i)("TXT_EMAIL_COMPLETO") = dtScripting.Rows(j)("TXT_EMAIL_COMPLETO").ToString
                            dtHistorial.Rows(i)("TXT_MOTIVO_NO_ACT") = dtScripting.Rows(j)("TXT_MOTIVO_NO_ACT").ToString
                            dtHistorial.Rows(i)("CBO_TIPO_VIA") = dtScripting.Rows(j)("CBO_TIPO_VIA").ToString
                            dtHistorial.Rows(i)("TXT_NOMBRE_VIA") = dtScripting.Rows(j)("TXT_NOMBRE_VIA").ToString
                            dtHistorial.Rows(i)("TXT_NRO_VIA") = dtScripting.Rows(j)("TXT_NRO_VIA").ToString
                            dtHistorial.Rows(i)("CBO_MANZANA") = dtScripting.Rows(j)("CBO_MANZANA").ToString
                            dtHistorial.Rows(i)("TXT_NRO_LETRA") = dtScripting.Rows(j)("TXT_NRO_LETRA").ToString
                            dtHistorial.Rows(i)("TXT_LOTE") = dtScripting.Rows(j)("TXT_LOTE").ToString
                            dtHistorial.Rows(i)("TXT_NRO_LOTE") = dtScripting.Rows(j)("TXT_NRO_LOTE").ToString
                            dtHistorial.Rows(i)("CBO_TIPO") = dtScripting.Rows(j)("CBO_TIPO").ToString
                            dtHistorial.Rows(i)("TXT_NRO") = dtScripting.Rows(j)("TXT_NRO").ToString
                            dtHistorial.Rows(i)("TXT_DIRECCION") = dtScripting.Rows(j)("TXT_DIRECCION").ToString
                            dtHistorial.Rows(i)("CBO_URBANIZACION") = dtScripting.Rows(j)("CBO_URBANIZACION").ToString
                            dtHistorial.Rows(i)("TXT_NOMBRE_URB") = dtScripting.Rows(j)("TXT_NOMBRE_URB").ToString
                            dtHistorial.Rows(i)("CBO_ZONA") = dtScripting.Rows(j)("CBO_ZONA").ToString
                            dtHistorial.Rows(i)("TXT_NOMBRE_ZONA") = dtScripting.Rows(j)("TXT_NOMBRE_ZONA").ToString
                            dtHistorial.Rows(i)("TXT_REFERENCIA") = dtScripting.Rows(j)("TXT_REFERENCIA").ToString
                            dtHistorial.Rows(i)("TXT_REFERENCIA_COMPLETA") = dtScripting.Rows(j)("TXT_REFERENCIA_COMPLETA").ToString
                            dtHistorial.Rows(i)("CBO_DEPARTAMENTO") = dtScripting.Rows(j)("CBO_DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("CBO_PROVINCIA") = dtScripting.Rows(j)("CBO_PROVINCIA").ToString
                            dtHistorial.Rows(i)("CBO_DISTRITO") = dtScripting.Rows(j)("CBO_DISTRITO").ToString
                            dtHistorial.Rows(i)("CBO_TIPO_CONTACTO") = dtScripting.Rows(j)("CBO_TIPO_CONTACTO").ToString
                            dtHistorial.Rows(i)("CBO_RESULTADO") = dtScripting.Rows(j)("CBO_RESULTADO").ToString
                            dtHistorial.Rows(i)("TXT_TELEF_REFERENCIA") = dtScripting.Rows(j)("TXT_TELEF_REFERENCIA").ToString
                            dtHistorial.Rows(i)("FECHA_EVALUACION") = dtScripting.Rows(j)("FECHA_EVALUACION").ToString
                            dtHistorial.Rows(i)("LOGIN_CALIDAD") = dtScripting.Rows(j)("LOGIN_CALIDAD").ToString
                            dtHistorial.Rows(i)("OBS_CALIDAD") = dtScripting.Rows(j)("OBS_CALIDAD").ToString


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
        Dim nombre As String = "Recupero_Fisico"
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
