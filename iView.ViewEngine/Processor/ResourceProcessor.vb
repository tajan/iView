Imports HtmlAgilityPack

Public Class ResourceProcessor
    Implements IProcessor

    Public Function PostProcess(content As String) As String Implements IProcessor.PostProcess

        Dim htmlNode As HtmlNode = Helper.GetHtmlNode(content)
        Dim resourceNodes As HtmlNodeCollection = htmlNode.SelectNodes(RESOURCE_TAG_XPATH_FILTER)

        If resourceNodes Is Nothing OrElse resourceNodes.Count = 0 Then
            Return content
        End If

        Dim manifest As Manifest = ManifestHelper.GetManifest
        Dim headNode As HtmlNode = htmlNode.SelectSingleNode(HEAD_TAG_XPATH_FILTER)

        If headNode Is Nothing Then
            Throw New Exception("There is not head node in html content to add resource in.")
        End If

        Dim resourceNodesToBeAdded As New Dictionary(Of String, Resource)

        'add resource objects to dictionary to avoid duplicate resources
        For Each resourceNode In resourceNodes

            Dim name As String = resourceNode.Attributes(RESOURCE_TAG_NAME_ATTRIBUTE).Value

            If Not manifest.Resources.ContainsKey(name) Then
                Throw New Exception("Resource " & name & " does not exists in manifest file.")
            End If

            If Not resourceNodesToBeAdded.ContainsKey(name) Then
                resourceNodesToBeAdded.Add(name, manifest.Resources.Item(name))
            End If

        Next

        'remove resource nodes from content
        For i As Integer = 0 To resourceNodes.Count - 1
            resourceNodes(i).Remove()
        Next

        'add resources to head node
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
