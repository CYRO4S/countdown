Module Util

    Public Function GetApplicationDir() As String
        Return Environment.CurrentDirectory & "\"
    End Function

    Public Function GetConfigFile() As String
        Return Environment.CurrentDirectory & "\Config\config.ini"
    End Function

End Module
