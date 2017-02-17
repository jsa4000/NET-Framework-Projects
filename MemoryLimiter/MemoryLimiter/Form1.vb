Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            Dim pID As Integer = Integer.Parse(txtProcessID.Text)
            If (MemoryManagement.ProcessExists(pID)) Then
                MemoryManagement.LimitSizeProcess(pID, nMaximun.Value, nMinimun.Value)
            Else
                MsgBox("Error: Process doesn't exit.")
            End If
        Catch
            MsgBox("Error: Process ID format Incorrect.")
        End Try
    End Sub

End Class
