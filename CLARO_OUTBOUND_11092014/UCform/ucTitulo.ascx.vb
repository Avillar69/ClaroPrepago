
Partial Class UCform_ucTitulo
    Inherits System.Web.UI.UserControl

   
    WriteOnly Property Titulo() As String
        Set(ByVal value As String)
            lblTitulo.Text = value
        End Set
    End Property

    WriteOnly Property imagen() As String
        Set(ByVal value As String)
            imgLogo.ImageUrl = value
        End Set
    End Property
End Class
