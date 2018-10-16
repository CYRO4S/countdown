Imports System.Windows.Forms

Module TrayIcon

    Dim ico As NotifyIcon
    Dim wndConfig As ConfigWindow

    Public Sub ShowTrayIcon()
        If ico IsNot Nothing Then Exit Sub

        ico = New NotifyIcon With {
            .Text = "Aronnax",
            .Icon = New System.Drawing.Icon(GetApplicationDir() & "Resources\timer_tray.ico")
        }

        Dim menu As New ContextMenu

        Dim mnuShowMainWindow As New MenuItem With {
            .Text = "偏好设置 (&P)..."
        }
        AddHandler mnuShowMainWindow.Click, AddressOf ShowMainWindowHandler
        menu.MenuItems.Add(mnuShowMainWindow)

        menu.MenuItems.Add("-")

        Dim mnuExit As New MenuItem With {
            .Text = "退出 (&X)..."
        }
        AddHandler mnuExit.Click, AddressOf ExitHandler
        menu.MenuItems.Add(mnuExit)

        ico.ContextMenu = menu
        ico.Visible = True

    End Sub

    Private Sub ExitHandler(sender As Object, e As EventArgs)
        End
    End Sub

    Private Sub ShowMainWindowHandler(sender As Object, e As EventArgs)
        wndConfig = New ConfigWindow
        wndConfig.Show()
    End Sub

End Module
