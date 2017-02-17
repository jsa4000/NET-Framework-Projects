Module DeleteFunctions

    Private App_Folder As String = My.Application.Info.DirectoryPath

    Public Sub DeleteFilesAndFoldersRecursively(ByVal target_dir As String)
        For i As Integer = 0 To 2
            Try
                For Each file As String In My.Computer.FileSystem.GetFiles(target_dir)
                    DeleteFile(file)
                Next

                For Each subDir As String In My.Computer.FileSystem.GetDirectories(target_dir)
                    DeleteFilesAndFoldersRecursively(subDir)
                Next

                Threading.Thread.Sleep(1) ' This makes the difference between whether it works or not. Sleep(0) is not enough.
                My.Computer.FileSystem.DeleteDirectory(target_dir, FileIO.DeleteDirectoryOption.DeleteAllContents)

                'Exit if complete
                Exit For
            Catch ex As Exception
                WriteStatus(ex.Message)
                Threading.Thread.Sleep(1) ' This makes the difference between whether it works or not. Sleep(0) is not enough.
            End Try
        Next
    End Sub

    Public Sub DeleteFile(ByVal file As String)
        For i As Integer = 0 To 2
            Try
                My.Computer.FileSystem.DeleteFile(file)
                'Exit if complete
                Exit For
            Catch ex As Exception
                WriteStatus(ex.Message)
                Threading.Thread.Sleep(1) ' This makes the difference between whether it works or not. Sleep(0) is not enough.
            End Try
        Next
    End Sub

    Public Sub WriteStatus(ByVal StatusText As String)
        Dim LogFile As String = App_Folder & "\APCM.log"
        Dim foLog As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(LogFile, True, System.Text.Encoding.UTF8)
        foLog.WriteLine("[" & Now.ToString("dd/MM/yyyy HH:mm:ss") & "] " & StatusText)
        foLog.Close()
        foLog.Dispose()

        ' Cut log every 10 MBytes
        If My.Computer.FileSystem.GetFileInfo(LogFile).Length > 10485760 Then
            Dim i As Integer = 1
            While My.Computer.FileSystem.FileExists(App_Folder & "\APCM." & i.ToString & ".log")
                i += 1
            End While
            My.Computer.FileSystem.RenameFile(LogFile, "APCM." & i.ToString & ".log")
        End If
    End Sub


End Module
