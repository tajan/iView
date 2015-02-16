Imports System.Web.Hosting

Public Class iViewResourceProvider
    Inherits VirtualPathProvider

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function FileExists(ByVal virtualPath As String) As Boolean
        Return Helper.FileExists(GetViewFilePath(virtualPath))
    End Function

    Public Overrides Function GetFile(ByVal virtualPath As String) As VirtualFile
        Return New iViewVirtualFile(GetViewFilePath(virtualPath))
    End Function

    Private Function GetViewFilePath(ByVal virtualPath As String) As String
        Return virtualPath
    End Function

End Class


