Imports System.ComponentModel

Public Class Buffer
    Event ProcessCompleted(ByVal ProcessName As String)

    Private AppFolder As String = My.Application.Info.DirectoryPath

    Public Const DEFAULT_TIMEOUT As Integer = Int32.MaxValue

    Private BufferName As String = String.Empty
    Private Slots As New List(Of BackgroundWorker)
    Private lstProcess As New Queue(Of String)


    Public Sub New(ByVal pBufferName As String, ByVal numberOfSlots As Integer)
        'Set the Name of the buffer
        BufferName = pBufferName
        'Create slots as Cores
        For i As Integer = 1 To numberOfSlots
            Dim slot As New BackgroundWorker()
            AddHandler slot.DoWork, AddressOf BackgroundProcessor_DoWork
            AddHandler slot.RunWorkerCompleted, AddressOf BackgroundProcessor_RunWorkerCompleted
            Slots.Add(slot)
        Next
    End Sub

    Protected Overrides Sub Finalize()
        For Each slot As System.ComponentModel.BackgroundWorker In Slots
            RemoveHandler slot.DoWork, AddressOf BackgroundProcessor_DoWork
            RemoveHandler slot.RunWorkerCompleted, AddressOf BackgroundProcessor_RunWorkerCompleted
        Next
        Slots.Clear()
    End Sub

    Private Function GetFreeSlot() As BackgroundWorker
        Dim result As BackgroundWorker = Nothing
        Dim currentDate As DateTime = DateTime.Now
        'Wait until free slot or timeout
        While (result Is Nothing AndAlso DateTime.Now.Subtract(currentDate).Milliseconds < DEFAULT_TIMEOUT)
            'Look for a free slot
            For Each slot As BackgroundWorker In Slots
                If (Not slot.IsBusy) Then
                    result = slot
                    Exit For
                End If
            Next
            'Check if an slot has been released
            If (result Is Nothing) Then
                System.Threading.Thread.Sleep(10)
            End If
        End While

        Return result
    End Function

    Public Function LaunchProcessAsync(ByVal ProcessTask As String) As Boolean
        'Find the first free slot
        Dim slot As BackgroundWorker = GetFreeSlot()
        If (slot IsNot Nothing) Then
            ' Execute, if it exists, the next processing in the background
            slot.RunWorkerAsync(ProcessTask)
            'Returns true
            Return True
        Else
            'time out finding an slot
            Return False
        End If

    End Function

    Public Function WaitUntilComplete() As Boolean
        Dim IsSlotRunning As Boolean = True
        Dim currentDate As DateTime = DateTime.Now

        While (IsSlotRunning AndAlso DateTime.Now.Subtract(currentDate).Milliseconds < DEFAULT_TIMEOUT)
            'Initialize the variable to look for new state 
            IsSlotRunning = False
            For Each slot As BackgroundWorker In Slots
                If (slot.IsBusy) Then
                    IsSlotRunning = True
                    Exit For
                End If
            Next
        End While

        If (IsSlotRunning) Then
            'End with time out
            Return False
        Else
            'All processed has ended
            Return True
        End If

    End Function

    Public Sub AddProcess(ByVal ProcessTask As String)
        'Add the task into the queue
        lstProcess.Enqueue(ProcessTask)
    End Sub

    Public Function LaunchAllAndWait() As Boolean
        'Execute all the task and wait
        While lstProcess.Count > 0
            'Find the first free slot
            Dim slot As BackgroundWorker = GetFreeSlot()
            If (slot IsNot Nothing) Then
                ' Execute, if it exists, the next processing in the background
                slot.RunWorkerAsync(lstProcess.Dequeue())
            Else
                Return False
            End If
        End While

        If (WaitUntilComplete()) Then
            'All task completed
            Return True
        Else
            'Returned an error
            Return False
        End If
    End Function

    Private Sub BackgroundProcessor_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

        e.Result = e.Argument.ToString()
    End Sub


    Private Sub BackgroundProcessor_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        If e.Error IsNot Nothing Then
            Dim LogFile As String = AppFolder & "\BufferError_" & BufferName & ".log"
            Dim foLog As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(LogFile, True, System.Text.Encoding.UTF8)
            foLog.WriteLine("[" & Now.ToString("dd/MM/yyyy HH:mm:ss") & "] " & e.Error.Message)
            foLog.WriteLine(e.Error.StackTrace)
            foLog.Close()
            foLog.Dispose()
        End If
        'Raise the event completed
        RaiseEvent ProcessCompleted(e.Result)
    End Sub
End Class
