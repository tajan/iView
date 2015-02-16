Imports HtmlAgilityPack

Public Class AttributeBinderProcessor
    Inherits BaseControlProcessor

    Protected Overrides Function ProcessControlNode(viewControlNode As HtmlNode, sourceControlNode As HtmlNode) As String

        If viewControlNode.NodeType <> HtmlNodeType.Element Then
            Return viewControlNode.OuterHtml
        End If

        'copy all attributes from view root node to control root node
        For Each attribute In viewControlNode.Attributes.Where(Function(x) x.Name <> ManifestProvider.Manifest.AttributePrefix).ToList

            If sourceControlNode.Attributes.Contains(attribute.Name) Then
                sourceControlNode.Attributes(attribute.Name).Value = attribute.Value
            Else
                sourceControlNode.Attributes.Add(attribute)
            End If

        Next

        Return sourceControlNode.OuterHtml

    End Function

End Class

