Imports HtmlAgilityPack
Imports System.Web

Friend Class Helper

    Public Shared Function GetHtmlNode(htmlContent As String) As HtmlNode

        Dim htmlDoc As New HtmlAgilityPack.HtmlDocument()

        'htmlDoc.OptionOutputAsXml = True
        'htmlDoc.OptionFixNestedTags = True
        'htmlDoc.OptionWriteEmptyNodes = True
        htmlDoc.LoadHtml(htmlContent)

        If htmlDoc Is Nothing Then
            Throw New Exception("Error in LoadHtmlNode, Invalid file path: " & htmlContent)
        End If

        If htmlDoc.DocumentNode Is Nothing Then
            Throw New Exception("Error in LoadHtmlNode, root node is null: " & htmlContent)
        End If

        Return htmlDoc.DocumentNode

    End Function

    Public Shared Function GetVirtualFileContent(virtualPath As String) As String

        If Not FileExists(virtualPath) Then
            Throw New Exception("Error loading file: " & virtualPath)
        End If

        Dim filePath As String = HttpContext.Current.Server.MapPath(virtualPath)
        Return System.IO.File.ReadAllText(filePath)

    End Function

    Public Shared Function FileExists(virtualPath As String) As Boolean

        Dim filePath As String = HttpContext.Current.Server.MapPath(virtualPath)
        Return System.IO.File.Exists(filePath)

    End Function

    Public Shared Function GetControlSourceNode(htmlNode As HtmlNode) As String

        If IsControl(htmlNode) Then

            Dim controlSourceFilePath As String = ManifestHelper.GetControlFileVirtualPath(htmlNode, ManifestHelper.GetManifest)

            If Not Helper.FileExists(controlSourceFilePath) Then
                Throw New Exception("GetControlSourceNode, Control file does not found! File path: " & controlSourceFilePath)
            End If

            Return Helper.GetVirtualFileContent(controlSourceFilePath)

        Else

            Return Nothing

        End If

    End Function

    Public Shared Function IsControl(htmlNode As HtmlNode)
        If htmlNode.Attributes.Where(Function(x) x.Name = CONTROL_TAG_TYPE_ATTRIBUTE).Count = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
