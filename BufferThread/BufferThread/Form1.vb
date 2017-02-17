Imports System.Threading
Public Class Form1

    Dim taskCounter As Integer = 0
    Dim taskManager As TaskManager = Nothing

    Const DEFAULT_SYNC_TIME As Integer = 100
    Private timeLock As New Object
    Private lastSyncTime As Date = DateTime.MinValue

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnStart.Click
        lstSteps.Items.Clear()
        taskCounter = 0

        If (taskManager Is Nothing) Then
            taskManager = New TaskManager("iBuffer", nCores.Value)
            taskManager.Start()
        End If

        btnStop.Enabled = True
        btnStart.Enabled = False
        btnAddTask.Enabled = True
    End Sub

    Private Sub BtnStop_Click_2(sender As System.Object, e As System.EventArgs) Handles btnStop.Click

        btnStop.Enabled = False
        btnStart.Enabled = False
        btnAddTask.Enabled = False

        If (taskManager IsNot Nothing) Then
            taskManager.Wait()
            taskManager.Stops()
            taskManager = Nothing
        End If
        
        btnStop.Enabled = False
        btnStart.Enabled = True
        btnAddTask.Enabled = False
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        btnStop.Enabled = False
        btnAddTask.Enabled = False

        'Start the timer that will update the interface
        tmrUpdate.Enabled = True
    End Sub

    Private Delegate Sub UpdateControlDelegate(ByVal status As String, ByVal IsSync As Boolean)

    Private Function IsSyncTime() As Boolean
        Dim result As Boolean = False
        Dim currentTime As DateTime = DateTime.Now
        SyncLock timeLock
            If (currentTime.Subtract(lastSyncTime).Milliseconds >= DEFAULT_SYNC_TIME) Then
                result = True
                lastSyncTime = currentTime
            End If
        End SyncLock
        Return result
    End Function


    Private Sub UpdateTextStep(ByVal status As String, ByVal IsSync As Boolean)
        'If (IsSync) Then
        '    'lstSteps.Items.Add(status)
        'ElseIf (IsSyncTime()) Then
        '    Me.BeginInvoke(New UpdateControlDelegate(AddressOf UpdateTextStep), status, True) 'Con esto consume handlers
        '    Exit Sub
        'End If

    End Sub

    Private TextStatus As String = String.Empty
    Private Sub UpdateTextStatus(ByVal status As String, ByVal IsSync As Boolean)
        'If (IsSync) Then
        '    txtStatus.Text = status
        'ElseIf (IsSyncTime()) Then
        '    Me.BeginInvoke(New UpdateControlDelegate(AddressOf UpdateTextStatus), status, True) 'Con esto consume handlers
        '    Exit Sub
        'End If
        TextStatus = status
    End Sub

    Private Proc1Status As String = String.Empty
    Private Sub UpdateProc1Status(ByVal status As String, ByVal IsSync As Boolean)
        'If (IsSync) Then
        '    txtProc1.Text = status
        'ElseIf (IsSyncTime()) Then
        '    Me.BeginInvoke(New UpdateControlDelegate(AddressOf UpdateProc1Status), status, True) 'Con esto consume handlers
        '    Exit Sub
        'End If
        Proc1Status = status
    End Sub

    Private Proc2Status As String = String.Empty
    Private Sub UpdateProc2Status(ByVal status As String, ByVal IsSync As Boolean)
        'If (IsSync) Then
        '    txtProc2.Text = status
        'ElseIf (IsSyncTime()) Then
        '    Me.BeginInvoke(New UpdateControlDelegate(AddressOf UpdateProc2Status), status, True) 'Con esto consume handlers
        '    Exit Sub
        'End If
        Proc2Status = status
    End Sub

    Private Proc3Status As String = String.Empty
    Private Sub UpdateProc3Status(ByVal status As String, ByVal IsSync As Boolean)
        'If (IsSync) Then
        '    txtProc3.Text = status
        'ElseIf (IsSyncTime()) Then
        '    Me.BeginInvoke(New UpdateControlDelegate(AddressOf UpdateProc3Status), status, True) 'Con esto consume handlers
        '    Exit Sub
        'End If
        Proc3Status = status
    End Sub

    Private Proc4Status As String = String.Empty
    Private Sub UpdateProc4Status(ByVal status As String, ByVal IsSync As Boolean)
        'If (IsSync) Then
        '    txtProc4.Text = status
        'ElseIf (IsSyncTime()) Then
        '    Me.BeginInvoke(New UpdateControlDelegate(AddressOf UpdateProc4Status), status, True) 'Con esto consume handlers
        '    Exit Sub
        'End If
        Proc4Status = status
    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles btnClean.Click

        'Clean the variables
        TextStatus = String.Empty
        Proc1Status = String.Empty
        Proc2Status = String.Empty
        Proc3Status = String.Empty
        Proc4Status = String.Empty

        GC.Collect()
        GC.WaitForPendingFinalizers()
        GC.Collect()
    End Sub

    Private Sub ProcessTask(ByVal task As Task)
        UpdateTextStatus(task.Name, False)
        For i As Integer = 0 To 3
            Select Case task.CoreUsed
                Case 0
                    UpdateProc1Status(task.Name & ":" & i, False)
                Case 1
                    UpdateProc2Status(task.Name & ":" & i, False)
                Case 2
                    UpdateProc3Status(task.Name & ":" & i, False)
                    Throw New System.Exception("Error")
                Case 3
                    UpdateProc4Status(task.Name & ":" & i, False)
            End Select
            'System.Threading.Thread.Sleep(1)
            'System.Threading.Thread.Sleep(500)
            SuperShell("C:\TERESA\Kernel\_TeresaBatch.exe")
        Next
    End Sub

    Private Sub btnAddTask_Click(sender As System.Object, e As System.EventArgs) Handles btnAddTask.Click
        'If (taskManager IsNot Nothing) Then
        '    taskCounter += 1
        '    Dim taskName As String = "Task" & taskCounter
        '    UpdateTextStep("ADD TASK " & taskName, True)
        '    taskManager.AddTask(taskName, AddressOf ProcessTask)
        'End If

        If (taskManager IsNot Nothing) Then
            For i As Integer = 0 To 100000
                taskCounter += 1
                Dim taskName As String = "Task" & taskCounter
                UpdateTextStep("ADD TASK " & taskName, True)
                taskManager.AddTask(taskName, String.Empty, AddressOf ProcessTask)
                'taskManager.AddTask(taskName, String.Empty, New ProcessTaskEvent(AddressOf ProcessTask))
            Next
         
        End If
    End Sub

    Private Sub nCores_ValueChanged(sender As System.Object, e As System.EventArgs) Handles nCores.ValueChanged
        'Create 1 core and add 1 tasks
        If (taskManager Is Nothing) Then
            'taskManager = New TaskManager(nCores.Value)
            'taskManager.Start()

            'btnStop.Enabled = True
            'btnStart.Enabled = False
            'btnAddTask.Enabled = True
        Else
            If (taskManager.NumberOfcores <> nCores.Value) Then
                btnStop.Enabled = False
                btnStart.Enabled = False
                btnAddTask.Enabled = False

                UpdateTextStep("WAIT", True)
                taskManager.Wait()
                UpdateTextStep("STOP", True)
                taskManager.Stops()
                taskManager = Nothing
                'Start again
                taskManager = New TaskManager("Buffer", nCores.Value)
                taskManager.Start()

                btnStop.Enabled = True
                btnStart.Enabled = False
                btnAddTask.Enabled = True
            End If
        End If
    End Sub

    Private Sub Form1_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Create 1 core and add 1 tasks
        If (taskManager IsNot Nothing) Then
            taskManager.Stops(True)
            taskManager = Nothing
        End If

        'System.Environment.Exit(1)
    End Sub


    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        'txtDateTime.Text = DateTime.UtcNow.ToString("HH:mm:ss")

        'Update Bindings?
        TextStatus = TextStatus
        txtProc1.Text = Proc1Status
        txtProc2.Text = Proc2Status
        txtProc3.Text = Proc3Status
        txtProc4.Text = Proc4Status

        My.Application.DoEvents()
    End Sub

    Private Sub AddTaskToList(ByVal text As String)
        lstSteps.Items.Add(text)
        My.Application.DoEvents()
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles btnTEST.Click

        'Disable UI
        btnStop.Enabled = False
        btnStart.Enabled = False
        btnAddTask.Enabled = False
        btnTEST.Enabled = False

        lstSteps.Items.Clear()
        taskCounter = 0

        UpdateTextStep("STARTING TEST", True)
        AddTaskToList("STARTING TEST")

        'Start task manager
        If (taskManager Is Nothing) Then
            taskManager = New TaskManager("iBuffer", nCores.Value)
            taskManager.Start()
        End If

        'Start the test
        UpdateTextStep("Creating task 1", True)
        AddTaskToList("Creating task 1")
        taskManager.AddTask("task1", 3000, AddressOf ProcessTaskTime)
        UpdateTextStep("Creating task 2", True)
        AddTaskToList("Creating task 2")
        taskManager.AddTask("task2", 3000, AddressOf ProcessTaskTime)
        UpdateTextStep("Creating task 3", True)
        AddTaskToList("Creating task 3")
        taskManager.AddTask("task3", 3000, AddressOf ProcessTaskTime)

        'Wait until all the task has been completed
        AddTaskToList("Wait until all task have been finished")
        taskManager.Wait(True)

        AddTaskToList("Creating task 4")
        taskManager.AddTask("task4", 1000, AddressOf ProcessTaskTime)
        AddTaskToList("Creating task 5")
        taskManager.AddTask("task5", 1000, AddressOf ProcessTaskTime)
        AddTaskToList("Creating task 6")
        taskManager.AddTask("task6", 1000, AddressOf ProcessTaskTime)
        'taskManager.Wait()

        AddTaskToList("FINISHED TEST")

        'Stop task manager
        'If (taskManager IsNot Nothing) Then
        '    taskManager.Stops(True)
        '    taskManager = Nothing
        'End If

        'EnaBLE ui
        btnStop.Enabled = False
        btnStart.Enabled = False
        btnAddTask.Enabled = False
        btnTEST.Enabled = True
    End Sub


    Private Sub ProcessTaskTime(ByVal task As Task)
        'UpdateProc1Status("Start: " & task.Name, False)
        ''Thread.Sleep(Val(task.Parameters))
        Dim currentdate As DateTime = DateTime.Now
        Dim i As Integer = 0
        While DateTime.Now <= currentdate.AddMilliseconds(Val(task.Parameters))
            'Wait
            'Select Case task.CoreUsed
            '    Case 0
            '        UpdateProc1Status(task.Name & ":" & i, False)
            '    Case 1
            '        UpdateProc2Status(task.Name & ":" & i, False)
            '    Case 2
            '        UpdateProc3Status(task.Name & ":" & i, False)
            '        'Throw New System.Exception("Error")
            '    Case 3
            '        UpdateProc4Status(task.Name & ":" & i, False)
            'End Select
            'i += 1
        End While

        MsgBox(task.Name & " DONE!")
        'UpdateProc1Status("End:" & task.Name, False)
    End Sub

End Class
