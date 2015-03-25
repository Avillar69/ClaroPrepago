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
        'lnkExportar.Visible = False
        lnkExpoExcel.Visible = False
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_RENOVACIONES_SUMARIZADO(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("COD_GESTION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO_DOCUMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NOMBRE_CAMPANIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NOMBRE CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("GENERO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ANIONACIMIENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("COD_AREA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FONO_COMERCIAL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FONO_CELULAR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DIRECCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CIUDAD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("LOCALIDAD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELEFONO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MOTIVO", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("SUBMOTIVO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBS_AGENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("COMODIN", Type.GetType("System.String"))
                dtHistorial.Columns.Add("ADICIONALES", Type.GetType("System.String"))
                'dtHistorial.Columns.Add("ID", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CUENTA_CON_MAIL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NRO_LLAMADAS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MESES_PERMANENCIA", Type.GetType("System.String"))


                Dim dtScripting As DataTable = da.SP_LISTAR_RENOVACIONES_SUMARIZADO()

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then
                            dtHistorial.Rows(i)("COD_GESTION") = dtScripting.Rows(j)("COD_GESTION").ToString
                            dtHistorial.Rows(i)("NRO_DOCUMENTO") = dtScripting.Rows(j)("NRO_DOCUMENTO").ToString
                            dtHistorial.Rows(i)("NOMBRE_CAMPANIA") = dtScripting.Rows(j)("NOMBRE_CAMPANIA").ToString
                            dtHistorial.Rows(i)("NOMBRE CLIENTE") = dtScripting.Rows(j)("NOMBRE CLIENTE").ToString
                            dtHistorial.Rows(i)("GENERO") = dtScripting.Rows(j)("GENERO").ToString
                            dtHistorial.Rows(i)("ANIONACIMIENTO") = dtScripting.Rows(j)("ANIONACIMIENTO").ToString
                            dtHistorial.Rows(i)("COD_AREA") = dtScripting.Rows(j)("COD_AREA").ToString
                            dtHistorial.Rows(i)("FONO_COMERCIAL") = dtScripting.Rows(j)("FONO_COMERCIAL").ToString
                            dtHistorial.Rows(i)("FONO_CELULAR") = dtScripting.Rows(j)("FONO_CELULAR").ToString
                            dtHistorial.Rows(i)("DIRECCION") = dtScripting.Rows(j)("DIRECCION").ToString
                            dtHistorial.Rows(i)("CIUDAD") = dtScripting.Rows(j)("CIUDAD").ToString
                            dtHistorial.Rows(i)("LOCALIDAD") = dtScripting.Rows(j)("LOCALIDAD").ToString
                            dtHistorial.Rows(i)("TELEFONO") = dtScripting.Rows(j)("TELEFONO").ToString
                            dtHistorial.Rows(i)("MOTIVO") = dtScripting.Rows(j)("MOTIVO").ToString
                            'dtHistorial.Rows(i)("SUBMOTIVO") = dtScripting.Rows(j)("SUBMOTIVO").ToString
                            dtHistorial.Rows(i)("OBS_AGENTE") = dtScripting.Rows(j)("OBS_AGENTE").ToString
                            dtHistorial.Rows(i)("COMODIN") = dtScripting.Rows(j)("COMODIN").ToString
                            dtHistorial.Rows(i)("ADICIONALES") = dtScripting.Rows(j)("ADICIONALES").ToString
                            'dtHistorial.Rows(i)("ID") = dtScripting.Rows(j)("ID").ToString
                            dtHistorial.Rows(i)("CUENTA_CON_MAIL") = dtScripting.Rows(j)("CUENTA_CON_MAIL").ToString
                            dtHistorial.Rows(i)("NRO_LLAMADAS") = dtScripting.Rows(j)("NRO_LLAMADAS").ToString
                            dtHistorial.Rows(i)("MESES_PERMANENCIA") = dtScripting.Rows(j)("MESES_PERMANENCIA").ToString
                        End If
                    Next
                Next

                grvReporte.DataSource = dtHistorial
                Session("tablaExportar") = dtHistorial
                grvReporte.DataBind()
                'lnkExportar.Visible = True
                lnkExpoExcel.Visible = True
                lblMsg.Text = "Cantidad de registros encontrados: " & dtHistorial.Rows.Count
            Else
                lblMsg.Text = "No hay datos con parametro de busqueda"

            End If
        Catch ex As Exception
            lblMsg.Text = "btnBuscar " & ex.Message
        End Try
    End Sub

    'Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
    '    ExportarTxt()
    'End Sub
    Sub EXPORTAR()
        Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"
        Response.ClearContent()
        Response.Write(style)
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
        Dim file As String = "Renovaciones_Sumarizado" & Now.ToString("yyyyMMddHHmmss")
        For i As Integer = 0 To grvReporte.Rows.Count - 1
            Dim row As GridViewRow = grvReporte.Rows(i)
            ''Apply text style to each Row
            row.Cells(0).Attributes.Add("class", "textmode")
            row.Cells(7).Attributes.Add("class", "textmode")
            row.Cells(13).Attributes.Add("class", "textmode")
            row.Cells(14).Attributes.Add("class", "textmode")
            row.Cells(15).Attributes.Add("class", "textmode")
            row.Cells(17).Attributes.Add("class", "textmode")
        Next
        Response.AddHeader("content-disposition", "attachment; filename=" & file & ".xls")
        Response.ContentType = "application/vnd.ms-excel"
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
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sw.ToString())
        Response.Flush()
        Response.End()
    End Sub

    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("FECHAHORA| IDBASE| IDCALLCENTER| CALLCENTER| MSISDN| IDCAMPANA| IDTIPIFICACION| NIVEL1| NIVEL2| NIVEL3| IDAGENTE| COMENTARIO| NCONTACTOS")
        str.Append(vbNewLine)

        For i = 0 To grvReporte.Rows.Count - 1
            Dim j As Integer
            For j = 0 To grvReporte.Columns.Count - 1
                Dim cabecera As String = grvReporte.Columns(j).HeaderText.ToString
                Dim campo As String = grvReporte.Rows(i).Cells(j).Text
                campo = Replace(campo, "&nbsp;", "")
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
                If cabecera = "IDAGENTE" Then campo = campo & "|"
                If cabecera = "COMENTARIO" Then campo = campo & "|"
                If cabecera = "NCONTACTOS" Then campo = campo & "|"

                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Tipif_CALL_07_" & Now.ToString("yyyyMMdd") & ".txt")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
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
