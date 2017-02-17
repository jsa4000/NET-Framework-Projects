Public Class ImageTest

    Public Shared Sub CreateSampleImage()
        Dim pathFile As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\eudem_dem_4258_europe.39.3_39.7_-31.5_-31.1.heights.txt"
        'Dim folder As String = "D:\Data\geotiff\Samples\set34"
        'Dim file As String = "ello.tif.35_40_25_30.5.120.geoterrainelev.txt"
        'Dim pathFile As String = folder & "\" & file

        Dim palette As New List(Of ColorRange)
        'Gray scale
        Dim result As Double() = ImageUtils.GetBoundaries(pathFile)
        palette.Add(New ColorRange(result(0), result(1), &HFFFFFFFF, &HFF000000, True))

        'Another palette of colors
        'palette.Add(New ColorRange(-1000.0, -45.0, &HFFD3D3D3, &HFFD3D3D3))
        'palette.Add(New ColorRange(-45.0, -10.0, &HFFB2182B, &HFFD6604D))
        'palette.Add(New ColorRange(-10.0, -5.0, &HFFD6604D, &HFFF4A582))
        'palette.Add(New ColorRange(-5.0, 0.0, &HFFF4A582, &HFFFDDBC7))
        'palette.Add(New ColorRange(0.0, 5.0, &HFFFDDBC7, &HFFF7F7F7))
        'palette.Add(New ColorRange(5.0, 10.0, &HFFF7F7F7, &HFFD1E5F0))
        'palette.Add(New ColorRange(10.0, 15.0, &HFFD1E5F0, &HFF92C5DE))
        'palette.Add(New ColorRange(15.0, 20.0, &HFF92C5DE, &HFF4393C3))
        'palette.Add(New ColorRange(20.0, 95.0, &HFF4393C3, &HFF2166AC))

        Dim myImage As Image = ImageUtils.LoadImage(pathFile, palette)
        If (myImage IsNot Nothing) Then
            myImage.Save("C:\Development\VB2015\GEOVisibility\Debug\Outputs\test01.png", System.Drawing.Imaging.ImageFormat.Png)
        End If

    End Sub
    Public Shared Sub CreateSampleImage2()

        Dim pathFile As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\eudem_dem_4258_europe.39.3_39.7_-31.5_-31.1.136.geoterrainelev.txt"
        'Dim folder As String = "D:\Data\geotiff\Samples\set34"
        'Dim file As String = "ello.tif.35_40_25_30.5.120.geoterrainelev.txt"
        'Dim pathFile As String = folder & "\" & file

        Dim palette As New List(Of ColorRange)
        'Gray scale
        'Dim result As Double() = ImageUtils.GetLimits(pathFile)
        'palette.Add(New ColorRange(result(0), result(1), &HFFFFFFFF, &HFF000000, True))

        'Another palette of colors
        palette.Add(New ColorRange(-1000.0, -45.0, &HFFD3D3D3, &HFFD3D3D3))
        palette.Add(New ColorRange(-45.0, -10.0, &HFFB2182B, &HFFD6604D))
        palette.Add(New ColorRange(-10.0, -5.0, &HFFD6604D, &HFFF4A582))
        palette.Add(New ColorRange(-5.0, 0.0, &HFFF4A582, &HFFFDDBC7))
        palette.Add(New ColorRange(0.0, 5.0, &HFFFDDBC7, &HFFF7F7F7))
        palette.Add(New ColorRange(5.0, 10.0, &HFFF7F7F7, &HFFD1E5F0))
        palette.Add(New ColorRange(10.0, 15.0, &HFFD1E5F0, &HFF92C5DE))
        palette.Add(New ColorRange(15.0, 20.0, &HFF92C5DE, &HFF4393C3))
        palette.Add(New ColorRange(20.0, 95.0, &HFF4393C3, &HFF2166AC))

        Dim myImage As Image = ImageUtils.LoadImage(pathFile, palette)
        If (myImage IsNot Nothing) Then
            myImage.Save("C:\Development\VB2015\GEOVisibility\Debug\Outputs\test02.png", System.Drawing.Imaging.ImageFormat.Png)
        End If

    End Sub
    Public Shared Sub CreateSampleImage3()
        Dim pathFile As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\eudem_dem_4258_europe.39.3_39.7_-31.5_-31.1.136.visible.txt"
        'Dim folder As String = "D:\Data\geotiff\Samples\set34"
        'Dim file As String = "ello.tif.35_40_20_25.5.120.visible.txt"
        'Dim pathFile As String = folder & "\" & file

        Dim palette As New List(Of ColorRange)
        'Gray scale
        'palette.Add(New ColorRange(-10, 70, 0, 255, True))
        'Another palette of colors
        palette.Add(New ColorRange(-1, 0, &HD3D3D3, &HD3D3D3))
        palette.Add(New ColorRange(1, &HFFB2182B))

        Dim myImage As Image = ImageUtils.LoadImage(pathFile, palette)
        If (myImage IsNot Nothing) Then
            myImage.Save("C:\Development\VB2015\GEOVisibility\Debug\Outputs\test03.png", System.Drawing.Imaging.ImageFormat.Png)
        End If

    End Sub
    Public Shared Sub CreateSampleImageMerge()
        'Dim pathFile As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\test02.png"
        Dim pathFile As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\resized.png"
        Dim files As String(,) = {{pathFile, pathFile}, {pathFile, pathFile}}

        Dim myImage As Image = ImageUtils.MergeImages(files)
        If (myImage IsNot Nothing) Then
            myImage.Save("c:\Development\VB2015\GEOVisibility\Debug\Outputs\merged2.png", System.Drawing.Imaging.ImageFormat.Png)
        End If
    End Sub
    Public Shared Sub CreateSampleImageMerge2()
        'Dim pathFile As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\test02.png"
        Dim pathFile As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\resized.png"
        Dim files As String(,) = {{"C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_0_0.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_0_1.png"},
                                {"C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_1_0.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_1_1.png"}}

        Dim myImage As Image = ImageUtils.MergeImages(files)
        If (myImage IsNot Nothing) Then
            myImage.Save("c:\Development\VB2015\GEOVisibility\Debug\Outputs\merged3.png", System.Drawing.Imaging.ImageFormat.Png)
        End If
    End Sub
    Public Shared Sub CreateSampleImageMerge3()
        'Dim pathFile As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\test02.png"
        Dim pathFile As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\resized.png"
        Dim files As String(,) = {{"C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_0_0.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_0_1.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_0_2.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_0_3.png"},
                                {"C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_1_0.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_1_1.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_1_2.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_1_3.png"},
                                {"C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_2_0.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_2_1.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_2_2.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_2_3.png"},
                                 {"C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_3_0.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_3_1.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_3_2.png", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\split_3_3.png"}}

        Dim myImage As Image = ImageUtils.MergeImages(files)
        If (myImage IsNot Nothing) Then
            myImage.Save("c:\Development\VB2015\GEOVisibility\Debug\Outputs\merged4.png", System.Drawing.Imaging.ImageFormat.Png)
        End If
    End Sub
    Public Shared Sub CreateSampleImageSplit()
        Dim image(,) As Image = ImageUtils.SplitImage("C:\Development\VB2015\GEOVisibility\Debug\Outputs\ello.tif.35_40_20_25.5.120.geoterrainelev.png", 2, 2)
        For i As Integer = 0 To image.GetLength(0) - 1
            For j As Integer = 0 To image.GetLength(1) - 1
                Dim myImage As Image = image(i, j)
                myImage.Save("c:\Development\VB2015\GEOVisibility\Debug\Outputs\split_" & i & "_" & j & ".png", System.Drawing.Imaging.ImageFormat.Png)
            Next
        Next
    End Sub
    Public Shared Sub CreateSampleImageSplit2()
        Dim image(,) As Image = ImageUtils.SplitImage("C:\Development\VB2015\GEOVisibility\Debug\Outputs\ello.tif.35_40_20_25.5.120.geoterrainelev.png", 4, 4)
        For i As Integer = 0 To image.GetLength(0) - 1
            For j As Integer = 0 To image.GetLength(1) - 1
                Dim myImage As Image = image(i, j)
                myImage.Save("c:\Development\VB2015\GEOVisibility\Debug\Outputs\split_" & i & "_" & j & ".png", System.Drawing.Imaging.ImageFormat.Png)
            Next
        Next
    End Sub
    Public Shared Sub CreateSampleImageResize()
        Dim pathFile As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\test02.png"

        Dim myImage As Image = ImageUtils.ResizeImage(pathFile, 900, 900)
        If (myImage IsNot Nothing) Then
            myImage.Save("c:\Development\VB2015\GEOVisibility\Debug\Outputs\resized.png", System.Drawing.Imaging.ImageFormat.Png)
        End If
    End Sub
    Public Shared Sub CreateSampleImageText()
        Dim myImage As Image = ImageUtils.TextImage("00000001", 900, 900)
        If (myImage IsNot Nothing) Then
            myImage.Save("c:\Development\VB2015\GEOVisibility\Debug\Outputs\textimage.png", System.Drawing.Imaging.ImageFormat.Png)
        End If
    End Sub

End Class
