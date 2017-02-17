Public Class Form1

    Private Delegate Sub UpdateControlDelegate(ByVal status As String)

    Private MyThread As System.Threading.Thread = Nothing
    Private isProcessing As Boolean = False

    Private MyBuffer As New Buffer("MyBufferTest", 4)
    Private WithEvents MyBufferPool As New BufferPool("MyBufferTest")

    Public Sub Start()
        'Check if the buffer has started
        If (MyThread IsNot Nothing) Then Stops()
        'Enable the thread to Start the buffer processing
        isProcessing = True
        MyThread = New System.Threading.Thread(AddressOf MyprocessThread)
        MyThread.Start()
    End Sub

    Public Sub Stops()
        'Stops the buffer
        isProcessing = False
        'MyThread.Join()
        'MyThread = Nothing
    End Sub


    Private Sub UpdateStatus(ByVal status As String)
        If InvokeRequired Then
            Me.Invoke(New UpdateControlDelegate(AddressOf UpdateStatus), status)
            Exit Sub
        End If
        lblStatus.Text = status
        My.Application.DoEvents()
    End Sub

    Private Sub MyprocessThread()

        Dim i As Integer = 0

        While isProcessing
            'Call to the buffer to do the task async
            MyBuffer.AddProcess("Proceso número " & i.ToString())

            'Update the status
            UpdateStatus("Procesando número " & i.ToString())

            'Launch the process
            ' MyBuffer.LaunchProcessAsync()
            MyBuffer.LaunchAllAndWait()

            UpdateStatus("[OK]Procesado número " & i.ToString())

            'Sleep for the next execution
            'System.Threading.Thread.Sleep(100)

            i += 1
        End While

        'Wait until complete all the tasks
        MyBuffer.WaitUntilComplete()

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnTEST.Click
        For i As Integer = 0 To Int32.MaxValue
            ChangeTime(New DateTime(DateTime.Now.Ticks))
            WriteFile(New DateTime(DateTime.Now.Ticks))
        Next

        MsgBox("Done!")
    End Sub

    Private Sub btnTEST2_Click(sender As System.Object, e As System.EventArgs) Handles btnTEST2.Click

        If (Not isProcessing) Then
            btnTEST2.Text = "STOP"
            Start()
        Else
            Stops()
            btnTEST2.Text = "START"
        End If

    End Sub

    Private Sub btnLaunh_Click(sender As System.Object, e As System.EventArgs) Handles btnLaunh.Click

        UpdateStatus("INIT")

        'Add the processes to be computed
        For i As Integer = 0 To 1000
            'Call to the buffer to do the task async
            MyBuffer.AddProcess("Proceso número " & i.ToString())
        Next
        'Launch all the tasks
        MyBuffer.LaunchAllAndWait()

        UpdateStatus("END")

    End Sub

    Private Sub btnLaunchPool_Click(sender As System.Object, e As System.EventArgs) Handles btnLaunchPool.Click
        StartPool()
    End Sub


    Public Sub StartPool()
        'Check if the buffer has started
        If (MyThread IsNot Nothing) Then Stops()
        'Enable the thread to Start the buffer processing
        isProcessing = True
        MyThread = New System.Threading.Thread(AddressOf MyprocessThreadPool)
        MyThread.Start()
    End Sub

    Public Sub StopsPool()
        'Stops the buffer
        isProcessing = False
        MyThread.Join()
        MyThread = Nothing
    End Sub

    Private Sub BufferNOTAM_LocalProcesses(ByVal ProcessName As String) Handles MyBufferPool.LocalProcesses
        UpdateStatus(ProcessName)
    End Sub

    Private Sub MyprocessThreadPool()

        Dim i As Integer = 0
        Dim MaxCounter As Integer = Int32.MaxValue

        While isProcessing
            'Call to the buffer to do the task async
            MyBufferPool.Buffer_AddLocalProcess("Proceso número " & i.ToString())

            i += 1

            If (i = MaxCounter) Then
                isProcessing = False
            End If

        End While

    End Sub


End Class
