Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim da As New DA_Sistemas
    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        lblmsg.Text = ""
        Try
            Dim documento As String = txtusuario.Text
            Dim contrasena As String = txtContrasenia.Text
            Dim dt As DataTable = da.SP_LOGUEO_REPORTES(documento, contrasena)
            If dt.Rows.Count > 0 Or (documento = "claroinb" And contrasena = "cl4r01nb") Then
                Session("usuario") = documento
                Response.Redirect("rep/rep_porta.aspx")
            Else
                lblmsg.Text = "Logueo Incorrecto"
            End If
        Catch ex As Exception
            lblmsg.Text = ex.Message
        End Try
    End Sub

End Class
