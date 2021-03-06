﻿Imports HtmlAgilityPack

Public Class LayoutProcessor
    Inherits BaseProcessor

    Public Overrides Function PreProcess(content As String) As String

        Dim htmlNode As HtmlNode = Helper.GetHtmlNode(content)

        'find first layout node
        'each view file contains only 1 layout (inheritance)
        Dim layoutViewNode As HtmlNode = htmlNode.SelectSingleNode(Manifest.LayoutTagName)

        'if there is no layout node, there is no need to process the node
        If layoutViewNode Is Nothing OrElse layoutViewNode.ChildNodes.Count = 0 Then
            Return htmlNode.OuterHtml
        End If

        'load parent layout html and its node
        Dim parentLayoutPath As String = ManifestProvider.GetLayoutVirtualFilePath(layoutViewNode, Manifest)

        If Not Helper.FileExists(parentLayoutPath) Then
            Throw New Exception("LayoutPreProcessor - PreProcess, Layout file does not found! File path: " & parentLayoutPath)
        End If

        Dim parentLayoutContent As String = Helper.GetVirtualFileContent(parentLayoutPath)

        'the layout file should only be pre processed
        parentLayoutContent = iViewProcessManager.PreProcess(parentLayoutContent)
        Dim parentLayoutNode As HtmlNode = Helper.GetHtmlNode(parentLayoutContent)

        Return ReplaceParentNodesWithChildNodes(parentLayoutNode, layoutViewNode)

    End Function

    Private Function ReplaceParentNodesWithChildNodes(parentLayoutNode As HtmlNode, viewNode As HtmlNode) As String

        'take first level child nodes, only html element nodes
        Dim validChildNodes As List(Of HtmlNode) = viewNode.ChildNodes.Where(Function(x) x.NodeType = HtmlNodeType.Element).ToList

        For Each sectionNode In validChildNodes

            'find the similar node in layout 
            Dim layoutSimilarNodes As HtmlNodeCollection = parentLayoutNode.SelectNodes("//" & sectionNode.Name)

            If layoutSimilarNodes Is Nothing OrElse layoutSimilarNodes.Count = 0 Then

                'if the node does not exists in parent layout node, we shod append it
                'to pass it to the higher parent
                parentLayoutNode.AppendChild(sectionNode)

            Else

                For Each layoutNode In layoutSimilarNodes

                    'replace the layout node with new view node 
                    layoutNode.ParentNode.ReplaceChild(sectionNode, layoutNode)
                    If Not layoutNode.Attributes.Contains(Manifest.LayoutExcludeRemovalAttributeName) Then
                        sectionNode.Attributes.Add(Manifest.ProcessedTagAttributeName, "true")
                    Else
                        sectionNode.Attributes.Add(Manifest.LayoutExcludeRemovalAttributeName, "true")
                    End If

                Next

            End If

        Next

        Return parentLayoutNode.OuterHtml

    End Function


End Class
