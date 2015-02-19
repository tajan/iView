Imports HtmlAgilityPack

Public Class ProcessedTagRemovalProcessor
    Inherits BaseProcessor

    Public Overrides Function PostProcess(content As String) As String

        Dim htmlNode As HtmlNode = Helper.GetHtmlNode(content)
        RemoveWithoutChildrenByAttribute(htmlNode, Manifest.ProcessedTagAttributeName)
        Return htmlNode.OuterHtml

    End Function

    Private Sub RemoveWithoutChildrenByAttribute(rootTag As HtmlNode, attributeName As String)

        For Each child In rootTag.ChildNodes.ToList

            RemoveWithoutChildrenByAttribute(child, attributeName)

            If child.Attributes.Contains(attributeName) Then

                For i As Integer = 0 To child.ChildNodes.Count - 1
                    child.ParentNode.InsertBefore(child.ChildNodes(i).CloneNode(True), child)
                Next

                child.Remove()

            End If

        Next

    End Sub

End Class
