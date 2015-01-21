Imports HtmlAgilityPack

Public MustInherit Class BaseControlProcessor
    Implements IControlProcessor

    Public Function ProcessControl(viewControlHtmlContent As String, sourceControlHtmlContent As String) As String Implements IControlProcessor.ProcessControl

        'there is always one node in Document Node
        Dim controlViewNode As HtmlNode = Helper.GetHtmlNode(viewControlHtmlContent).ChildNodes(0)
        Dim controlSourceNode As HtmlNode = Helper.GetHtmlNode(sourceControlHtmlContent).ChildNodes(CONTROL_TAG)

        If controlSourceNode Is Nothing Then
            Throw New NullReferenceException("There is no control tag in control file:" & controlViewNode.Name)
        End If

        Return ProcessControlNode(controlViewNode, controlSourceNode)

    End Function

    Protected MustOverride Function ProcessControlNode(viewControlNode As HtmlNode, sourceControlNode As HtmlNode) As String

End Class
