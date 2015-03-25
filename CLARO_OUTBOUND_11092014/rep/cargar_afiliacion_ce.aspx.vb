Imports System.Data
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

            If dt1.Rows.Count > 0 Then btnGenerar.Visible = True : lnkExportar.Visible = True Else btnGenerar.Visible = False : lnkExportar.Visible = False

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Sub GUARDAR()
        Dim dtId As DataTable = da.MAXIMO_ID_AFILIACION_CE
        Dim id As Integer = 0

        Dim c_bien As Integer = 0
        Dim c_no_bien As Integer = 0
        Dim c_error As Integer = 0

        Dim dtMostar As New DataTable
        dtMostar.Columns.Add("ID", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO1", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO2", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO3", Type.GetType("System.String"))

        If dtId.Rows.Count > 0 Then
            If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then id = 0 Else id = dtId.Rows(0)("ID").ToString

            Dim dt As DataTable = Session("tablaCarga")
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    id += 1
                    be.VAR_ID = id
                    be.VAR_CUENTA = dt.Rows(i)("CUENTA").ToString
                    be.VAR_CLIENTE = dt.Rows(i)("CLIENTE").ToString
                    be.VAR_CONTACTO = dt.Rows(i)("CONTACTO").ToString
                    be.VAR_DEPARTAMENTO = dt.Rows(i)("DEPARTAMENTO").ToString
                    be.VAR_PROVINCIA = dt.Rows(i)("PROVINCIA").ToString
                    be.VAR_DISTRITO = dt.Rows(i)("DISTRITO").ToString
                    be.VAR_TEL1 = dt.Rows(i)("TEL1").ToString
                    be.VAR_TEL2 = dt.Rows(i)("TEL2").ToString
                    be.VAR_TELCELULAR = dt.Rows(i)("TELCELULAR").ToString
                    be.VAR_PLAN = dt.Rows(i)("PLAN").ToString
                    be.VAR_DNI = dt.Rows(i)("DNI").ToString
                    be.VAR_CICLO_FACT = dt.Rows(i)("CICLO_FACT").ToString
                    be.VAR_ID_CARGA = dt.Rows(i)("ID_CARGA").ToString
                    be.VAR_NOMBRE_BASE = dt.Rows(i)("NOMBRE_BASE").ToString
                    be.VAR_FECHA_INICIO = dt.Rows(i)("FECHA_INICIO").ToString
                    be.VAR_FECHA_FIN = dt.Rows(i)("FECHA_FIN").ToString

                    'be.VAR_RESPONSABLE = Session("usuario")
                    Dim ms As String = da.SP_REGISTRAR_BASE_SCRIPTING_OUT_AFILIACION_CE(be)
                    'Dim ms As String = "1"
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

                            If Not IsNumeric(be.VAR_TELCELULAR) Then telefono1 = "" Else telefono1 = Convert.ToInt64(be.VAR_TELCELULAR)
                            Select Case Microsoft.VisualBasic.Left(telefono1, 1)
                                Case "9"
                                    If telefono1.Length = 9 Then
                                        telefono1 = "'035" & telefono1 & "'"
                                    End If
                                Case Else
                                    If telefono1.Length = 7 Then
                                        telefono1 = "'0351" & telefono1 & "'"
                                    ElseIf telefono1.Length = 8 Then
                                        telefono1 = "'035" & telefono1 & "'"
                                    Else
                                        telefono1 = ""
                                    End If
                            End Select
                            c.Item(1) = telefono1

                            If Not IsNumeric(be.VAR_TEL1) Then telefono2 = "" Else telefono2 = Convert.ToInt64(be.VAR_TEL1)
                            Select Case Microsoft.VisualBasic.Left(telefono2, 1)
                                Case "9"
                                    If telefono2.Length = 9 Then
                                        telefono2 = "'035" & telefono2 & "'"
                                    End If
                                Case Else
                                    If telefono2.Length = 7 Then
                                        telefono2 = "'0351" & telefono2 & "'"
                                    ElseIf telefono2.Length = 8 Then
                                        telefono2 = "'035" & telefono2 & "'"
                                    Else
                                        telefono2 = ""
                                    End If
                            End Select
                            c.Item(2) = telefono2

                            If Not IsNumeric(be.VAR_TEL2) Then telefono3 = "" Else telefono3 = Convert.ToInt64(be.VAR_TEL2)
                            Select Case Microsoft.VisualBasic.Left(telefono3, 1)
                                Case "9"
                                    If telefono3.Length = 9 Then
                                        telefono3 = "'035" & telefono3 & "'"
                                    End If
                                Case Else
                                    If telefono3.Length = 7 Then
                                        telefono3 = "'0351" & telefono3 & "'"
                                    ElseIf telefono3.Length = 8 Then
                                        telefono3 = "'035" & telefono3 & "'"
                                    Else
                                        telefono3 = ""
                                    End If
                            End Select
                            c.Item(3) = telefono3

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
        str.Append("ID,TELEFONO1,TELEFONO2,TELEFONO3")
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
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=BASE_AFILIA_CE.csv")
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
