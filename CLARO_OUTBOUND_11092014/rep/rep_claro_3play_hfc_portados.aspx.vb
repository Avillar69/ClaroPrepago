Imports System.Data
Imports System.IO
Imports System.Drawing

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
        Session("tablaCarga") = Nothing
        Session("ids") = Nothing
        lnkExportar1.Visible = False
        lnkExportar2.Visible = False
        grvReporte.DataSource = Nothing
        grvExport.DataSource = Nothing
        grvReporte.DataBind()
        grvExport.DataBind()
        lblMsg.Text = ""
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            Dim dtHistorial As DataTable = da.SP_HISTORIAL_CLARO_3PLAY_HFC_PORTADOS(be)
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
                dtHistorial.Columns.Add("TXT_TITULAR_USUARIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TXT_TELEF_ADICIONAL", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_PRODUCTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_TIPO_LLAMADA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_SUBTIPO_LLAMADA", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_PROB_TECNICO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_NO_CONFORME", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CBO_INFORMACION", Type.GetType("System.String"))

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
                Session("ids") = IDS
                Dim dtScripting As DataTable = da.SP_LISTAR_CLARO_3PLAY(IDS)

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    Dim columns(1) As DataColumn
                    columns(0) = dtScripting.Columns("ID")
                    dtScripting.PrimaryKey = columns
                    Dim row As DataRow = dtScripting.Rows.Find(IDhIS)

                    dtHistorial.Rows(i)("D_SEMANA") = row(1)
                    dtHistorial.Rows(i)("D_COD_CLIENTE") = row(2)
                    dtHistorial.Rows(i)("D_NOMBRE_CLI") = row(3)
                    dtHistorial.Rows(i)("D_SOLUCION") = row(4)
                    dtHistorial.Rows(i)("D_DIRECCION") = row(5)
                    dtHistorial.Rows(i)("D_DISTRITO") = row(6)
                    dtHistorial.Rows(i)("D_TELEFONO_01") = row(7)
                    dtHistorial.Rows(i)("D_TELEFONO_02") = row(8)
                    dtHistorial.Rows(i)("D_TELEFONO_03") = row(9)
                    dtHistorial.Rows(i)("D_TELEFONO_04") = row(10)
                    dtHistorial.Rows(i)("D_TELEFONO_05") = row(11)
                    dtHistorial.Rows(i)("D_PROYECTO") = row(12)
                    dtHistorial.Rows(i)("D_NRO_DOCUMENTO") = row(13)
                    dtHistorial.Rows(i)("D_SERVICIO") = row(14)
                    dtHistorial.Rows(i)("D_REFERENCIA") = row(15)
                    dtHistorial.Rows(i)("D_DEPARTAMENTO") = row(16)
                    dtHistorial.Rows(i)("D_FEC_INSTALACION") = row(17)
                    dtHistorial.Rows(i)("D_PROVINCIA") = row(18)
                    dtHistorial.Rows(i)("CBO_PRODUCTO") = row(19)
                    dtHistorial.Rows(i)("CBO_TIPO") = row(20)
                    dtHistorial.Rows(i)("CBO_TIPO_LLAMADA") = row(21)
                    dtHistorial.Rows(i)("CBO_SUBTIPO_LLAMADA") = row(22)
                    dtHistorial.Rows(i)("CBO_PROB_TECNICO") = row(23)
                    dtHistorial.Rows(i)("CBO_NO_CONFORME") = row(24)
                    dtHistorial.Rows(i)("CBO_INFORMACION") = row(25)

                    dtHistorial.Rows(i)("CBO_SOLICITUD_PEN") = row(26)
                    dtHistorial.Rows(i)("CBO_INTERNET") = row(27)
                    dtHistorial.Rows(i)("CBO_TELEFONIA") = row(28)
                    dtHistorial.Rows(i)("CBO_TV") = row(29)
                    dtHistorial.Rows(i)("CBO_INTER_TELEF") = row(30)
                    dtHistorial.Rows(i)("CBO_INTER_TV") = row(31)
                    dtHistorial.Rows(i)("CBO_INTER_TV_TELEF") = row(32)
                    dtHistorial.Rows(i)("CBO_RECIBO") = row(33)
                    dtHistorial.Rows(i)("CBO_ESTADO_PAGO") = row(34)
                    dtHistorial.Rows(i)("CBO_ESTADO_RECIBO") = row(35)
                    dtHistorial.Rows(i)("TXT_CORREO") = row(36)
                    dtHistorial.Rows(i)("CBO_AFILIA_RECIBO_ELEC") = row(37)
                    dtHistorial.Rows(i)("TXT_PORQUE_NO") = row(38)
                    dtHistorial.Rows(i)("CBO_REALIZA_DESCARTE_ADM") = row(39)
                    dtHistorial.Rows(i)("CBO_DETECTA_PROB_ADM") = row(40)
                    dtHistorial.Rows(i)("CBO_NO_DETEC_PROB_ADM") = row(41)
                    dtHistorial.Rows(i)("CBO_FRENTE_A_EQ") = row(42)
                    dtHistorial.Rows(i)("CBO_NO_FRENTE_A_EQ") = row(43)
                    dtHistorial.Rows(i)("TXT_CODIGO") = row(44)
                    dtHistorial.Rows(i)("TXT_NOM_CLI") = row(45)
                    dtHistorial.Rows(i)("TXT_NRO_NOM_CONTACTO") = row(46)
                    dtHistorial.Rows(i)("CBO_SERVI_AFECTADO") = row(47)
                    dtHistorial.Rows(i)("TXT_CAMBIO_EQ") = row(48)
                    dtHistorial.Rows(i)("TXT_HORARIO_LLAMADA") = row(49)
                    dtHistorial.Rows(i)("DTP_FEC_INSTALACION") = row(50)
                    dtHistorial.Rows(i)("TXT_OBSERVACION") = row(51)
                    dtHistorial.Rows(i)("TXT_PROB_TEC_CORREO") = row(52)
                    dtHistorial.Rows(i)("TXT_TITULAR_USUARIO") = row(53)
                    dtHistorial.Rows(i)("TXT_TELEF_ADICIONAL") = row(54)
                Next

                IDS = Nothing
                dtScripting.Dispose()

                If dtHistorial.Rows.Count > 0 Then
                    Session("tablaExportar") = dtHistorial
                    'Session("tablaCarga") = dtmostrar
                    'grvReporte.DataSource = dtmostrar
                    grvReporte.DataSource = dtHistorial
                    grvReporte.DataBind()

                    'grvExport.DataSource = Nothing
                    'grvExport.DataSource = dtHistorial
                    'grvExport.DataBind()
                    'EXPORTAR2()
                    lnkExportar1.Visible = False
                    lnkExportar2.Visible = True
                    btnProcesar.Visible = False
                    lblMsg.Text = "TOTAL DE REGISTROS : " & dtHistorial.Rows.Count
                    lblMsg.CssClass = "alert alert-success"
                End If
            Else
                lnkExportar1.Visible = False
                lnkExportar2.Visible = False
                lblMsg.Text = "SIN REGISTROS VALIDOS PARA LAS FECHAS EN BUSQUEDA"
                lblMsg.CssClass = "alert alert-danger"

            End If

            dtHistorial.Dispose()

        Catch ex As Exception
            lblMsg.Text = "[ERROR #500 ####] : " & ex.Message
            lblMsg.CssClass = "alert alert-danger"
        End Try
    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As System.EventArgs) Handles btnProcesar.Click
        Try
            Dim dtHistorial As DataTable = Session("tablaExportar")
            Dim dtmostrar As DataTable = dtHistorial.Clone
            Dim dtunique As DataTable = da.SP_LISTAR_UNIQUE__3PLAY(Session("ids"))
            Dim found As Integer = 0
            For i = 0 To dtunique.Rows.Count - 1
                'Dim nrow As DataRow = dtHistorial.Rows.Find(dtunique.Rows(i)(0))
                Dim nrow() As DataRow = dtHistorial.Select("D_COD_CLIENTE IN(" & dtunique.Rows(i)(0) & ")")
                found = 0
                'Dim nrow2 = dtmostrar.NewRow
                For k = 0 To 3
                    If Not found > 0 Then
                        Select Case k
                            Case 0
                                For j = 1 To nrow.Length
                                    If Not nrow(j - 1).Item(30) Is DBNull.Value OrElse nrow(j - 1).Item(30) Is Nothing Then
                                        If nrow(j - 1).Item(30) = "CONTACTO VALIDO" Then
                                            found = 1
                                            dtmostrar.ImportRow(nrow(j - 1))
                                            Exit For
                                            Exit Sub
                                            'dtmostrar.Rows.InsertAt(nrow(j)(0).ToString(), dtmostrar.Rows.Count())
                                        End If
                                    End If
                                Next
                            Case 1
                                For j = 1 To nrow.Length
                                    If Not nrow(j - 1).Item(30) Is DBNull.Value OrElse nrow(j - 1).Item(30) Is Nothing Then
                                        If nrow(j - 1).Item(30) = "CONTACTO NO VALIDO" Then
                                            found = 1
                                            dtmostrar.ImportRow(nrow(j - 1))
                                            Exit For
                                            Exit Sub
                                            'dtmostrar.Rows.InsertAt(nrow(j)(0).ToString(), dtmostrar.Rows.Count())
                                        End If
                                    End If
                                Next
                            Case 2
                                For j = 1 To nrow.Length
                                    If Not nrow(j - 1).Item(30) Is DBNull.Value OrElse nrow(j - 1).Item(30) Is Nothing Then
                                        If nrow(j - 1).Item(30) = "NO CONTACTO" Then
                                            found = 1
                                            dtmostrar.ImportRow(nrow(j - 1))
                                            Exit For
                                            Exit Sub
                                            'dtmostrar.Rows.InsertAt(nrow(j)(0).ToString(), dtmostrar.Rows.Count())
                                        End If
                                    End If
                                Next
                            Case 3
                                found = 1
                                dtmostrar.ImportRow(nrow(0))
                                Exit For
                                Exit Sub
                        End Select
                    End If
                Next
            Next
            dtunique.Dispose()
            If dtmostrar.Rows.Count > 0 Then
                'Session("tablaExportar") = dtHistorial
                Session("tablaCarga") = dtmostrar
                grvReporte.DataSource = dtmostrar
                grvReporte.DataBind()
                'EXPORTAR2()
                lnkExportar1.Visible = True
                lnkExportar2.Visible = True
                btnProcesar.Visible = False
                lblMsg.Text = "TOTAL DE REGISTROS UNICOS: " & dtmostrar.Rows.Count
                lblMsg.CssClass = "alert alert-success"
                Session("ids") = Nothing
            Else
                lnkExportar1.Visible = False
                lnkExportar2.Visible = True
                btnProcesar.Visible = True
                lblMsg.Text = "SIN REGISTROS VALIDOS PARA LAS FECHAS EN BUSQUEDA"
                lblMsg.CssClass = "alert alert-danger"
            End If
        Catch ex As Exception
            lblMsg.Text = "[ERROR #500 ####] : " & ex.Message
            lblMsg.CssClass = "alert alert-danger"
        End Try


    End Sub


    'Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
    '    If grvReporte.Rows.Count > 0 Then
    '        EXPORTAR()
    '    End If
    'End Sub
    Sub EXPORTAR1()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        'grvExport.DataSource = Nothing
        'grvExport.DataBind()
        'grvExport.EnableViewState = False

        grvExport.DataSource = Nothing
		grvExport.DataBind()
        grvExport.DataSource = Session("tablaExportar")
        grvExport.DataBind()
        grvExport.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvExport)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim nombre As String = "Unicos 3 Play " & Now.ToString("yyyyMMddHHmmss")
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.Flush()
        Response.End()
    End Sub

    Sub EXPORTAR2()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        'grvExport.DataSource = Nothing
        'grvExport.DataSource = Session("tablaExportar")
        'grvExport.DataBind()
        grvExport.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvExport)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim nombre As String = "Detalle 3 Play " & Now.ToString("yyyyMMddHHmmss")
        'Response.ContentType = "application/vnd.ms-excel"
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombre & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.Flush()
        Response.End()
    End Sub



    Sub ExportarTxt(ByVal tipo As Integer)
        Dim nombre As String
        Dim dt As DataTable
        If tipo = 1 Then
            dt = Session("tablaExportar")
            nombre = "Detalle 3 Play HFC" & Now.ToString("yyyyMMddHHmmss")
        Else
            dt = Session("tablaCarga")
            nombre = "Unico 3 Play HFC" & Now.ToString("yyyyMMddHHmmss")
        End If

        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID_LOG,FECHA,LOADID,ID,ID_FINAL,FINAL,LOGIN,TELEFONO,TALKTIME,ACWTIME,D_SEMANA,D_COD_CLIENTE,D_NOMBRE_CLI,D_SOLUCION,D_DIRECCION,D_DISTRITO,D_TELEFONO_01,D_TELEFONO_02,D_TELEFONO_03,D_TELEFONO_04,D_TELEFONO_05,D_PROYECTO,D_NRO_DOCUMENTO,D_SERVICIO,D_REFERENCIA,D_DEPARTAMENTO,D_FEC_INSTALACION,D_PROVINCIA,TXT_TITULAR_USUARIO,TXT_TELEF_ADICIONAL,CBO_PRODUCTO,CBO_TIPO,CBO_TIPO_LLAMADA,CBO_SUBTIPO_LLAMADA,CBO_PROB_TECNICO,CBO_NO_CONFORME,CBO_INFORMACION,CBO_SOLICITUD_PEN,CBO_INTERNET,CBO_TELEFONIA,CBO_TV,CBO_INTER_TELEF,CBO_INTER_TV,CBO_INTER_TV_TELEF,CBO_RECIBO,CBO_ESTADO_PAGO,CBO_ESTADO_RECIBO,TXT_CORREO,CBO_AFILIA_RECIBO_ELEC,TXT_PORQUE_NO,CBO_REALIZA_DESCARTE_ADM,CBO_DETECTA_PROB_ADM,CBO_NO_DETEC_PROB_ADM,CBO_FRENTE_A_EQ,CBO_NO_FRENTE_A_EQ,TXT_CODIGO,TXT_NOM_CLI,TXT_NRO_NOM_CONTACTO,CBO_SERVI_AFECTADO,TXT_CAMBIO_EQ,TXT_HORARIO_LLAMADA,DTP_FEC_INSTALACION,TXT_OBSERVACION,TXT_PROB_TEC_CORREO")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim campo As String
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                If dt.Rows(i)(j).ToString Is DBNull.Value OrElse dt.Rows(i)(j).ToString Is Nothing OrElse dt.Rows(i)(j).ToString.Equals("") Then
                    campo = ""
                Else
                    campo = dt.Rows(i)(j).ToString()
                End If
                'Dim campo As String = dt.Rows(i)(j).ToString
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
                If cabecera = "D_SEMANA" Then campo = campo & " ,"
                If cabecera = "D_COD_CLIENTE" Then campo = campo & " ,"
                If cabecera = "D_NOMBRE_CLI" Then campo = campo & " ,"
                If cabecera = "D_SOLUCION" Then campo = campo & " ,"
                If cabecera = "D_DIRECCION" Then campo = campo & " ,"
                If cabecera = "D_DISTRITO" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_01" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_02" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_03" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_04" Then campo = campo & " ,"
                If cabecera = "D_TELEFONO_05" Then campo = campo & " ,"

                If cabecera = "D_PROYECTO" Then campo = campo & " ,"
                If cabecera = "D_NRO_DOCUMENTO" Then campo = campo & " ,"
                If cabecera = "D_SERVICIO" Then campo = campo & " ,"
                If cabecera = "D_REFERENCIA" Then campo = campo & " ,"
                If cabecera = "D_DEPARTAMENTO" Then campo = campo & " ,"
                If cabecera = "D_FEC_INSTALACION" Then campo = campo & " ,"
                If cabecera = "D_PROVINCIA" Then campo = campo & " ,"
                If cabecera = "TXT_TITULAR_USUARIO" Then campo = campo & " ,"
                If cabecera = "TXT_TELEF_ADICIONAL" Then campo = campo & " ,"
                If cabecera = "CBO_PRODUCTO" Then campo = campo & " ,"
                If cabecera = "CBO_TIPO" Then campo = campo & " ,"
                If cabecera = "CBO_TIPO_LLAMADA" Then campo = campo & " ,"
                If cabecera = "CBO_SUBTIPO_LLAMADA" Then campo = campo & " ,"
                If cabecera = "CBO_PROB_TECNICO" Then campo = campo & " ,"
                If cabecera = "CBO_NO_CONFORME" Then campo = campo & " ,"

                If cabecera = "CBO_INFORMACION" Then campo = campo & " ,"
                If cabecera = "CBO_SOLICITUD_PEN" Then campo = campo & " ,"
                If cabecera = "CBO_INTERNET" Then campo = campo & " ,"
                If cabecera = "CBO_TELEFONIA" Then campo = campo & " ,"
                If cabecera = "CBO_TV" Then campo = campo & " ,"
                If cabecera = "CBO_INTER_TELEF" Then campo = campo & " ,"
                If cabecera = "CBO_INTER_TV" Then campo = campo & " ,"
                If cabecera = "CBO_INTER_TV_TELEF" Then campo = campo & " ,"
                If cabecera = "CBO_RECIBO" Then campo = campo & " ,"
                If cabecera = "CBO_ESTADO_PAGO" Then campo = campo & " ,"
                If cabecera = "CBO_ESTADO_RECIBO" Then campo = campo & " ,"
                If cabecera = "TXT_CORREO" Then campo = campo & " ,"
                If cabecera = "CBO_AFILIA_RECIBO_ELEC" Then campo = campo & " ,"
                If cabecera = "TXT_PORQUE_NO" Then campo = campo & " ,"
                If cabecera = "CBO_REALIZA_DESCARTE_ADM" Then campo = campo & " ,"
                If cabecera = "CBO_DETECTA_PROB_ADM" Then campo = campo & " ,"
                If cabecera = "CBO_NO_DETEC_PROB_ADM" Then campo = campo & " ,"
                If cabecera = "CBO_FRENTE_A_EQ" Then campo = campo & " ,"
                If cabecera = "CBO_NO_FRENTE_A_EQ" Then campo = campo & " ,"
                If cabecera = "TXT_CODIGO" Then campo = campo & " ,"
                If cabecera = "TXT_NOM_CLI" Then campo = campo & " ,"
                If cabecera = "TXT_NRO_NOM_CONTACTO" Then campo = campo & " ,"
                If cabecera = "CBO_SERVI_AFECTADO" Then campo = campo & " ,"
                If cabecera = "TXT_CAMBIO_EQ" Then campo = campo & " ,"
                If cabecera = "TXT_HORARIO_LLAMADA" Then campo = campo & " ,"
                If cabecera = "DTP_FEC_INSTALACION" Then campo = campo & " ,"
                If cabecera = "TXT_OBSERVACION" Then campo = campo & " ,"
                If cabecera = "TXT_PROB_TEC_CORREO" Then campo = campo
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & nombre & ".csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()

    End Sub

    'Protected Sub lnkExportarCsv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportarCsv.Click
    '    If grvReporte.Rows.Count > 0 Then
    '        ExportarTxt()
    '    End If
    'End Sub

    Protected Sub grvReporte_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvReporte.PageIndexChanging
        grvReporte.PageIndex = e.NewPageIndex
        grvReporte.DataSource = Session("tablaExportar")
        grvReporte.DataBind()
        If grvReporte.Rows.Count < 1 Then
            grvReporte.DataSource = Session("tablaCarga")
            grvReporte.DataBind()
        End If
    End Sub

    Protected Sub ExportToExcel2(sender As Object, e As System.EventArgs) Handles lnkExportar2.Click
        'ExportarTxt(1)
		EXPORTAR1()
    End Sub

    Protected Sub ExportToExcel1(sender As Object, e As System.EventArgs) Handles lnkExportar1.Click
        ExportarTxt(2)
    End Sub

End Class
