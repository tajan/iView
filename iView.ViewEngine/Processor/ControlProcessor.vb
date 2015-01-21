Imports HtmlAgilityPack
Public Class ControlProcessor
    Inherits BaseProcessor

    Public Overrides Function Process(content As String) As String

        Dim htmlNode As HtmlNode = Helper.GetHtmlNode(content)
        ProcessNode(htmlNode)
        Return htmlNode.OuterHtml

    End Function

    Private Sub ProcessNode(htmlNode As HtmlNode)

        For i As Integer = 0 To htmlNode.ChildNodes.Count - 1

            Dim chilNode As HtmlNode = htmlNode.ChildNodes(i)

            ProcessNode(chilNode)

            If Helper.IsControl(chilNode) Then
                ProcessComplexControl(chilNode)
            End If

        Next

    End Sub

    Private Sub ProcessComplexControl(htmlNode As HtmlNode)

        Dim sourceControlContent As String = Process(Helper.GetControlSourceNode(htmlNode))
        Dim processedContent As String = ProcessManager.ProcessControls(htmlNode.OuterHtml, sourceControlContent)
        Dim newNode = Helper.GetHtmlNode(processedContent)

        htmlNode.ParentNode.ReplaceChild(newNode, htmlNode)

    End Sub

    Public Overrides Function PostProcess(content As String) As String
        'do nothing
        Return content
    End Function

    Public Overrides Function PreProcess(content As String) As String
        'do nothing
        Return content
    End Function

End Class
