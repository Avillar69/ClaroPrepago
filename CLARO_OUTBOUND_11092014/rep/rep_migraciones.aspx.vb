Imports System.Data
Imports System.IO

Partial Class rep_re_general_migraciones
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO
    Dim be2 As New BE_CLARO_MIGRA

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtInicio.Text = Now.ToString("yyyy-MM-dd")
            txtFin.Text = Now.ToString("yyyy-MM-dd")
        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        lnkExportar.Visible = False
        lnkExpoExcel.Visible = False
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Try
            be2.inicio = txtInicio.Text
            be2.fin = txtFin.Text

           
                'dtHistorial.Columns.Add("ID", Type.GetType("System.String"))
               

            Dim dtHistorial As DataTable = da.SP_LISTAR_MIGRACIONES_CLIENTE(be2)
                If dtHistorial.Rows.Count > 0 Then

                'dtHistorial.Rows(i)("ID") = dtScripting.Rows(j)("ID").ToString

                dtHistorial.Rows(0)("FECHAHORA").ToString()
                dtHistorial.Rows(1)("IDBASE").ToString()
                dtHistorial.Rows(2)("IDCALLCENTER").ToString()
                dtHistorial.Rows(3)("CALLCENTER").ToString()
                dtHistorial.Rows(4)("MSISDN").ToString()
                dtHistorial.Rows(5)("IDCAMPANA").ToString()
                dtHistorial.Rows(6)("IDTIPIFICACION").ToString()
                dtHistorial.Rows(7)("NIVEL1").ToString()
                dtHistorial.Rows(8)("NIVEL2").ToString()
                dtHistorial.Rows(9)("NIVEL3").ToString()
                dtHistorial.Rows(10)("NIVEL4").ToString()
                dtHistorial.Rows(11)("IDAGENTE").ToString()
                dtHistorial.Rows(12)("OTROS1").ToString()
                dtHistorial.Rows(13)("OTROS2").ToString()
                dtHistorial.Rows(14)("OTROS3").ToString()
                dtHistorial.Rows(15)("COMENTARIO").ToString()
                dtHistorial.Rows(16)("NCONTACTOS").ToString()

                grvReporte.DataSource = dtHistorial
                Session("tablaExportar") = dtHistorial
                grvReporte.DataBind()
                lnkExportar.Visible = True
                lnkExpoExcel.Visible = True
                lblMsg.Text = "Cantidad de registros encontrados: " & dtHistorial.Rows.Count
            Else
                lblMsg.Text = "No hay datos con parametro de busqueda"
            End If


        Catch ex As Exception
            lblMsg.Text = "btnBuscar " & ex.Message
        End Try

    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
        ExportarTxt()
    End Sub
    Sub EXPORTAR()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        grvReporte.DataSource = Nothing
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
        grvReporte.EnableViewState = False

       
        'grvReporte3.DataSource = Nothing
        'grvReporte3.DataSource = Session("tablaExportar2")
        'grvReporte3.DataBind()
        'grvReporte3.EnableViewState = False


        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte)
        'form.Controls.Add(grvReporte3)



        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim mes As String = Request("Mes")
        Dim anio As String = Request("Ano")
        Dim nombre As String = "RetencionesOut"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Tipif_CALL_07_" & Now.ToString("yyyyMMdd") & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Sub ExportarTxt()
        Dim var_ruta As String = ""
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("FECHAHORA|IDBASE|IDCALLCENTER |CALLCENTER|MSISDN |IDCAMPANA|IDTIPIFICACION|NIVEL1|NIVEL2|NIVEL3|NIVEL4|IDAGENTE|OTROS1|OTROS2|OTROS3|COMENTARIO|NCONTACTOS")
        str.Append(vbNewLine)

        For i = 0 To grvReporte.Rows.Count - 1
            Dim j As Integer
            For j = 0 To grvReporte.Columns.Count - 1
                Dim cabecera As String = grvReporte.Columns(j).HeaderText.ToString
                Dim campo As String = grvReporte.Rows(i).Cells(j).Text
                campo = Replace(campo, "&nbsp;", "")
                campo = Replace(campo, "á", "a")
                campo = Replace(campo, "é", "e")
                campo = Replace(campo, "í", "i")
                campo = Replace(campo, "ó", "o")
                campo = Replace(campo, "ú", "u")
                campo = Replace(campo, "ñ", "n")
                If cabecera = "FECHAHORA" Then campo = campo & "|"
                If cabecera = "IDBASE" Then campo = campo & "|"
                If cabecera = "IDCALLCENTER" Then campo = campo & "|"
                If cabecera = "CALLCENTER" Then campo = campo & "|"
                If cabecera = "MSISDN" Then campo = campo & "|"
                If cabecera = "IDCAMPANA" Then campo = campo & "|"
                If cabecera = "IDTIPIFICACION" Then campo = campo & "|"
                If cabecera = "NIVEL1" Then campo = campo & "|"
                If cabecera = "NIVEL2" Then campo = campo & "|"
                If cabecera = "NIVEL3" Then campo = campo & "|"
                If cabecera = "NIVEL4" Then campo = campo & "|"
                If cabecera = "IDAGENTE" Then campo = campo & "|"
                If cabecera = "OTROS1" Then campo = campo & "|"
                If cabecera = "OTROS2" Then campo = campo & "|"
                If cabecera = "OTROS3" Then campo = campo & "|"
                If cabecera = "COMENTARIO" Then campo = campo & "|"
                If cabecera = "NCONTACTOS" Then campo = campo & "|"

                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        

        'Dim var_ruta2 As String = ""
        'Dim dt2 As DataTable = Session("tablaExportar2")
        'Dim str2 As New StringBuilder()
        'Dim k As Integer
        'str2.Append("FECHAHORA| IDBASE| IDCALLCENTER| CALLCENTER| MSISDN| IDCAMPANA| IDTIPIFICACION| NIVEL1| NIVEL2| NIVEL3| NIVEL4| IDAGENTE| COMENTARIO| OTROS2| OTROS3| NCONTACTOS")
        'str2.Append(vbNewLine)

        'For k = 0 To grvReporte3.Rows.Count - 1
        '    Dim l As Integer
        '    For l = 0 To grvReporte3.Columns.Count - 1
        '        Dim cabecera2 As String = grvReporte3.Columns(l).HeaderText.ToString
        '        Dim campo2 As String = grvReporte3.Rows(k).Cells(l).Text
        '        campo2 = Replace(campo2, "&nbsp;", "")
        '        campo2 = Replace(campo2, "á", "a")
        '        campo2 = Replace(campo2, "é", "e")
        '        campo2 = Replace(campo2, "í", "i")
        '        campo2 = Replace(campo2, "ó", "o")
        '        campo2 = Replace(campo2, "ú", "u")
        '        campo2 = Replace(campo2, "ñ", "n")
        '        If cabecera2 = "FECHAHORA" Then campo2 = campo2 & "|"
        '        If cabecera2 = "IDBASE" Then campo2 = campo2 & "|"
        '        If cabecera2 = "IDCALLCENTER" Then campo2 = campo2 & "|"
        '        If cabecera2 = "CALLCENTER" Then campo2 = campo2 & "|"
        '        If cabecera2 = "MSISDN" Then campo2 = campo2 & "|"
        '        If cabecera2 = "IDCAMPANA" Then campo2 = campo2 & "|"
        '        If cabecera2 = "IDTIPIFICACION" Then campo2 = campo2 & "|"
        '        If cabecera2 = "NIVEL1" Then campo2 = campo2 & "|"
        '        If cabecera2 = "NIVEL2" Then campo2 = campo2 & "|"
        '        If cabecera2 = "NIVEL3" Then campo2 = campo2 & "|"
        '        If cabecera2 = "NIVEL4" Then campo2 = campo2 & "|"
        '        If cabecera2 = "IDAGENTE" Then campo2 = campo2 & "|"
        '        If cabecera2 = "COMENTARIO" Then campo2 = campo2 & "|"
        '        If cabecera2 = "OTROS2" Then campo2 = campo2 & "|"
        '        If cabecera2 = "OTROS3" Then campo2 = campo2 & "|"
        '        If cabecera2 = "NCONTACTOS" Then campo2 = campo2 & "|"

        '        str2.Append(campo2)
        '    Next l

        '    str2.Append(vbNewLine)
        'Next k

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Tipif_CALL_07_" & Now.ToString("yyyyMMdd") & ".txt")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)

        'var_ruta = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "Tipif_CALL_07_" & Now.ToString("yyyyMMdd") & ".txt"
        'Dim tw As IO.TextWriter = New IO.StreamWriter(var_ruta, False, System.Text.Encoding.ASCII)
        'tw.Write(stringWrite)
        'tw.Close()

        Response.Write(str.ToString())
        'Response.Write(str2.ToString())
        Response.End()
    End Sub

    Protected Sub grvReporte_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte.PageIndexChanging
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
    End Sub

    Protected Sub lnkExpoExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExpoExcel.Click
        If grvReporte.Rows.Count > 0 Then
            EXPORTAR()
        End If
    End Sub


End Class
