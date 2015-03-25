Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Partial Class rep_outGestionInformativa
    Inherits System.Web.UI.Page
    Dim be As New BE_CLARO
    Dim da As New DA_claro
    Dim dtCarga As New DataTable

    Protected Sub btnVisualizar_Click(sender As Object, e As System.EventArgs) Handles btnVisualizar.Click
        lnkExportar.Visible = False
        Session("tablaExportar") = Nothing
        Dim ficha As String = Now.ToString("yyyyMMddHHmmss")
        Dim ruta As String = "C:\ClaroCargas\" & ficha & ".xls"
        Try
            If FileUpload1.PostedFile IsNot Nothing Then
                FileUpload1.PostedFile.SaveAs(ruta)
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
        CargarExcel(ruta)
    End Sub
    Sub CargarExcel(ByVal SLibro As String)
        lblMsg.Text = ""
        dtCarga = Nothing

        Dim cs As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                           "Data Source=" & SLibro & ";" & _
                           "Extended Properties=""Excel 8.0;HDR=YES"""
        Try
            Dim cn As New OleDbConnection(cs)
            If Not System.IO.File.Exists(SLibro) Then
                MsgBox("No se encontró el Libro: " & _
                        SLibro, MsgBoxStyle.Critical, _
                        "Ruta inválida")
                Exit Sub
            End If

            'Dim dAdapter As New OleDbDataAdapter("Select D_SERVICE,D_CUST_ACCOUNT,D_ACCOUNT_DESC,D_CLIENTE,D_TIPO_DOCUMENTO,D_NRO_DOCUMENTO,D_FEC_ACTIVACION,D_DEPARTAMENTO,D_PROVINCIA,D_DISTRITO,D_DIRECCION,D_CICLO_FACTURACION,D_TIPO_DOC_EMITIDO,D_RECIBO,D_FEC_EMISION,D_FEC_VENCIMIENTO,D_MONTO_RECIBO,D_FEC_ASIGNACION,D_PLAN_TARIFARIO,D_NRO_SERVICIO,D_EST_ACT_SERVICIO,D_INDICADOR,D_TEL_01,D_TEL_02,D_TEL_03,D_TEL_04,D_TEL_05,D_TEL_06 From [Hoja1$]", cs)
            Dim dAdapter As New OleDbDataAdapter("SELECT D_DOC_IDENTIDAD,D_CLIENTE,D_TIPO_DOC,D_FEC_MIGRACION,D_COD_ANTERIOR,D_NUEVO_CODIGO,D_CODIGO_PAGO,D_MONTO_PENDIENTE,D_DEUDA_COD_ANTIGUO,D_NOMBRE_PLAN,D_TELEF_SERVICIO,D_DEBITO_AUTOMATICO,D_SERV_TELEF_PLAN_ORIGEN,D_FEC_ASIGNACION,D_NOMBRE_CARTERA,D_CC,D_NRO_SERVICIO ,D_TIPO_DE_SERVICIO,D_ESCENARIO_CLIENTE,D_REF_MONTO_TOTAL,D_TENER_EN_CUENTA,D_VARIOS_RECIBOS,D_TELEF_REFERENCIA_1,D_TELEF_REFERENCIA_2,D_DOCUMENTO,D_TIPO,D_CUSTOMER_ID,D_CUENTA_LARGA,D_TIPO_DOC,D_FECHA_EMISION,D_FECHA_VENCIMIENTO,D_MONTO_ORIGINAL,D_MONTO_PENDIENTE,D_ANTIG_DEUDA  From [Hoja1$]", cs)

            Dim dt As New DataTable
            dAdapter.Fill(dt)
            dtCarga = dt
            Session("tablaCarga") = dtCarga
            lblMsg.Text = "Cantidad de Registros : " & dt.Rows.Count

            ' mostrar los 50 primeros
            Dim dAdapter1 As New OleDbDataAdapter("Select top 50 * From [Hoja1$]", cs)
            Dim dt1 As New DataTable
            dAdapter1.Fill(dt1)

            grvCarga.DataSource = dt1
            grvCarga.DataBind()

            'If dt1.Rows.Count > 0 Then btnGenerar.Visible = True : lnkExportar.Visible = True Else btnGenerar.Visible = False : lnkExportar.Visible = False
            If dt1.Rows.Count > 0 Then btnGenerar.Visible = True : lnkExportar.Visible = False Else btnGenerar.Visible = False : lnkExportar.Visible = False
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Sub GUARDAR()
        Dim dtId As DataTable = da.MAXIMO_ID_GESTION_INFORMATIVA()

        Dim id As Integer = 0
        Dim idf As String 'Almacenar el primer id guardado
        Dim dtOrg As DataTable = Session("tablaCarga")
        Dim dtUnique As DataTable = dtOrg.Clone
        Dim c_bien As Integer = 0
        Dim c_no_bien As Integer = 0
        Dim c_error As Integer = 0
        Dim ms As String

        If dtId.Rows.Count > 0 Then
            If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then idf = 0 Else idf = dtId.Rows(0)("ID").ToString
        Else
            lblMsg.Text = "No se pudo obtener ID numérico"
            Exit Sub
        End If

        'Obtengo registros unicos
        Dim vwtb As New DataView(dtOrg)
        vwtb.Sort = "D_DOC_IDENTIDAD"
        dtUnique.ImportRow(vwtb.ToTable.Rows(0))
        For i = 1 To vwtb.Count - 1
            If String.Compare(vwtb.ToTable.Rows(i)(0).ToString, vwtb.ToTable.Rows(i - 1)(0).ToString()) Then
                ' If String.Compare(vwtb.Table.Rows(i)(0).ToString(), vwtb.Table.Rows(i - 1)(0).ToString()) Then
                dtUnique.ImportRow(vwtb.ToTable.Rows(i))
                'dtUnique.ImportRow(vwtb.Table.Rows(i))
            End If
        Next

        Dim dtMostar As New DataTable
        Dim dtguardarDet As New DataTable

        dtMostar.Columns.Add("ID", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO_01", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO_02", Type.GetType("System.String"))

        If dtId.Rows.Count > 0 Then
            If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then id = 0 Else id = dtId.Rows(0)("ID").ToString
            Dim dt As DataTable

            dt = dtUnique

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    id += 1
                    be.VAR_ID = id
                    be.VAR_D_CLIENTE = dt.Rows(i)("D_CLIENTE").ToString
                    be.VAR_D_TIPO_DOC = dt.Rows(i)("D_TIPO_DOC").ToString
                    be.VAR_D_DOCUMENTO = dt.Rows(i)("D_DOC_IDENTIDAD").ToString
                    be.VAR_D_FEC_MIGRACION = dt.Rows(i)("D_FEC_MIGRACION").ToString
                    be.VAR_D_COD_ANTERIOR = dt.Rows(i)("D_COD_ANTERIOR").ToString
                    be.VAR_D_NUEVO_CODIGO = dt.Rows(i)("D_NUEVO_CODIGO").ToString
                    be.VAR_D_CODIGO_PAGO = dt.Rows(i)("D_CODIGO_PAGO").ToString
                    be.VAR_D_MONTO_PENDIENTE = dt.Rows(i)("D_MONTO_PENDIENTE").ToString
                    be.VAR_D_DEUDA_COD_ANTIGUO = dt.Rows(i)("D_DEUDA_COD_ANTIGUO").ToString
                    be.VAR_D_NOMBRE_PLAN = dt.Rows(i)("D_NOMBRE_PLAN").ToString
                    be.VAR_D_TELEF_SERVICIO = dt.Rows(i)("D_TELEF_SERVICIO").ToString
                    be.VAR_D_DEBITO_AUTOMATICO = dt.Rows(i)("D_DEBITO_AUTOMATICO").ToString
                    be.VAR_D_SERV_TELEF_PLAN_ORIGEN = dt.Rows(i)("D_SERV_TELEF_PLAN_ORIGEN").ToString
                    be.VAR_D_FEC_ASIGNACION = dt.Rows(i)("D_FEC_ASIGNACION").ToString
                    be.VAR_D_NOMBRE_CARTERA = dt.Rows(i)("D_NOMBRE_CARTERA").ToString
                    be.VAR_D_CC = dt.Rows(i)("D_CC").ToString
                    be.VAR_D_NRO_SERVICIO = dt.Rows(i)("D_NRO_SERVICIO").ToString
                    be.VAR_D_TIPO_DE_SERVICIO = dt.Rows(i)("D_TIPO_DE_SERVICIO").ToString
                    be.VAR_D_ESCENARIO_CLIENTE = dt.Rows(i)("D_ESCENARIO_CLIENTE").ToString
                    be.VAR_D_REF_MONTO_TOTAL = dt.Rows(i)("D_REF_MONTO_TOTAL").ToString
                    be.VAR_D_TENER_EN_CUENTA = dt.Rows(i)("D_TENER_EN_CUENTA").ToString
                    be.VAR_D_VARIOS_RECIBOS = dt.Rows(i)("D_VARIOS_RECIBOS").ToString
                    be.VAR_D_TELEF_REFERENCIA_1 = dt.Rows(i)("D_TELEF_REFERENCIA_1").ToString
                    be.VAR_D_TELEF_REFERENCIA_2 = dt.Rows(i)("D_TELEF_REFERENCIA_2").ToString



                    '
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    ms = da.SP_REGISTRAR_BASE_GESTION_INFORMATIVA(be)

                    Select Case ms
                        Case "0"
                            c_no_bien += 1
                            btnGenerar.Enabled = True
                            btnVisualizar.Enabled = False
                        Case "1"
                            c_bien += 1
                            Dim c As DataRow = dtMostar.NewRow
                            c.Item(0) = be.VAR_ID
                            Dim telefono1 As String = ""
                            Dim telefono2 As String = ""

                            If Not IsNumeric(be.VAR_D_TELEF_REFERENCIA_1) Then telefono1 = "" Else telefono1 = CInt(be.VAR_D_TELEF_REFERENCIA_1)
                            Select Case Microsoft.VisualBasic.Left(telefono1, 1)
                                Case "9"
                                    telefono1 = "'035" & telefono1 & "'"
                                Case "1"
                                    telefono1 = "'035" & telefono1 & "'"
                                Case Else
                                    If telefono1.Trim.Length > 0 Then
                                        telefono1 = "'0351" & telefono1 & "'"
                                    Else
                                        telefono1 = ""
                                    End If

                            End Select
                            c.Item(1) = telefono1

                            If Not IsNumeric(be.VAR_D_TELEF_REFERENCIA_2) Then telefono2 = "" Else telefono2 = Convert.ToInt64(be.VAR_D_TELEF_REFERENCIA_2)
                            Select Case Microsoft.VisualBasic.Left(telefono2, 1)
                                Case "9"
                                    telefono2 = "'035" & telefono2 & "'"
                                Case "1"
                                    telefono2 = "'035" & telefono2 & "'"
                                Case Else
                                    If telefono2.Trim.Length > 0 Then
                                        telefono2 = "'0351" & telefono2 & "'"
                                    Else
                                        telefono2 = ""
                                    End If
                            End Select
                            c.Item(2) = telefono2



                            dtMostar.Rows.InsertAt(c, dtMostar.Rows.Count)
                            btnGenerar.Enabled = False
                            btnVisualizar.Enabled = True
                        Case Else
                            c_error += 1
                            btnGenerar.Enabled = True
                            btnVisualizar.Enabled = False
                    End Select
                Next

                'Dim ms As String
                dt = dtOrg
                Dim err As Integer = 0
                Dim dtdoc As DataTable = da.SP_LISTAR_IDS_GESTION_INFORMATIVA(idf) 'Obtener lista de nrodocs
                For o = 0 To dtdoc.Rows.Count - 1
                    Dim nrow() As DataRow = dtOrg.Select("D_DOC_IDENTIDAD = '" & dtdoc.Rows(o)(1) & "'")

                    For u = 0 To nrow.Length - 1
                        be.VAR_ID = dtdoc.Rows(o)(0)
                        be.VAR_D_DOC_IDENTIDAD = dtdoc.Rows(o)(1)
                        be.VAR_D_TIPO = nrow(u)("D_TIPO").ToString
                        be.VAR_D_CUSTOMER_ID = nrow(u)("D_CUSTOMER_ID").ToString
                        be.VAR_D_CUENTA_LARGA = nrow(u)("D_CUENTA_LARGA").ToString
                        be.VAR_D_TIPO_DOC = nrow(u)("D_TIPO_DOC").ToString
                        be.VAR_D_FECHA_EMISION = nrow(u)("D_FECHA_EMISION").ToString
                        be.VAR_D_FECHA_VENCIMIENTO = nrow(u)("D_FECHA_VENCIMIENTO").ToString
                        be.VAR_D_MONTO_ORIGINAL = nrow(u)("D_MONTO_ORIGINAL").ToString
                        be.VAR_D_MONTO_PENDIENTE = nrow(u)("D_MONTO_PENDIENTE").ToString
                        be.VAR_D_ANTIG_DEUDA = nrow(u)("D_ANTIG_DEUDA").ToString
                        be.VAR_D_DETDOCUMENTO = nrow(u)("D_DOCUMENTO").ToString


                        ms = da.SP_REGISTRAR_BASE_DET_SCRIPTING_GESTION_INFORMATIVA(be)
                        If ms = "0" Then
                            err = err + 1
                            lblMsg.Text = "Nro de errores al guardar detalle: " & err
                        End If
                    Next
                Next

                grvMostrar.DataSource = dtMostar
                grvMostrar.DataBind()
                lnkExportar.Visible = True


                Session("tablaExportar") = dtMostar

                lblMsg.Text = "Archivos Correcto " & c_bien & ",  Archivos no subidos " & c_no_bien & ",  Archivos con errores " & c_error
                btnGenerar.Visible = False
                LIMPIAR()
            End If
        End If

    End Sub
    Sub LIMPIAR()
        Session("tablaCarga") = Nothing
        Dim dt As DataTable = Session("tablaCarga")
        grvCarga.DataSource = dt
        grvCarga.DataBind()
    End Sub

    Protected Sub btnGenerar_Click(sender As Object, e As System.EventArgs) Handles btnGenerar.Click
        GUARDAR()
    End Sub
    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID,TELEFONO_01,TELEFONO_02")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "ID" Then campo = campo & ","
                If cabecera = "TELEFONO_01" Then campo = campo & ","
                If cabecera = "TELEFONO_02" Then campo = campo
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i
        Dim nombre As String = "Gest_Informativa" & Now.ToString("yyyyMMddHHmmss")
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

    Protected Sub lnkExportar_Click(sender As Object, e As System.EventArgs) Handles lnkExportar.Click

        If grvMostrar.Rows.Count > 0 Then
            ExportarTxt()
        End If

    End Sub
    Protected Sub grvMostrar_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvMostrar.PageIndexChanging
        grvCarga.PageIndex = e.NewPageIndex
        grvCarga.DataSource = Session("tablaCarga")
        grvCarga.DataBind()
    End Sub
End Class
