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
        RENOVACIONES_RANKING()
      
    End Sub
    Sub RENOVACIONES_RANKING()
        ' lblMensaje.Text = ""
        Try
            grvReporte.DataSource = Nothing
            grvReporte.DataBind()
            '  imgExportar.Visible = False

            be.inicio = txtInicio.Text
            be.fin = txtFin.Text

            Dim dt As DataTable = da.SP_RENOVACIONES_RANKING(be)
            If dt.Rows.Count > 0 Then
                grvReporte.DataSource = dt
                grvReporte.DataBind()
                '  imgExportar.Visible = True


            Else
                '  lblMensaje.Text = "No existe datos con parametro de busqueda"
            End If
        Catch ex As Exception
            ' lblMensaje.Text = ex.Message
        End Try
    End Sub
End Class
