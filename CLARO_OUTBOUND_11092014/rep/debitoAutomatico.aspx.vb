Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class rep_debitoAutomatico
    Inherits System.Web.UI.Page

    Dim be As New BE_CLARO
    Dim da As New DA_claro
    Dim dtCarga As New DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

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
        btnGenerar.Enabled = True
        grvMostrar.Visible = False
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
            Dim dAdapter As New OleDbDataAdapter("select D_DNI,D_CELULAR,D_NOMBRES,D_SERVICIO_CLARO,D_ENTIDAD_BANCARIA,D_TIPO_TARJETA,D_MONTO_TOPE_MAX,D_FEC_RECHAZO,D_MOTIVO_RECHAZO,D_MONTO_RECHAZADO,D_NRO_CUENTA,D_EMAIL,D_DEUDA_1,D_FECHA_1,D_TIPO,D_NRO_CASO,D_RESULTADO_CASO,D_DETALLE,D_MONTO_TOTAL_FAC,D_MONTO_DEBITADO,D_FEC_VENCIMIENTO_FAC,D_NRO_TELEF1,D_NRO_TELEF2,D_NRO_TELEF3,D_NRO_TELEF4 From [Hoja1$]", cs)
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
        Dim dtId As DataTable = da.SCRIPTING_OUTBOUND_DEBITO_AUTOMATICO()

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
        vwtb.Sort = "D_DNI"
        dtUnique.ImportRow(vwtb.Table.Rows(0))
        For i = 1 To vwtb.Count - 1
            If String.Compare(vwtb.Table.Rows(i)(0).ToString(), vwtb.Table.Rows(i - 1)(0).ToString()) Then
                dtUnique.ImportRow(vwtb.Table.Rows(i))
            End If
        Next

        Dim dtMostar As New DataTable
        Dim dtguardarDet As New DataTable

        dtMostar.Columns.Add("ID", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO_01", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO_02", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO_03", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO_04", Type.GetType("System.String"))

        If dtId.Rows.Count > 0 Then
            If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then id = 0 Else id = dtId.Rows(0)("ID").ToString
            Dim dt As DataTable

            dt = dtUnique

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    id += 1
                    be.VAR_ID = id
                    be.VAR_D_CELULAR = dt.Rows(i)("D_CELULAR").ToString
                    be.VAR_D_NOMBRES = dt.Rows(i)("D_NOMBRES").ToString
                    be.VAR_D_SERVICIO_CLARO = dt.Rows(i)("D_SERVICIO_CLARO").ToString
                    be.VAR_D_ENTIDAD_BANCARIA = dt.Rows(i)("D_ENTIDAD_BANCARIA").ToString
                    be.VAR_D_TIPO_TARJETA = dt.Rows(i)("D_TIPO_TARJETA").ToString
                    be.VAR_D_MONTO_TOPE_MAX = dt.Rows(i)("D_MONTO_TOPE_MAX").ToString
                    be.VAR_D_FEC_RECHAZO = dt.Rows(i)("D_FEC_RECHAZO").ToString
                    be.VAR_D_MOTIVO_RECHAZO = dt.Rows(i)("D_MOTIVO_RECHAZO").ToString
                    be.VAR_D_MONTO_RECHAZADO = dt.Rows(i)("D_MONTO_RECHAZADO").ToString
                    be.VAR_D_NRO_CUENTA = dt.Rows(i)("D_NRO_CUENTA").ToString
                    be.VAR_D_EMAIL = dt.Rows(i)("D_EMAIL").ToString
                    be.VAR_D_DEUDA_1 = dt.Rows(i)("D_DEUDA_1").ToString
                    be.VAR_D_FECHA_1 = dt.Rows(i)("D_FECHA_1").ToString
                    be.VAR_D_TIPO = dt.Rows(i)("D_TIPO").ToString
                    be.VAR_D_NRO_CASO = dt.Rows(i)("D_NRO_CASO").ToString
                    be.VAR_D_RESULTADO_CASO = dt.Rows(i)("D_RESULTADO_CASO").ToString
                    be.VAR_D_DETALLE = dt.Rows(i)("D_DETALLE").ToString
                    be.VAR_D_MONTO_TOTAL_FAC = dt.Rows(i)("D_MONTO_TOTAL_FAC").ToString
                    be.VAR_D_MONTO_DEBITADO = dt.Rows(i)("D_MONTO_DEBITADO").ToString
                    be.VAR_D_FEC_VENCIMIENTO_FAC = dt.Rows(i)("D_FEC_VENCIMIENTO_FAC").ToString
                    be.VAR_D_DNI = dt.Rows(i)("D_DNI").ToString
                    be.VAR_D_NRO_TELEF1 = dt.Rows(i)("D_NRO_TELEF1").ToString
                    be.VAR_D_NRO_TELEF2 = dt.Rows(i)("D_NRO_TELEF2").ToString
                    be.VAR_D_NRO_TELEF3 = dt.Rows(i)("D_NRO_TELEF3").ToString
                    be.VAR_D_NRO_TELEF4 = dt.Rows(i)("D_NRO_TELEF4").ToString


                    '
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    ms = da.SP_REGISTRAR_SCRIPTING_OUTBOUND_DEBITO_AUTOMATICO(be)


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
                            Dim telefono3 As String = ""
                            Dim telefono4 As String = ""

                            If Not IsNumeric(be.VAR_D_NRO_TELEF1) Then telefono1 = "" Else telefono1 = CInt(be.VAR_D_NRO_TELEF1)
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

                            If Not IsNumeric(be.VAR_D_NRO_TELEF2) Then telefono2 = "" Else telefono2 = Convert.ToInt64(be.VAR_D_NRO_TELEF2)
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

                            If Not IsNumeric(be.VAR_D_NRO_TELEF3) Then telefono3 = "" Else telefono3 = Convert.ToInt64(be.VAR_D_NRO_TELEF3)
                            Select Case Microsoft.VisualBasic.Left(telefono3, 1)
                                Case "9"
                                    telefono3 = "'035" & telefono3 & "'"
                                Case "1"
                                    telefono3 = "'035" & telefono3 & "'"
                                Case Else
                                    If telefono3.Trim.Length > 0 Then
                                        telefono3 = "'0351" & telefono3 & "'"
                                    Else
                                        telefono3 = ""
                                    End If
                            End Select
                            c.Item(3) = telefono3

                            If Not IsNumeric(be.VAR_D_NRO_TELEF4) Then telefono4 = "" Else telefono4 = Convert.ToInt64(be.VAR_D_NRO_TELEF4)
                            Select Case Microsoft.VisualBasic.Left(telefono4, 1)
                                Case "9"
                                    telefono4 = "'035" & telefono4 & "'"
                                Case "1"
                                    telefono4 = "'035" & telefono4 & "'"
                                Case Else
                                    If telefono4.Trim.Length > 0 Then
                                        telefono4 = "'0351" & telefono4 & "'"
                                    Else
                                        telefono4 = ""
                                    End If
                            End Select
                            c.Item(4) = telefono4

                           

                            dtMostar.Rows.InsertAt(c, dtMostar.Rows.Count)
                            btnGenerar.Enabled = False
                            btnVisualizar.Enabled = True
                        Case Else
                            c_error += 1
                            btnGenerar.Enabled = True
                            btnVisualizar.Enabled = False
                    End Select
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
        grvMostrar.Visible = True
    End Sub
  
    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID,TELEFONO_01,TELEFONO_02,TELEFONO_03,TELEFONO_04")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "ID" Then campo = campo & ","
                If cabecera = "TELEFONO_01" Then campo = campo & ","
                If cabecera = "TELEFONO_02" Then campo = campo & ","
                If cabecera = "TELEFONO_03" Then campo = campo & ","
                If cabecera = "TELEFONO_04" Then campo = campo 
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i
        Dim nombre As String = "Gest_Prev" & Now.ToString("yyyyMMddHHmmss")
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

    Protected Sub grvCarga_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvCarga.PageIndexChanging
        grvCarga.PageIndex = e.NewPageIndex
        grvCarga.DataSource = Session("tablaCarga")
        grvCarga.DataBind()
    End Sub
End Class
