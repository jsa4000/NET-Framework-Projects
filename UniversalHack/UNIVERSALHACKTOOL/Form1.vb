Public Class Form1


    Dim hack As UNIVERSALHack = Nothing

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        hack = New UNIVERSALHack(txtAppFolder.Text, txtFilesFolder.Text)
        hack.Open()
    End Sub

    Private Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
        If (hack IsNot Nothing) Then
            hack.ChangeParent(panelApp)
        End If
    End Sub

    Private Sub btnHide_Click(sender As Object, e As EventArgs) Handles btnHide.Click
        If (hack IsNot Nothing) Then
            If (btnHide.Text = "HIDE") Then
                hack.Hide(True)
                btnHide.Text = "SHOW"
            Else
                hack.Hide(False)
                btnHide.Text = "HIDE"
            End If
        End If
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If (hack IsNot Nothing) Then
            hack.StartProcess()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If (hack IsNot Nothing) Then
            hack.Close()
        End If
    End Sub

    Private Sub btnAll_Click(sender As Object, e As EventArgs) Handles btnAll.Click
        hack = New UNIVERSALHack(txtAppFolder.Text, txtFilesFolder.Text)
        hack.Open()
        If (hack.IsOpen) Then
            UNIVERSALHack.SetWindowLong(hack.Handle, UNIVERSALHack.GWL_STYLE, UNIVERSALHack.GetWindowLong(hack.Handle, UNIVERSALHack.GWL_STYLE) And (Not UNIVERSALHack.WS_CAPTION))
            UNIVERSALHack.SetParent(hack.Handle, panelApp.Handle)
            UNIVERSALHack.MoveWindow(hack.Handle, 0, 0, panelApp.Width, panelApp.Height, True)
            'UNIVERSALHack.ShowWindow(hack.Handle, UNIVERSALHack.SW_HIDE)
            hack.StartProcess()
            hack.Close()
        End If
    End Sub
End Class
