Imports HtmlAgilityPack

Public Class IncludeProcessor
    Implements IProcessor

    Public Function PreProcess(content As String) As String Implements IProcessor.PreProcess

        Dim htmlNode As HtmlNode = Helper.GetHtmlNode(content)

        'find all include elements in view
        Dim includeNodesInView As HtmlNodeCollection = htmlNode.SelectNodes(INCLUDE_TAG_XPATH_FILTER)

        'if there is no include node, there is no need to be processed
        If includeNodesInView Is Nothing OrElse includeNodesInView.Count = 0 Then
            Return htmlNode.OuterHtml
        End If

        For Each includeInViewNode In includeNodesInView

            Dim includeFileVirtualPath As String = includeInViewNode.Attributes(INCLUDE_TAG_SOURCE_ATTRIBUTE).Value

            If Not Helper.FileExists(includeFileVirtualPath) Then
                Throw New Exception("IncludePreProcessor - PreProcess, Include file does not found! File path: " & includeFileVirtualPath)
            End If

            Dim includeHtmlContent As String = Helper.GetVirtualFileContent(includeFileVirtualPath)

            'process include file content
            includeHtmlContent = ProcessManager.ProcessView(includeHtmlContent)

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