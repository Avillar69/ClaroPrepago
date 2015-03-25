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
        Dim i As Integer = 0
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            'Dim dtGeneral As New DataTable()

            Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_VALIDACIONES(be)
            If dtScripting.Rows.Count > 0 Then

            Else
                lblMsg.Text = "No hay datos con parametro de busqueda"

            End If
            If dtScripting.Rows.Count > 0 Then
                Session("tablaExportar") = dtScripting
                grvReporte.DataSource = dtScripting
                grvReporte.DataBind()
                lnkExportar.Visible = True
                lblMsg.Text = "Cantidad de registros encontrados: " & dtScripting.Rows.Count
            End If


        Catch ex As Exception
            lblMsg.Text = "btnBuscar " & ex.Message
        End Try
    End Sub
    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
        If grvReporte.Rows.Count > 0 Then
            EXPORTARBK()
        End If
    End Sub
    Sub EXPORTARBK()
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
        Dim nombre As String = "ClaroValidaciones"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
	
	Sub EXPORTAR()
        Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"
        Response.ClearContent()
        Response.Write(style)
		grvReporte2.DataSource = Session("tablaExportar")
        grvReporte2.DataBind()
        Dim file As String = "ClaroValidaciones_" & Now.ToString("yyyyMMddHHmmss")
        For i As Integer = 0 To grvReporte2.Rows.Count - 1
            Dim row As GridViewRow = grvReporte2.Rows(i)
            ''Apply text style to each Row
            row.Cells(6).Attributes.Add("class", "textmode")
            row.Cells(7).Attributes.Add("class", "textmode")
            row.Cells(8).Attributes.Add("class", "textmode")
            row.Cells(17).Attributes.Add("class", "textmode")
            row.Cells(18).Attributes.Add("class", "textmode")
            row.Cells(19).Attributes.Add("class", "textmode")
        Next
        Response.AddHeader("content-disposition", "attachment; filename=" & file & ".xls")
        Response.ContentType = "application/vnd.ms-excel"
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        grvReporte2.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte2)

        pagina.RenderControl(htw)
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sw.ToString())
        Response.Flush()
        Response.End()
    End Sub
	

    Protected Sub grvReporte_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte.PageIndexChanging
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
    End Sub
End Class