Imports HtmlAgilityPack

Public Class SimpleAttributeBinderProcessor
    Inherits BaseControlProcessor

    Protected Overrides Function ProcessControlNode(viewControlNode As HtmlNode, sourceControlNode As HtmlNode) As String

         'find the similar node in source control, based on tag name in view
        Dim sourceNodes As HtmlNodeCollection = sourceControlNode.SelectNodes("//" & viewControlNode.Name)

        'if source node does not exists, do nothing
        If sourceNodes Is Nothing OrElse sourceNodes.Count = 0 Then
            Return sourceControlNode.OuterHtml
        End If

        'find all normal attributes in view
        Dim attributes = viewControlNode.Attributes.Where(Function(x) Not x.Name.Contains(Manifest.AttributePrefix))

        'attach all the normal attributes to source
        For Each sourceNode In sourceNodes
            For Each attribute In attributes

                UpdateAttributes(sourceNode, attribute.Name, attribute.Value)

            Next
        Next

        Return sourceControlNode.OuterHtml

    End Function

    Private Sub UpdateAttributes(htmlNode As HtmlNode, attributeName As String, attributeValue As String)

        If htmlNode.Attributes.Contains(attributeName) Then
            htmlNode.Attributes(attributeName).Value = attributeValue
        Else
            htmlNode.Attributes.Add(attributeName, attributeValue)
        End If

    End Sub


End Class