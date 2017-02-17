Module ModuleLeak

    Dim TimeLeak As DateTime = DateTime.Now
    Dim TimeLeakStr As String = String.Empty

    Public Sub ChangeTime(ByVal currentTime As DateTime)
        TimeLeak = currentTime
        TimeLeakStr = currentTime.ToString() & "_Str" & currentTime.Millisecond
    End Sub

    Public Sub WriteFile(ByVal currentTime As DateTime)
        Dim f_Gplot_Script As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(My.Application.Info.DirectoryPath & "\" & currentTime.ToString("yyyyMMddhhmmss") & currentTime.Millisecond & ".txt", False, System.Text.Encoding.ASCII)
        f_Gplot_Script.WriteLine("set term png" & currentTime.Ticks & " size 960, 720" & currentTime.Millisecond & "milliseconds")
        f_Gplot_Script.WriteLine("plot file0 u 2:(($8>=0 && $8<29) ? $8 : 0/0) lt 3 pt 13 notitle" & _
                                 ", '' u 2:($8==-1 " & currentTime.Millisecond & "? -2 : 0/0) lt 1 pt 13 notitle" & _
                                 ", '' u 2" & currentTime.Millisecond & ":($8==63 " & currentTime.Millisecond & "? -1 : 0/0) lt 4 pt 13 notitle" & _
                                 ", '' u 2:(($8>28 && " & currentTime.Millisecond & "$8<63) ? 29 : 0/0) lt " & currentTime.Millisecond & "4 pt 13 notitle")
        f_Gplot_Script.Close()
        f_Gplot_Script.Dispose()
        My.Computer.FileSystem.DeleteFile(My.Application.Info.DirectoryPath & "\" & currentTime.ToString("yyyyMMddhhmmss") & currentTime.Millisecond & ".txt")
    End Sub



End Module
