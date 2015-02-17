Imports HtmlAgilityPack

Public MustInherit Class BaseControlProcessor
    Implements IControlProcessor


    Private _manifest As Manifest
    Protected ReadOnly Property Manifest As Manifest
        Get
            Return Me._manifest
        End Get

    End Property

    Public Sub New()
        _manifest = ManifestProvider.Manifest
    End Sub

    Public Function ProcessControl(viewControlHtmlContent As String, sourceControlHtmlContent As String) As String Implements IControlProcessor.ProcessControl

        'there is always one node in Document Node
        Dim controlViewNode As HtmlNode = Helper.GetHtmlNode(viewControlHtmlContent).ChildNodes(0)
        Dim controlSourceNode As HtmlNode = Helper.GetHtmlNode(sourceControlHtmlContent).ChildNodes(Manifest.ControlTagName)

        If controlSourceNode Is Nothing Then
            Throw New NullReferenceException("There is no control tag in control file:" & controlViewNode.Name)
        End If

        Return ProcessControlNode(controlViewNode, controlSourceNode)

    End Function

    Protected MustOverride Function ProcessControlNode(viewControlNode As HtmlNode, sourceControlNode As HtmlNode) As String

End Class
