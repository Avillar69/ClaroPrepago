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

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_TV_RECUPERO_1(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("D_NOM_CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_CODINSSRV", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_COD_CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_ESTADOPAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_PLAN_GENERAL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_COD_RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_01", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_02", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_03", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_04", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_05", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_06", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_07", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_08", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_09", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_10", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CAPA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MOTIVO_NO_RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CHK_ACT_SERV_ACTIVO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_GENERA_COMPROMISO_RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_ACT_FEC_RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_OFER_GENERA_RECARGA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_MINUTOS_MOVIL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_ACT_CELU_CLARO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_CONTESTA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_ACT_MOTIVO_BAJA_1", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_ACT_MOTIVO_BAJA_2", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_COMPROMISO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ACT_PORQUE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_ACT_VE_ACTUALMENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_ACT_NAME_COMPETENCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_ACT_XQ_A_COMPETENCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OBSERVACIONES", Type.GetType("System.String"))


                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_TV_RECUPERO_1(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("D_NOM_CLIENTE") = dtScripting.Rows(j)("D_NOM_CLIENTE").ToString
                            dtHistorial.Rows(i)("D_CODINSSRV") = dtScripting.Rows(j)("D_CODINSSRV").ToString
                            dtHistorial.Rows(i)("D_COD_CLIENTE") = dtScripting.Rows(j)("D_COD_CLIENTE").ToString
                            dtHistorial.Rows(i)("D_ESTADOPAGO") = dtScripting.Rows(j)("D_ESTADOPAGO").ToString
                            dtHistorial.Rows(i)("D_PLAN_GENERAL") = dtScripting.Rows(j)("D_PLAN_GENERAL").ToString
                            dtHistorial.Rows(i)("D_DEPARTAMENTO") = dtScripting.Rows(j)("D_DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("D_COD_RECARGA") = dtScripting.Rows(j)("D_COD_RECARGA").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_01") = dtScripting.Rows(j)("D_TELEFONO_01").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_02") = dtScripting.Rows(j)("D_TELEFONO_02").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_03") = dtScripting.Rows(j)("D_TELEFONO_03").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_04") = dtScripting.Rows(j)("D_TELEFONO_04").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_05") = dtScripting.Rows(j)("D_TELEFONO_05").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_06") = dtScripting.Rows(j)("D_TELEFONO_06").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_07") = dtScripting.Rows(j)("D_TELEFONO_07").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_08") = dtScripting.Rows(j)("D_TELEFONO_08").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_09") = dtScripting.Rows(j)("D_TELEFONO_09").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_10") = dtScripting.Rows(j)("D_TELEFONO_10").ToString
                            dtHistorial.Rows(i)("CAPA") = dtScripting.Rows(j)("CAPA").ToString
                            dtHistorial.Rows(i)("CBO_MOTIVO_NO_RECARGA") = dtScripting.Rows(j)("CBO_MOTIVO_NO_RECARGA").ToString
                            dtHistorial.Rows(i)("CHK_ACT_SERV_ACTIVO") = dtScripting.Rows(j)("CHK_ACT_SERV_ACTIVO").ToString
                            dtHistorial.Rows(i)("CBO_ACT_GENERA_COMPROMISO_RECARGA") = dtScripting.Rows(j)("CBO_ACT_GENERA_COMPROMISO_RECARGA").ToString
                            dtHistorial.Rows(i)("DTP_ACT_FEC_RECARGA") = dtScripting.Rows(j)("DTP_ACT_FEC_RECARGA").ToString
                            dtHistorial.Rows(i)("CBO_OFER_GENERA_RECARGA") = dtScripting.Rows(j)("CBO_OFER_GENERA_RECARGA").ToString
                            dtHistorial.Rows(i)("CBO_MINUTOS_MOVIL") = dtScripting.Rows(j)("CBO_MINUTOS_MOVIL").ToString
                            dtHistorial.Rows(i)("TXT_ACT_CELU_CLARO") = dtScripting.Rows(j)("TXT_ACT_CELU_CLARO").ToString
                            dtHistorial.Rows(i)("CBO_ACT_CONTESTA") = dtScripting.Rows(j)("CBO_ACT_CONTESTA").ToString
                            dtHistorial.Rows(i)("TXT_ACT_MOTIVO_BAJA_1") = dtScripting.Rows(j)("TXT_ACT_MOTIVO_BAJA_1").ToString
                            dtHistorial.Rows(i)("TXT_ACT_MOTIVO_BAJA_2") = dtScripting.Rows(j)("TXT_ACT_MOTIVO_BAJA_2").ToString
                            dtHistorial.Rows(i)("CBO_ACT_COMPROMISO") = dtScripting.Rows(j)("CBO_ACT_COMPROMISO").ToString
                            dtHistorial.Rows(i)("CBO_ACT_PORQUE") = dtScripting.Rows(j)("CBO_ACT_PORQUE").ToString
                            dtHistorial.Rows(i)("TXT_ACT_VE_ACTUALMENTE") = dtScripting.Rows(j)("TXT_ACT_VE_ACTUALMENTE").ToString
                            dtHistorial.Rows(i)("TXT_ACT_NAME_COMPETENCIA") = dtScripting.Rows(j)("TXT_ACT_NAME_COMPETENCIA").ToString
                            dtHistorial.Rows(i)("TXT_ACT_XQ_A_COMPETENCIA") = dtScripting.Rows(j)("TXT_ACT_XQ_A_COMPETENCIA").ToString
                            dtHistorial.Rows(i)("TXT_OBSERVACIONES") = dtScripting.Rows(j)("TXT_OBSERVACIONES").ToString

                        End If
                    Next
                Next
                If dtHistorial.Rows.Count > 0 Then
                    Session("tablaExportar") = dtHistorial
                    Session("tablaExportar") = dtHistorial
                    grvReporte.DataSource = dtHistorial
                    grvReporte.DataBind()
                    'EXPORTAR()
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
        Dim nombre As String = "Tv Recupero 1"
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
        str.Append("ID_LOG, FECHA, LOADID, ID, ID_FINAL, FINAL, LOGIN, TELEFONO, TALKTIME, ACWTIME, D_NOM_CLIENTE, D_CODINSSRV, D_COD_CLIENTE, D_ESTADOPAGO, D_PLAN_GENERAL, D_DEPARTAMENTO, D_COD_RECARGA, D_TELEFONO_01, D_TELEFONO_02, D_TELEFONO_03, D_TELEFONO_04, D_TELEFONO_05, D_TELEFONO_06, D_TELEFONO_07, D_TELEFONO_08, D_TELEFONO_09, D_TELEFONO_10, CAPA, RBN_MOT_ECONOMICO, RBN_MOT_MALA_INFO, RBN_MOT_MIGRACION_SERV, RBN_MOT_MUDANZA, RBN_MOT_VIAJE, RBN_MOT_PROB_ADMIN, RBN_MOT_PT_CONGELA_IMG, RBN_MOT_PT_CORTE_SENIAL, RBN_MOT_PT_PERDIDA_CANAL, RBN_MOT_PT_MAYOR_CANT_CANALES, RBN_MOT_PT_MEJORES_CANALES, RBN_MOT_PT_PRECIOS_BAJOS, RBN_MOT_SNU_UTILIZA_ANTENA, RBN_MOT_SNU_NO_VE_TV, RBN_MOT_SNU_SOLO_VE_NACIONALES, CHK_ACT_SERV_ACTIVO, CBO_ACT_GENERA_COMPROMISO_RECARGA, DTP_ACT_FEC_RECARGA, TXT_ACT_CELU_CLARO, CBO_ACT_CONTESTA, TXT_ACT_MOTIVO_BAJA_1, TXT_ACT_MOTIVO_BAJA_2, CBO_ACT_COMPROMISO, CBO_ACT_PORQUE, TXT_ACT_VE_ACTUALMENTE, TXT_ACT_NAME_COMPETENCIA, TXT_ACT_XQ_A_COMPETENCIA, TXT_OBSERVACIONES")
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
                If cabecera = "D_NOM_CLIENTE" Then campo = campo & " ,"
                If cabecera = "D_CODINSSRV" Then campo = campo & " ,"
                If cabecera = "D_COD_CLIENTE" Then campo = campo & " ,"
                If cabecera = "D_ESTADOPAGO" Then campo = campo & " ,"
                If cabecera = "D_PLAN_GENERAL" Then campo = campo & " ,"
                If cabecera = "D_DEPARTAMENTO" Then campo = campo & " ,"
                If cabecera = "D_COD_RECARGA" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_01" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_02" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_03" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_04" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_05" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_06" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_07" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_08" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_09" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_10" Then campo = campo & " ,"
                If cabecera = "CAPA" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_ECONOMICO" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_MALA_INFO" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_MIGRACION_SERV" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_MUDANZA" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_VIAJE" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_PROB_ADMIN" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_PT_CONGELA_IMG" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_PT_CORTE_SENIAL" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_PT_PERDIDA_CANAL" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_PT_MAYOR_CANT_CANALES" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_PT_MEJORES_CANALES" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_PT_PRECIOS_BAJOS" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_SNU_UTILIZA_ANTENA" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_SNU_NO_VE_TV" Then campo = campo & " ,"
                If cabecera = "RBN_MOT_SNU_SOLO_VE_NACIONALES" Then campo = campo & " ,"
                If cabecera = "CHK_ACT_SERV_ACTIVO" Then campo = campo & " ,"
                If cabecera = "CBO_ACT_GENERA_COMPROMISO_RECARGA" Then campo = campo & " ,"
                If cabecera = "DTP_ACT_FEC_RECARGA" Then campo = campo & " ,"
                If cabecera = "TXT_ACT_CELU_CLARO" Then campo = campo & " ,"
                If cabecera = "CBO_ACT_CONTESTA" Then campo = campo & " ,"
                If cabecera = "TXT_ACT_MOTIVO_BAJA_1" Then campo = campo & " ,"
                If cabecera = "TXT_ACT_MOTIVO_BAJA_2" Then campo = campo & " ,"
                If cabecera = "CBO_ACT_COMPROMISO" Then campo = campo & " ,"
                If cabecera = "CBO_ACT_PORQUE" Then campo = campo & " ,"
                If cabecera = "TXT_ACT_VE_ACTUALMENTE" Then campo = campo & " ,"
                If cabecera = "TXT_ACT_NAME_COMPETENCIA" Then campo = campo & " ,"
                If cabecera = "TXT_ACT_XQ_A_COMPETENCIA" Then campo = campo & " ,"
                If cabecera = "TXT_OBSERVACIONES" Then campo = campo & " ,"

                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=TvRecupero1.csv")
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
