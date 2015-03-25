Imports System.Data
Imports System.IO

Partial Class rep_rep_porta
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtInicio.Text = Now.ToString("yyyy-MM-dd")
            txtFin.Text = Now.ToString("yyyy-MM-dd")
        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        grvReporte.DataSource = Nothing
        grvReporte.DataBind()
        lblMsg.Text = ""
        Try
            be.inicio = txtInicio.Text
            be.fin = txtFin.Text
            be.telefono = txtTelefono.Text.Trim

            Dim dtHistorial As DataTable = da.SP_CONSULTA_CLARO_CANCELACIONES_X_TELEFONO(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("RUC DNI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PLAN TARIFARIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("FECHA EXPIRACION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("MOTIVO CANCELCION", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CICLO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("SEGMENTO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("NOMBRES", Type.GetType("System.String"))

                Dim dtScripting As DataTable = da.SP_LISTAR_CANCELACIONES()

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("RUC DNI") = dtScripting.Rows(j)("RUC DNI").ToString
                            dtHistorial.Rows(i)("PLAN TARIFARIO") = dtScripting.Rows(j)("PLAN TARIFARIO").ToString
                            dtHistorial.Rows(i)("TIPO CLIENTE") = dtScripting.Rows(j)("TIPO CLIENTE").ToString
                            dtHistorial.Rows(i)("FECHA EXPIRACION") = dtScripting.Rows(j)("FECHA EXPIRACION").ToString
                            dtHistorial.Rows(i)("MOTIVO CANCELCION") = dtScripting.Rows(j)("MOTIVO CANCELCION").ToString
                            dtHistorial.Rows(i)("CICLO") = dtScripting.Rows(j)("CICLO").ToString
                            dtHistorial.Rows(i)("SEGMENTO") = dtScripting.Rows(j)("SEGMENTO").ToString

                        End If
                    Next
                Next
                grvReporte.DataSource = dtHistorial
                Session("tablaExportar") = dtHistorial
                grvReporte.DataBind()
                lblMsg.Text = "Cantidad de registros encontrados: " & dtHistorial.Rows.Count
            Else
                lblMsg.Text = "No hay datos con parametro de busqueda"

            End If
        Catch ex As Exception
            lblMsg.Text = "btnBuscar " & ex.Message
        End Try
    End Sub

End Class
