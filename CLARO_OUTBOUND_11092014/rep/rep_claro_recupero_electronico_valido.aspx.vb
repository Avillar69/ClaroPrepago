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
        lnkExportarCsv.Visible = False
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_RECUPERO_ELECTRONICO(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("TXT_OBSERVACIONES", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CUENTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_PROVINCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DISTRITO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEF_CELULAR", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_PLAN", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DNI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_EMAIL_ERRADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CICLO_FACT", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACEPTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_EMAIL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_DOMINIO_MAIL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_EMAIL_COMPLETO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_MOTIVO_NO_ACT", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO_CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_RESULTADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_REFERENCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA_EVALUACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("LOGIN_CALIDAD", Type.GetType("System.String"))
                dtHistorial.Columns.Add("OBS_CALIDAD", Type.GetType("System.String"))



                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_RECUPERO_ELECTRONICO(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("TXT_OBSERVACIONES") = dtScripting.Rows(j)("TXT_OBSERVACIONES").ToString
                            dtHistorial.Rows(i)("D_CUENTA") = dtScripting.Rows(j)("D_CUENTA").ToString
                            dtHistorial.Rows(i)("D_CLIENTE") = dtScripting.Rows(j)("D_CLIENTE").ToString
                            dtHistorial.Rows(i)("D_CONTACTO") = dtScripting.Rows(j)("D_CONTACTO").ToString
                            dtHistorial.Rows(i)("D_DEPARTAMENTO") = dtScripting.Rows(j)("D_DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("D_PROVINCIA") = dtScripting.Rows(j)("D_PROVINCIA").ToString
                            dtHistorial.Rows(i)("D_DISTRITO") = dtScripting.Rows(j)("D_DISTRITO").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_1") = dtScripting.Rows(j)("D_TELEFONO_01").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_2") = dtScripting.Rows(j)("D_TELEFONO_02").ToString
                            dtHistorial.Rows(i)("D_TELEF_CELULAR") = dtScripting.Rows(j)("D_TELEF_CELULAR").ToString
                            dtHistorial.Rows(i)("D_PLAN") = dtScripting.Rows(j)("D_PLAN").ToString
                            dtHistorial.Rows(i)("D_DNI") = dtScripting.Rows(j)("D_DNI").ToString
                            dtHistorial.Rows(i)("D_EMAIL_ERRADO") = dtScripting.Rows(j)("D_EMAIL_ERRADO").ToString
                            dtHistorial.Rows(i)("D_CICLO_FACT") = dtScripting.Rows(j)("D_CICLO_FACT").ToString
                            dtHistorial.Rows(i)("CBO_ACEPTA") = dtScripting.Rows(j)("CBO_ACEPTA").ToString
                            dtHistorial.Rows(i)("TXT_EMAIL") = dtScripting.Rows(j)("TXT_EMAIL").ToString
                            dtHistorial.Rows(i)("CBO_DOMINIO_MAIL") = dtScripting.Rows(j)("CBO_DOMINIO_MAIL").ToString
                            dtHistorial.Rows(i)("TXT_EMAIL_COMPLETO") = dtScripting.Rows(j)("TXT_EMAIL_COMPLETO").ToString
                            dtHistorial.Rows(i)("TXT_MOTIVO_NO_ACT") = dtScripting.Rows(j)("TXT_MOTIVO_NO_ACT").ToString
                            dtHistorial.Rows(i)("CBO_TIPO_CONTACTO") = dtScripting.Rows(j)("CBO_TIPO_CONTACTO").ToString
                            dtHistorial.Rows(i)("CBO_RESULTADO") = dtScripting.Rows(j)("CBO_RESULTADO").ToString
                            dtHistorial.Rows(i)("TXT_REFERENCIA") = dtScripting.Rows(j)("TXT_REFERENCIA").ToString
                            dtHistorial.Rows(i)("FECHA_EVALUACION") = dtScripting.Rows(j)("FECHA_EVALUACION").ToString
                            dtHistorial.Rows(i)("LOGIN_CALIDAD") = dtScripting.Rows(j)("LOGIN_CALIDAD").ToString
                            dtHistorial.Rows(i)("OBS_CALIDAD") = dtScripting.Rows(j)("OBS_CALIDAD").ToString

                        End If
                    Next
                Next
                If dtHistorial.Rows.Count > 0 Then
                    Session("tablaExportar") = dtHistorial
                    Session("tablaExportar") = dtHistorial
                    grvReporte.DataSource = dtHistorial
                    grvReporte.DataBind()
                    'ExportarTxt()
                    lnkExportar.Visible = True
                    lnkExportarCsv.Visible = True
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
        Dim nombre As String = "RecuperoElectronico"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID_LOG, FECHA, LOADID, ID, ID_FINAL, FINAL, LOGIN, TELEFONO, TALKTIME, ACWTIME,TXT_OBSERVACIONES,D_CUENTA,D_CLIENTE,D_CONTACTO,D_DEPARTAMENTO,D_PROVINCIA,D_DISTRITO,D_TELEFONO_1,D_TELEFONO_2,D_TELEF_CELULAR,D_PLAN,D_DNI,D_EMAIL_ERRADO,D_CICLO_FACT,CBO_ACEPTA,TXT_EMAIL,CBO_DOMINIO_MAIL,TXT_EMAIL_COMPLETO,TXT_MOTIVO_NO_ACT,CBO_TIPO_CONTACTO,CBO_RESULTADO,TXT_REFERENCIA,FECHA_EVALUACION,LOGIN_CALIDAD,OBS_CALIDAD")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "ID_LOG" Then campo = campo & " ,"
                If cabecera = "FECHA" Then campo = campo & " ,"
                If cabecera = "LOADID" Then campo = campo & " ,"
                If cabecera = "ID" Then campo = campo & " ,"
                If cabecera = "ID_FINAL" Then campo = campo & " ,"
                If cabecera = "FINAL" Then campo = campo & " ,"
                If cabecera = "LOGIN" Then campo = campo & " ,"
                If cabecera = "TELEFONO" Then campo = campo & " ,"
                If cabecera = "TALKTIME" Then campo = campo & " ,"
                If cabecera = "ACWTIME" Then campo = campo & " ,"
                If cabecera = "TXT_OBSERVACIONES" Then campo = campo & "  ,"
                If cabecera = "D_CUENTA" Then campo = campo & "  ,"
                If cabecera = "D_CLIENTE" Then campo = campo & "  ,"
                If cabecera = "D_CONTACTO" Then campo = campo & "  ,"
                If cabecera = "D_DEPARTAMENTO" Then campo = campo & "  ,"
                If cabecera = "D_PROVINCIA" Then campo = campo & "  ,"
                If cabecera = "D_DISTRITO" Then campo = campo & "  ,"
                If cabecera = "D_TELEFONO_1" Then campo = campo & "  ,"
                If cabecera = "D_TELEFONO_2" Then campo = campo & "  ,"
                If cabecera = "D_TELEF_CELULAR" Then campo = campo & "  ,"
                If cabecera = "D_PLAN" Then campo = campo & "  ,"
                If cabecera = "D_DNI" Then campo = campo & "  ,"
                If cabecera = "D_EMAIL_ERRADO" Then campo = campo & "  ,"
                If cabecera = "D_CICLO_FACT" Then campo = campo & "  ,"
                If cabecera = "CBO_ACEPTA" Then campo = campo & "  ,"
                If cabecera = "TXT_EMAIL" Then campo = campo & "  ,"
                If cabecera = "CBO_DOMINIO_MAIL" Then campo = campo & "  ,"
                If cabecera = "TXT_EMAIL_COMPLETO" Then campo = campo & "  ,"
                If cabecera = "TXT_MOTIVO_NO_ACT" Then campo = campo & "  ,"
                If cabecera = "CBO_TIPO_CONTACTO" Then campo = campo & "  ,"
                If cabecera = "CBO_RESULTADO" Then campo = campo & "  ,"
                If cabecera = "TXT_REFERENCIA" Then campo = campo & "  ,"
                If cabecera = "FECHA_EVALUACION" Then campo = campo & "  ,"
                If cabecera = "LOGIN_CALIDAD" Then campo = campo & "  ,"
                If cabecera = "OBS_CALIDAD" Then campo = campo & "  ,"
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=RecuperoElectronico.csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()

    End Sub

    Protected Sub lnkExportarCsv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportarCsv.Click
        If grvReporte.Rows.Count > 0 Then
            ExportarTxt()
        End If
    End Sub

    Protected Sub grvReporte_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte.PageIndexChanging
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
    End Sub
End Class
