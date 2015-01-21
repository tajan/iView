Imports HtmlAgilityPack

Public Class ProcessManager

    Private Shared _locker As New Object

    Private Shared Processors As New List(Of IProcessor)
    Private Shared ControlProcessors As New List(Of IControlProcessor)

    Public Shared Sub RegisterProcessor(process As IProcessor)
        SyncLock _locker
            Processors.Add(process)
        End SyncLock
    End Sub

    Public Shared Sub RegisterControlProcessor(controlProcessor As IControlProcessor)
        SyncLock _locker
            ControlProcessors.Add(controlProcessor)
        End SyncLock
    End Sub

    Friend Shared Function PreProcess(htmlContent As String) As String

        Dim content As String = htmlContent

        For Each _process In Processors
            content = _process.PreProcess(content)
        Next

        Return content

    End Function

    Friend Shared Function PostProcess(htmlContent As String) As String

        Dim content As String = htmlContent

        For Each _process In Processors
            content = _process.PostProcess(content)
        Next

        Return content

    End Function

    Friend Shared Function Process(htmlContent As String) As String

        Dim content As String = htmlContent

        For Each _process In Processors
            content = _process.Process(content)
        Next

        Return content

    End Function

    Friend Shared Function ProcessControls(viewContent As String, sourceContent As String) As String

        Dim content As String = sourceContent

        For Each _process In ControlProcessors
            content = _process.ProcessControl(viewContent, content)
        Next

        Return content

    End Function

    Public Shared Function ProcessView(htmlContent As String) As String

        Dim out As String = htmlContent

        out = PreProcess(out)
        out = Process(out)
        out = PostProcess(out)

        Return out

    End Function

End Class
