Imports System.Web.Hosting
Imports Microsoft.Owin

Public Class iViewResourceProvider
    Inherits VirtualPathProvider

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function FileExists(ByVal virtualPath As String) As Boolean
        Return Helper.FileExists(virtualPath)
    End Function

    Public Overrides Function GetFile(ByVal virtualPath As String) As VirtualFile
        Return New iViewVirtualFile(virtualPath)
    End Function


End Class


