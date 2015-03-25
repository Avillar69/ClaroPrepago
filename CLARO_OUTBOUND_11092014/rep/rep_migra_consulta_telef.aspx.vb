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

            Dim dtHistorial As DataTable = da.SP_CONSULTA_CLARO_MIGRACIONES_X_TELEFONO(be)
            If dtHistorial.Rows.Count > 0 Then

                dtHistorial.Columns.Add("NOMBRES COMPLETOS", Type.GetType("System.String"))
                dtHistorial.Columns.Add("RUC DNI", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TELEFONO BASE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("PLAN TARIFARIO", Type.GetType("System.String"))
                dtHistorial.Columns.Add("TIPO CLIENTE", Type.GetType("System.String"))
                dtHistorial.Columns.Add("CICLO", Type.GetType("System.String"))

                Dim dtScripting As DataTable = da.SP_LISTAR_MIGRACIONES()

                For i = 0 To dtHistorial.Rows.Count - 1
                    Dim IDhIS As String = dtHistorial.Rows(i)("ID").ToString.Trim

                    For j = 0 To dtScripting.Rows.Count - 1
                        Dim IDScrip As String = dtScripting.Rows(j)("ID").ToString.Trim

                        If IDScrip = IDhIS Then

                            dtHistorial.Rows(i)("NOMBRES COMPLETOS") = dtScripting.Rows(j)("NOMBRES COMPLETOS").ToString
                            dtHistorial.Rows(i)("RUC DNI") = dtScripting.Rows(j)("RUC DNI").ToString
                            dtHistorial.Rows(i)("TELEFONO BASE") = dtScripting.Rows(j)("TELEFONO BASE").ToString
                            dtHistorial.Rows(i)("PLAN TARIFARIO") = dtScripting.Rows(j)("PLAN TARIFARIO").ToString
                            dtHistorial.Rows(i)("TIPO CLIENTE") = dtScripting.Rows(j)("TIPO CLIENTE").ToString
                            dtHistorial.Rows(i)("CICLO") = dtScripting.Rows(j)("CICLO").ToString

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
