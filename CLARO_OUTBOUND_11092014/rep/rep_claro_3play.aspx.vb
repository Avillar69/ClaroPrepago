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

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_3PLAY(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("D_SEMANA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_COD_CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NOMBRE_CLI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_SOLUCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DIRECCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DISTRITO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_01", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_02", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_03", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_04", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_TELEFONO_05", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_PROYECTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_NRO_DOCUMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_SERVICIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_REFERENCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_DEPARTAMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_FEC_INSTALACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("D_PROVINCIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_PRODUCTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO_LLAMADA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_SUBTIPO_LLAMADA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_PROB_TECNICO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_NO_CONFORME", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_SOLICITUD_PEN", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_INTERNET", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TELEFONIA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TV", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_INTER_TELEF", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_INTER_TV", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_INTER_TV_TELEF", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_RECIBO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ESTADO_PAGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_ESTADO_RECIBO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_CORREO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_AFILIA_RECIBO_ELEC", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_PORQUE_NO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_REALIZA_DESCARTE_ADM", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_DETECTA_PROB_ADM", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_NO_DETEC_PROB_ADM", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_FRENTE_A_EQ", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_NO_FRENTE_A_EQ", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_CODIGO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NOM_CLI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_NRO_NOM_CONTACTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_SERVI_AFECTADO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_CAMBIO_EQ", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_HORARIO_LLAMADA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("DTP_FEC_INSTALACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_OBSERVACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_PROB_TEC_CORREO", Type.GetType("System.String"))


                Dim IDS As String = ""
                For i = 0 To dtHistorial.Rows.Count - 1
                    IDS = IDS & "" & dtHistorial.Rows(i)("ID").ToString & ","
                Next
                IDS = Microsoft.VisualBasic.Left(IDS, Len(IDS) - 1)

                Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_3PLAY(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("D_SEMANA") = dtScripting.Rows(j)("D_SEMANA").ToString
                            dtHistorial.Rows(i)("D_COD_CLIENTE") = dtScripting.Rows(j)("D_COD_CLIENTE").ToString
                            dtHistorial.Rows(i)("D_NOMBRE_CLI") = dtScripting.Rows(j)("D_NOMBRE_CLI").ToString
                            dtHistorial.Rows(i)("D_SOLUCION") = dtScripting.Rows(j)("D_SOLUCION").ToString
                            dtHistorial.Rows(i)("D_DIRECCION") = dtScripting.Rows(j)("D_DIRECCION").ToString
                            dtHistorial.Rows(i)("D_DISTRITO") = dtScripting.Rows(j)("D_DISTRITO").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_01") = dtScripting.Rows(j)("D_TELEFONO_01").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_02") = dtScripting.Rows(j)("D_TELEFONO_02").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_03") = dtScripting.Rows(j)("D_TELEFONO_03").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_04") = dtScripting.Rows(j)("D_TELEFONO_04").ToString
                            dtHistorial.Rows(i)("D_TELEFONO_05") = dtScripting.Rows(j)("D_TELEFONO_05").ToString
                            dtHistorial.Rows(i)("D_PROYECTO") = dtScripting.Rows(j)("D_PROYECTO").ToString
                            dtHistorial.Rows(i)("D_NRO_DOCUMENTO") = dtScripting.Rows(j)("D_NRO_DOCUMENTO").ToString
                            dtHistorial.Rows(i)("D_SERVICIO") = dtScripting.Rows(j)("D_SERVICIO").ToString
                            dtHistorial.Rows(i)("D_REFERENCIA") = dtScripting.Rows(j)("D_REFERENCIA").ToString
                            dtHistorial.Rows(i)("D_DEPARTAMENTO") = dtScripting.Rows(j)("D_DEPARTAMENTO").ToString
                            dtHistorial.Rows(i)("D_FEC_INSTALACION") = dtScripting.Rows(j)("D_FEC_INSTALACION").ToString
                            dtHistorial.Rows(i)("D_PROVINCIA") = dtScripting.Rows(j)("D_PROVINCIA").ToString
                            dtHistorial.Rows(i)("CBO_PRODUCTO") = dtScripting.Rows(j)("CBO_PRODUCTO").ToString
                            dtHistorial.Rows(i)("CBO_TIPO") = dtScripting.Rows(j)("CBO_TIPO").ToString
                            dtHistorial.Rows(i)("CBO_TIPO_LLAMADA") = dtScripting.Rows(j)("CBO_TIPO_LLAMADA").ToString
                            dtHistorial.Rows(i)("CBO_SUBTIPO_LLAMADA") = dtScripting.Rows(j)("CBO_SUBTIPO_LLAMADA").ToString
                            dtHistorial.Rows(i)("CBO_PROB_TECNICO") = dtScripting.Rows(j)("CBO_PROB_TECNICO").ToString
                            dtHistorial.Rows(i)("CBO_NO_CONFORME") = dtScripting.Rows(j)("CBO_NO_CONFORME").ToString
                            dtHistorial.Rows(i)("CBO_SOLICITUD_PEN") = dtScripting.Rows(j)("CBO_SOLICITUD_PEN").ToString
                            dtHistorial.Rows(i)("CBO_INTERNET") = dtScripting.Rows(j)("CBO_INTERNET").ToString
                            dtHistorial.Rows(i)("CBO_TELEFONIA") = dtScripting.Rows(j)("CBO_TELEFONIA").ToString
                            dtHistorial.Rows(i)("CBO_TV") = dtScripting.Rows(j)("CBO_TV").ToString
                            dtHistorial.Rows(i)("CBO_INTER_TELEF") = dtScripting.Rows(j)("CBO_INTER_TELEF").ToString
                            dtHistorial.Rows(i)("CBO_INTER_TV") = dtScripting.Rows(j)("CBO_INTER_TV").ToString
                            dtHistorial.Rows(i)("CBO_INTER_TV_TELEF") = dtScripting.Rows(j)("CBO_INTER_TV_TELEF").ToString
                            dtHistorial.Rows(i)("CBO_RECIBO") = dtScripting.Rows(j)("CBO_RECIBO").ToString
                            dtHistorial.Rows(i)("CBO_ESTADO_PAGO") = dtScripting.Rows(j)("CBO_ESTADO_PAGO").ToString
                            dtHistorial.Rows(i)("CBO_ESTADO_RECIBO") = dtScripting.Rows(j)("CBO_ESTADO_RECIBO").ToString
                            dtHistorial.Rows(i)("TXT_CORREO") = dtScripting.Rows(j)("TXT_CORREO").ToString
                            dtHistorial.Rows(i)("CBO_AFILIA_RECIBO_ELEC") = dtScripting.Rows(j)("CBO_AFILIA_RECIBO_ELEC").ToString
                            dtHistorial.Rows(i)("TXT_PORQUE_NO") = dtScripting.Rows(j)("TXT_PORQUE_NO").ToString
                            dtHistorial.Rows(i)("CBO_REALIZA_DESCARTE_ADM") = dtScripting.Rows(j)("CBO_REALIZA_DESCARTE_ADM").ToString
                            dtHistorial.Rows(i)("CBO_DETECTA_PROB_ADM") = dtScripting.Rows(j)("CBO_DETECTA_PROB_ADM").ToString
                            dtHistorial.Rows(i)("CBO_NO_DETEC_PROB_ADM") = dtScripting.Rows(j)("CBO_NO_DETEC_PROB_ADM").ToString
                            dtHistorial.Rows(i)("CBO_FRENTE_A_EQ") = dtScripting.Rows(j)("CBO_FRENTE_A_EQ").ToString
                            dtHistorial.Rows(i)("CBO_NO_FRENTE_A_EQ") = dtScripting.Rows(j)("CBO_NO_FRENTE_A_EQ").ToString
                            dtHistorial.Rows(i)("TXT_CODIGO") = dtScripting.Rows(j)("TXT_CODIGO").ToString
                            dtHistorial.Rows(i)("TXT_NOM_CLI") = dtScripting.Rows(j)("TXT_NOM_CLI").ToString
                            dtHistorial.Rows(i)("TXT_NRO_NOM_CONTACTO") = dtScripting.Rows(j)("TXT_NRO_NOM_CONTACTO").ToString
                            dtHistorial.Rows(i)("CBO_SERVI_AFECTADO") = dtScripting.Rows(j)("CBO_SERVI_AFECTADO").ToString
                            dtHistorial.Rows(i)("TXT_CAMBIO_EQ") = dtScripting.Rows(j)("TXT_CAMBIO_EQ").ToString
                            dtHistorial.Rows(i)("TXT_HORARIO_LLAMADA") = dtScripting.Rows(j)("TXT_HORARIO_LLAMADA").ToString
                            dtHistorial.Rows(i)("DTP_FEC_INSTALACION") = dtScripting.Rows(j)("DTP_FEC_INSTALACION").ToString
                            dtHistorial.Rows(i)("TXT_OBSERVACION") = dtScripting.Rows(j)("TXT_OBSERVACION").ToString
                            dtHistorial.Rows(i)("TXT_PROB_TEC_CORREO") = dtScripting.Rows(j)("TXT_PROB_TEC_CORREO").ToString

                        End If
                    Next
                Next

                For i = 0 To dtHistorial.Rows.Count()

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
        grvReporte2.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvReporte2)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim nombre As String = "3Play"
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

    Protected Sub grvReporte2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte2.PageIndexChanging
        'grvReporte.PageIndexChanging()
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
    End Sub
End Class
