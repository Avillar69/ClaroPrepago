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
        If (txtCarga.Text.Trim() = "") Then
            lblMsg.Text = "Ingresar el nro de carga"
            txtCarga.Focus()
        Else
            Session("tablaExportar") = Nothing
            lnkExportar.Visible = False
            grvReporte.DataSource = Nothing
            grvReporte.DataBind()
            lblMsg.Text = ""
            Try
                be.inicio = txtInicio.Text
                be.fin = txtFin.Text
                be.VAR_ID_CARGA = txtCarga.Text

                Dim dt1 As DataTable = da.SP_CLARO_3PLAY_HFC_BLOQUEO_P1(be)

                If dt1.Rows.Count > 0 Then
                    Dim IDS As String = ""
                    For i = 0 To dt1.Rows.Count - 1
                        IDS = IDS & "" & dt1.Rows(i)("ID").ToString & ","
                    Next
                    IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)
                    be.VAR_IDS = IDS
                    Dim dt2 As DataTable = da.SP_CLARO_3PLAY_HFC_BLOQUEO_P2(be)

                    If dt2.Rows.Count > 0 Then
                        Dim COD_CLIE As String = ""
                        For i = 0 To dt2.Rows.Count - 1
                            COD_CLIE = COD_CLIE & "" & dt2.Rows(i)("COD_CLI").ToString & ","
                        Next
                        COD_CLIE = Microsoft.VisualBasic.Left(COD_CLIE, Len(COD_CLIE) - 1)
                        be.VAR_CODS_CLI = COD_CLIE
                        Dim dt3 As DataTable = da.SP_CLARO_3PLAY_HFC_BLOQUEO_P3(be)

                        If dt3.Rows.Count > 0 Then
                            Dim IDS2 As String = ""
                            For i = 0 To dt3.Rows.Count - 1
                                IDS2 = IDS2 & "" & dt3.Rows(i)("ID").ToString & ","
                            Next
                            IDS2 = Microsoft.VisualBasic.Left(IDS2, Len(IDS2) - 1)
                            be.VAR_CODS_CLI = IDS2
                            Dim dt4 As DataTable = da.SP_CLARO_3PLAY_HFC_BLOQUEO_P4(be)
                            If dt4.Rows.Count > 0 Then
                                Session("tablaExportar") = dt4
                                grvReporte.DataSource = dt4
                                grvReporte.DataBind()
                                lnkExportar.Visible = True
                                lblMsg.Text = "Cantidad de registros encontrados: " & dt4.Rows.Count
                            Else
                                lblMsg.Text = "No hay datos con parametro de busqueda"
                                txtCarga.Text = ""
                                txtCarga.Focus()
                            End If
                        Else
                            lblMsg.Text = "No se encontraron registros de los ID de carga de la Base nro : " & txtCarga.Text
                            txtCarga.Text = ""
                            txtCarga.Focus()
                        End If
                    Else
                        lblMsg.Text = "No se encontraron registros en la carga de la Base nro : " & txtCarga.Text
                        txtCarga.Text = ""
                        txtCarga.Focus()
                    End If
                Else
                    lblMsg.Text = "No se encontraron registros en el Log de Presence."
                    txtCarga.Text = ""
                    txtCarga.Focus()
                End If
            Catch ex As Exception
                lblMsg.Text = "Error: " & ex.Message
                txtCarga.Focus()
            End Try
        End If
    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
        ExportarTxt()
    End Sub
    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("CARGA, SERVICIO, ID")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "CARGA" Then campo = campo & " ,"
                If cabecera = "SERVICIO" Then campo = campo & " ,"
                If cabecera = "ID" Then campo = campo & " ,"

                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i
        Dim fileT As String = Now.ToString("ddMMyyyyHHmmss")
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=BLOQ_CLARO_3PLAY_HFC" & fileT & ".csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()
    End Sub

End Class
