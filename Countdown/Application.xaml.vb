Class Application


    Private Sub Application_Startup(sender As Object, e As StartupEventArgs)

        ' check resources & directories
        If Not IO.Directory.Exists(GetApplicationDir() & "\Config") Then
            Try
                IO.Directory.CreateDirectory(GetApplicationDir() & "\Config")
            Catch ex As Exception
                MessageBox.Show("无法创建程序所需目录。程序将退出。", "",
                                MessageBoxButton.OK, MessageBoxImage.Error)
                End
            End Try
        End If

        If Not IO.File.Exists(GetApplicationDir() & "\Resources\timer_tray.ico") Then
            MessageBox.Show("找不到程序所需资源，程序将退出。", "",
                            MessageBoxButton.OK, MessageBoxImage.Error)
            End
        End If

        If Not IO.File.Exists(GetConfigFile) Then
            Dim wndConfig As New ConfigWindow
            wndConfig.Show()
        Else
            ShowTrayIcon()
        End If
    End Sub



End Class
