Imports HtmlAgilityPack
Imports System.Web

Public Class iViewProcessManager

#Region " Private Members "

    Private Shared Processors As List(Of IProcessor)
    Private Shared ControlProcessors As List(Of IControlProcessor)
    Private Shared Cache As Dictionary(Of String, String)

    Private Shared _locker As New Object

    Shared Sub New()
        Processors = New List(Of IProcessor)
        ControlProcessors = New List(Of IControlProcessor)
        cache = New Dictionary(Of String, String)
    End Sub

#End Region

#Region " Public Members "

    Public Shared Sub RegisterViewProcessor(process As IProcessor)
        Processors.Add(process)
    End Sub

    Public Shared Sub RegisterControlProcessor(controlProcessor As IControlProcessor)
        ControlProcessors.Add(controlProcessor)
    End Sub

    Public Shared Function ProcessView(virtualFilePath As String) As String

        If ManifestProvider.Manifest.Debug Then

            Dim physicalPath As String = VirtualPathUtility.ToAppRelative(virtualFilePath)
            Return iViewProcessManager.ProcessViewContent(Helper.GetVirtualFileContent(virtualFilePath))

        Else

            If Not Cache.ContainsKey(virtualFilePath) Then
                SyncLock _locker
                    If Not Cache.ContainsKey(virtualFilePath) Then
                        Cache.Item(virtualFilePath) = iViewProcessManager.ProcessViewContent(Helper.GetVirtualFileContent(virtualFilePath))
                    End If
                End SyncLock
            End If

            Return Cache.Item(virtualFilePath)

        End If

    End Function

#End Region

#Region " Friend Members "

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

    Friend Shared Function ProcessViewContent(htmlContent As String) As String

        Dim out As String = htmlContent

        out = PreProcess(out)
        out = Process(out)
        out = PostProcess(out)

        Return out

    End Function

#End Region

End Class
