Imports HtmlAgilityPack

Public Class SimpleAttributeBinderProcessor
    Inherits BaseControlProcessor

    Protected Overrides Function ProcessControlNode(viewControlNode As HtmlNode, sourceControlNode As HtmlNode) As String

         'find the similar node in source control, based on tag name in view
        Dim sourceNodes As HtmlNodeCollection = sourceControlNode.SelectNodes("//" & viewControlNode.Name)

        'if source node does not exists, do nothing
        If sourceNodes Is Nothing OrElse sourceNodes.Count = 0 Then
            Return sourceControlNode.OuterHtml
        End If

        'find all normal attributes in view
        Dim attributes = viewControlNode.Attributes.Where(Function(x) Not x.Name.Contains(IV_ATTRIBUTE_PREFIX))

        'attach all the normal attributes to source
        For Each sourceNode In sourceNodes
            For Each attribute In attributes

                UpdateAttributes(sourceNode, attribute.Name, attribute.Value)

            Next
        Next

        Return sourceControlNode.OuterHtml

    End Function

    Private Sub UpdateAttributes(htmlNode As HtmlNode, attributeName As String, attributeValue As String)

        If htmlNode.Attributes.Contains(attributeName) Then
            htmlNode.Attributes(attributeName).Value = attributeValue
        Else
            htmlNode.Attributes.Add(attributeName, attributeValue)
        End If

    End Sub


End Class

'Imports HtmlAgilityPack

'Public Class AttributeComplexValueProcessor
'    Implements IControlProcessor

'    Public Const ATTRIBUTE_NAME_PREFIX As String = "iv-"
'    Public Const CONTROL_ATTRIBUTE_NAME As String = "iv-type"
'    Public Const ATTRIBUTE_VALUE_PREFIX As String = "$"

'    'Public Const CONTROL_THEME_ATTRIBUTE_NAME As String = "iv-theme"
'    'Public Const CONTROL_ROOT_ATTRIBUTE_NAME As String = "iv-root"
'    'Public Const CONTROL_FOLDER_ATTRIBUTE_NAME As String = "iv-controls"

'    'Public Const CONTROL_FILE_EXTENTION As String = ".html"




'    'Public Const ATTRIBUTE_VALUE_PREFIX As String = "$"

'    'Public Const CONTENT_FLAG As String = "$content"

'    Public Function ProcessControl(viewControlHtmlContent As String, sourceControlHtmlContent As String) As String Implements IControlProcessor.ProcessControl

'        Dim viewNode As HtmlNode = Helper.GetHtmlNode(viewControlHtmlContent)
'        Dim sourceNode As HtmlNode = Helper.GetHtmlNode(sourceControlHtmlContent)

'        For i As Integer = 0 To viewNode.ChildNodes.Count - 1

'            Dim childNode As HtmlNode = viewNode.ChildNodes(i)
'            Dim childContent As String = childNode.OuterHtml

'            OverwriteAttributes(viewNode, sourceNode)

'            For Each _controlProcess In ControlProcesses
'                controlContent = ProcessManager.ProcessControls(controlContent)
'            Next

'            childNode.ReplaceChild(Helper.GetHtmlNode(controlContent), childNode)

'        Next

'        Return sourceNode.OuterHtml

'    End Function

'    Private Sub OverwriteAttributes(viewNode As HtmlNode, sourceNode As HtmlNode)

'        Dim attributes = viewNode.Attributes.Select(Function(x) New KeyValuePair(Of String, String)(x.Name, x.Value)).ToList

'        For Each attribute In attributes
'            UpdateAttribute(sourceNode, attribute.Key, attribute.Value)
'        Next

'    End Sub

'    Private Sub UpdateAttribute(htmlNode As HtmlNode, attributeName As String, attributeValue As String)

'        If attributeName = CONTROL_ATTRIBUTE_NAME Then
'            Exit Sub
'        End If

'        'find attribute in source control by value to replace its value by source value
'        If attributeName.StartsWith(ATTRIBUTE_NAME_PREFIX) Then

'            Dim matchedAttributesByValue = htmlNode.DescendantsAndSelf.SelectMany(Function(x) x.Attributes).Where(Function(x) x.Value.IndexOf(ATTRIBUTE_VALUE_PREFIX & attributeName) >= 0).ToList

'            If matchedAttributesByValue Is Nothing OrElse matchedAttributesByValue.Count = 0 Then
'                Exit Sub
'            End If

'            For Each targetAttribute In matchedAttributesByValue
'                targetAttribute.Value = targetAttribute.Value.Replace(ATTRIBUTE_VALUE_PREFIX & attributeName, attributeValue)
'            Next

'        End If

'    End Sub

'    Private Sub UpdateAttribute1(htmlNode As HtmlNode, attributeName As String, attributeValue As String)

'        If attributeName = CONTROL_ATTRIBUTE_NAME Then
'            Exit Sub
'        End If

'        If htmlNode.Attributes.Contains(attributeName) Then
'            htmlNode.Attributes(attributeName).Value = attributeValue
'            'If String.IsNullOrEmpty(htmlNode.Attributes(attributeName).Value) Then
'            '    htmlNode.Attributes(attributeName).Value = attributeValue
'            'Else
'            '    If htmlNode.Attributes(attributeName).Value <> attributeValue Then
'            '        htmlNode.Attributes(attributeName).Value &= " " & attributeValue
'            '    End If
'            'End If
'        Else
'            htmlNode.Attributes.Add(attributeName, attributeValue)
'        End If

'        'find attribute in source control by value to replace its value by source value
'        If Not attributeName.StartsWith(ATTRIBUTE_NAME_PREFIX) Then

'            Dim matchedAttributesByValue = htmlNode.DescendantsAndSelf.SelectMany(Function(x) x.Attributes).Where(Function(x) x.Value.IndexOf(ATTRIBUTE_VALUE_PREFIX & attributeName) >= 0).ToList

'            If matchedAttributesByValue Is Nothing OrElse matchedAttributesByValue.Count = 0 Then
'                Exit Sub
'            End If

'            For Each targetAttribute In matchedAttributesByValue
'                targetAttribute.Value = targetAttribute.Value.Replace(ATTRIBUTE_VALUE_PREFIX & attributeName, attributeValue)
'            Next

'        End If

'    End Sub

'End Class
