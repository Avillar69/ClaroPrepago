Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class rep_outClaroOutbound
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
        str.Append("ID,TELEFONO1,TELEFONO2,TELEFONO3,TELEFONO4,TELEFONO5")
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
        grvMostrar.Visible = True
    End Sub

    Sub GUARDAR()
        Dim dtId As New DataTable

        dtId = da.MAXIMO_ID_SCRIPTING_OUTBOUND_CLARO_OLDSITTING()
       

        Dim id As Integer = 0
        Dim idf As String 'Almacenar el primer id guardado
        Dim dtOrg As DataTable = Session("tablaCarga")
        Dim dtUnique As DataTable = dtOrg.Clone
        Dim c_bien As Integer = 0
        Dim c_no_bien As Integer = 0
        Dim c_error As Integer = 0
        Dim ms As String = ""

        If dtId.Rows.Count > 0 Then
            If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then idf = 0 Else idf = dtId.Rows(0)("ID").ToString
        Else
            lblMsg.Text = "No se pudo obtener ID numérico"
            Exit Sub
        End If

        'Obtengo registros unicos
        Dim vwtb As New DataView(dtOrg)
        vwtb.Sort = "D_CODCLI"
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

        If dtId.Rows.Count > 0 Then
            If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then id = 0 Else id = dtId.Rows(0)("ID").ToString

            'Dim dt As DataTable = Session("tablaCarga")
            Dim dt As DataTable
            dt = dtUnique
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    id += 1
                    be.VAR_ID = id
                    be.VAR_D_CODSOLOT = dt.Rows(i)("D_CODSOLOT").ToString
                    be.VAR_D_TIPO_TRABAJO = dt.Rows(i)("D_TIPO_TRABAJO").ToString
                    be.VAR_D_DSCTIPSRV = dt.Rows(i)("D_DSCTIPSRV").ToString
                    be.VAR_D_ESTADO_SOT = dt.Rows(i)("D_ESTADO_SOT").ToString
                    be.VAR_D_FECUSU = dt.Rows(i)("D_FECUSU").ToString
                    be.VAR_D_MES = dt.Rows(i)("D_MES").ToString
                    be.VAR_D_ANIO = dt.Rows(i)("D_ANIO").ToString
                    be.VAR_D_CODCLI = dt.Rows(i)("D_CODCLI").ToString
                    be.VAR_D_NOMCLI = dt.Rows(i)("D_NOMCLI").ToString
                    be.VAR_D_COD_PAGO = dt.Rows(i)("D_COD_PAGO").ToString
                    be.VAR_D_COD_ID = dt.Rows(i)("D_COD_ID").ToString
                    be.VAR_D_MAIL_1 = dt.Rows(i)("D_MAIL_1").ToString
                    be.VAR_D_MAIL_2 = dt.Rows(i)("D_MAIL_2").ToString
                    be.VAR_D_TELEFONO_1 = dt.Rows(i)("D_TELEFONO_1").ToString
                    be.VAR_D_TELEFONO_2 = dt.Rows(i)("D_TELEFONO_2").ToString
                    be.VAR_D_TELEFONO_3 = dt.Rows(i)("D_TELEFONO_3").ToString
                    be.VAR_D_TELEFONO_4 = dt.Rows(i)("D_TELEFONO_4").ToString
                    be.VAR_D_TELEFONO_5 = dt.Rows(i)("D_TELEFONO_5").ToString


                    '
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ms = da.SP_SCRIPTING_OUTBOUND_CLARO_OLDSITTING(be)

                    
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


                            If Not IsNumeric(be.VAR_D_TELEFONO_1) Then telefono1 = "" Else telefono1 = CInt(be.VAR_D_TELEFONO_1)
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

                            If Not IsNumeric(be.VAR_D_TELEFONO_2) Then telefono2 = "" Else telefono2 = Convert.ToInt64(be.VAR_D_TELEFONO_2)
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

                            If Not IsNumeric(be.VAR_D_TELEFONO_3) Then telefono3 = "" Else telefono3 = Convert.ToInt64(be.VAR_D_TELEFONO_3)
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

                            If Not IsNumeric(be.VAR_D_TELEFONO_4) Then telefono4 = "" Else telefono4 = Convert.ToInt64(be.VAR_D_TELEFONO_4)
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

                            If Not IsNumeric(be.VAR_D_TELEFONO_5) Then telefono5 = "" Else telefono5 = Convert.ToInt64(be.VAR_D_TELEFONO_5)
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
End Class
