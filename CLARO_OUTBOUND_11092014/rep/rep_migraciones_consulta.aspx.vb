Imports System.Data
Imports System.IO
Imports MySql.Data.MySqlClient

Partial Class rep_re_general_migraciones
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO_MIGRA
    Dim cn As New MySqlConnection("datasource=192.168.150.35;username=reportes;password=r3p0rt3sd1n;database=BD_SCRIPTING_CLARO")

    Dim arr_usuario_activos() As String = { _
"09676008", _
"43501696", _
"43304392" _
}
    Dim arr_finalBackoffice() As String = {"Seleccionar", "Alta", "No Alta_deuda", "No Alta_No Califica", "Pendiente"}
    Dim arr_finalCalidad() As String = {"Seleccionar", "APROBADO", "DESAPROBADO", "RECUPERABLE"}


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim id_usu As String = Session("id")

            ''INVITADOS y  AGENTES
            If id_usu = "17" Or id_usu = "1" Then
                btnGuardar.Visible = False
            End If
            'SP_LISTA_SERVICIOS_OUTBOUND()
            SP_LISTA_UBIGEO_DEPARTAMENTO()
            SP_LISTA_UBIGEO_PROVINCIA(cboDepartamento.Text.ToString.Trim)
            SP_LISTA_UBIGEO_DISTRITO(cboDepartamento.Text.ToString.Trim, cboProvincia.Text.ToString.Trim)
            SP_LISTA_NIVEL_1(be)
            SP_LISTA_NIVEL_2(be)
            SP_LISTA_NIVEL_3(be)
            SP_LISTA_NIVEL_4(be)
            SP_LISTA_FINAL_CALIDAD()

            cboFinalBackoffice.DataSource = arr_finalBackoffice
            cboFinalBackoffice.DataBind()

        End If

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        limpiar()
        SP_LISTA_VENTA_X_ID_TELEFONO()

    End Sub

    Sub SP_LISTA_NIVEL_1(ByVal be As BE_CLARO_MIGRA)


        be.VAR_NIVEL_1 = ""
        be.VAR_NIVEL_2 = ""
        be.VAR_NIVEL_3 = ""

        CBO_NIVEL_1.DataSource = da.SP_LISTA_NIVEL_TIPIFICACION(1, be)
        CBO_NIVEL_1.DataTextField = "Descripcion"
        CBO_NIVEL_1.DataValueField = "Descripcion"
        CBO_NIVEL_1.DataBind()
    End Sub

    Sub SP_LISTA_NIVEL_2(ByVal be As BE_CLARO_MIGRA)


        be.VAR_NIVEL_1 = CBO_NIVEL_1.Text
        be.VAR_NIVEL_2 = ""
        be.VAR_NIVEL_3 = ""

        CBO_NIVEL_2.DataSource = da.SP_LISTA_NIVEL_TIPIFICACION(2, be)
        CBO_NIVEL_2.DataTextField = "Descripcion"
        CBO_NIVEL_2.DataValueField = "Descripcion"
        CBO_NIVEL_2.DataBind()
    End Sub

    Sub SP_LISTA_NIVEL_3(ByVal be As BE_CLARO_MIGRA)


        be.VAR_NIVEL_1 = CBO_NIVEL_1.Text
        be.VAR_NIVEL_2 = CBO_NIVEL_2.Text
        be.VAR_NIVEL_3 = ""

        CBO_NIVEL_3.DataSource = da.SP_LISTA_NIVEL_TIPIFICACION(3, be)
        CBO_NIVEL_3.DataTextField = "Descripcion"
        CBO_NIVEL_3.DataValueField = "Descripcion"
        CBO_NIVEL_3.DataBind()
    End Sub

    Sub SP_LISTA_NIVEL_4(ByVal be As BE_CLARO_MIGRA)


        be.VAR_NIVEL_1 = CBO_NIVEL_1.Text
        be.VAR_NIVEL_2 = CBO_NIVEL_2.Text
        be.VAR_NIVEL_3 = CBO_NIVEL_3.Text

        CBO_NIVEL_4.DataSource = da.SP_LISTA_NIVEL_TIPIFICACION(4, be)
        CBO_NIVEL_4.DataTextField = "Descripcion"
        CBO_NIVEL_4.DataValueField = "Descripcion"
        CBO_NIVEL_4.DataBind()
    End Sub

    Sub SP_LISTA_UBIGEO_DEPARTAMENTO()
        Dim dt As DataTable = da.SP_LISTA_UBIGEO_DEPARTAMENTO()

        cboDepartamento.DataSource = dt
        cboDepartamento.DataTextField = "DESC"
        cboDepartamento.DataValueField = "ID"
        cboDepartamento.DataBind()

    End Sub

    Sub SP_LISTA_UBIGEO_PROVINCIA(ByVal str_Departamento As String)
        Dim dt As DataTable = da.SP_LISTA_UBIGEO_PROVINCIA(str_Departamento)
        cboProvincia.DataSource = dt
        cboProvincia.DataValueField = "ID"
        cboProvincia.DataTextField = "DESC"
        cboProvincia.DataBind()
    End Sub

    Sub SP_LISTA_UBIGEO_DISTRITO(ByVal str_Departamento As String, ByVal str_Provincia As String)
        Dim dt As DataTable = da.SP_LISTA_UBIGEO_DISTRITO(str_Departamento, str_Provincia)
        cboDistrito.DataSource = dt
        cboDistrito.DataValueField = "ID"
        cboDistrito.DataTextField = "DESC"
        cboDistrito.DataBind()
    End Sub

    Sub SP_LISTA_FINAL_CALIDAD()
        cboFinalCalidad.Items.Clear()
        Dim dt As DataTable = da.SP_CODIGOS_PRESENCE(294)
        cboFinalCalidad.DataSource = dt
        cboFinalCalidad.DataValueField = "QCODE"
        cboFinalCalidad.DataTextField = "DESCRIPTION"
        cboFinalCalidad.DataBind()

    End Sub

    Sub limpiar()

        TXT_PLAN_POSTPAGO_CONTRATADO.Text = ""
        TXT_CARGO_FIJO_MENSUAL.Text = ""
        TXT_CICLO_FACTURACION.Text = ""
        TXT_NOMBRES.Text = ""
        TXT_APELLIDOS.Text = ""
        TXT_NRO_DNI.Text = ""
        TXT_NRO_MIGRAR.Text = ""
        txtId.Text = ""
        TXT_LUGAR_NAC.Text = ""
        TXT_TELEFONO_REF.Text = ""
        TXT_EMAIL.Text = ""
        DTP_FEC_NAC.Text = ""
        cboDepartamento.SelectedItem.Text = "Seleccionar"
        cboProvincia.SelectedItem.Text = "Seleccionar"
        cboDistrito.SelectedItem.Text = "Seleccionar"
        TXT_DIRECCION.Text = ""
        CBO_NIVEL_1.SelectedItem.Text = "Seleccionar"
        CBO_NIVEL_2.SelectedItem.Text = "Seleccionar"
        CBO_NIVEL_3.SelectedItem.Text = "Seleccionar"
        CBO_NIVEL_4.SelectedItem.Text = "Seleccionar"
        cboFinalCalidad.SelectedValue = "999"
        cboFinalBackoffice.SelectedItem.Text = "Seleccionar"

        grvHistorial.DataSource = Nothing
        grvHistorial.DataBind()

        grvUltimoResultado.DataSource = Nothing
        grvUltimoResultado.DataBind()

    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click



        lblMsg.Text = ""

        If cboFinalBackoffice.SelectedItem.Text = "SELECCIONAR" Then
            lblMsg.Text = "SELECCIONAR UN FINAL DE BACKOFFICE"
        Else

            be.VAR_ID = txtId.Text
            be.VAR_TXT_PLAN_POSTPAGO_CONTRATADO = TXT_PLAN_POSTPAGO_CONTRATADO.Text
            be.VAR_TXT_CARGO_FIJO_MENSUAL = TXT_CARGO_FIJO_MENSUAL.Text.Trim.ToUpper
            be.VAR_TXT_CICLO_FACTURACION = TXT_CICLO_FACTURACION.Text.Trim.ToUpper
            be.VAR_TXT_NOMBRES = TXT_NOMBRES.Text.Trim.ToUpper
            be.VAR_TXT_APELLIDOS = TXT_APELLIDOS.Text.Trim.ToUpper
            be.VAR_TXT_NRO_DNI = TXT_NRO_DNI.Text.Trim.ToUpper
            be.VAR_TXT_NRO_MIGRAR = TXT_NRO_MIGRAR.Text
            be.VAR_TXT_LUGAR_NAC = TXT_LUGAR_NAC.Text
            be.VAR_DTP_FEC_NAC = DTP_FEC_NAC.Text
            be.VAR_TXT_DIRECCION = TXT_DIRECCION.Text.Trim
            be.VAR_TXT_DISTRITO = cboDistrito.SelectedItem.Text.Trim.ToUpper
            be.VAR_TXT_TELEFONO_REF = TXT_TELEFONO_REF.Text.Trim.ToUpper
            be.VAR_TXT_DEPARTAMENTO = cboDepartamento.SelectedItem.Text.Trim.ToUpper
            be.VAR_TXT_PROVINCIA = cboProvincia.SelectedItem.Text.Trim.ToUpper
            be.VAR_TXT_EMAIL = TXT_EMAIL.Text.Trim.ToUpper
            be.VAR_NIVEL_1 = CBO_NIVEL_1.SelectedItem.Text.Trim.ToUpper
            be.VAR_NIVEL_2 = CBO_NIVEL_2.SelectedItem.Text.Trim.ToUpper
            be.VAR_NIVEL_3 = CBO_NIVEL_3.SelectedItem.Text.Trim.ToUpper
            be.VAR_NIVEL_4 = CBO_NIVEL_4.SelectedItem.Text.Trim.ToUpper
            be.VAR_TXT_RESULTADO_BACKOFFICE = cboFinalBackoffice.SelectedItem.Text.Trim.ToUpper
            be.VAR_TXT_RESULTADO_CALIDAD = cboFinalCalidad.SelectedValue

            be.VAR_OBS_BACKOFFICE = OBS_BACKOFFICE.Text.Trim.ToUpper
            'be.dni = Session("dni")
            be.login = Session("usuario")

            lblMsg.Text = da.SP_ACTUALIZAR_SCRIPTING_MIGRACIONES_WEB(be)
            'SP_REPORTE_HISTORIAL_VENTA_130620()
            'SP_ULTIMO_RESULTADO(txtId.Text)
            'SP_REPORTE_HISTORIAL_VENTA_130620()
            limpiar()
        End If
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

        Dim da As New MySqlDataAdapter("BD_SCRIPTING_CLARO.SP_LISTA_BASE_MIGRACIONES", cn)
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

    'Sub SP_REPORTE_HISTORIAL_VENTA_130620()
    '    be.VAR_ID = txtId.Text
    '    Dim dt As DataTable = da.SP_REPORTE_HISTORIAL_VENTA_130620(be)
    '    If dt.Rows.Count > 0 Then
    '        grvHistorial.DataSource = dt
    '        grvHistorial.DataBind()
    '    Else
    '        grvHistorial.DataSource = Nothing
    '        grvHistorial.DataBind()
    '    End If
    'End Sub

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

    Sub SP_ULTIMO_RESULTADO_MIGRACIONES(ByVal ID_SCR As String)
        grvUltimoResultado.DataSource = Nothing

        Dim dt As DataTable = da.SP_ULTIMO_RESULTADO_MIGRACIONES(be)
        Dim dt2 As DataTable = da.SP_ULTIMO_RESULTADO_BACKOFFICE_MIGRACIONES(be)
        If dt2.Rows.Count > 0 Then


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
        If dt.Rows.Count > 0 Then
            grvUltimoResultado.DataSource = dt
            grvUltimoResultado.DataBind()
        End If

    End Sub

    Protected Sub grvResultado_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles grvResultado.SelectedIndexChanging
        limpiar()
        SP_LISTA_FINAL_CALIDAD()
        Try
            'PARAMETRO PARA IDENTIFICAR A QUE MES PERTENECE LA VENTA
            'SI ES 201401 - GESTOR = NEOTEL --> enviar los finales de esta campaña.
            Dim sms As String = ""

            Dim fila As String = e.NewSelectedIndex
            be.VAR_ID = grvResultado.Rows(fila).Cells(1).Text
            Dim dt As DataTable = da.SP_LISTA_VENTAS_X_ID_MIGRACIONES(be)
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
                TXT_PLAN_POSTPAGO_CONTRATADO.Text = dt.Rows(0)("TXT_PLAN_POSTPAGO_CONTRATADO").ToString
                TXT_CARGO_FIJO_MENSUAL.Text = dt.Rows(0)("TXT_CARGO_FIJO_MENSUAL").ToString
                TXT_CICLO_FACTURACION.Text = dt.Rows(0)("TXT_CICLO_FACTURACION").ToString
                TXT_NOMBRES.Text = dt.Rows(0)("TXT_NOMBRES").ToString
                TXT_APELLIDOS.Text = dt.Rows(0)("TXT_APELLIDOS").ToString
                TXT_NRO_DNI.Text = dt.Rows(0)("TXT_NRO_DNI").ToString
                TXT_NRO_MIGRAR.Text = dt.Rows(0)("TXT_NRO_MIGRAR").ToString
                DTP_FEC_NAC.Text = dt.Rows(0)("DTP_FEC_NAC").ToString
                TXT_LUGAR_NAC.Text = dt.Rows(0)("TXT_LUGAR_NAC").ToString
                TXT_TELEFONO_REF.Text = dt.Rows(0)("TXT_TELEFONO_REF").ToString
                TXT_EMAIL.Text = dt.Rows(0)("TXT_EMAIL").ToString
                cboDepartamento.SelectedItem.Text = dt.Rows(0)("TXT_DEPARTAMENTO").ToString
                SP_LISTA_UBIGEO_PROVINCIA(cboDepartamento.SelectedValue.ToString.Trim)
                cboProvincia.SelectedItem.Text = dt.Rows(0)("TXT_PROVINCIA").ToString
                SP_LISTA_UBIGEO_DISTRITO(cboDepartamento.SelectedValue.ToString.Trim, cboProvincia.SelectedValue.ToString.Trim)
                cboDistrito.SelectedItem.Text = dt.Rows(0)("TXT_DISTRITO").ToString
                TXT_DIRECCION.Text = dt.Rows(0)("TXT_DIRECCION").ToString
                CBO_NIVEL_1.SelectedItem.Text = dt.Rows(0)("CBO_NIVEL_1").ToString
                CBO_NIVEL_2.SelectedItem.Text = dt.Rows(0)("CBO_NIVEL_2").ToString
                CBO_NIVEL_3.SelectedItem.Text = dt.Rows(0)("CBO_NIVEL_3").ToString
                CBO_NIVEL_4.SelectedItem.Text = dt.Rows(0)("CBO_NIVEL_4").ToString

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
                SP_ULTIMO_RESULTADO_MIGRACIONES(txtId.Text)

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

    Protected Sub cboDepartamento_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboDepartamento.SelectedIndexChanged
        SP_LISTA_UBIGEO_PROVINCIA(cboDepartamento.SelectedValue.ToString)
    End Sub

    Protected Sub cboProvincia_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboProvincia.SelectedIndexChanged
        SP_LISTA_UBIGEO_DISTRITO(cboDepartamento.SelectedValue.ToString, cboProvincia.SelectedValue.ToString)
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

    Protected Sub CBO_NIVEL_3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CBO_NIVEL_3.SelectedIndexChanged
        If CBO_NIVEL_3.SelectedIndex > 0 Then
            SP_LISTA_NIVEL_4(be)
        End If
    End Sub

End Class
