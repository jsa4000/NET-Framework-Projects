Public Class ImageManager
    Public Shared Sub CreateImageGEOElevation(InputFile As String, OutputFile As String)
        Dim palette As New List(Of ColorRange)
        palette.Add(New ColorRange(-1000.0, -45.0, &HFFD3D3D3, &HFFD3D3D3))
        palette.Add(New ColorRange(-45.0, -10.0, &HFFB2182B, &HFFD6604D))
        palette.Add(New ColorRange(-10.0, -5.0, &HFFD6604D, &HFFF4A582))
        palette.Add(New ColorRange(-5.0, 0.0, &HFFF4A582, &HFFFDDBC7))
        palette.Add(New ColorRange(0.0, 5.0, &HFFFDDBC7, &HFFF7F7F7))
        palette.Add(New ColorRange(5.0, 10.0, &HFFF7F7F7, &HFFD1E5F0))
        palette.Add(New ColorRange(10.0, 15.0, &HFFD1E5F0, &HFF92C5DE))
        palette.Add(New ColorRange(15.0, 20.0, &HFF92C5DE, &HFF4393C3))
        palette.Add(New ColorRange(20.0, 95.0, &HFF4393C3, &HFF2166AC))
        Dim myImage As Image = ImageUtils.LoadImage(InputFile, palette)
        If (myImage IsNot Nothing) Then
            myImage.Save(OutputFile, System.Drawing.Imaging.ImageFormat.Png)
        End If
    End Sub
    Public Shared Sub CreateImageVisibility(InputFile As String, OutputFile As String)
        Dim palette As New List(Of ColorRange)
        palette.Add(New ColorRange(-1, 0, &HD3D3D3, &HD3D3D3))
        palette.Add(New ColorRange(1, &HFFB2182B))
        Dim myImage As Image = ImageUtils.LoadImage(InputFile, palette)
        If (myImage IsNot Nothing) Then
            myImage.Save(OutputFile, System.Drawing.Imaging.ImageFormat.Png)
        End If
    End Sub

End Class
