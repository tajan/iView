Public MustInherit Class BaseProcessor
    Implements IProcessor

    Private _manifest As Manifest
    Protected ReadOnly Property Manifest As Manifest
        Get
            Return Me._manifest
        End Get
    End Property

    Public Overridable Function PostProcess(content As String) As String Implements IProcessor.PostProcess
        'do nothing
        Return content
    End Function

    Public Overridable Function PreProcess(content As String) As String Implements IProcessor.PreProcess
        'do nothing
        Return content
    End Function

    Public Overridable Function Process(content As String) As String Implements IProcessor.Process
        'do nothing
        Return content
    End Function

    Public Sub New()
        _manifest = ManifestProvider.Manifest
    End Sub

End Class
