Imports HtmlAgilityPack

Public Class IncludeProcessor
    Implements IProcessor

    Public Function PreProcess(content As String) As String Implements IProcessor.PreProcess

        Dim htmlNode As HtmlNode = Helper.GetHtmlNode(content)

        Dim manifest As Manifest = ManifestProvider.Manifest

        'find all include elements in view
        Dim xpathFilter As String = "//" & manifest.IncludeTagName & "[@" & manifest.IncludeSourceAttributeName & "]"
        Dim includeNodesInView As HtmlNodeCollection = htmlNode.SelectNodes(xpathFilter)

        'if there is no include node, there is no need to be processed
        If includeNodesInView Is Nothing OrElse includeNodesInView.Count = 0 Then
            Return htmlNode.OuterHtml
        End If

        For Each includeInViewNode In includeNodesInView

            Dim includeFileVirtualPath As String = includeInViewNode.Attributes(manifest.IncludeSourceAttributeName).Value

            If Not Helper.FileExists(includeFileVirtualPath) Then
                Throw New Exception("IncludePreProcessor - PreProcess, Include file does not found! File path: " & includeFileVirtualPath)
            End If

            'process include file content
            Dim includeHtmlContent As String = iViewProcessManager.ProcessView(includeFileVirtualPath)

            'replace include content with include node in view
            Dim includeInSourceNode As HtmlNode = Helper.GetHtmlNode(includeHtmlContent)
            includeInViewNode.ParentNode.ReplaceChild(includeInSourceNode, includeInViewNode)

        Next

        Return htmlNode.OuterHtml

    End Function

    Public Function PostProcess(content As String) As String Implements IProcessor.PostProcess
        'do nothing
        Return content
    End Function

    Public Function Process(content As String) As String Implements IProcessor.Process
        'do nothing
        Return content
    End Function

End Class