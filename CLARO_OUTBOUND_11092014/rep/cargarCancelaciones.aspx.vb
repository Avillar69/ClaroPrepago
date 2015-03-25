Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Linq

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

            Dim dAdapter2 As New OleDbDataAdapter("SELECT * FROM [Hoja1$] WHERE " & _
                                                  "ESTADO IN (""ACTIVO"",""SUSPENDIDO"") AND TIPO_CLIENTE = ""CONSUMER"" " & _
                                                  "AND ACCION = ""No Retenido"" AND USUARIO NOT IN (""E880660"",""E880669"",""E880675"",""E880748"",""E880678"",""E880680"",""E880681"",""E880751"",""E880753"",""E880705"",""E880656"",""E880798"")" & _
                               "AND CAC NOT IN (""Cuentas Corporativas 1"",""Cuentas Corporativas 2"",""Cuentas Corporativas 3"") AND (BLOQUEO IS NULL OR BLOQUEO IN (""Perdida_Robo""))", cs)
            Dim dt2 As New DataTable
            dAdapter2.Fill(dt2)

            'Dim dia1 As String = dt2.Rows(0)(0)
            'Dim dia2 As String = dt2.Rows(1)(0)

            '"AND FORMAT(FECHA_CREACION,""dd/mm/aaaa"") IN (FORMAT(""" & dia1 & """,""dd/mm/aaaa""),FORMAT(""" & dia2 & """,""dd/mm/aaaa"")) ", cs)
            'Dim dt3 As New DataTable
            'dAdapter3.Fill(dt3)
            'dtCarga = dt3

            Session("tablaCarga") = dt2

            lblMsg.Text = "Cantidad de Registros : " & dt2.Rows.Count

            ' mostrar los 50 primeros
            Dim dAdapter1 As New OleDbDataAdapter("Select top 50 * From [Hoja1$]", cs)
            Dim dt1 As New DataTable
            dAdapter1.Fill(dt1)

            grvCarga.DataSource = dt2
            grvCarga.DataBind()

            If dt1.Rows.Count > 0 Then btnGenerar.Visible = True : lnkExportar.Visible = True Else btnGenerar.Visible = False : lnkExportar.Visible = False

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Sub GUARDAR()
        Dim dtId As DataTable = da.MAXIMO_ID_CANCELACIONES
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
                    be.VAR_RUC_DNI = dt.Rows(i)("RUC_DNI").ToString
                    be.VAR_TELEFONO = dt.Rows(i)("TELEFONO").ToString
                    be.VAR_PLAN = dt.Rows(i)("PLAN").ToString
                    be.VAR_D_ESTADO_SP = dt.Rows(i)("ESTADO").ToString
                    be.VAR_TIPO_CLIENTE = dt.Rows(i)("TIPO_CLIENTE").ToString
                    be.VAR_ACCION = dt.Rows(i)("ACCION").ToString
                    be.VAR_USUARIO = dt.Rows(i)("USUARIO").ToString
                    be.VAR_MOTIVO_CANCELACION = dt.Rows(i)("MOTIVO_CANCELACION").ToString
                    be.VAR_FECHA_CREACION = dt.Rows(i)("FECHA_CREACION").ToString()
                    be.VAR_FECHAEXP_CREDDEB = dt.Rows(i)("FECHAEXP_CREDDEB").ToString
                    be.VAR_CICLO = dt.Rows(i)("CICLO").ToString
                    be.VAR_SEGMENTO = dt.Rows(i)("SEGMENTO").ToString
                    be.VAR_SERVICIO = dt.Rows(i)("SERVICIO").ToString
                    be.VAR_TELEF_REFERENCIA = dt.Rows(i)("TELEF_REFERENCIA").ToString
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
                    be.VAR_NOMBRE_BASE = dt.Rows(i)("NOMBRE_BASE").ToString

                    '
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'If be.VAR_ACCION = "No Retenido" And (be.VAR_TIPO_CLIENTE = "CONSUMER" Or be.VAR_TIPO_CLIENTE = "B2E" Or be.VAR_TIPO_CLIENTE = "DEMO") Then
                    Dim ms As String = da.SP_REGISTRAR_BASE_SCRIPTING_OUTBOUND_CANCELACIONES(be)
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

                            If Not IsNumeric(be.VAR_TELEF_PREP1) Then telefono1 = "" Else telefono1 = CInt(be.VAR_TELEF_PREP1)
                            Select Case Microsoft.VisualBasic.Left(telefono1, 1)
                                Case "9"
                                    telefono1 = "'034" & telefono1 & "'"
                                Case "1"
                                    telefono1 = "'034" & telefono1 & "'"
                                Case Else
                                    If telefono1.Trim.Length > 0 Then
                                        telefono1 = "'0341" & telefono1 & "'"
                                    Else
                                        telefono1 = ""
                                    End If

                            End Select
                            c.Item(1) = telefono1

                            If Not IsNumeric(be.VAR_TELEF_PREP2) Then telefono2 = "" Else telefono2 = CInt(be.VAR_TELEF_PREP2)
                            Select Case Microsoft.VisualBasic.Left(telefono2, 1)
                                Case "9"
                                    telefono2 = "'034" & telefono2 & "'"
                                Case "1"
                                    telefono2 = "'034" & telefono2 & "'"
                                Case Else
                                    If telefono2.Trim.Length > 0 Then
                                        telefono2 = "'0341" & telefono2 & "'"
                                    Else
                                        telefono2 = ""
                                    End If
                            End Select
                            c.Item(2) = telefono2

                            If Not IsNumeric(be.VAR_TELEF_PREP3) Then telefono3 = "" Else telefono3 = CInt(be.VAR_TELEF_PREP3)
                            Select Case Microsoft.VisualBasic.Left(telefono3, 1)
                                Case "9"
                                    telefono3 = "'034" & telefono3 & "'"
                                Case "1"
                                    telefono3 = "'034" & telefono3 & "'"
                                Case Else
                                    If telefono3.Trim.Length > 0 Then
                                        telefono3 = "'0341" & telefono3 & "'"
                                    Else
                                        telefono3 = ""
                                    End If
                            End Select
                            c.Item(3) = telefono3

                            If Not IsNumeric(be.VAR_TELEF_PREP4) Then telefono4 = "" Else telefono4 = CInt(be.VAR_TELEF_PREP4)
                            Select Case Microsoft.VisualBasic.Left(telefono4, 1)
                                Case "9"
                                    telefono4 = "'034" & telefono4 & "'"
                                Case "1"
                                    telefono4 = "'034" & telefono4 & "'"
                                Case Else
                                    If telefono4.Trim.Length > 0 Then
                                        telefono4 = "'0341" & telefono4 & "'"
                                    Else
                                        telefono4 = ""
                                    End If
                            End Select
                            c.Item(4) = telefono4

                            If Not IsNumeric(be.VAR_TELEF_PREP5) Then telefono5 = "" Else telefono5 = CInt(be.VAR_TELEF_PREP5)
                            Select Case Microsoft.VisualBasic.Left(telefono5, 1)
                                Case "9"
                                    telefono5 = "'034" & telefono5 & "'"
                                Case "1"
                                    telefono5 = "'034" & telefono5 & "'"
                                Case Else
                                    If telefono5.Trim.Length > 0 Then
                                        telefono5 = "'0341" & telefono5 & "'"
                                    Else
                                        telefono5 = ""
                                    End If
                            End Select
                            c.Item(5) = telefono5

                            If Not IsNumeric(be.VAR_TELEF_POST1) Then telefono6 = "" Else telefono6 = CInt(be.VAR_TELEF_POST1)
                            Select Case Microsoft.VisualBasic.Left(telefono6, 1)
                                Case "9"
                                    telefono6 = "'034" & telefono6 & "'"
                                Case "1"
                                    telefono6 = "'034" & telefono6 & "'"
                                Case Else
                                    If telefono6.Trim.Length > 0 Then
                                        telefono6 = "'0341" & telefono6 & "'"
                                    Else
                                        telefono6 = ""
                                    End If
                            End Select
                            c.Item(6) = telefono6

                            If Not IsNumeric(be.VAR_TELEF_POST2) Then telefono7 = "" Else telefono7 = CInt(be.VAR_TELEF_POST2)
                            Select Case Microsoft.VisualBasic.Left(telefono7, 1)
                                Case "9"
                                    telefono7 = "'034" & telefono7 & "'"
                                Case "1"
                                    telefono7 = "'034" & telefono7 & "'"
                                Case Else
                                    If telefono7.Trim.Length > 0 Then
                                        telefono7 = "'0341" & telefono7 & "'"
                                    Else
                                        telefono7 = ""
                                    End If
                            End Select
                            c.Item(7) = telefono7

                            If Not IsNumeric(be.VAR_TELEF_POST3) Then telefono8 = "" Else telefono8 = CInt(be.VAR_TELEF_POST3)
                            Select Case Microsoft.VisualBasic.Left(telefono8, 1)
                                Case "9"
                                    telefono8 = "'034" & telefono8 & "'"
                                Case "1"
                                    telefono8 = "'034" & telefono8 & "'"
                                Case Else
                                    If telefono8.Trim.Length > 0 Then
                                        telefono8 = "'0341" & telefono8 & "'"
                                    Else
                                        telefono8 = ""
                                    End If
                            End Select
                            c.Item(8) = telefono8

                            If Not IsNumeric(be.VAR_TELEF_POST4) Then telefono9 = "" Else telefono9 = CInt(be.VAR_TELEF_POST4)
                            Select Case Microsoft.VisualBasic.Left(telefono9, 1)
                                Case "9"
                                    telefono9 = "'034" & telefono9 & "'"
                                Case "1"
                                    telefono9 = "'034" & telefono9 & "'"
                                Case Else
                                    If telefono9.Trim.Length > 0 Then
                                        telefono9 = "'0341" & telefono9 & "'"
                                    Else
                                        telefono9 = ""
                                    End If
                            End Select
                            c.Item(9) = telefono9

                            If Not IsNumeric(be.VAR_TELEF_POST5) Then telefono10 = "" Else telefono10 = CInt(be.VAR_TELEF_POST5)
                            Select Case Microsoft.VisualBasic.Left(telefono10, 1)
                                Case "9"
                                    telefono10 = "'034" & telefono10 & "'"
                                Case "1"
                                    telefono10 = "'034" & telefono10 & "'"
                                Case Else
                                    If telefono10.Trim.Length > 0 Then
                                        telefono10 = "'0341" & telefono10 & "'"
                                    Else
                                        telefono10 = ""
                                    End If
                            End Select
                            c.Item(10) = telefono10

                            If Not IsNumeric(be.VAR_TELEF_REFERENCIA) Then telefono11 = "" Else telefono11 = CONVERT.TOINT64(be.VAR_TELEF_REFERENCIA)
                            Select Case Microsoft.VisualBasic.Left(telefono11, 1)
                                Case "9"
                                    telefono11 = "'034" & telefono11 & "'"
                                Case "1"
                                    telefono11 = "'035" & telefono11 & "'"
                                Case "0"
                                    telefono11 = "'034" & telefono11.Substring(1, telefono11.Length - 1).ToString & "'"
                                Case Else
                                    If telefono11.Trim.Length > 0 Then
                                        telefono11 = "'034" & telefono11 & "'"
                                    Else
                                        telefono11 = ""
                                    End If
                            End Select
                            c.Item(11) = telefono11


                            If Not IsNumeric(be.VAR_TELEFONO) Then telefono12 = "" Else telefono12 = Convert.ToString(be.VAR_TELEFONO)
                            Select Case Microsoft.VisualBasic.Left(telefono12, 1)
                                Case "9"
                                    telefono12 = "'034" & telefono12 & "'"
                                Case "5"
                                    telefono12 = "'034" & telefono12.Substring(2, telefono12.Length - 2).ToString & "'"
                                Case "0"
                                    telefono12 = "'034" & telefono12.Substring(1, telefono12.Length - 1).ToString & "'"
                                Case Else
                                    If telefono12.Trim.Length > 0 Then
                                        telefono12 = "'034" & telefono12 & "'"
                                    Else
                                        telefono12 = ""
                                    End If
                            End Select
                            c.Item(12) = telefono12

                            If be.VAR_PLAN = "Plan Exacto C" Or be.VAR_PLAN = "Plan Exacto Y" Or be.VAR_PLAN = "Plan Exacto S" Or be.VAR_PLAN = "Plan Modem" Or be.VAR_PLAN = "Plan Tablet" _
                                Or be.VAR_PLAN = "Plan Modem Internet" Or be.VAR_PLAN = "Plan Modem (C)" Or be.VAR_PLAN = "Plan Modem Empleado" Or be.VAR_PLAN = "Plan iPhone 0" Or be.VAR_PLAN = "Plan iPhone 1" Then
                                div = "MODEM"
                            Else : div = "MOVIL"
                            End If
                            Select Case Microsoft.VisualBasic.Left(div, 3)
                                Case "MOV"
                                    div = "MOVIL-FE " & Convert.ToDateTime(be.VAR_FECHAEXP_CREDDEB).ToString("yyyy-MM-dd") & ""
                                Case "MOD"
                                    div = "MODEM-FE " & Convert.ToDateTime(be.VAR_FECHAEXP_CREDDEB).ToString("yyyy-MM-dd") & ""
                                Case Else
                                    div = ""
                            End Select
                            c.Item(13) = div

                            dtMostar.Rows.InsertAt(c, dtMostar.Rows.Count)
                        Case Else
                            c_error += 1
                    End Select
                    'End If


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
        Dim nombre As String = "Dynamicall"
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
        Response.AddHeader("content-disposition", "attachment;filename=CARGAR_CANCELACIONES.csv")
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
