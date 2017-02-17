'*********************************************************************
' MODULE: TaskManager
' FILENAME: TaskManager.vb
' AUTHOR: Javier Santos
' DEPENDENCIES: None
'
' DESCRIPTION:
' This module deals with tasks using differents threads
'
' MODIFICATION HISTORY:
' 1.0.0 28-Dic-2015 JSA - Initial Version
'*********************************************************************

Imports System.Threading

''' <summary>
''' Status of the Tasks
''' </summary>
''' <remarks></remarks>
Public Enum TaskStatus
    Initial
    Running
    Finished
End Enum

''' <summary>
''' Delegated that will be raised when the the task is processing and completed
''' </summary>
''' <param name="task"></param>
''' <remarks></remarks>
Public Delegate Sub ProcessTaskEvent(ByVal task As Task)
'Public Delegate Sub TaskCompletedEvent(ByVal task As Task)

''' <summary>
''' Task Main class.
''' </summary>
''' <remarks></remarks>
Public Class Task
    'Event that will be raised when the task will be processed or completed
    Private ProcessTask As ProcessTaskEvent
    ' Public Event TaskCompleted As TaskCompletedEvent

    'Thread that will be created for each Task when it runs
    Private TaskThread As Thread = Nothing

    'Name, Status and the Result of the Task when status has finished
    Public Name As String = String.Empty
    Public Parameters As Object = Nothing
    Public Status As TaskStatus = TaskStatus.Initial
    Public CoreUsed As Integer = -1
    Public Result As Boolean = False

    'construcutor for the class #1
    Public Sub New(ByVal Name As String, ByVal taskToProcess As ProcessTaskEvent)
        Me.Name = Name
        'Get the deleagate Process to process
        ProcessTask = taskToProcess
    End Sub

    'Constructor for the class #2
    Public Sub New(ByVal Name As String, ByVal Parameters As Object, ByVal taskToProcess As ProcessTaskEvent)
        Me.New(Name, taskToProcess)
        Me.Parameters = Parameters
    End Sub

    Protected Overloads Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub '

    ''' <summary>
    ''' Close the Thread fo the task.
    ''' if aborted set the initial status to initial if not let the process task to set this parameter
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseThread(Optional ByVal Abort As Boolean = False)
        If (TaskThread IsNot Nothing) Then
            'Check if the thread need to be aborted or wait until the end
            If (Abort) Then
                'Abort and set the status to initial
                TaskThread.Abort()
                TaskThread.Join()
                Status = TaskStatus.Initial
            Else
                TaskThread.Join()
            End If
            ProcessTask = Nothing
            TaskThread = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Abort()
        'Check the running thread to abort if necessary
        CloseThread(True)
    End Sub

    ''' <summary>
    ''' Run the thread asyncronously.
    ''' TaskCompleted event will be raised when the task will be completed
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Run()
        'Check the running thread to abort if necessary
        CloseThread(True)
        'Create a new Thread for this Task
        TaskThread = New Thread(New ThreadStart(AddressOf ProcessThreadTask))
        TaskThread.IsBackground = True
        'Start the Task
        TaskThread.Start()
    End Sub

    ''' <summary>
    '''  Run the thread asyncronously but it will wait until the task completed entirely.
    ''' TaskCompleted event will be raised when the task will be completed
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RunAndWait() As Boolean
        'Check the running thread
        CloseThread(True)
        'Create a new Thread for this Task
        TaskThread = New Thread(New ThreadStart(AddressOf ProcessThreadTask))
        'Start the Task
        TaskThread.Start()
        'Wait until the thread has been completed and finish
        CloseThread()
        'Return the result
        Return Result
    End Function

    ''' <summary>
    ''' This is the method that will be executed in a new thread when the task will be executed
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProcessThreadTask()
        'By default set result to true 
        Result = True
        'Set the status of the task to running
        Status = TaskStatus.Running
        Try
            'Throw the delegate mothod that will be processed for this task. This methos is sync with this thread
            'ProcessTask.Invoke(Me)
            If (ProcessTask IsNot Nothing) Then ProcessTask(Me)
        Catch ex As Exception
            Logger.WriteError(Name, ex)
            'Set resuklt to false
            Result = False
        End Try
        'Set the status of the thread to finished
        Status = TaskStatus.Finished
        'Throw the complete event for the task
        'RaiseEvent TaskCompleted(Me)
    End Sub

End Class

Public Class Logger

    Public Shared AppFolder As String = My.Application.Info.DirectoryPath
    Private Shared ObjLock As New Object

    Public Shared Sub WriteError(ByVal Name As String, ByVal e As Exception)

        SyncLock ObjLock
            Dim LogFile As String = AppFolder & "\LoggerError_" & Name & ".log"
            Dim foLog As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(LogFile, True, System.Text.Encoding.UTF8)
            foLog.WriteLine("[" & Now.ToString("dd/MM/yyyy HH:mm:ss") & "] " & e.Message)
            foLog.WriteLine(e.StackTrace)
            foLog.Close()
            foLog.Dispose()
        End SyncLock

    End Sub

End Class

Public Class TaskManager
    'Pause Task ID. This is for a specific task on order to wait until the previous tasks have been completed.
    Public Const PAUSE_TASK As String = "PAUSE_TASK"
    'Number os Cores to process each Task
    Public Name As String = String.Empty
    Public NumberOfcores As Integer = 0
    'Thread that will check if there are any pecend Task to throw
    Private BufferThread As Thread = Nothing
    Private IsRunning As Integer = False
    'All task that will be added to the TaskManager in order to be dispathed
    Private TasksLock As New Object
    Private Tasks As New Queue(Of Task)
    'All Slot that will be available to use at the same time for the Taskmanager.
    Private SlotsLock As New Object
    Private Slots As New Dictionary(Of Integer, Task)


#Region "Constructors"

    Public Sub New(ByVal Name As String, ByVal NumberOfcores As Integer)
        'Initialize all variables
        Me.Name = Name
        Me.NumberOfcores = NumberOfcores
        'Create empty slots for each Core created
        For i As Integer = 0 To NumberOfcores - 1
            Slots.Add(i, Nothing)
        Next
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        'Stops all the track being running
        Stops(True)

        'Clear the slots (All the tasks have being stopped before)
        SyncLock SlotsLock
            Slots.Clear()
        End SyncLock
        'Cleat all the task that couldbe waiting for performing
        SyncLock TasksLock
            Tasks.Clear()
        End SyncLock

    End Sub 'Finalize

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Check if there are free slots and returns the position of the first one founded
    ''' Retuns -1 if there is no free slot
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFreeSlot() As Integer
        Dim result As Integer = -1
        SyncLock SlotsLock
            'Get a freee slot
            For Each i As Integer In Slots.Keys
                'Check if this slot are empty
                If (Slots(i) Is Nothing) Then
                    result = i
                    Exit For
                Else
                    'if not empty check if the task has finished
                    If (Slots(i).Status = TaskStatus.Finished) Then
                        result = i
                        Exit For
                    End If
                End If
            Next
        End SyncLock

        Return result
    End Function

    ''' <summary>
    ''' This is the main thread that will process all the task being queued.
    ''' If there are an empty slot (depending of the cores configured) this will throw the task.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProcessBuffer()
        'Loop unti the process stops
        While IsRunning
            Try
                'Check if there are Task availabe to throw
                If (HasTasks()) Then
                    'Check if there are free slots before throw the task
                    Dim slot As Integer = GetFreeSlot()
                    If (slot <> -1) Then
                        Dim nextTask As Task = Nothing
                        'Process the next queued Task
                        SyncLock TasksLock
                            'Get the next task queued
                            nextTask = Tasks.Dequeue()
                        End SyncLock

                        'Check if the current task it's the pause in order to wait until all current processing tasks have been completed
                        If (nextTask.Name = PAUSE_TASK) Then
                            'Wait until the current running tasks has been completed.
                            WaitForCurrentTasks()
                        Else
                            'Set the task into the free slot
                            SyncLock SlotsLock
                                Slots(slot) = nextTask
                            End SyncLock
                            'Finally run the task
                            nextTask.CoreUsed = slot
                            nextTask.Run()
                        End If
                    Else
                        'Sleep this thread until the next
                        Thread.Sleep(10)
                    End If
                Else
                    'Sleep this thread until the next
                    Thread.Sleep(10)
                End If
            Catch ex As Exception
                Logger.WriteError(Name, ex)
                'Error
            End Try
        End While

    End Sub

    ''' <summary>
    ''' This process will wait until all the current tasks have been finished
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub WaitForCurrentTasks()
        'This Task will wait until all the task will be completed
        Dim TaskFinished As Boolean = False
        While Not TaskFinished
            'Default set task finished to true
            TaskFinished = True
            'Check for slots that are currently being running
            SyncLock SlotsLock
                For Each i As Integer In Slots.Keys
                    'Check if this slot are empty
                    If (Slots(i) IsNot Nothing) Then
                        'if not empty check if the task has finished
                        If (Slots(i).Status <> TaskStatus.Finished) Then
                            TaskFinished = False
                        End If
                    End If
                Next
            End SyncLock
        End While
    End Sub

    Private Function HasTasks() As Boolean
        Dim result As Boolean = False
        'Lock the queue to not being used for other thread
        SyncLock TasksLock
            If (Tasks.Count > 0) Then
                result = True
            End If
        End SyncLock
        Return result
    End Function

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' This will run the main process all the Task being enqueued
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Start()
        'Check if there is another thread already been running
        Stops()
        'Create a new Thread for this Task
        BufferThread = New Thread(New ThreadStart(AddressOf ProcessBuffer))
        BufferThread.IsBackground = True
        'Start the Task
        IsRunning = True
        BufferThread.Start()
    End Sub

    ''' <summary>
    ''' This will Stop the main thread 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Stops(Optional ByVal Abort As Boolean = False)
        'Check if there is another thread already been running
        If (BufferThread IsNot Nothing) Then
            IsRunning = False
            If (Abort) Then
                SyncLock SlotsLock
                    'Abort all the Task already being running
                    For Each i As Integer In Slots.Keys
                        'Check if this slot are not empty
                        If (Slots(i) IsNot Nothing) Then
                            Slots(i).Abort()
                            'Slots(i) = Nothing
                        End If
                    Next
                End SyncLock
                'Abort  this current thread
                BufferThread.Abort()
                BufferThread.Join()
            Else
                'Wait all the Tasks
                Wait()
                'Wait until this task finish
                BufferThread.Join()
            End If
            BufferThread = Nothing
        End If
    End Sub

    Public Sub AddTask(ByVal Name As String, ByVal Parameters As Object, ByVal TaskToProcess As ProcessTaskEvent)
        'Lock the queue to not being used for other thread
        SyncLock TasksLock
            'Enqueue the Task
            Tasks.Enqueue(New Task(Name, Parameters, TaskToProcess))
        End SyncLock
    End Sub

    ''' <summary>
    ''' This process will wait until all the task have been finished
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Wait(Optional ByVal ContinueWithProcess As Boolean = False)

        'Check if the wait is not blocking the thread.
        If (ContinueWithProcess) Then
            'Add a task called pause_task in order to pause the task manager thread unitl all previous task before this one have been completed
            AddTask(PAUSE_TASK, Nothing, Nothing)
            'End this function
            Return
        End If

        'This sentence will wait until the queue will be empty 
        While HasTasks()
            'Wait 10 millseconds
            Thread.Sleep(10)
        End While

        'Once all the task of the queue are running, next wait until all current tasks have been finished
        WaitForCurrentTasks()
    End Sub

#End Region

End Class
