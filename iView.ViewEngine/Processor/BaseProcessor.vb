Public MustInherit Class BaseProcessor
    Implements IProcessor

    Private _manifest As Manifest
    Protected ReadOnly Property Manifest As Manifest
        Get
            Return Me._manifest
        End Get
    End Property

    Public MustOverride Function PostProcess(content As String) As String Implements IProcessor.PostProcess

    Public MustOverride Function PreProcess(content As String) As String Implements IProcessor.PreProcess

    Public MustOverride Function Process(content As String) As String Implements IProcessor.Process

    Public Sub New()
        _manifest = ManifestHelper.GetManifest
        If HtmlAgilityPack.HtmlNode.ElementsFlags.ContainsKey("form") Then
            HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("form")
        End If
    End Sub

End Class
