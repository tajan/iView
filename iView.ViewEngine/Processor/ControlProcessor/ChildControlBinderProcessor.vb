Imports HtmlAgilityPack

Public Class ChildControlBinderProcessor
    Inherits BaseControlProcessor

    Protected Overrides Function ProcessControlNode(viewControlNode As HtmlNode, sourceControlNode As HtmlNode) As String

         For Each viewChildNode In viewControlNode.ChildNodes.Where(Function(x) x.NodeType = HtmlNodeType.Element)

            Dim sourceNodes = sourceControlNode.Descendants(viewChildNode.Name).ToList.Where(Function(x) x.Name <> CONTROL_TAG AndAlso x.Name <> CONTROL_TAG_CONTENT_TAG AndAlso x.Name.StartsWith(IV_ATTRIBUTE_PREFIX))

            For Each sourceNode In sourceNodes

                  For Each innerChildNode In viewChildNode.ChildNodes.ToList

                    Dim newNodeToBeAppend = innerChildNode.CloneNode(True)
                    sourceNode.ChildNodes.Add(newNodeToBeAppend)

                Next
            Next

        Next

        Return sourceControlNode.OuterHtml

    End Function

End Class
