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

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_ENCUESTA_PREP(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("D_CAMPANIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CODINSSRV", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_COD_CLI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NOMBRE_CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_COD_RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_3", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_REALIZA_ENC_AHORA_O_NUNCA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MOTIVO_NO_RECARGO_AHORA_O_NUNCA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_PROBLEMAS_TECNICOS_AHORA_O_NUNCA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_COMPETENCIA_AHORA_O_NUNCA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_BENEFICIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MICRORECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_FECHA_COMPROMISO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OBSERVACIONES_AHORA_O_NUNCA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_REALIZA_ENC_FUTBOLISTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MOTIVO_NO_RECARGO_FUTBOLISTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_PROBLEMAS_TECNICOS_FUTBOLISTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_COMPETENCIA_FUTBOLISTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OBSERVACIONES_FUTBOLISTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_BENEFICIO_FUTBOLISTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_FECHA_COMPROMISO_FUTBOLISTA", Type.GetType("System.String"))

                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_ENCUESTA_PREP(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("D_CAMPANIA") = dtScripting.Rows(j)("D_CAMPANIA").ToString
                            dtHistorial.Rows(i)("D_CODINSSRV") = dtScripting.Rows(j)("D_CODINSSRV").ToString
                            dtHistorial.Rows(i)("D_COD_CLI") = dtScripting.Rows(j)("D_COD_CLI").ToString
                            dtHistorial.Rows(i)("D_NOMBRE_CLIENTE") = dtScripting.Rows(j)("D_NOMBRE_CLIENTE").ToString
                            dtHistorial.Rows(i)("D_DEPARTAMENTO") = dtScripting.Rows(j)("D_DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("D_COD_RECARGA") = dtScripting.Rows(j)("D_COD_RECARGA").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_1") = dtScripting.Rows(j)("D_TELEFONO_1").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_2") = dtScripting.Rows(j)("D_TELEFONO_2").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_3") = dtScripting.Rows(j)("D_TELEFONO_3").ToString
                            dtHistorial.Rows(i)("CBO_REALIZA_ENC_AHORA_O_NUNCA") = dtScripting.Rows(j)("CBO_REALIZA_ENC_AHORA_O_NUNCA").ToString
                            dtHistorial.Rows(i)("CBO_MOTIVO_NO_RECARGO_AHORA_O_NUNCA") = dtScripting.Rows(j)("CBO_MOTIVO_NO_RECARGO_AHORA_O_NUNCA").ToString
                            dtHistorial.Rows(i)("CBO_PROBLEMAS_TECNICOS_AHORA_O_NUNCA") = dtScripting.Rows(j)("CBO_PROBLEMAS_TECNICOS_AHORA_O_NUNCA").ToString
                            dtHistorial.Rows(i)("CBO_COMPETENCIA_AHORA_O_NUNCA") = dtScripting.Rows(j)("CBO_COMPETENCIA_AHORA_O_NUNCA").ToString
                            dtHistorial.Rows(i)("CBO_BENEFICIO") = dtScripting.Rows(j)("CBO_BENEFICIO").ToString
                            dtHistorial.Rows(i)("CBO_MICRORECARGA") = dtScripting.Rows(j)("CBO_MICRORECARGA").ToString
                            dtHistorial.Rows(i)("DTP_FECHA_COMPROMISO") = dtScripting.Rows(j)("DTP_FECHA_COMPROMISO").ToString
                            dtHistorial.Rows(i)("TXT_OBSERVACIONES_AHORA_O_NUNCA") = dtScripting.Rows(j)("TXT_OBSERVACIONES_AHORA_O_NUNCA").ToString
                            dtHistorial.Rows(i)("CBO_REALIZA_ENC_FUTBOLISTA") = dtScripting.Rows(j)("CBO_REALIZA_ENC_FUTBOLISTA").ToString
                            dtHistorial.Rows(i)("CBO_MOTIVO_NO_RECARGO_FUTBOLISTA") = dtScripting.Rows(j)("CBO_MOTIVO_NO_RECARGO_FUTBOLISTA").ToString
                            dtHistorial.Rows(i)("CBO_PROBLEMAS_TECNICOS_FUTBOLISTA") = dtScripting.Rows(j)("CBO_PROBLEMAS_TECNICOS_FUTBOLISTA").ToString
                            dtHistorial.Rows(i)("CBO_COMPETENCIA_FUTBOLISTA") = dtScripting.Rows(j)("CBO_COMPETENCIA_FUTBOLISTA").ToString
                            dtHistorial.Rows(i)("TXT_OBSERVACIONES_FUTBOLISTA") = dtScripting.Rows(j)("TXT_OBSERVACIONES_FUTBOLISTA").ToString
                            dtHistorial.Rows(i)("CBO_BENEFICIO_FUTBOLISTA") = dtScripting.Rows(j)("CBO_BENEFICIO_FUTBOLISTA").ToString
                            dtHistorial.Rows(i)("DTP_FECHA_COMPROMISO_FUTBOLISTA") = dtScripting.Rows(j)("DTP_FECHA_COMPROMISO_FUTBOLISTA").ToString

                        End If
                    Next
                Next
                If dtHistorial.Rows.Count > 0 Then
                    Session("tablaExportar") = dtHistorial
                    Session("tablaExportar") = dtHistorial
                    grvReporte.DataSource = dtHistorial
                    grvReporte.DataBind()
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
        grvReporte.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim nombre As String = "EncuestasPrepago"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
