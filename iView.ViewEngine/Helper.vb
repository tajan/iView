Imports HtmlAgilityPack
Imports System.Web
Imports System.IO

Friend Class Helper

    Public Shared Function GetHtmlNode(htmlContent As String) As HtmlNode

        Dim htmlDoc As New HtmlAgilityPack.HtmlDocument()
        htmlDoc.LoadHtml(htmlContent)
        Return htmlDoc.DocumentNode

    End Function

    Public Shared Function GetVirtualFileContent(virtualPath As String) As String

        If Not FileExists(virtualPath) Then
            Throw New FileNotFoundException("Error loading file: ", virtualPath)
        End If

        Dim filePath As String = HttpContext.Current.Server.MapPath(virtualPath)
        Return System.IO.File.ReadAllText(filePath)

    End Function

    Public Shared Function FileExists(virtualPath As String) As Boolean

        'If Not virtualPath.StartsWith(ManifestHelper.Manifest.ViewFolderPath) Then
        '    Return False
        'End If

        Dim filePath As String = HttpContext.Current.Server.MapPath(virtualPath)
        Return System.IO.File.Exists(filePath)

    End Function

End Class
