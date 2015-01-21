Imports System.Web
Imports System.Web.Hosting

Public Class iViewHttpModule
    Implements IHttpModule

    Private WithEvents _context As HttpApplication

    Public Sub Init(ByVal context As HttpApplication) Implements IHttpModule.Init
        _context = context
        AddHandler _context.BeginRequest, AddressOf OnRegister
    End Sub

    Public Sub Dispose() Implements IHttpModule.Dispose

    End Sub

    Private Shared _locker As New Object
    Private Shared _firstTime As Object

    Private Sub OnRegister(ByVal context As HttpApplication, e As EventArgs)

        If _firstTime Is Nothing Then

            SyncLock _locker

                If _firstTime Is Nothing Then
                    _firstTime = New Object

                    HostingEnvironment.RegisterVirtualPathProvider(New iViewResourceProvider)

                    ProcessManager.RegisterProcessor(New IncludeProcessor)
                    ProcessManager.RegisterProcessor(New LayoutProcessor)
                    ProcessManager.RegisterProcessor(New ControlProcessor)
                    ProcessManager.RegisterProcessor(New ControlTagRemoverProcessor)

                    ProcessManager.RegisterControlProcessor(New AttributeBinderProcessor)
                    ProcessManager.RegisterControlProcessor(New ContentBinderProcessor)
                    ProcessManager.RegisterControlProcessor(New ComplexAttributeProcessor)
                    ProcessManager.RegisterControlProcessor(New ChildControlBinderProcessor)
                    'ProcessManager.RegisterControlProcessor(New ControlTagRemoverProcessor)

                End If

            End SyncLock

        End If


    End Sub

End Class
