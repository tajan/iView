Public Interface IProcessor

    Function PreProcess(content As String) As String
    Function Process(content As String) As String
    Function PostProcess(content As String) As String


End Interface
