Imports System.Data
Imports System.IO

Partial Class rep_rep_porta2
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
        lnkExportar1.Visible = False
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Dim i As Integer = 0
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            Dim dtGeneral As New DataTable()
            dtGeneral.Columns.Add("ID_LOG", Type.GetType("System.String"))
            dtGeneral.Columns.Add("FECHA GESTION", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("HORA INI", Type.GetType("System.String"))
            dtGeneral.Columns.Add("HORA FIN", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("TIEMPO EN SEG.", Type.GetType("System.String"))
            dtGeneral.Columns.Add("LOADID", Type.GetType("System.String"))
            dtGeneral.Columns.Add("ID2", Type.GetType("System.String"))
            dtGeneral.Columns.Add("ID_FINAL", Type.GetType("System.String"))
            dtGeneral.Columns.Add("FINAL", Type.GetType("System.String"))
            dtGeneral.Columns.Add("LOGIN", Type.GetType("System.String"))
            dtGeneral.Columns.Add("TELEFONO", Type.GetType("System.String"))
            dtGeneral.Columns.Add("TALKTIME", Type.GetType("System.String"))
            dtGeneral.Columns.Add("ACWTIME", Type.GetType("System.String"))

            dtGeneral.Columns.Add("ID", Type.GetType("System.String"))
            dtGeneral.Columns.Add("NOMBRE DE CARTERA", Type.GetType("System.String"))
            dtGeneral.Columns.Add("RUC/DNI", Type.GetType("System.String"))
            dtGeneral.Columns.Add("CUSTOMER ID ( CUST ACCOUNT )", Type.GetType("System.String"))
            dtGeneral.Columns.Add("NRO DE DOCUMENTO", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("PLAN TARIFARIO ", Type.GetType("System.String"))
            dtGeneral.Columns.Add("AFILIACION A CORREO", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("MONTO GESTIONADO", Type.GetType("System.String"))
            dtGeneral.Columns.Add("NOMBRE COMPLETO CLIENTE", Type.GetType("System.String"))
            dtGeneral.Columns.Add("AGENTE", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("TIPO DE GESTION ( CALL / CAMPO / IVR )", Type.GetType("System.String"))
            dtGeneral.Columns.Add("TIPIFICACION", Type.GetType("System.String"))
            dtGeneral.Columns.Add("ESCENARIO DE TIPIFICACION", Type.GetType("System.String"))
            dtGeneral.Columns.Add("RESULTADO ( CONTACTO / No contacto )", Type.GetType("System.String"))
            dtGeneral.Columns.Add("MOTIVO NO PAGO", Type.GetType("System.String"))
            dtGeneral.Columns.Add("OBSERVACIONES ( DETALLE DE GESTION )", Type.GetType("System.String"))
            dtGeneral.Columns.Add("TELF. DE CONTACTO", Type.GetType("System.String"))
            'dtGeneral.Columns.Add("GESTOR", Type.GetType("System.String"))
            dtGeneral.Columns.Add("DEBITO", Type.GetType("System.String"))

            'Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_SEG_CLI_TOP(be)

            'Dim IDS As String = ""
            'For a = 0 To dtHistorial.Rows.Count - 1
            'IDS = "" & dtHistorial.Rows(a)("ID").ToString

            'IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)
            Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_CLIENTE_TOP(be)
            If dtScripting.Rows.Count > 0 Then



                'dtGeneral.Rows.Add(dr)
                'End If
                'Next
                'Next
            Else
                lblMsg.Text = "No hay datos con parametro de busqueda"

            End If
            'Next
            If dtScripting.Rows.Count > 0 Then
                Session("tablaExportar") = dtScripting
                grvReporte.DataSource = dtScripting
                grvReporte.DataBind()
                'EXPORTAR()
                lnkExportar1.Visible = True
                lblMsg.Text = "TOTAL DE REGISTROS : " & dtScripting.Rows.Count
                lblMsg.CssClass = "alert alert-success"
            Else
                lblMsg.Text = "SIN REGISTROS VALIDOS PARA LAS FECHAS EN BUSQUEDA"
                lblMsg.CssClass = "alert alert-danger"
            End If


        Catch ex As Exception
            lblMsg.Text = "[ERROR #500 ####] : " & ex.Message
            lblMsg.CssClass = "alert alert-danger"
        End Try
    End Sub
    'Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
    '    If grvReporte.Rows.Count > 0 Then
    '        EXPORTAR()
    '    End If
    'End Sub
    Sub EXPORTAR()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        grvExport.DataSource = Session("tablaExportar")
        grvExport.DataBind()
        grvExport.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvExport)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim nombre As String = "Seguimiento_ClientesTop" & Now.ToString("yyyy-MM-dd")
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub ExportToExcel1(sender As Object, e As System.EventArgs) Handles lnkExportar1.Click
        EXPORTAR()
    End Sub

    Protected Sub grvReporte_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte.PageIndexChanging
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
        If grvReporte.Rows.Count < 1 Then
            grvReporte.DataSource = Session("tablaCarga")
            grvReporte.DataBind()
        End If
    End Sub
End Class