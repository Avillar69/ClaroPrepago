﻿Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class rep_cargar
    Inherits System.Web.UI.Page
    Dim be As New BE_CLARO
    Dim da As New DA_claro
    Dim dtCarga As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
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

            'Dim dAdapter2 As New OleDbDataAdapter("SELECT * FROM [Hoja1$] WHERE " & _
            '                          "PLAN_TARIFARIO NOT IN (""Plan Pyme 37"",""Plan Pyme 50"") AND TIPO_CLIENTE = ""CONSUMER"" ", cs)

            Dim dAdapter2 As New OleDbDataAdapter("SELECT * FROM [Hoja1$] WHERE " & _
                          "ESTADO=""Activo"" AND TIPO_CLIENTE = ""CONSUMER"" AND MID(MSISDN,1,1) IN (""9"") ", cs)

            Dim dt2 As New DataTable
            dAdapter2.Fill(dt2)

            'Dim dAdapter As New OleDbDataAdapter("Select * From [Hoja1$]", cs)
            'Dim dt As New DataTable
            'dAdapter.Fill(dt)
            'dtCarga = dt
            'Session("tablaCarga") = dtCarga


            lblMsg.Text = "Cantidad de Registros : " & dt2.Rows.Count

            '' mostrar los 50 primeros
            'Dim dAdapter1 As New OleDbDataAdapter("Select * From [Hoja1$]", cs)
            'Dim dt1 As New DataTable
            'dAdapter1.Fill(dt1)

            grvCarga.DataSource = dt2
            Session("tablaCarga") = dt2
            grvCarga.DataBind()

            If dt2.Rows.Count > 0 Then btnGenerar.Visible = True : lnkExportar.Visible = True Else btnGenerar.Visible = False : lnkExportar.Visible = False

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Sub LIMPIAR()
        Session("tablaCarga") = Nothing
        Dim dt As DataTable = Session("tablaCarga")
        grvCarga.DataSource = dt
        grvCarga.DataBind()
    End Sub

    Sub GUARDAR()
        Dim dtId As DataTable = da.MAXIMO_ID_MIGRACIONES
        Dim id As Integer = 0

        Dim c_bien As Integer = 0
        Dim c_no_bien As Integer = 0
        Dim c_error As Integer = 0

        Dim dtMostar As New DataTable
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
        dtMostar.Columns.Add("TELEFONO11", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO12", Type.GetType("System.String"))
        dtMostar.Columns.Add("DIV", Type.GetType("System.String"))

        If dtId.Rows.Count > 0 Then
            If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then id = 0 Else id = dtId.Rows(0)("ID").ToString

            Dim dt As DataTable = Session("tablaCarga")
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    id += 1
                    be.VAR_ID = id
                    be.VAR_RAZON_SOCIAL = dt.Rows(i)("RAZON_SOCIAL").ToString
                    be.VAR_RUC = dt.Rows(i)("RUC").ToString
                    be.VAR_MSISDN = dt.Rows(i)("MSISDN").ToString
                    be.VAR_PLAN_TARIFARIO = dt.Rows(i)("PLAN_TARIFARIO").ToString
                    be.VAR_TIPO_CLIENTE = dt.Rows(i)("TIPO_CLIENTE").ToString
                    be.VAR_CICLO = dt.Rows(i)("CICLO").ToString
                    be.VAR_FECHA_ITERACCION = dt.Rows(i)("FECHA_ITERACION").ToString
                    be.VAR_CLAROPUNTOS = dt.Rows(i)("CLARO_PUNTOS").ToString
                    be.VAR_IMR = dt.Rows(i)("IMR").ToString
                    be.VAR_DISTRITO = dt.Rows(i)("DISTRITO").ToString
                    be.VAR_PROVINCIA = dt.Rows(i)("PROVINCIA").ToString
                    be.VAR_DEPARTAMENTO = dt.Rows(i)("DEPARTAMENTO").ToString
                    be.VAR_DIRECCION = dt.Rows(i)("DIRECCION_LEGAL").ToString
                    be.VAR_SEGMENTO = dt.Rows(i)("SEGMENTO").ToString
                    be.VAR_TELF_REF = dt.Rows(i)("TEL_REF").ToString
                    be.VAR_TELEF_PREP1 = dt.Rows(i)("NUM_PREP1").ToString
                    be.VAR_TELEF_PREP2 = dt.Rows(i)("NUM_PREP2").ToString
                    be.VAR_TELEF_PREP3 = dt.Rows(i)("NUM_PREP3").ToString
                    be.VAR_TELEF_PREP4 = dt.Rows(i)("NUM_PREP4").ToString
                    be.VAR_TELEF_PREP5 = dt.Rows(i)("NUM_PREP5").ToString
                    be.VAR_TELEF_POST1 = dt.Rows(i)("NUM_POST1").ToString
                    be.VAR_TELEF_POST2 = dt.Rows(i)("NUM_POST2").ToString
                    be.VAR_TELEF_POST3 = dt.Rows(i)("NUM_POST3").ToString
                    be.VAR_TELEF_POST4 = dt.Rows(i)("NUM_POST4").ToString
                    be.VAR_TELEF_POST5 = dt.Rows(i)("NUM_POST5").ToString
                    be.VAR_PLAZO_ACUERDO = dt.Rows(i)("PLAZO_ACUERDO").ToString
                    be.VAR_NOMBRE_BASE = dt.Rows(i)("NOMBRE_BASE").ToString

                    'be.VAR_RESPONSABLE = Session("usuario")
                    Dim ms As String = da.SP_REGISTRAR_BASE_SCRIPTING_OUTBOUND_MIGRACIONES(be)
                    Select Case ms
                        Case "0"
                            c_no_bien += 1
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
                            Dim telefono11 As String = ""
                            Dim telefono12 As String = ""
                            Dim div As String = ""

                            If Not IsNumeric(be.VAR_MSISDN) Then telefono1 = "" Else telefono1 = CInt(be.VAR_MSISDN)
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

                            If Not IsNumeric(be.VAR_TELEF_PREP1) Then telefono2 = "" Else telefono2 = CInt(be.VAR_TELEF_PREP1)
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

                            If Not IsNumeric(be.VAR_TELEF_PREP2) Then telefono3 = "" Else telefono3 = CInt(be.VAR_TELEF_PREP2)
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

                            If Not IsNumeric(be.VAR_TELEF_PREP3) Then telefono4 = "" Else telefono4 = CInt(be.VAR_TELEF_PREP3)
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

                            If Not IsNumeric(be.VAR_TELEF_PREP4) Then telefono5 = "" Else telefono5 = CInt(be.VAR_TELEF_PREP4)
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

                            If Not IsNumeric(be.VAR_TELEF_PREP5) Then telefono6 = "" Else telefono6 = CInt(be.VAR_TELEF_PREP5)
                            Select Case Microsoft.VisualBasic.Left(telefono6, 1)
                                Case "9"
                                    telefono6 = "'034" & telefono6 & "'"
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

                            If Not IsNumeric(be.VAR_TELEF_POST1) Then telefono7 = "" Else telefono7 = CInt(be.VAR_TELEF_POST1)
                            Select Case Microsoft.VisualBasic.Left(telefono7, 1)
                                Case "9"
                                    telefono7 = "'035" & telefono7 & "'"
                                Case "1"
                                    telefono7 = "'035" & telefono7 & "'"
                                Case Else
                                    If telefono7.Trim.Length > 0 Then
                                        telefono7 = "'0351" & telefono7 & "'"
                                    Else
                                        telefono7 = ""
                                    End If
                            End Select
                            c.Item(7) = telefono7

                            If Not IsNumeric(be.VAR_TELEF_POST2) Then telefono8 = "" Else telefono8 = CInt(be.VAR_TELEF_POST2)
                            Select Case Microsoft.VisualBasic.Left(telefono8, 1)
                                Case "9"
                                    telefono8 = "'035" & telefono8 & "'"
                                Case "1"
                                    telefono8 = "'035" & telefono8 & "'"
                                Case Else
                                    If telefono8.Trim.Length > 0 Then
                                        telefono8 = "'0351" & telefono8 & "'"
                                    Else
                                        telefono8 = ""
                                    End If
                            End Select
                            c.Item(8) = telefono8

                            If Not IsNumeric(be.VAR_TELEF_POST3) Then telefono9 = "" Else telefono9 = CInt(be.VAR_TELEF_POST3)
                            Select Case Microsoft.VisualBasic.Left(telefono9, 1)
                                Case "9"
                                    telefono9 = "'035" & telefono9 & "'"
                                Case "1"
                                    telefono9 = "'035" & telefono9 & "'"
                                Case Else
                                    If telefono9.Trim.Length > 0 Then
                                        telefono9 = "'0351" & telefono9 & "'"
                                    Else
                                        telefono9 = ""
                                    End If
                            End Select
                            c.Item(9) = telefono9

                            If Not IsNumeric(be.VAR_TELEF_POST4) Then telefono10 = "" Else telefono10 = CInt(be.VAR_TELEF_POST4)
                            Select Case Microsoft.VisualBasic.Left(telefono10, 1)
                                Case "9"
                                    telefono10 = "'035" & telefono10 & "'"
                                Case "1"
                                    telefono10 = "'035" & telefono10 & "'"
                                Case Else
                                    If telefono10.Trim.Length > 0 Then
                                        telefono10 = "'0351" & telefono10 & "'"
                                    Else
                                        telefono10 = ""
                                    End If
                            End Select
                            c.Item(10) = telefono10

                            If Not IsNumeric(be.VAR_TELEF_POST5) Then telefono11 = "" Else telefono11 = CInt(be.VAR_TELEF_POST5)
                            Select Case Microsoft.VisualBasic.Left(telefono11, 1)
                                Case "9"
                                    telefono11 = "'035" & telefono11 & "'"
                                Case "1"
                                    telefono11 = "'035" & telefono11 & "'"
                                Case Else
                                    If telefono11.Trim.Length > 0 Then
                                        telefono11 = "'0351" & telefono11 & "'"
                                    Else
                                        telefono11 = ""
                                    End If
                            End Select
                            c.Item(11) = telefono11

                            If Not IsNumeric(be.VAR_TELF_REF) Then telefono12 = "" Else telefono12 = CInt(be.VAR_TELF_REF)
                            Select Case Microsoft.VisualBasic.Left(telefono12, 1)
                                Case "9"
                                    telefono12 = "'035" & telefono12 & "'"
                                Case "1"
                                    telefono12 = "'035" & telefono12 & "'"
                                Case "0"
                                    telefono12 = "'035" & telefono12.Substring(1, telefono12.Length - 1).ToString & "'"
                                Case Else
                                    If telefono12.Trim.Length > 0 Then
                                        telefono12 = "'035" & telefono12 & "'"
                                    Else
                                        telefono12 = ""
                                    End If
                            End Select
                            c.Item(12) = telefono12

                            If Left(be.VAR_PLAN_TARIFARIO, 1) = "P" Or Left(be.VAR_PLAN_TARIFARIO, 1) = "C" Or Left(be.VAR_PLAN_TARIFARIO, 1) = "i" Then
                                div = "MODEM"
                            ElseIf Left(be.VAR_PLAN_TARIFARIO, 1) = "H" Or Left(be.VAR_PLAN_TARIFARIO, 1) = "T" Or Left(be.VAR_PLAN_TARIFARIO, 1) = "R" Then
                                div = "MOVIL"
                            Else
                                div = ""
                            End If
                            Select Case Microsoft.VisualBasic.Left(div, 3)
                                Case "MOV"
                                    div = "MOVIL-C_" & be.VAR_CICLO & ""
                                Case "MOD"
                                    div = "MODEM-C_" & be.VAR_CICLO & ""
                                Case Else
                                    div = ""
                            End Select
                            c.Item(13) = div

                            dtMostar.Rows.InsertAt(c, dtMostar.Rows.Count)
                        Case Else
                            c_error += 1
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

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        GUARDAR()
    End Sub
    Sub EXPORTAR()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        grvMostrar.EnableViewState = False

        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grvMostrar)

        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Dim mes As String = Request("Mes")
        Dim anio As String = Request("Ano")
        Dim nombre As String = "CargarMigraciones"
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
        str.Append("ID,TELEFONO1,TELEFONO2,TELEFONO3,TELEFONO4,TELEFONO5,TELEFONO6,TELEFONO7,TELEFONO8,TELEFONO9,TELEFONO10,TELEFONO11,TELEFONO12,DIV")
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
                If cabecera = "TELEFONO6" Then campo = campo & ","
                If cabecera = "TELEFONO7" Then campo = campo & ","
                If cabecera = "TELEFONO8" Then campo = campo & ","
                If cabecera = "TELEFONO9" Then campo = campo & ","
                If cabecera = "TELEFONO10" Then campo = campo & ","
                If cabecera = "TELEFONO11" Then campo = campo & ","
                If cabecera = "TELEFONO12" Then campo = campo & ","
                If cabecera = "DIV" Then campo = campo & ","
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=CargarMigraciones.csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()
     


    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click
        If grvMostrar.Rows.Count > 0 Then
            ExportarTxt()
        End If
    End Sub
End Class
