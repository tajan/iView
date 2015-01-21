Imports System.Web.Hosting
Imports System.Web

Public Class iViewResourceProvider
    Inherits System.Web.Hosting.VirtualPathProvider

    Public Sub New()
        MyBase.New()
    End Sub
    Public Overrides Function FileExists(ByVal virtualPath As String) As Boolean
        Return Helper.FileExists(GetViewFilePath(virtualPath))
    End Function

    Public Overrides Function GetFile(ByVal virtualPath As String) As VirtualFile
        Return New iViewFile(GetViewFilePath(virtualPath))
    End Function

    Private Function GetViewFilePath(ByVal virtualPath As String) As String
        Return virtualPath '& ".html"
    End Function

End Class


