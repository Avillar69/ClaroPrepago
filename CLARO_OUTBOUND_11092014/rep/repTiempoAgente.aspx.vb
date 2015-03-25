Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO

Partial Class frmMaestro_repTiempoAgente
    Inherits System.Web.UI.Page
    Dim da As New DA_claro

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            txtInicio.Text = Now.ToString("yyyy-MM-dd")
            txtFin.Text = Now.ToString("yyyy-MM-dd")

            Dim ini As String = Request.QueryString("ini")
            Dim fin As String = Request.QueryString("fin")
            If Not IsNothing(ini) > 0 And Not IsNothing(fin) Then
                txtInicio.Text = ini
                txtFin.Text = fin
                SP_REPORTE_TIEMPO_PARADAS_X_FECHA()
            End If

        End If

    End Sub

    Sub SP_REPORTE_TIEMPO_PARADAS_X_FECHA()
        Session("tablaExportar") = Nothing
        Dim be As New BE_CLARO
        grv.DataSource = Nothing : grv.DataBind()
        lblError.Text = ""
        btnExportar.Visible = False
        Try
            Dim dt As DataTable = da.SP_REPORTE_TIEMPO_PARADAS_X_FECHA(txtInicio.Text, txtFin.Text)
            For index = 0 To Convert.ToDateTime(txtFin.Text).DayOfYear - Convert.ToDateTime(txtInicio.Text).DayOfYear
                be.inicio = Convert.ToDateTime(txtInicio.Text).AddDays(index).ToString("yyyy-MM-dd")
                be.fin = be.inicio
                Dim temp As DataTable = da.SP_REPORTE_TIEMPO_PARADAS_X_FECHA(be.inicio, be.fin)
                If index = 0 Then
                    dt = temp.Clone
                End If
                For Each item As DataRow In temp.Rows
                    dt.ImportRow(item)
                Next
            Next
            If dt.Rows.Count > 0 And dt.Columns(0).ColumnName = "ERROR" Then
                lblError.Text = dt.Rows(0)(0).ToString : Exit Sub
            End If

            If dt.Rows.Count > 0 Then
                grv.DataSource = dt : grv.DataBind()
                btnExportar.Visible = True
                lblError.Text = "Cantidad de registros : " & dt.Rows.Count & ", del " & txtInicio.Text & ", al " & txtFin.Text
                Session("tablaExportar") = dt
            Else
                lblError.Text = "No hay datos con parametro de busqueda"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        SP_REPORTE_TIEMPO_PARADAS_X_FECHA()
    End Sub

    'Protected Sub grv_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grv.PageIndexChanging
    '    grv.PageIndex = e.NewPageIndex
    '    SP_REPORTE_TIEMPO_PARADAS_X_FECHA()
    'End Sub


    Sub EXPORTAR()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
		Dim grv1 As New GridView
        grv1.DataSource = Session("tablaExportar")
        grv1.DataBind()
        grv1.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grv1)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim nombre As String = "ClienTop_TiempoGestion"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        If grv.Rows.Count > 0 Then
            EXPORTAR()
        End If
    End Sub

    Protected Sub grv_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grv.PageIndexChanging
        grv.PageIndex = e.NewPageIndex
        grv.DataSource = Session("tablaExportar")
        grv.DataBind()
        'SP_REPORTE_TIEMPO_PARADAS_X_FECHA()
    End Sub
End Class
