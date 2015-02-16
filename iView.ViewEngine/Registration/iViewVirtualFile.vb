Imports System.Web.Hosting
Imports System.IO
Imports System.Web
Imports System.Text

Public Class iViewVirtualFile
    Inherits VirtualFile

    Private path As String

    Public Sub New(ByVal virtualPath As String)
        MyBase.New(virtualPath)
        path = virtualPath
    End Sub

    Public Overrides Function Open() As System.IO.Stream

        Dim content As String = iViewProcessManager.ProcessView(path)
        Dim memoryStream As New MemoryStream(Encoding.UTF8.GetBytes(content))
        memoryStream.Seek(0, SeekOrigin.Begin)
        Return memoryStream

    End Function

End Class