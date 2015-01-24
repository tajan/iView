Imports HtmlAgilityPack

Public Class ManifestHelper

    'todo: implement caching
    Public Shared Function GetManifest() As Manifest

        Dim manifest As New Manifest

        If Not Helper.FileExists(MANIFEST_FILE_VIRTUAL_PATH) Then
            Return manifest
        End If

        Dim manifestFileContent As String = Helper.GetVirtualFileContent(MANIFEST_FILE_VIRTUAL_PATH)
        Dim manifestNode As HtmlNode = Helper.GetHtmlNode(manifestFileContent)

        If manifestNode.ChildNodes("root") Is Nothing Then
            Throw New Exception("root node does not exist in manifest file.")
        End If

        If manifestNode.ChildNodes("root").Attributes.Contains(MANIFEST_ATTRIBUTE_DEBUG) Then
            manifest.Debug = Boolean.Parse(manifestNode.ChildNodes("root").Attributes.Item(MANIFEST_ATTRIBUTE_DEBUG).Value)
        End If

        LoadLayout(manifest, manifestNode)
        LoadControl(manifest, manifestNode)
        LoadResources(manifest, manifestNode)

        Return manifest

    End Function

    Private Shared Sub LoadLayout(manifest As Manifest, manifestNode As HtmlNode)

        Dim layoutNode As HtmlNode = manifestNode.SelectSingleNode(LAYOUT_TAG_XPATH_FILTER)

        If layoutNode Is Nothing Then
            Exit Sub
        End If

        With manifest

            .Layout.RootPath = layoutNode.GetAttributeValue(LAYOUT_TAG_ROOT_ATTRIBUTE, .Layout.RootPath)
            .Layout.ThemeName = layoutNode.GetAttributeValue(LAYOUT_TAG_THEME_ATTRIBUTE, .Layout.ThemeName)
            .Layout.LayoutPath = layoutNode.GetAttributeValue(LAYOUT_TAG_LAYOUT_ATTRIBUTE, .Layout.LayoutPath)
            .Layout.SourceFile = layoutNode.GetAttributeValue(LAYOUT_TAG_SOURCE_ATTRIBUTE, .Layout.SourceFile)

        End With

    End Sub

    Private Shared Sub LoadControl(manifest As Manifest, manifestNode As HtmlNode)

        Dim controlNode As HtmlNode = manifestNode.SelectSingleNode(LAYOUT_TAG_XPATH_FILTER)

        If controlNode Is Nothing Then
            Exit Sub
        End If

        With manifest

            .Control.RootPath = controlNode.GetAttributeValue(CONTROL_TAG_ROOT_ATTRIBUTE, .Control.RootPath)
            .Control.ThemeName = controlNode.GetAttributeValue(CONTROL_TAG_THEME_ATTRIBUTE, .Control.ThemeName)
            .Control.ControlsPath = controlNode.GetAttributeValue(CONTROL_TAG_PATH_ATTRIBUTE, .Control.ControlsPath)

        End With

    End Sub

    Private Shared Sub LoadResources(manifest As Manifest, manifestNode As HtmlNode)

        Dim resourceNodes As HtmlNodeCollection = manifestNode.SelectNodes("//resources/resource")

        If resourceNodes Is Nothing Then
            Exit Sub
        End If

        For Each resourceNode In resourceNodes

            Dim resource As New Resource
            resource.Name = resourceNode.GetAttributeValue("iv-name", resource.Name)
            resource.Type = resourceNode.GetAttributeValue("iv-type", resource.Type)
            resource.Source = resourceNode.GetAttributeValue("iv-source", resource.Source)

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

        Dim rootPath As String = layoutNode.GetAttributeValue(LAYOUT_TAG_ROOT_ATTRIBUTE, manifest.Layout.RootPath)
        Dim themeName As String = layoutNode.GetAttributeValue(LAYOUT_TAG_THEME_ATTRIBUTE, manifest.Layout.ThemeName)
        Dim layoutPath As String = layoutNode.GetAttributeValue(LAYOUT_TAG_LAYOUT_ATTRIBUTE, manifest.Layout.LayoutPath)
        Dim sourcePath As String = layoutNode.GetAttributeValue(LAYOUT_TAG_SOURCE_ATTRIBUTE, manifest.Layout.SourceFile)

        Return rootPath & "/" & themeName & "/" & layoutPath & "/" & sourcePath

    End Function

    Public Shared Function GetControlFileVirtualPath(controlNode As HtmlNode, manifest As Manifest) As String

        Dim rootPath As String = controlNode.GetAttributeValue(CONTROL_TAG_ROOT_ATTRIBUTE, manifest.Control.RootPath)
        Dim themeName As String = controlNode.GetAttributeValue(CONTROL_TAG_THEME_ATTRIBUTE, manifest.Control.ThemeName)
        Dim controlsPath As String = controlNode.GetAttributeValue(CONTROL_TAG_PATH_ATTRIBUTE, manifest.Control.ControlsPath)
        Dim controlPath As String = controlNode.Name
        Dim controlFileName As String = controlNode.Attributes(CONTROL_TAG_TYPE_ATTRIBUTE).Value & CONTROL_FILE_EXTENTION

        Return rootPath & "/" & themeName & "/" & controlsPath & "/" & controlPath & "/" & controlFileName

    End Function

End Class
