Imports System.Drawing.Imaging

Public Class LogoMarker
    Private LogoImage As Bitmap = Nothing
    Private AppFolder As String = My.Application.Info.DirectoryPath
    Private thisLock As New Object
    Public Enum ImagesFormat
        ifPNG
        ifJPG
        ifSameAsSource
    End Enum
    Public Enum ImagePosition
        TopLeft
        TopMid
        TopRight
        MidLeft
        Center
        MidRight
        BottomLeft
        BottomMid
        BottomRight
    End Enum
    Private Shared ReadOnly _instance As New Lazy(Of LogoMarker)(Function() New _
       LogoMarker(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication)
    Public Sub New()
        'Load the logo 
        LogoImage = New Bitmap(AppFolder & "\Settings\logo.png")
    End Sub
    Protected Overrides Sub Finalize()
        ' Unload Logo 
        LogoImage.Dispose()
    End Sub
    Public Shared ReadOnly Property Instance() As LogoMarker
        Get
            Return _instance.Value
        End Get
    End Property
    ' 0: OK
    ' 1: Base Image doesn't exist
    ' 2: Base Image type unknown
    ' 3: Image not saved for whatever reason...
    Public Function MarkImage(ByVal BaseImage As String, ByVal SaveOptions As ImagesFormat, ByVal LogoPosition As ImagePosition,
                              ByVal LogoSize As Integer, ByVal LogoOpacity As Integer, ByVal OutputFile As String) As Boolean
        Dim result As Boolean = True
        Dim baseBitmap As Bitmap = Nothing
        Try
            'Lock to protect the funcion for concurrent calls unti all the variables have been disposed.
            SyncLock thisLock
                ' Base Image Load
                baseBitmap = New Bitmap(BaseImage)
                ' Reescale Logo
                Dim LogoHeight As Integer = LogoImage.Height * (baseBitmap.Width * LogoSize / 100) / LogoImage.Width
                Dim LogoWidth As Integer = baseBitmap.Width * LogoSize / 100
                ' Compute the final position of the logo
                Dim UpLeftPixelX, UpLeftPixelY As Integer
                If LogoPosition = ImagePosition.TopLeft Then
                    UpLeftPixelX = 0
                    UpLeftPixelY = 0
                ElseIf LogoPosition = ImagePosition.TopMid Then
                    UpLeftPixelX = (baseBitmap.Width - LogoWidth) / 2
                    UpLeftPixelY = 0
                ElseIf LogoPosition = ImagePosition.TopRight Then
                    UpLeftPixelX = baseBitmap.Width - LogoWidth
                    UpLeftPixelY = 0
                ElseIf LogoPosition = ImagePosition.MidLeft Then
                    UpLeftPixelX = 0
                    UpLeftPixelY = (baseBitmap.Height - LogoHeight) / 2
                ElseIf LogoPosition = ImagePosition.MidRight Then
                    UpLeftPixelX = baseBitmap.Width - LogoWidth
                    UpLeftPixelY = (baseBitmap.Height - LogoHeight) / 2
                ElseIf LogoPosition = ImagePosition.BottomLeft Then
                    UpLeftPixelX = 0
                    UpLeftPixelY = baseBitmap.Height - LogoHeight
                ElseIf LogoPosition = ImagePosition.BottomMid Then
                    UpLeftPixelX = (baseBitmap.Width - LogoWidth) / 2
                    UpLeftPixelY = baseBitmap.Height - LogoHeight
                ElseIf LogoPosition = ImagePosition.BottomRight Then
                    UpLeftPixelX = baseBitmap.Width - LogoWidth
                    UpLeftPixelY = baseBitmap.Height - LogoHeight
                Else 'Center
                    UpLeftPixelX = (baseBitmap.Width - LogoWidth) / 2
                    UpLeftPixelY = (baseBitmap.Height - LogoHeight) / 2
                End If
                'Merge the Logo with the base image using the configuration computed
                result = MergeImages(baseBitmap, LogoImage, OutputFile, True, SaveOptions, LogoOpacity, New Rectangle(UpLeftPixelX, UpLeftPixelY, LogoWidth, LogoHeight))
            End SyncLock
        Catch ex As Exception
            ' Error adding watermark
            result = False
        Finally
            'Dispose the base bitmap image created
            If baseBitmap IsNot Nothing Then baseBitmap.Dispose()
        End Try
        'Finally return the value
        Return result
    End Function
    Private Shared Function GetFormat(ByVal SaveOptions As ImagesFormat, Optional imageBitmap As Bitmap = Nothing) As ImageFormat
        If SaveOptions = LogoMarker.ImagesFormat.ifJPG Then
            Return ImageFormat.Jpeg
        ElseIf SaveOptions = LogoMarker.ImagesFormat.ifPNG Then
            Return ImageFormat.Png
        ElseIf (imageBitmap IsNot Nothing) Then
            Return imageBitmap.RawFormat
        Else
            'By default use png format is not specified
            Return ImageFormat.Png
        End If
    End Function
    Public Shared Function MergeImages(ByVal BaseImage As String, ByVal TopImage As String, ByVal OutputFile As String) As Boolean
        Dim result As Boolean = True
        Dim baseBitmap As Bitmap = Nothing
        Dim topBitmap As Bitmap = Nothing
        Try
            ' Load images Base and Top images
            baseBitmap = New Bitmap(BaseImage)
            topBitmap = New Bitmap(TopImage)
            'Merge the Logo with the base image using the configuration computed
            result = MergeImages(baseBitmap, topBitmap, OutputFile)
        Catch ex As Exception
            ' Error merging images
            result = False
        Finally
            'Dispose the base bitmap image created
            If baseBitmap IsNot Nothing Then baseBitmap.Dispose()
            If topBitmap IsNot Nothing Then topBitmap.Dispose()
        End Try
        'Finally return the value
        Return result
    End Function
    Public Shared Function MergeImages(ByVal BaseBitmap As Bitmap, ByVal TopBitmap As Bitmap, ByVal OutputFile As String,
                                       Optional Overwrite As Boolean = True, Optional ByVal SaveOptions As ImagesFormat = ImagesFormat.ifSameAsSource,
                                       Optional ByVal Opacity As Integer = 50, Optional Rect As Rectangle = Nothing) As Boolean
        Dim result As Boolean = True
        Dim outputBitmap As Bitmap = Nothing
        Dim grx As Graphics = Nothing
        Dim outputFormat As ImageFormat = ImageFormat.Png
        Try
            'Get the format of the image that will be generated
            outputFormat = GetFormat(SaveOptions, BaseBitmap)
            ' We create a blank base image to ensure that image composition is allowed
            outputBitmap = New Bitmap(BaseBitmap.Width, BaseBitmap.Height)
            grx = Graphics.FromImage(outputBitmap)
            grx.DrawImage(BaseBitmap, 0, 0)
            Dim colormatrix As ColorMatrix = New ColorMatrix()
            colormatrix.Matrix33 = Opacity / 100
            Dim imgAttribute As ImageAttributes = New ImageAttributes()
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)
            'If not rect is defined the fit the top image into the base image (but not maintainig the ratio or the top image)
            If (Rect = Nothing) Then Rect = New Rectangle(0, 0, BaseBitmap.Width, BaseBitmap.Height)
            ' Draw the second image on top of the base image
            grx.DrawImage(TopBitmap, Rect, 0, 0, TopBitmap.Width, TopBitmap.Height, GraphicsUnit.Pixel, imgAttribute)
            If (Overwrite) Then BaseBitmap.Dispose()
            'Finally save the image
            outputBitmap.Save(OutputFile, outputFormat)
        Catch ex As Exception
            ' Error adding watermark
            result = False
        Finally
            ' Dispose unneeded variables and finally save the final image
            If grx IsNot Nothing Then grx.Dispose()
            If outputBitmap IsNot Nothing Then outputBitmap.Dispose()
        End Try
        'Finally return the value
        Return result
    End Function
End Class
