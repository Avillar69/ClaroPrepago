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
        Dim dtId As DataTable = da.MAXIMO_ID_ENCUESTAS
        Dim id As Integer = 0

        Dim c_bien As Integer = 0
        Dim c_no_bien As Integer = 0
        Dim c_error As Integer = 0

        Dim dtMostar As New DataTable
        dtMostar.Columns.Add("ID", Type.GetType("System.String"))
        dtMostar.Columns.Add("TELEFONO", Type.GetType("System.String"))
        If dtId.Rows.Count > 0 Then
            If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then id = 0 Else id = dtId.Rows(0)("ID").ToString

            Dim dt As DataTable = Session("tablaCarga")
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    id += 1
                    be.VAR_ID = id
                    be.VAR_TELEFONO = dt.Rows(i)("NUMERO").ToString
                    be.VAR_CLIENTE = dt.Rows(i)("CLIENTE").ToString
                    be.VAR_CAMPANIA = dt.Rows(i)("CAMPAÑA").ToString
                    be.VAR_DESCRIPCION = dt.Rows(i)("DESCRIPCION").ToString
                    be.VAR_VOZ_MODEM = dt.Rows(i)("VOZ/MODEM").ToString

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim ms As String = da.SP_REGISTRAR_BASE_SCRIPTING_OUTBOUND_ENCUESTAS(be)
                    Select Case ms
                        Case "0"
                            c_no_bien += 1
                        Case "1"
                            c_bien += 1
                            Dim c As DataRow = dtMostar.NewRow
                            c.Item(0) = be.VAR_ID
                            Dim telefono As String = ""
                            Dim div As String = ""

                            If Not IsNumeric(be.VAR_TELEFONO) Then telefono = "" Else telefono = CInt(be.VAR_TELEFONO)
                            Select Case Microsoft.VisualBasic.Left(telefono, 1)
                                Case "9"
                                    telefono = "'035" & telefono & "'"
                                Case "1"
                                    telefono = "'035" & telefono & "'"
                                Case Else
                                    If telefono.Trim.Length > 0 Then
                                        telefono = "'0351" & telefono & "'"
                                    Else
                                        telefono = ""
                                    End If

                            End Select
                            c.Item(1) = telefono

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
        str.Append("ID,TELEFONO")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "ID" Then campo = campo & ","
                If cabecera = "TELEFONO" Then campo = campo & ","
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=CARGAR_ENCUESTAS.csv")
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
