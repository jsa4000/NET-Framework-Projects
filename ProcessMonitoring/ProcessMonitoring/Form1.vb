Public Class Form1

    Private Const START_TAG As String = "Start"
    Private Const STOP_TAG As String = "Stop"

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If (Not String.IsNullOrEmpty(txtProcessName.Text)) Then
            If (btnProcess.Text = START_TAG) Then
                btnProcess.Text = STOP_TAG
                tmrProcess.Enabled = True
            Else
                'Stop the process
                btnProcess.Text = START_TAG
                tmrProcess.Enabled = False
            End If
        Else
            MsgBox("You must enter a Name of the process to Monitorize", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnProcess.Text = START_TAG
        tmrProcess.Interval = 1000
    End Sub

    Private Sub tmrProcess_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrProcess.Tick
        'Start the prcoess
        For Each p As Process In Process.GetProcesses()
            If (p.ProcessName = txtProcessName.Text) Then
                WriteFile(p.ProcessName, p.PrivateMemorySize64, p.PeakWorkingSet64, p.VirtualMemorySize64, p.WorkingSet64, p.HandleCount)
            End If
        Next

    End Sub

    Public Sub WriteFile(ByVal ProcessName As String, ByVal PrivateMemorySize As Integer, ByVal PeakWorkingSet As Integer, _
                         ByVal VirtualMemorySize As Integer, ByVal WorkingSet As Integer, ByVal HandleCount As Integer)
        Dim FileExists As Boolean = True
        Dim pathFile As String = My.Application.Info.DirectoryPath & "\MemoryLeak_" & Now.ToString("yyyyMMdd") & ".txt"


        If (Not My.Computer.FileSystem.FileExists(pathFile)) Then FileExists = False
        Dim FileStream As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(pathFile, True, System.Text.Encoding.ASCII)

        If (Not FileExists) Then
            FileStream.WriteLine("Date" & vbTab & "Process Name" & vbTab & "Private Memory Size" & vbTab & "Virtual Memory Size" & vbTab & "Peak Working Set" & vbTab & "Working Set" & vbTab & "Handles")
        End If

        FileStream.WriteLine(Now.ToString("dd/MM/yyyy HH:mm:ss") & vbTab & ProcessName & vbTab & PrivateMemorySize & vbTab & VirtualMemorySize & vbTab & PeakWorkingSet & vbTab & WorkingSet & vbTab & HandleCount)
        FileStream.Close()
    End Sub
End Class
