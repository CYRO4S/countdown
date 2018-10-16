Class Application


    Private Sub Application_Startup(sender As Object, e As StartupEventArgs)
        If Not IO.File.Exists(GetConfigFile) Then
            Dim wndConfig As New ConfigWindow
            wndConfig.Show()
        Else
            ShowTrayIcon()
        End If
    End Sub



End Class
