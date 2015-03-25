Imports Microsoft.VisualBasic

Public Class BE_CLARO
    Public Property VAR_FECHA_CARGA As String
    Public Property VAR_EMAIL As String
    Public Property VAR_ID_LLAMADA As String
    Public Property VAR_FECHA As String
    Public Property VAR_REFERENCIA As String
    Public Property VAR_REFERENCIA_1 As String
    Public Property VAR_REFERENCIA_2 As String
    Public Property VAR_SEMANA As String
    Public Property VAR_PROYECTO As String
    Public Property VAR_FECH_INSTALACION As String
    Public Property VAR_FEC_NAC As String
    Public Property VAR_COMPANIA As String
    Public Property VAR_CF_TOTAL As String
    Public Property VAR_NRO_SOT As String
    Public Property VAR_FEC_ACT As String
    Public Property VAR_MAT_DES As String
    Public Property VAR_CUSTCODE As String
    Public Property VAR_CUSTOMER_ID As String
    Public Property VAR_FACTURA As String
    Public Property VAR_FECHA_VENC As String
    Public Property VAR_MONTO_ORG As String
    Public Property VAR_MONTO_PEND As String
    Public Property VAR_CODS_CLI As String
    Public Property VAR_IDS As String
    Public Property VAR_USUARIO As String
    Public Property VAR_FECHA_CREACION As String
    Public Property VAR_NRO_DIA1 As String
    Public Property VAR_NRO_DIA2 As String
    Public Property VAR_ACCOUNT_DESC As String
    Public Property VAR_TIPO_DOC_EMITIDO As String
    Public Property VAR_RECIBO As String
    Public Property VAR_FEC_EMISION As String
    Public Property VAR_MONTO_REC As String
    Public Property VAR_FECHA_ASIG As String
    Public Property VAR_EST_ACT_SERVICIO As String
    Public Property VAR_INDICADOR As String
    Public Property VAR_TEL3 As String
    Public Property VAR_TEL4 As String
    Public Property VAR_TEL5 As String
    Public Property VAR_TEL6 As String
    Public Property VAR_NIVEL_1 As String
    Public Property VAR_NIVEL_2 As String
    Public Property VAR_NIVEL_3 As String
    Public Property VAR_TXT_RESULTADO_BACKOFFICE As String
    Public Property VAR_TXT_RESULTADO_CALIDAD As String
    Public Property login As String
    Public Property VAR_OBS_BACKOFFICE As String

    Private _inicio As String
    Public Property inicio() As String
        Get
            Return _inicio
        End Get
        Set(ByVal value As String)
            _inicio = value
        End Set
    End Property

    Private _fin As String
    Public Property fin() As String
        Get
            Return _fin
        End Get
        Set(ByVal value As String)
            _fin = value
        End Set
    End Property

    Private _telefono As String
    Public Property telefono() As String
        Get
            Return _telefono
        End Get
        Set(ByVal value As String)
            _telefono = value
        End Set
    End Property

    Private _parametro As String
    Public Property parametro() As String
        Get
            Return _parametro
        End Get
        Set(ByVal value As String)
            _parametro = value
        End Set
    End Property

    Private _tipoConsulta As String
    Public Property tipoConsulta() As String
        Get
            Return _tipoConsulta
        End Get
        Set(ByVal value As String)
            _tipoConsulta = value
        End Set
    End Property

    Private _servicio As String
    Public Property servicio() As String
        Get
            Return _servicio
        End Get
        Set(ByVal value As String)
            _servicio = value
        End Set
    End Property


    Private _campanya As String
    Public Property campanya() As String
        Get
            Return _campanya
        End Get
        Set(ByVal value As String)
            _campanya = value
        End Set
    End Property

    Private _tipo As String
    Public Property tipo() As String
        Get
            Return _tipo
        End Get
        Set(ByVal value As String)
            _tipo = value
        End Set
    End Property

    Private _VAR_ID As String
    Public Property VAR_ID() As String
        Get
            Return _VAR_ID
        End Get
        Set(ByVal value As String)
            _VAR_ID = value
        End Set
    End Property

    Private _VAR_TXT_CAC_CAMPANIA As String
    Public Property VAR_TXT_CAC_CAMPANIA() As String
        Get
            Return _VAR_TXT_CAC_CAMPANIA
        End Get
        Set(ByVal value As String)
            _VAR_TXT_CAC_CAMPANIA = value
        End Set
    End Property

    Private _VAR_TXT_CAC_NOM_CLIENTE As String
    Public Property VAR_TXT_CAC_NOM_CLIENTE() As String
        Get
            Return _VAR_TXT_CAC_NOM_CLIENTE
        End Get
        Set(ByVal value As String)
            _VAR_TXT_CAC_NOM_CLIENTE = value
        End Set
    End Property

    Private _VAR_TXT_CAC_DNI As String
    Public Property VAR_TXT_CAC_DNI() As String
        Get
            Return _VAR_TXT_CAC_DNI
        End Get
        Set(ByVal value As String)
            _VAR_TXT_CAC_DNI = value
        End Set
    End Property

    Private _VAR_TXT_CAC_PLAN_OFRECIDO As String
    Public Property VAR_TXT_CAC_PLAN_OFRECIDO() As String
        Get
            Return _VAR_TXT_CAC_PLAN_OFRECIDO
        End Get
        Set(ByVal value As String)
            _VAR_TXT_CAC_PLAN_OFRECIDO = value
        End Set
    End Property

    Private _VAR_TXT_CAC_MARCA_MODELO As String
    Public Property VAR_TXT_CAC_MARCA_MODELO() As String
        Get
            Return _VAR_TXT_CAC_MARCA_MODELO
        End Get
        Set(ByVal value As String)
            _VAR_TXT_CAC_MARCA_MODELO = value
        End Set
    End Property

    Private _VAR_TXT_CAC_PLAZO_CONTRATO As String
    Public Property VAR_TXT_CAC_PLAZO_CONTRATO() As String
        Get
            Return _VAR_TXT_CAC_PLAZO_CONTRATO
        End Get
        Set(ByVal value As String)
            _VAR_TXT_CAC_PLAZO_CONTRATO = value
        End Set
    End Property

    Private _VAR_TXT_CAC_TOPE_CONSUMO As String
    Public Property VAR_TXT_CAC_TOPE_CONSUMO() As String
        Get
            Return _VAR_TXT_CAC_TOPE_CONSUMO
        End Get
        Set(ByVal value As String)
            _VAR_TXT_CAC_TOPE_CONSUMO = value
        End Set
    End Property

    Private _VAR_TXT_PAGO_EQ_FRACCIONADO As String
    Public Property VAR_TXT_PAGO_EQ_FRACCIONADO() As String
        Get
            Return _VAR_TXT_PAGO_EQ_FRACCIONADO
        End Get
        Set(ByVal value As String)
            _VAR_TXT_PAGO_EQ_FRACCIONADO = value
        End Set
    End Property
    Private _VAR_D_DETALLE As String
    Public Property VAR_D_DETALLE() As String
        Get
            Return _VAR_D_DETALLE
        End Get
        Set(ByVal value As String)
            _VAR_D_DETALLE = value
        End Set
    End Property

    Private _VAR_TXT_PRECIO_EQ As String
    Public Property VAR_TXT_PRECIO_EQ() As String
        Get
            Return _VAR_TXT_PRECIO_EQ
        End Get
        Set(ByVal value As String)
            _VAR_TXT_PRECIO_EQ = value
        End Set
    End Property

    Private _VAR_TXT_CAC_CALLCENTER As String
    Public Property VAR_TXT_CAC_CALLCENTER() As String
        Get
            Return _VAR_TXT_CAC_CALLCENTER
        End Get
        Set(ByVal value As String)
            _VAR_TXT_CAC_CALLCENTER = value
        End Set
    End Property

    Private _VAR_D_TELEF_MOVIL As String
    Public Property VAR_D_TELEF_MOVIL() As String
        Get
            Return _VAR_D_TELEF_MOVIL
        End Get
        Set(ByVal value As String)
            _VAR_D_TELEF_MOVIL = value
        End Set
    End Property

    Private _VAR_D_FECHA_ENVIO As String
    Public Property VAR_D_FECHA_ENVIO() As String
        Get
            Return _VAR_D_FECHA_ENVIO
        End Get
        Set(ByVal value As String)
            _VAR_D_FECHA_ENVIO = value
        End Set
    End Property

    Private _VAR_D_FECHA_GESTION As String
    Public Property VAR_D_FECHA_GESTION() As String
        Get
            Return _VAR_D_FECHA_GESTION
        End Get
        Set(ByVal value As String)
            _VAR_D_FECHA_GESTION = value
        End Set
    End Property

    Private _VAR_D_COD_RECARGA As String
    Public Property VAR_D_COD_RECARGA() As String
        Get
            Return _VAR_D_COD_RECARGA
        End Get
        Set(ByVal value As String)
            _VAR_D_COD_RECARGA = value
        End Set
    End Property
    Private _VAR_D_CODINSSRV As String
    Public Property VAR_D_CODINSSRV() As String
        Get
            Return _VAR_D_CODINSSRV
        End Get
        Set(ByVal value As String)
            _VAR_D_CODINSSRV = value
        End Set
    End Property

    Private _VAR_D_COD_CLIENTE As String
    Public Property VAR_D_COD_CLIENTE() As String
        Get
            Return _VAR_D_COD_CLIENTE
        End Get
        Set(ByVal value As String)
            _VAR_D_COD_CLIENTE = value
        End Set
    End Property

    Private _VAR_D_ESTADOPAGO As String
    Public Property VAR_D_ESTADOPAGO() As String
        Get
            Return _VAR_D_ESTADOPAGO
        End Get
        Set(ByVal value As String)
            _VAR_D_ESTADOPAGO = value
        End Set
    End Property
    Private _VAR_D_SOLICITUD As String
    Public Property VAR_D_SOLICITUD() As String
        Get
            Return _VAR_D_SOLICITUD
        End Get
        Set(ByVal value As String)
            _VAR_D_SOLICITUD = value
        End Set
    End Property

    Private _VAR_D_NRO_SEC As String
    Public Property VAR_D_NRO_SEC() As String
        Get
            Return _VAR_D_NRO_SEC
        End Get
        Set(ByVal value As String)
            _VAR_D_NRO_SEC = value
        End Set
    End Property

    Private _VAR_D_CANTIDAD As String
    Public Property VAR_D_CANTIDAD() As String
        Get
            Return _VAR_D_CANTIDAD
        End Get
        Set(ByVal value As String)
            _VAR_D_CANTIDAD = value
        End Set
    End Property

    Private _VAR_D_TIPO As String
    Public Property VAR_D_TIPO() As String
        Get
            Return _VAR_D_TIPO
        End Get
        Set(ByVal value As String)
            _VAR_D_TIPO = value
        End Set
    End Property

    Private _VAR_D_Despacho As String
    Public Property VAR_D_Despacho() As String
        Get
            Return _VAR_D_Despacho
        End Get
        Set(ByVal value As String)
            _VAR_D_Despacho = value
        End Set
    End Property

    Private _VAR_D_VENTA_EFECTIVA As String
    Public Property VAR_D_VENTA_EFECTIVA() As String
        Get
            Return _VAR_D_VENTA_EFECTIVA
        End Get
        Set(ByVal value As String)
            _VAR_D_VENTA_EFECTIVA = value
        End Set
    End Property

    Private _VAR_D_OPERADOR_CEDENTE As String
    Public Property VAR_D_OPERADOR_CEDENTE() As String
        Get
            Return _VAR_D_OPERADOR_CEDENTE
        End Get
        Set(ByVal value As String)
            _VAR_D_OPERADOR_CEDENTE = value
        End Set
    End Property

    Private _VAR_D_OPERADOR_RECEPTOR As String
    Public Property VAR_D_OPERADOR_RECEPTOR() As String
        Get
            Return _VAR_D_OPERADOR_RECEPTOR
        End Get
        Set(ByVal value As String)
            _VAR_D_OPERADOR_RECEPTOR = value
        End Set
    End Property

    Private _VAR_D_MODALIDAD_TELEFONO As String
    Public Property VAR_D_MODALIDAD_TELEFONO() As String
        Get
            Return _VAR_D_MODALIDAD_TELEFONO
        End Get
        Set(ByVal value As String)
            _VAR_D_MODALIDAD_TELEFONO = value
        End Set
    End Property

    Private _VAR_D_CONTACTO_CLIENTE As String
    Public Property VAR_D_CONTACTO_CLIENTE() As String
        Get
            Return _VAR_D_CONTACTO_CLIENTE
        End Get
        Set(ByVal value As String)
            _VAR_D_CONTACTO_CLIENTE = value
        End Set
    End Property

    Private _VAR_D_TIPO_DOC As String
    Public Property VAR_D_TIPO_DOC() As String
        Get
            Return _VAR_D_TIPO_DOC
        End Get
        Set(ByVal value As String)
            _VAR_D_TIPO_DOC = value
        End Set
    End Property


    Private _VAR_D_NOMBRE_CLIENTE As String
    Public Property VAR_D_NOMBRE_CLIENTE() As String
        Get
            Return _VAR_D_NOMBRE_CLIENTE
        End Get
        Set(ByVal value As String)
            _VAR_D_NOMBRE_CLIENTE = value
        End Set
    End Property

    Private _VAR_D_ESTADO_SP As String
    Public Property VAR_D_ESTADO_SP() As String
        Get
            Return _VAR_D_ESTADO_SP
        End Get
        Set(ByVal value As String)
            _VAR_D_ESTADO_SP = value
        End Set
    End Property

    Private _VAR_D_TIPO_MENSAJE_SP As String
    Public Property VAR_D_TIPO_MENSAJE_SP() As String
        Get
            Return _VAR_D_TIPO_MENSAJE_SP
        End Get
        Set(ByVal value As String)
            _VAR_D_TIPO_MENSAJE_SP = value
        End Set
    End Property

    Private _VAR_D_MOTIVO_SP As String
    Public Property VAR_D_MOTIVO_SP() As String
        Get
            Return _VAR_D_MOTIVO_SP
        End Get
        Set(ByVal value As String)
            _VAR_D_MOTIVO_SP = value
        End Set
    End Property

    Private _VAR_D_OBSERVACION_SP As String
    Public Property VAR_D_OBSERVACION_SP() As String
        Get
            Return _VAR_D_OBSERVACION_SP
        End Get
        Set(ByVal value As String)
            _VAR_D_OBSERVACION_SP = value
        End Set
    End Property

    Private _VAR_D_FEC_PROGRAMACION As String
    Public Property VAR_D_FEC_PROGRAMACION() As String
        Get
            Return _VAR_D_FEC_PROGRAMACION
        End Get
        Set(ByVal value As String)
            _VAR_D_FEC_PROGRAMACION = value
        End Set
    End Property

    Private _VAR_D_FECHA_REGISTRO As String
    Public Property VAR_D_FECHA_REGISTRO() As String
        Get
            Return _VAR_D_FECHA_REGISTRO
        End Get
        Set(ByVal value As String)
            _VAR_D_FECHA_REGISTRO = value
        End Set
    End Property

    Private _VAR_D_PUNTO_VENTA As String
    Public Property VAR_D_PUNTO_VENTA() As String
        Get
            Return _VAR_D_PUNTO_VENTA
        End Get
        Set(ByVal value As String)
            _VAR_D_PUNTO_VENTA = value
        End Set
    End Property

    Private _VAR_D_ID_SOLICITUD_PORTA As String
    Public Property VAR_D_ID_SOLICITUD_PORTA() As String
        Get
            Return _VAR_D_ID_SOLICITUD_PORTA
        End Get
        Set(ByVal value As String)
            _VAR_D_ID_SOLICITUD_PORTA = value
        End Set
    End Property

    Private _VAR_D_OBSERVACION As String
    Public Property VAR_D_OBSERVACION() As String
        Get
            Return _VAR_D_OBSERVACION
        End Get
        Set(ByVal value As String)
            _VAR_D_OBSERVACION = value
        End Set
    End Property

    Private _VAR_D_SUSTENTO As String
    Public Property VAR_D_SUSTENTO() As String
        Get
            Return _VAR_D_SUSTENTO
        End Get
        Set(ByVal value As String)
            _VAR_D_SUSTENTO = value
        End Set
    End Property
    Private _VAR_RAZON_SOCIAL As String
    Public Property VAR_RAZON_SOCIAL() As String
        Get
            Return _VAR_RAZON_SOCIAL
        End Get
        Set(ByVal value As String)
            _VAR_RAZON_SOCIAL = value
        End Set
    End Property

    Private _VAR_RUC As String
    Public Property VAR_RUC() As String
        Get
            Return _VAR_RUC
        End Get
        Set(ByVal value As String)
            _VAR_RUC = value
        End Set
    End Property

    Private _VAR_MSISDN As String
    Public Property VAR_MSISDN() As String
        Get
            Return _VAR_MSISDN
        End Get
        Set(ByVal value As String)
            _VAR_MSISDN = value
        End Set
    End Property

    Private _VAR_PLAN_TARIFARIO As String
    Public Property VAR_PLAN_TARIFARIO() As String
        Get
            Return _VAR_PLAN_TARIFARIO
        End Get
        Set(ByVal value As String)
            _VAR_PLAN_TARIFARIO = value
        End Set
    End Property

    Private _VAR_TIPO_CLIENTE As String
    Public Property VAR_TIPO_CLIENTE() As String
        Get
            Return _VAR_TIPO_CLIENTE
        End Get
        Set(ByVal value As String)
            _VAR_TIPO_CLIENTE = value
        End Set
    End Property

    Private _VAR_CICLO As String
    Public Property VAR_CICLO() As String
        Get
            Return _VAR_CICLO
        End Get
        Set(ByVal value As String)
            _VAR_CICLO = value
        End Set
    End Property

    Private _VAR_FECHA_ITERACCION As String
    Public Property VAR_FECHA_ITERACCION() As String
        Get
            Return _VAR_FECHA_ITERACCION
        End Get
        Set(ByVal value As String)
            _VAR_FECHA_ITERACCION = value
        End Set
    End Property

    Private _VAR_CLAROPUNTOS As String
    Public Property VAR_CLAROPUNTOS() As String
        Get
            Return _VAR_CLAROPUNTOS
        End Get
        Set(ByVal value As String)
            _VAR_CLAROPUNTOS = value
        End Set
    End Property

    Private _VAR_IMR As String
    Public Property VAR_IMR() As String
        Get
            Return _VAR_IMR
        End Get
        Set(ByVal value As String)
            _VAR_IMR = value
        End Set
    End Property

    Private _VAR_DISTRITO As String
    Public Property VAR_DISTRITO() As String
        Get
            Return _VAR_DISTRITO
        End Get
        Set(ByVal value As String)
            _VAR_DISTRITO = value
        End Set
    End Property

    Private _VAR_PROVINCIA As String
    Public Property VAR_PROVINCIA() As String
        Get
            Return _VAR_PROVINCIA
        End Get
        Set(ByVal value As String)
            _VAR_PROVINCIA = value
        End Set
    End Property

    Private _VAR_DEPARTAMENTO As String
    Public Property VAR_DEPARTAMENTO() As String
        Get
            Return _VAR_DEPARTAMENTO
        End Get
        Set(ByVal value As String)
            _VAR_DEPARTAMENTO = value
        End Set
    End Property

    Private _VAR_DIRECCION As String
    Public Property VAR_DIRECCION() As String
        Get
            Return _VAR_DIRECCION
        End Get
        Set(ByVal value As String)
            _VAR_DIRECCION = value
        End Set
    End Property

    Private _VAR_SEGMENTO As String
    Public Property VAR_SEGMENTO() As String
        Get
            Return _VAR_SEGMENTO
        End Get
        Set(ByVal value As String)
            _VAR_SEGMENTO = value
        End Set
    End Property

    Private _VAR_RUC_DNI As String
    Public Property VAR_RUC_DNI() As String
        Get
            Return _VAR_RUC_DNI
        End Get
        Set(ByVal value As String)
            _VAR_RUC_DNI = value
        End Set
    End Property
    Private _VAR_VARIOS_RECIBOS As String
    Public Property VAR_VARIOS_RECIBOS() As String
        Get
            Return _VAR_VARIOS_RECIBOS
        End Get
        Set(ByVal value As String)
            _VAR_VARIOS_RECIBOS = value
        End Set
    End Property
    Private _VAR_NRO_PAGO As String
    Public Property VAR_NRO_PAGO() As String
        Get
            Return _VAR_NRO_PAGO
        End Get
        Set(ByVal value As String)
            _VAR_NRO_PAGO = value
        End Set
    End Property

    Private _VAR_TELEFONO As String
    Public Property VAR_TELEFONO() As String
        Get
            Return _VAR_TELEFONO
        End Get
        Set(ByVal value As String)
            _VAR_TELEFONO = value
        End Set
    End Property

    Private _VAR_PLAN As String
    Public Property VAR_PLAN() As String
        Get
            Return _VAR_PLAN
        End Get
        Set(ByVal value As String)
            _VAR_PLAN = value
        End Set
    End Property

    Private _VAR_CUENTA As String
    Public Property VAR_CUENTA() As String
        Get
            Return _VAR_CUENTA
        End Get
        Set(ByVal value As String)
            _VAR_CUENTA = value
        End Set
    End Property

    Private _VAR_CLIENTE As String
    Public Property VAR_CLIENTE() As String
        Get
            Return _VAR_CLIENTE
        End Get
        Set(ByVal value As String)
            _VAR_CLIENTE = value
        End Set
    End Property

    Private _VAR_CAMPANIA As String
    Public Property VAR_CAMPANIA() As String
        Get
            Return _VAR_CAMPANIA
        End Get
        Set(ByVal value As String)
            _VAR_CAMPANIA = value
        End Set
    End Property

    Private _VAR_DESCRIPCION As String
    Public Property VAR_DESCRIPCION() As String
        Get
            Return _VAR_DESCRIPCION
        End Get
        Set(ByVal value As String)
            _VAR_DESCRIPCION = value
        End Set
    End Property

    Private _VAR_VOZ_MODEM As String
    Public Property VAR_VOZ_MODEM() As String
        Get
            Return _VAR_VOZ_MODEM
        End Get
        Set(ByVal value As String)
            _VAR_VOZ_MODEM = value
        End Set
    End Property

    Private _VAR_CONTACTO As String
    Public Property VAR_CONTACTO() As String
        Get
            Return _VAR_CONTACTO
        End Get
        Set(ByVal value As String)
            _VAR_CONTACTO = value
        End Set
    End Property

    Private _VAR_TEL1 As String
    Public Property VAR_TEL1() As String
        Get
            Return _VAR_TEL1
        End Get
        Set(ByVal value As String)
            _VAR_TEL1 = value
        End Set
    End Property

    Private _VAR_TEL2 As String
    Public Property VAR_TEL2() As String
        Get
            Return _VAR_TEL2
        End Get
        Set(ByVal value As String)
            _VAR_TEL2 = value
        End Set
    End Property
    Private _VAR_TELEF_PREP6 As String
    Public Property VAR_TELEF_PREP6() As String
        Get
            Return _VAR_TELEF_PREP6
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_PREP6 = value
        End Set
    End Property
    Private _VAR_TELEF_PREP7 As String
    Public Property VAR_TELEF_PREP7() As String
        Get
            Return _VAR_TELEF_PREP7
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_PREP7 = value
        End Set
    End Property
    Private _VAR_D_PLAN_GENERAL As String
    Public Property VAR_D_PLAN_GENERAL() As String
        Get
            Return _VAR_D_PLAN_GENERAL
        End Get
        Set(ByVal value As String)
            _VAR_D_PLAN_GENERAL = value
        End Set
    End Property
    Private _VAR_D_DEPARTAMENTO As String
    Public Property VAR_D_DEPARTAMENTO() As String
        Get
            Return _VAR_D_DEPARTAMENTO
        End Get
        Set(ByVal value As String)
            _VAR_D_DEPARTAMENTO = value
        End Set
    End Property
    Private _VAR_TELEF_PREP8 As String
    Public Property VAR_TELEF_PREP8() As String
        Get
            Return _VAR_TELEF_PREP8
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_PREP8 = value
        End Set
    End Property
    Private _VAR_TELEF_PREP9 As String
    Public Property VAR_TELEF_PREP9() As String
        Get
            Return _VAR_TELEF_PREP9
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_PREP9 = value
        End Set
    End Property
    Private _VAR_TELEF_PREP10 As String
    Public Property VAR_TELEF_PREP10() As String
        Get
            Return _VAR_TELEF_PREP10
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_PREP10 = value
        End Set
    End Property

    Private _VAR_TELCELULAR As String
    Public Property VAR_TELCELULAR() As String
        Get
            Return _VAR_TELCELULAR
        End Get
        Set(ByVal value As String)
            _VAR_TELCELULAR = value
        End Set
    End Property

    Private _VAR_DNI As String
    Public Property VAR_DNI() As String
        Get
            Return _VAR_DNI
        End Get
        Set(ByVal value As String)
            _VAR_DNI = value
        End Set
    End Property

    Private _VAR_EMAIL_ERRADO As String
    Public Property VAR_EMAIL_ERRADO() As String
        Get
            Return _VAR_EMAIL_ERRADO
        End Get
        Set(ByVal value As String)
            _VAR_EMAIL_ERRADO = value
        End Set
    End Property

    Private _VAR_ERRADO As String
    Public Property VAR_ERRADO() As String
        Get
            Return _VAR_ERRADO
        End Get
        Set(ByVal value As String)
            _VAR_ERRADO = value
        End Set
    End Property

    Private _VAR_CICLO_FACT As String
    Public Property VAR_CICLO_FACT() As String
        Get
            Return _VAR_CICLO_FACT
        End Get
        Set(ByVal value As String)
            _VAR_CICLO_FACT = value
        End Set
    End Property

    Private _VAR_ID_CARGA As String
    Public Property VAR_ID_CARGA() As String
        Get
            Return _VAR_ID_CARGA
        End Get
        Set(ByVal value As String)
            _VAR_ID_CARGA = value
        End Set
    End Property

    Private _VAR_NOMBRE_BASE As String
    Public Property VAR_NOMBRE_BASE() As String
        Get
            Return _VAR_NOMBRE_BASE
        End Get
        Set(ByVal value As String)
            _VAR_NOMBRE_BASE = value
        End Set
    End Property

    Private _VAR_FECHA_FIN As String
    Public Property VAR_FECHA_FIN() As String
        Get
            Return _VAR_FECHA_FIN
        End Get
        Set(ByVal value As String)
            _VAR_FECHA_FIN = value
        End Set
    End Property

    Private _VAR_FECHA_INICIO As String
    Public Property VAR_FECHA_INICIO() As String
        Get
            Return _VAR_FECHA_INICIO
        End Get
        Set(ByVal value As String)
            _VAR_FECHA_INICIO = value
        End Set
    End Property

    Private _VAR_FECHAEXP_CREDDEB As String
    Public Property VAR_FECHAEXP_CREDDEB() As String
        Get
            Return _VAR_FECHAEXP_CREDDEB
        End Get
        Set(ByVal value As String)
            _VAR_FECHAEXP_CREDDEB = value
        End Set
    End Property

    Private _VAR_SERVICIO As String
    Public Property VAR_SERVICIO() As String
        Get
            Return _VAR_SERVICIO
        End Get
        Set(ByVal value As String)
            _VAR_SERVICIO = value
        End Set
    End Property

    Private _VAR_TELEF_REFERENCIA As String
    Public Property VAR_TELEF_REFERENCIA() As String
        Get
            Return _VAR_TELEF_REFERENCIA
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_REFERENCIA = value
        End Set
    End Property

    Private _VAR_MOTIVO_CANCELACION As String
    Public Property VAR_MOTIVO_CANCELACION() As String
        Get
            Return _VAR_MOTIVO_CANCELACION
        End Get
        Set(ByVal value As String)
            _VAR_MOTIVO_CANCELACION = value
        End Set
    End Property

    Private _VAR_TELEF_PREP1 As String
    Public Property VAR_TELEF_PREP1() As String
        Get
            Return _VAR_TELEF_PREP1
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_PREP1 = value
        End Set
    End Property

    Private _VAR_TELEF_PREP2 As String
    Public Property VAR_TELEF_PREP2() As String
        Get
            Return _VAR_TELEF_PREP2
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_PREP2 = value
        End Set
    End Property
    Private _VAR_TELEF_PREP3 As String
    Public Property VAR_TELEF_PREP3() As String
        Get
            Return _VAR_TELEF_PREP3
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_PREP3 = value
        End Set
    End Property
    Private _VAR_TELEF_PREP4 As String
    Public Property VAR_TELEF_PREP4() As String
        Get
            Return _VAR_TELEF_PREP4
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_PREP4 = value
        End Set
    End Property
    Private _VAR_TELEF_PREP5 As String
    Public Property VAR_TELEF_PREP5() As String
        Get
            Return _VAR_TELEF_PREP5
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_PREP5 = value
        End Set
    End Property

    Private _VAR_TELEF_POST1 As String
    Public Property VAR_TELEF_POST1() As String
        Get
            Return _VAR_TELEF_POST1
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_POST1 = value
        End Set
    End Property

    Private _VAR_TELEF_POST2 As String
    Public Property VAR_TELEF_POST2() As String
        Get
            Return _VAR_TELEF_POST2
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_POST2 = value
        End Set
    End Property

    Private _VAR_TELEF_POST3 As String
    Public Property VAR_TELEF_POST3() As String
        Get
            Return _VAR_TELEF_POST3
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_POST3 = value
        End Set
    End Property

    Private _VAR_TELEF_POST4 As String
    Public Property VAR_TELEF_POST4() As String
        Get
            Return _VAR_TELEF_POST4
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_POST4 = value
        End Set
    End Property

    Private _VAR_TELEF_POST5 As String
    Public Property VAR_TELEF_POST5() As String
        Get
            Return _VAR_TELEF_POST5
        End Get
        Set(ByVal value As String)
            _VAR_TELEF_POST5 = value
        End Set
    End Property

    Private _VAR_TELF_REF As String
    Public Property VAR_TELF_REF() As String
        Get
            Return _VAR_TELF_REF
        End Get
        Set(ByVal value As String)
            _VAR_TELF_REF = value
        End Set
    End Property

    Private _VAR_PLAZO_ACUERDO As String
    Public Property VAR_PLAZO_ACUERDO() As String
        Get
            Return _VAR_PLAZO_ACUERDO
        End Get
        Set(ByVal value As String)
            _VAR_PLAZO_ACUERDO = value
        End Set
    End Property


    Private _VAR_ACCION As String
    Public Property VAR_ACCION() As String
        Get
            Return _VAR_ACCION
        End Get
        Set(ByVal value As String)
            _VAR_ACCION = value
        End Set
    End Property

    Private _VAR_IDLOG As String
    Public Property VAR_IDLOG() As String
        Get
            Return _VAR_IDLOG
        End Get
        Set(ByVal value As String)
            _VAR_IDLOG = value
        End Set
    End Property

    Private _VAR_RDATE As String
    Public Property VAR_RDATE() As String
        Get
            Return _VAR_RDATE
        End Get
        Set(ByVal value As String)
            _VAR_RDATE = value
        End Set
    End Property

    Private _VAR_SERVICEID As String
    Public Property VAR_SERVICEID() As String
        Get
            Return _VAR_SERVICEID
        End Get
        Set(ByVal value As String)
            _VAR_SERVICEID = value
        End Set
    End Property

    Private _VAR_LOADID As String
    Public Property VAR_LOADID() As String
        Get
            Return _VAR_LOADID
        End Get
        Set(ByVal value As String)
            _VAR_LOADID = value
        End Set
    End Property

    Private _VAR_SOURCEID As String
    Public Property VAR_SOURCEID() As String
        Get
            Return _VAR_SOURCEID
        End Get
        Set(ByVal value As String)
            _VAR_SOURCEID = value
        End Set
    End Property

    Private _VAR_LOGIN As String
    Public Property VAR_LOGIN() As String
        Get
            Return _VAR_LOGIN
        End Get
        Set(ByVal value As String)
            _VAR_LOGIN = value
        End Set
    End Property

    Private _VAR_QCODE As String
    Public Property VAR_QCODE() As String
        Get
            Return _VAR_QCODE
        End Get
        Set(ByVal value As String)
            _VAR_QCODE = value
        End Set
    End Property

    Private _VAR_FINAL As String
    Public Property VAR_FINAL() As String
        Get
            Return _VAR_FINAL
        End Get
        Set(ByVal value As String)
            _VAR_FINAL = value
        End Set
    End Property

    Private _bd As String
    Public Property bd() As String
        Get
            Return _bd
        End Get
        Set(ByVal value As String)
            _bd = value
        End Set
    End Property


    Private _VAR_D_CELULAR As String
    Public Property VAR_D_CELULAR() As String
        Get
            Return _VAR_D_CELULAR
        End Get
        Set(ByVal value As String)
            _VAR_D_CELULAR = value
        End Set
    End Property

    Private _VAR_D_NOMBRES As String
    Public Property VAR_D_NOMBRES() As String
        Get
            Return _VAR_D_NOMBRES
        End Get
        Set(ByVal value As String)
            _VAR_D_NOMBRES = value
        End Set
    End Property

    Private _VAR_D_SERVICIO_CLARO As String
    Public Property VAR_D_SERVICIO_CLARO() As String
        Get
            Return _VAR_D_SERVICIO_CLARO
        End Get
        Set(ByVal value As String)
            _VAR_D_SERVICIO_CLARO = value
        End Set
    End Property

    Private _VAR_D_ENTIDAD_BANCARIA As String
    Public Property VAR_D_ENTIDAD_BANCARIA() As String
        Get
            Return _VAR_D_ENTIDAD_BANCARIA
        End Get
        Set(ByVal value As String)
            _VAR_D_ENTIDAD_BANCARIA = value
        End Set
    End Property
    Private _VAR_D_TIPO_TARJETA As String
    Public Property VAR_D_TIPO_TARJETA() As String
        Get
            Return _VAR_D_TIPO_TARJETA
        End Get
        Set(ByVal value As String)
            _VAR_D_TIPO_TARJETA = value
        End Set
    End Property
    Private _VAR_D_MONTO_TOPE_MAX As String
    Public Property VAR_D_MONTO_TOPE_MAX() As String
        Get
            Return _VAR_D_MONTO_TOPE_MAX
        End Get
        Set(ByVal value As String)
            _VAR_D_MONTO_TOPE_MAX = value
        End Set
    End Property
    Private _VAR_D_FEC_RECHAZO As String
    Public Property VAR_D_FEC_RECHAZO() As String
        Get
            Return _VAR_D_FEC_RECHAZO
        End Get
        Set(ByVal value As String)
            _VAR_D_FEC_RECHAZO = value
        End Set
    End Property
    Private _VAR_D_MOTIVO_RECHAZO As String
    Public Property VAR_D_MOTIVO_RECHAZO() As String
        Get
            Return _VAR_D_MOTIVO_RECHAZO
        End Get
        Set(ByVal value As String)
            _VAR_D_MOTIVO_RECHAZO = value
        End Set
    End Property
    Private _VAR_D_MONTO_RECHAZADO As String
    Public Property VAR_D_MONTO_RECHAZADO() As String
        Get
            Return _VAR_D_MONTO_RECHAZADO
        End Get
        Set(ByVal value As String)
            _VAR_D_MONTO_RECHAZADO = value
        End Set
    End Property
    Private _VAR_D_NRO_CUENTA As String
    Public Property VAR_D_NRO_CUENTA() As String
        Get
            Return _VAR_D_NRO_CUENTA
        End Get
        Set(ByVal value As String)
            _VAR_D_NRO_CUENTA = value
        End Set
    End Property
    Private _VAR_D_NRO_TELEF1 As String
    Public Property VAR_D_NRO_TELEF1() As String
        Get
            Return _VAR_D_NRO_TELEF1
        End Get
        Set(ByVal value As String)
            _VAR_D_NRO_TELEF1 = value
        End Set
    End Property
    Private _VAR_D_NRO_TELEF2 As String
    Public Property VAR_D_NRO_TELEF2() As String
        Get
            Return _VAR_D_NRO_TELEF2
        End Get
        Set(ByVal value As String)
            _VAR_D_NRO_TELEF2 = value
        End Set
    End Property
    Private _VAR_D_NRO_TELEF3 As String
    Public Property VAR_D_NRO_TELEF3() As String
        Get
            Return _VAR_D_NRO_TELEF3
        End Get
        Set(ByVal value As String)
            _VAR_D_NRO_TELEF3 = value
        End Set
    End Property

    Private _VAR_D_NRO_TELEF4 As String
    Public Property VAR_D_NRO_TELEF4() As String
        Get
            Return _VAR_D_NRO_TELEF4
        End Get
        Set(ByVal value As String)
            _VAR_D_NRO_TELEF4 = value
        End Set
    End Property
    Private _VAR_D_EMAIL As String
    Public Property VAR_D_EMAIL() As String
        Get
            Return _VAR_D_EMAIL
        End Get
        Set(ByVal value As String)
            _VAR_D_EMAIL = value
        End Set
    End Property
    Private _VAR_D_DEUDA_1 As String
    Public Property VAR_D_DEUDA_1() As String
        Get
            Return _VAR_D_DEUDA_1
        End Get
        Set(ByVal value As String)
            _VAR_D_DEUDA_1 = value
        End Set
    End Property
    Private _VAR_D_FECHA_1 As String
    Public Property VAR_D_FECHA_1() As String
        Get
            Return _VAR_D_FECHA_1
        End Get
        Set(ByVal value As String)
            _VAR_D_FECHA_1 = value
        End Set
    End Property
    Private _VAR_D_NRO_CASO As String
    Public Property VAR_D_NRO_CASO() As String
        Get
            Return _VAR_D_NRO_CASO
        End Get
        Set(ByVal value As String)
            _VAR_D_NRO_CASO = value
        End Set
    End Property
    Private _VAR_D_RESULTADO_CASO As String
    Public Property VAR_D_RESULTADO_CASO() As String
        Get
            Return _VAR_D_RESULTADO_CASO
        End Get
        Set(ByVal value As String)
            _VAR_D_RESULTADO_CASO = value
        End Set
    End Property
    Private _VAR_D_MONTO_TOTAL_FAC As String
    Public Property VAR_D_MONTO_TOTAL_FAC() As String
        Get
            Return _VAR_D_MONTO_TOTAL_FAC
        End Get
        Set(ByVal value As String)
            _VAR_D_MONTO_TOTAL_FAC = value
        End Set
    End Property
    Private _VAR_D_FEC_VENCIMIENTO_FAC As String
    Public Property VAR_D_FEC_VENCIMIENTO_FAC() As String
        Get
            Return _VAR_D_FEC_VENCIMIENTO_FAC
        End Get
        Set(ByVal value As String)
            _VAR_D_FEC_VENCIMIENTO_FAC = value
        End Set
    End Property
    Private _VAR_D_MONTO_DEBITADO As String
    Public Property VAR_D_MONTO_DEBITADO() As String
        Get
            Return _VAR_D_MONTO_DEBITADO
        End Get
        Set(ByVal value As String)
            _VAR_D_MONTO_DEBITADO = value
        End Set
    End Property
    Private _VAR_D_DNI As String
    Public Property VAR_D_DNI() As String
        Get
            Return _VAR_D_DNI
        End Get
        Set(ByVal value As String)
            _VAR_D_DNI = value
        End Set
    End Property

    Private _tabla As String
    Public Property tabla() As String
        Get
            Return _tabla
        End Get
        Set(ByVal value As String)
            _tabla = value
        End Set
    End Property
    '*****************************************

    Private _VAR_D_CODSOLOT As String
    Public Property VAR_D_CODSOLOT() As String
        Get
            Return _VAR_D_CODSOLOT
        End Get
        Set(ByVal value As String)
            _VAR_D_CODSOLOT = value
        End Set
    End Property
    Private _VAR_D_TIPO_TRABAJO As String
    Public Property VAR_D_TIPO_TRABAJO() As String
        Get
            Return _VAR_D_TIPO_TRABAJO
        End Get
        Set(ByVal value As String)
            _VAR_D_TIPO_TRABAJO = value
        End Set
    End Property
    Private _VAR_D_DSCTIPSRV As String
    Public Property VAR_D_DSCTIPSRV() As String
        Get
            Return _VAR_D_DSCTIPSRV
        End Get
        Set(ByVal value As String)
            _VAR_D_DSCTIPSRV = value
        End Set
    End Property
    Private _VAR_D_ESTADO_SOT As String
    Public Property VAR_D_ESTADO_SOT() As String
        Get
            Return _VAR_D_ESTADO_SOT
        End Get
        Set(ByVal value As String)
            _VAR_D_ESTADO_SOT = value
        End Set
    End Property
    Private _VAR_D_FECUSU As String
    Public Property VAR_D_FECUSU() As String
        Get
            Return _VAR_D_FECUSU
        End Get
        Set(ByVal value As String)
            _VAR_D_FECUSU = value
        End Set
    End Property
    Private _VAR_D_ANIO As String
    Public Property VAR_D_ANIO() As String
        Get
            Return _VAR_D_ANIO
        End Get
        Set(ByVal value As String)
            _VAR_D_ANIO = value
        End Set
    End Property
    Private _VAR_D_MES As String
    Public Property VAR_D_MES() As String
        Get
            Return _VAR_D_MES
        End Get
        Set(ByVal value As String)
            _VAR_D_MES = value
        End Set
    End Property
    Private _VAR_D_CODCLI As String
    Public Property VAR_D_CODCLI() As String
        Get
            Return _VAR_D_CODCLI
        End Get
        Set(ByVal value As String)
            _VAR_D_CODCLI = value
        End Set
    End Property
    Private _VAR_D_NOMCLI As String
    Public Property VAR_D_NOMCLI() As String
        Get
            Return _VAR_D_NOMCLI
        End Get
        Set(ByVal value As String)
            _VAR_D_NOMCLI = value
        End Set
    End Property
    Private _VAR_D_COD_PAGO As String
    Public Property VAR_D_COD_PAGO() As String
        Get
            Return _VAR_D_COD_PAGO
        End Get
        Set(ByVal value As String)
            _VAR_D_COD_PAGO = value
        End Set
    End Property
    Private _VAR_D_COD_ID As String
    Public Property VAR_D_COD_ID() As String
        Get
            Return _VAR_D_COD_ID
        End Get
        Set(ByVal value As String)
            _VAR_D_COD_ID = value
        End Set
    End Property
    Private _VAR_D_MAIL_1 As String
    Public Property VAR_D_MAIL_1() As String
        Get
            Return _VAR_D_MAIL_1
        End Get
        Set(ByVal value As String)
            _VAR_D_MAIL_1 = value
        End Set
    End Property
    Private _VAR_D_MAIL_2 As String
    Public Property VAR_D_MAIL_2() As String
        Get
            Return _VAR_D_MAIL_2
        End Get
        Set(ByVal value As String)
            _VAR_D_MAIL_2 = value
        End Set
    End Property
    Private _VAR_D_TELEFONO_1 As String
    Public Property VAR_D_TELEFONO_1() As String
        Get
            Return _VAR_D_TELEFONO_1
        End Get
        Set(ByVal value As String)
            _VAR_D_TELEFONO_1 = value
        End Set
    End Property
    Private _VAR_D_TELEFONO_2 As String
    Public Property VAR_D_TELEFONO_2() As String
        Get
            Return _VAR_D_TELEFONO_2
        End Get
        Set(ByVal value As String)
            _VAR_D_TELEFONO_2 = value
        End Set
    End Property
    Private _VAR_D_TELEFONO_3 As String
    Public Property VAR_D_TELEFONO_3() As String
        Get
            Return _VAR_D_TELEFONO_3
        End Get
        Set(ByVal value As String)
            _VAR_D_TELEFONO_3 = value
        End Set
    End Property
    Private _VAR_D_TELEFONO_4 As String
    Public Property VAR_D_TELEFONO_4() As String
        Get
            Return _VAR_D_TELEFONO_4
        End Get
        Set(ByVal value As String)
            _VAR_D_TELEFONO_4 = value
        End Set
    End Property
    Private _VAR_D_TELEFONO_5 As String
    Public Property VAR_D_TELEFONO_5() As String
        Get
            Return _VAR_D_TELEFONO_5
        End Get
        Set(ByVal value As String)
            _VAR_D_TELEFONO_5 = value
        End Set

    End Property
    '***************************************************

    Private _VAR_D_CLIENTE As String
    Public Property VAR_D_CLIENTE() As String
        Get
            Return _VAR_D_CLIENTE
        End Get
        Set(ByVal value As String)
            _VAR_D_CLIENTE = value
        End Set

    End Property
    Private _VAR_D_DOCUMENTO As String
    Public Property VAR_D_DOCUMENTO() As String
        Get
            Return _VAR_D_DOCUMENTO
        End Get
        Set(ByVal value As String)
            _VAR_D_DOCUMENTO = value
        End Set

    End Property
    Private _VAR_D_FEC_MIGRACION As String
    Public Property VAR_D_FEC_MIGRACION() As String
        Get
            Return _VAR_D_FEC_MIGRACION
        End Get
        Set(ByVal value As String)
            _VAR_D_FEC_MIGRACION = value
        End Set

    End Property
    Private _VAR_D_COD_ANTERIOR As String
    Public Property VAR_D_COD_ANTERIOR() As String
        Get
            Return _VAR_D_COD_ANTERIOR
        End Get
        Set(ByVal value As String)
            _VAR_D_COD_ANTERIOR = value
        End Set

    End Property
    Private _VAR_D_NUEVO_CODIGO As String
    Public Property VAR_D_NUEVO_CODIGO() As String
        Get
            Return _VAR_D_NUEVO_CODIGO
        End Get
        Set(ByVal value As String)
            _VAR_D_NUEVO_CODIGO = value
        End Set

    End Property
    Private _VAR_D_CODIGO_PAGO As String
    Public Property VAR_D_CODIGO_PAGO() As String
        Get
            Return _VAR_D_CODIGO_PAGO
        End Get
        Set(ByVal value As String)
            _VAR_D_CODIGO_PAGO = value
        End Set

    End Property
    Private _VAR_D_MONTO_PENDIENTE As String
    Public Property VAR_D_MONTO_PENDIENTE() As String
        Get
            Return _VAR_D_MONTO_PENDIENTE
        End Get
        Set(ByVal value As String)
            _VAR_D_MONTO_PENDIENTE = value
        End Set

    End Property
    Private _VAR_D_DEUDA_COD_ANTIGUO As String
    Public Property VAR_D_DEUDA_COD_ANTIGUO() As String
        Get
            Return _VAR_D_DEUDA_COD_ANTIGUO
        End Get
        Set(ByVal value As String)
            _VAR_D_DEUDA_COD_ANTIGUO = value
        End Set

    End Property
    Private _VAR_D_NOMBRE_PLAN As String
    Public Property VAR_D_NOMBRE_PLAN() As String
        Get
            Return _VAR_D_NOMBRE_PLAN
        End Get
        Set(ByVal value As String)
            _VAR_D_NOMBRE_PLAN = value
        End Set

    End Property
    Private _VAR_D_TELEF_SERVICIO As String
    Public Property VAR_D_TELEF_SERVICIO() As String
        Get
            Return _VAR_D_TELEF_SERVICIO
        End Get
        Set(ByVal value As String)
            _VAR_D_TELEF_SERVICIO = value
        End Set

    End Property
    Private _VAR_D_DEBITO_AUTOMATICO As String
    Public Property VAR_D_DEBITO_AUTOMATICO() As String
        Get
            Return _VAR_D_DEBITO_AUTOMATICO
        End Get
        Set(ByVal value As String)
            _VAR_D_DEBITO_AUTOMATICO = value
        End Set

    End Property
    Private _VAR_D_SERV_TELEF_PLAN_ORIGEN As String
    Public Property VAR_D_SERV_TELEF_PLAN_ORIGEN() As String
        Get
            Return _VAR_D_SERV_TELEF_PLAN_ORIGEN
        End Get
        Set(ByVal value As String)
            _VAR_D_SERV_TELEF_PLAN_ORIGEN = value
        End Set

    End Property
    Private _VAR_D_FEC_ASIGNACION As String
    Public Property VAR_D_FEC_ASIGNACION() As String
        Get
            Return _VAR_D_FEC_ASIGNACION
        End Get
        Set(ByVal value As String)
            _VAR_D_FEC_ASIGNACION = value
        End Set

    End Property
    Private _VAR_D_NOMBRE_CARTERA As String
    Public Property VAR_D_NOMBRE_CARTERA() As String
        Get
            Return _VAR_D_TELEFONO_5
        End Get
        Set(ByVal value As String)
            _VAR_D_NOMBRE_CARTERA = value
        End Set

    End Property
    Private _VAR_D_CC As String
    Public Property VAR_D_CC() As String
        Get
            Return _VAR_D_CC
        End Get
        Set(ByVal value As String)
            _VAR_D_CC = value
        End Set

    End Property
    Private _VAR_D_NRO_SERVICIO As String
    Public Property VAR_D_NRO_SERVICIO() As String
        Get
            Return _VAR_D_NRO_SERVICIO
        End Get
        Set(ByVal value As String)
            _VAR_D_NRO_SERVICIO = value
        End Set

    End Property
    Private _VAR_D_TIPO_DE_SERVICIO As String
    Public Property VAR_D_TIPO_DE_SERVICIO() As String
        Get
            Return _VAR_D_TIPO_DE_SERVICIO
        End Get
        Set(ByVal value As String)
            _VAR_D_TIPO_DE_SERVICIO = value
        End Set

    End Property
    Private _VAR_D_ESCENARIO_CLIENTE_5 As String
    Public Property VAR_D_ESCENARIO_CLIENTE() As String
        Get
            Return _VAR_D_ESCENARIO_CLIENTE_5
        End Get
        Set(ByVal value As String)
            _VAR_D_ESCENARIO_CLIENTE_5 = value
        End Set

    End Property
    Private _VAR_D_REF_MONTO_TOTAL As String
    Public Property VAR_D_REF_MONTO_TOTAL() As String
        Get
            Return _VAR_D_REF_MONTO_TOTAL
        End Get
        Set(ByVal value As String)
            _VAR_D_REF_MONTO_TOTAL = value
        End Set

    End Property
    Private _VAR_D_TENER_EN_CUENTA As String
    Public Property VAR_D_TENER_EN_CUENTA() As String
        Get
            Return _VAR_D_TENER_EN_CUENTA
        End Get
        Set(ByVal value As String)
            _VAR_D_TENER_EN_CUENTA = value
        End Set

    End Property
    Private _VAR_D_VARIOS_RECIBOS As String
    Public Property VAR_D_VARIOS_RECIBOS() As String
        Get
            Return _VAR_D_VARIOS_RECIBOS
        End Get
        Set(ByVal value As String)
            _VAR_D_VARIOS_RECIBOS = value
        End Set

    End Property
    Private _VAR_D_TELEF_REFERENCIA_1 As String
    Public Property VAR_D_TELEF_REFERENCIA_1() As String
        Get
            Return _VAR_D_TELEF_REFERENCIA_1
        End Get
        Set(ByVal value As String)
            _VAR_D_TELEF_REFERENCIA_1 = value
        End Set

    End Property
    Private _VAR_D_TELEF_REFERENCIA_2 As String
    Public Property VAR_D_TELEF_REFERENCIA_2() As String
        Get
            Return _VAR_D_TELEF_REFERENCIA_2
        End Get
        Set(ByVal value As String)
            _VAR_D_TELEF_REFERENCIA_2 = value
        End Set

    End Property
    Private _VAR_D_DOC_IDENTIDAD As String
    Public Property VAR_D_DOC_IDENTIDAD() As String
        Get
            Return _VAR_D_DOC_IDENTIDAD
        End Get
        Set(ByVal value As String)
            _VAR_D_DOC_IDENTIDAD = value
        End Set

    End Property
    Private _VAR_D_CUSTOMER_ID As String
    Public Property VAR_D_CUSTOMER_ID() As String
        Get
            Return _VAR_D_CUSTOMER_ID
        End Get
        Set(ByVal value As String)
            _VAR_D_CUSTOMER_ID = value
        End Set

    End Property
    Private _VAR_D_CUENTA_LARGA As String
    Public Property VAR_D_CUENTA_LARGA() As String
        Get
            Return _VAR_D_CUENTA_LARGA
        End Get
        Set(ByVal value As String)
            _VAR_D_CUENTA_LARGA = value
        End Set

    End Property
    Private _VAR_D_FECHA_EMISION As String
    Public Property VAR_D_FECHA_EMISION() As String
        Get
            Return _VAR_D_FECHA_EMISION
        End Get
        Set(ByVal value As String)
            _VAR_D_FECHA_EMISION = value
        End Set
    End Property
    Private _VAR_D_FECHA_VENCIMIENTO As String
    Public Property VAR_D_FECHA_VENCIMIENTO() As String
        Get
            Return _VAR_D_FECHA_VENCIMIENTO
        End Get
        Set(ByVal value As String)
            _VAR_D_FECHA_VENCIMIENTO = value
        End Set

    End Property
    Private _VAR_D_MONTO_ORIGINAL As String
    Public Property VAR_D_MONTO_ORIGINAL() As String
        Get
            Return _VAR_D_MONTO_ORIGINAL
        End Get
        Set(ByVal value As String)
            _VAR_D_MONTO_ORIGINAL = value
        End Set
    End Property
    Private _VAR_D_ANTIG_DEUDA As String
    Public Property VAR_D_ANTIG_DEUDA() As String
        Get
            Return _VAR_D_ANTIG_DEUDA
        End Get
        Set(ByVal value As String)
            _VAR_D_ANTIG_DEUDA = value
        End Set

    End Property

    Private _VAR_D_DETDOCUMENTO As String
    Public Property VAR_D_DETDOCUMENTO() As String
        Get
            Return _VAR_D_DETDOCUMENTO
        End Get
        Set(ByVal value As String)
            _VAR_D_DETDOCUMENTO = value
        End Set

    End Property

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private _VAR_D_SERVICIO As String
    Public Property VAR_D_SERVICIO() As String
        Get
            Return _VAR_D_SERVICIO
        End Get
        Set(ByVal value As String)
            _VAR_D_SERVICIO = value
        End Set
    End Property
    Private _VAR_D_CODIGO As String
    Public Property VAR_D_CODIGO() As String
        Get
            Return _VAR_D_CODIGO
        End Get
        Set(ByVal value As String)
            _VAR_D_CODIGO = value
        End Set
    End Property
    Private _VAR_D_CODIGO_BSCS As String
    Public Property VAR_D_CODIGO_BSCS() As String
        Get
            Return _VAR_D_CODIGO_BSCS
        End Get
        Set(ByVal value As String)
            _VAR_D_CODIGO_BSCS = value
        End Set
    End Property

    Private _VAR_D_RAZ_SOCIAL As String
    Public Property VAR_D_RAZ_SOCIAL() As String
        Get
            Return _VAR_D_RAZ_SOCIAL
        End Get
        Set(ByVal value As String)
            _VAR_D_RAZ_SOCIAL = value
        End Set
    End Property
    Private _VAR_D_RUC As String
    Public Property VAR_D_RUC() As String
        Get
            Return _VAR_D_RUC
        End Get
Set(ByVal value As String)   
            _VAR_D_RUC = value
        End Set
    End Property
    Private _VAR_D_AGENTE_DNINO As String
    Public Property VAR_D_AGENTE_DNINO() As String
        Get
            Return _VAR_D_AGENTE_DNINO
        End Get
        Set(ByVal value As String)
            _VAR_D_AGENTE_DNINO = value
        End Set
    End Property
    Private _VAR_D_TIPO_CLIENTE As String
    Public Property VAR_D_TIPO_CLIENTE() As String
        Get
            Return _VAR_D_TIPO_CLIENTE
        End Get
        Set(ByVal value As String)
            _VAR_D_TIPO_CLIENTE = value
        End Set
    End Property
    Private _VAR_D_FORMA_PAGO As String
    Public Property VAR_D_FORMA_PAGO() As String
        Get
            Return _VAR_D_FORMA_PAGO
        End Get
        Set(ByVal value As String)
            _VAR_D_FORMA_PAGO = value
        End Set
    End Property
    Private _VAR_D_ESTADO_CUENTA As String
    Public Property VAR_D_ESTADO_CUENTA() As String
        Get
            Return _VAR_D_ESTADO_CUENTA
        End Get
        Set(ByVal value As String)
            _VAR_D_ESTADO_CUENTA = value
        End Set
    End Property
    Private _VAR_D_FEC_ACTIVACION As String
    Public Property VAR_D_FEC_ACTIVACION() As String
        Get
            Return _VAR_D_FEC_ACTIVACION
        End Get
        Set(ByVal value As String)
            _VAR_D_FEC_ACTIVACION = value
        End Set
    End Property
  
    Private _VAR_D_PROVINCIA As String
    Public Property VAR_D_PROVINCIA() As String
        Get
            Return _VAR_D_PROVINCIA
        End Get
        Set(ByVal value As String)
            _VAR_D_PROVINCIA = value
        End Set
    End Property
    Private _VAR_D_DISTRITO As String
    Public Property VAR_D_DISTRITO() As String
        Get
            Return _VAR_D_DISTRITO
        End Get
        Set(ByVal value As String)
            _VAR_D_DISTRITO = value
        End Set
    End Property
    Private _VAR_D_CANT_LINEAS_A As String
    Public Property VAR_D_CANT_LINEAS_A() As String
        Get
            Return _VAR_D_CANT_LINEAS_A
        End Get
        Set(ByVal value As String)
            _VAR_D_CANT_LINEAS_A = value
        End Set
    End Property
    Private _VAR_D_SEGMENTO As String
    Public Property VAR_D_SEGMENTO() As String
        Get
            Return _VAR_D_SEGMENTO
        End Get
        Set(ByVal value As String)
            _VAR_D_SEGMENTO = value
        End Set
    End Property
    Private _VAR_D_TIPO_SEGMENTO As String
    Public Property VAR_D_TIPO_SEGMENTO() As String
        Get
            Return _VAR_D_TIPO_SEGMENTO
        End Get
        Set(ByVal value As String)
            _VAR_D_TIPO_SEGMENTO = value
        End Set
    End Property
    Private _VAR_D_CICLO As String
    Public Property VAR_D_CICLO() As String
        Get
            Return _VAR_D_CICLO
        End Get
        Set(ByVal value As String)
            _VAR_D_CICLO = value
        End Set
    End Property
    Private _VAR_D_NOMBRE_CICLO As String
    Public Property VAR_D_NOMBRE_CICLO() As String
        Get
            Return _VAR_D_NOMBRE_CICLO
        End Get
        Set(ByVal value As String)
            _VAR_D_NOMBRE_CICLO = value
        End Set
    End Property
    Private _VAR_D_SERVICIO_PRESTADO As String
    Public Property VAR_D_SERVICIO_PRESTADO() As String
        Get
            Return _VAR_D_SERVICIO_PRESTADO
        End Get
        Set(ByVal value As String)
            _VAR_D_SERVICIO_PRESTADO = value
        End Set
    End Property
    Private _VAR_D_ESTADO_DOCUMENTO As String
    Public Property VAR_D_ESTADO_DOCUMENTO() As String
        Get
            Return _VAR_D_ESTADO_DOCUMENTO
        End Get
        Set(ByVal value As String)
            _VAR_D_ESTADO_DOCUMENTO = value
        End Set
    End Property
    Private _VAR_D_TIPO_DOCUMENTO As String
    Public Property VAR_D_TIPO_DOCUMENTO() As String
        Get
            Return _VAR_D_TIPO_DOCUMENTO
        End Get
        Set(ByVal value As String)
            _VAR_D_TIPO_DOCUMENTO = value
        End Set
    End Property
    Private _VAR_D_DEBITO As String
    Public Property VAR_D_DEBITO() As String
        Get
            Return _VAR_D_DEBITO
        End Get
        Set(ByVal value As String)
            _VAR_D_DEBITO = value
        End Set
    End Property
    Private _VAR_D_NRO_DOCUMENTO As String
    Public Property VAR_D_NRO_DOCUMENTO() As String
        Get
            Return _VAR_D_NRO_DOCUMENTO
        End Get
        Set(ByVal value As String)
            _VAR_D_NRO_DOCUMENTO = value
        End Set
    End Property
    Private _VAR_D_FEC_EMISION As String
    Public Property VAR_D_FEC_EMISION() As String
        Get
            Return _VAR_D_FEC_EMISION
        End Get
        Set(ByVal value As String)
            _VAR_D_FEC_EMISION = value
        End Set
    End Property
    Private _VAR_D_FEC_VCTO As String
    Public Property VAR_D_FEC_VCTO() As String
        Get
            Return _VAR_D_FEC_VCTO
        End Get
        Set(ByVal value As String)
            _VAR_D_FEC_VCTO = value
        End Set
    End Property
    Private _VAR_D_ANT_CUENTA As String
    Public Property VAR_D_ANT_CUENTA() As String
        Get
            Return _VAR_D_ANT_CUENTA
        End Get
        Set(ByVal value As String)
            _VAR_D_ANT_CUENTA = value
        End Set
    End Property
    Private _VAR_D_ANT_DOC As String
    Public Property VAR_D_ANT_DOC() As String
        Get
            Return _VAR_D_ANT_DOC
        End Get
        Set(ByVal value As String)
            _VAR_D_ANT_DOC = value
        End Set
    End Property
    Private _VAR_D_TRAMO As String
    Public Property VAR_D_TRAMO() As String
        Get
            Return _VAR_D_TRAMO
        End Get
        Set(ByVal value As String)
            _VAR_D_TRAMO = value
        End Set
    End Property
    Private _VAR_D_MONEDA As String
    Public Property VAR_D_MONEDA() As String
        Get
            Return _VAR_D_MONEDA
        End Get
        Set(ByVal value As String)
            _VAR_D_MONEDA = value
        End Set
    End Property
    Private _VAR_D_IMPORTE_FACTURADO As String
    Public Property VAR_D_IMPORTE_FACTURADO() As String
        Get
            Return _VAR_D_IMPORTE_FACTURADO
        End Get
        Set(ByVal value As String)
            _VAR_D_IMPORTE_FACTURADO = value
        End Set
    End Property
    Private _VAR_D_IMPORTE_PENDIENTE As String
    Public Property VAR_D_IMPORTE_PENDIENTE() As String
        Get
            Return _VAR_D_IMPORTE_PENDIENTE
        End Get
        Set(ByVal value As String)
            _VAR_D_IMPORTE_PENDIENTE = value
        End Set
    End Property
    Private _VAR_D_IMPORTE_PENDIENTE_SOLES As String
    Public Property VAR_D_IMPORTE_PENDIENTE_SOLES() As String
        Get
            Return _VAR_D_IMPORTE_PENDIENTE_SOLES
        End Get
        Set(ByVal value As String)
            _VAR_D_IMPORTE_PENDIENTE_SOLES = value
        End Set
    End Property
    Private _VAR_D_MONTO_DISPUTA As String
    Public Property VAR_D_MONTO_DISPUTA() As String
        Get
            Return _VAR_D_MONTO_DISPUTA
        End Get
        Set(ByVal value As String)
            _VAR_D_MONTO_DISPUTA = value
        End Set
    End Property
    Private _VAR_D_GESTOR_COBRANZAS As String
    Public Property VAR_D_GESTOR_COBRANZAS() As String
        Get
            Return _VAR_D_GESTOR_COBRANZAS
        End Get
        Set(ByVal value As String)
            _VAR_D_GESTOR_COBRANZAS = value
        End Set
    End Property
    Private _VAR_D_CARTERA As String
    Public Property VAR_D_CARTERA() As String
        Get
            Return _VAR_D_CARTERA
        End Get
        Set(ByVal value As String)
            _VAR_D_CARTERA = value
        End Set
    End Property
    Private _VAR_D_CANAL As String
    Public Property VAR_D_CANAL() As String
        Get
            Return _VAR_D_CANAL
        End Get
        Set(ByVal value As String)
            _VAR_D_CANAL = value
        End Set
    End Property
    Private _VAR_D_DISTRIBUIDOR As String
    Public Property VAR_D_DISTRIBUIDOR() As String
        Get
            Return _VAR_D_DISTRIBUIDOR
        End Get
        Set(ByVal value As String)
            _VAR_D_DISTRIBUIDOR = value
        End Set
    End Property
    Private _VAR_D_CONSULTOR As String
    Public Property VAR_D_CONSULTOR() As String
        Get
            Return _VAR_D_CONSULTOR
        End Get
        Set(ByVal value As String)
            _VAR_D_CONSULTOR = value
        End Set
    End Property
    Private _VAR_D_SUBCANAL As String
    Public Property VAR_D_SUBCANAL() As String
        Get
            Return _VAR_D_SUBCANAL
        End Get
        Set(ByVal value As String)
            _VAR_D_SUBCANAL = value
        End Set
    End Property
    Private _VAR_D_GERENTE As String
    Public Property VAR_D_GERENTE() As String
        Get
            Return _VAR_D_GERENTE
        End Get
        Set(ByVal value As String)
            _VAR_D_GERENTE = value
        End Set
    End Property
    Private _VAR_D_SUBDIRECCION As String
    Public Property VAR_D_SUBDIRECCION() As String
        Get
            Return _VAR_D_SUBDIRECCION
        End Get
        Set(ByVal value As String)
            _VAR_D_SUBDIRECCION = value
        End Set
    End Property
    Private _VAR_D_JEFE As String
    Public Property VAR_D_JEFE() As String
        Get
            Return _VAR_D_JEFE
        End Get
        Set(ByVal value As String)
            _VAR_D_JEFE = value
        End Set
    End Property
    Private _VAR_D_ASESOR As String
    Public Property VAR_D_ASESOR() As String
        Get
            Return _VAR_D_ASESOR
        End Get
        Set(ByVal value As String)
            _VAR_D_ASESOR = value
        End Set
    End Property
    Private _VAR_D_SUPERVISOR As String
    Public Property VAR_D_SUPERVISOR() As String
        Get
            Return _VAR_D_SUPERVISOR
        End Get
        Set(ByVal value As String)
            _VAR_D_SUPERVISOR = value
        End Set
    End Property
    Private _VAR_D_SECTOR As String
    Public Property VAR_D_SECTOR() As String
        Get
            Return _VAR_D_SECTOR
        End Get
        Set(ByVal value As String)
            _VAR_D_SECTOR = value
        End Set
    End Property
    Private _VAR_D_REGION As String
    Public Property VAR_D_REGION() As String
        Get
            Return _VAR_D_REGION
        End Get
        Set(ByVal value As String)
            _VAR_D_REGION = value
        End Set
    End Property
    Private _VAR_D_ACCOUNT_MANAGER As String
    Public Property VAR_D_ACCOUNT_MANAGER() As String
        Get
            Return _VAR_D_ACCOUNT_MANAGER
        End Get
        Set(ByVal value As String)
            _VAR_D_ACCOUNT_MANAGER = value
        End Set
    End Property
    Private _VAR_D_GRUPO_ECON As String
    Public Property VAR_D_GRUPO_ECON() As String
        Get
            Return _VAR_D_GRUPO_ECON
        End Get
        Set(ByVal value As String)
            _VAR_D_GRUPO_ECON = value
        End Set
    End Property
    Private _VAR_D_CLIENTES_100 As String
    Public Property VAR_D_CLIENTES_100() As String
        Get
            Return _VAR_D_CLIENTES_100
        End Get
        Set(ByVal value As String)
            _VAR_D_CLIENTES_100 = value
        End Set
    End Property
    Private _VAR_D_CARTAS_JUNIO As String
    Public Property VAR_D_CARTAS_JUNIO() As String
        Get
            Return _VAR_D_CARTAS_JUNIO
        End Get
        Set(ByVal value As String)
            _VAR_D_CARTAS_JUNIO = value
        End Set
    End Property

    '********************************
    Private _VAR_D_CODIGO_SAP As String
    Public Property VAR_D_CODIGO_SAP() As String
        Get
            Return _VAR_D_CODIGO_SAP
        End Get
        Set(ByVal value As String)
            _VAR_D_CODIGO_SAP = value
        End Set
    End Property
    Private _VAR_D_CONTACTO As String
    Public Property VAR_D_CONTACTO() As String
        Get
            Return _VAR_D_CONTACTO
        End Get
        Set(ByVal value As String)
            _VAR_D_CONTACTO = value
        End Set
    End Property
    Private _VAR_D_OBSERVCIONES_1 As String
    Public Property VAR_D_OBSERVCIONES_1() As String
        Get
            Return _VAR_D_OBSERVCIONES_1
        End Get
        Set(ByVal value As String)
            _VAR_D_OBSERVCIONES_1 = value
        End Set
    End Property
    Private _VAR_D_OBSERVCIONES_2 As String
    Public Property VAR_D_OBSERVCIONES_2() As String
        Get
            Return _VAR_D_OBSERVCIONES_2
        End Get
        Set(ByVal value As String)
            _VAR_D_OBSERVCIONES_2 = value
        End Set
    End Property
    Private _VAR_D_OBSERVCIONES_3 As String
    Public Property VAR_D_OBSERVCIONES_3() As String
        Get
            Return _VAR_D_OBSERVCIONES_3
        End Get
        Set(ByVal value As String)
            _VAR_D_OBSERVCIONES_3 = value
        End Set
    End Property
    Private _VAR_D_OBSERVCIONES_4 As String
    Public Property VAR_D_OBSERVCIONES_4() As String
        Get
            Return _VAR_D_OBSERVCIONES_4
        End Get
        Set(ByVal value As String)
            _VAR_D_OBSERVCIONES_4 = value
        End Set
    End Property
End Class



