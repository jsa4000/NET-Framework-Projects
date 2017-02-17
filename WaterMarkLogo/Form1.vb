Public Class Form1

    Dim LocalServerFolder As String = My.Application.Info.DirectoryPath & "\Outputs"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not My.Computer.FileSystem.DirectoryExists(LocalServerFolder) Then
            My.Computer.FileSystem.CreateDirectory(LocalServerFolder)
        End If

        'Start Painting the graphics 
        Dim buffer As New TaskManager("Buffer", nCores.Value)
        buffer.Start()

        For i As Integer = 0 To nImagesThreads.Value - 1
            buffer.AddTask("Sample00" & i, txtImagePath.Text & "\sample00.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample01" & i, txtImagePath.Text & "\sample01.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample02" & i, txtImagePath.Text & "\sample02.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample03" & i, txtImagePath.Text & "\sample03.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample04" & i, txtImagePath.Text & "\sample04.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample05" & i, txtImagePath.Text & "\sample05.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample06" & i, txtImagePath.Text & "\sample06.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample07" & i, txtImagePath.Text & "\sample07.png", AddressOf ExecuteGraphicsProcess)
        Next

        'Wait until all task completed
        buffer.Wait()
        buffer.Stops()
        buffer = Nothing

        MessageBox.Show("Done!")
    End Sub


    Private Sub ExecuteGraphicsProcess(ByVal task As Task)

        Dim filename As String = My.Computer.FileSystem.GetName(task.Parameters)
        'Check the new method
        If (chkNewMethod.Checked) Then
            LogoMarker.Instance.MarkImage(task.Parameters, LogoMarker.ImagesFormat.ifPNG,
                       LogoMarker.ImagePosition.TopLeft, 15, 50, LocalServerFolder & "\" & task.Name & "_" & filename)
        Else
            LogoMarkerOriginal.Instance.MarkImage_FreeImage(task.Parameters, LogoMarkerOriginal.ImageFormat.ifPNG,
                                LogoMarkerOriginal.ImagePosition.TopLeft, 15, 50, LocalServerFolder & "\" & task.Name & "_" & filename)

        End If


    End Sub


    Private Sub ExecuteProcess(ByVal task As Task)

        If Not My.Computer.FileSystem.DirectoryExists(LocalServerFolder) Then
            My.Computer.FileSystem.CreateDirectory(LocalServerFolder)
        End If

        'Start Painting the graphics 
        Dim buffer As New TaskManager("Buffer", nCores.Value)
        buffer.Start()

        Dim folder As String = My.Application.Info.DirectoryPath & "\settings"

        For i As Integer = 0 To nImagesThreads.Value - 1
            buffer.AddTask("Sample00" & i, folder & "\sample00.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample01" & i, folder & "\sample01.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample02" & i, folder & "\sample02.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample03" & i, folder & "\sample03.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample04" & i, folder & "\sample04.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample05" & i, folder & "\sample05.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample06" & i, folder & "\sample06.png", AddressOf ExecuteGraphicsProcess)
            buffer.AddTask("Sample07" & i, folder & "\sample07.png", AddressOf ExecuteGraphicsProcess)
        Next

        'Wait until all task completed
        buffer.Wait()
        buffer.Stops()
        buffer = Nothing

        MessageBox.Show("Done!")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtImagePath.Text = My.Application.Info.DirectoryPath & "\settings"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Start Painting the graphics 
        Dim buffer As New TaskManager("Buffer", 1)
        buffer.Start()

        buffer.AddTask("Main", Nothing, AddressOf ExecuteProcess)

        ''Wait until all task completed
        'buffer.Wait()
        'buffer.Stops()
        'buffer = Nothing

        'MessageBox.Show("Done!")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        For i As Integer = 0 To 1000
            LogoMarker.MergeImages(txtImagePath.Text & "\sample00.png", txtImagePath.Text & "\sampletrans02.png", LocalServerFolder & "\" & "test.png")
            LogoMarker.MergeImages(txtImagePath.Text & "\sample00.png", txtImagePath.Text & "\trolltunga.jpg", LocalServerFolder & "\" & "test2.png")
            LogoMarker.MergeImages(txtImagePath.Text & "\sample00.png", txtImagePath.Text & "\logo.png", LocalServerFolder & "\" & "test3.png")

            LogoMarker.Instance.MarkImage(txtImagePath.Text & "\sample00.png", LogoMarker.ImagesFormat.ifPNG,
                               LogoMarker.ImagePosition.Center, 15, 50, LocalServerFolder & "\" & "mitest00.png")
            LogoMarker.Instance.MarkImage(txtImagePath.Text & "\sample01.png", LogoMarker.ImagesFormat.ifPNG,
                               LogoMarker.ImagePosition.TopLeft, 15, 50, LocalServerFolder & "\" & "mitest01.png")
            LogoMarker.Instance.MarkImage(txtImagePath.Text & "\sample02.png", LogoMarker.ImagesFormat.ifPNG,
                               LogoMarker.ImagePosition.BottomRight, 15, 50, LocalServerFolder & "\" & "mitest02.png")
            LogoMarker.Instance.MarkImage(txtImagePath.Text & "\sample03.png", LogoMarker.ImagesFormat.ifPNG,
                               LogoMarker.ImagePosition.BottomLeft, 15, 50, LocalServerFolder & "\" & "mitest03.png")


            My.Computer.FileSystem.CopyFile(txtImagePath.Text & "\sample01.png", LocalServerFolder & "\" & "sample01.png", True)
            LogoMarker.MergeImages(LocalServerFolder & "\" & "sample01.png", txtImagePath.Text & "\logo.png", LocalServerFolder & "\" & "sample01.png")

        Next




        MessageBox.Show("Done!")

    End Sub
End Class
