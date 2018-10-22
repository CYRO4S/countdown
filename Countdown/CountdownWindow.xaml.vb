Class CountDownWindow

    Private timer As Timers.Timer

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        timer = New Timers.Timer With {
            .Interval = 1000
        }

        AddHandler timer.Elapsed, AddressOf ElapsedHandler
        timer.Start()
    End Sub

    Private Sub ElapsedHandler(sender As Timers.Timer, e As Timers.ElapsedEventArgs)
        Me.Dispatcher.Invoke(New Action(
            Sub()

                If proMain.Value > 96 Then
                    timer.Stop()
                    DoShutdown()
                    Exit Sub
                End If

                proMain.Value += 3.33
            End Sub))
    End Sub

    Private Sub DoShutdown()
        Dim pref As Settings = LoadSettings()
        If pref.IsImmediatly Then
            If pref.IsRapidStart Then
                Process.Start("shutdown.exe", "/s /hybrid /t 0")
                End
            Else
                Process.Start("shutdown.exe", "/s /t 0")
                End
            End If
        Else
            If pref.IsRapidStart Then
                Process.Start("shutdown.exe", "/s /hybrid")
                End
            Else
                Process.Start("shutdown.exe", "/s")
                End
            End If
        End If
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        timer.Stop()
    End Sub
End Class
