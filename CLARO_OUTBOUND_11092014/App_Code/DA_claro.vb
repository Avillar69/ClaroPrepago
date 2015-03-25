Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Data.OleDb

Public Class DA_claro
    Dim cn As New Conexion
    Dim cnxSql As New SqlConnection(cn.CNX_DNINO)
    Dim cnxMySql As New MySqlConnection(cn.CNX_MYSQL)
    Dim cnxMySqlDN As New MySqlConnection(cn.CNX_MYSQLDN)
    Dim cnxMySql2 As New MySqlConnection(cn.CNX_MYSQL_CARGAREG)
    Dim cnxSql2 As New SqlConnection(cn.CNX_DNINO2)
    Dim var_ruta As String = "C:\CrearBDTable\"

    Public Function LISTA_DE_CAMPANYA_PRESENCE(ByVal usuario As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim sql As String = ""
            If usuario = "claroinb" Then
                sql = "select ID,cast(ID as varchar)+' - '+NAME 'NAME'from dnino.sqlpr1.pview.service where TYPE='Inbound' AND ID IN(317,318,319,450)"
            Else
                sql = "select ID,cast(ID as varchar)+' - '+NAME 'NAME'from dnino.sqlpr1.pview.service where TYPE='Inbound' AND NAME LIKE '%CLARO%'"
            End If
            Dim cmd As New SqlCommand(sql, cnxSql)
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("LISTA_DE_CAMPANYA_PRESENCE " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_RESULTADO_PRESENCE(ByVal inicio As String, ByVal fin As String, ByVal servicio As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim sql As String = "SELECT ID,SERVICEID,QCODE'ID_FINAL', " & _
            "(SELECT DESCRIPTION FROM DNINO.SQLPR1.PVIEW.SERVICEQCODE B WHERE A.QCODE=B.QCODE AND A.serviceid=B.serviceid)'FINAL', " & _
            "LOGIN,(SELECT AGENTNAME FROM DNINO.SQLPR1.PVIEW.AGENTLOGIN B WHERE A.LOGIN=B.LOGIN)'AGENTE',PHONE'TELEFONO',TALKTIME,ACWTIME,STATION'ESTACION',VDN  " & _
            "FROM DNINO.SQLPR1.PREP.PCO_INBOUNDLOG A WHERE CAST(RDATE AS DATE)BETWEEN '" & inicio & "'  and '" & fin & "' and serviceid= '" & servicio & "'"
            Dim cmd As New SqlCommand(sql, cnxSql)
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_REPORTES_CLARO_INBOUND " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_REPORTES_CLARO_INBOUND(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_REPORTES_CLARO_INBOUND", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
                .Add("@CAM", SqlDbType.VarChar, 10).Value = be.campanya
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_REPORTES_CLARO_INBOUND " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_OBTENER_CARGAS_GESTPREV(ByVal serviceid As Integer, ByVal carga As Integer) As DataTable
        Dim cmd As New SqlCommand
        Dim dt As New DataTable
        Try
            cmd = New SqlCommand("SP_OBTENER_CARGAS_GESTINF", cnxSql)
            cmd.CommandTimeout = 600000
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@serviceid", serviceid)
            cmd.Parameters.AddWithValue("@LOADID ", carga)
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("Error")
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_OBTENER_CARGAS_RECUPERO(ByVal ini As String, ByVal fin As String, ByVal tipo As Integer) As DataTable
        Dim cmd As New MySqlCommand
        Dim dt As New DataTable
        Try
            Select Case tipo
                Case 1
                    cmd = New MySqlCommand("SP_OBTENER_CARGAS_RECUPERO_FISICA", cnxMySql)
                Case 2
                    cmd = New MySqlCommand("SP_OBTENER_CARGAS_RECUPERO_ELECTRONICO", cnxMySql)
                Case 3
                    cmd = New MySqlCommand("SP_OBTENER_CARGAS_RECUPERO_AFILIACION_RCE", cnxMySql)
            End Select
            cmd.CommandTimeout = 600000
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("VAR_INI", ini)
            cmd.Parameters.AddWithValue("VAR_FIN", fin)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_OBTENER_CARGAS_RECUPERO:" & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_RESULTADO_PRESENCE_CARGA_RECUPERO(ByVal servicio As String, ByVal cadenaIds As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim sql As String = "SELECT DISTINCT SOURCEID,PHONE FROM DNINO.SQLPR1.PVIEW.OUTBOUNDQUEUE WHERE SERVICEID='" + servicio + "' AND" _
                                + " SOURCEID IN (" + cadenaIds + ")"
            Dim cmd As New SqlCommand(sql, cnxSql)
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_RESULTADO_PRESENCE_CARGA_RECUPERO:" & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_RESULTADO_PRESENCE_CARGA_GESTPREV(ByVal serviceid As Integer, ByVal cadenaIds As String) As DataTable
        Dim dt As New DataTable
        Dim tabla As String = ""
        Dim campoDoc As String = ""
        Select Case serviceid
            Case 178
                tabla = "SCRIPTING_OUTBOUND_GESTION_PREVENTIVA"
                campoDoc = "D_NRO_DOCUMENTO"
            Case 189
                tabla = "SCRIPTING_OUTBOUND_GESTION_INFORMATIVA_NUM_PAGO"
                campoDoc = "D_DOCUMENTO"
        End Select
        Try
            Dim sql As String = "SELECT ID,D_CLIENTE," + campoDoc + " as 'D_NRO_DOCUMENTO' FROM " + tabla + " WHERE ID IN (" + cadenaIds + ")"
            Dim cmd As New MySqlCommand(sql, cnxMySql)
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_RESULTADO_PRESENCE_CARGA_RECUPERO " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_REPORTE_SCRIPTING(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_REPORTE_SCRIPTING", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_INI", MySqlDbType.VarChar, 10).Value = be.inicio
                .Add("VAR_FIN", MySqlDbType.VarChar, 10).Value = be.fin
                .Add("VAR_CAM", MySqlDbType.VarChar, 10).Value = be.campanya
            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_REPORTE_SCRIPTING " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    '********************* REVISAR ************************** 
    '********************* REVISAR ************************** 
    '********************* REVISAR ************************** 
    Public Function SP_REPORTE_HISTORIAL_VENTA_130620(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_REPORTE_HISTORIAL_VENTA_130620", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
            End With
            cmd.CommandTimeout = 6000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_REPORTE_HISTORIAL_VENTA_130620 " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTA_SERVICIO() As DataTable
        Dim dt As New DataTable
        Try
            Dim sql As String = ""
            sql = "SELECT QCODE, DESCRIPTION+' - Finales 190' AS 'DESCRIPCION',SERVICEID FROM DNINO.SQLPR1.PVIEW.SERVICEQCODE WHERE SERVICEID IN (190)" & _
                                     "AND (QCODE IN (90,91)OR QCODE>=100)" & _
                                     " UNION " & _
                                     "SELECT QCODE, DESCRIPTION+' - Finales 290' AS 'DESCRIPCION',SERVICEID FROM DNINO.SQLPR1.PVIEW.SERVICEQCODE WHERE SERVICEID IN (290)" &
                                     "AND (QCODE IN (90,91)OR QCODE>=100)" & _
                                     " UNION " & _
                                     "SELECT QCODE, DESCRIPTION+' - Finales 291' AS 'DESCRIPCION',SERVICEID FROM DNINO.SQLPR1.PVIEW.SERVICEQCODE WHERE SERVICEID IN (291)" & _
                                     "AND (QCODE IN (90,91)OR QCODE>=100)" & _
                                     "ORDER BY SERVICEID,QCODE"
            Dim cmd As New SqlCommand(sql, cnxSql)
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("LISTA_DE_CAMPANYA_PRESENCE " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt

    End Function

    Public Function SP_ULTIMO_RESULTADO(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("UNICEF_PERU.SP_ULTIMO_RESULTADO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
            End With
            cmd.CommandTimeout = 60000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_ULTIMO_RESULTADO " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_ACTUALIZAR_DATOS_X_FICHA_WEB_130620(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_ACTUALIZAR_SCRIPTING_RENOVACIONES_WEB", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .AddWithValue("VAR_ID", be.VAR_ID)
                .AddWithValue("VAR_TXT_CAC_CAMPANIA", be.VAR_TXT_CAC_CAMPANIA)
                .AddWithValue("VAR_TXT_CAC_NOM_CLIENTE", be.VAR_TXT_CAC_NOM_CLIENTE)
                .AddWithValue("VAR_TXT_CAC_DNI", be.VAR_TXT_CAC_DNI)
                .AddWithValue("VAR_TXT_CAC_PLAN_OFRECIDO", be.VAR_TXT_CAC_PLAN_OFRECIDO)
                .AddWithValue("VAR_TXT_CAC_MARCA_MODELO", be.VAR_TXT_CAC_MARCA_MODELO)
                .AddWithValue("VAR_TXT_CAC_PLAZO_CONTRATO", be.VAR_TXT_CAC_PLAZO_CONTRATO)
                .AddWithValue("VAR_TXT_CAC_TOPE_CONSUMO", be.VAR_TXT_CAC_TOPE_CONSUMO)
                .AddWithValue("VAR_TXT_PAGO_EQ_FRACCIONADO", be.VAR_TXT_PAGO_EQ_FRACCIONADO)
                .AddWithValue("VAR_TXT_PRECIO_EQ", be.VAR_TXT_PRECIO_EQ)
                .AddWithValue("VAR_TXT_CAC_CALLCENTER", be.VAR_TXT_CAC_CALLCENTER)
                .AddWithValue("VAR_CBO_NIVEL_1", be.VAR_NIVEL_1)
                .AddWithValue("VAR_CBO_NIVEL_2", be.VAR_NIVEL_2)
                .AddWithValue("VAR_CBO_NIVEL_3", be.VAR_NIVEL_3)
                .AddWithValue("VAR_TXT_RESULTADO_BACKOFFICE", be.VAR_TXT_RESULTADO_BACKOFFICE)
                .AddWithValue("VAR_TXT_RESULTADO_CALIDAD", be.VAR_TXT_RESULTADO_CALIDAD)
                .AddWithValue("VAR_LOGIN_CALIDAD", be.login)
                .AddWithValue("VAR_OBS_BACKOFFICE", be.VAR_OBS_BACKOFFICE)
            End With
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = 0 Then
                ms = "No se logro actualizar"
            Else
                ms = "Actualizacion correcta"
            End If
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_LISTA_VENTAS_X_ID(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTA_SCRIPTING_RENOVACIONES_X_ID", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
            End With
            cmd.CommandTimeout = 6000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTA_SCRIPTING_RENOVACIONES_X_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_LISTA_ACT_DIRECCIONES_X_FECHA(ByVal inicio As String, ByVal fin As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("SP_LISTAR_ACT_DIRECCIONES_2", cnxSql2)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .AddWithValue("@INICIO", inicio)
                .AddWithValue("@FIN", fin)
            End With
            cmd.CommandTimeout = 6000
            cnxSql2.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_ACT_DIRECCIONES" & ex.Message)
        Finally
            cnxSql2.Close()
        End Try
        Return dt
    End Function

    '********************* REVISAR ************************** 
    '********************* REVISAR ************************** 
    '********************* REVISAR ************************** 

    Public Function SP_TVSAT_RESUMEN_LLAMADAS_UNICAS(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_TVSAT_RESUMEN_LLAMADAS_UNICAS", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_INI", MySqlDbType.VarChar, 10).Value = be.inicio
                .Add("VAR_FIN", MySqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_TVSAT_RESUMEN_LLAMADAS_UNICAS " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function MAXIMO_ID() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID)'ID' FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_PORTABILIDAD", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function MAXIMO_ID_MIGRACIONES() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID)'ID' FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_MIGRACIONES", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function MAXIMO_ID_RECUPERO_ELECTRONICO() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID)'ID' FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_RECUPERO_ELECTRONICO", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function MAXIMO_ID_AFILIACION_CE() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID)'ID' FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_AFILIACION_RCE", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function MAXIMO_ID_CANCELACIONES() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID)'ID' FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CANCELACIONES", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function MAXIMO_ID_ENCUESTAS() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID)'ID' FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_ENCUESTA_POSTPAGO", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_REGISTRAR_BASE_SCRIPTING_OUTBOUND_PORTABILIDAD(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_REGISTRAR_BASE", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
                .Add("VAR_D_TELEF_MOVIL", MySqlDbType.VarChar, 100).Value = be.VAR_D_TELEF_MOVIL
                .Add("VAR_D_FECHA_ENVIO", MySqlDbType.VarChar, 100).Value = be.VAR_D_FECHA_ENVIO
                .Add("VAR_D_FECHA_GESTION", MySqlDbType.VarChar, 100).Value = be.VAR_D_FECHA_GESTION
                .Add("VAR_D_SOLICITUD", MySqlDbType.VarChar, 100).Value = be.VAR_D_SOLICITUD
                .Add("VAR_D_NRO_SEC", MySqlDbType.VarChar, 100).Value = be.VAR_D_NRO_SEC
                .Add("VAR_D_CANTIDAD", MySqlDbType.VarChar, 100).Value = be.VAR_D_CANTIDAD
                .Add("VAR_D_TIPO", MySqlDbType.VarChar, 100).Value = be.VAR_D_TIPO
                .Add("VAR_D_Despacho", MySqlDbType.VarChar, 100).Value = be.VAR_D_Despacho
                .Add("VAR_D_VENTA_EFECTIVA", MySqlDbType.VarChar, 100).Value = be.VAR_D_VENTA_EFECTIVA
                .Add("VAR_D_OPERADOR_CEDENTE", MySqlDbType.VarChar, 100).Value = be.VAR_D_OPERADOR_CEDENTE
                .Add("VAR_D_OPERADOR_RECEPTOR", MySqlDbType.VarChar, 100).Value = be.VAR_D_OPERADOR_RECEPTOR
                .Add("VAR_D_MODALIDAD_TELEFONO", MySqlDbType.VarChar, 100).Value = be.VAR_D_MODALIDAD_TELEFONO
                .Add("VAR_D_CONTACTO_CLIENTE", MySqlDbType.VarChar, 100).Value = be.VAR_D_CONTACTO_CLIENTE
                .Add("VAR_D_TIPO_DOC", MySqlDbType.VarChar, 100).Value = be.VAR_D_TIPO_DOC
                .Add("VAR_D_NRO_DOCUMENTO", MySqlDbType.VarChar, 100).Value = be.VAR_D_NRO_DOCUMENTO
                .Add("VAR_D_NOMBRE_CLIENTE", MySqlDbType.VarChar, 100).Value = be.VAR_D_NOMBRE_CLIENTE
                .Add("VAR_D_ESTADO_SP", MySqlDbType.VarChar, 100).Value = be.VAR_D_ESTADO_SP
                .Add("VAR_D_TIPO_MENSAJE_SP", MySqlDbType.VarChar, 100).Value = be.VAR_D_TIPO_MENSAJE_SP
                .Add("VAR_D_MOTIVO_SP", MySqlDbType.VarChar, 100).Value = be.VAR_D_MOTIVO_SP
                .Add("VAR_D_OBSERVACION_SP", MySqlDbType.VarChar, 100).Value = be.VAR_D_OBSERVACION_SP
                .Add("VAR_D_FEC_PROGRAMACION", MySqlDbType.VarChar, 100).Value = be.VAR_D_FEC_PROGRAMACION
                .Add("VAR_D_FECHA_REGISTRO", MySqlDbType.VarChar, 100).Value = be.VAR_D_FECHA_REGISTRO
                .Add("VAR_D_PUNTO_VENTA", MySqlDbType.VarChar, 100).Value = be.VAR_D_PUNTO_VENTA
                .Add("VAR_D_ID_SOLICITUD_PORTA", MySqlDbType.VarChar, 100).Value = be.VAR_D_ID_SOLICITUD_PORTA
                .Add("VAR_D_OBSERVACION", MySqlDbType.VarChar, 1000).Value = be.VAR_D_OBSERVACION
                .Add("VAR_D_SUSTENTO", MySqlDbType.VarChar, 100).Value = be.VAR_D_SUSTENTO
            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_REGISTRAR_BASE_SCRIPTING_OUTBOUND_MIGRACIONES(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_CARGAR_SCRIPTING_CLARO_MIGRACIONES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
                '.Add("VAR_ID", MySqlDbType.Int32).Value = be.VAR_ID
                .Add("VAR_RAZON_SOCIAL", MySqlDbType.VarChar, 200).Value = be.VAR_RAZON_SOCIAL
                .Add("VAR_RUC", MySqlDbType.VarChar, 100).Value = be.VAR_RUC
                .Add("VAR_MSISDN", MySqlDbType.VarChar, 100).Value = be.VAR_MSISDN
                .Add("VAR_PLAN_TARIFARIO", MySqlDbType.VarChar, 100).Value = be.VAR_PLAN_TARIFARIO
                .Add("VAR_TIPO_CLIENTE", MySqlDbType.VarChar, 100).Value = be.VAR_TIPO_CLIENTE
                .Add("VAR_CICLO", MySqlDbType.VarChar, 100).Value = be.VAR_CICLO
                .Add("VAR_FECHA_ITERACCION", MySqlDbType.VarChar, 100).Value = be.VAR_FECHA_ITERACCION
                .Add("VAR_CLAROPUNTOS", MySqlDbType.VarChar, 100).Value = be.VAR_CLAROPUNTOS
                .Add("VAR_IMR", MySqlDbType.VarChar, 100).Value = be.VAR_IMR
                .Add("VAR_DISTRITO", MySqlDbType.VarChar, 100).Value = be.VAR_DISTRITO
                .Add("VAR_PROVINCIA", MySqlDbType.VarChar, 100).Value = be.VAR_PROVINCIA
                .Add("VAR_DEPARTAMENTO", MySqlDbType.VarChar, 100).Value = be.VAR_DEPARTAMENTO
                .Add("VAR_DIRECCION", MySqlDbType.VarChar, 200).Value = be.VAR_DIRECCION
                .Add("VAR_SEGMENTO", MySqlDbType.VarChar, 100).Value = be.VAR_SEGMENTO
                .Add("VAR_TELF_REF", MySqlDbType.VarChar, 200).Value = be.VAR_TELF_REF
                .Add("VAR_TELEF_PREP1", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_PREP1
                .Add("VAR_TELEF_PREP2", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_PREP2
                .Add("VAR_TELEF_PREP3", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_PREP3
                .Add("VAR_TELEF_PREP4", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_PREP4
                .Add("VAR_TELEF_PREP5", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_PREP5
                .Add("VAR_TELEF_POST1", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_POST1
                .Add("VAR_TELEF_POST2", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_POST2
                .Add("VAR_TELEF_POST3", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_POST3
                .Add("VAR_TELEF_POST4", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_POST4
                .Add("VAR_TELEF_POST5", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_POST5
                .Add("VAR_PLAZO_ACUERDO", MySqlDbType.VarChar, 200).Value = be.VAR_PLAZO_ACUERDO
                .Add("VAR_NOMBRE_BASE", MySqlDbType.VarChar, 200).Value = be.VAR_NOMBRE_BASE

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_REGISTRAR_BASE_SCRIPTING_OUT_RECUPERO_ELECTRONICO(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_CARGAR_SCRIPTING_CLARO_RECUPERO_ELECTRONICO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
                .Add("VAR_CUENTA", MySqlDbType.VarChar, 300).Value = be.VAR_CUENTA
                .Add("VAR_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_CLIENTE
                .Add("VAR_CONTACTO", MySqlDbType.VarChar, 300).Value = be.VAR_CONTACTO
                .Add("VAR_DEPARTAMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_DEPARTAMENTO
                .Add("VAR_PROVINCIA", MySqlDbType.VarChar, 300).Value = be.VAR_PROVINCIA
                .Add("VAR_DISTRITO", MySqlDbType.VarChar, 300).Value = be.VAR_DISTRITO
                .Add("VAR_TEL1", MySqlDbType.VarChar, 300).Value = be.VAR_TEL1
                .Add("VAR_TEL2", MySqlDbType.VarChar, 300).Value = be.VAR_TEL2
                .Add("VAR_TELCELULAR", MySqlDbType.VarChar, 300).Value = be.VAR_TELCELULAR
                .Add("VAR_PLAN", MySqlDbType.VarChar, 300).Value = be.VAR_PLAN
                .Add("VAR_DNI", MySqlDbType.VarChar, 300).Value = be.VAR_DNI
                .Add("VAR_EMAIL_ERRADO", MySqlDbType.VarChar, 300).Value = be.VAR_EMAIL_ERRADO
                .Add("VAR_CICLO_FACT", MySqlDbType.VarChar, 300).Value = be.VAR_CICLO_FACT
                .Add("VAR_ID_CARGA", MySqlDbType.VarChar, 300).Value = be.VAR_ID_CARGA
                .Add("VAR_NOMBRE_BASE", MySqlDbType.VarChar, 300).Value = be.VAR_NOMBRE_BASE
                .Add("VAR_FECHA_INICIO", MySqlDbType.VarChar, 300).Value = be.VAR_FECHA_INICIO
                .Add("VAR_FECHA_FIN", MySqlDbType.VarChar, 300).Value = be.VAR_FECHA_FIN

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_REPORTE_TIEMPO_PARADAS_X_FECHA(ByVal inicio As String, ByVal fin As String) As DataTable
        Dim dt As New DataTable("tabla")
        Try
            Dim da As New MySqlDataAdapter("BD_DNINOWEB.SP_REPORTE_TIEMPO_PARADAS_X_FECHA", cnxMySql)
            da.SelectCommand.CommandType = CommandType.StoredProcedure
            With da.SelectCommand.Parameters
                .Add("VAR_INI", MySqlDbType.VarChar, 10).Value = inicio
                .Add("VAR_FIN", MySqlDbType.VarChar, 10).Value = fin
                .Add("VAR_USU", MySqlDbType.VarChar, 10).Value = "43083287"
            End With
            da.SelectCommand.CommandTimeout = 6000
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_REPORTE_TIEMPO_PARADAS_X_FECHA : " & ex.Message)
        End Try
        Return dt
    End Function

    Public Function SP_REGISTRAR_BASE_SCRIPTING_OUT_AFILIACION_CE(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_CARGAR_SCRIPTING_CLARO_AFILIACION_CE", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
                .Add("VAR_CUENTA", MySqlDbType.VarChar, 300).Value = be.VAR_CUENTA
                .Add("VAR_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_CLIENTE
                .Add("VAR_CONTACTO", MySqlDbType.VarChar, 300).Value = be.VAR_CONTACTO
                .Add("VAR_DEPARTAMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_DEPARTAMENTO
                .Add("VAR_PROVINCIA", MySqlDbType.VarChar, 300).Value = be.VAR_PROVINCIA
                .Add("VAR_DISTRITO", MySqlDbType.VarChar, 300).Value = be.VAR_DISTRITO
                .Add("VAR_TEL1", MySqlDbType.VarChar, 300).Value = be.VAR_TEL1
                .Add("VAR_TEL2", MySqlDbType.VarChar, 300).Value = be.VAR_TEL2
                .Add("VAR_TELCELULAR", MySqlDbType.VarChar, 300).Value = be.VAR_TELCELULAR
                .Add("VAR_PLAN", MySqlDbType.VarChar, 300).Value = be.VAR_PLAN
                .Add("VAR_DNI", MySqlDbType.VarChar, 300).Value = be.VAR_DNI
                .Add("VAR_CICLO_FACT", MySqlDbType.VarChar, 300).Value = be.VAR_CICLO_FACT
                .Add("VAR_ID_CARGA", MySqlDbType.VarChar, 300).Value = be.VAR_ID_CARGA
                .Add("VAR_NOMBRE_BASE", MySqlDbType.VarChar, 300).Value = be.VAR_NOMBRE_BASE
                .Add("VAR_FECHA_INICIO", MySqlDbType.VarChar, 300).Value = be.VAR_FECHA_INICIO
                .Add("VAR_FECHA_FIN", MySqlDbType.VarChar, 300).Value = be.VAR_FECHA_FIN

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_REGISTRAR_BASE_SCRIPTING_OUTBOUND_CANCELACIONES(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_CARGAR_SCRIPTING_CLARO_CANCELACIONES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
                .Add("VAR_RUC_DNI", MySqlDbType.VarChar, 200).Value = be.VAR_RUC_DNI
                .Add("VAR_TELEFONO", MySqlDbType.VarChar, 200).Value = be.VAR_TELEFONO
                .Add("VAR_PLAN", MySqlDbType.VarChar, 200).Value = be.VAR_PLAN
                .Add("VAR_TIPO_CLIENTE", MySqlDbType.VarChar, 200).Value = be.VAR_TIPO_CLIENTE
                .Add("VAR_FECHAEXP_CREDDEB", MySqlDbType.VarChar, 200).Value = be.VAR_FECHAEXP_CREDDEB
                .Add("VAR_MOTIVO_CANCELACION", MySqlDbType.VarChar, 200).Value = be.VAR_MOTIVO_CANCELACION
                .Add("VAR_CICLO", MySqlDbType.VarChar, 100).Value = be.VAR_CICLO
                .Add("VAR_SEGMENTO", MySqlDbType.VarChar, 200).Value = be.VAR_SEGMENTO
                .Add("VAR_TELEF_PREP1", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_PREP1
                .Add("VAR_TELEF_PREP2", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_PREP2
                .Add("VAR_TELEF_PREP3", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_PREP3
                .Add("VAR_TELEF_PREP4", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_PREP4
                .Add("VAR_TELEF_PREP5", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_PREP5
                .Add("VAR_TELEF_POST1", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_POST1
                .Add("VAR_TELEF_POST2", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_POST2
                .Add("VAR_TELEF_POST3", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_POST3
                .Add("VAR_TELEF_POST4", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_POST4
                .Add("VAR_TELEF_POST5", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_POST5
                .Add("VAR_ACCION", MySqlDbType.VarChar, 200).Value = be.VAR_ACCION
                .Add("VAR_SERVICIO", MySqlDbType.VarChar, 200).Value = be.VAR_SERVICIO
                .Add("VAR_TELEF_REFERENCIA", MySqlDbType.VarChar, 200).Value = be.VAR_TELEF_REFERENCIA
                .Add("VAR_NOMBRE_BASE", MySqlDbType.VarChar, 200).Value = be.VAR_NOMBRE_BASE

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_REGISTRAR_BASE_SCRIPTING_OUTBOUND_ENCUESTAS(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_CARGAR_SCRIPTING_CLARO_ENCUESTAS", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
                .Add("VAR_TELEFONO", MySqlDbType.VarChar, 200).Value = be.VAR_TELEFONO
                .Add("VAR_CLIENTE", MySqlDbType.VarChar, 200).Value = be.VAR_CLIENTE
                .Add("VAR_CAMPANIA", MySqlDbType.VarChar, 200).Value = be.VAR_CAMPANIA
                .Add("VAR_DESCRIPCION", MySqlDbType.VarChar, 200).Value = be.VAR_DESCRIPCION
                .Add("VAR_VOZ_MODEM", MySqlDbType.VarChar, 200).Value = be.VAR_VOZ_MODEM
            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_INSERT_OUTBOUNDLOG_TV_RECUPERO(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            If Not IsNumeric(be.VAR_IDLOG) Then be.VAR_IDLOG = 0
            If Not IsDate(be.VAR_RDATE) Then be.VAR_RDATE = Nothing
            If Not IsNumeric(be.VAR_SERVICEID) Then be.VAR_SERVICEID = 0
            If Not IsNumeric(be.VAR_LOADID) Then be.VAR_LOADID = 0
            If Not IsNumeric(be.VAR_SOURCEID) Then be.VAR_SOURCEID = 0
            If Not IsNumeric(be.VAR_LOGIN) Then be.VAR_LOGIN = 0
            If Not IsNumeric(be.VAR_QCODE) Then be.VAR_QCODE = 0
            If IsNothing(be.VAR_FINAL) Then be.VAR_FINAL = ""

            Dim cmd As New MySqlCommand("SP_INSERT_OUTBOUNDLOG_TV_RECUPERO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_IDLOG", MySqlDbType.Int64).Value = be.VAR_IDLOG
                .Add("VAR_RDATE", MySqlDbType.DateTime).Value = be.VAR_RDATE
                .Add("VAR_SERVICEID", MySqlDbType.Int64).Value = be.VAR_SERVICEID
                .Add("VAR_LOADID", MySqlDbType.Int64).Value = be.VAR_LOADID
                .Add("VAR_SOURCEID", MySqlDbType.Int64).Value = be.VAR_SOURCEID
                .Add("VAR_LOGIN", MySqlDbType.Int64).Value = be.VAR_LOGIN
                .Add("VAR_QCODE", MySqlDbType.Int64).Value = be.VAR_QCODE
                .Add("VAR_FINAL", MySqlDbType.VarChar, 300).Value = be.VAR_FINAL
            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_OUTBOUNDLOG_TV_RECUPERO(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_OUTBOUNDLOG_TV_RECUPERO", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_OUTBOUNDLOG_TV_RECUPERO " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_PORT() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_PORT", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_PORT " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_RECUPERO_MULTIPLICA(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_RECUPERO_MULTIPLICA", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_RECUPERO_MULTIPLICA " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_RECUPERO_LLAMANOMAS(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_RECUPERO_LLAMANOMAS", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_RECUPERO_LLAMANOMAS " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_RECUPERO_RECARGAS(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_RECUPERO_RECARGAS", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_RECUPERO_RECARGAS " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_ENCUESTA_POSTPAGO(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_ENCUESTA_POSTPAGO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CLARO_ENCUESTA_POSTPAGO " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_GESTION_INFORMATIVA(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_GESTION_INFORMATIVA", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CLARO_GESTION_INFORMATIVA " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_TV_RECUPERO_ALL(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_TV_RECUPERO_ALL", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_TV_RECUPERO_ALL " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_TVSAT_RESUMEN_LLAM_TOTAL(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_TVSAT_RESUMEN_LLAM_TOTAL", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_TVSAT_RESUMEN_LLAM_TOTAL " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_TV_RECUPERO_1(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_TV_RECUPERO_1", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_TV_RECUPERO_1 " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_3PLAY(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_3PLAY", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CLARO_3PLAY " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_GESTION_PREV(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_GESTION_PREV", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_LISTAR_CLARO_GESTION_PREV " & ex.Message)
            dt = dtError("SP_LISTAR_CLARO_GESTION_PREV : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_ONTOP_PREVENTIVA(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_ONTOP_PREVENTIVA", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_LISTAR_CLARO_GESTION_PREV " & ex.Message)
            dt = dtError("SP_LISTAR_CLARO_ONTOP_PREVENTIVA : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_UNIQUE__3PLAY(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_UNIQUE_3PLAY", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_UNIQUE_3PLAY " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_UNIQUE_3PLAY_DTH(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_UNIQUE_3PLAY_DTH", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_UNIQUE_3PLAY_DTH " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_HISTORIAL_RECUPERA_FISICA(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_HISTORIAL_RECUPERA_FISICA", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CLARO_3PLAY " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_3PLAY_DTH(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_3PLAY_DTH", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CLARO_3PLAY_DTH " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_RECUPERO_TFI_POST(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_RECUPERO_TFI_POST", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CLARO_RECUPERO_TFI_POST " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_ENCUESTA_PREP(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_ENCUESTA_PREP", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CLARO_ENCUESTA_PREP " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_RECUPERO_ELECTRONICO(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_RECUPERO_ELECTRONICO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CLARO_RECUPERO_ELECTRONICO " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_AFILIACION_RCE(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_AFILIACION_RCE", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CLARO_AFILIACION_RCE " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_DEBITO_AUTO(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_DEBITO_AUTO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_DEBITO_AUTO " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_TV_RECUPERO_2(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_TV_RECUPERO_2", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_TV_RECUPERO_2 " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_TV_RECUPERO_3(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_TV_RECUPERO_3", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_TV_RECUPERO_3 " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CANCELACIONES() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CANCELACIONES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CANCELACIONES " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_MIGRACIONES() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_MIGRACIONES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_MIGRACIONES " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_TV_RETENCIONES() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_TV_RETENCIONES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_TV_RETENCIONES " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_RENOVACIONES(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_RENOVACIONES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_INI", MySqlDbType.VarChar, 10).Value = be.inicio
                .Add("VAR_FIN", MySqlDbType.VarChar, 10).Value = be.fin
            End With

            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_RENOVACIONES " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_RENOVACIONES_SUMARIZADO() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_RENOVACIONES_SUMARIZADO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_RENOVACIONES_SUMARIZADO " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_RENOVACIONES_GENERAL() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_RENOVACIONES_GENERAL", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_LISTAR_RENOVACIONES_GENERAL " & ex.Message)
            dt = dtError("SP_LISTAR_RENOVACIONES_GENERAL : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_DTH(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_DTH", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_DTH " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt

    End Function

    Public Function SP_HISTORIAL_CLARO_PORT(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_PORT", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_PORT " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function


    Public Function SP_HISTORIAL_CLARO_RECUPERO_LLAMANOMAS(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_RECUPERO_LLAMANOMAS", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_RECUPERO_LLAMANOMAS " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_RECUPERO_MULTIPLICA(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_RECUPERO_MULTIPLICA", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_RECUPERO_MULTIPLICA " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_RECUPERO_RECARGAS(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_RECUPERO_RECARGAS", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_RECUPERO_RECARGAS " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_ENCUESTA(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_ENCUESTA", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_ENCUESTA " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_ONTOP_PREVENTIVA(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_ONTOP_PREVENTIVA", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
                .Add("@SERVICIO", SqlDbType.VarChar, 10).Value = be.servicio
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_ONTOP_PREVENTIVA " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_GESTION_INFORMATIVA(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_GESTION_INFORMATIVA", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_GESTION_INFORMATIVA " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_TV_RECUPERO_ALL(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_TV_RECUPERO_ALL", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_TV_RECUPERO_ALL " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_CLARO_TV_RECUPERO_BLOQUEO(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_CLARO_TV_RECUPERO_BLOQUEO", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_CLARO_TV_RECUPERO_BLOQUEO " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_TVSAT_RESUMEN_LLAMADAS_TOTALES(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.TVSAT_RESUMEN_LLAMADAS_TOTAL", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@DIAINICIAL", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@DIAFINAL", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_TVSAT_RESUMEN_LLAMADAS_TOTALES " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_TV_RECUPERO_1(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_TV_RECUPERO_1", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_TV_RECUPERO_1 " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_SEG_CLI_TOP(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_SEG_CLI_TOP", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_SEG_CLI_TOP " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_3PLAY(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_3PLAY", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_HISTORIAL_CLARO_3PLAY " & ex.Message)
            dt = dtError("SP_HISTORIAL_CLARO_3PLAY : " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_3PLAY_HFC_PORTADOS(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_3PLAY_HFC_PORTADOS", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_HISTORIAL_CLARO_3PLAY " & ex.Message)
            dt = dtError("SP_HISTORIAL_CLARO_3PLAY_HFC_PORTADOS : " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_3PLAY_HFC_GUIA_INTERACTIVA(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_3PLAY_HFC_GUIA_INTERACTIVA", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_HISTORIAL_CLARO_3PLAY " & ex.Message)
            dt = dtError("SP_HISTORIAL_CLARO_3PLAY_HFC_GUIA_INTERACTIVA : " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function
  Public Function SP_HISTORIAL_CLARO_193(ByVal ini As String, ByVal fin As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_HISTORIAL_CLARO_193", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .AddWithValue("VAR_INI", ini)
                .AddWithValue("VAR_FIN", fin)
            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_HISTORIAL_CLARO_193 : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_HISTORIAL_CLARO_193_SQL(ByVal ini As String, ByVal fin As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("presence.SP_HISTORIAL_CLARO_193", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = ini
                .Add("@FIN", SqlDbType.VarChar, 10).Value = fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_HISTORIAL_CLARO_GESTION_PREV : " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_HISTORIAL_CLARO_GESTION_PREV(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_GESTION_PREV", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_HISTORIAL_CLARO_GESTION_PREV " & ex.Message)
            dt = dtError("SP_HISTORIAL_CLARO_GESTION_PREV : " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_RECUPERO_FISICA(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_RECUPERO_FISICA", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_RECUPERO_FISICA " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_3PLAY_DTH(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_3PLAY_DTH", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_3PLAY_DTH " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_OLDSITTING(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("presence.SP_HISTORIAL_CLARO_OLDSITTING", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_3PLAY_DTH " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_REPORTE_DETALLE_RESPUESTA(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("BD_CLARO_OUTBOUND.calidad_venta_2015.SP_REPORTE_DETALLE_RESPUESTA", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_REPORTE_DETALLE_RESPUESTA " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_LISTAR_CLARO_OLDSITTING(ByVal IDS As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_OLDSITTING", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CLARO_OLDSITTING " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_RECUPERO_TFI_POST(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_RECUPERO_TFI_POST", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_RECUPERO_TFI_POST " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_ENCUESTA_PREP(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_ENCUESTA_PREP", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_ENCUESTA_PREP " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_RECUPERO_ELECTRONICO(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_RECUPERO_ELECTRONICO", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_RECUPERO_ELECTRONICO " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_AFILIACION_CE(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_AFILIACION_CE", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_AFILIACION_CE " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_DEBITO_AUTO(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_DEBITO_AUTO", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_DEBITO_AUTO " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_TV_RECUPERO_2(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_TV_RECUPERO_2", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_TV_RECUPERO_2 " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_TV_RECUPERO_3(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_TV_RECUPERO_3", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_TV_RECUPERO_3 " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_CANCELACIONES(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_CANCELACIONES", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
                .Add("@TIPO", SqlDbType.Int).Value = be.tipo
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_CANCELACIONES " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_CONSULTA_CLARO_CANCELACIONES_X_TELEFONO(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_CONSULTA_CLARO_CANCELACIONES_X_TELEFONO", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
                .Add("@TELEFONO", SqlDbType.VarChar, 20).Value = be.telefono
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_CONSULTA_CLARO_CANCELACIONES_X_TELEFONO " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_CONSULTA_CLARO_MIGRACIONES_X_TELEFONO(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_CONSULTA_CLARO_MIGRACIONES_X_TELEFONO", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
                .Add("@TELEFONO", SqlDbType.VarChar, 20).Value = be.telefono
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_CONSULTA_CLARO_MIGRACIONES_X_TELEFONO " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_MIGRACIONES(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_MIGRACIONES", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
                .Add("@TIPO", SqlDbType.Int).Value = be.tipo

            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_MIGRACIONES " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_RENOVACIONES_SUMARIZADO(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_RENOVACIONES_SUMARIZADO", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_HISTORIAL_CLARO_TV_RETENCIONES " & ex.Message)
            dt = dtError("SP_HISTORIAL_CLARO_RENOVACIONES_SUMARIZADO : " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_TV_RETENCIONES(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_TV_RETENCIONES", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_TV_RETENCIONES " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_RENOVACIONES(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_RENOVACIONES", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_HISTORIAL_CLARO_RENOVACIONES " & ex.Message)
            dt = dtError("SP_HISTORIAL_CLARO_RENOVACIONES : " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_RENOVACIONES_SUMARIZADO(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_LISTAR_RENOVACIONES_SUMARIZADO", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_RENOVACIONES_SUMARIZADO " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_RENOVACIONES_GENERAL(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_RENOVACIONES_GENERAL", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_RENOVACIONES_GENERAL " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_RENOVACIONES_GENERAL_VENTAS(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_RENOVACIONES_GENERAL_VENTAS", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_HISTORIAL_CLARO_RENOVACIONES_GENERAL_VENTAS " & ex.Message)
            dt = dtError("SP_HISTORIAL_CLARO_RENOVACIONES_GENERAL_VENTAS : " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_DTH(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_DTH", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_HISTORIAL_CLARO_DTH " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_DET_HISTORIAL_CLARO_DTH(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_DET_HISTORIAL_CLARO_DTH", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_DET_HISTORIAL_CLARO_DTH" & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_REPORTE_OUT_ENCUESTA(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("BD_CLARO_OUTBOUND.dbo.SP_REPORTE_OUT_ENCUESTA", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_REPORTE_OUT_ENCUESTA" & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_CLIENTE_TOP(ByVal be As BE_CLARO) As DataTable

        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_CLIENTE_TOP_v2", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            'cmd.CommandTimeout = 600000
            cmd.Parameters.AddWithValue("VAR_INI", be.inicio)
            cmd.Parameters.AddWithValue("VAR_FIN", be.fin)
            '.Add("VAR_INI", SqlDbType.VarChar, 10).Value = be.inicio
            '.Add("VAR_FIN", SqlDbType.VarChar, 10).Value = be.fin

            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_LISTAR_CLARO_CLIENTE_TOP_v2 : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_GARANTIA_SERVICIO(ByVal be As BE_CLARO) As DataTable

        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_GARANTIA_SERVICIO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("VAR_INI", be.inicio)
            cmd.Parameters.AddWithValue("VAR_FIN", be.fin)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_LISTAR_CLARO_GARANTIA_SERVICIO : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_VALIDACIONES(ByVal be As BE_CLARO) As DataTable

        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_VALIDACIONES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("VAR_INI", be.inicio)
            cmd.Parameters.AddWithValue("VAR_FIN", be.fin)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_LISTAR_CLARO_VALIDACIONES : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Function dtError(ByVal msError As String) As DataTable
        Dim dt As New DataTable("tabla")
        dt.Columns.Add("ERROR", System.Type.GetType("System.String"))
        Dim c As DataRow = dt.NewRow
        c.Item(0) = msError
        dt.Rows.InsertAt(c, 0)
        Return dt
    End Function

    Public Function SP_GRABAR_RECUPERO_EMAIL_GRABACIONES(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_GRABAR_RECUPERO_EMAIL_GRABACIONES", cnxMySql)
            With cmd
                .Parameters.AddWithValue("VAR_F_CARGA", be.VAR_FECHA_CARGA)
                .Parameters.AddWithValue("VAR_CICLO", be.VAR_CICLO)
                .Parameters.AddWithValue("VAR_CUENTA", be.VAR_CUENTA)
                .Parameters.AddWithValue("VAR_BASE", be.VAR_NOMBRE_BASE)
                .Parameters.AddWithValue("VAR_EMAIL", be.VAR_EMAIL)
                .Parameters.AddWithValue("VAR_TELCELULAR", be.VAR_TELCELULAR)
                .Parameters.AddWithValue("VAR_ESTADO", be.VAR_D_ESTADO_SP)
                .Parameters.AddWithValue("VAR_ID_DE_LA_LLAMADA", be.VAR_ID_LLAMADA)
            End With

            cmd.CommandType = CommandType.StoredProcedure
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            MsgBox("SP_GRABAR_RECUPERO_EMAIL_GRABACIONES : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_RECUPERO_EMAIL_FECHA_REG() As DataTable

        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_RECUPERO_EMAIL_FECHA_REG", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure

            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_RECUPERO_EMAIL_FECHA_REG : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_RECUPERO_EMAIL_REG(ByVal fecha As String) As DataTable

        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_RECUPERO_EMAIL_REG", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("VAR_FECHA", fecha)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_RECUPERO_EMAIL_FECHA_REG : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function MAXIMO_ID_RECUPERO_FISICA() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID)'ID' FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_RECUPERO_FISICA", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_REGISTRAR_BASE_SCRIPTING_OUT_RECUPERO_FISICO(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_CARGAR_SCRIPTING_CLARO_RECUPERO_FISICO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
                .Add("VAR_D_CUSTID", MySqlDbType.VarChar, 300).Value = be.VAR_CUENTA
                .Add("VAR_D_CICLO", MySqlDbType.VarChar, 300).Value = be.VAR_CICLO
                .Add("VAR_D_RAZ_SOCIAL", MySqlDbType.VarChar, 300).Value = be.VAR_RAZON_SOCIAL
                .Add("VAR_D_NOMBRE", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRE_CLIENTE
                .Add("VAR_D_DNI", MySqlDbType.VarChar, 300).Value = be.VAR_DNI
                .Add("VAR_D_DIRECCION", MySqlDbType.VarChar, 300).Value = be.VAR_DIRECCION
                .Add("VAR_D_REFERENCIA", MySqlDbType.VarChar, 300).Value = be.VAR_REFERENCIA
                .Add("VAR_D_DEPARTAMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_DEPARTAMENTO
                .Add("VAR_D_PROVINCIA", MySqlDbType.VarChar, 300).Value = be.VAR_PROVINCIA
                .Add("VAR_D_DISTRITO", MySqlDbType.VarChar, 300).Value = be.VAR_DISTRITO
                .Add("VAR_D_MOVIL_CLARO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TELEF_MOVIL
                .Add("VAR_D_REFERENCIA_1", MySqlDbType.VarChar, 300).Value = be.VAR_REFERENCIA_1
                .Add("VAR_D_REFERENCIA_2", MySqlDbType.VarChar, 300).Value = be.VAR_REFERENCIA_2
                .Add("VAR_D_PLAN", MySqlDbType.VarChar, 300).Value = be.VAR_PLAN

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function MAXIMO_ID_3PLAY() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID, MAX(ID_CARGA) AS CARGA FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_3PLAY;", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function MAXIMO_ID_3PLAY_DTH() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID, MAX(ID_CARGA) AS CARGA FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_3PLAY_DTH;", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("MAXIMO_ID_3PLAY_DTH :" & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_REGISTRAR_BASE_SCRIPTING_OUTBOUND_3PLAY(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_CARGAR_SCRIPTING_OUTBOUND_3PLAYPRUEBA", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters

                .Add("VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
                .Add("VAR_D_SEMANA", MySqlDbType.VarChar, 300).Value = be.VAR_SEMANA
                .Add("VAR_D_COD_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_CLIENTE
                .Add("VAR_D_NOMBRE_CLI", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRE_CLIENTE
                .Add("VAR_D_SOLUCION", MySqlDbType.VarChar, 300).Value = be.VAR_D_SOLICITUD
                .Add("VAR_D_DIRECCION", MySqlDbType.VarChar, 300).Value = be.VAR_DIRECCION
                .Add("VAR_D_DISTRITO", MySqlDbType.VarChar, 300).Value = be.VAR_DISTRITO
                .Add("VAR_D_TELEFONO_01", MySqlDbType.VarChar, 100).Value = be.VAR_TELEF_PREP1
                .Add("VAR_D_TELEFONO_02", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP2
                .Add("VAR_D_TELEFONO_03", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP3
                .Add("VAR_D_TELEFONO_04", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP4
                .Add("VAR_D_TELEFONO_05", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP5
                .Add("VAR_D_PROYECTO", MySqlDbType.VarChar, 300).Value = be.VAR_PROYECTO
                .Add("VAR_D_NRO_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_DOCUMENTO
                .Add("VAR_D_SERVICIO", MySqlDbType.VarChar, 300).Value = be.VAR_SERVICEID
                .Add("VAR_D_REFERENCIA", MySqlDbType.VarChar, 300).Value = be.VAR_REFERENCIA
                .Add("VAR_D_DEPARTAMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_DEPARTAMENTO
                .Add("VAR_D_FEC_INSTALACION", MySqlDbType.VarChar, 300).Value = be.VAR_FECH_INSTALACION
                .Add("VAR_D_PROVINCIA", MySqlDbType.VarChar, 300).Value = be.VAR_PROVINCIA

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_REGISTRAR_BASE_SCRIPTING_OUTBOUND_3PLAY_DTH(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_CARGAR_SCRIPTING_OUTBOUND_3PLAY_DTHPRUEBA", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters

                .Add("VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
                .Add("VAR_D_SEMANA", MySqlDbType.VarChar, 300).Value = be.VAR_SEMANA
                .Add("VAR_D_COD_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_CLIENTE
                .Add("VAR_D_NOMBRE_CLI", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRE_CLIENTE
                .Add("VAR_D_NRO_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_DOCUMENTO
                .Add("VAR_D_FEC_NAC", MySqlDbType.VarChar, 300).Value = be.VAR_FEC_NAC
                .Add("VAR_D_DIRECCION", MySqlDbType.VarChar, 300).Value = be.VAR_DIRECCION
                .Add("VAR_D_DISTRITO", MySqlDbType.VarChar, 100).Value = be.VAR_DISTRITO
                .Add("VAR_D_PROVINCIA", MySqlDbType.VarChar, 300).Value = be.VAR_PROVINCIA
                .Add("VAR_D_DEPARTAMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_DEPARTAMENTO
                .Add("VAR_D_TELEFONO_01", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP1
                .Add("VAR_D_TELEFONO_02", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP2
                .Add("VAR_D_TELEFONO_03", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP3
                .Add("VAR_D_TELEFONO_04", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP4
                .Add("VAR_D_TELEFONO_05", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP5
                .Add("VAR_D_CAMPANIA", MySqlDbType.VarChar, 300).Value = be.VAR_COMPANIA
                .Add("VAR_D_CF_TOTAL", MySqlDbType.VarChar, 300).Value = be.VAR_CF_TOTAL
                .Add("VAR_D_NRO_SOT", MySqlDbType.VarChar, 300).Value = be.VAR_NRO_SOT
                .Add("VAR_D_FEC_ACTIVACION", MySqlDbType.VarChar, 300).Value = be.VAR_FEC_ACT
                .Add("VAR_D_MATERIAL_DES", MySqlDbType.VarChar, 300).Value = be.VAR_MAT_DES
                .Add("VAR_D_PLAN_TARIFARIO", MySqlDbType.VarChar, 300).Value = be.VAR_PLAN_TARIFARIO

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function MAXIMO_ID_RECUPERO_TFI_POST() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_RECUPERO_TFI_POST", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function MAXIMO_ID_GESTION_PREVENTIVA() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_GESTION_PREVENTIVA", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dtError("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function MAXIMO_ID_SCRIPTING_OUTBOUND_SEG_CLIENTES_TOP() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_SEG_CLIENTES_TOP", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dtError("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function MAXIMO_ID_GESTION_INFORMATIVA() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_GESTION_INFORMATIVA_NUM_PAGO", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dtError("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function SCRIPTING_OUTBOUND_DEBITO_AUTOMATICO() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM SCRIPTING_OUTBOUND_DEBITO_AUTOMATICO", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dtError("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_REGISTRAR_SCRIPTING_OUTBOUND_DEBITO_AUTOMATICO(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_REGISTRAR_SCRIPTING_OUTBOUND_DEBITO_AUTOMATICO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters

                .Add("VAR_ID", MySqlDbType.VarChar, 10).Value = be.VAR_ID
                .Add("VAR_D_CELULAR", MySqlDbType.VarChar, 300).Value = be.VAR_D_CELULAR
                .Add("VAR_D_NOMBRES", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRES
                .Add("VAR_D_SERVICIO_CLARO", MySqlDbType.VarChar, 300).Value = be.VAR_D_SERVICIO_CLARO
                .Add("VAR_D_ENTIDAD_BANCARIA", MySqlDbType.VarChar, 300).Value = be.VAR_D_ENTIDAD_BANCARIA
                .Add("VAR_D_TIPO_TARJETA", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_TARJETA
                .Add("VAR_D_MONTO_TOPE_MAX", MySqlDbType.VarChar, 300).Value = be.VAR_D_MONTO_TOPE_MAX
                .Add("VAR_D_FEC_RECHAZO", MySqlDbType.VarChar, 300).Value = be.VAR_D_FEC_RECHAZO
                .Add("VAR_D_MOTIVO_RECHAZO", MySqlDbType.VarChar, 300).Value = be.VAR_D_MOTIVO_RECHAZO
                .Add("VAR_D_MONTO_RECHAZADO", MySqlDbType.VarChar, 300).Value = be.VAR_D_MONTO_RECHAZADO
                .Add("VAR_D_NRO_CUENTA", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_CUENTA
                .Add("VAR_D_EMAIL", MySqlDbType.VarChar, 300).Value = be.VAR_D_EMAIL
                .Add("VAR_D_DEUDA_1", MySqlDbType.VarChar, 300).Value = be.VAR_D_DEUDA_1
                .Add("VAR_D_FECHA_1", MySqlDbType.VarChar, 300).Value = be.VAR_D_FECHA_1
                .Add("VAR_D_TIPO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO
                .Add("VAR_D_NRO_CASO", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_CASO
                .Add("VAR_D_RESULTADO_CASO", MySqlDbType.VarChar, 300).Value = be.VAR_D_RESULTADO_CASO
                .Add("VAR_D_DETALLE", MySqlDbType.VarChar, 300).Value = be.VAR_D_DETALLE
                .Add("VAR_D_MONTO_TOTAL_FAC", MySqlDbType.VarChar, 300).Value = be.VAR_D_MONTO_TOTAL_FAC
                .Add("VAR_D_MONTO_DEBITADO", MySqlDbType.VarChar, 300).Value = be.VAR_D_MONTO_DEBITADO
                .Add("VAR_D_FEC_VENCIMIENTO_FAC", MySqlDbType.VarChar, 300).Value = be.VAR_D_FEC_VENCIMIENTO_FAC
                .Add("VAR_D_DNI", MySqlDbType.VarChar, 300).Value = be.VAR_D_DNI
                .Add("VAR_D_NRO_TELEF1", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_TELEF1
                .Add("VAR_D_NRO_TELEF2", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_TELEF2
                .Add("VAR_D_NRO_TELEF3", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_TELEF3
                .Add("VAR_D_NRO_TELEF4", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_TELEF4

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function MAXIMO_ID_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1() As DataTable
        Dim dt As New DataTable
        Try
            'Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_GESTION_PREVENTIVA", cnxMySql)
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dtError("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function MAXIMO_ID_SCRIPTING_OUTBOUND_CLARO_OLDSITTING() As DataTable
        Dim dt As New DataTable
        Try
            'Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_GESTION_PREVENTIVA", cnxMySql)
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CLARO_OLDSITTING", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dtError("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function MAXIMO_ID_SCRIPTING_OUTBOUND_SEG_CLIENTES_ESPECIALES() As DataTable
        Dim dt As New DataTable
        Try
            'Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_GESTION_PREVENTIVA", cnxMySql)
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_SEG_CLIENTES_ESPECIALES", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dtError("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function MAXIMO_ID_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2() As DataTable
        Dim dt As New DataTable
        Try
            'Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_GESTION_PREVENTIVA", cnxMySql)
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dtError("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function MAXIMO_ID_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO3() As DataTable
        Dim dt As New DataTable
        Try
            'Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_GESTION_PREVENTIVA", cnxMySql)
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CLAROTV_RECUPERO3", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dtError("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_REGISTRAR_BASE_SCRIPTING_OUTBOUND_RECUPERO_TFI_POST(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_CARGAR_SCRIPTING_OUTBOUND_RECUPERO_TFI_POST", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters

                .Add("VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
                .Add("VAR_D_TELEFONO", MySqlDbType.VarChar, 300).Value = be.VAR_CONTACTO
                .Add("VAR_D_TELEFONO_2", MySqlDbType.VarChar, 300).Value = be.VAR_TELEFONO
                .Add("VAR_D_CUSTCODE", MySqlDbType.VarChar, 300).Value = be.VAR_CUSTCODE
                .Add("VAR_D_NOMBRES", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRE_CLIENTE
                .Add("VAR_D_CUSTOMER_ID", MySqlDbType.VarChar, 300).Value = be.VAR_CUSTOMER_ID
                .Add("VAR_D_FACTURA", MySqlDbType.VarChar, 300).Value = be.VAR_FACTURA
                .Add("VAR_D_FEC_EMISION", MySqlDbType.VarChar, 100).Value = be.VAR_D_FECHA_ENVIO
                .Add("VAR_D_FEC_VENCIMIENTO", MySqlDbType.VarChar, 300).Value = be.VAR_FECHA_VENC
                .Add("VAR_D_MONTO_ORIGINAL", MySqlDbType.VarChar, 300).Value = be.VAR_MONTO_ORG
                .Add("VAR_D_MONTO_PENDIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_MONTO_PEND

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_REGISTRAR_BASE_SCRIPTING_GESTION_PREVENTIVA(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_REGISTRAR_BASE_SCRIPTING_GESTION_PREVENTIVA2", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters

                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = be.VAR_ID
                .Add("VAR_D_SERVICE", MySqlDbType.VarChar, 300).Value = be.VAR_SERVICIO
                .Add("VAR_D_CUST_ACCOUNT", MySqlDbType.VarChar, 300).Value = be.VAR_CUSTCODE
                .Add("VAR_D_ACCOUNT_DESC", MySqlDbType.VarChar, 300).Value = be.VAR_ACCOUNT_DESC
                .Add("VAR_D_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_CLIENTE
                .Add("VAR_D_TIPO_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_DOC
                .Add("VAR_D_NRO_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_DOCUMENTO
                .Add("VAR_D_FEC_ACTIVACION", MySqlDbType.VarChar, 300).Value = be.VAR_FEC_ACT
                .Add("VAR_D_DEPARTAMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_DEPARTAMENTO
                .Add("VAR_D_PROVINCIA", MySqlDbType.VarChar, 300).Value = be.VAR_PROVINCIA
                .Add("VAR_D_DISTRITO", MySqlDbType.VarChar, 300).Value = be.VAR_DISTRITO
                .Add("VAR_D_DIRECCION", MySqlDbType.VarChar, 300).Value = be.VAR_DIRECCION
                .Add("VAR_D_CICLO_FACTURACION", MySqlDbType.VarChar, 300).Value = be.VAR_CICLO_FACT
                .Add("VAR_D_TIPO_DOC_EMITIDO", MySqlDbType.VarChar, 300).Value = be.VAR_TIPO_DOC_EMITIDO
                .Add("VAR_D_RECIBO", MySqlDbType.VarChar, 300).Value = be.VAR_RECIBO
                .Add("VAR_D_FEC_EMISION", MySqlDbType.VarChar, 300).Value = be.VAR_FEC_EMISION
                .Add("VAR_D_FEC_VENCIMIENTO", MySqlDbType.VarChar, 300).Value = be.VAR_FECHA_VENC
                .Add("VAR_D_MONTO_ORIGINAL", MySqlDbType.VarChar, 300).Value = be.VAR_MONTO_ORG
                .Add("VAR_D_MONTO_RECIBO", MySqlDbType.VarChar, 300).Value = be.VAR_MONTO_REC
                .Add("VAR_D_FEC_ASIGNACION", MySqlDbType.VarChar, 300).Value = be.VAR_FECHA_ASIG
                .Add("VAR_D_PLAN_TARIFARIO", MySqlDbType.VarChar, 300).Value = be.VAR_PLAN_TARIFARIO
                .Add("VAR_D_NRO_SERVICIO", MySqlDbType.VarChar, 300).Value = be.VAR_SERVICEID
                .Add("VAR_D_EST_ACT_SERVICIO", MySqlDbType.VarChar, 300).Value = be.VAR_EST_ACT_SERVICIO
                .Add("VAR_D_INDICADOR", MySqlDbType.VarChar, 300).Value = be.VAR_INDICADOR
                .Add("VAR_D_VARIOS_RECIBOS", MySqlDbType.VarChar, 300).Value = be.VAR_VARIOS_RECIBOS
                .Add("VAR_D_NRO_PAGO", MySqlDbType.VarChar, 300).Value = be.VAR_NRO_PAGO
                .Add("VAR_D_TEL_01", MySqlDbType.VarChar, 100).Value = be.VAR_TEL1
                .Add("VAR_D_TEL_02", MySqlDbType.VarChar, 100).Value = be.VAR_TEL2
                .Add("VAR_D_TEL_03", MySqlDbType.VarChar, 100).Value = be.VAR_TEL3
                .Add("VAR_D_TEL_04", MySqlDbType.VarChar, 100).Value = be.VAR_TEL4
                .Add("VAR_D_TEL_05", MySqlDbType.VarChar, 100).Value = be.VAR_TEL5
                .Add("VAR_D_TEL_06", MySqlDbType.VarChar, 100).Value = be.VAR_TEL6

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_REGISTRAR_BASE_SEG_CLIENTES_TOP(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_REGISTRAR_BASE_SEG_CLIENTES_TOP", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters

                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = be.VAR_ID
                .Add("VAR_D_SERVICIO", MySqlDbType.VarChar, 300).Value = be.VAR_D_SERVICIO
                .Add("VAR_D_CODIGO", MySqlDbType.VarChar, 300).Value = be.VAR_D_CODIGO
                .Add("VAR_D_CODIGO_BSCS", MySqlDbType.VarChar, 300).Value = be.VAR_D_CODIGO_BSCS
                .Add("VAR_D_RAZ_SOCIAL", MySqlDbType.VarChar, 300).Value = be.VAR_D_RAZ_SOCIAL
                .Add("VAR_D_RUC", MySqlDbType.VarChar, 300).Value = be.VAR_D_RUC
                .Add("VAR_D_AGENTE_DNINO", MySqlDbType.VarChar, 300).Value = be.VAR_D_AGENTE_DNINO
                .Add("VAR_D_TIPO_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_CLIENTE
                .Add("VAR_D_FORMA_PAGO", MySqlDbType.VarChar, 300).Value = be.VAR_D_FORMA_PAGO
                .Add("VAR_D_ESTADO_CUENTA", MySqlDbType.VarChar, 300).Value = be.VAR_D_ESTADO_CUENTA
                .Add("VAR_D_FEC_ACTIVACION", MySqlDbType.VarChar, 300).Value = be.VAR_D_FEC_ACTIVACION
                .Add("VAR_D_DEPARTAMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DEPARTAMENTO
                .Add("VAR_D_PROVINCIA", MySqlDbType.VarChar, 300).Value = be.VAR_D_PROVINCIA
                .Add("VAR_D_DISTRITO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DISTRITO
                .Add("VAR_D_CANT_LINEAS_A", MySqlDbType.VarChar, 300).Value = be.VAR_D_CANT_LINEAS_A
                .Add("VAR_D_SEGMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_SEGMENTO
                .Add("VAR_D_TIPO_SEGMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_SEGMENTO
                .Add("VAR_D_CICLO", MySqlDbType.VarChar, 300).Value = be.VAR_D_CICLO
                .Add("VAR_D_NOMBRE_CICLO", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRE_CICLO
                .Add("VAR_D_SERVICIO_PRESTADO", MySqlDbType.VarChar, 300).Value = be.VAR_D_SERVICIO_PRESTADO
                .Add("VAR_D_ESTADO_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_ESTADO_DOCUMENTO
                .Add("VAR_D_TIPO_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_DOCUMENTO
                .Add("VAR_D_DEBITO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DEBITO
                .Add("VAR_D_NRO_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_DOCUMENTO
                .Add("VAR_D_FEC_EMISION", MySqlDbType.VarChar, 300).Value = be.VAR_D_FEC_EMISION
                .Add("VAR_D_FEC_VCTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_FEC_VCTO
                .Add("VAR_D_ANT_CUENTA", MySqlDbType.VarChar, 300).Value = be.VAR_D_ANT_CUENTA
                .Add("VAR_D_ANT_DOC", MySqlDbType.VarChar, 300).Value = be.VAR_D_ANT_DOC
                .Add("VAR_D_TRAMO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TRAMO
                .Add("VAR_D_MONEDA", MySqlDbType.VarChar, 300).Value = be.VAR_D_MONEDA
                .Add("VAR_D_IMPORTE_FACTURADO", MySqlDbType.VarChar, 300).Value = be.VAR_D_IMPORTE_FACTURADO
                .Add("VAR_D_IMPORTE_PENDIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_IMPORTE_PENDIENTE
                .Add("VAR_D_IMPORTE_PENDIENTE_SOLES", MySqlDbType.VarChar, 300).Value = be.VAR_D_IMPORTE_PENDIENTE_SOLES
                .Add("VAR_D_MONTO_DISPUTA", MySqlDbType.VarChar, 300).Value = be.VAR_D_MONTO_DISPUTA
                .Add("VAR_D_GESTOR_COBRANZAS", MySqlDbType.VarChar, 300).Value = be.VAR_D_GESTOR_COBRANZAS
                .Add("VAR_D_CARTERA", MySqlDbType.VarChar, 300).Value = be.VAR_D_CARTERA
                .Add("VAR_D_CANAL", MySqlDbType.VarChar, 300).Value = be.VAR_D_CANAL
                .Add("VAR_D_DISTRIBUIDOR", MySqlDbType.VarChar, 300).Value = be.VAR_D_DISTRIBUIDOR
                .Add("VAR_D_CONSULTOR", MySqlDbType.VarChar, 300).Value = be.VAR_D_CONSULTOR
                .Add("VAR_D_SUBCANAL", MySqlDbType.VarChar, 300).Value = be.VAR_D_SUBCANAL
                .Add("VAR_D_GERENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_GERENTE
                .Add("VAR_D_SUBDIRECCION", MySqlDbType.VarChar, 300).Value = be.VAR_D_SUBDIRECCION
                .Add("VAR_D_JEFE", MySqlDbType.VarChar, 300).Value = be.VAR_D_JEFE
                .Add("VAR_D_ASESOR", MySqlDbType.VarChar, 300).Value = be.VAR_D_ASESOR
                .Add("VAR_D_SUPERVISOR", MySqlDbType.VarChar, 300).Value = be.VAR_D_SUPERVISOR
                .Add("VAR_D_SECTOR", MySqlDbType.VarChar, 300).Value = be.VAR_D_SECTOR
                .Add("VAR_D_REGION", MySqlDbType.VarChar, 300).Value = be.VAR_D_REGION
                .Add("VAR_D_ACCOUNT_MANAGER", MySqlDbType.VarChar, 300).Value = be.VAR_D_ACCOUNT_MANAGER
                .Add("VAR_D_GRUPO_ECON", MySqlDbType.VarChar, 300).Value = be.VAR_D_GRUPO_ECON
                .Add("VAR_D_CLIENTES_100", MySqlDbType.VarChar, 300).Value = be.VAR_D_CLIENTES_100
                .Add("VAR_D_CARTAS_JUNIO", MySqlDbType.VarChar, 300).Value = be.VAR_D_CARTAS_JUNIO
                .Add("VAR_TELEFONO", MySqlDbType.VarChar, 300).Value = be.VAR_TEL1

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_REGISTRAR_BASE_GESTION_INFORMATIVA(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_REGISTRAR_BASE_GESTION_INFORMATIVA", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters

                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = be.VAR_ID
                .Add("VAR_D_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_CLIENTE
                .Add("VAR_D_TIPO_DOC", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_DOC
                .Add("VAR_D_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DOCUMENTO
                .Add("VAR_D_FEC_MIGRACION", MySqlDbType.VarChar, 300).Value = be.VAR_D_FEC_MIGRACION
                .Add("VAR_D_COD_ANTERIOR", MySqlDbType.VarChar, 300).Value = be.VAR_D_COD_ANTERIOR
                .Add("VAR_D_NUEVO_CODIGO", MySqlDbType.VarChar, 300).Value = be.VAR_D_NUEVO_CODIGO
                .Add("VAR_D_CODIGO_PAGO", MySqlDbType.VarChar, 300).Value = be.VAR_D_CODIGO_PAGO
                .Add("VAR_D_MONTO_PENDIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_MONTO_PENDIENTE
                .Add("VAR_D_DEUDA_COD_ANTIGUO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DEUDA_COD_ANTIGUO
                .Add("VAR_D_NOMBRE_PLAN", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRE_PLAN
                .Add("VAR_D_TELEF_SERVICIO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TELEF_SERVICIO
                .Add("VAR_D_DEBITO_AUTOMATICO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DEBITO_AUTOMATICO
                .Add("VAR_D_SERV_TELEF_PLAN_ORIGEN", MySqlDbType.VarChar, 300).Value = be.VAR_D_SERV_TELEF_PLAN_ORIGEN
                .Add("VAR_D_FEC_ASIGNACION", MySqlDbType.VarChar, 300).Value = be.VAR_D_FEC_ASIGNACION
                .Add("VAR_D_NOMBRE_CARTERA", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRE_CARTERA
                .Add("VAR_D_CC", MySqlDbType.VarChar, 300).Value = be.VAR_D_CC
                .Add("VAR_D_NRO_SERVICIO", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_SERVICIO
                .Add("VAR_D_TIPO_DE_SERVICIO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_DE_SERVICIO
                .Add("VAR_D_ESCENARIO_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_ESCENARIO_CLIENTE
                .Add("VAR_D_REF_MONTO_TOTAL", MySqlDbType.VarChar, 300).Value = be.VAR_D_REF_MONTO_TOTAL
                .Add("VAR_D_TENER_EN_CUENTA", MySqlDbType.VarChar, 300).Value = be.VAR_D_TENER_EN_CUENTA
                .Add("VAR_D_VARIOS_RECIBOS", MySqlDbType.VarChar, 300).Value = be.VAR_D_VARIOS_RECIBOS
                .Add("VAR_D_TELEF_REFERENCIA_1", MySqlDbType.VarChar, 300).Value = be.VAR_D_TELEF_REFERENCIA_1
                .Add("VAR_D_TELEF_REFERENCIA_2", MySqlDbType.VarChar, 300).Value = be.VAR_D_TELEF_REFERENCIA_2


            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = be.VAR_ID
                .Add("VAR_D_NOM_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRE_CLIENTE
                .Add("VAR_D_CODINSSRV", MySqlDbType.VarChar, 300).Value = be.VAR_D_CODINSSRV
                .Add("VAR_D_COD_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_COD_CLIENTE
                .Add("VAR_D_ESTADOPAGO", MySqlDbType.VarChar, 300).Value = be.VAR_D_ESTADOPAGO
                .Add("VAR_D_PLAN_GENERAL", MySqlDbType.VarChar, 300).Value = be.VAR_D_PLAN_GENERAL
                .Add("VAR_D_DEPARTAMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DEPARTAMENTO
                .Add("VAR_D_COD_RECARGA", MySqlDbType.VarChar, 300).Value = be.VAR_D_COD_RECARGA
                .Add("VAR_D_TELEFONO_01", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP1
                .Add("VAR_D_TELEFONO_02", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP2
                .Add("VAR_D_TELEFONO_03", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP3
                .Add("VAR_D_TELEFONO_04", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP4
                .Add("VAR_D_TELEFONO_05", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP5
                .Add("VAR_D_TELEFONO_06", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP6
                .Add("VAR_D_TELEFONO_07", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP7
                .Add("VAR_D_TELEFONO_08", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP8
                .Add("VAR_D_TELEFONO_09", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP9
                .Add("VAR_D_TELEFONO_10", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP10

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function
    Public Function SP_SCRIPTING_OUTBOUND_CLARO_OLDSITTING(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_SCRIPTING_OUTBOUND_CLARO_OLDSITTING", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = be.VAR_ID
                .Add("VAR_D_CODSOLOT", MySqlDbType.VarChar, 300).Value = be.VAR_D_CODSOLOT
                .Add("VAR_D_TIPO_TRABAJO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_TRABAJO
                .Add("VAR_D_DSCTIPSRV", MySqlDbType.VarChar, 300).Value = be.VAR_D_DSCTIPSRV
                .Add("VAR_D_ESTADO_SOT", MySqlDbType.VarChar, 300).Value = be.VAR_D_ESTADO_SOT
                .Add("VAR_D_FECUSU", MySqlDbType.VarChar, 300).Value = be.VAR_D_FECUSU
                .Add("VAR_D_MES", MySqlDbType.VarChar, 300).Value = be.VAR_D_MES
                .Add("VAR_D_ANIO", MySqlDbType.VarChar, 300).Value = be.VAR_D_ANIO
                .Add("VAR_D_CODCLI", MySqlDbType.VarChar, 300).Value = be.VAR_D_CODCLI
                .Add("VAR_D_NOMCLI", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMCLI
                .Add("VAR_D_COD_PAGO", MySqlDbType.VarChar, 300).Value = be.VAR_D_COD_PAGO
                .Add("VAR_D_COD_ID", MySqlDbType.VarChar, 300).Value = be.VAR_D_COD_ID
                .Add("VAR_D_MAIL_1", MySqlDbType.VarChar, 300).Value = be.VAR_D_MAIL_1
                .Add("VAR_D_MAIL_2", MySqlDbType.VarChar, 300).Value = be.VAR_D_MAIL_2
                .Add("VAR_D_TELEFONO_1", MySqlDbType.VarChar, 300).Value = be.VAR_D_TELEFONO_1
                .Add("VAR_D_TELEFONO_2", MySqlDbType.VarChar, 300).Value = be.VAR_D_TELEFONO_2
                .Add("VAR_D_TELEFONO_3", MySqlDbType.VarChar, 300).Value = be.VAR_D_TELEFONO_3
                .Add("VAR_D_TELEFONO_4", MySqlDbType.VarChar, 300).Value = be.VAR_D_TELEFONO_4
                .Add("VAR_D_TELEFONO_5", MySqlDbType.VarChar, 300).Value = be.VAR_D_TELEFONO_5

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_SCRIPTING_OUTBOUND_SEG_CLIENTES_ESPECIALES(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_SCRIPTING_OUTBOUND_SEG_CLIENTES_ESPECIALES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = be.VAR_ID
                .Add("VAR_D_RUC", MySqlDbType.VarChar, 300).Value = be.VAR_D_RUC
                .Add("VAR_D_CODIGO_SAP", MySqlDbType.VarChar, 300).Value = be.VAR_D_CODIGO_SAP
                .Add("VAR_D_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_CLIENTE
                '.Add("VAR_D_EMAIL", MySqlDbType.VarChar, 300).Value = be.VAR_D_EMAIL
                '.Add("VAR_D_CONTACTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_CONTACTO
                '.Add("VAR_D_OBSERVCIONES_1", MySqlDbType.VarChar, 3000).Value = be.VAR_D_OBSERVCIONES_1
                '.Add("VAR_D_OBSERVCIONES_2", MySqlDbType.VarChar, 3000).Value = be.VAR_D_OBSERVCIONES_2
                '.Add("VAR_D_OBSERVCIONES_3", MySqlDbType.VarChar, 3000).Value = be.VAR_D_OBSERVCIONES_3
                '.Add("VAR_D_OBSERVCIONES_4", MySqlDbType.VarChar, 3000).Value = be.VAR_D_OBSERVCIONES_4
                .Add("VAR_D_TELEFONO_1", MySqlDbType.VarChar, 300).Value = be.VAR_D_TELEFONO_1
                
            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function
    Public Function SP_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = be.VAR_ID
                .Add("VAR_D_NOM_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRE_CLIENTE
                .Add("VAR_D_CODINSSRV", MySqlDbType.VarChar, 300).Value = be.VAR_D_CODINSSRV
                .Add("VAR_D_COD_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_COD_CLIENTE
                .Add("VAR_D_ESTADOPAGO", MySqlDbType.VarChar, 300).Value = be.VAR_D_ESTADOPAGO
                .Add("VAR_D_PLAN_GENERAL", MySqlDbType.VarChar, 300).Value = be.VAR_D_PLAN_GENERAL
                .Add("VAR_D_DEPARTAMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DEPARTAMENTO
                .Add("VAR_D_COD_RECARGA", MySqlDbType.VarChar, 300).Value = be.VAR_D_COD_RECARGA
                .Add("VAR_D_TELEFONO_01", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP1
                .Add("VAR_D_TELEFONO_02", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP2
                .Add("VAR_D_TELEFONO_03", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP3
                .Add("VAR_D_TELEFONO_04", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP4
                .Add("VAR_D_TELEFONO_05", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP5
                .Add("VAR_D_TELEFONO_06", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP6
                .Add("VAR_D_TELEFONO_07", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP7
                .Add("VAR_D_TELEFONO_08", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP8
                .Add("VAR_D_TELEFONO_09", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP9
                .Add("VAR_D_TELEFONO_10", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP10

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function
    Public Function SP_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO3(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO3", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = be.VAR_ID
                .Add("VAR_D_NOM_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRE_CLIENTE
                .Add("VAR_D_CODINSSRV", MySqlDbType.VarChar, 300).Value = be.VAR_D_CODINSSRV
                .Add("VAR_D_COD_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_COD_CLIENTE
                .Add("VAR_D_ESTADOPAGO", MySqlDbType.VarChar, 300).Value = be.VAR_D_ESTADOPAGO
                .Add("VAR_D_PLAN_GENERAL", MySqlDbType.VarChar, 300).Value = be.VAR_D_PLAN_GENERAL
                .Add("VAR_D_DEPARTAMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DEPARTAMENTO
                .Add("VAR_D_COD_RECARGA", MySqlDbType.VarChar, 300).Value = be.VAR_D_COD_RECARGA
                .Add("VAR_D_TELEFONO_01", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP1
                .Add("VAR_D_TELEFONO_02", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP2
                .Add("VAR_D_TELEFONO_03", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP3
                .Add("VAR_D_TELEFONO_04", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP4
                .Add("VAR_D_TELEFONO_05", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP5
                .Add("VAR_D_TELEFONO_06", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP6
                .Add("VAR_D_TELEFONO_07", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP7
                .Add("VAR_D_TELEFONO_08", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP8
                .Add("VAR_D_TELEFONO_09", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP9
                .Add("VAR_D_TELEFONO_10", MySqlDbType.VarChar, 300).Value = be.VAR_TELEF_PREP10

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_REGISTRAR_BASE_DET_SCRIPTING_GESTION_PREVENTIVA(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_REGISTRAR_DET_SCRIPTING_GESTION_PREVENTIVA", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters

                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = be.VAR_ID
                .Add("VAR_RECIBO", MySqlDbType.VarChar, 300).Value = be.VAR_RECIBO
                .Add("VAR_NRO_SERVICIO", MySqlDbType.VarChar, 300).Value = be.VAR_SERVICEID
                .Add("VAR_FEC_EMISION", MySqlDbType.VarChar, 300).Value = be.VAR_FEC_EMISION
                .Add("VAR_FEC_VENCIMIENTO", MySqlDbType.VarChar, 300).Value = be.VAR_FECHA_VENC
                .Add("VAR_MONTO_ORIGINAL", MySqlDbType.VarChar, 300).Value = be.VAR_MONTO_ORG
                .Add("VAR_MONTO_RECIBO", MySqlDbType.VarChar, 300).Value = be.VAR_MONTO_REC

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function
    Public Function SP_REGISTRAR_SCRIPTING_DETALLE_FACTURA_SEG_CLI_TOP(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_REGISTRAR_SCRIPTING_DETALLE_FACTURA_SEG_CLI_TOP", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters

                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = be.VAR_ID
                .Add("VAR_D_SERVICIO", MySqlDbType.VarChar, 300).Value = be.VAR_D_SERVICIO
                .Add("VAR_D_CODIGO", MySqlDbType.VarChar, 300).Value = be.VAR_D_CODIGO
                .Add("VAR_D_CODIGO_BSCS", MySqlDbType.VarChar, 300).Value = be.VAR_D_CODIGO_BSCS
                .Add("VAR_D_RAZ_SOCIAL", MySqlDbType.VarChar, 300).Value = be.VAR_D_RAZ_SOCIAL
                .Add("VAR_D_RUC", MySqlDbType.VarChar, 300).Value = be.VAR_D_RUC
                .Add("VAR_D_TIPO_CLIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_CLIENTE
                .Add("VAR_D_FORMA_PAGO", MySqlDbType.VarChar, 300).Value = be.VAR_D_FORMA_PAGO
                .Add("VAR_D_ESTADO_CUENTA", MySqlDbType.VarChar, 300).Value = be.VAR_D_ESTADO_CUENTA
                .Add("VAR_D_FEC_ACTIVACION", MySqlDbType.VarChar, 300).Value = be.VAR_D_FEC_ACTIVACION
                .Add("VAR_D_DEPARTAMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DEPARTAMENTO
                .Add("VAR_D_PROVINCIA", MySqlDbType.VarChar, 300).Value = be.VAR_D_PROVINCIA
                .Add("VAR_D_DISTRITO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DISTRITO
                .Add("VAR_D_CANT_LINEAS_A", MySqlDbType.VarChar, 300).Value = be.VAR_D_CANT_LINEAS_A
                .Add("VAR_D_SEGMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_SEGMENTO
                .Add("VAR_D_TIPO_SEGMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_SEGMENTO
                .Add("VAR_D_CICLO", MySqlDbType.VarChar, 300).Value = be.VAR_D_CICLO
                .Add("VAR_D_NOMBRE_CICLO", MySqlDbType.VarChar, 300).Value = be.VAR_D_NOMBRE_CICLO
                .Add("VAR_D_SERVICIO_PRESTADO", MySqlDbType.VarChar, 300).Value = be.VAR_D_SERVICIO_PRESTADO
                .Add("VAR_D_ESTADO_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_ESTADO_DOCUMENTO
                .Add("VAR_D_TIPO_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_DOCUMENTO
                .Add("VAR_D_DEBITO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DEBITO
                .Add("VAR_D_NRO_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_NRO_DOCUMENTO
                .Add("VAR_D_FEC_EMISION", MySqlDbType.VarChar, 300).Value = be.VAR_D_FEC_EMISION
                .Add("VAR_D_FEC_VCTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_FEC_VCTO
                .Add("VAR_D_ANT_CUENTA", MySqlDbType.VarChar, 300).Value = be.VAR_D_ANT_CUENTA
                .Add("VAR_D_ANT_DOC", MySqlDbType.VarChar, 300).Value = be.VAR_D_ANT_DOC
                .Add("VAR_D_TRAMO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TRAMO
                .Add("VAR_D_MONEDA", MySqlDbType.VarChar, 300).Value = be.VAR_D_MONEDA
                .Add("VAR_D_IMPORTE_FACTURADO", MySqlDbType.VarChar, 300).Value = be.VAR_D_IMPORTE_FACTURADO
                .Add("VAR_D_IMPORTE_PENDIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_IMPORTE_PENDIENTE
                .Add("VAR_D_IMPORTE_PENDIENTE_SOLES", MySqlDbType.VarChar, 300).Value = be.VAR_D_IMPORTE_PENDIENTE_SOLES
                .Add("VAR_D_MONTO_DISPUTA", MySqlDbType.VarChar, 300).Value = be.VAR_D_MONTO_DISPUTA
                .Add("VAR_D_GESTOR_COBRANZAS", MySqlDbType.VarChar, 300).Value = be.VAR_D_GESTOR_COBRANZAS
                .Add("VAR_D_CARTERA", MySqlDbType.VarChar, 300).Value = be.VAR_D_CARTERA
                .Add("VAR_D_CANAL", MySqlDbType.VarChar, 300).Value = be.VAR_D_CANAL
                .Add("VAR_D_DISTRIBUIDOR", MySqlDbType.VarChar, 300).Value = be.VAR_D_DISTRIBUIDOR
                .Add("VAR_D_CONSULTOR", MySqlDbType.VarChar, 300).Value = be.VAR_D_CONSULTOR
                .Add("VAR_D_SUBCANAL", MySqlDbType.VarChar, 300).Value = be.VAR_D_SUBCANAL
                .Add("VAR_D_GERENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_GERENTE
                .Add("VAR_D_SUBDIRECCION", MySqlDbType.VarChar, 300).Value = be.VAR_D_SUBDIRECCION
                .Add("VAR_D_JEFE", MySqlDbType.VarChar, 300).Value = be.VAR_D_JEFE
                .Add("VAR_D_ASESOR", MySqlDbType.VarChar, 300).Value = be.VAR_D_ASESOR
                .Add("VAR_D_SUPERVISOR", MySqlDbType.VarChar, 300).Value = be.VAR_D_SUPERVISOR
                .Add("VAR_D_SECTOR", MySqlDbType.VarChar, 300).Value = be.VAR_D_SECTOR
                .Add("VAR_D_REGION", MySqlDbType.VarChar, 300).Value = be.VAR_D_REGION
                .Add("VAR_D_ACCOUNT_MANAGER", MySqlDbType.VarChar, 300).Value = be.VAR_D_ACCOUNT_MANAGER
                .Add("VAR_D_GRUPO_ECON", MySqlDbType.VarChar, 300).Value = be.VAR_D_GRUPO_ECON
                .Add("VAR_D_CLIENTES_100", MySqlDbType.VarChar, 300).Value = be.VAR_D_CLIENTES_100
                .Add("VAR_D_CARTAS_JUNIO", MySqlDbType.VarChar, 300).Value = be.VAR_D_CARTAS_JUNIO



            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function
    Public Function SP_REGISTRAR_BASE_DET_SCRIPTING_GESTION_INFORMATIVA(ByVal be As BE_CLARO) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_REGISTRAR_DET_SCRIPTING_GESTION_INFORMATIVA", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters

                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = be.VAR_ID
                .Add("VAR_D_DOC_IDENTIDAD", MySqlDbType.VarChar, 300).Value = be.VAR_D_DOC_IDENTIDAD
                .Add("VAR_D_TIPO", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO
                .Add("VAR_D_CUSTOMER_ID", MySqlDbType.VarChar, 300).Value = be.VAR_D_CUSTOMER_ID
                .Add("VAR_D_CUENTA_LARGA", MySqlDbType.VarChar, 300).Value = be.VAR_D_CUENTA_LARGA
                .Add("VAR_D_DOCUMENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_DETDOCUMENTO
                .Add("VAR_D_TIPO_DOC", MySqlDbType.VarChar, 300).Value = be.VAR_D_TIPO_DOC
                .Add("VAR_D_FECHA_EMISION", MySqlDbType.VarChar, 300).Value = be.VAR_D_FECHA_EMISION
                .Add("VAR_D_FECHA_VENCIMIENTO", MySqlDbType.VarChar, 300).Value = be.VAR_D_FECHA_VENCIMIENTO
                .Add("VAR_D_MONTO_ORIGINAL", MySqlDbType.VarChar, 300).Value = be.VAR_D_MONTO_ORIGINAL
                .Add("VAR_D_MONTO_PENDIENTE", MySqlDbType.VarChar, 300).Value = be.VAR_D_MONTO_PENDIENTE
                .Add("VAR_D_ANTIG_DEUDA", MySqlDbType.VarChar, 300).Value = be.VAR_D_ANTIG_DEUDA

            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = "0" Then ms = "0" Else ms = "1"
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    Public Function SP_LISTAR_IDS_GESTION_PREVENTIVA(ByVal id As String) As DataTable
        Dim dt As New DataTable
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_ID_GEST_PREV", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = id
            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_LISTAR_ID_GEST_PREV :" & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_LISTAR_ID_SEG_CLIENTES_TOP(ByVal id As String) As DataTable
        Dim dt As New DataTable
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_ID_SEG_CLIENTES_TOP", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = id
            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_LISTAR_ID_GEST_PREV :" & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
    Public Function SP_LISTAR_IDS_GESTION_INFORMATIVA(ByVal id As String) As DataTable
        Dim dt As New DataTable
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_ID_GEST_INFORMATIVA", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.VarChar, 6).Value = id
            End With
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_LISTAR_ID_GEST_INFORMATIVA :" & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_CLARO_3PLAY_HFC_BLOQUEO_P1(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("[presence].[SP_CLARO_3PLAY_HFC_BLOQUEO_P1]", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
                .Add("@CARGA", SqlDbType.VarChar, 10).Value = be.VAR_ID_CARGA
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_CLARO_3PLAY_HFC_BLOQUEO_P1 :" & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_CLARO_3PLAY_HFC_BLOQUEO_P2(ByVal be As BE_CLARO) As DataTable

        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_CLARO_3PLAY_HFC_BLOQUEO_P2", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            'cmd.CommandTimeout = 600000
            cmd.Parameters.AddWithValue("VAR_ID_CARGA", be.VAR_ID_CARGA)
            cmd.Parameters.AddWithValue("VAR_ID", be.VAR_IDS)

            'cmd.Parameters.Add("VAR_ID_CARGA", MySqlDbType.VarChar, 10).Value = be.VAR_ID_CARGA
            'cmd.Parameters.Add("VAR_ID", MySqlDbType.VarChar, 999).Value = be.VAR_IDS

            '.Add("VAR_INI", SqlDbType.VarChar, 10).Value = be.inicio
            '.Add("VAR_FIN", SqlDbType.VarChar, 10).Value = be.fin

            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_CLARO_3PLAY_HFC_BLOQUEO_P2 : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_CLARO_3PLAY_HFC_BLOQUEO_P3(ByVal be As BE_CLARO) As DataTable

        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_CLARO_3PLAY_HFC_BLOQUEO_P3", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.Add("VAR_ID", MySqlDbType.Text).Value = IDS
            'cmd.CommandTimeout = 600000
            cmd.Parameters.AddWithValue("VAR_ID_CARGA", be.VAR_ID_CARGA)
            cmd.Parameters.AddWithValue("VAR_COD_CLIE", be.VAR_CODS_CLI)
            '.Add("VAR_INI", SqlDbType.VarChar, 10).Value = be.inicio
            '.Add("VAR_FIN", SqlDbType.VarChar, 10).Value = be.fin

            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_CLARO_3PLAY_HFC_BLOQUEO_P3 : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function


    Public Function SP_CLARO_3PLAY_HFC_BLOQUEO_P4(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("[presence].[SP_CLARO_3PLAY_HFC_BLOQUEO_P4]", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@ID", SqlDbType.VarChar).Value = be.VAR_CODS_CLI
                .Add("@CARGA", SqlDbType.VarChar, 10).Value = be.VAR_ID_CARGA
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_CLARO_3PLAY_HFC_BLOQUEO_P4 :" & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_CLARO_3PLAY_DTH_BLOQUEO_P1(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("[presence].[SP_CLARO_3PLAY_DTH_BLOQUEO_P1]", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
                .Add("@CARGA", SqlDbType.VarChar, 10).Value = be.VAR_ID_CARGA
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_CLARO_3PLAY_DTH_BLOQUEO_P1 :" & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_CLARO_3PLAY_DTH_BLOQUEO_P2(ByVal be As BE_CLARO) As DataTable

        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_CLARO_3PLAY_DTH_BLOQUEO_P2", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("VAR_ID_CARGA", be.VAR_ID_CARGA)
            cmd.Parameters.AddWithValue("VAR_ID", be.VAR_IDS)

            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_CLARO_3PLAY_DTH_BLOQUEO_P2 : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_CLARO_3PLAY_DTH_BLOQUEO_P3(ByVal be As BE_CLARO) As DataTable

        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_CLARO_3PLAY_DTH_BLOQUEO_P3", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("VAR_ID_CARGA", be.VAR_ID_CARGA)
            cmd.Parameters.AddWithValue("VAR_COD_CLIE", be.VAR_CODS_CLI)


            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_CLARO_3PLAY_DTH_BLOQUEO_P3 : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function


    Public Function SP_CLARO_3PLAY_DTH_BLOQUEO_P4(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("[presence].[SP_CLARO_3PLAY_DTH_BLOQUEO_P4]", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@ID", SqlDbType.VarChar).Value = be.VAR_CODS_CLI
                .Add("@CARGA", SqlDbType.VarChar, 10).Value = be.VAR_ID_CARGA
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_CLARO_3PLAY_DTH_BLOQUEO_P4 :" & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_RANK_RECUPERO_OUT(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_REPORTE_RANKING_CLARO_RECUPERO_OUT", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAL_FEC_INI", MySqlDbType.VarChar).Value = be.VAR_FECHA_INICIO
                .Add("VAL_FEC_FIN", MySqlDbType.VarChar, 10).Value = be.VAR_FECHA_FIN
                .Add("VAL_TIPO_SERV", MySqlDbType.Int24).Value = be.VAR_SERVICEID
                .Add("VAL_LOG_CALIDAD", MySqlDbType.Int64).Value = Convert.ToInt64(be.VAR_IDLOG)
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_REPORTE_RANKING_CLARO_RECUPERO_OUT:" & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_RANK_ASIGNACION_PREVENTIVA_OUT(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_RANKING_ASIGNACION_PREVENTIVA_OUT", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAL_FEC_INI", MySqlDbType.VarChar).Value = be.VAR_FECHA_INICIO
                .Add("VAL_FEC_FIN", MySqlDbType.VarChar, 10).Value = be.VAR_FECHA_FIN
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_RANKING_ASIGNACION_PREVENTIVA_OUT:" & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_RANK_DET_RECUPERO_OUT(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_REPORTE_DET_RANKING_CLARO_RECUPERO_OUT", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAL_FEC_INI", MySqlDbType.VarChar).Value = be.VAR_FECHA_INICIO
                .Add("VAL_FEC_FIN", MySqlDbType.VarChar, 10).Value = be.VAR_FECHA_FIN
                .Add("VAL_TIPO_SERV", MySqlDbType.Int24).Value = be.VAR_SERVICEID
                .Add("VAL_LOG_CALIDAD", MySqlDbType.Int64).Value = Convert.ToInt64(be.VAR_IDLOG)
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_REPORTE_DET_RANKING_CLARO_RECUPERO_OUT:" & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function


    Public Function SP_LISTA_CABECERAS(ByVal be As BE_CLARO) As DataTable
        Dim da As New MySqlDataAdapter("SP_LISTA_CABECERAS", cnxMySql2)
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        With da.SelectCommand.Parameters
            .Add("VAR_BD", MySqlDbType.VarChar, 50).Value = be.bd
            .Add("VAR_TABLA", MySqlDbType.VarChar, 50).Value = be.tabla
        End With
        da.SelectCommand.CommandTimeout = 6000
        Dim dt As New DataTable("tabla")
        da.Fill(dt)
        Return dt
    End Function
    Public Function CargarExcel(ByVal SLibro As String) As DataTable

        Dim dtDevolver As New DataTable

        Dim cs As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                           "Data Source=" & SLibro & ";" & _
                           "Extended Properties=""Excel 8.0;HDR=YES"""
        Try
            Dim cnxMySql2 As New OleDbConnection(cs)
            If Not System.IO.File.Exists(SLibro) Then
                WriteLine("No se encontró el Libro: " & SLibro, MsgBoxStyle.Critical, "Ruta inválida")
                Return dtDevolver
                Exit Function
            End If

            Dim dAdapter As New OleDbDataAdapter("Select * From [Hoja1$]", cs)
            Dim dt As New DataTable
            dAdapter.Fill(dt)
            dtDevolver = dt
            'If dt.Rows.Count > 0 Then
            '    Dim nombreHoja As String = "[Hoja" & Now.ToString("yyyyMMddHHmmss") & "]"
            '    cn.Open()
            '    Dim cmd As New OleDbCommand("SELECT '' as CAMPO INTO [Excel 8.0;Database=" & var_ruta & "Book1.xls]." & nombreHoja & "", cn)
            '    cmd.ExecuteNonQuery()
            '    cn.Close()

            'End If
        Catch ex As Exception
            'WriteLine(ex.Message)
        End Try
        Return dtDevolver
    End Function
    Public Function CREAR_CABECERA_A_EXCEL(ByVal nombreHoja As String) As String
        Dim ms As String = ""
        Try
            Dim cs As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                        "Data Source=" & var_ruta & "Book1.xls;" & _
                        "Extended Properties=""Excel 8.0;HDR=YES"""
            Dim cn As New OleDbConnection(cs)
            Dim cmd As New OleDbCommand("SELECT  '' as [CAMPO] INTO [Excel 8.0;Database=" & var_ruta & "Book1.xls]." & nombreHoja & "", cn)
            cn.Open()
            ms = cmd.ExecuteNonQuery
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql2.Close()
        End Try
        Return ms
    End Function
    Public Function INSERTAR_CABECERA_A_EXCEL(ByVal nombreHoja As String, ByVal campoinsertar As String) As String
        Dim ms As String = ""
        Try
            Dim cs As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                        "Data Source=" & var_ruta & "Book1.xls;" & _
                        "Extended Properties=""Excel 8.0;HDR=YES"""
            Dim cn As New OleDbConnection(cs)
            Dim cmd As New OleDbCommand("Insert into " & nombreHoja & " ([CAMPO]) values('" & campoinsertar & "')", cn)
            cn.Open()
            ms = cmd.ExecuteNonQuery()
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql2.Close()
        End Try
        Return ms
    End Function
    Public Function INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1(ByVal ID As String, ByVal VAR_D_CODINSSRV As String, ByVal VAR_D_COD_CLIENTE As String, ByVal VAR_D_DEPARTAMENTO As String, ByVal VAR_D_NOM_CLIENTE As String, ByVal VAR_D_TELEFONO_01 As String, ByVal VAR_D_TELEFONO_02 As String, ByVal VAR_D_TELEFONO_03 As String, ByVal VAR_D_TELEFONO_04 As String, ByVal VAR_D_TELEFONO_05 As String, ByVal VAR_D_COD_RECARGA As String, ByVal VAR_D_PLAN_GENERAL As String, ByVal VAR_D_ESTADOPAGO As String, ByVal VAR_D_TELEFONO_06 As String, ByVal VAR_D_TELEFONO_07 As String, ByVal VAR_D_TELEFONO_08 As String, ByVal VAR_D_TELEFONO_09 As String, ByVal VAR_D_TELEFONO_10 As String) As String
        Dim ms As String = ""
        Dim sql As String
        VAR_D_CODINSSRV = Replace(Replace(Replace(Replace(Replace(VAR_D_CODINSSRV, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_COD_CLIENTE = Replace(Replace(Replace(Replace(Replace(VAR_D_COD_CLIENTE, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_DEPARTAMENTO = Replace(Replace(Replace(Replace(Replace(VAR_D_DEPARTAMENTO, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_NOM_CLIENTE = Replace(Replace(Replace(Replace(Replace(VAR_D_NOM_CLIENTE, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_TELEFONO_01 = Replace(Replace(Replace(Replace(Replace(VAR_D_TELEFONO_01, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_TELEFONO_02 = Replace(Replace(Replace(Replace(Replace(VAR_D_TELEFONO_02, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_TELEFONO_03 = Replace(Replace(Replace(Replace(Replace(VAR_D_TELEFONO_03, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_TELEFONO_04 = Replace(Replace(Replace(Replace(Replace(VAR_D_TELEFONO_04, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_TELEFONO_05 = Replace(Replace(Replace(Replace(Replace(VAR_D_TELEFONO_05, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_COD_RECARGA = Replace(Replace(Replace(Replace(Replace(VAR_D_COD_RECARGA, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_PLAN_GENERAL = Replace(Replace(Replace(Replace(Replace(VAR_D_PLAN_GENERAL, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_ESTADOPAGO = Replace(Replace(Replace(Replace(Replace(VAR_D_ESTADOPAGO, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_TELEFONO_06 = Replace(Replace(Replace(Replace(Replace(VAR_D_TELEFONO_06, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_TELEFONO_07 = Replace(Replace(Replace(Replace(Replace(VAR_D_TELEFONO_07, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_TELEFONO_08 = Replace(Replace(Replace(Replace(Replace(VAR_D_TELEFONO_08, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_TELEFONO_09 = Replace(Replace(Replace(Replace(Replace(VAR_D_TELEFONO_09, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_TELEFONO_10 = Replace(Replace(Replace(Replace(Replace(VAR_D_TELEFONO_10, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")

        Try
            sql = "INSERT INTO BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1(ID,D_CODINSSRV,	D_COD_CLIENTE,	D_DEPARTAMENTO,	D_NOM_CLIENTE,	D_TELEFONO_01,	D_TELEFONO_02,	D_TELEFONO_03,	D_TELEFONO_04,	D_TELEFONO_05,	D_COD_RECARGA,	D_PLAN_GENERAL,	D_ESTADOPAGO,	D_TELEFONO_06,	D_TELEFONO_07,	D_TELEFONO_08,	D_TELEFONO_09,	D_TELEFONO_10) " & _
                                       "VALUES('" & ID & "', '" & VAR_D_CODINSSRV & "','" & VAR_D_COD_CLIENTE & "','" & VAR_D_DEPARTAMENTO & "','" & VAR_D_NOM_CLIENTE & "','" & VAR_D_TELEFONO_01 & "','" & VAR_D_TELEFONO_02 & "','" & VAR_D_TELEFONO_03 & "','" & VAR_D_TELEFONO_04 & "','" & VAR_D_TELEFONO_05 & "','" & VAR_D_COD_RECARGA & "','" & VAR_D_PLAN_GENERAL & "','" & VAR_D_ESTADOPAGO & "','" & VAR_D_TELEFONO_06 & "','" & VAR_D_TELEFONO_07 & "','" & VAR_D_TELEFONO_08 & "','" & VAR_D_TELEFONO_09 & "','" & VAR_D_TELEFONO_10 & "')"

            Dim cmd As New MySqlCommand(sql, cnxMySql2)
            cnxMySql2.Open()
            ms = cmd.ExecuteNonQuery
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql2.Close()
        End Try
        Return ms
    End Function
    Function ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1() As String
        Dim cmd As New MySqlCommand("SELECT MAX(id)+1 FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1", cnxMySql2)
        cnxMySql2.Open()
        Dim ms As String = cmd.ExecuteScalar
        cnxMySql2.Close()
        Return ms
    End Function

    Public Function INSERTAR_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2(ByVal ID As String, ByVal VAR_D_TELEFONO As String, ByVal VAR_D_CODINSSRV As String, ByVal VAR_D_COD_CLIENTE As String, ByVal VAR_D_DEPARTAMENTO As String, ByVal VAR_D_NOM_CLIENTE As String, ByVal VAR_D_COD_RECARGA As String, ByVal VAR_D_PLAN_GENERAL As String, ByVal VAR_D_ESTADOPAGO As String) As String
        Dim ms As String = ""
        Dim sql As String
        VAR_D_CODINSSRV = Replace(Replace(Replace(Replace(Replace(VAR_D_CODINSSRV, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_COD_CLIENTE = Replace(Replace(Replace(Replace(Replace(VAR_D_COD_CLIENTE, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_DEPARTAMENTO = Replace(Replace(Replace(Replace(Replace(VAR_D_DEPARTAMENTO, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_NOM_CLIENTE = Replace(Replace(Replace(Replace(Replace(VAR_D_NOM_CLIENTE, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_TELEFONO = Replace(Replace(Replace(Replace(Replace(VAR_D_TELEFONO, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_COD_RECARGA = Replace(Replace(Replace(Replace(Replace(VAR_D_COD_RECARGA, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_PLAN_GENERAL = Replace(Replace(Replace(Replace(Replace(VAR_D_PLAN_GENERAL, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")
        VAR_D_ESTADOPAGO = Replace(Replace(Replace(Replace(Replace(VAR_D_ESTADOPAGO, "Ã‘", "N"), "‘", ""), "'", "."), "’", "."), "Ã", " ")

        Try
            sql = "INSERT INTO BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1(ID,TELEFONO,D_CODINSSRV,	D_COD_CLIENTE,	D_DEPARTAMENTO,	D_NOM_CLIENTE,	D_COD_RECARGA,	D_PLAN_GENERAL,	D_ESTADOPAGO) " & _
                                       "VALUES('" & ID & "', '" & VAR_D_TELEFONO & "', '" & VAR_D_CODINSSRV & "','" & VAR_D_COD_CLIENTE & "','" & VAR_D_DEPARTAMENTO & "','" & VAR_D_NOM_CLIENTE & "','" & VAR_D_COD_RECARGA & "','" & VAR_D_PLAN_GENERAL & "','" & VAR_D_ESTADOPAGO & "')"

            Dim cmd As New MySqlCommand(sql, cnxMySql2)
            cnxMySql2.Open()
            ms = cmd.ExecuteNonQuery
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql2.Close()
        End Try
        Return ms
    End Function
    Function ID_MAXIMO_TABLA_SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2() As String
        Dim cmd As New MySqlCommand("SELECT MAX(id)+1 FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1", cnxMySql2)
        cnxMySql2.Open()
        Dim ms As String = cmd.ExecuteScalar
        cnxMySql2.Close()
        Return ms
    End Function
	
	Public Function SP_LISTAR_CLARO_NEG_CLIE_TOP(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_NEG_CLIE_TOP", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_INI", MySqlDbType.Text).Value = be.inicio
            cmd.Parameters.Add("VAR_FIN", MySqlDbType.Text).Value = be.fin
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTAR_CLARO_NEG_CLIE_TOP " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_CLARO_SEG_CLIE_ESP(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_CLARO_SEG_CLIE_ESP", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_INI", MySqlDbType.Text).Value = be.inicio
            cmd.Parameters.Add("VAR_FIN", MySqlDbType.Text).Value = be.fin
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            dt = dtError("SP_LISTAR_CLARO_SEG_CLIE_ESP " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_TIEMPO_AGENTE_INICIO_FIN_20141105(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_TIEMPO_AGENTE_INICIO_FIN_20141105", cnxMySqlDN)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_INI", MySqlDbType.VarChar).Value = be.inicio
            cmd.Parameters.Add("VAR_FIN", MySqlDbType.VarChar).Value = be.fin
            cmd.Parameters.Add("VAR_USUARIO", MySqlDbType.VarChar).Value = be.VAR_USUARIO
            cmd.CommandTimeout = 600000
            cnxMySqlDN.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_TIEMPO_AGENTE_INICIO_FIN_20141105 " & ex.Message)
        Finally
            cnxMySqlDN.Close()
        End Try
        Return dt
    End Function

    Public Function SP_TIEMPO_AGENTE_INICIO_FIN_OUTBOUND(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_TIEMPO_AGENTE_INICIO_FIN_OUTBOUND", cnxMySqlDN)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("VAR_INI", MySqlDbType.VarChar).Value = be.inicio
            cmd.Parameters.Add("VAR_FIN", MySqlDbType.VarChar).Value = be.fin
            cmd.Parameters.Add("SERVICIO", MySqlDbType.VarChar).Value = be.VAR_SERVICIO
            cmd.CommandTimeout = 600000
            cnxMySqlDN.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_TIEMPO_AGENTE_INICIO_FIN_OUTBOUND " & ex.Message)
        Finally
            cnxMySqlDN.Close()
        End Try
        Return dt
    End Function

    Public Function SP_RENOVACIONES_RANKING(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("presence.SP_RENOVACIONES_RANKING", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_REPORTES_CLARO_INBOUND " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    '**********************************************************************************
    Public Function CLAROTV_RECUPERO1() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID, MAX(ID_CARGA) AS CARGA FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CLAROTV_RECUPERO1;", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function CLAROTV_RECUPERO2() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID, MAX(ID_CARGA) AS CARGA FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CLAROTV_RECUPERO2;", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function CLAROTV_RECUPERO3() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SELECT MAX(ID) AS ID, MAX(ID_CARGA) AS CARGA FROM BD_SCRIPTING_CLARO.SCRIPTING_OUTBOUND_CLAROTV_RECUPERO3;", cnxMySql)
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("MAXIMO_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_MIGRACIONES_GENERAL(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_MIGRACIONES_GENERAL", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_HISTORIAL_CLARO_MIGRACIONES_GENERAL " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_MIGRACIONES_GENERAL() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_MIGRACIONES_GENERAL", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_LISTAR_RENOVACIONES_GENERAL " & ex.Message)
            dt = dtError("SP_LISTAR_RENOVACIONES_GENERAL : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_MIGRACIONES_GENERAL_VENTAS(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_MIGRACIONES_GENERAL_VENTAS", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_HISTORIAL_CLARO_RENOVACIONES_GENERAL_VENTAS " & ex.Message)
            dt = dtError("SP_HISTORIAL_CLARO_MIGRACIONES_GENERAL_VENTAS : " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_HISTORIAL_CLARO_MIGRACIONES_CLIENTE(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("PRESENCE.SP_HISTORIAL_CLARO_MIGRACIONES_CLIENTE", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@INI", SqlDbType.VarChar, 10).Value = be.inicio
                .Add("@FIN", SqlDbType.VarChar, 10).Value = be.fin
            End With
            cmd.CommandTimeout = 600000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_HISTORIAL_CLARO_RENOVACIONES " & ex.Message)
            dt = dtError("SP_HISTORIAL_CLARO_MIGRACIONES_CLIENTE : " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_MIGRACIONES_CLIENTE(ByVal be As BE_CLARO_MIGRA) As DataTable
        Dim dt As New DataTable
        Dim MS As String
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_MIGRACIONES_CLIENTE", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_INI", MySqlDbType.VarChar, 10).Value = be.inicio
                .Add("VAR_FIN", MySqlDbType.VarChar, 10).Value = be.fin
            End With

            cnxMySql.Open()
            cmd.CommandTimeout = 600000
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MS = "SP_LISTAR_MIGRACIONES_CLIENTE " & ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_ACTUALIZAR_SCRIPTING_MIGRACIONES_WEB(ByVal be As BE_CLARO_MIGRA) As String
        Dim ms As String = ""
        Try
            Dim cmd As New MySqlCommand("SP_ACTUALIZAR_SCRIPTING_MIGRACIONES_WEB", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters

                .AddWithValue("VAR_ID", be.VAR_ID)
                .AddWithValue("VAR_TXT_PLAN_POSTPAGO_CONTRATADO", be.VAR_TXT_PLAN_POSTPAGO_CONTRATADO)
                .AddWithValue("VAR_TXT_CARGO_FIJO_MENSUAL", be.VAR_TXT_CARGO_FIJO_MENSUAL)
                .AddWithValue("VAR_TXT_CICLO_FACTURACION", be.VAR_TXT_CICLO_FACTURACION)
                .AddWithValue("VAR_TXT_NOMBRES", be.VAR_TXT_NOMBRES)
                .AddWithValue("VAR_TXT_APELLIDOS", be.VAR_TXT_APELLIDOS)
                .AddWithValue("VAR_TXT_NRO_DNI", be.VAR_TXT_NRO_DNI)
                .AddWithValue("VAR_TXT_NRO_MIGRAR", be.VAR_TXT_NRO_MIGRAR)
                .AddWithValue("VAR_DTP_FEC_NAC", be.VAR_DTP_FEC_NAC)
                .AddWithValue("VAR_TXT_LUGAR_NAC", be.VAR_TXT_LUGAR_NAC)
                .AddWithValue("VAR_TXT_DIRECCION", be.VAR_TXT_DIRECCION)
                .AddWithValue("VAR_TXT_DISTRITO", be.VAR_TXT_DISTRITO)
                .AddWithValue("VAR_TXT_TELEFONO_REF", be.VAR_TXT_TELEFONO_REF)
                .AddWithValue("VAR_TXT_DEPARTAMENTO", be.VAR_TXT_DEPARTAMENTO)
                .AddWithValue("VAR_TXT_PROVINCIA", be.VAR_TXT_PROVINCIA)
                .AddWithValue("VAR_TXT_EMAIL", be.VAR_TXT_EMAIL)
                .AddWithValue("VAR_CBO_NIVEL_1", be.VAR_NIVEL_1)
                .AddWithValue("VAR_CBO_NIVEL_2", be.VAR_NIVEL_2)
                .AddWithValue("VAR_CBO_NIVEL_3", be.VAR_NIVEL_3)
                .AddWithValue("VAR_CBO_NIVEL_4", be.VAR_NIVEL_4)
                .AddWithValue("VAR_TXT_RESULTADO_BACKOFFICE", be.VAR_TXT_RESULTADO_BACKOFFICE)
                .AddWithValue("VAR_TXT_RESULTADO_CALIDAD", be.VAR_TXT_RESULTADO_CALIDAD)
                .AddWithValue("VAR_LOGIN_CALIDAD", be.login)
                .AddWithValue("VAR_OBS_BACKOFFICE", be.VAR_OBS_BACKOFFICE)
            End With
            cnxMySql.Open()
            Dim i As String = cmd.ExecuteNonQuery
            If i = 0 Then
                ms = "No se logro actualizar"
            Else
                ms = "Actualizacion correcta"
            End If
        Catch ex As Exception
            ms = ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return ms
    End Function

    'Public Function SP_REPORTE_HISTORIAL_VENTA_130620(ByVal be As BE_CLARO) As DataTable
    '    Dim dt As New DataTable
    '    Try
    '        Dim cmd As New MySqlCommand("SP_REPORTE_HISTORIAL_VENTA_130620", cnxMySql)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        With cmd.Parameters
    '            .Add("@ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
    '        End With
    '        cmd.CommandTimeout = 6000
    '        cnxMySql.Open()
    '        Dim da As New MySqlDataAdapter(cmd)
    '        da.Fill(dt)
    '    Catch ex As Exception
    '        MsgBox("SP_REPORTE_HISTORIAL_VENTA_130620 " & ex.Message)
    '    Finally
    '        cnxMySql.Close()
    '    End Try
    '    Return dt
    'End Function

    Public Function SP_LISTA_VENTAS_X_ID_MIGRACIONES(ByVal be As BE_CLARO_MIGRA) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTA_SCRIPTING_MIGRACIONES_X_ID_MU", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@VAR_ID", MySqlDbType.VarChar, 18).Value = be.VAR_ID
            End With
            cmd.CommandTimeout = 6000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_LISTA_SCRIPTING_RENOVACIONES_X_ID " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTA_UBIGEO_DEPARTAMENTO() As DataTable
        Dim dt As New DataTable
        Try

            Dim cmd As New MySqlCommand("BD_SCRIPTING_RIMAC.SP_LISTA_UBIGEO_DEPARTAMENTO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
            Dim c As DataRow = dt.NewRow
            c.Item(0) = "0"
            c.Item(1) = "Seleccionar"
            dt.Rows.InsertAt(c, 0)
        Catch ex As Exception
            MsgBox("SP_LISTA_UBIGEO_DEPARTAMENTO : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTA_UBIGEO_PROVINCIA(ByVal str_dep As String) As DataTable
        Dim dt As New DataTable
        Try
            If Not IsNumeric(str_dep) Then str_dep = 0
            Dim cmd As New MySqlCommand("BD_SCRIPTING_RIMAC.SP_LISTA_UBIGEO_PROVINCIA", cnxMySql)
            cmd.Parameters.Add("VAR_DEP", MySqlDbType.VarChar).Value = str_dep
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
            Dim c As DataRow = dt.NewRow
            c.Item(0) = "0"
            c.Item(1) = "Seleccionar"
            dt.Rows.InsertAt(c, 0)
        Catch ex As Exception
            MsgBox("SP_LISTA_UBIGEO_PROVINCIA : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTA_UBIGEO_DISTRITO(ByVal str_dep As String, ByVal str_prov As String) As DataTable
        Dim dt As New DataTable
        Try
            If Not IsNumeric(str_dep) Then str_dep = 0
            Dim cmd As New MySqlCommand("BD_SCRIPTING_RIMAC.SP_LISTA_UBIGEO_DISTRITO", cnxMySql)
            cmd.Parameters.Add("VAR_DEP", MySqlDbType.VarChar).Value = str_dep
            cmd.Parameters.Add("VAR_PROV", MySqlDbType.VarChar).Value = str_prov
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
            Dim c As DataRow = dt.NewRow
            c.Item(0) = "0"
            c.Item(1) = "Seleccionar"
            dt.Rows.InsertAt(c, 0)
        Catch ex As Exception
            MsgBox("SP_LISTA_UBIGEO_DISTRITO : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTA_NIVEL_TIPIFICACION(ByVal tipo As String, ByVal be As BE_CLARO_MIGRA) As DataTable

        Dim dt As New DataTable
        Try

            Dim cmd As New MySqlCommand("SP_LISTA_NIVEL_TIPIFICACION", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 600000
            cmd.Parameters.Add("VAR_NIVEL", MySqlDbType.String).Value = tipo
            cmd.Parameters.Add("VAR_NIVEL_1", MySqlDbType.String).Value = be.VAR_NIVEL_1
            cmd.Parameters.Add("VAR_NIVEL_2", MySqlDbType.String).Value = be.VAR_NIVEL_2
            cmd.Parameters.Add("VAR_NIVEL_3", MySqlDbType.String).Value = be.VAR_NIVEL_3
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As DataRow = dt.NewRow
            dr.Item(0) = "0"
            dr.Item(1) = "Seleccionar"
            dt.Rows.InsertAt(dr, 0)
        Catch ex As Exception
            MsgBox("BD_SCRIPTING_CLARO.SP_LISTA_NIVEL_TIPIFICACION : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_ULTIMO_RESULTADO_MIGRACIONES(ByVal be As BE_CLARO_MIGRA) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("SP_ULTIMO_RESULTADO_MIGRACIONES", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@VAR_ID", SqlDbType.Int).Value = be.VAR_ID
            End With
            cmd.CommandTimeout = 60000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_ULTIMO_RESULTADO_MIGRACIONES " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_CODIGOS_PRESENCE(servicio As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("SP_CODIGOS_PRESENCE", cnxSql2)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@SERVICEID", SqlDbType.Int).Value = servicio
            End With
            cmd.CommandTimeout = 60000
            cnxSql2.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As DataRow = dt.NewRow
            dr.Item(0) = "999"
            dr.Item(1) = "Seleccionar"
            dt.Rows.InsertAt(dr, 0)
        Catch ex As Exception
            MsgBox("SP_CODIGOS_PRESENCE" & ex.Message)
        Finally
            cnxSql2.Close()
        End Try
        Return dt
    End Function

    Public Function SP_ULTIMO_RESULTADO_BACKOFFICE_MIGRACIONES(be As BE_CLARO_MIGRA) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_ULTIMO_RESULTADO_BACKOFFICE_MIGRACIONES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.Int32).Value = be.VAR_ID
            End With
            cmd.CommandTimeout = 60000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)

        Catch ex As Exception
            MsgBox("SP_ULTIMO_RESULTADO_MIGRACIONES" & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTA_NIVEL_FINALES(ByVal tipo As String, ByVal be As BE_CLARO) As DataTable

        Dim dt As New DataTable
        Try

            Dim cmd As New MySqlCommand("SP_LISTA_NIVEL_FINALES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 600000
            cmd.Parameters.Add("VAR_NIVEL", MySqlDbType.String).Value = tipo
            cmd.Parameters.Add("VAR_NIVEL_1", MySqlDbType.String).Value = be.VAR_NIVEL_1
            cmd.Parameters.Add("VAR_NIVEL_2", MySqlDbType.String).Value = be.VAR_NIVEL_2
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As DataRow = dt.NewRow
            dr.Item(0) = "0"
            dr.Item(1) = "Seleccionar"
            dt.Rows.InsertAt(dr, 0)
        Catch ex As Exception
            MsgBox("BD_SCRIPTING_CLARO.SP_LISTA_NIVEL_TIPIFICACION : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_ULTIMO_RESULTADO_RENO(ByVal id As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New SqlCommand("SP_ULTIMO_RESULTADO_RENO", cnxSql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("@VAR_ID", SqlDbType.Int).Value = id
            End With
            cmd.CommandTimeout = 60000
            cnxSql.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("SP_ULTIMO_RESULTADO_RENO " & ex.Message)
        Finally
            cnxSql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_ULTIMO_RESULTADO_BACKOFFICE_RENO(ByVal id As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_ULTIMO_RESULTADO_BACKOFFICE_RENO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.Int32).Value = id
            End With
            cmd.CommandTimeout = 60000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)

        Catch ex As Exception
            MsgBox("SP_ULTIMO_RESULTADO_BACKOFFICE_RENO" & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTA_ULTIMO_RESULTADO_RENO(ByVal id As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTA_ULTIMO_RESULTADO_RENO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_ID", MySqlDbType.Int32).Value = id
            End With
            cmd.CommandTimeout = 60000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)

        Catch ex As Exception
            MsgBox("SP_LISTA_ULTIMO_RESULTADO_RENO" & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_REPORTE_CONSOLIDADO_MIGRA_RENO(ByVal be As BE_CLARO_MIGRA) As DataTable
        Dim dt As New DataTable
        Dim MS As String
        Try
            Dim cmd As New MySqlCommand("SP_REPORTE_CONSOLIDADO_MIGRA_RENO", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_INI", MySqlDbType.VarChar, 10).Value = be.inicio
                .Add("VAR_FIN", MySqlDbType.VarChar, 10).Value = be.fin
            End With

            cnxMySql.Open()
            cmd.CommandTimeout = 600000
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MS = "SP_REPORTE_CONSOLIDADO_MIGRA_RENO " & ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_REPORTE_SUMARIZADO_MIGRACIONES(ByVal be As BE_CLARO_MIGRA) As DataTable
        Dim dt As New DataTable
        Dim MS As String
        Try
            Dim cmd As New MySqlCommand("SP_REPORTE_SUMARIZADO_MIGRACIONES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_INI", MySqlDbType.VarChar, 10).Value = be.inicio
                .Add("VAR_FIN", MySqlDbType.VarChar, 10).Value = be.fin
            End With

            cnxMySql.Open()
            cmd.CommandTimeout = 600000
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MS = "SP_REPORTE_SUMARIZADO_MIGRACIONES " & ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_REPORTE_SUMARIZADO_RENOVACIONES(ByVal be As BE_CLARO) As DataTable
        Dim dt As New DataTable
        Dim MS As String
        Try
            Dim cmd As New MySqlCommand("SP_REPORTE_SUMARIZADO_RENOVACIONES", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure
            With cmd.Parameters
                .Add("VAR_INI", MySqlDbType.VarChar, 10).Value = be.inicio
                .Add("VAR_FIN", MySqlDbType.VarChar, 10).Value = be.fin
            End With

            cnxMySql.Open()
            cmd.CommandTimeout = 600000
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            MS = "SP_REPORTE_SUMARIZADO_RENOVACIONES " & ex.Message
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function

    Public Function SP_LISTAR_RENOVACIONES_GENERAL_VENTAS() As DataTable
        Dim dt As New DataTable
        Try
            Dim cmd As New MySqlCommand("SP_LISTAR_RENOVACIONES_GENERAL_VENTAS", cnxMySql)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 600000
            cnxMySql.Open()
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox("SP_LISTAR_RENOVACIONES_GENERAL " & ex.Message)
            dt = dtError("SP_LISTAR_RENOVACIONES_GENERAL : " & ex.Message)
        Finally
            cnxMySql.Close()
        End Try
        Return dt
    End Function
End Class

