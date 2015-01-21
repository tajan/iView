﻿Imports HtmlAgilityPack

Public Class ManifestHelper

    Public Shared Function GetManifest() As Manifest

        Dim manifest As New Manifest

        If Not Helper.FileExists(MANIFEST_FILE_VIRTUAL_PATH) Then
            Return manifest
        End If

        Dim manifestFileContent As String = Helper.GetVirtualFileContent(MANIFEST_FILE_VIRTUAL_PATH)
        Dim manifestNode As HtmlNode = Helper.GetHtmlNode(manifestFileContent)

        LoadLayout(manifest, manifestNode)
        LoadControl(manifest, manifestNode)

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
