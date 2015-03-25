Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Partial Class rep_outSegClientes
    Inherits System.Web.UI.Page

    Dim da As New DA_claro
    Dim dtCarga As New DataTable
    Dim dtCarga1 As New DataTable

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
        dtCarga1 = Nothing

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
            Dim dAdapter As New OleDbDataAdapter("SELECT D_RUC,D_RAZ_SOCIAL,D_CODIGO_BSCS,D_NRO_DOCUMENTO,D_CODIGO,D_SERVICIO,D_AGENTE_DNINO,D_TIPO_CLIENTE,D_FORMA_PAGO,D_ESTADO_CUENTA,D_FEC_ACTIVACION,D_DEPARTAMENTO,D_PROVINCIA,D_DISTRITO,D_CANT_LINEAS_A,D_SEGMENTO,D_TIPO_SEGMENTO,D_CICLO,D_NOMBRE_CICLO,D_SERVICIO_PRESTADO,D_ESTADO_DOCUMENTO,D_TIPO_DOCUMENTO,D_DEBITO,D_FEC_EMISION,D_FEC_VCTO,D_ANT_CUENTA,D_ANT_DOC,D_TRAMO,D_MONEDA,D_IMPORTE_FACTURADO,D_IMPORTE_PENDIENTE,D_IMPORTE_PENDIENTE_SOLES,D_MONTO_DISPUTA,D_GESTOR_COBRANZAS,D_CARTERA,D_CANAL,D_DISTRIBUIDOR,D_CONSULTOR,D_SUBCANAL,D_GERENTE,D_SUBDIRECCION,D_JEFE,D_ASESOR,D_SUPERVISOR,D_SECTOR,D_REGION,D_ACCOUNT_MANAGER,D_GRUPO_ECON,D_CLIENTES_100,D_CARTAS_JUNIO,TELEFONO From [Hoja1$] order by D_RUC", cs)
            Dim dt As New DataTable
            dAdapter.Fill(dt)
            'dtCarga = dt
            Session("tablaCarga") = dt
            lblMsg.Text = "Cantidad de Registros : " & dt.Rows.Count

            'Dim dAdapter As New OleDbDataAdapter("SELECT D_RUC,D_RAZ_SOCIAL,D_CODIGO_BSCS,D_NRO_DOCUMENTO,D_CODIGO,D_SERVICIO,D_AGENTE_DNINO,D_TIPO_CLIENTE,D_FORMA_PAGO,D_ESTADO_CUENTA,D_FEC_ACTIVACION,D_DEPARTAMENTO,D_PROVINCIA,D_DISTRITO,D_CANT_LINEAS_A,D_SEGMENTO,D_TIPO_SEGMENTO,D_CICLO,D_NOMBRE_CICLO,D_SERVICIO_PRESTADO,D_ESTADO_DOCUMENTO,D_TIPO_DOCUMENTO,D_DEBITO,D_FEC_EMISION,D_FEC_VCTO,D_ANT_CUENTA,D_ANT_DOC,D_TRAMO,D_MONEDA,D_IMPORTE_FACTURADO,D_IMPORTE_PENDIENTE,D_IMPORTE_PENDIENTE_SOLES,D_MONTO_DISPUTA,D_GESTOR_COBRANZAS,D_CARTERA,D_CANAL,D_DISTRIBUIDOR,D_CONSULTOR,D_SUBCANAL,D_GERENTE,D_SUBDIRECCION,D_JEFE,D_ASESOR,D_SUPERVISOR,D_SECTOR,D_REGION,D_ACCOUNT_MANAGER,D_GRUPO_ECON,D_CLIENTES_100,D_CARTAS_JUNIO,TELEFONO From [Hoja1$] order by D_RUC", cs)
            'Dim lis As New List(Of String)



            Dim dAdaptertt As New OleDbDataAdapter("SELECT distinct D_RUC From [Hoja1$] ", cs)
            Dim dtt As New DataTable
            dAdaptertt.Fill(dtt)
            'dtCarga1 = dtt
            Session("tablaCarga1") = dtt

            'lblMsg.Text = "Total de Tabla con RUC: " & dtt.Rows.Count()
            'Exit Sub

            ' mostrar los 50 primeros
            Dim dAdapter1 As New OleDbDataAdapter("Select top 50 * From [Hoja1$]", cs)
            Dim dt1 As New DataTable
            dAdapter1.Fill(dt1)

            grvCarga.DataSource = dt1
            grvCarga.DataBind()

            'If dt1.Rows.Count > 0 Then btnGenerar.Visible = True : lnkExportar.Visible = True Else btnGenerar.Visible = False : lnkExportar.Visible = False
            If dt1.Rows.Count > 0 Then btnGenerar.Visible = True : lnkExportar.Visible = False Else btnGenerar.Visible = False : lnkExportar.Visible = False
            dt1.Clear()
            dt1.Dispose()


        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Sub GUARDAR()
        Try


            Dim be As New BE_CLARO

            Dim dtId As DataTable = da.MAXIMO_ID_SCRIPTING_OUTBOUND_SEG_CLIENTES_TOP()

            Dim id As Integer = 0
            Dim idf As String 'Almacenar el primer id guardado
            Dim dtOrg As DataTable = Session("tablaCarga")
            Dim dtOrg1 As DataTable = Session("tablaCarga1")
            'Dim dtUnique As DataTable = dtOrg.Clone
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
            'Dim vwtb As New DataView(dtOrg)
            'Dim vwtb1 As New DataView(dtOrg1)

            'dtOrg1.Clear()
            'dtOrg1.Dispose()

            Session("tablaCarga") = Nothing
            Session("tablaCarga1") = Nothing

            'vwtb.Sort = "D_RUC"
            'dtUnique.ImportRow(vwtb.ToTable.Rows(0))
            Dim dtMostar As New DataTable
            Dim dtguardarDet As New DataTable
            id = dtId.Rows(0)("ID").ToString
            dtMostar.Columns.Add("ID", Type.GetType("System.String"))
            dtMostar.Columns.Add("RAZÓN SOCIAL", Type.GetType("System.String"))
            dtMostar.Columns.Add("RUC", Type.GetType("System.String"))
            dtMostar.Columns.Add("DEPARTAMENTO", Type.GetType("System.String"))
            dtMostar.Columns.Add("CICLO", Type.GetType("System.String"))
            dtMostar.Columns.Add("TRAMO", Type.GetType("System.String"))
            dtMostar.Columns.Add("LOGIN", Type.GetType("System.String"))
            dtMostar.Columns.Add("TELEFONO_01", Type.GetType("System.String"))

            dtId.Clear()
            dtId.Dispose()
            dtId = Nothing

            'lblMsg.Text = "Entre al For & For and If:  " & vwtb1.ToTable.Rows.Count() & "Tabla II: " & vwtb.ToTable.Rows.Count() '& "<br />" & vwtb1.ToTable.Rows(0)(0).ToString() & " = " & vwtb.ToTable.Rows(0)(0).ToString()
            'Exit Sub

            For i = 0 To dtOrg1.Rows.Count - 1
                For j = 0 To dtOrg.Rows.Count - 1
                    If dtOrg1.Rows(i)(0).ToString() = dtOrg.Rows(j)(0).ToString() Then

                        'dtUnique.ImportRow(dtOrg.ToTable.Rows(i))
                        id += 1
                        be.VAR_ID = id
                        be.VAR_D_RUC = dtOrg.Rows(j)("D_RUC").ToString
                        be.VAR_D_RAZ_SOCIAL = dtOrg.Rows(j)("D_RAZ_SOCIAL").ToString
                        be.VAR_D_CODIGO_BSCS = dtOrg.Rows(j)("D_CODIGO_BSCS").ToString
                        be.VAR_D_NRO_DOCUMENTO = dtOrg.Rows(j)("D_NRO_DOCUMENTO").ToString
                        be.VAR_D_CODIGO = dtOrg.Rows(j)("D_CODIGO").ToString
                        be.VAR_D_SERVICIO = dtOrg.Rows(j)("D_SERVICIO").ToString
                        be.VAR_D_AGENTE_DNINO = dtOrg.Rows(j)("D_AGENTE_DNINO").ToString
                        be.VAR_D_TIPO_CLIENTE = dtOrg.Rows(j)("D_TIPO_CLIENTE").ToString
                        be.VAR_D_FORMA_PAGO = dtOrg.Rows(j)("D_FORMA_PAGO").ToString
                        be.VAR_D_ESTADO_CUENTA = dtOrg.Rows(j)("D_ESTADO_CUENTA").ToString
                        be.VAR_D_FEC_ACTIVACION = dtOrg.Rows(j)("D_FEC_ACTIVACION").ToString
                        be.VAR_D_DEPARTAMENTO = dtOrg.Rows(j)("D_DEPARTAMENTO").ToString
                        be.VAR_D_PROVINCIA = dtOrg.Rows(j)("D_PROVINCIA").ToString
                        be.VAR_D_DISTRITO = dtOrg.Rows(j)("D_DISTRITO").ToString
                        be.VAR_D_CANT_LINEAS_A = dtOrg.Rows(j)("D_CANT_LINEAS_A").ToString
                        be.VAR_D_SEGMENTO = dtOrg.Rows(j)("D_SEGMENTO").ToString
                        be.VAR_D_TIPO_SEGMENTO = dtOrg.Rows(j)("D_TIPO_SEGMENTO").ToString
                        be.VAR_D_CICLO = dtOrg.Rows(j)("D_CICLO").ToString
                        be.VAR_D_NOMBRE_CICLO = dtOrg.Rows(j)("D_NOMBRE_CICLO").ToString
                        be.VAR_D_SERVICIO_PRESTADO = dtOrg.Rows(j)("D_SERVICIO_PRESTADO").ToString
                        be.VAR_D_ESTADO_DOCUMENTO = dtOrg.Rows(j)("D_ESTADO_DOCUMENTO").ToString
                        be.VAR_D_TIPO_DOCUMENTO = dtOrg.Rows(j)("D_TIPO_DOCUMENTO").ToString
                        be.VAR_D_DEBITO = dtOrg.Rows(j)("D_DEBITO").ToString
                        be.VAR_D_FEC_EMISION = dtOrg.Rows(j)("D_FEC_EMISION").ToString
                        be.VAR_D_FEC_VCTO = dtOrg.Rows(j)("D_FEC_VCTO").ToString
                        be.VAR_D_ANT_CUENTA = dtOrg.Rows(j)("D_ANT_CUENTA").ToString
                        be.VAR_D_ANT_DOC = dtOrg.Rows(j)("D_ANT_DOC").ToString
                        be.VAR_D_TRAMO = dtOrg.Rows(j)("D_TRAMO").ToString
                        be.VAR_D_MONEDA = dtOrg.Rows(j)("D_MONEDA").ToString
                        be.VAR_D_IMPORTE_FACTURADO = dtOrg.Rows(j)("D_IMPORTE_FACTURADO").ToString
                        be.VAR_D_IMPORTE_PENDIENTE = dtOrg.Rows(j)("D_IMPORTE_PENDIENTE").ToString
                        be.VAR_D_IMPORTE_PENDIENTE_SOLES = dtOrg.Rows(j)("D_IMPORTE_PENDIENTE_SOLES").ToString
                        be.VAR_D_MONTO_DISPUTA = dtOrg.Rows(j)("D_MONTO_DISPUTA").ToString
                        be.VAR_D_GESTOR_COBRANZAS = dtOrg.Rows(j)("D_GESTOR_COBRANZAS").ToString
                        be.VAR_D_CARTERA = dtOrg.Rows(j)("D_CARTERA").ToString
                        be.VAR_D_CANAL = dtOrg.Rows(j)("D_CANAL").ToString
                        be.VAR_D_DISTRIBUIDOR = dtOrg.Rows(j)("D_DISTRIBUIDOR").ToString
                        be.VAR_D_CONSULTOR = dtOrg.Rows(j)("D_CONSULTOR").ToString
                        be.VAR_D_SUBCANAL = dtOrg.Rows(j)("D_SUBCANAL").ToString
                        be.VAR_D_GERENTE = dtOrg.Rows(j)("D_GERENTE").ToString
                        be.VAR_D_SUBDIRECCION = dtOrg.Rows(j)("D_SUBDIRECCION").ToString
                        be.VAR_D_JEFE = dtOrg.Rows(j)("D_JEFE").ToString
                        be.VAR_D_ASESOR = dtOrg.Rows(j)("D_ASESOR").ToString
                        be.VAR_D_SUPERVISOR = dtOrg.Rows(j)("D_SUPERVISOR").ToString
                        be.VAR_D_SECTOR = dtOrg.Rows(j)("D_SECTOR").ToString
                        be.VAR_D_REGION = dtOrg.Rows(j)("D_REGION").ToString
                        be.VAR_D_ACCOUNT_MANAGER = dtOrg.Rows(j)("D_ACCOUNT_MANAGER").ToString
                        be.VAR_D_GRUPO_ECON = dtOrg.Rows(j)("D_GRUPO_ECON").ToString
                        be.VAR_D_CLIENTES_100 = dtOrg.Rows(j)("D_CLIENTES_100").ToString
                        be.VAR_D_CARTAS_JUNIO = dtOrg.Rows(j)("D_CARTAS_JUNIO").ToString
                        be.VAR_TEL1 = dtOrg.Rows(j)("TELEFONO").ToString

                        ms = da.SP_REGISTRAR_BASE_SEG_CLIENTES_TOP(be)

                        'ms = 1
                        Dim c As DataRow = dtMostar.NewRow
                        'c.Item(0) = be.VAR_ID
                        'c.Item(1) = be.VAR_D_RUC

                            Select ms
                            Case "0"
                                c_no_bien += 1
                                btnGenerar.Enabled = True
                                btnVisualizar.Enabled = False
                            Case "1"
                                If ms = "0" Then
                                    c_no_bien = c_no_bien + 1
                                    lblMsg.Text = "Nro de errores al guardar detalle: " & c_no_bien
                                Else
                                    c_bien += 1
                                    'Dim c As DataRow = dtMostar.NewRow
                                    c.Item(0) = be.VAR_ID
                                    c.Item(1) = be.VAR_D_RAZ_SOCIAL
                                    c.Item(2) = be.VAR_D_RUC
                                    c.Item(3) = be.VAR_D_DEPARTAMENTO
                                    c.Item(4) = be.VAR_D_CICLO
                                    c.Item(5) = be.VAR_D_TRAMO
                                    c.Item(6) = be.VAR_D_GESTOR_COBRANZAS
                                    Dim telefono1 As String = ""
                                    '        Dim telefono2 As String = ""
                                    '        Dim telefono3 As String = ""
                                    '        Dim telefono4 As String = ""
                                    '        Dim telefono5 As String = ""
                                    '        Dim telefono6 As String = ""

                                    If Not IsNumeric(be.VAR_TEL1) Then telefono1 = "" Else telefono1 = CInt(be.VAR_TEL1)
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
                                    c.Item(7) = telefono1




                                    dtMostar.Rows.InsertAt(c, dtMostar.Rows.Count)
                                End If
                                btnGenerar.Enabled = False
                                btnVisualizar.Enabled = True
                            Case Else
                                c_error += 1
                                btnGenerar.Enabled = True
                                btnVisualizar.Enabled = False
                        End Select
                                Exit For
                    End If

                Next


            Next

            'Obtengo registros unicos
            'Dim vwtb As New DataView(dtOrg)
            'vwtb.Sort = "D_RUC"
            'dtUnique.ImportRow(vwtb.ToTable.Rows(0))
            'For i = 1 To vwtb.Count - 1
            '    If String.Compare(vwtb.ToTable.Rows(i)(0).ToString(), vwtb.ToTable.Rows(i - 1)(0).ToString()) Then
            '        dtUnique.ImportRow(vwtb.ToTable.Rows(i))
            '    End If

            'Next


            'Dim dtMostar As New DataTable
            'Dim dtguardarDet As New DataTable

            'dtMostar.Columns.Add("ID", Type.GetType("System.String"))
            'dtMostar.Columns.Add("D_RUC", Type.GetType("System.String"))
            'dtMostar.Columns.Add("D_RUC", Type.GetType("System.String"))
            'dtMostar.Columns.Add("D_CODIGO", Type.GetType("System.String"))
            'dtMostar.Columns.Add("D_CODIGO_BSCS", Type.GetType("System.String"))
            'dtMostar.Columns.Add("D_RAZ_SOCIAL", Type.GetType("System.String"))
            'dtMostar.Columns.Add("TELEFONO_01", Type.GetType("System.String"))


            'If dtId.Rows.Count > 0 Then
            '    If Not IsNumeric(dtId.Rows(0)("ID").ToString) Then id = 0 Else id = dtId.Rows(0)("ID").ToString
            '    Dim dt As DataTable

            '    dt = dtUnique

            '    If dt.Rows.Count > 0 Then
            '        For i = 0 To dt.Rows.Count - 1
            '            id += 1
            'be.VAR_ID = id
            'be.VAR_D_RUC = dt.Rows(i)("D_RUC").ToString
            'be.VAR_D_SERVICIO = dt.Rows(i)("D_SERVICIO").ToString
            'be.VAR_D_CODIGO = dt.Rows(i)("D_CODIGO").ToString
            'be.VAR_D_CODIGO_BSCS = dt.Rows(i)("D_CODIGO_BSCS").ToString
            'be.VAR_D_RAZ_SOCIAL = dt.Rows(i)("D_RAZ_SOCIAL").ToString
            'be.VAR_D_AGENTE_DNINO = dt.Rows(i)("D_AGENTE_DNINO").ToString
            'be.VAR_D_TIPO_CLIENTE = dt.Rows(i)("D_TIPO_CLIENTE").ToString
            'be.VAR_D_FORMA_PAGO = dt.Rows(i)("D_FORMA_PAGO").ToString
            'be.VAR_D_ESTADO_CUENTA = dt.Rows(i)("D_ESTADO_CUENTA").ToString
            'be.VAR_D_FEC_ACTIVACION = dt.Rows(i)("D_FEC_ACTIVACION").ToString
            'be.VAR_D_DEPARTAMENTO = dt.Rows(i)("D_DEPARTAMENTO").ToString
            'be.VAR_D_PROVINCIA = dt.Rows(i)("D_PROVINCIA").ToString
            'be.VAR_D_DISTRITO = dt.Rows(i)("D_DISTRITO").ToString
            'be.VAR_D_CANT_LINEAS_A = dt.Rows(i)("D_CANT_LINEAS_A").ToString
            'be.VAR_D_SEGMENTO = dt.Rows(i)("D_SEGMENTO").ToString
            'be.VAR_D_TIPO_SEGMENTO = dt.Rows(i)("D_TIPO_SEGMENTO").ToString
            'be.VAR_D_CICLO = dt.Rows(i)("D_CICLO").ToString
            'be.VAR_D_NOMBRE_CICLO = dt.Rows(i)("D_NOMBRE_CICLO").ToString
            'be.VAR_D_SERVICIO_PRESTADO = dt.Rows(i)("D_SERVICIO_PRESTADO").ToString
            'be.VAR_D_ESTADO_DOCUMENTO = dt.Rows(i)("D_ESTADO_DOCUMENTO").ToString
            'be.VAR_D_TIPO_DOCUMENTO = dt.Rows(i)("D_TIPO_DOCUMENTO").ToString
            'be.VAR_D_DEBITO = dt.Rows(i)("D_DEBITO").ToString
            'be.VAR_D_NRO_DOCUMENTO = dt.Rows(i)("D_NRO_DOCUMENTO").ToString
            'be.VAR_D_FEC_EMISION = dt.Rows(i)("D_FEC_EMISION").ToString
            'be.VAR_D_FEC_VCTO = dt.Rows(i)("D_FEC_VCTO").ToString
            'be.VAR_D_ANT_CUENTA = dt.Rows(i)("D_ANT_CUENTA").ToString
            'be.VAR_D_ANT_DOC = dt.Rows(i)("D_ANT_DOC").ToString
            'be.VAR_D_TRAMO = dt.Rows(i)("D_TRAMO").ToString
            'be.VAR_D_MONEDA = dt.Rows(i)("D_MONEDA").ToString
            'be.VAR_D_IMPORTE_FACTURADO = dt.Rows(i)("D_IMPORTE_FACTURADO").ToString
            'be.VAR_D_IMPORTE_PENDIENTE = dt.Rows(i)("D_IMPORTE_PENDIENTE").ToString
            'be.VAR_D_IMPORTE_PENDIENTE_SOLES = dt.Rows(i)("D_IMPORTE_PENDIENTE_SOLES").ToString
            'be.VAR_D_MONTO_DISPUTA = dt.Rows(i)("D_MONTO_DISPUTA").ToString
            'be.VAR_D_GESTOR_COBRANZAS = dt.Rows(i)("D_GESTOR_COBRANZAS").ToString
            'be.VAR_D_CARTERA = dt.Rows(i)("D_CARTERA").ToString
            'be.VAR_D_CANAL = dt.Rows(i)("D_CANAL").ToString
            'be.VAR_D_DISTRIBUIDOR = dt.Rows(i)("D_DISTRIBUIDOR").ToString
            'be.VAR_D_CONSULTOR = dt.Rows(i)("D_CONSULTOR").ToString
            'be.VAR_D_SUBCANAL = dt.Rows(i)("D_SUBCANAL").ToString
            'be.VAR_D_GERENTE = dt.Rows(i)("D_GERENTE").ToString
            'be.VAR_D_SUBDIRECCION = dt.Rows(i)("D_SUBDIRECCION").ToString
            'be.VAR_D_JEFE = dt.Rows(i)("D_JEFE").ToString
            'be.VAR_D_ASESOR = dt.Rows(i)("D_ASESOR").ToString
            'be.VAR_D_SUPERVISOR = dt.Rows(i)("D_SUPERVISOR").ToString
            'be.VAR_D_SECTOR = dt.Rows(i)("D_SECTOR").ToString
            'be.VAR_D_REGION = dt.Rows(i)("D_REGION").ToString
            'be.VAR_D_ACCOUNT_MANAGER = dt.Rows(i)("D_ACCOUNT_MANAGER").ToString
            'be.VAR_D_GRUPO_ECON = dt.Rows(i)("D_GRUPO_ECON").ToString
            'be.VAR_D_CLIENTES_100 = dt.Rows(i)("D_CLIENTES_100").ToString
            'be.VAR_D_CARTAS_JUNIO = dt.Rows(i)("D_CARTAS_JUNIO").ToString
            'be.VAR_TEL1 = dt.Rows(i)("TELEFONO").ToString



            '
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'ms = da.SP_REGISTRAR_BASE_SEG_CLIENTES_TOP(be)
            'ms = 1

            'Select Case ms
            '    Case "0"
            '        c_no_bien += 1
            '        btnGenerar.Enabled = True
            '        btnVisualizar.Enabled = False
            '    Case "1"
            '        c_bien += 1
            '        Dim c As DataRow = dtMostar.NewRow
            '        c.Item(0) = be.VAR_ID
            '        Dim telefono1 As String = ""
            '        '        Dim telefono2 As String = ""
            '        '        Dim telefono3 As String = ""
            '        '        Dim telefono4 As String = ""
            '        '        Dim telefono5 As String = ""
            '        '        Dim telefono6 As String = ""

            '        If Not IsNumeric(be.VAR_TEL1) Then telefono1 = "" Else telefono1 = CInt(be.VAR_TEL1)
            '        Select Case Microsoft.VisualBasic.Left(telefono1, 1)
            '            Case "9"
            '                telefono1 = "'0034" & telefono1 & "'"
            '            Case "1"
            '                telefono1 = "'0034" & telefono1 & "'"
            '            Case Else
            '                If telefono1.Trim.Length > 0 Then
            '                    telefono1 = "'0351" & telefono1 & "'"
            '                Else
            '                    telefono1 = "'0034'"
            '                End If

            '        End Select
            '        c.Item(3) = telefono1



            'dtMostar.Rows.InsertAt(c, dtMostar.Rows.Count)
            'btnGenerar.Enabled = False
            'btnVisualizar.Enabled = True
            '    Case Else
            'c_error += 1
            'btnGenerar.Enabled = True
            'btnVisualizar.Enabled = False
            'End Select
            'Next

            'Dim ms As String
            'dt = dtOrg
            Dim err As Integer = 0
            Dim dtdoc As DataTable = da.SP_LISTAR_ID_SEG_CLIENTES_TOP(idf) 'Obtener lista de nrodocs
            For o = 0 To dtdoc.Rows.Count - 1
                Dim nrow() As DataRow = dtOrg.Select("D_RUC = '" & dtdoc.Rows(o)(1) & "'")

                For u = 0 To nrow.Length - 1
                    be.VAR_ID = dtdoc.Rows(o)(0)
                    'be.VAR_D_NRO_DOCUMENTO = nrow(u)("D_NRO_DOCUMENTO").ToString
                    be.VAR_D_SERVICIO = nrow(u)("D_SERVICIO").ToString
                    be.VAR_D_CODIGO = nrow(u)("D_CODIGO").ToString
                    be.VAR_D_CODIGO_BSCS = nrow(u)("D_CODIGO_BSCS").ToString
                    be.VAR_D_RAZ_SOCIAL = nrow(u)("D_RAZ_SOCIAL").ToString
                    be.VAR_D_RUC = nrow(u)("D_RUC").ToString
                    be.VAR_D_TIPO_CLIENTE = nrow(u)("D_TIPO_CLIENTE").ToString
                    be.VAR_D_FORMA_PAGO = nrow(u)("D_FORMA_PAGO").ToString
                    be.VAR_D_ESTADO_CUENTA = nrow(u)("D_ESTADO_CUENTA").ToString
                    be.VAR_D_FEC_ACTIVACION = nrow(u)("D_FEC_ACTIVACION").ToString
                    be.VAR_D_DEPARTAMENTO = nrow(u)("D_DEPARTAMENTO").ToString
                    be.VAR_D_PROVINCIA = nrow(u)("D_PROVINCIA").ToString
                    be.VAR_D_DISTRITO = nrow(u)("D_DISTRITO").ToString
                    be.VAR_D_CANT_LINEAS_A = nrow(u)("D_CANT_LINEAS_A").ToString
                    be.VAR_D_SEGMENTO = nrow(u)("D_SEGMENTO").ToString
                    be.VAR_D_TIPO_SEGMENTO = nrow(u)("D_TIPO_SEGMENTO").ToString
                    be.VAR_D_CICLO = nrow(u)("D_CICLO").ToString
                    be.VAR_D_NOMBRE_CICLO = nrow(u)("D_NOMBRE_CICLO").ToString
                    be.VAR_D_SERVICIO_PRESTADO = nrow(u)("D_SERVICIO_PRESTADO").ToString
                    be.VAR_D_ESTADO_DOCUMENTO = nrow(u)("D_ESTADO_DOCUMENTO").ToString
                    be.VAR_D_TIPO_DOCUMENTO = nrow(u)("D_TIPO_DOCUMENTO").ToString
                    be.VAR_D_DEBITO = nrow(u)("D_DEBITO").ToString
                    be.VAR_D_NRO_DOCUMENTO = nrow(u)("D_NRO_DOCUMENTO").ToString
                    be.VAR_D_FEC_EMISION = nrow(u)("D_FEC_EMISION").ToString
                    be.VAR_D_FEC_VCTO = nrow(u)("D_FEC_VCTO").ToString
                    be.VAR_D_ANT_CUENTA = nrow(u)("D_ANT_CUENTA").ToString
                    be.VAR_D_ANT_DOC = nrow(u)("D_ANT_DOC").ToString
                    be.VAR_D_TRAMO = nrow(u)("D_TRAMO").ToString
                    be.VAR_D_MONEDA = nrow(u)("D_MONEDA").ToString
                    be.VAR_D_IMPORTE_FACTURADO = nrow(u)("D_IMPORTE_FACTURADO").ToString
                    be.VAR_D_IMPORTE_PENDIENTE = nrow(u)("D_IMPORTE_PENDIENTE").ToString
                    be.VAR_D_IMPORTE_PENDIENTE_SOLES = nrow(u)("D_IMPORTE_PENDIENTE_SOLES").ToString
                    be.VAR_D_MONTO_DISPUTA = nrow(u)("D_MONTO_DISPUTA").ToString
                    be.VAR_D_GESTOR_COBRANZAS = nrow(u)("D_GESTOR_COBRANZAS").ToString
                    be.VAR_D_CARTERA = nrow(u)("D_CARTERA").ToString
                    be.VAR_D_CANAL = nrow(u)("D_CANAL").ToString
                    be.VAR_D_DISTRIBUIDOR = nrow(u)("D_DISTRIBUIDOR").ToString
                    be.VAR_D_CONSULTOR = nrow(u)("D_CONSULTOR").ToString
                    be.VAR_D_SUBCANAL = nrow(u)("D_SUBCANAL").ToString
                    be.VAR_D_GERENTE = nrow(u)("D_GERENTE").ToString
                    be.VAR_D_SUBDIRECCION = nrow(u)("D_SUBDIRECCION").ToString
                    be.VAR_D_JEFE = nrow(u)("D_JEFE").ToString
                    be.VAR_D_ASESOR = nrow(u)("D_ASESOR").ToString
                    be.VAR_D_SUPERVISOR = nrow(u)("D_SUPERVISOR").ToString
                    be.VAR_D_SECTOR = nrow(u)("D_SECTOR").ToString
                    be.VAR_D_REGION = nrow(u)("D_REGION").ToString
                    be.VAR_D_ACCOUNT_MANAGER = nrow(u)("D_ACCOUNT_MANAGER").ToString
                    be.VAR_D_GRUPO_ECON = nrow(u)("D_GRUPO_ECON").ToString
                    be.VAR_D_CLIENTES_100 = nrow(u)("D_CLIENTES_100").ToString
                    be.VAR_D_CARTAS_JUNIO = nrow(u)("D_CARTAS_JUNIO").ToString

                    ms = da.SP_REGISTRAR_SCRIPTING_DETALLE_FACTURA_SEG_CLI_TOP(be)
                    ms = 1
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
            '    End If
            'End If
        Catch ex As Exception
            lblMsg.Text = "error : " & ex.Message
        End Try

    End Sub
    Sub LIMPIAR()
        Session("tablaCarga") = Nothing
        Dim dt As DataTable = Session("tablaCarga")
        grvCarga.DataSource = dt
        grvCarga.DataBind()
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        GUARDAR()
        grvMostrar.Visible = True
    End Sub
    Sub ExportarTxt()
        Dim dt As DataTable = Session("tablaExportar")
        Dim str As New StringBuilder()
        Dim i As Integer
        str.Append("ID,TELEFONO_01")
        str.Append(vbNewLine)

        For i = 0 To dt.Rows.Count - 1
            Dim j As Integer
            For j = 0 To dt.Columns.Count - 1
                Dim cabecera As String = dt.Columns(j).ColumnName.ToString
                Dim campo As String = dt.Rows(i)(j).ToString
                If cabecera = "ID" Then campo = campo & ","
                If cabecera = "TELEFONO_01" Then campo = campo ' &  ","
                'If cabecera = "TELEFONO_02" Then campo = campo & ","
                'If cabecera = "TELEFONO_03" Then campo = campo & ","
                'If cabecera = "TELEFONO_04" Then campo = campo & ","
                'If cabecera = "TELEFONO_05" Then campo = campo & ","
                'If cabecera = "TELEFONO_06" Then campo = campo
                str.Append(campo)
            Next j

            str.Append(vbNewLine)
        Next i
        Dim nombre As String = "Seg_Cliente_Top" & Now.ToString("yyyyMMddHHmmss")
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

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click

        If grvMostrar.Rows.Count > 0 Then
            ExportarTxt()
        End If
    End Sub

    Protected Sub grvCarga_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvCarga.PageIndexChanging
        grvCarga.PageIndex = e.NewPageIndex
        grvCarga.DataSource = Session("tablaCarga")
        grvCarga.DataBind()
    End Sub



End Class
