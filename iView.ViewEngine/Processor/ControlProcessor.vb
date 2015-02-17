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

            'recursivly load child nodes and process down/up
            ProcessNode(chilNode)

            If IsControl(chilNode) Then
                ProcessComplexControl(chilNode)
            End If

        Next

    End Sub

    Private Sub ProcessComplexControl(htmlNode As HtmlNode)

        Dim sourceControlContent As String = Process(GetControlSourceNode(htmlNode))
        Dim processedContent As String = iViewProcessManager.ProcessControls(htmlNode.OuterHtml, sourceControlContent)
        Dim newNode = Helper.GetHtmlNode(processedContent)

        htmlNode.ParentNode.ReplaceChild(newNode, htmlNode)

    End Sub

    Private Function GetControlSourceNode(htmlNode As HtmlNode) As String

        If IsControl(htmlNode) Then

            Dim controlSourceFilePath As String = ManifestProvider.GetControlFileVirtualPath(htmlNode, Manifest)

            If Not Helper.FileExists(controlSourceFilePath) Then
                Throw New Exception("Control file does not exist! File path: " & controlSourceFilePath)
            End If

            Return Helper.GetVirtualFileContent(controlSourceFilePath)

        Else

            Return Nothing

        End If

    End Function

    Private Function IsControl(htmlNode As HtmlNode) As Boolean
        If htmlNode.Attributes.Where(Function(x) x.Name = ManifestProvider.Manifest.ControlTypeAttributeName).Count = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
