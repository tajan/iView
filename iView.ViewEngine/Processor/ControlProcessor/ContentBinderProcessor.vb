Imports HtmlAgilityPack

Public Class ContentBinderProcessor
    Inherits BaseControlProcessor

    Protected Overrides Function ProcessControlNode(viewControlNode As HtmlNode, sourceControlNode As HtmlNode) As String

        If viewControlNode.NodeType <> HtmlNodeType.Element Then
            Return sourceControlNode.OuterHtml
        End If

        Dim contentNodes = sourceControlNode.Descendants(Manifest.ControlContentTagName).ToList

        For Each contentNodeInSource In contentNodes
            viewControlNode.ChildNodes.ToList.ForEach(Sub(x) contentNodeInSource.ChildNodes.Add(x))
            contentNodeInSource.ParentNode.RemoveChild(contentNodeInSource, True)
        Next

        Return sourceControlNode.OuterHtml

    End Function

End Class