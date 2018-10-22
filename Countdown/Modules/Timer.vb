Imports System.Windows.Forms

Module Timer

    Public timer As Timers.Timer
    Public Event TimesUp()

    Public Sub StartTimer()
        Dim pref As Settings = LoadSettings()
        If pref.ScheduleTime = "" Or pref.LastTime = "" Then Exit Sub

        Dim tim As Integer
        If pref.IsSchedule Then
            Dim time_set As New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day,
                                      pref.ScheduleTime.Split(":")(0), pref.ScheduleTime.Split(":")(1), 0)
            Dim span As TimeSpan = time_set - Date.Now
            tim = CInt(span.TotalSeconds) * 1000
        Else
            Dim hours As Integer = pref.LastTime.Split(":")(0)
            Dim minutes As Integer = pref.LastTime.Split(":")(1)
            Dim seconds As Integer = pref.LastTime.Split(":")(2)

            tim = (hours * 3600 + minutes * 60 + seconds) * 1000
        End If

        ' Time set has elapsed.
        If tim < 0 Then
            Exit Sub
        End If

        timer = New Timers.Timer With {
            .Interval = tim
        }
        AddHandler timer.Elapsed, AddressOf ElapsedHandler
        timer.Start()
    End Sub


    Private Sub ElapsedHandler(sender As Timers.Timer, e As Timers.ElapsedEventArgs)
        timer.Stop()
        RaiseEvent TimesUp()
    End Sub

End Module
