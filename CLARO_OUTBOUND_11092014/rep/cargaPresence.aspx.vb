Imports System.Data.OleDb
Imports System.Data
Imports System.IO

Partial Class frm_cargaPresence
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO
    Dim var_ruta As String = "C:\CrearBDTable\" & Now.ToString("yyyyMMddHHmmss") & ".xls"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("dtExcel") = Nothing
            btnMostrarExcel.Visible = False
            btnGenerar.Visible = False
        End If
    End Sub
    Sub conectarSql(ByVal nombreHoja As String)
        SqlDataSource1.ProviderName = "System.Data.OleDb"
        SqlDataSource1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & "C:\CrearBDTable\Book1.xls" & ";Extended Properties=""Excel 8.0;HDR=YES"""
        SqlDataSource1.SelectCommand = "Select * From [" & nombreHoja & "$]"
        'Response.Write(SqlDataSource1.ConnectionString.ToString() & " - - " & SqlDataSource1.ProviderName.ToString() & " - - " & SqlDataSource1.SelectCommand.ToString())
        SqlDataSource1.DataBind()
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        btnMostrarExcel.Visible = False
        btnGenerar.Visible = False
        be.bd = txtEsquema.Text
        be.tabla = txtTabla.Text
        Dim dt As DataTable = da.SP_LISTA_CABECERAS(be)
        If dt.Rows.Count > 0 Then
            DataList1.DataSource = dt
            DataList1.DataBind()
            btnMostrarExcel.Visible = True
        Else
            DataList1.DataSource = Nothing
            DataList1.DataBind()
        End If
    End Sub
    Protected Sub btnMostrarExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMostrarExcel.Click
        lblMsg.Text = ""
        btnGenerar.Visible = False
        Session("dtExcel") = Nothing
        Try
            If FileUpload1.PostedFile IsNot Nothing Then
                FileUpload1.PostedFile.SaveAs(var_ruta)
            End If
            If FileUpload1.PostedFile.FileName Is Nothing Or FileUpload1.PostedFile.FileName = "" Then
                lblMsg.Text = "No se ha seleccionado ningun archivo" : Exit Sub
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

        Session("dtExcel") = da.CargarExcel(var_ruta)
        Dim dt As DataTable = Session("dtExcel")
        If dt.Rows.Count > 0 Then
            Dim nombreHoja As String = "Hoja" & Now.ToString("yyyyMMddHHmmss")
            da.CREAR_CABECERA_A_EXCEL(nombreHoja)
            For i = 0 To dt.Columns.Count - 1
                da.INSERTAR_CABECERA_A_EXCEL(nombreHoja, dt.Columns(i).ColumnName)
            Next
            conectarSql(nombreHoja)
            ENLAZAR_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()

        End If
    End Sub

    Sub ENLAZAR_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()

        If txtTabla.Text = "SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1" Then
            For i = 0 To DataList1.Items.Count - 1
                Dim cboCruce As DropDownList = CType(DataList1.Items(i).FindControl("cboCruce"), DropDownList)
                Dim lblCampo As Label = CType(DataList1.Items(i).FindControl("lblCampo"), Label)

                Dim campo As String = lblCampo.Text
                Dim cruce As String = cboCruce.SelectedValue.ToString

                If campo = "D_CODINSSRV" Then cboCruce.SelectedValue = "CODINSSRV"
                If campo = "D_COD_CLIENTE" Then cboCruce.SelectedValue = "CODCLI"
                If campo = "D_DEPARTAMENTO" Then cboCruce.SelectedValue = "DEPARTAMENTO"
                If campo = "D_NOM_CLIENTE" Then cboCruce.SelectedValue = "NOMCLI"
                If campo = "D_TELEFONO_01" Then cboCruce.SelectedValue = "TLF_O1"
                If campo = "D_TELEFONO_02" Then cboCruce.SelectedValue = "TLF_O2"
                If campo = "D_TELEFONO_03" Then cboCruce.SelectedValue = "TLF_O3"
                If campo = "D_TELEFONO_04" Then cboCruce.SelectedValue = "TLF_O4"
                If campo = "D_TELEFONO_05" Then cboCruce.SelectedValue = "TLF_O5"
                If campo = "D_COD_RECARGA" Then cboCruce.SelectedValue = "COD_RECARGA"
                If campo = "D_PLAN_GENERAL" Then cboCruce.SelectedValue = "PLAN_GENERAL"
                If campo = "D_ESTADOPAGO" Then cboCruce.SelectedValue = "EST_PAGO"
                If campo = "D_TELEFONO_06" Then cboCruce.SelectedValue = "TLF_O6"
                If campo = "D_TELEFONO_07" Then cboCruce.SelectedValue = "TLF_O7"
                If campo = "D_TELEFONO_08" Then cboCruce.SelectedValue = "TLF_O8"
                If campo = "D_TELEFONO_09" Then cboCruce.SelectedValue = "TLF_O9"
                If campo = "D_TELEFONO_10" Then cboCruce.SelectedValue = "TLF_O10"
            Next
            btnGenerar.Visible = True
        End If
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        lblMsg.Text = ""
        Try
            Dim view As DataView = New DataView(Session("dtExcel"))
            Dim dt As New DataTable
            view.Sort = "CODCLI"
            dt = view.ToTable
            Dim dtNuevaTabla As New DataTable
            Dim dtExportExc As New DataTable

            ' COLACAMOS CABECERA AL DATATABLE A DEVOLVER
            dtNuevaTabla.Columns.Add("ID", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("ID", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_TELEFONO", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_ESTADOPAGO", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_TELEFONO_01", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_TELEFONO_02", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_TELEFONO_03", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_TELEFONO_04", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_TELEFONO_05", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_TELEFONO_06", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_TELEFONO_07", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_TELEFONO_08", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_TELEFONO_09", System.Type.GetType("System.String"))
            dtExportExc.Columns.Add("D_TELEFONO_10", System.Type.GetType("System.String"))

            For i = 0 To DataList1.Items.Count - 1
                Dim lblCampo As Label = CType(DataList1.Items(i).FindControl("lblCampo"), Label)
                dtNuevaTabla.Columns.Add(lblCampo.Text, System.Type.GetType("System.String"))
            Next

            ' VERIFICAMOS SI EL CLIENTE TIENE VARIOS TELEFONOS
            For i = 0 To dt.Rows.Count - 1
                Dim c As DataRow = dtNuevaTabla.NewRow

                    For ic = 0 To DataList1.Items.Count - 1
                        Dim combo As DropDownList = CType(DataList1.Items(ic).FindControl("cboCruce"), DropDownList)
                        If ic = 0 Then c.Item("D_NOM_CLIENTE") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 1 Then c.Item("D_CODINSSRV") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 2 Then c.Item("D_COD_CLIENTE") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 3 Then c.Item("D_ESTADOPAGO") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 4 Then c.Item("D_PLAN_GENERAL") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 5 Then c.Item("D_DEPARTAMENTO") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 6 Then c.Item("D_COD_RECARGA") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 7 Then c.Item("D_TELEFONO_01") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 8 Then c.Item("D_TELEFONO_02") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 9 Then c.Item("D_TELEFONO_03") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 10 Then c.Item("D_TELEFONO_04") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 11 Then c.Item("D_TELEFONO_05") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 12 Then c.Item("D_TELEFONO_06") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 13 Then c.Item("D_TELEFONO_07") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 14 Then c.Item("D_TELEFONO_08") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 15 Then c.Item("D_TELEFONO_09") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                        If ic = 16 Then c.Item("D_TELEFONO_10") = dt.Rows(i)(combo.SelectedValue.ToString).ToString
                    Next
                dtNuevaTabla.Rows.InsertAt(c, dtNuevaTabla.Rows.Count)
            Next

            Dim c_bien As Integer = 0
            Dim c_mal As Integer = 0
            Dim c_error As Integer = 0

            For h = 0 To 9
                For i = 0 To dtNuevaTabla.Rows.Count - 1
                    Select Case h
                        Case 0
                            If dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString().Trim() <> "" Then
                                Dim tel As String = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString()
                                Dim id As String = da.ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()
                                Dim ms As String = da.INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(id, tel, dtNuevaTabla.Rows(i)("D_CODINSSRV").ToString, dtNuevaTabla.Rows(i)("D_COD_CLIENTE").ToString, _
                                dtNuevaTabla.Rows(i)("D_DEPARTAMENTO").ToString, dtNuevaTabla.Rows(i)("D_NOM_CLIENTE").ToString, dtNuevaTabla.Rows(i)("D_COD_RECARGA").ToString, dtNuevaTabla.Rows(i)("D_PLAN_GENERAL").ToString, _
                                dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString)
                                Select Case ms
                                    Case "0" : c_mal += 1
                                    Case "1" : c_bien += 1
                                    Case Else : c_error += 1
                                End Select
                                'dtNuevaTabla.Rows(i)("ID") = id

                                Dim d As DataRow = dtExportExc.NewRow
                                d.Item("ID") = id
                                d.Item("D_TELEFONO") = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString().Trim()
                                d.Item("D_ESTADOPAGO") = dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString()
                                d.Item("D_TELEFONO_01") = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString()
                                d.Item("D_TELEFONO_02") = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString()
                                d.Item("D_TELEFONO_03") = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString()
                                d.Item("D_TELEFONO_04") = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString()
                                d.Item("D_TELEFONO_05") = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString()
                                d.Item("D_TELEFONO_06") = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString()
                                d.Item("D_TELEFONO_07") = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString()
                                d.Item("D_TELEFONO_08") = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString()
                                d.Item("D_TELEFONO_09") = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString()
                                d.Item("D_TELEFONO_10") = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString()
                                dtExportExc.Rows.InsertAt(d, dtExportExc.Rows.Count)
                            End If
                        Case 1
                            If dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString().Trim() <> "" Then
                                Dim tel As String = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString()
                                Dim id As String = da.ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()
                                Dim ms As String = da.INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(id, tel, dtNuevaTabla.Rows(i)("D_CODINSSRV").ToString, dtNuevaTabla.Rows(i)("D_COD_CLIENTE").ToString, _
                                dtNuevaTabla.Rows(i)("D_DEPARTAMENTO").ToString, dtNuevaTabla.Rows(i)("D_NOM_CLIENTE").ToString, dtNuevaTabla.Rows(i)("D_COD_RECARGA").ToString, dtNuevaTabla.Rows(i)("D_PLAN_GENERAL").ToString, _
                                dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString)

                                Select Case ms
                                    Case "0" : c_mal += 1
                                    Case "1" : c_bien += 1
                                    Case Else : c_error += 1
                                End Select
                                'dtNuevaTabla.Rows(i)("ID") = id

                                Dim d As DataRow = dtExportExc.NewRow
                                d.Item("ID") = id
                                d.Item("D_TELEFONO") = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString().Trim()
                                d.Item("D_ESTADOPAGO") = dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString()
                                d.Item("D_TELEFONO_01") = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString()
                                d.Item("D_TELEFONO_02") = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString()
                                d.Item("D_TELEFONO_03") = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString()
                                d.Item("D_TELEFONO_04") = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString()
                                d.Item("D_TELEFONO_05") = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString()
                                d.Item("D_TELEFONO_06") = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString()
                                d.Item("D_TELEFONO_07") = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString()
                                d.Item("D_TELEFONO_08") = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString()
                                d.Item("D_TELEFONO_09") = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString()
                                d.Item("D_TELEFONO_10") = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString()
                                dtExportExc.Rows.InsertAt(d, dtExportExc.Rows.Count)
                            End If
                        Case 2
                            If dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString().Trim() <> "" Then
                                Dim tel As String = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString()
                                Dim id As String = da.ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()
                                Dim ms As String = da.INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(id, tel, dtNuevaTabla.Rows(i)("D_CODINSSRV").ToString, dtNuevaTabla.Rows(i)("D_COD_CLIENTE").ToString, _
                                dtNuevaTabla.Rows(i)("D_DEPARTAMENTO").ToString, dtNuevaTabla.Rows(i)("D_NOM_CLIENTE").ToString, dtNuevaTabla.Rows(i)("D_COD_RECARGA").ToString, dtNuevaTabla.Rows(i)("D_PLAN_GENERAL").ToString, _
                                dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString)
                                Select Case ms
                                    Case "0" : c_mal += 1
                                    Case "1" : c_bien += 1
                                    Case Else : c_error += 1
                                End Select
                                'dtNuevaTabla.Rows(i)("ID") = id

                                Dim d As DataRow = dtExportExc.NewRow
                                d.Item("ID") = id
                                d.Item("D_TELEFONO") = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString().Trim()
                                d.Item("D_ESTADOPAGO") = dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString()
                                d.Item("D_TELEFONO_01") = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString()
                                d.Item("D_TELEFONO_02") = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString()
                                d.Item("D_TELEFONO_03") = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString()
                                d.Item("D_TELEFONO_04") = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString()
                                d.Item("D_TELEFONO_05") = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString()
                                d.Item("D_TELEFONO_06") = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString()
                                d.Item("D_TELEFONO_07") = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString()
                                d.Item("D_TELEFONO_08") = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString()
                                d.Item("D_TELEFONO_09") = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString()
                                d.Item("D_TELEFONO_10") = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString()
                                dtExportExc.Rows.InsertAt(d, dtExportExc.Rows.Count)
                            End If
                        Case 3
                            If dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString().Trim() <> "" Then
                                Dim tel As String = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString()
                                Dim id As String = da.ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()
                                Dim ms As String = da.INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(id, tel, dtNuevaTabla.Rows(i)("D_CODINSSRV").ToString, dtNuevaTabla.Rows(i)("D_COD_CLIENTE").ToString, _
                                dtNuevaTabla.Rows(i)("D_DEPARTAMENTO").ToString, dtNuevaTabla.Rows(i)("D_NOM_CLIENTE").ToString, dtNuevaTabla.Rows(i)("D_COD_RECARGA").ToString, dtNuevaTabla.Rows(i)("D_PLAN_GENERAL").ToString, _
                                dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString)
                                Select Case ms
                                    Case "0" : c_mal += 1
                                    Case "1" : c_bien += 1
                                    Case Else : c_error += 1
                                End Select
                                'dtNuevaTabla.Rows(i)("ID") = id

                                Dim d As DataRow = dtExportExc.NewRow
                                d.Item("ID") = id
                                d.Item("D_TELEFONO") = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString().Trim()
                                d.Item("D_ESTADOPAGO") = dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString()
                                d.Item("D_TELEFONO_01") = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString()
                                d.Item("D_TELEFONO_02") = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString()
                                d.Item("D_TELEFONO_03") = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString()
                                d.Item("D_TELEFONO_04") = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString()
                                d.Item("D_TELEFONO_05") = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString()
                                d.Item("D_TELEFONO_06") = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString()
                                d.Item("D_TELEFONO_07") = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString()
                                d.Item("D_TELEFONO_08") = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString()
                                d.Item("D_TELEFONO_09") = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString()
                                d.Item("D_TELEFONO_10") = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString()
                                dtExportExc.Rows.InsertAt(d, dtExportExc.Rows.Count)
                            End If
                        Case 4
                            If dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString().Trim() <> "" Then
                                Dim tel As String = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString()
                                Dim id As String = da.ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()
                                Dim ms As String = da.INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(id, tel, dtNuevaTabla.Rows(i)("D_CODINSSRV").ToString, dtNuevaTabla.Rows(i)("D_COD_CLIENTE").ToString, _
                                dtNuevaTabla.Rows(i)("D_DEPARTAMENTO").ToString, dtNuevaTabla.Rows(i)("D_NOM_CLIENTE").ToString, dtNuevaTabla.Rows(i)("D_COD_RECARGA").ToString, dtNuevaTabla.Rows(i)("D_PLAN_GENERAL").ToString, _
                                dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString)
                                Select Case ms
                                    Case "0" : c_mal += 1
                                    Case "1" : c_bien += 1
                                    Case Else : c_error += 1
                                End Select
                                'dtNuevaTabla.Rows(i)("ID") = id

                                Dim d As DataRow = dtExportExc.NewRow
                                d.Item("ID") = id
                                d.Item("D_TELEFONO") = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString().Trim()
                                d.Item("D_ESTADOPAGO") = dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString()
                                d.Item("D_TELEFONO_01") = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString()
                                d.Item("D_TELEFONO_02") = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString()
                                d.Item("D_TELEFONO_03") = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString()
                                d.Item("D_TELEFONO_04") = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString()
                                d.Item("D_TELEFONO_05") = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString()
                                d.Item("D_TELEFONO_06") = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString()
                                d.Item("D_TELEFONO_07") = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString()
                                d.Item("D_TELEFONO_08") = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString()
                                d.Item("D_TELEFONO_09") = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString()
                                d.Item("D_TELEFONO_10") = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString()
                                dtExportExc.Rows.InsertAt(d, dtExportExc.Rows.Count)
                            End If
                        Case 5
                            If dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString().Trim() <> "" Then
                                Dim tel As String = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString()
                                Dim id As String = da.ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()
                                Dim ms As String = da.INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(id, tel, dtNuevaTabla.Rows(i)("D_CODINSSRV").ToString, dtNuevaTabla.Rows(i)("D_COD_CLIENTE").ToString, _
                                dtNuevaTabla.Rows(i)("D_DEPARTAMENTO").ToString, dtNuevaTabla.Rows(i)("D_NOM_CLIENTE").ToString, dtNuevaTabla.Rows(i)("D_COD_RECARGA").ToString, dtNuevaTabla.Rows(i)("D_PLAN_GENERAL").ToString, _
                                dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString)
                                Select Case ms
                                    Case "0" : c_mal += 1
                                    Case "1" : c_bien += 1
                                    Case Else : c_error += 1
                                End Select
                                'dtNuevaTabla.Rows(i)("ID") = id

                                Dim d As DataRow = dtExportExc.NewRow
                                d.Item("ID") = id
                                d.Item("D_TELEFONO") = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString().Trim()
                                d.Item("D_ESTADOPAGO") = dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString()
                                d.Item("D_TELEFONO_01") = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString()
                                d.Item("D_TELEFONO_02") = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString()
                                d.Item("D_TELEFONO_03") = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString()
                                d.Item("D_TELEFONO_04") = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString()
                                d.Item("D_TELEFONO_05") = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString()
                                d.Item("D_TELEFONO_06") = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString()
                                d.Item("D_TELEFONO_07") = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString()
                                d.Item("D_TELEFONO_08") = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString()
                                d.Item("D_TELEFONO_09") = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString()
                                d.Item("D_TELEFONO_10") = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString()
                                dtExportExc.Rows.InsertAt(d, dtExportExc.Rows.Count)
                            End If
                        Case 6
                            If dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString().Trim() <> "" Then
                                Dim tel As String = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString()
                                Dim id As String = da.ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()
                                Dim ms As String = da.INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(id, tel, dtNuevaTabla.Rows(i)("D_CODINSSRV").ToString, dtNuevaTabla.Rows(i)("D_COD_CLIENTE").ToString, _
                                dtNuevaTabla.Rows(i)("D_DEPARTAMENTO").ToString, dtNuevaTabla.Rows(i)("D_NOM_CLIENTE").ToString, dtNuevaTabla.Rows(i)("D_COD_RECARGA").ToString, dtNuevaTabla.Rows(i)("D_PLAN_GENERAL").ToString, _
                                dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString)
                                Select Case ms
                                    Case "0" : c_mal += 1
                                    Case "1" : c_bien += 1
                                    Case Else : c_error += 1
                                End Select
                                'dtNuevaTabla.Rows(i)("ID") = id

                                Dim d As DataRow = dtExportExc.NewRow
                                d.Item("ID") = id
                                d.Item("D_TELEFONO") = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString().Trim()
                                d.Item("D_ESTADOPAGO") = dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString()
                                d.Item("D_TELEFONO_01") = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString()
                                d.Item("D_TELEFONO_02") = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString()
                                d.Item("D_TELEFONO_03") = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString()
                                d.Item("D_TELEFONO_04") = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString()
                                d.Item("D_TELEFONO_05") = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString()
                                d.Item("D_TELEFONO_06") = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString()
                                d.Item("D_TELEFONO_07") = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString()
                                d.Item("D_TELEFONO_08") = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString()
                                d.Item("D_TELEFONO_09") = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString()
                                d.Item("D_TELEFONO_10") = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString()
                                dtExportExc.Rows.InsertAt(d, dtExportExc.Rows.Count)
                            End If
                        Case 7
                            If dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString().Trim() <> "" Then
                                Dim tel As String = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString()
                                Dim id As String = da.ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()
                                Dim ms As String = da.INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(id, tel, dtNuevaTabla.Rows(i)("D_CODINSSRV").ToString, dtNuevaTabla.Rows(i)("D_COD_CLIENTE").ToString, _
                                dtNuevaTabla.Rows(i)("D_DEPARTAMENTO").ToString, dtNuevaTabla.Rows(i)("D_NOM_CLIENTE").ToString, dtNuevaTabla.Rows(i)("D_COD_RECARGA").ToString, dtNuevaTabla.Rows(i)("D_PLAN_GENERAL").ToString, _
                                dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString)
                                Select Case ms
                                    Case "0" : c_mal += 1
                                    Case "1" : c_bien += 1
                                    Case Else : c_error += 1
                                End Select
                                'dtNuevaTabla.Rows(i)("ID") = id

                                Dim d As DataRow = dtExportExc.NewRow
                                d.Item("ID") = id
                                d.Item("D_TELEFONO") = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString().Trim()
                                d.Item("D_ESTADOPAGO") = dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString()
                                d.Item("D_TELEFONO_01") = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString()
                                d.Item("D_TELEFONO_02") = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString()
                                d.Item("D_TELEFONO_03") = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString()
                                d.Item("D_TELEFONO_04") = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString()
                                d.Item("D_TELEFONO_05") = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString()
                                d.Item("D_TELEFONO_06") = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString()
                                d.Item("D_TELEFONO_07") = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString()
                                d.Item("D_TELEFONO_08") = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString()
                                d.Item("D_TELEFONO_09") = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString()
                                d.Item("D_TELEFONO_10") = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString()
                                dtExportExc.Rows.InsertAt(d, dtExportExc.Rows.Count)
                            End If
                        Case 8
                            If dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString().Trim() <> "" Then
                                Dim tel As String = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString()
                                Dim id As String = da.ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()
                                Dim ms As String = da.INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(id, tel, dtNuevaTabla.Rows(i)("D_CODINSSRV").ToString, dtNuevaTabla.Rows(i)("D_COD_CLIENTE").ToString, _
                                dtNuevaTabla.Rows(i)("D_DEPARTAMENTO").ToString, dtNuevaTabla.Rows(i)("D_NOM_CLIENTE").ToString, dtNuevaTabla.Rows(i)("D_COD_RECARGA").ToString, dtNuevaTabla.Rows(i)("D_PLAN_GENERAL").ToString, _
                                dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString)
                                Select Case ms
                                    Case "0" : c_mal += 1
                                    Case "1" : c_bien += 1
                                    Case Else : c_error += 1
                                End Select
                                'dtNuevaTabla.Rows(i)("ID") = id

                                Dim d As DataRow = dtExportExc.NewRow
                                d.Item("ID") = id
                                d.Item("D_TELEFONO") = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString().Trim()
                                d.Item("D_ESTADOPAGO") = dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString()
                                d.Item("D_TELEFONO_01") = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString()
                                d.Item("D_TELEFONO_02") = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString()
                                d.Item("D_TELEFONO_03") = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString()
                                d.Item("D_TELEFONO_04") = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString()
                                d.Item("D_TELEFONO_05") = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString()
                                d.Item("D_TELEFONO_06") = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString()
                                d.Item("D_TELEFONO_07") = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString()
                                d.Item("D_TELEFONO_08") = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString()
                                d.Item("D_TELEFONO_09") = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString()
                                d.Item("D_TELEFONO_10") = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString()
                                dtExportExc.Rows.InsertAt(d, dtExportExc.Rows.Count)
                            End If
                        Case 9
                            If dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString().Trim() <> "" Then
                                Dim tel As String = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString()
                                Dim id As String = da.ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1()
                                Dim ms As String = da.INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(id, tel, dtNuevaTabla.Rows(i)("D_CODINSSRV").ToString, dtNuevaTabla.Rows(i)("D_COD_CLIENTE").ToString, _
                                dtNuevaTabla.Rows(i)("D_DEPARTAMENTO").ToString, dtNuevaTabla.Rows(i)("D_NOM_CLIENTE").ToString, dtNuevaTabla.Rows(i)("D_COD_RECARGA").ToString, dtNuevaTabla.Rows(i)("D_PLAN_GENERAL").ToString, _
                                dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString)
                                Select Case ms
                                    Case "0" : c_mal += 1
                                    Case "1" : c_bien += 1
                                    Case Else : c_error += 1
                                End Select
                                'dtNuevaTabla.Rows(i)("ID") = id

                                Dim d As DataRow = dtExportExc.NewRow
                                d.Item("ID") = id
                                d.Item("D_TELEFONO") = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString().Trim()
                                d.Item("D_ESTADOPAGO") = dtNuevaTabla.Rows(i)("D_ESTADOPAGO").ToString()
                                d.Item("D_TELEFONO_01") = dtNuevaTabla.Rows(i)("D_TELEFONO_01").ToString()
                                d.Item("D_TELEFONO_02") = dtNuevaTabla.Rows(i)("D_TELEFONO_02").ToString()
                                d.Item("D_TELEFONO_03") = dtNuevaTabla.Rows(i)("D_TELEFONO_03").ToString()
                                d.Item("D_TELEFONO_04") = dtNuevaTabla.Rows(i)("D_TELEFONO_04").ToString()
                                d.Item("D_TELEFONO_05") = dtNuevaTabla.Rows(i)("D_TELEFONO_05").ToString()
                                d.Item("D_TELEFONO_06") = dtNuevaTabla.Rows(i)("D_TELEFONO_06").ToString()
                                d.Item("D_TELEFONO_07") = dtNuevaTabla.Rows(i)("D_TELEFONO_07").ToString()
                                d.Item("D_TELEFONO_08") = dtNuevaTabla.Rows(i)("D_TELEFONO_08").ToString()
                                d.Item("D_TELEFONO_09") = dtNuevaTabla.Rows(i)("D_TELEFONO_09").ToString()
                                d.Item("D_TELEFONO_10") = dtNuevaTabla.Rows(i)("D_TELEFONO_10").ToString()
                                dtExportExc.Rows.InsertAt(d, dtExportExc.Rows.Count)
                            End If
                    End Select
                Next
            Next
            lblMsg.Text = "Cantidad de archivos en Excel : " & dt.Rows.Count & ", registros insertados : " & c_bien & ", registros no insertados : " & c_mal & ", registros con errores : " & c_error

            ExportarTxt(dtExportExc)
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Sub ExportarTxt(ByVal dt As DataTable)

        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID,	D_TELEFONO, SEGMENTACION, D_TELEFONO_01, D_TELEFONO_02, D_TELEFONO_03, D_TELEFONO_04, D_TELEFONO_05, D_TELEFONO_06, D_TELEFONO_07, D_TELEFONO_08, D_TELEFONO_09, D_TELEFONO_10")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            If dt.Rows(i)("D_TELEFONO").ToString().Trim() <> "" Then
                Dim id As String = dt.Rows(i)("ID").ToString
                Dim tel1 As String = dt.Rows(i)("D_TELEFONO").ToString
                Dim segmentacion As String = dt.Rows(i)("D_ESTADOPAGO").ToString
                Dim tel01 As String = dt.Rows(i)("D_TELEFONO_01").ToString()
                Dim tel02 As String = dt.Rows(i)("D_TELEFONO_02").ToString()
                Dim tel03 As String = dt.Rows(i)("D_TELEFONO_03").ToString()
                Dim tel04 As String = dt.Rows(i)("D_TELEFONO_04").ToString()
                Dim tel05 As String = dt.Rows(i)("D_TELEFONO_05").ToString()
                Dim tel06 As String = dt.Rows(i)("D_TELEFONO_06").ToString()
                Dim tel07 As String = dt.Rows(i)("D_TELEFONO_07").ToString()
                Dim tel08 As String = dt.Rows(i)("D_TELEFONO_08").ToString()
                Dim tel09 As String = dt.Rows(i)("D_TELEFONO_09").ToString()
                Dim tel10 As String = dt.Rows(i)("D_TELEFONO_10").ToString()
                If tel1.Trim.Length > 0 Then tel1 = "'035" & tel1 & "'"
                segmentacion = "'" & segmentacion & "'"
                Dim campo As String = id & "," & tel1 & "," & segmentacion & "," & tel01 & "," & tel02 & "," & tel03 & "," & tel04 & "," & tel05 & "," & tel06 & "," & tel07 & "," & tel08 & "," & tel09 & "," & tel10
                str.Append(campo)
                str.Append(vbNewLine)
            End If
        Next

        'Dim id As String = dt.Rows(i)("ID").ToString
        'Dim tel1 As String = dt.Rows(i)("D_TELEFONO").ToString
        'Dim segmentacion As String = dt.Rows(i)("D_ESTADOPAGO").ToString

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=TvRecupero" & Now.ToString("yyyyMMddHHmmss") & ".csv")
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.text"
        Dim stringWrite As New StringWriter
        Dim htmlWrite As New HtmlTextWriter(stringWrite)
        Response.Write(str.ToString())
        Response.End()

    End Sub

End Class
