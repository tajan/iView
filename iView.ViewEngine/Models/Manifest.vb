Public Class Manifest

    Public Property Layout As Layout
    Public Property Control As Control
    Public Property Resources As New Dictionary(Of String, Resource)
    Public Property Debug As Boolean

    Public Sub New()
        Me.Layout = New Layout
        Me.Control = New Control
    End Sub


End Class
