Public Class Form1

    Private Sub dtpFromDay_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFromDay.ValueChanged
        dtpToDay.Value = dtpFromDay.Value
        txtFromDOY.Text = Format(dtpFromDay.Value.DayOfYear, "000")
        txtToDOY.Text = txtFromDOY.Text
    End Sub

    Private Sub dtpToDay_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpToDay.CloseUp
        If dtpToDay.Value < dtpFromDay.Value Then
            MsgBox("End date cannot be lower than Start date!", vbCritical, "Error")
            dtpToDay.Value = dtpFromDay.Value
        End If
    End Sub

    Private Sub dtpToDay_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpToDay.ValueChanged
        txtToDOY.Text = Format(dtpToDay.Value.DayOfYear, "000")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        btnCreate.Enabled = False

        Dim StartDate As Date = dtpFromDay.Value
        Dim EndDate As Date = dtpToDay.Value

        Dim OutputsFolder As String = My.Application.Info.DirectoryPath & "\Outputs\"

        For Date_Counter As Integer = 0 To EndDate.Subtract(StartDate).Days
            Dim DateUsed As Date = StartDate.AddDays(Date_Counter)

            'Create folders
            OutputsFolder = My.Application.Info.DirectoryPath & "\Outputs\" & DateUsed.ToString("yyyy") & "\" & Format(DateUsed.DayOfYear, "000")
            If Not My.Computer.FileSystem.DirectoryExists(OutputsFolder) Then My.Computer.FileSystem.CreateDirectory(OutputsFolder)
        Next

        btnCreate.Enabled = True
    End Sub
End Class
