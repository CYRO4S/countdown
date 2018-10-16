Imports System.Windows.Forms

Module TrayIcon

    Dim ico As NotifyIcon

    Public Sub ShowTrayIcon()
        If ico IsNot Nothing Then Exit Sub

        ico = New NotifyIcon With {
            .Text = "Aronnax",
            .Icon = New System.Drawing.Icon(GetApplicationDir() & "Resources\package_tray.ico")
        }

        Dim menu As New ContextMenu


        menu.MenuItems.Add("-")

        Dim mnuShowMainWindow As New MenuItem With {
            .Text = "偏好设置 (&P)..."
        }
        AddHandler mnuShowMainWindow.Click, AddressOf ShowMainWindowHandler
        menu.MenuItems.Add(mnuShowMainWindow)

        Dim mnuExit As New MenuItem With {
            .Text = "退出 (&X)..."
        }
        AddHandler mnuExit.Click, AddressOf ExitHandler
        menu.MenuItems.Add(mnuExit)

        ico.ContextMenu = menu
        ico.Visible = True

    End Sub

    Private Sub ExitHandler(sender As Object, e As EventArgs)

    End Sub

    Private Sub ShowMainWindowHandler(sender As Object, e As EventArgs)

    End Sub

End Module
