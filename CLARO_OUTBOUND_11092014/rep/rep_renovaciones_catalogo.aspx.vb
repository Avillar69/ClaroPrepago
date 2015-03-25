Imports System.Data
Imports System.IO

Partial Class rep_rep_porta
    Inherits System.Web.UI.Page
    Dim da As New DA_claro
    Dim be As New BE_CLARO

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
 
    End Sub

    Protected Sub CBO_MARCA_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBO_MARCA.SelectedIndexChanged
        If CBO_MARCA.SelectedItem.ToString = "ALCATEL" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("Alcatel OT5050 POP S3")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "APPLE" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("Apple IPHONE 4 8GB Remanufacturado")
            CBO_EQUIPO.Items.Add("Apple IPHONE 4S 8GB")
            CBO_EQUIPO.Items.Add("Apple iPhone 5C 8GB")
            CBO_EQUIPO.Items.Add("Apple iPhone 5C 16GB")
            CBO_EQUIPO.Items.Add("Apple iPhone 5S 16GB")
            CBO_EQUIPO.Items.Add("Apple iPhone 5S 32GB")
            CBO_EQUIPO.Items.Add("Apple iPhone 5S 64GB")
            CBO_EQUIPO.Items.Add("Apple iPhone 6 16GB")
            CBO_EQUIPO.Items.Add("Apple iPhone 6 64GB")
            CBO_EQUIPO.Items.Add("Apple iPhone 6 128GB")
            CBO_EQUIPO.Items.Add("Apple iPhone 6 Plus 16GB")
            CBO_EQUIPO.Items.Add("Apple iPhone 6 Plus 64GB")
            CBO_EQUIPO.Items.Add("Apple iPhone 6 Plus 128GB")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "AZUMI" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("Azumi A50C")
            CBO_EQUIPO.Items.Add("AZUMI ARKIA A35S")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "HUAWEI" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("Huawei Ascend G620S")
            CBO_EQUIPO.Items.Add("Huawei Ascend G6")
            CBO_EQUIPO.Items.Add("Huawei Ascend Y330")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "HTC" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("HTC DESIRE 510")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "LANIX" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("Lanix S106")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "LENOVO" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("Lenovo A369i")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "LG" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("LG L20")
            CBO_EQUIPO.Items.Add("LG L40 - D165F")
            CBO_EQUIPO.Items.Add("LG G Flex")
            CBO_EQUIPO.Items.Add("LG G2 D805")
            CBO_EQUIPO.Items.Add("LG G3 D855")
            CBO_EQUIPO.Items.Add("LG G Pro Lite D680")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "M4" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("M4TEL SS1060")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "MOTOROLA" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("Motorola Moto E")
            CBO_EQUIPO.Items.Add("Motorola Moto G LTE")
            CBO_EQUIPO.Items.Add("Motorola Moto X")
            CBO_EQUIPO.Items.Add("Motorola Moto X 2")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "NOKIA" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("Nokia Lumia 530")
            CBO_EQUIPO.Items.Add("Nokia Lumia 625")
            CBO_EQUIPO.Items.Add("Nokia Lumia 635")
            CBO_EQUIPO.Items.Add("Nokia Lumia 735")
            CBO_EQUIPO.Items.Add("Nokia Lumia 830")
            CBO_EQUIPO.Items.Add("Nokia Lumia 1320")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "SAMSUNG" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("Samsung Galaxy A3 A300M")
            CBO_EQUIPO.Items.Add("Samsung Galaxy ACE STYLE G357F")
            CBO_EQUIPO.Items.Add("Samsung Galaxy ACE 3")
            CBO_EQUIPO.Items.Add("SAMSUNG GALAXY ALPHA 32GB - G850M")
            CBO_EQUIPO.Items.Add("Samsung Galaxy Fame Lite - S6790")
            CBO_EQUIPO.Items.Add("Samsung Galaxy Note Neo III - N7505 16GB")
            CBO_EQUIPO.Items.Add("Samsung Galaxy SIII Mini - I8190")
            CBO_EQUIPO.Items.Add("Samsung Galaxy SIII Mini - I8200L")
            CBO_EQUIPO.Items.Add("Samsung Galaxy S IV - I9500")
            CBO_EQUIPO.Items.Add("Samsung Galaxy S IV - I337")
            CBO_EQUIPO.Items.Add("Samsung Galaxy S IV - I9515")
            CBO_EQUIPO.Items.Add("Samsung Galaxy SIV Mini - I9190")
            CBO_EQUIPO.Items.Add("Samsung Galaxy SIV Mini - I9195")
            CBO_EQUIPO.Items.Add("Samsung Galaxy Pocket Neo - S5310")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "SONY" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("Sony Xperia E1")
            CBO_EQUIPO.Items.Add("Sony Xperia E3")
            CBO_EQUIPO.Items.Add("SONY XPERIA M2 AQUA D2406")
            CBO_EQUIPO.Items.Add("SONY XPERIA M2")
            CBO_EQUIPO.Items.Add("SONY XPERIA Z3 D6603")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "VERYKOOL" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("Verykool S354")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        ElseIf CBO_MARCA.SelectedItem.ToString = "ZTE" Then
            CBO_EQUIPO.Items.Clear()
            CBO_EQUIPO.Items.Add("SELECCIONAR")
            CBO_EQUIPO.Items.Add("ZTE BLADE APEX 2")
            CBO_EQUIPO.SelectedIndex = 0
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        Else
            CBO_EQUIPO.Items.Clear()
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""
        End If
    End Sub

    Protected Sub CBO_EQUIPO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBO_EQUIPO.SelectedIndexChanged
        If CBO_EQUIPO.SelectedItem.ToString = "Alcatel OT5050 POP S3" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "iOS "
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4S 8GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 8GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 16GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 16GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 32GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 64GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 16GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 64GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 128GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 16GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 64GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 128GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "iOS"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Azumi A50C" Then
            TXT_CAMARA.Text = "12 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "AZUMI ARKIA A35S" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G620S" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G6" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend Y330" Then
            TXT_CAMARA.Text = "3 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "HTC DESIRE 510" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Lanix S106" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Lenovo A369i" Then
            TXT_CAMARA.Text = "2 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "LG L20" Then
            TXT_CAMARA.Text = "2 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "LG L40 - D165F" Then
            TXT_CAMARA.Text = "3.15MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "LG G Flex" Then
            TXT_CAMARA.Text = "13 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "LG G2 D805" Then
            TXT_CAMARA.Text = "13MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3.5G+"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "LG G3 D855" Then
            TXT_CAMARA.Text = "13 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "LG G Pro Lite D680" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3.5G+"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "M4TEL SS1060" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3.5G+"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto E" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3.5G+"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto G LTE" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X" Then
            TXT_CAMARA.Text = "10 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X 2" Then
            TXT_CAMARA.Text = "13 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 530" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Windows"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 625" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Windows"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 635" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Windows"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 735" Then
            TXT_CAMARA.Text = "6.7 MP"
            TXT_SO.Text = "Windows"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 830" Then
            TXT_CAMARA.Text = "10 MP"
            TXT_SO.Text = "Windows"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 1320" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Windows"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy A3 A300M" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE STYLE G357F" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE 3" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "SAMSUNG GALAXY ALPHA 32GB - G850M" Then
            TXT_CAMARA.Text = "12 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Fame Lite - S6790" Then
            TXT_CAMARA.Text = "3 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3.5G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Note Neo III - N7505 16GB" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8190" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3.5G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8200L" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3.5G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9500" Then
            TXT_CAMARA.Text = "13 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3.5G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I337" Then
            TXT_CAMARA.Text = "13 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9515" Then
            TXT_CAMARA.Text = "13 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9190" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3.5G+"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9195" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Pocket Neo - S5310" Then
            TXT_CAMARA.Text = "2 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E1" Then
            TXT_CAMARA.Text = "3.15 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3.5G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E3" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2 AQUA D2406" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2" Then
            TXT_CAMARA.Text = "8 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA Z3 D6603" Then
            TXT_CAMARA.Text = "20.7 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "Verykool S354" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "3G"

        ElseIf CBO_EQUIPO.SelectedItem.ToString = "ZTE BLADE APEX 2" Then
            TXT_CAMARA.Text = "5 MP"
            TXT_SO.Text = "Android"
            TXT_TECNOLOGIA.Text = "4G"

        Else
            TXT_CAMARA.Text = ""
            TXT_SO.Text = ""
            TXT_TECNOLOGIA.Text = ""

        End If

    End Sub

    Protected Sub CBO_PLAN_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBO_PLAN.SelectedIndexChanged

        If CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Alcatel OT5050 POP S3" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "649"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4S 8GB" Then
            TXT_PRECIO.Text = "1059"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 8GB" Then
            TXT_PRECIO.Text = "1579"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 16GB" Then
            TXT_PRECIO.Text = "1939"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 16GB" Then
            TXT_PRECIO.Text = "2299"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 32GB" Then
            TXT_PRECIO.Text = "2189"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 64GB" Then
            TXT_PRECIO.Text = "3029"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 16GB" Then
            TXT_PRECIO.Text = "2299"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 64GB" Then
            TXT_PRECIO.Text = "2669"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 128GB" Then
            TXT_PRECIO.Text = "3029"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 16GB" Then
            TXT_PRECIO.Text = "2669"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 64GB" Then
            TXT_PRECIO.Text = "3029"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 128GB" Then
            TXT_PRECIO.Text = "3399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Azumi A50C" Then
            TXT_PRECIO.Text = "299"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "AZUMI ARKIA A35S" Then
            TXT_PRECIO.Text = "39"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G620S" Then
            TXT_PRECIO.Text = "599"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G6" Then
            TXT_PRECIO.Text = "729"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend Y330" Then
            TXT_PRECIO.Text = "79"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "HTC DESIRE 510" Then
            TXT_PRECIO.Text = "449"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Lanix S106" Then
            TXT_PRECIO.Text = "39"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Lenovo A369i" Then
            TXT_PRECIO.Text = "39"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "LG L20" Then
            TXT_PRECIO.Text = "89"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "LG L40 - D165F" Then
            TXT_PRECIO.Text = "179"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Flex" Then
            TXT_PRECIO.Text = "2029"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "LG G2 D805" Then
            TXT_PRECIO.Text = "1299"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "LG G3 D855" Then
            TXT_PRECIO.Text = "1969"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Pro Lite D680" Then
            TXT_PRECIO.Text = "649"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "M4TEL SS1060" Then
            TXT_PRECIO.Text = "249"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto E" Then
            TXT_PRECIO.Text = "299"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto G LTE" Then
            TXT_PRECIO.Text = "579"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X" Then
            TXT_PRECIO.Text = "1109"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X 2" Then
            TXT_PRECIO.Text = "1899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 530" Then
            TXT_PRECIO.Text = "259"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 625" Then
            TXT_PRECIO.Text = "669"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 635" Then
            TXT_PRECIO.Text = "499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 735" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 830" Then
            TXT_PRECIO.Text = "1249"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 1320" Then
            TXT_PRECIO.Text = "1049"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy A3 A300M" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE STYLE G357F" Then
            TXT_PRECIO.Text = "479"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE 3" Then
            TXT_PRECIO.Text = "589"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "SAMSUNG GALAXY ALPHA 32GB - G850M" Then
            TXT_PRECIO.Text = "1899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Fame Lite - S6790" Then
            TXT_PRECIO.Text = "249"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Note Neo III - N7505 16GB" Then
            TXT_PRECIO.Text = "1839"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8190" Then
            TXT_PRECIO.Text = "439"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8200L" Then
            TXT_PRECIO.Text = "439"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9500" Then
            TXT_PRECIO.Text = "1469"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I337" Then
            TXT_PRECIO.Text = "1469"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9515" Then
            TXT_PRECIO.Text = "1469"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9190" Then
            TXT_PRECIO.Text = "859"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9195" Then
            TXT_PRECIO.Text = "859"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Pocket Neo - S5310" Then
            TXT_PRECIO.Text = "99"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E1" Then
            TXT_PRECIO.Text = "229"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E3" Then
            TXT_PRECIO.Text = "549"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2 AQUA D2406" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2" Then
            TXT_PRECIO.Text = "749"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA Z3 D6603" Then
            TXT_PRECIO.Text = "2179"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "Verykool S354" Then
            TXT_PRECIO.Text = "39"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo1 [30-40]" And CBO_EQUIPO.SelectedItem.ToString = "ZTE BLADE APEX 2" Then
            TXT_PRECIO.Text = "349"


        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "319"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4S 8GB" Then
            TXT_PRECIO.Text = "939"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 8GB" Then
            TXT_PRECIO.Text = "1449"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 16GB" Then
            TXT_PRECIO.Text = "1819"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 16GB" Then
            TXT_PRECIO.Text = "2179"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 32GB" Then
            TXT_PRECIO.Text = "2119"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 64GB" Then
            TXT_PRECIO.Text = "2909"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 16GB" Then
            TXT_PRECIO.Text = "2179"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 64GB" Then
            TXT_PRECIO.Text = "2539"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 128GB" Then
            TXT_PRECIO.Text = "2909"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 16GB" Then
            TXT_PRECIO.Text = "2539"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 64GB" Then
            TXT_PRECIO.Text = "2909"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 128GB" Then
            TXT_PRECIO.Text = "3269"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Azumi A50C" Then
            TXT_PRECIO.Text = "219"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "AZUMI ARKIA A35S" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G620S" Then
            TXT_PRECIO.Text = "499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G6" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend Y330" Then
            TXT_PRECIO.Text = "19"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "HTC DESIRE 510" Then
            TXT_PRECIO.Text = "349"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Lanix S106" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Lenovo A369i" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "LG L20" Then
            TXT_PRECIO.Text = "39"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "LG L40 - D165F" Then
            TXT_PRECIO.Text = "39"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Flex" Then
            TXT_PRECIO.Text = "1909"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "LG G2 D805" Then
            TXT_PRECIO.Text = "1199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "LG G3 D855" Then
            TXT_PRECIO.Text = "1929"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Pro Lite D680" Then
            TXT_PRECIO.Text = "529"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "M4TEL SS1060" Then
            TXT_PRECIO.Text = "169"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto E" Then
            TXT_PRECIO.Text = "249"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto G LTE" Then
            TXT_PRECIO.Text = "459"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X" Then
            TXT_PRECIO.Text = "989"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X 2" Then
            TXT_PRECIO.Text = "1699"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 530" Then
            TXT_PRECIO.Text = "39"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 625" Then
            TXT_PRECIO.Text = "549"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 635" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 735" Then
            TXT_PRECIO.Text = "849"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 830" Then
            TXT_PRECIO.Text = "1199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 1320" Then
            TXT_PRECIO.Text = "1029"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy A3 A300M" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE STYLE G357F" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE 3" Then
            TXT_PRECIO.Text = "529"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "SAMSUNG GALAXY ALPHA 32GB - G850M" Then
            TXT_PRECIO.Text = "1799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Fame Lite - S6790" Then
            TXT_PRECIO.Text = "59"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Note Neo III - N7505 16GB" Then
            TXT_PRECIO.Text = "1719"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8190" Then
            TXT_PRECIO.Text = "319"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8200L" Then
            TXT_PRECIO.Text = "319"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9500" Then
            TXT_PRECIO.Text = "1349"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I337" Then
            TXT_PRECIO.Text = "1349"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9515" Then
            TXT_PRECIO.Text = "1349"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9190" Then
            TXT_PRECIO.Text = "759"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9195" Then
            TXT_PRECIO.Text = "759"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Pocket Neo - S5310" Then
            TXT_PRECIO.Text = "39"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E1" Then
            TXT_PRECIO.Text = "199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E3" Then
            TXT_PRECIO.Text = "499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2 AQUA D2406" Then
            TXT_PRECIO.Text = "849"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2" Then
            TXT_PRECIO.Text = "679"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA Z3 D6603" Then
            TXT_PRECIO.Text = "2119"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "Verykool S354" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo2 [55-65]" And CBO_EQUIPO.SelectedItem.ToString = "ZTE BLADE APEX 2" Then
            TXT_PRECIO.Text = "269"


        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "49"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4S 8GB" Then
            TXT_PRECIO.Text = "909"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 8GB" Then
            TXT_PRECIO.Text = "1299"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 16GB" Then
            TXT_PRECIO.Text = "1419"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 16GB" Then
            TXT_PRECIO.Text = "1769"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 32GB" Then
            TXT_PRECIO.Text = "2059"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 64GB" Then
            TXT_PRECIO.Text = "2879"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 16GB" Then
            TXT_PRECIO.Text = "2149"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 64GB" Then
            TXT_PRECIO.Text = "2509"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 128GB" Then
            TXT_PRECIO.Text = "2879"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 16GB" Then
            TXT_PRECIO.Text = "2509"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 64GB" Then
            TXT_PRECIO.Text = "2879"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 128GB" Then
            TXT_PRECIO.Text = "3229"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Azumi A50C" Then
            TXT_PRECIO.Text = "99"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "AZUMI ARKIA A35S" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G620S" Then
            TXT_PRECIO.Text = "249"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G6" Then
            TXT_PRECIO.Text = "209"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend Y330" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "HTC DESIRE 510" Then
            TXT_PRECIO.Text = "149"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Lanix S106" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Lenovo A369i" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "LG L20" Then
            TXT_PRECIO.Text = "19"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "LG L40 - D165F" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Flex" Then
            TXT_PRECIO.Text = "1749"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "LG G2 D805" Then
            TXT_PRECIO.Text = "789"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "LG G3 D855" Then
            TXT_PRECIO.Text = "1859"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Pro Lite D680" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "M4TEL SS1060" Then
            TXT_PRECIO.Text = "19"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto E" Then
            TXT_PRECIO.Text = "69"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto G LTE" Then
            TXT_PRECIO.Text = "249"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X" Then
            TXT_PRECIO.Text = "959"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X 2" Then
            TXT_PRECIO.Text = "1599"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 530" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 625" Then
            TXT_PRECIO.Text = "299"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 635" Then
            TXT_PRECIO.Text = "199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 735" Then
            TXT_PRECIO.Text = "649"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 830" Then
            TXT_PRECIO.Text = "1149"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 1320" Then
            TXT_PRECIO.Text = "669"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy A3 A300M" Then
            TXT_PRECIO.Text = "749"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE STYLE G357F" Then
            TXT_PRECIO.Text = "209"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE 3" Then
            TXT_PRECIO.Text = "249"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "SAMSUNG GALAXY ALPHA 32GB - G850M" Then
            TXT_PRECIO.Text = "1749"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Fame Lite - S6790" Then
            TXT_PRECIO.Text = "19"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Note Neo III - N7505 16GB" Then
            TXT_PRECIO.Text = "1599"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8190" Then
            TXT_PRECIO.Text = "169"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8200L" Then
            TXT_PRECIO.Text = "169"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9500" Then
            TXT_PRECIO.Text = "1309"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I337" Then
            TXT_PRECIO.Text = "1309"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9515" Then
            TXT_PRECIO.Text = "1309"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9190" Then
            TXT_PRECIO.Text = "699"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9195" Then
            TXT_PRECIO.Text = "699"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Pocket Neo - S5310" Then
            TXT_PRECIO.Text = "19"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E1" Then
            TXT_PRECIO.Text = "49"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E3" Then
            TXT_PRECIO.Text = "199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2 AQUA D2406" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2" Then
            TXT_PRECIO.Text = "499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA Z3 D6603" Then
            TXT_PRECIO.Text = "1999"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "Verykool S354" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo3 [69-85]" And CBO_EQUIPO.SelectedItem.ToString = "ZTE BLADE APEX 2" Then
            TXT_PRECIO.Text = "9"



        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4S 8GB" Then
            TXT_PRECIO.Text = "449"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 8GB" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 16GB" Then
            TXT_PRECIO.Text = "1149"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 16GB" Then
            TXT_PRECIO.Text = "1299"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 32GB" Then
            TXT_PRECIO.Text = "1599"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 64GB" Then
            TXT_PRECIO.Text = "1899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 16GB" Then
            TXT_PRECIO.Text = "1449"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 64GB" Then
            TXT_PRECIO.Text = "1799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 128GB" Then
            TXT_PRECIO.Text = "2149"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 16GB" Then
            TXT_PRECIO.Text = "1799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 64GB" Then
            TXT_PRECIO.Text = "2149"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 128GB" Then
            TXT_PRECIO.Text = "2499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Azumi A50C" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "AZUMI ARKIA A35S" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G620S" Then
            TXT_PRECIO.Text = "179"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G6" Then
            TXT_PRECIO.Text = "99"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend Y330" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "HTC DESIRE 510" Then
            TXT_PRECIO.Text = "99"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Lanix S106" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Lenovo A369i" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "LG L20" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "LG L40 - D165F" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Flex" Then
            TXT_PRECIO.Text = "999"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "LG G2 D805" Then
            TXT_PRECIO.Text = "649"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "LG G3 D855" Then
            TXT_PRECIO.Text = "1449"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Pro Lite D680" Then
            TXT_PRECIO.Text = "329"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "M4TEL SS1060" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto E" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto G LTE" Then
            TXT_PRECIO.Text = "79"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X" Then
            TXT_PRECIO.Text = "599"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X 2" Then
            TXT_PRECIO.Text = "1399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 530" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 625" Then
            TXT_PRECIO.Text = "49"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 635" Then
            TXT_PRECIO.Text = "59"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 735" Then
            TXT_PRECIO.Text = "349"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 830" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 1320" Then
            TXT_PRECIO.Text = "499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy A3 A300M" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE STYLE G357F" Then
            TXT_PRECIO.Text = "99"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE 3" Then
            TXT_PRECIO.Text = "99"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "SAMSUNG GALAXY ALPHA 32GB - G850M" Then
            TXT_PRECIO.Text = "1399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Fame Lite - S6790" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Note Neo III - N7505 16GB" Then
            TXT_PRECIO.Text = "999"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8190" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8200L" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9500" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I337" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9515" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9190" Then
            TXT_PRECIO.Text = "199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9195" Then
            TXT_PRECIO.Text = "199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Pocket Neo - S5310" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E1" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E3" Then
            TXT_PRECIO.Text = "49"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2 AQUA D2406" Then
            TXT_PRECIO.Text = "549"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2" Then
            TXT_PRECIO.Text = "199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA Z3 D6603" Then
            TXT_PRECIO.Text = "1749"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "Verykool S354" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo4 [100-125]" And CBO_EQUIPO.SelectedItem.ToString = "ZTE BLADE APEX 2" Then
            TXT_PRECIO.Text = "9"


        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4S 8GB" Then
            TXT_PRECIO.Text = "379"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 8GB" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 16GB" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 16GB" Then
            TXT_PRECIO.Text = "999"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 32GB" Then
            TXT_PRECIO.Text = "1399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 64GB" Then
            TXT_PRECIO.Text = "1799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 16GB" Then
            TXT_PRECIO.Text = "1199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 64GB" Then
            TXT_PRECIO.Text = "1499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 128GB" Then
            TXT_PRECIO.Text = "1799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 16GB" Then
            TXT_PRECIO.Text = "1499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 64GB" Then
            TXT_PRECIO.Text = "1799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 128GB" Then
            TXT_PRECIO.Text = "2099"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Azumi A50C" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "AZUMI ARKIA A35S" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G620S" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G6" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend Y330" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "HTC DESIRE 510" Then
            TXT_PRECIO.Text = "79"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Lanix S106" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Lenovo A369i" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "LG L20" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "LG L40 - D165F" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Flex" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "LG G2 D805" Then
            TXT_PRECIO.Text = "549"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "LG G3 D855" Then
            TXT_PRECIO.Text = "1099"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Pro Lite D680" Then
            TXT_PRECIO.Text = "169"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "M4TEL SS1060" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto E" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto G LTE" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X" Then
            TXT_PRECIO.Text = "499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X 2" Then
            TXT_PRECIO.Text = "1199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 530" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 625" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 635" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 735" Then
            TXT_PRECIO.Text = "199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 830" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 1320" Then
            TXT_PRECIO.Text = "299"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy A3 A300M" Then
            TXT_PRECIO.Text = "299"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE STYLE G357F" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE 3" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "SAMSUNG GALAXY ALPHA 32GB - G850M" Then
            TXT_PRECIO.Text = "999"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Fame Lite - S6790" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Note Neo III - N7505 16GB" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8190" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8200L" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9500" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I337" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9515" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9190" Then
            TXT_PRECIO.Text = "99"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9195" Then
            TXT_PRECIO.Text = "99"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Pocket Neo - S5310" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E1" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E3" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2 AQUA D2406" Then
            TXT_PRECIO.Text = "449"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2" Then
            TXT_PRECIO.Text = "139"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA Z3 D6603" Then
            TXT_PRECIO.Text = "1499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "Verykool S354" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo5 [130-155]" And CBO_EQUIPO.SelectedItem.ToString = "ZTE BLADE APEX 2" Then
            TXT_PRECIO.Text = "9"


        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4S 8GB" Then
            TXT_PRECIO.Text = "249"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 8GB" Then
            TXT_PRECIO.Text = "699"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 16GB" Then
            TXT_PRECIO.Text = "699"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 16GB" Then
            TXT_PRECIO.Text = "879"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 32GB" Then
            TXT_PRECIO.Text = "669"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 64GB" Then
            TXT_PRECIO.Text = "1489"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 16GB" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 64GB" Then
            TXT_PRECIO.Text = "1199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 128GB" Then
            TXT_PRECIO.Text = "1499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 16GB" Then
            TXT_PRECIO.Text = "1199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 64GB" Then
            TXT_PRECIO.Text = "1499"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 128GB" Then
            TXT_PRECIO.Text = "1799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Azumi A50C" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "AZUMI ARKIA A35S" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G620S" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G6" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend Y330" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "HTC DESIRE 510" Then
            TXT_PRECIO.Text = "49"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Lanix S106" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Lenovo A369i" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "LG L20" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "LG L40 - D165F" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Flex" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "LG G2 D805" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "LG G3 D855" Then
            TXT_PRECIO.Text = "949"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Pro Lite D680" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "M4TEL SS1060" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto E" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto G LTE" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X 2" Then
            TXT_PRECIO.Text = "999"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 530" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 625" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 635" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 735" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 830" Then
            TXT_PRECIO.Text = "699"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 1320" Then
            TXT_PRECIO.Text = "199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy A3 A300M" Then
            TXT_PRECIO.Text = "199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE STYLE G357F" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE 3" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "SAMSUNG GALAXY ALPHA 32GB - G850M" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Fame Lite - S6790" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Note Neo III - N7505 16GB" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8190" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8200L" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9500" Then
            TXT_PRECIO.Text = "599"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I337" Then
            TXT_PRECIO.Text = "599"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9515" Then
            TXT_PRECIO.Text = "599"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9190" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9195" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Pocket Neo - S5310" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E1" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E3" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2 AQUA D2406" Then
            TXT_PRECIO.Text = "349"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2" Then
            TXT_PRECIO.Text = "1"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA Z3 D6603" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "Verykool S354" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo6 [175-230]" And CBO_EQUIPO.SelectedItem.ToString = "ZTE BLADE APEX 2" Then
            TXT_PRECIO.Text = "9"


        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4 8GB Remanufacturado" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple IPHONE 4S 8GB" Then
            TXT_PRECIO.Text = "249"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 8GB" Then
            TXT_PRECIO.Text = "699"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5C 16GB" Then
            TXT_PRECIO.Text = "699"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 16GB" Then
            TXT_PRECIO.Text = "879"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 32GB" Then
            TXT_PRECIO.Text = "669"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 5S 64GB" Then
            TXT_PRECIO.Text = "1489"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 16GB" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 64GB" Then
            TXT_PRECIO.Text = "699"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 128GB" Then
            TXT_PRECIO.Text = "999"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 16GB" Then
            TXT_PRECIO.Text = "699"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 64GB" Then
            TXT_PRECIO.Text = "999"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Apple iPhone 6 Plus 128GB" Then
            TXT_PRECIO.Text = "1299"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Azumi A50C" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "AZUMI ARKIA A35S" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G620S" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend G6" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Huawei Ascend Y330" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "HTC DESIRE 510" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Lanix S106" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Lenovo A369i" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "LG L20" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "LG L40 - D165F" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Flex" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "LG G2 D805" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "LG G3 D855" Then
            TXT_PRECIO.Text = "949"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "LG G Pro Lite D680" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "M4TEL SS1060" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto E" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto G LTE" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Motorola Moto X 2" Then
            TXT_PRECIO.Text = "399"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 530" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 625" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 635" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 735" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 830" Then
            TXT_PRECIO.Text = "699"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Nokia Lumia 1320" Then
            TXT_PRECIO.Text = "199"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy A3 A300M" Then
            TXT_PRECIO.Text = "99"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE STYLE G357F" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy ACE 3" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "SAMSUNG GALAXY ALPHA 32GB - G850M" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Fame Lite - S6790" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Note Neo III - N7505 16GB" Then
            TXT_PRECIO.Text = "799"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8190" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIII Mini - I8200L" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9500" Then
            TXT_PRECIO.Text = "599"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I337" Then
            TXT_PRECIO.Text = "599"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy S IV - I9515" Then
            TXT_PRECIO.Text = "599"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9190" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy SIV Mini - I9195" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Samsung Galaxy Pocket Neo - S5310" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E1" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Sony Xperia E3" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2 AQUA D2406" Then
            TXT_PRECIO.Text = "349"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA M2" Then
            TXT_PRECIO.Text = "1"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "SONY XPERIA Z3 D6603" Then
            TXT_PRECIO.Text = "899"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "Verykool S354" Then
            TXT_PRECIO.Text = "9"

        ElseIf CBO_PLAN.SelectedItem.ToString = "Grupo7 [255-320]" And CBO_EQUIPO.SelectedItem.ToString = "ZTE BLADE APEX 2" Then
            TXT_PRECIO.Text = "9"

        Else
            TXT_PRECIO.Text = ""

        End If
    End Sub
End Class
