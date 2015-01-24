Imports HtmlAgilityPack

Public Class ResourceProcessor
    Implements IProcessor

    'todo: 
    Public Function PostProcess(content As String) As String Implements IProcessor.PostProcess

        Dim htmlNode As HtmlNode = Helper.GetHtmlNode(content)
        Dim resourceNodes = htmlNode.Descendants("resource")

        If resourceNodes Is Nothing OrElse resourceNodes.Count = 0 Then
            Return content
        End If

        Dim manifest As Manifest = ManifestHelper.GetManifest
        Dim headNodes = htmlNode.Descendants("head")

        If headNodes Is Nothing OrElse headNodes.Count = 0 Then
            Throw New Exception("There is not head node in html content to add resource in.")
        End If

        Dim headNode = headNodes(0)
        Dim resourceNodesToBeAdded As New Dictionary(Of String, Resource)

        For Each resourceNode In resourceNodes

            Dim name As String = resourceNode.Attributes("iv-name").Value

            If Not manifest.Resources.ContainsKey(name) Then
                Throw New Exception("Resource " & name & " does not exists in manifest file.")
            End If

            If Not resourceNodesToBeAdded.ContainsKey(name) Then
                resourceNodesToBeAdded.Add(name, manifest.Resources.Item(name))
            End If

        Next


        For Each resource In resourceNodesToBeAdded.Values

            Dim newNode As HtmlNode = Nothing

            Select Case resource.Type.ToLower

                Case "css"
                    newNode = headNode.OwnerDocument.CreateElement("link")
                    newNode.Attributes.Add("rel", "stylesheet")
                    newNode.Attributes.Add("href", resource.Source)

                Case "js"
                    newNode = htmlNode.OwnerDocument.CreateElement("script")
                    newNode.Attributes.Add("src", resource.Source)

                Case Else
                    Throw New Exception("There is no such a type in resources: " & resource.Type)

            End Select

            headNode.ChildNodes.Add(newNode)

        Next

        Return htmlNode.OuterHtml
       
    End Function

    Public Function PreProcess(content As String) As String Implements IProcessor.PreProcess
        'do nothing
        Return content
    End Function

    Public Function Process(content As String) As String Implements IProcessor.Process
        'do nothing
        Return content
    End Function

End Class
