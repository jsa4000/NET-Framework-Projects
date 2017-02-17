Module ExecCommand
    Private AppFolder As String = My.Application.Info.DirectoryPath
    Private AppAssemblyName As String = My.Application.Info.AssemblyName

    ' SuperShell returned values:
    ' 0: process ended
    ' 1: process killed
    ' 2: process started and function exited
    Public Function SuperShell(ByVal ProcessExecutable As String, Optional ByVal ProcessArguments As String = "", _
                          Optional ByVal Wait As Boolean = False, Optional ByVal TimeOutMiliseconds As Integer = Int32.MaxValue, _
                          Optional ByVal WindowStyle As System.Diagnostics.ProcessWindowStyle = ProcessWindowStyle.Normal) As Integer
        Dim TheProcess As New System.Diagnostics.Process
        TheProcess.StartInfo.FileName = ProcessExecutable
        TheProcess.StartInfo.Arguments = ProcessArguments
        TheProcess.StartInfo.WindowStyle = WindowStyle
        If Wait Then
            TheProcess.Start()
            If TheProcess.WaitForExit(TimeOutMiliseconds) Then
                ' Process Ended
                SuperShell = 0
            Else
                ' Kill Process
                TheProcess.Kill()
                SuperShell = 1
            End If
            TheProcess.Close()
            TheProcess.Dispose()
        Else
            TheProcess.EnableRaisingEvents = True
            AddHandler TheProcess.Exited, AddressOf TheProcess_Exited
            TheProcess.Start()
            SuperShell = 2
        End If
    End Function

    Private Sub TheProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs)
        sender.Close()
        sender.Dispose()
    End Sub

    Public Sub ExecBat(ByVal CmdLine As String, Optional ByVal TimeOut As Integer = Int32.MaxValue)
        Dim BatFile As String = AppFolder & "\TempBat_" & _
            AppAssemblyName & "_" & Now.ToString("yyyyMMddHHmmss") & "_"

        Dim i As Integer = 0
        While My.Computer.FileSystem.FileExists(BatFile & i.ToString & ".bat")
            i += 1
        End While
        BatFile &= i.ToString & ".bat"

        Dim fo_Bat As System.IO.StreamWriter
        fo_Bat = My.Computer.FileSystem.OpenTextFileWriter(BatFile, False, System.Text.Encoding.ASCII)
        fo_Bat.WriteLine(CmdLine)
        fo_Bat.Close()
        fo_Bat.Dispose()

        SuperShell(BatFile, , True, TimeOut, ProcessWindowStyle.Hidden)

        My.Computer.FileSystem.DeleteFile(BatFile)
    End Sub

End Module
