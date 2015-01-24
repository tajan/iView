Module Constants


    Public Const IV_ATTRIBUTE_PREFIX As String = "iv-"

#Region " Manifest "

    Public Const MANIFEST_FILE_VIRTUAL_PATH As String = "~/manifest.xml"
    Public Const MANIFEST_ATTRIBUTE_DEBUG As String = "debug"
    Public Const TRUE_VALUE As String = "true"
#End Region


#Region " Layout "

    Public Const LAYOUT_TAG As String = "layout"
    Public Const LAYOUT_TAG_ROOT_ATTRIBUTE As String = IV_ATTRIBUTE_PREFIX & "root"
    Public Const LAYOUT_TAG_THEME_ATTRIBUTE As String = IV_ATTRIBUTE_PREFIX & "theme"
    Public Const LAYOUT_TAG_LAYOUT_ATTRIBUTE As String = IV_ATTRIBUTE_PREFIX & "layout"
    Public Const LAYOUT_TAG_SOURCE_ATTRIBUTE As String = IV_ATTRIBUTE_PREFIX & "source"
    Public Const LAYOUT_TAG_XPATH_FILTER As String = "//" & LAYOUT_TAG
   
#End Region


#Region " Include "

    Public Const INCLUDE_TAG As String = "include"
    Public Const INCLUDE_TAG_SOURCE_ATTRIBUTE As String = IV_ATTRIBUTE_PREFIX & "source"
    Public Const INCLUDE_TAG_XPATH_FILTER As String = "//" & INCLUDE_TAG & "[@" & INCLUDE_TAG_SOURCE_ATTRIBUTE & "]"

#End Region

#Region " Control "

    Public Const CONTROL_TAG As String = "control"
    Public Const CONTROL_TAG_ROOT_ATTRIBUTE As String = IV_ATTRIBUTE_PREFIX & "root"
    Public Const CONTROL_TAG_THEME_ATTRIBUTE As String = IV_ATTRIBUTE_PREFIX & "theme"
    Public Const CONTROL_TAG_PATH_ATTRIBUTE As String = IV_ATTRIBUTE_PREFIX & "controls"
    Public Const CONTROL_TAG_TYPE_ATTRIBUTE As String = IV_ATTRIBUTE_PREFIX & "type"
    Public Const CONTROL_TAG_ATTRIBUTE_PREFIX As String = "$"
    Public Const CONTROL_FILE_EXTENTION As String = ".html"
    Public Const CONTROL_TAG_CONTENT_TAG As String = "iv-content"

#End Region
    

End Module
