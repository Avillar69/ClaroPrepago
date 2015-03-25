
Partial Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuario") = "" Then
                Response.Redirect("../Default.aspx")
            End If
        End If
    End Sub
    Sub NavigationMenu_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs)

    End Sub
End Class

