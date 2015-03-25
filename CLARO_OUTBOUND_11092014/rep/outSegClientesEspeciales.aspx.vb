Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Partial Class rep_outSegClientesEspeciales
    Inherits System.Web.UI.Page
    Dim be As New BE_CLARO
    Dim da As New DA_claro
    Dim dtCarga As New DataTable
   
    Protected Sub btnVisualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizar.Click
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

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click

        If grvMostrar.Rows.Count > 0 Then
            ExportarTxt()
        End If
    End Sub
    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID,TELEFONO1")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "ID" Then campo = campo & ","
                If cabecera = "TELEFONO1" Then campo = campo
                
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Seg_Clientes_Especiales.csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()


    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click

        GUARDAR()
        grvMostrar.Visible = True
    End Sub

    Sub GUARDAR()
        Dim dtId As New DataTable

        dtId = da.MAXIMO_ID_SCRIPTING_OUTBOUND_SEG_CLIENTES_ESPECIALES()


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
        vwtb.Sort = "D_RUC"
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
        
        If dtId.Rows.Count > 0 Then
            If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then id = 0 Else id = dtId.Rows(0)("ID").ToString

            'Dim dt As DataTable = Session("tablaCarga")
            Dim dt As DataTable
            dt = dtUnique
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    id += 1
                    be.VAR_ID = id
                    be.VAR_D_RUC = dt.Rows(i)("D_RUC").ToString
                    be.VAR_D_CODIGO_SAP = dt.Rows(i)("D_CODIGO_SAP").ToString
                    be.VAR_D_CLIENTE = dt.Rows(i)("D_CLIENTE").ToString
                    'be.VAR_D_EMAIL = dt.Rows(i)("D_EMAIL").ToString
                    'be.VAR_D_CONTACTO = dt.Rows(i)("D_CONTACTO").ToString
                    'be.VAR_D_OBSERVCIONES_1 = dt.Rows(i)("D_OBSERVCIONES_1").ToString
                    'be.VAR_D_OBSERVCIONES_2 = dt.Rows(i)("D_OBSERVCIONES_2").ToString
                    'be.VAR_D_OBSERVCIONES_3 = dt.Rows(i)("D_OBSERVCIONES_3").ToString
                    'be.VAR_D_OBSERVCIONES_4 = dt.Rows(i)("D_OBSERVCIONES_4").ToString
                    be.VAR_D_TELEFONO_1 = dt.Rows(i)("D_TELEFONO").ToString
                   

                    '
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ms = da.SP_SCRIPTING_OUTBOUND_SEG_CLIENTES_ESPECIALES(be)


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
                           


                            If Not IsNumeric(be.VAR_D_TELEFONO_1) Then telefono1 = "" Else telefono1 = CInt(be.VAR_D_TELEFONO_1)
                            Select Case Microsoft.VisualBasic.Left(telefono1, 1)
                                Case "9"
                                    telefono1 = "'0034" & telefono1 & "'"
                                Case "1"
                                    telefono1 = "'0034" & telefono1 & "'"
                                Case Else
                                    If telefono1.Trim.Length > 0 Then
                                        telefono1 = "'0351" & telefono1 & "'"
                                    Else
                                        telefono1 = "'0034'"
                                    End If

                            End Select
                            c.Item(1) = telefono1

                           

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
