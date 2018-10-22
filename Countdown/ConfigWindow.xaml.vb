Public Class ConfigWindow

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        lstMenu.SelectedIndex = 0

        If IO.File.Exists(GetConfigFile) Then
            Dim pref As Settings = LoadSettings()

            If pref.IsSchedule Then
                radSchedule.IsChecked = True
            Else
                radLast.IsChecked = True
            End If

            If pref.IsImmediatly Then
                radImme.IsChecked = True
            Else
                radDelay.IsChecked = True
            End If

            txtSchedule.Text = pref.ScheduleTime
            txtLast.Text = pref.LastTime

            togRapidStart.IsChecked = pref.IsRapidStart

        End If
        CheckRapidStartCompatibility()
    End Sub

    Private Sub lstMenu_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim item As ListBoxItem = lstMenu.SelectedItem
        Select Case item.Tag
            Case "optSchedule"
                grdOptions.Visibility = Visibility.Hidden
                grdSchedule.Visibility = Visibility.Visible
            Case "optOptions"
                grdSchedule.Visibility = Visibility.Hidden
                grdOptions.Visibility = Visibility.Visible
        End Select
    End Sub

    Private Sub radLast_Checked(sender As Object, e As RoutedEventArgs)
        txtSchedule.IsEnabled = False
        txtLast.IsEnabled = True
    End Sub

    Private Sub radLast_Unchecked(sender As Object, e As RoutedEventArgs)
        txtLast.IsEnabled = False
        txtSchedule.IsEnabled = True
    End Sub

    Private Sub CheckRapidStartCompatibility()
        If Not IsHybridAvailiable() Then
            togRapidStart.IsChecked = False
            togRapidStart.IsEnabled = False
            pnlUnsupported.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        Dim pref As New Settings With {
            .IsSchedule = radSchedule.IsChecked,
            .ScheduleTime = txtSchedule.Text,
            .LastTime = txtLast.Text,
            .IsImmediatly = radImme.IsChecked,
            .IsRapidStart = togRapidStart.IsChecked
        }
        SaveSettings(pref)
        ShowTrayIcon()
    End Sub
End Class
