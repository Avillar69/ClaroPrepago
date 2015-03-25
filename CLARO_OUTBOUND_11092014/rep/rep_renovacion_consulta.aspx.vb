Imports System.Data
Imports System.IO
Imports MySql.Data.MySqlClient

Partial Class rep_rep_porta
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO
    Dim cn As New MySqlConnection("datasource=192.168.150.35;username=reportes;password=r3p0rt3sd1n;database=BD_SCRIPTING_CLARO")

    Dim arr_finalBackoffice() As String = {"Seleccionar", "Alta", "No Alta_deuda", "No Alta_No Califica", "Pendiente"}
    Dim arr_usuario_activos() As String = { _
"09676008", _
"43501696", _
"43304392" _
}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim id_usu As String = Session("id")

            ''INVITADOS y  AGENTES
            If id_usu = "17" Or id_usu = "1" Then
                'btnGuardar.Visible = False
            End If
            'SP_LISTA_SERVICIOS_OUTBOUND()
            SP_LISTA_NIVEL_1(be)
            SP_LISTA_NIVEL_2(be)
            SP_LISTA_NIVEL_3(be)
            SP_LISTA_FINAL_CALIDAD()

            cboFinalBackoffice.DataSource = arr_finalBackoffice
            cboFinalBackoffice.DataBind()
        End If
    End Sub

    Sub SP_LISTA_NIVEL_1(ByVal be As BE_CLARO)


        be.VAR_NIVEL_1 = ""
        be.VAR_NIVEL_2 = ""
        be.VAR_NIVEL_3 = ""

        CBO_NIVEL_1.DataSource = da.SP_LISTA_NIVEL_FINALES(1, be)
        CBO_NIVEL_1.DataTextField = "Descripcion"
        CBO_NIVEL_1.DataValueField = "Descripcion"
        CBO_NIVEL_1.DataBind()
    End Sub

    Sub SP_LISTA_NIVEL_2(ByVal be As BE_CLARO)


        be.VAR_NIVEL_1 = CBO_NIVEL_1.Text
        be.VAR_NIVEL_2 = ""
        be.VAR_NIVEL_3 = ""

        CBO_NIVEL_2.DataSource = da.SP_LISTA_NIVEL_FINALES(2, be)
        CBO_NIVEL_2.DataTextField = "Descripcion"
        CBO_NIVEL_2.DataValueField = "Descripcion"
        CBO_NIVEL_2.DataBind()
    End Sub

    Sub SP_LISTA_NIVEL_3(ByVal be As BE_CLARO)


        be.VAR_NIVEL_1 = CBO_NIVEL_1.Text
        be.VAR_NIVEL_2 = CBO_NIVEL_2.Text
        be.VAR_NIVEL_3 = ""

        CBO_NIVEL_3.DataSource = da.SP_LISTA_NIVEL_FINALES(3, be)
        CBO_NIVEL_3.DataTextField = "Descripcion"
        CBO_NIVEL_3.DataValueField = "Descripcion"
        CBO_NIVEL_3.DataBind()
    End Sub

    Sub SP_LISTA_FINAL_CALIDAD()
        cboFinalCalidad.Items.Clear()
        Dim dt As DataTable = da.SP_CODIGOS_PRESENCE(291)
        cboFinalCalidad.DataSource = dt
        cboFinalCalidad.DataValueField = "QCODE"
        cboFinalCalidad.DataTextField = "DESCRIPTION"
        cboFinalCalidad.DataBind()

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        SP_LISTA_VENTA_X_ID_TELEFONO()

    End Sub

    Sub limpiar()
        'btnGuardar.Visible = False
        TXT_CAC_CAMPANIA.Text = ""
        TXT_CAC_NOM_CLIENTE.Text = ""
        TXT_CAC_DNI.Text = ""
        TXT_CAC_PLAN_OFRECIDO.Text = ""
        TXT_CAC_MARCA_MODELO.Text = ""
        TXT_CAC_PLAZO_CONTRATO.Text = ""
        TXT_CAC_TOPE_CONSUMO.Text = ""
        TXT_PAGO_EQ_FRACCIONADO.Text = ""
        txtId.Text = ""
        TXT_PRECIO_EQ.Text = ""
        TXT_CAC_CALLCENTER.Text = ""
        OBS_BACKOFFICE.Text = ""
        CBO_NIVEL_1.SelectedItem.Text = "Seleccionar"
        CBO_NIVEL_2.SelectedItem.Text = "Seleccionar"
        CBO_NIVEL_3.SelectedItem.Text = "Seleccionar"
        cboFinalCalidad.SelectedValue = "999"
        cboFinalBackoffice.SelectedItem.Text = "Seleccionar"

        grvHistorial.DataSource = Nothing
        grvHistorial.DataBind()

        grvUltimoResultado.DataSource = Nothing
        grvUltimoResultado.DataBind()

    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If TXT_CAC_CAMPANIA.Text.Trim.Length = "0" Then
            lblMsg.Text = "Campaña es obligatorio"
            TXT_CAC_CAMPANIA.Focus()
            Exit Sub
        End If
        If TXT_CAC_NOM_CLIENTE.Text.Trim.Length = "0" Then
            lblMsg.Text = "Cliente es obligatorio"
            TXT_CAC_NOM_CLIENTE.Focus()
            Exit Sub
        End If
        If TXT_CAC_DNI.Text.Trim.Length = "0" Then
            lblMsg.Text = "Dni es obligatorio"
            TXT_CAC_DNI.Focus()
            Exit Sub
        End If
        If TXT_CAC_PLAN_OFRECIDO.Text.Trim.Length = "0" Then
            lblMsg.Text = "Plan Ofrecido es obligatorio"
            TXT_CAC_PLAN_OFRECIDO.Focus()
            Exit Sub
        End If
        If TXT_CAC_MARCA_MODELO.Text.Trim.Length = "0" Then
            lblMsg.Text = "Marca-Modelo es obligatorio"
            TXT_CAC_MARCA_MODELO.Focus()
            Exit Sub
        End If
        If TXT_CAC_PLAZO_CONTRATO.Text.Trim.Length = "0" Then
            lblMsg.Text = "Plazo Contrato es obligatorio"
            TXT_CAC_PLAZO_CONTRATO.Focus()
            Exit Sub
        End If
        If TXT_CAC_TOPE_CONSUMO.Text.Trim.Length = "0" Then
            lblMsg.Text = "Tope Consumo es obligatorio"
            TXT_CAC_TOPE_CONSUMO.Focus()
            Exit Sub
        End If
        If TXT_PAGO_EQ_FRACCIONADO.Text.Trim.Length = "0" Then
            lblMsg.Text = "Pago Eq Fraccionado es obligatorio"
            TXT_PAGO_EQ_FRACCIONADO.Focus()
            Exit Sub
        End If
        If TXT_PRECIO_EQ.Text.Trim.Length = "0" Then
            lblMsg.Text = "Precio Equipo es obligatorio"
            TXT_PRECIO_EQ.Focus()
            Exit Sub
        End If
        If TXT_CAC_CALLCENTER.Text.Trim.Length = "0" Then
            lblMsg.Text = "Callcenter es obligatorio"
            TXT_CAC_CALLCENTER.Focus()
            Exit Sub
        End If

        lblMsg.Text = ""

        be.VAR_ID = txtId.Text
        be.VAR_TXT_CAC_CAMPANIA = TXT_CAC_CAMPANIA.Text
        be.VAR_TXT_CAC_NOM_CLIENTE = TXT_CAC_NOM_CLIENTE.Text.Trim.ToUpper
        be.VAR_TXT_CAC_DNI = TXT_CAC_DNI.Text.Trim.ToUpper
        be.VAR_TXT_CAC_PLAN_OFRECIDO = TXT_CAC_PLAN_OFRECIDO.Text.Trim.ToUpper
        be.VAR_TXT_CAC_MARCA_MODELO = TXT_CAC_MARCA_MODELO.Text.Trim.ToUpper
        be.VAR_TXT_CAC_PLAZO_CONTRATO = TXT_CAC_PLAZO_CONTRATO.Text.Trim.ToUpper
        be.VAR_TXT_CAC_TOPE_CONSUMO = TXT_CAC_TOPE_CONSUMO.Text.Trim.ToUpper
        be.VAR_TXT_PAGO_EQ_FRACCIONADO = TXT_PAGO_EQ_FRACCIONADO.Text
        be.VAR_TXT_PRECIO_EQ = TXT_PRECIO_EQ.Text
        be.VAR_TXT_CAC_CALLCENTER = TXT_CAC_CALLCENTER.Text.Trim
        be.VAR_NIVEL_1 = CBO_NIVEL_1.SelectedItem.Text
        be.VAR_NIVEL_2 = CBO_NIVEL_2.SelectedItem.Text
        be.VAR_NIVEL_3 = CBO_NIVEL_3.SelectedItem.Text

        be.VAR_TXT_RESULTADO_BACKOFFICE = cboFinalBackoffice.SelectedItem.Text.Trim.ToUpper
        be.VAR_TXT_RESULTADO_CALIDAD = cboFinalCalidad.SelectedValue
        be.VAR_OBS_BACKOFFICE = OBS_BACKOFFICE.Text.Trim.ToUpper

        'be.dni = Session("dni")
        be.login = Session("usuario")

        lblMsg.Text = da.SP_ACTUALIZAR_DATOS_X_FICHA_WEB_130620(be)
        limpiar()
        'SP_REPORTE_HISTORIAL_VENTA_130620()
        'SP_ULTIMO_RESULTADO(txtId.Text)
        'SP_REPORTE_HISTORIAL_VENTA_130620()
    End Sub

    Sub SP_LISTA_VENTA_X_ID_TELEFONO()
        grvResultado.DataSource = Nothing
        grvResultado.DataBind()
        lblMsg.Text = ""

        If txtParametro.Text.Trim.Length = 0 Then
            lblMsg.Text = "Ingrese un dato a buscar" : Exit Sub
        End If

        be.parametro = txtParametro.Text.Trim

        If cbo_ConsultaPor.SelectedIndex = 1 Then
            be.tipoConsulta = 1
        ElseIf cbo_ConsultaPor.SelectedIndex = 2 Then
            be.tipoConsulta = 2
        Else
            lblMsg.Text = "Seleccione Tipo de Consulta"
        End If

        Dim da As New MySqlDataAdapter("BD_SCRIPTING_CLARO.SP_LISTA_BASE_RENOVACIONES", cn)
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        With da.SelectCommand.Parameters
            .Add("VAR_TIPO", MySqlDbType.Int32).Value = be.tipoConsulta
            .Add("VAR_PARAMETRO", MySqlDbType.VarChar, 300).Value = be.parametro
        End With
        da.SelectCommand.CommandTimeout = 60000
        Dim dt As New DataTable
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            grvResultado.DataSource = dt
            grvResultado.DataBind()
        Else
            lblMsg.Text = "No hay datos con parametro de busqueda"
        End If
    End Sub

    Sub SP_REPORTE_HISTORIAL_VENTA_130620()
        be.VAR_ID = txtId.Text
        Dim dt As DataTable = da.SP_REPORTE_HISTORIAL_VENTA_130620(be)
        If dt.Rows.Count > 0 Then
            grvHistorial.DataSource = dt
            grvHistorial.DataBind()
        Else
            grvHistorial.DataSource = Nothing
            grvHistorial.DataBind()
        End If
    End Sub

    'Sub SP_LISTA_SERVICIOS_OUTBOUND()
    '    Try
    '        Dim dt As DataTable = da.SP_LISTA_SERVICIO
    '        With CBO_TIPIFICACION
    '            .DataTextField = "DESCRIPCION"
    '            .DataValueField = "QCODE"
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '    Catch ex As Exception
    '        MsgBox(" SP_LISTA_SERVICIOS_OUTBOUND : " & ex.Message)
    '    End Try
    'End Sub

    Sub SP_ULTIMO_RESULTADO_RENOVACIONES(ByVal ID_SCR As String)
        grvUltimoResultado.DataSource = Nothing


        Dim dt As DataTable = da.SP_ULTIMO_RESULTADO_RENO(ID_SCR)
        Dim dtt As DataTable = da.SP_LISTA_ULTIMO_RESULTADO_RENO(ID_SCR)
        For i = 0 To dt.Rows.Count - 1

            Dim IDhIS As String = dt.Rows(i)("ID").ToString.Trim

            For j = 0 To dtt.Rows.Count - 1

                Dim IDScrip As String = dtt.Rows(j)("ID").ToString.Trim
                Dim obs_back = dt.Rows(i)("OBSERVACION").ToString
                If IDhIS = IDScrip Then

                    obs_back = dtt.Rows(j)("OBS").ToString
                End If


                Dim dt2 As DataTable = da.SP_ULTIMO_RESULTADO_BACKOFFICE_RENO(ID_SCR)
                If dt2.Rows.Count > 0 Then

                    If IDhIS = IDScrip Then
                        Dim dr As DataRow = dt.NewRow
                        dr.Item(0) = dt2.Rows(0)(0)
                        dr.Item(1) = dt2.Rows(0)(1)
                        dr.Item(2) = dt2.Rows(0)(2)
                        dr.Item(3) = dt2.Rows(0)(3)
                        dr.Item(4) = dt2.Rows(0)(4)
                        dr.Item(5) = dt2.Rows(0)(5)
                        dr.Item(6) = dt2.Rows(0)(6)
                        dt.Rows.InsertAt(dr, dt.Rows.Count)

                    End If
                End If
            Next
        Next
        If dt.Rows.Count > 0 Then
            grvUltimoResultado.DataSource = dt
            grvUltimoResultado.DataBind()
        End If

    End Sub

    Protected Sub grvResultado_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles grvResultado.SelectedIndexChanging
        limpiar()
        Try
            'PARAMETRO PARA IDENTIFICAR A QUE MES PERTENECE LA VENTA
            'SI ES 201401 - GESTOR = NEOTEL --> enviar los finales de esta campaña.
            Dim sms As String = ""

            Dim fila As String = e.NewSelectedIndex
            be.VAR_ID = grvResultado.Rows(fila).Cells(1).Text
            Dim dt As DataTable = da.SP_LISTA_VENTAS_X_ID(be)
            If dt.Rows.Count > 0 Then
                For Each i As String In arr_usuario_activos
                    If Session("usuario").ToString = i Then
                        btnGuardar.Visible = True
                        Exit For
                    End If
                Next

                grvResultado.DataSource = Nothing
                grvResultado.DataBind()

                'sms = CStr(Year(dt.Rows(0)("FECHA_VENTA").ToString)) & Mid(dt.Rows(0)("FECHA_VENTA").ToString, 4, 2)

                txtId.Text = dt.Rows(0)("ID").ToString
                TXT_CAC_CAMPANIA.Text = dt.Rows(0)("TXT_CAC_CAMPANIA").ToString
                TXT_CAC_NOM_CLIENTE.Text = dt.Rows(0)("TXT_CAC_NOM_CLIENTE").ToString
                TXT_CAC_DNI.Text = dt.Rows(0)("TXT_CAC_DNI").ToString
                TXT_CAC_PLAN_OFRECIDO.Text = dt.Rows(0)("TXT_CAC_PLAN_OFRECIDO").ToString
                TXT_CAC_MARCA_MODELO.Text = dt.Rows(0)("TXT_CAC_MARCA_MODELO").ToString
                TXT_CAC_PLAZO_CONTRATO.Text = dt.Rows(0)("TXT_CAC_PLAZO_CONTRATO").ToString
                TXT_CAC_TOPE_CONSUMO.Text = dt.Rows(0)("TXT_CAC_TOPE_CONSUMO").ToString
                TXT_PAGO_EQ_FRACCIONADO.Text = dt.Rows(0)("TXT_PAGO_EQ_FRACCIONADO").ToString
                TXT_PRECIO_EQ.Text = dt.Rows(0)("TXT_PRECIO_EQ").ToString
                TXT_CAC_CALLCENTER.Text = dt.Rows(0)("TXT_CAC_CALLCENTER").ToString
                CBO_NIVEL_1.SelectedItem.Text = dt.Rows(0)("CBO_NIVEL_1").ToString
                CBO_NIVEL_2.SelectedItem.Text = dt.Rows(0)("CBO_NIVEL_2").ToString
                CBO_NIVEL_3.SelectedItem.Text = dt.Rows(0)("CBO_NIVEL_3").ToString

                If dt.Rows(0)("TXT_RESULTADO_BACKOFFICE").ToString = "" Or dt.Rows(0)("TXT_RESULTADO_BACKOFFICE").ToString = Nothing Then

                    cboFinalBackoffice.SelectedItem.Text = "Seleccionar"
                Else

                    cboFinalBackoffice.SelectedItem.Text = dt.Rows(0)("TXT_RESULTADO_BACKOFFICE").ToString
                End If



                If dt.Rows(0)("CODE_CALIDAD").ToString = "" Or dt.Rows(0)("CODE_CALIDAD").ToString = Nothing Then

                    cboFinalCalidad.SelectedValue = "999"
                Else
                    cboFinalCalidad.SelectedValue = dt.Rows(0)("CODE_CALIDAD").ToString

                End If
                OBS_BACKOFFICE.Text = dt.Rows(0)("OBS_BACKOFFICE").ToString

                SP_ULTIMO_RESULTADO_RENOVACIONES(txtId.Text)

                'SP_REPORTE_HISTORIAL_VENTA_130620()

                'If Session("id").ToString = "17" Then
                '    btnGuardar.Visible = False
                'End If

            Else
                lblMsg.Text = "No hay datos"
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Protected Sub CBO_NIVEL_1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CBO_NIVEL_1.SelectedIndexChanged
        If CBO_NIVEL_1.SelectedIndex > 0 Then
            SP_LISTA_NIVEL_2(be)
        End If
    End Sub

    Protected Sub CBO_NIVEL_2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CBO_NIVEL_2.SelectedIndexChanged
        If CBO_NIVEL_2.SelectedIndex > 0 Then
            SP_LISTA_NIVEL_3(be)
        End If
    End Sub


   
End Class
