Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks

Public Class ColorRange
    Public FromValue As Double = 0
    Public ToValue As Double = 255
    Public FromColor As Integer = &HFF000000 'ARGB
    Public ToColor As Integer = &HFFFFFFFF 'ARGB
    Public IsGrayScale As Boolean = False
    Public Sub New(pFromValue As Double, pToValue As Double, pFromColor As Integer, pToColor As Integer, Optional pIsGrayScale As Boolean = False)
        FromValue = pFromValue
        ToValue = pToValue
        FromColor = pFromColor
        ToColor = pToColor
        IsGrayScale = pIsGrayScale
    End Sub
    Public Sub New(pValue As Double, pColor As Integer)
        FromValue = pValue
        ToValue = pValue
        FromColor = pColor
        ToColor = pColor
    End Sub
    Public Function CheckValue(Value As Double) As Boolean
        'Check if the value is in the same range
        If (Value >= FromValue AndAlso Value <= ToValue) Then
            Return True
        End If
        Return False
    End Function
    Public Function GetColor(Value As Double) As Integer
        Dim result As Integer = FromColor
        'Check if the value is in the same range
        If (CheckValue(Value)) Then
            'Check if the color must be mixed
            If (FromValue <> ToValue) Then
                'Get the percentage of the value from this range
                Dim TotalRange As Double = ToValue - FromValue
                Dim percentage As Double = (TotalRange - (ToValue - Value)) / TotalRange
                'Compute the average depending on the percentage
                If (ToColor >= FromColor) Then
                    result = ImageUtils.GetMixedColor(FromColor, ToColor, percentage)
                Else
                    result = ImageUtils.GetMixedColor(ToColor, FromColor, percentage)
                End If
            End If
            'Check if the range is in grayscale
            If (IsGrayScale) Then
                'Get the grayscaele from the brightness
                Dim color As Color = Color.FromArgb(result)
                Dim brightness As Double = (Convert.ToInt32(color.R) + Convert.ToInt32(color.G) + Convert.ToInt32(color.B)) / 3
                result = ImageUtils.Grayscale(Convert.ToInt32(255 - brightness)).ToArgb
            End If
        End If
        Return result
    End Function
End Class

Public Class ImageUtils

    Public Shared PARALLEL_PROCESSES As Integer = Environment.ProcessorCount
    Public Shared DEFAULT_COLOR As Integer = &HFF000000

    Public Shared Function Grayscale(brightness As Integer) As Color
        Return Color.FromArgb(brightness, brightness, brightness)
    End Function
    Public Shared Function GetMixedColor(fromColor As Double, toColor As Double, percentage As Double) As Integer
        Dim color1 As String = Hex(fromColor).PadLeft(8, "0"c) 'Pad with 0's for alpha values = 0
        Dim color2 As String = Hex(toColor).PadLeft(8, "0"c) 'Pad with 0's for alpha values = 0
        'Get the mix color from both
        Dim A As Integer = Convert.ToInt32(Convert.ToInt32(color1.Substring(0, 2), 16) * percentage + Convert.ToInt32(color2.Substring(0, 2), 16) * (1 - percentage))
        Dim R As Integer = Convert.ToInt32(Convert.ToInt32(color1.Substring(2, 2), 16) * percentage + Convert.ToInt32(color2.Substring(2, 2), 16) * (1 - percentage))
        Dim G As Integer = Convert.ToInt32(Convert.ToInt32(color1.Substring(4, 2), 16) * percentage + Convert.ToInt32(color2.Substring(4, 2), 16) * (1 - percentage))
        Dim B As Integer = Convert.ToInt32(Convert.ToInt32(color1.Substring(6, 2), 16) * percentage + Convert.ToInt32(color2.Substring(6, 2), 16) * (1 - percentage))
        'Return the ARGB color
        Return Convert.ToInt32(Hex(A) & Hex(R) & Hex(G) & Hex(B), 16)
    End Function
    Public Shared Function GetColorFromPalette(Value As Double, palette As List(Of ColorRange)) As Integer
        Dim result As Integer = DEFAULT_COLOR
        'Loop over color palette until the range is inside the value
        For Each range As ColorRange In palette
            If (range.CheckValue(Value)) Then
                'Get the color for the value in the palette defined
                result = range.GetColor(Value)
                Exit For
            End If
        Next
        Return result
    End Function
    Public Shared Function CombineImages(filePath1 As String, filePath2 As String) As Image
        Dim result As Bitmap = Nothing
        If (File.Exists(filePath1) AndAlso File.Exists(filePath2)) Then
            Dim Image1 As New Bitmap(filePath1)
            Dim Image2 As New Bitmap(filePath2)
            ' Check if the files are equal in size
            If (Not (Image1.Width = Image2.Width AndAlso Image1.Height = Image2.Height)) Then Return Nothing

            ' Get the height and width
            Dim Height As Integer = Image1.Height
            Dim Width As Integer = Image1.Width
            Dim BoundsRect As Rectangle = New Rectangle(0, 0, Width, Height)

            'Inititialize Combined image
            result = New Bitmap(Width, Height, PixelFormat.Format32bppArgb) ' 8bits R * 8bits G * 8bits B * 8bits A
            Dim bmpData As BitmapData = result.LockBits(BoundsRect, ImageLockMode.WriteOnly, result.PixelFormat)
            Dim bytes As Integer = (bmpData.Stride * result.Height) 'Strides contains the 4 bytes added for tyhe Pixelforma used
            Dim rgbValues(bytes) As Byte

            ' Create matrices for the inputs images
            Dim Image1Data As BitmapData = Image1.LockBits(BoundsRect, ImageLockMode.ReadOnly, result.PixelFormat)
            Dim Image2Data As BitmapData = Image2.LockBits(BoundsRect, ImageLockMode.ReadOnly, result.PixelFormat)
            Dim rgbaImage1(bytes) As Byte
            Dim rgbaImage2(bytes) As Byte
            ' Copy the RGB values into the array.
            Marshal.Copy(Image1Data.Scan0, rgbaImage1, 0, bytes)
            Marshal.Copy(Image2Data.Scan0, rgbaImage2, 0, bytes)

            For counter As Integer = 0 To rgbValues.Length - 1
                If (rgbaImage1(counter) = 0 Or rgbaImage2(counter) = 0) Then
                    rgbValues(counter) = 0
                Else
                    If (rgbaImage1(counter) = 0) Then
                        rgbValues(counter) = rgbaImage2(counter)
                    Else
                        rgbValues(counter) = rgbaImage1(counter)
                    End If
                End If
            Next

            'Fill in rgbValues from array
            Marshal.Copy(rgbValues, 0, bmpData.Scan0, bytes)
            'Dispose and unlock the iamges
            result.UnlockBits(bmpData)
            Image1.UnlockBits(Image1Data)
            Image2.UnlockBits(Image2Data)
        End If
        Return result
    End Function

    Public Shared Function ReplaceColorImage(filePath As String, color As Color) As Image
        Dim result As Bitmap = Nothing
        If (File.Exists(filePath)) Then
            Dim Image As New Bitmap(filePath)

            ' Get the height and width
            Dim Height As Integer = Image.Height
            Dim Width As Integer = Image.Width
            Dim BoundsRect As Rectangle = New Rectangle(0, 0, Width, Height)

            ' Inititialize Combined image
            result = New Bitmap(Width, Height, PixelFormat.Format32bppArgb) ' 8bits R * 8bits G * 8bits B * 8bits A
            Dim bmpData As BitmapData = result.LockBits(BoundsRect, ImageLockMode.WriteOnly, result.PixelFormat)
            Dim bytes As Integer = (bmpData.Stride * result.Height) 'Strides contains the 4 bytes added for tyhe Pixelforma used
            Dim rgbValues(bytes - 1) As Byte

            ' Create matrices for the inputs images
            Dim ImageData As BitmapData = Image.LockBits(BoundsRect, ImageLockMode.ReadOnly, result.PixelFormat)
            Dim rgbaImage(bytes - 1) As Byte

            ' Copy the RGB values into the array.
            Marshal.Copy(ImageData.Scan0, rgbaImage, 0, bytes)

            For counter As Integer = 0 To rgbValues.Length - 1
                If (rgbaImage(counter + 3) = 0) Then
                    counter += 3
                    Continue For
                End If
                rgbValues(counter) = color.B
                rgbValues(counter + 1) = color.G
                rgbValues(counter + 2) = color.R
                rgbValues(counter + 3) = color.A
                counter += 3
            Next

            'Fill in rgbValues from array
            Marshal.Copy(rgbValues, 0, bmpData.Scan0, bytes)
            'Dispose and unlock the iamges
            result.UnlockBits(bmpData)
            Image.UnlockBits(ImageData)
        End If
        Return result
    End Function
    Public Shared Function LoadImage(filePath As String, palette As List(Of ColorRange), Optional delimiter As Char = " "c) As Image
        Dim result As Bitmap = Nothing
        If (File.Exists(filePath)) Then
            'Read all the rows (lines) from the file
            Dim rows As String() = File.ReadAllLines(filePath)
            'The height of the image will correspondo with the number of lines
            Dim Height As Integer = rows.Length
            'The Width of the image will correspondo with the number of characters
            Dim Width As Integer = rows(0).Split(New Char() {delimiter}, StringSplitOptions.RemoveEmptyEntries).Length
            'Create the image with the dimensions
            result = New Bitmap(Width, Height, PixelFormat.Format32bppArgb) ' 8bits R * 8bits G * 8bits B * 8bits A
            'Set the boundaries of the Image
            Dim BoundsRect As Rectangle = New Rectangle(0, 0, Width, Height)
            Dim bmpData As BitmapData = result.LockBits(BoundsRect, ImageLockMode.WriteOnly, result.PixelFormat)
            'Write Bytes in the image
            Dim ptr As IntPtr = bmpData.Scan0
            Dim bytes As Integer = (bmpData.Stride * result.Height) 'Strides contains the 4 bytes added for tyhe Pixelforma used
            Dim rgbValues(bytes) As Byte
            'Generate the image
            Dim mylock As New Object
            'Set the limit processes
            Dim options As New ParallelOptions()
            options.MaxDegreeOfParallelism = PARALLEL_PROCESSES
            Parallel.For(0, Height, options,
                    Sub(i) 'Row of the Matrix
                        Dim columns As String() = rows((Height - 1) - i).Split(New Char() {delimiter}, StringSplitOptions.RemoveEmptyEntries)
                        'Pararell for the PRNs
                        Parallel.For(0, Width, options,
                        Sub(j)  'Columns of the Matrix
                            Dim value As Double = Val(columns(j))
                            Dim index As Integer = (bmpData.Stride * i) + (j * (bmpData.Stride / Width))
                            Dim bufferSrc As Byte() = BitConverter.GetBytes(GetColorFromPalette(value, palette))
                            Buffer.BlockCopy(bufferSrc, 0, rgbValues, index, bufferSrc.Length)
                        End Sub) 'END PARALLEL FOR J
                    End Sub) 'END PARALLEL FOR I
            'Fill in rgbValues from array
            Marshal.Copy(rgbValues, 0, ptr, bytes)
            result.UnlockBits(bmpData)
        End If
        Return result
    End Function
    Public Shared Function GetBoundaries(filePath As String, Optional delimiter As Char = " "c) As Double()
        Dim result As Double() = Nothing
        If (File.Exists(filePath)) Then
            ReDim result(1)
            result(0) = Double.MaxValue
            result(1) = Double.MinValue
            'Read all the rows (lines) from the file
            Dim rows As String() = File.ReadAllLines(filePath)
            'The height of the image will correspondo with the number of lines
            Dim Height As Integer = rows.Length
            'The Width of the image will correspondo with the number of characters
            Dim Width As Integer = rows(0).Split(New Char() {delimiter}, StringSplitOptions.RemoveEmptyEntries).Length
            'GEt min and max from the file
            For i As Integer = 0 To Height - 1
                Dim columns As String() = rows((Height - 1) - i).Split(New Char() {delimiter}, StringSplitOptions.RemoveEmptyEntries)
                For j As Integer = 0 To Width - 1
                    Dim value As Double = Val(columns(j))
                    If (result(0) > value) Then
                        result(0) = value
                    End If
                    If (result(1) < value) Then
                        result(1) = value
                    End If
                Next
            Next
        End If
        Return result
    End Function
    Public Shared Function MergeImages(files As String(,)) As Image
        'The height Aan Width of the image 
        Dim Height As Integer, Width As Integer = 0
        'Load the images and store into the 2d array
        Dim Images(files.GetLength(0) - 1, files.GetLength(1) - 1) As Image
        For i As Integer = 0 To files.GetLength(0) - 1
            Dim localWidth As Integer = 0
            Dim localHeight As Integer = 0
            For j As Integer = 0 To files.GetLength(1) - 1
                Dim filePath As String = files(i, j)
                If (File.Exists(filePath)) Then
                    Images(i, j) = Image.FromFile(filePath)
                    'Get current local height ans width
                    If (Images(i, j).Height > localHeight) Then
                        localHeight = Images(i, j).Height
                    End If
                    localWidth += Images(i, j).Width
                End If
            Next
            'Set global width and height
            If (localWidth > Width) Then
                Width = localWidth
            End If
            Height += localHeight
        Next
        Dim CurrentHeight As Integer, CurrentWidth As Integer = 0
        'Create the Bitmap where the images will be merged
        Dim finalImage As New Bitmap(Width, Height, PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(finalImage)
            For i As Integer = 0 To files.GetLength(0) - 1
                Dim maxHeight As Integer = 0
                CurrentWidth = 0
                For j As Integer = 0 To files.GetLength(1) - 1
                    If (Images(i, j) IsNot Nothing) Then
                        g.DrawImage(Images(i, j), CurrentWidth, CurrentHeight, Images(i, j).Width, Images(i, j).Height)
                        CurrentWidth += Images(i, j).Width
                        'Ckech the maximun height for the next row of images
                        If (Images(i, j).Height > maxHeight) Then
                            maxHeight = Images(i, j).Height
                        End If
                    End If
                Next
                'Take the max height computed
                CurrentHeight += maxHeight
            Next
        End Using
        ' Dispose the memory
        For i As Integer = 0 To files.GetLength(0) - 1
            For j As Integer = 0 To files.GetLength(1) - 1
                Images(i, j).Dispose()
            Next
        Next
        Return finalImage
    End Function
    Public Shared Function ResizeImage(ImagePath As String, width As Integer, height As Integer) As Image
        Dim result As Bitmap = Nothing
        If (File.Exists(ImagePath)) Then
            Dim Image As Image = Image.FromFile(ImagePath)
            Dim destRect = New Rectangle(0, 0, width, height)
            result = New Bitmap(width, height)
            result.SetResolution(Image.HorizontalResolution, Image.VerticalResolution)
            Using g = Graphics.FromImage(result)
                'Maintain same quality and resolution from the original
                g.CompositingMode = CompositingMode.SourceCopy
                g.CompositingQuality = CompositingQuality.HighQuality
                g.InterpolationMode = InterpolationMode.HighQualityBicubic
                g.SmoothingMode = SmoothingMode.HighQuality
                g.PixelOffsetMode = PixelOffsetMode.HighQuality
                ' This will preserve the border lines of the result image
                Using attributes = New ImageAttributes()
                    attributes.SetWrapMode(WrapMode.TileFlipXY)
                    g.DrawImage(Image, destRect, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, attributes)
                End Using
                'Dispose the image
                Image.Dispose()
            End Using
        End If
        Return result
    End Function
    Public Shared Function SplitImage(ImagePath As String, rows As Integer, cols As Integer) As Image(,)
        Dim result(,) As Bitmap = Nothing
        If (File.Exists(ImagePath)) Then
            ReDim result(rows - 1, cols - 1)
            Dim Image As Image = Image.FromFile(ImagePath)
            Dim totalwidth As Integer = Image.Width
            Dim totalheight As Integer = Image.Height
            'Loop over the rows and cols to split the iamgs into parts
            Dim i_end As Integer = 0, i_start As Integer = 0
            For row As Integer = 0 To rows - 1
                'Check the height of the image in the row
                If (row < rows - 1) Then
                    i_end += totalheight \ rows
                Else
                    If ((totalheight Mod rows) = 0) Then
                        i_end += totalheight \ rows
                    Else
                        i_end += totalheight Mod rows
                    End If
                End If
                Dim j_start As Integer = 0, j_end As Integer = 0
                For column As Integer = 0 To cols - 1
                    If (column < cols - 1) Then
                        j_end += totalwidth \ cols
                    Else
                        If ((totalwidth Mod cols) = 0) Then
                            j_end += totalwidth \ cols
                        Else
                            j_end += totalwidth Mod cols
                        End If
                    End If
                    'Set the local Height and Width
                    Dim localWidth As Integer = j_end - j_start
                    Dim localHeight As Integer = i_end - i_start
                    'Split each image ans return into the proper index in the 2D array
                    Dim destRect = New Rectangle(0, 0, localWidth, localHeight)
                    Dim destImage As Bitmap = New Bitmap(localWidth, localHeight)
                    Using g = Graphics.FromImage(destImage)
                        g.DrawImage(Image, destRect, j_start, i_start, localWidth, localHeight, GraphicsUnit.Pixel)
                    End Using
                    'Add the current image to the array
                    result(row, column) = destImage
                    j_start = j_end
                Next
                i_start = i_end
            Next
            'Dispose the image
            Image.Dispose()
        End If
        Return result
    End Function
    Public Shared Function TextImage(text As String, width As Integer, height As Integer, Optional textsize As Integer = 40) As Image
        Dim result As New Bitmap(width, height)
        Using g = Graphics.FromImage(result)
            Dim Font As New Font("Segoe Script", textsize)
            Dim Brush As Brush = Brushes.Black
            Dim sf As StringFormat = New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            g.DrawString(text, Font, Brush, New Rectangle(0, 0, result.Width, result.Height), sf)
            sf.Dispose()
        End Using
        Return result
    End Function
    Public Shared Sub Save(Image As Image, filePath As String, format As ImageFormat)
        If (Image IsNot Nothing) Then
            Image.Save(filePath, format)
            Image.Dispose()
        End If
    End Sub
End Class
