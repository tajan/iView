Imports HtmlAgilityPack

Public Class ManifestProvider

    Public Const MANIFEST_FILE_VIRTUAL_PATH As String = "~/iViewManifest.config"

    Private Shared _manifest As Manifest
    Public Shared ReadOnly Property Manifest As Manifest
        Get
            Return _manifest
        End Get
    End Property

    Shared Sub New()

        _manifest = New Manifest

        If Not Helper.FileExists(MANIFEST_FILE_VIRTUAL_PATH) Then
            Exit Sub
        End If

        Dim manifestFileContent As String = Helper.GetVirtualFileContent(MANIFEST_FILE_VIRTUAL_PATH)
        Dim manifestNode As HtmlNode = Helper.GetHtmlNode(manifestFileContent)
        Dim manifest = ManifestProvider.Manifest
        Dim debugAttr = manifest.DebugAttributeName

        If manifestNode.ChildNodes(manifest.RootTagName) Is Nothing Then
            Exit Sub
        End If

        If manifestNode.ChildNodes(manifest.RootTagName).Attributes.Contains(debugAttr) Then
            _manifest.Debug = Boolean.Parse(manifestNode.ChildNodes(manifest.RootTagName).Attributes.Item(debugAttr).Value)
        End If

        LoadConfig(_manifest, manifestNode)
        LoadLayout(_manifest, manifestNode)
        LoadControl(_manifest, manifestNode)
        LoadResources(_manifest, manifestNode)

    End Sub


    Private Shared Sub LoadConfig(manifest As Manifest, manifestNode As HtmlNode)
        'todo: amir, implement here to set default values from congif file
    End Sub

    Private Shared Sub LoadLayout(manifest As Manifest, manifestNode As HtmlNode)

        Dim layoutNode As HtmlNode = manifestNode.SelectSingleNode("//" & ManifestProvider.Manifest.LayoutTagName)

        If layoutNode Is Nothing Then
            Exit Sub
        End If

        With manifest

            .Layout.RootPath = layoutNode.GetAttributeValue(manifest.LayoutRootAttributeName, .Layout.RootPath)
            .Layout.ThemeName = layoutNode.GetAttributeValue(manifest.LayoutThemeAttributeName, .Layout.ThemeName)
            .Layout.LayoutPath = layoutNode.GetAttributeValue(manifest.LayoutPathAttributeName, .Layout.LayoutPath)
            .Layout.SourceFile = layoutNode.GetAttributeValue(manifest.LayoutSourceAttributeName, .Layout.SourceFile)

        End With

    End Sub

    Private Shared Sub LoadControl(manifest As Manifest, manifestNode As HtmlNode)

        Dim controlNode As HtmlNode = manifestNode.SelectSingleNode("//" & ManifestProvider.Manifest.LayoutTagName)

        If controlNode Is Nothing Then
            Exit Sub
        End If

        With manifest

            .Control.RootPath = controlNode.GetAttributeValue(manifest.ControlRootAttributeName, .Control.RootPath)
            .Control.ThemeName = controlNode.GetAttributeValue(manifest.ControlThemeAttributeName, .Control.ThemeName)
            .Control.ControlsPath = controlNode.GetAttributeValue(manifest.ControlPathAttributeName, .Control.ControlsPath)

        End With

    End Sub

    Private Shared Sub LoadResources(manifest As Manifest, manifestNode As HtmlNode)

        Dim xpathFilter As String = "//" & manifest.ResourcesTagName & "/" & manifest.ResourceTagName
        Dim resourceNodes As HtmlNodeCollection = manifestNode.SelectNodes(xpathFilter)

        If resourceNodes Is Nothing Then
            Exit Sub
        End If

        For Each resourceNode In resourceNodes

            Dim resource As New Resource
            resource.Name = resourceNode.GetAttributeValue(manifest.ResourceNameAttributeName, resource.Name)
            resource.Type = resourceNode.GetAttributeValue(manifest.ResourceTypeAttributeName, resource.Type)
            resource.Source = resourceNode.GetAttributeValue(manifest.ResourceSourceAttributeName, resource.Source)

            If resource.Name Is Nothing Then
                Throw New Exception("The resource node in manifest file should contains an iv-name attribute.")
            End If

            If resource.Type Is Nothing Then
                Throw New Exception("The resource node in manifest file should contains an iv-type attribute.")
            End If

            If resource.Source Is Nothing Then
                Throw New Exception("The resource node in manifest file should contains an iv-source attribute.")
            End If

            If manifest.Resources.ContainsKey(resource.Name) Then
                Throw New Exception("Multiple resource node in manifest file with the same name:" & resource.Name)
            End If

            manifest.Resources.Add(resource.Name, resource)

        Next

    End Sub

    Public Shared Function GetLayoutVirtualFilePath(layoutNode As HtmlNode, manifest As Manifest) As String

        Dim rootPath As String = layoutNode.GetAttributeValue(manifest.LayoutRootAttributeName, manifest.Layout.RootPath)
        Dim themeName As String = layoutNode.GetAttributeValue(manifest.LayoutThemeAttributeName, manifest.Layout.ThemeName)
        Dim layoutPath As String = layoutNode.GetAttributeValue(manifest.LayoutPathAttributeName, manifest.Layout.LayoutPath)
        Dim sourcePath As String = layoutNode.GetAttributeValue(manifest.LayoutSourceAttributeName, manifest.Layout.SourceFile)

        Return rootPath & "/" & themeName & "/" & layoutPath & "/" & sourcePath

    End Function

    Public Shared Function GetControlFileVirtualPath(controlNode As HtmlNode, manifest As Manifest) As String


        Dim rootPath As String = controlNode.GetAttributeValue(manifest.ControlRootAttributeName, manifest.Control.RootPath)
        Dim themeName As String = controlNode.GetAttributeValue(manifest.ControlThemeAttributeName, manifest.Control.ThemeName)
        Dim controlsPath As String = controlNode.GetAttributeValue(manifest.ControlPathAttributeName, manifest.Control.ControlsPath)
        Dim controlPath As String = controlNode.Name.Replace(":", "\")
        Dim controlFileName As String = controlNode.Attributes(manifest.ControlTypeAttributeName).Value & manifest.ControlFileExtension

        Return rootPath & "/" & themeName & "/" & controlsPath & "/" & controlPath & "/" & controlFileName

    End Function

End Class
