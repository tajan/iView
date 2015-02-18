Imports HtmlAgilityPack

Public Class IncludeProcessor
    Inherits BaseProcessor

    Public Overrides Function PreProcess(content As String) As String

        Dim htmlNode As HtmlNode = Helper.GetHtmlNode(content)

        'find all include elements in view
        Dim xpathFilter As String = "//" & Manifest.IncludeTagName & "[@" & Manifest.IncludeSourceAttributeName & "]"
        Dim includeNodesInView As HtmlNodeCollection = htmlNode.SelectNodes(xpathFilter)

        'if there is no include node, there is no need to be processed
        If includeNodesInView Is Nothing OrElse includeNodesInView.Count = 0 Then
            Return htmlNode.OuterHtml
        End If

        For Each includeInViewNode In includeNodesInView

            Dim includeFileVirtualPath As String = includeInViewNode.Attributes(Manifest.IncludeSourceAttributeName).Value

            If Not Helper.FileExists(includeFileVirtualPath) Then
                Throw New System.IO.FileNotFoundException("Include file does not exist:", includeFileVirtualPath)
            End If

            'process include file content
            Dim includeHtmlContent As String = iViewProcessManager.ProcessView(includeFileVirtualPath)

            'replace include content with include node in view
            Dim includeInSourceNode As HtmlNode = Helper.GetHtmlNode(includeHtmlContent)
            includeInViewNode.ParentNode.ReplaceChild(includeInSourceNode, includeInViewNode)

        Next

        Return htmlNode.OuterHtml

    End Function

End Class