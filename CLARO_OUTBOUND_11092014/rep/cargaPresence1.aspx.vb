Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class rep_cargaPresence1
    Inherits System.Web.UI.Page

    Dim be As New BE_CLARO
    Dim da As New DA_claro
    Dim dtCarga As New DataTable
    Dim arr_recupero() As String = {"[Seleccionar]", "ClaroTV_Recupero1", "ClaroTV_Recupero2", "ClaroTV_Recupero3"}

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Me.Page.IsPostBack Then
            ddlrecupero.DataSource = arr_recupero : ddlrecupero.DataBind()
        End If

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
        grvCamp.Visible = False
        grvCarga.Visible = False
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

            Dim dAdapter As New OleDbDataAdapter("Select * From [Hoja1$]", cs)
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

            If dt1.Rows.Count > 0 Then btnGenerar.Visible = True : lnkExportar.Visible = False Else btnGenerar.Visible = False : lnkExportar.Visible = False

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkExportar_Click(sender As Object, e As System.EventArgs) Handles lnkExportar.Click
        If grvMostrar.Rows.Count > 0 Then
            ExportarTxt()
        End If
    End Sub
    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID,TELEFONO1,TELEFONO2,TELEFONO3,TELEFONO4,TELEFONO5,DIVISOR")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "ID" Then campo = campo & ","
                If cabecera = "TELEFONO1" Then campo = campo & ","
                If cabecera = "TELEFONO2" Then campo = campo & ","
                If cabecera = "TELEFONO3" Then campo = campo & ","
                If cabecera = "TELEFONO4" Then campo = campo & ","
                If cabecera = "TELEFONO5" Then campo = campo & ","
                If cabecera = "DIVISOR" Then campo = campo
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=FileName.csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()


    End Sub

    Protected Sub btnGenerar_Click(sender As Object, e As System.EventArgs) Handles btnGenerar.Click
        GUARDAR()

    End Sub
    Sub GUARDAR()
        Dim dtId As New DataTable

        If ddlrecupero.SelectedItem.ToString = "ClaroTV_Recupero1" Then
        dtId = da.MAXIMO_ID_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()
        ElseIf ddlrecupero.SelectedItem.ToString = "ClaroTV_Recupero2" Then
            dtId = da.MAXIMO_ID_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2()
        ElseIf ddlrecupero.SelectedItem.ToString = "ClaroTV_Recupero3" Then
            dtId = da.MAXIMO_ID_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO3()
        End If

        Dim id As Integer = 0
        Dim idf As String 'Almacenar el primer id guardado
        Dim dtOrg As DataTable = Session("tablaCarga")
        Dim dtUnique As DataTable = dtOrg.Clone
        Dim c_bien As Integer = 0
        Dim c_no_bien As Integer = 0
        Dim c_error As Integer = 0
        Dim ms As String=""

        'If ddlrecupero.SelectedItem.ToString = "ClaroTV_Recupero1" Then
        '    dtId = da.CLAROTV_RECUPERO1()
        'ElseIf ddlrecupero.SelectedItem.ToString = "ClaroTV_Recupero2" Then
        '    dtId = da.CLAROTV_RECUPERO2()
        'ElseIf ddlrecupero.SelectedItem.ToString = "ClaroTV_Recupero3" Then
        '    dtId = da.CLAROTV_RECUPERO3()
        'End If

        If dtId.Rows.Count > 0 Then
            If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then idf = 0 Else idf = dtId.Rows(0)("ID").ToString
        Else
            lblMsg.Text = "No se pudo obtener ID numérico"
            Exit Sub
        End If

        'Obtengo registros unicos
        Dim vwtb As New DataView(dtOrg)
        vwtb.Sort = "D_NOM_CLIENTE"
        dtUnique.ImportRow(vwtb.Table.Rows(0))
        For i = 1 To vwtb.Count - 1
            If String.Compare(vwtb.Table.Rows(i)(0).ToString(), vwtb.Table.Rows(i - 1)(0).ToString()) Then
                dtUnique.ImportRow(vwtb.Table.Rows(i))
            End If
        Next

        Dim dtMostar As New DataTable
        Dim dtguardarDet As New DataTable

            dtMostar.Columns.Add("ID", Type.GetType("System.String"))
            dtMostar.Columns.Add("TELEFONO1", Type.GetType("System.String"))
            dtMostar.Columns.Add("TELEFONO2", Type.GetType("System.String"))
            dtMostar.Columns.Add("TELEFONO3", Type.GetType("System.String"))
            dtMostar.Columns.Add("TELEFONO4", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO5", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO6", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO7", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO8", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO9", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO10", Type.GetType("System.String"))
        ' dtMostar.Columns.Add("DIVISOR", Type.GetType("System.String"))

            If dtId.Rows.Count > 0 Then
                If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then id = 0 Else id = dtId.Rows(0)("ID").ToString

            'Dim dt As DataTable = Session("tablaCarga")
            Dim dt As DataTable
            dt = dtUnique
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        id += 1
                    be.VAR_ID = id
                    be.VAR_D_NOMBRE_CLIENTE = dt.Rows(i)("D_NOM_CLIENTE").ToString
                    be.VAR_D_CODINSSRV = dt.Rows(i)("D_CODINSSRV").ToString
                    be.VAR_D_COD_CLIENTE = dt.Rows(i)("D_COD_CLIENTE").ToString
                    be.VAR_D_ESTADOPAGO = dt.Rows(i)("D_ESTADOPAGO").ToString
                    be.VAR_D_PLAN_GENERAL = dt.Rows(i)("D_PLAN_GENERAL").ToString
                    be.VAR_D_DEPARTAMENTO = dt.Rows(i)("D_DEPARTAMENTO").ToString
                    be.VAR_D_COD_RECARGA = dt.Rows(i)("D_COD_RECARGA").ToString
                    be.VAR_TELEF_PREP1 = dt.Rows(i)("D_TELEFONO_01").ToString
                    be.VAR_TELEF_PREP2 = dt.Rows(i)("D_TELEFONO_02").ToString
                    be.VAR_TELEF_PREP3 = dt.Rows(i)("D_TELEFONO_03").ToString
                    be.VAR_TELEF_PREP4 = dt.Rows(i)("D_TELEFONO_04").ToString
                    be.VAR_TELEF_PREP5 = dt.Rows(i)("D_TELEFONO_05").ToString
                    be.VAR_TELEF_PREP6 = dt.Rows(i)("D_TELEFONO_06").ToString
                    be.VAR_TELEF_PREP7 = dt.Rows(i)("D_TELEFONO_07").ToString
                    be.VAR_TELEF_PREP8 = dt.Rows(i)("D_TELEFONO_08").ToString
                    be.VAR_TELEF_PREP9 = dt.Rows(i)("D_TELEFONO_09").ToString
                    be.VAR_TELEF_PREP10 = dt.Rows(i)("D_TELEFONO_10").ToString

                    '
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'ms = da.SP_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1(be)

                    If ddlrecupero.SelectedItem.ToString = "ClaroTV_Recupero1" Then
                        ms = da.SP_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1(be)
                    ElseIf ddlrecupero.SelectedItem.ToString = "ClaroTV_Recupero2" Then
                        ms = da.SP_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(be)
                    ElseIf ddlrecupero.SelectedItem.ToString = "ClaroTV_Recupero3" Then
                        ms = da.SP_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO3(be)
                    End If

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
                            Dim telefono5 As String = ""
                            Dim telefono6 As String = ""
                            Dim telefono7 As String = ""
                            Dim telefono8 As String = ""
                            Dim telefono9 As String = ""
                            Dim telefono10 As String = ""


                            If Not IsNumeric(be.VAR_TEL1) Then telefono1 = "" Else telefono1 = CInt(be.VAR_TEL1)
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

                            If Not IsNumeric(be.VAR_TEL2) Then telefono2 = "" Else telefono2 = Convert.ToInt64(be.VAR_TEL2)
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

                            If Not IsNumeric(be.VAR_TEL3) Then telefono3 = "" Else telefono3 = Convert.ToInt64(be.VAR_TEL3)
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

                            If Not IsNumeric(be.VAR_TEL4) Then telefono4 = "" Else telefono4 = Convert.ToInt64(be.VAR_TEL4)
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

                            If Not IsNumeric(be.VAR_TEL5) Then telefono5 = "" Else telefono5 = Convert.ToInt64(be.VAR_TEL5)
                            Select Case Microsoft.VisualBasic.Left(telefono5, 1)
                                Case "9"
                                    telefono5 = "'035" & telefono5 & "'"
                                Case "1"
                                    telefono5 = "'035" & telefono5 & "'"
                                Case Else
                                    If telefono5.Trim.Length > 0 Then
                                        telefono5 = "'0351" & telefono5 & "'"
                                    Else
                                        telefono5 = ""
                                    End If
                            End Select
                            c.Item(5) = telefono5

                            If Not IsNumeric(be.VAR_TEL6) Then telefono6 = "" Else telefono6 = Convert.ToInt64(be.VAR_TEL6)
                            Select Case Microsoft.VisualBasic.Left(telefono6, 1)
                                Case "9"
                                    telefono6 = "'035" & telefono6 & "'"
                                Case "1"
                                    telefono6 = "'035" & telefono6 & "'"
                                Case Else
                                    If telefono6.Trim.Length > 0 Then
                                        telefono6 = "'0351" & telefono6 & "'"
                                    Else
                                        telefono6 = ""
                                    End If
                            End Select
                            c.Item(6) = telefono6

                            If Not IsNumeric(be.VAR_TELEF_PREP6) Then telefono7 = "" Else telefono7 = Convert.ToInt64(be.VAR_TELEF_PREP7)
                            Select Case Microsoft.VisualBasic.Left(telefono7, 1)
                                Case "9"
                                    telefono7 = "'035" & telefono7 & "'"
                                Case "1"
                                    telefono7 = "'035" & telefono7 & "'"
                                Case Else
                                    If telefono7.Trim.Length = 7 Then
                                        telefono7 = "'0351" & telefono7 & "'"
                                    ElseIf telefono7.Trim.Length > 7 Then
                                        telefono7 = "'035" & telefono7 & "'"
                                    Else
                                        telefono7 = ""
                                    End If
                            End Select
                            c.Item(7) = telefono7

                            If Not IsNumeric(be.VAR_TELEF_PREP8) Then telefono8 = "" Else telefono8 = Convert.ToInt64(be.VAR_TELEF_PREP8)
                            Select Case Microsoft.VisualBasic.Left(telefono8, 1)
                                Case "9"
                                    telefono8 = "'035" & telefono8 & "'"
                                Case "1"
                                    telefono8 = "'035" & telefono8 & "'"
                                Case Else
                                    If telefono8.Trim.Length = 7 Then
                                        telefono8 = "'0351" & telefono8 & "'"
                                    ElseIf telefono8.Trim.Length > 7 Then
                                        telefono8 = "'035" & telefono8 & "'"
                                    Else
                                        telefono8 = ""
                                    End If
                            End Select
                            c.Item(8) = telefono8

                            If Not IsNumeric(be.VAR_TELEF_PREP9) Then telefono9 = "" Else telefono9 = Convert.ToInt64(be.VAR_TELEF_PREP9)
                            Select Case Microsoft.VisualBasic.Left(telefono9, 1)
                                Case "9"
                                    telefono9 = "'035" & telefono9 & "'"
                                Case "1"
                                    telefono9 = "'035" & telefono9 & "'"
                                Case Else
                                    If telefono9.Trim.Length = 7 Then
                                        telefono9 = "'0351" & telefono9 & "'"
                                    ElseIf telefono9.Trim.Length > 7 Then
                                        telefono9 = "'035" & telefono9 & "'"
                                    Else
                                        telefono9 = ""
                                    End If
                            End Select
                            c.Item(9) = telefono9

                            If Not IsNumeric(be.VAR_TELEF_PREP10) Then telefono10 = "" Else telefono10 = Convert.ToInt64(be.VAR_TELEF_PREP10)
                            Select Case Microsoft.VisualBasic.Left(telefono10, 1)
                                Case "9"
                                    telefono10 = "'035" & telefono10 & "'"
                                Case "1"
                                    telefono10 = "'035" & telefono10 & "'"
                                Case Else
                                    If telefono10.Trim.Length = 7 Then
                                        telefono10 = "'0351" & telefono10 & "'"
                                    ElseIf telefono10.Trim.Length > 7 Then
                                        telefono10 = "'035" & telefono10 & "'"
                                    Else
                                        telefono10 = ""
                                    End If
                            End Select
                            c.Item(10) = telefono10


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
                'dt = dtOrg
                'Dim err As Integer = 0
                'Dim dtdoc As DataTable = da.SP_LISTAR_IDS_GESTION_PREVENTIVA(idf) 'Obtener lista de nrodocs
                'For o = 0 To dtdoc.Rows.Count - 1
                '    Dim nrow() As DataRow = dtOrg.Select("D_NRO_DOCUMENTO = '" & dtdoc.Rows(o)(1) & "'")

                '    For u = 0 To nrow.Length - 1
                '        be.VAR_ID = dtdoc.Rows(o)(0)
                '        be.VAR_RECIBO = dtdoc.Rows(o)(2)
                '        be.VAR_SERVICEID = nrow(u).Item("D_NRO_SERVICIO").ToString
                '        be.VAR_FEC_EMISION = nrow(u).Item("D_FEC_EMISION").ToString
                '        be.VAR_FECHA_VENC = nrow(u).Item("D_FEC_VENCIMIENTO").ToString
                '        be.VAR_MONTO_ORG = nrow(u).Item("D_MONTO_ORIGINAL").ToString
                '        be.VAR_MONTO_REC = nrow(u).Item("D_MONTO_RECIBO").ToString

                '        ms = da.SP_REGISTRAR_BASE_DET_SCRIPTING_GESTION_PREVENTIVA(be)
                '        If ms = "0" Then
                '            err = err + 1
                '            lblMsg.Text = "Nro de errores al guardar detalle: " & err
                '        End If
                '    Next
                'Next

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
End Class
