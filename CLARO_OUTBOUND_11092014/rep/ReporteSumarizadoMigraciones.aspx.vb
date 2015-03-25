Imports System.Data
Imports System.IO

Partial Class rep_ReporteSumarizadoMigraciones
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO
    Dim be2 As New BE_CLARO_MIGRA

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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


            Dim dtHistorial As DataTable = da.SP_REPORTE_SUMARIZADO_MIGRACIONES(be2)
            If dtHistorial.Rows.Count > 0 Then

                'dtHistorial.Rows(i)("ID") = dtScripting.Rows(j)("ID").ToString

                dtHistorial.Rows(0)("COD GESTION").ToString()
                dtHistorial.Rows(1)("NRO DOCUMENTO").ToString()
                dtHistorial.Rows(2)("NOMBRE CAMPANYA").ToString()
                dtHistorial.Rows(3)("NOMBRE CLIENTE").ToString()
                dtHistorial.Rows(4)("GENERO").ToString()
                dtHistorial.Rows(5)("FECHA NAC").ToString()
                dtHistorial.Rows(6)("COD AREA").ToString()
                dtHistorial.Rows(7)("NRO PARTICULAR").ToString()
                dtHistorial.Rows(8)("NRO COMERCIAL").ToString()
                dtHistorial.Rows(9)("NRO CELULAR").ToString()
                dtHistorial.Rows(10)("DIRECCION").ToString()
                dtHistorial.Rows(11)("CIUDAD").ToString()
                dtHistorial.Rows(12)("LOCALIDAD").ToString()
                dtHistorial.Rows(13)("INICIO LLAMADA").ToString()
                dtHistorial.Rows(14)("FIN LLAMADA").ToString()
                dtHistorial.Rows(15)("TELEFONO").ToString()
                dtHistorial.Rows(16)("CELULAR 2").ToString()
                dtHistorial.Rows(17)("DURACION").ToString()
                dtHistorial.Rows(18)("LOGIN").ToString()
                dtHistorial.Rows(19)("MOTIVO").ToString()
                dtHistorial.Rows(20)("SUB MOTIVO").ToString()
                dtHistorial.Rows(21)("SOURCEID").ToString()
                dtHistorial.Rows(22)("SERVICEID").ToString()
                dtHistorial.Rows(23)("FINAL").ToString()
                dtHistorial.Rows(24)("OBS AGENTE").ToString()
                dtHistorial.Rows(25)("CODIGO CARGA").ToString()
                dtHistorial.Rows(26)("COMODIN").ToString()
                dtHistorial.Rows(27)("ADICIONALES").ToString()
                dtHistorial.Rows(28)("ID").ToString()
                dtHistorial.Rows(29)("CUENTA CON CORREO").ToString()
                dtHistorial.Rows(30)("NRO LLAMADAS").ToString()
                dtHistorial.Rows(31)("MESES PERMANENCIA").ToString()
                dtHistorial.Rows(32)("BASE ORIGEN").ToString()


                grvReporte.DataSource = dtHistorial
                Session("tablaExportarMigra") = dtHistorial
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
        grvReporte3.DataSource = Nothing
        grvReporte3.DataSource = Session("tablaExportarMigra")
        grvReporte3.DataBind()
        grvReporte3.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte3)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim mes As String = Request("Mes")
        Dim anio As String = Request("Ano")
        Dim nombre As String = "RetencionesOut"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=ReporteSumarizadoMigraciones.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Sub ExportarTxt()
        Dim var_ruta As String = ""
        Dim dt As DataTable = Session("tablaExportarMigra")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("FECHAHORA| IDBASE| IDCALLCENTER| CALLCENTER| MSISDN| IDCAMPANA| IDTIPIFICACION| NIVEL1| NIVEL2| NIVEL3| NIVEL4| IDAGENTE| COMENTARIO| OTROS2| OTROS3| NCONTACTOS")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
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
                If cabecera = "COMENTARIO" Then campo = campo & "|"
                If cabecera = "OTROS2" Then campo = campo & "|"
                If cabecera = "OTROS3" Then campo = campo & "|"
                If cabecera = "NCONTACTOS" Then campo = campo & "|"

                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i


        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Sumarizado_Migra_" & Now.ToString("yyyyMMdd") & ".txt")
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
        grvReporte.DataSource = Session("tablaExportarMigra")
        grvReporte.DataBind()
    End Sub

    Protected Sub lnkExpoExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExpoExcel.Click
        If grvReporte.Rows.Count > 0 Then
            EXPORTAR()
        End If
    End Sub

End Class
