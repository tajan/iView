Public Class Manifest

    Public Property Layout As Layout
    Public Property Control As Control
    Public Property Resources As New Dictionary(Of String, Resource)
    Public Property Debug As Boolean

    Public Property ViewFolderPath As String = "/iView/"
    Public Property AttributePrefix As String = "iv-"

    Public Property LayoutTagName As String = "layout"
    Public Property RootTagName As String = "root"

    Public Property ControlContentTagName As String = "iv-content"
    Public Property ResourceTypeAttributeName As String = "iv-type"
    Public Property ResourceNameAttributeName As String = "iv-name"
    Public Property ResourceSourceAttributeName As String = "iv-source"
    Public Property ResourceTagName As String = "resource"
    Public Property ResourcesTagName As String = "resources"
    Public Property DebugAttributeName As String = "debug"


    Public Property ControlFileExtension As String = ".html"
    Public Property ControlAttributePrefix As String = "$"
    Public Property ControlTypeAttributeName As String = "iv-type"
    Public Property ControlPathAttributeName As String = "iv-controls"
    Public Property ControlThemeAttributeName As String = "iv-theme"
    Public Property ControlRootAttributeName As String = "iv-root"
    Public Property ControlTagName As String = "control"

    Public Property IncludeTagName As String = "include"
    Public Property IncludeSourceAttributeName As String = "iv-source"


    Public Property LayoutRootAttributeName As String = "iv-root"
    Public Property LayoutThemeAttributeName As String = "iv-theme"
    Public Property LayoutPathAttributeName As String = "iv-layout"
    Public Property LayoutSourceAttributeName As String = "iv-source"
    Public Property LayoutExcludeRemovalAttributeName As String = "iv-exclude"

    Public Sub New()
        Me.Layout = New Layout
        Me.Control = New Control
    End Sub


End Class
