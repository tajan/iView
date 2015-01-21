Imports HtmlAgilityPack

Public Class ControlTagRemoverProcessor
    Inherits BaseProcessor

    Public Overrides Function PostProcess(content As String) As String

        Dim htmlNode As HtmlNode = Helper.GetHtmlNode(content)
        Dim controlNodes = htmlNode.SelectNodes("//" & CONTROL_TAG)

        If controlNodes Is Nothing Then
            Return content
        End If

        For Each x In controlNodes.ToList
            x.ParentNode.RemoveChild(x, True)
        Next
        Return htmlNode.OuterHtml
    End Function

    Public Overrides Function PreProcess(content As String) As String
        'do nothing
        Return content
    End Function

    Public Overrides Function Process(content As String) As String
        'do nothing
        Return content
    End Function

End Class
