Public Class BufferPool
    Event LocalProcesses(ByVal ProcessName As String)

    Public WithEvents BackgroundProcessor As New System.ComponentModel.BackgroundWorker()
    Private AppFolder As String = My.Application.Info.DirectoryPath
    Private BufferName As String = String.Empty
    Private lstProcess As New List(Of String)

    Private isProcessing As Boolean = False
    Private IsBusy As Boolean = False

    Public Sub New(ByVal pBufferName As String)
        'Set the Name of the buffer
        BufferName = pBufferName
        'Set worker to report progress
        BackgroundProcessor.WorkerReportsProgress = False
        BackgroundProcessor.WorkerSupportsCancellation = True
    End Sub

    Public Sub Buffer_AddProcess(ByVal ProcessExecutable As String, Optional ByVal ProcessArguments As String = "", _
                                 Optional ByVal ChangeFolder As Boolean = False)
        lstProcess.Add(ProcessExecutable & "%%%%%" & ProcessArguments & "%%%%%" & Math.Abs(Val(ChangeFolder)).ToString)
        LaunchProcess()
    End Sub

    Public Sub Buffer_AddLocalProcess(ByVal query As String)
        lstProcess.Add(query & "%%%%%" & "%%%%%" & "local")
        LaunchProcess()
    End Sub

    Private Sub LaunchProcess()
        'If Not BackgroundProcessor.IsBusy Then
        While isProcessing
            'Wait until not is busy
        End While

        If Not BackgroundProcessor.IsBusy Then
            If lstProcess.Count > 0 Then
                ' Execute, if it exists, the next processing in the background
                IsBusy = True
                isProcessing = True
                If Not BackgroundProcessor.IsBusy Then
                    BackgroundProcessor.RunWorkerAsync(lstProcess(0))
                    lstProcess.RemoveAt(0)
                End If
            Else
                IsBusy = False
            End If
        End If
    End Sub

    Private Sub BackgroundProcessor_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundProcessor.DoWork
        Dim SeparatorString() As String = {"%%%%%"}
        Dim ProcessQuery As String = e.Argument.ToString.Split(SeparatorString, StringSplitOptions.None)(0)
        Dim ProcessArguments As String = e.Argument.ToString.Split(SeparatorString, StringSplitOptions.None)(1)
        Dim ProcessOptions As String = e.Argument.ToString.Split(SeparatorString, StringSplitOptions.None)(2)

     If ProcessOptions = "local" Then
            RaiseEvent LocalProcesses(ProcessQuery)
        Else
            If ProcessOptions = "1" Then
                ChDrive(ProcessQuery.Substring(0, 1))
                ChDir(My.Computer.FileSystem.GetFileInfo(ProcessQuery).DirectoryName)
            End If
            SuperShell(ProcessQuery, ProcessArguments, True, , ProcessWindowStyle.Minimized)
        End If
    End Sub

    Private Sub BackgroundProcessor_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundProcessor.RunWorkerCompleted
        If Not e.Error Is Nothing Then
            Dim LogFile As String = AppFolder & "\BufferError_" & BufferName & ".log"
            Dim foLog As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(LogFile, True, System.Text.Encoding.UTF8)
            foLog.WriteLine("[" & Now.ToString("dd/MM/yyyy HH:mm:ss") & "] " & e.Error.Message)
            foLog.WriteLine(e.Error.StackTrace)
            foLog.Close()
            foLog.Dispose()
        End If
        isProcessing = False
        'BackgroundProcessor.CancelAsync()
        LaunchProcess()
    End Sub
End Class
