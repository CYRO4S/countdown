Imports Catboy

Module Util

    Public Structure Settings
        Dim IsSchedule As Boolean
        Dim ScheduleTime As String
        Dim LastTime As String
        Dim IsImmediatly As Boolean
        Dim IsRapidStart As Boolean
    End Structure

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

    Public Sub SaveSettings(settings As Settings)
        Dim ini As New ConfigParser(GetConfigFile)
        ini.SetValue("General", "IsSchedule", If(settings.IsSchedule, 1, 0))
        ini.SetValue("General", "ScheduleTime", settings.ScheduleTime)
        ini.SetValue("General", "LastTime", settings.LastTime)
        ini.SetValue("General", "IsImmediatly", If(settings.IsImmediatly, 1, 0))
        ini.SetValue("General", "IsRapidStart", If(settings.IsRapidStart, 1, 0))
    End Sub

    Public Function LoadSettings() As Settings
        Dim ini As New ConfigParser(GetConfigFile)
        Dim pref As New Settings With {
            .IsSchedule = ini.GetValue("General", "IsSchedule", 0),
            .ScheduleTime = ini.GetValue("General", "ScheduleTime", ""),
            .LastTime = ini.GetValue("General", "LastTime", ""),
            .IsImmediatly = ini.GetValue("General", "IsImmediatly", 0),
            .IsRapidStart = ini.GetValue("General", "IsRapidStart", 0)
        }
        Return pref
    End Function

End Module
