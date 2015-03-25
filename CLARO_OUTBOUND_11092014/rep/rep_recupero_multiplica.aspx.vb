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

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_RECUPERO_MULTIPLICA(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("CBO_PREG1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_PREG2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_PREG3", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_PREG4", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OTRAS_PROMOCIONES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OBSERVACIONES", Type.GetType("System.String"))


                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_RECUPERO_MULTIPLICA(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("CBO_PREG1") = dtScripting.Rows(j)("CONOCIA_PROMOCION").ToString
                            dtHistorial.Rows(i)("CBO_PREG2") = dtScripting.Rows(j)("SABE_COMO_ACCEDER_A_PROMO").ToString
                            dtHistorial.Rows(i)("CBO_PREG3") = dtScripting.Rows(j)("HA_USADO_PROMO_ALGUNA_VEZ").ToString
                            dtHistorial.Rows(i)("CBO_PREG4") = dtScripting.Rows(j)("XQ_DEJO_USAR_PROMO").ToString
                            dtHistorial.Rows(i)("TXT_OTRAS_PROMOCIONES") = dtScripting.Rows(j)("OTRAS_PROMOCIONES").ToString
                            dtHistorial.Rows(i)("TXT_OBSERVACIONES") = dtScripting.Rows(j)("OBSERVACIONES").ToString

                        End If
                    Next
                Next
                If dtHistorial.Rows.Count > 0 Then
                    grvReporte.DataSource = dtHistorial
                    grvReporte.DataBind()
                    EXPORTAR()
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
        Dim nombre As String = "Recupero Multiplica"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
