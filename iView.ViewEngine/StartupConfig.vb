Imports System.Web.Hosting
Imports System.Runtime.CompilerServices
Imports Owin

Public Module StartupConfig

    <Extension>
    Public Sub iViewConfig(app As IAppBuilder)

        If HtmlAgilityPack.HtmlNode.ElementsFlags.ContainsKey("form") Then
            HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("form")
        End If

        HostingEnvironment.RegisterVirtualPathProvider(New iViewResourceProvider)

        iViewProcessManager.RegisterViewProcessor(New IncludeProcessor)
        iViewProcessManager.RegisterViewProcessor(New LayoutProcessor)
        iViewProcessManager.RegisterViewProcessor(New ControlProcessor)
        iViewProcessManager.RegisterViewProcessor(New ControlTagRemoverProcessor)
        iViewProcessManager.RegisterViewProcessor(New ResourceProcessor)

        iViewProcessManager.RegisterControlProcessor(New AttributeBinderProcessor)
        iViewProcessManager.RegisterControlProcessor(New ContentBinderProcessor)
        iViewProcessManager.RegisterControlProcessor(New ComplexAttributeProcessor)
        iViewProcessManager.RegisterControlProcessor(New ChildControlBinderProcessor)
        'ProcessManager.RegisterControlProcessor(New ControlTagRemoverProcessor)

    End Sub

End Module