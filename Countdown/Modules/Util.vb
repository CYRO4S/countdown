Module Util

    Public Function GetApplicationDir() As String
        Return Environment.CurrentDirectory & "\"
    End Function

    Public Function GetConfigFile() As String
        Return Environment.CurrentDirectory & "\Config\config.ini"
    End Function

    Public Function IsHybridAvailiable() As Boolean
        ' Check for windows version
        ' Hybrid only availiable on Windows 8 or higher version
        If Environment.OSVersion.Version.Major < 6 Then
            Return False
            Exit Function
        End If

        If Environment.OSVersion.Version.Minor < 2 Then
            Return False
            Exit Function
        End If

        Return True
    End Function

End Module
