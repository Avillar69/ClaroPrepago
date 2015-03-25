
Partial Class DynamicData_FieldTemplates_UCTitulo1
    Inherits System.Web.UI.UserControl
    WriteOnly Property Titulo() As String
        Set(ByVal value As String)
            lblTitulo.Text = value
        End Set
    End Property
End Class
