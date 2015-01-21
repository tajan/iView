Imports HtmlAgilityPack

Public Class ComplexAttributeProcessor
    Inherits BaseControlProcessor

    Protected Overrides Function ProcessControlNode(viewControlNode As HtmlNode, sourceControlNode As HtmlNode) As String

        Dim attributes = viewControlNode.Attributes.Where(Function(x) x.Name.StartsWith(IV_ATTRIBUTE_PREFIX)).ToList
        Dim out As String = sourceControlNode.OuterHtml

        For Each attribute In attributes
            out = out.Replace(CONTROL_TAG_ATTRIBUTE_PREFIX & attribute.Name, attribute.Value)
        Next

        'Dim attributes = viewControlNode.Attributes.Where(Function(x) x.Name.StartsWith(IV_ATTRIBUTE_PREFIX)).ToList
        'Dim sourceAttributes = sourceControlNode.DescendantsAndSelf.SelectMany(Function(x) x.Attributes).ToList

        'For Each attribute In attributes

        '    Dim targetAttributeValue As String = CONTROL_TAG_ATTRIBUTE_PREFIX & attribute.Name
        '    Dim targetAttributes = sourceAttributes.Where(Function(x) x.Value.IndexOf(targetAttributeValue) >= 0).ToList

        '    For Each sourceAttribute In targetAttributes
        '        sourceAttribute.Value = sourceAttribute.Value.Replace(targetAttributeValue, attribute.Value)
        '    Next

        'Next


        Return out

    End Function

End Class