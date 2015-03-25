Imports System.Data
Imports System.IO

Partial Class rep_rep_porta
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cboFecha.DataSource = da.SP_RECUPERO_EMAIL_FECHA_REG()
            cboFecha.DataMember = "FECHA"
            cboFecha.DataValueField = "FECHA"
            cboFecha.DataBind()
        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        lnkExportar.Visible = False
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Try
            be.VAR_FECHA = cboFecha.SelectedValue

            Dim dtHistorial As DataTable = da.SP_LISTAR_RECUPERO_EMAIL_REG(cboFecha.Text)
            If dtHistorial.Rows.Count > 0 Then

                'dtHistorial.Columns.Add("D_NOMBRES", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("D_TELEFONO", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("D_COD_CLIENTE", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("D_FECHA_BAJA", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("D_DISTRITO", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("D_PROVINCIA", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("D_DEPARTAMENTO", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("D_MATERIAL_DES", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("D_PLAN_TARIFARIO", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("D_DESC_PLAZO_ACUERDO", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_REALIZA_ENC", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_MOTIVO_NO_PAGO", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_FRECUENCIA_USO_SERVICIO", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_XQ_NO_USABA_SERV", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_OPI_CALI_SERV", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_OPI_PRECIO_SERV", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_XQ_DEJO_PAGAR", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_PROBLEMAS_TECNICOS", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("TXT_TELEF_50MIN", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("CBO_VE_ACTUALMENTE", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("TXT_OBSERVACIONES", Type.GetType("System.String"))

                'Dim IDS As String = ""
                'For i = 0 To dtHistorial.Rows.Count - 1
                '    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                'Next
                'IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                'Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_ENCUESTA_POSTPAGO(IDS)

                '    'For i = 0 To dtHistorial.Rows.Count - 1
                '    '    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                '    '    For j = 0 To dtScripting.Rows.Count - 1
                '    '        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                '    '        If IDScrip = IDhIS Then
                '    '            dtHistorial.Rows(i)("D_NOMBRES") = dtScripting.Rows(j)("D_NOMBRES").ToString
                '    '            dtHistorial.Rows(i)("D_TELEFONO") = dtScripting.Rows(j)("D_TELEFONO").ToString
                '    '            dtHistorial.Rows(i)("D_COD_CLIENTE") = dtScripting.Rows(j)("D_COD_CLIENTE").ToString
                '    '            dtHistorial.Rows(i)("D_FECHA_BAJA") = dtScripting.Rows(j)("D_FECHA_BAJA").ToString
                '    '            dtHistorial.Rows(i)("D_DISTRITO") = dtScripting.Rows(j)("D_DISTRITO").ToString
                '    '            dtHistorial.Rows(i)("D_PROVINCIA") = dtScripting.Rows(j)("D_PROVINCIA").ToString
                '    '            dtHistorial.Rows(i)("D_DEPARTAMENTO") = dtScripting.Rows(j)("D_DEPARTAMENTO").ToString
                '    '            dtHistorial.Rows(i)("D_MATERIAL_DES") = dtScripting.Rows(j)("D_MATERIAL_DES").ToString
                '    '            dtHistorial.Rows(i)("D_PLAN_TARIFARIO") = dtScripting.Rows(j)("D_PLAN_TARIFARIO").ToString
                '    '            dtHistorial.Rows(i)("D_DESC_PLAZO_ACUERDO") = dtScripting.Rows(j)("D_DESC_PLAZO_ACUERDO").ToString
                '    '            dtHistorial.Rows(i)("CBO_REALIZA_ENC") = dtScripting.Rows(j)("CBO_REALIZA_ENC").ToString
                '    '            dtHistorial.Rows(i)("CBO_MOTIVO_NO_PAGO") = dtScripting.Rows(j)("CBO_MOTIVO_NO_PAGO").ToString
                '    '            dtHistorial.Rows(i)("CBO_FRECUENCIA_USO_SERVICIO") = dtScripting.Rows(j)("CBO_FRECUENCIA_USO_SERVICIO").ToString
                '    '            dtHistorial.Rows(i)("CBO_XQ_NO_USABA_SERV") = dtScripting.Rows(j)("CBO_XQ_NO_USABA_SERV").ToString
                '    '            dtHistorial.Rows(i)("CBO_OPI_CALI_SERV") = dtScripting.Rows(j)("CBO_OPI_CALI_SERV").ToString
                '    '            dtHistorial.Rows(i)("CBO_OPI_PRECIO_SERV") = dtScripting.Rows(j)("CBO_OPI_PRECIO_SERV").ToString
                '    '            dtHistorial.Rows(i)("CBO_XQ_DEJO_PAGAR") = dtScripting.Rows(j)("CBO_XQ_DEJO_PAGAR").ToString
                '    '            dtHistorial.Rows(i)("CBO_PROBLEMAS_TECNICOS") = dtScripting.Rows(j)("CBO_PROBLEMAS_TECNICOS").ToString
                '    '            dtHistorial.Rows(i)("TXT_TELEF_50MIN") = dtScripting.Rows(j)("TXT_TELEF_50MIN").ToString
                '    '            dtHistorial.Rows(i)("CBO_VE_ACTUALMENTE") = dtScripting.Rows(j)("CBO_VE_ACTUALMENTE").ToString
                '    '            dtHistorial.Rows(i)("TXT_OBSERVACIONES") = dtScripting.Rows(j)("TXT_OBSERVACIONES").ToString

                'End If
                '        Next
                '    Next
                'If dtHistorial.Rows.Count > 0 Then
                Session("tablaExportar") = dtHistorial
                grvReporte.DataSource = dtHistorial
                grvReporte.DataBind()
                'EXPORTAR()
                lnkExportar.Visible = True
                lblMsg.Text = "Cantidad de registros encontrados: " & dtHistorial.Rows.Count
                'End If
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
        Dim nombre As String = "Claro Encuesta Postpago"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub grvReporte_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte.PageIndexChanging
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
    End Sub
End Class
