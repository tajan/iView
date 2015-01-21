Imports System.Web.Hosting
Imports System.IO
Imports System.Web
Imports System.Text

Public Class iViewFile
    Inherits VirtualFile

    Private path As String
    Private Shared cache As New Dictionary(Of String, String)
    Private Shared locker As New Object

    Public Sub New(ByVal virtualPath As String)
        MyBase.New(virtualPath)
        path = VirtualPathUtility.ToAppRelative(virtualPath)
    End Sub

    Public Overrides Function Open() As System.IO.Stream

        Dim content As String

#If DEBUG Then
        content = ProcessManager.ProcessView(Helper.GetVirtualFileContent(path))
#Else
        If Not cache.ContainsKey(path) Then
            SyncLock locker
                If Not cache.ContainsKey(path) Then
                    cache.Item(path) = ProcessManager.ProcessView(Helper.GetVirtualFileContent(path))
                End If
                content = cache.Item(path)
            End SyncLock

        Else
            content = cache.Item(path)
        End If
#End If


        Dim memoryStream As New MemoryStream(Encoding.UTF8.GetBytes(content))
        memoryStream.Seek(0, SeekOrigin.Begin)
        Return memoryStream

    End Function

End Class