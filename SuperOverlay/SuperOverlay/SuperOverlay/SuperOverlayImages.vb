Imports System.Threading.Tasks
Imports System.IO
Imports System.Drawing.Imaging

Public Class SuperOverlayImages

    Public Shared PARALLEL_PROCESSES As Integer = Environment.ProcessorCount
    Public Const MAX_LEVELS As Integer = 10
    Public Const LEVEL_TO_MERGE As Integer = 7
    Public Const IMAGES_TO_MERGE As Integer = 10
    Public Const MAX_QUADRANTS As Integer = 4
    Public Const IMAGE_SIZE_PER_DEGREE As Integer = 3600

    Public OriginalMapRange() As Decimal = {75, 25, -35, 45}

    'Public MapRange() As Decimal = {75, 25, -35, 45} ' Full map
    'Public MapRange() As Decimal = {70, 65, 15, 20}  '5 degrees
    Public MapRange() As Decimal = {70, 69, 21, 22} '1 degree

    Dim ImageSize As Integer = 900 '  Each Image in pixels
    Dim OriginalImageSize As Integer = 3600 '  Each Image in pixels
    Dim ImageDegrees As Double = 0.25 '  Degrees at the lowest level -> ImageSize / 0.25 = Image Pixels for 1 degree

    Public ImagesPath As String = My.Application.Info.DirectoryPath & "\Images"
    Public SourcePath As String = My.Application.Info.DirectoryPath & "\Images"
    Public Shared EmptyImage As String = My.Application.Info.DirectoryPath & "\Settings\empty.png"
    Public PRN As String = "120" 'By default

    Private Class Layer
        Public Name As String = String.Empty
        Public path As String = String.Empty
        'Child quadrants 
        '0: left upper quadrant
        '1: right upper quadrant
        '2: left lower quadrant
        '3: right lower quadrant
        Public NextQuadrants(MAX_QUADRANTS - 1) As String
        'Coordinates
        Public North As Decimal
        Public South As Decimal
        Public West As Decimal
        Public East As Decimal
        'Set Default Quadrants
        Public Sub SetDefaultQuadrants(ByVal quadrant As String)
            For index As Integer = 0 To MAX_QUADRANTS - 1
                NextQuadrants(index) = quadrant
            Next
        End Sub
    End Class

    Public Sub New(Top As Integer, Bottom As Integer, Left As Integer, Right As Integer)
        MapRange = {Top, Bottom, Left, Right}
    End Sub

#Region "Resize Source Images"

    Public Sub ResizeSourceImages(ByVal pSourcePath As String, Optional ByVal pCurrentLevel As Integer = LEVEL_TO_MERGE + 1)
        'Check for the directories
        If (pCurrentLevel <> MAX_LEVELS AndAlso My.Computer.FileSystem.DirectoryExists(pSourcePath)) Then

            Dim CurrentPathLevel As String = pSourcePath & "\" & pCurrentLevel - 1
            Dim TargetPathLevel As String = pSourcePath & "\" & pCurrentLevel
            'Create the new output
            If (Not My.Computer.FileSystem.DirectoryExists(TargetPathLevel)) Then
                My.Computer.FileSystem.CreateDirectory(TargetPathLevel)
            End If

            Dim search As IEnumerable(Of String) = Directory.EnumerateFiles(CurrentPathLevel, "*.png", SearchOption.TopDirectoryOnly)
            'Get all the files in the current directory
            For Each currentfile As String In search
                'This file must be splitted into four with the name of the current file plus 0,1, or 3
                Dim images(,) As Image = ImageUtils.SplitImage(currentfile, 2, 2)
                Dim index As Integer = 0
                For i As Integer = 0 To images.GetLength(0) - 1
                    For j As Integer = 0 To images.GetLength(1) - 1
                        Dim myImage As Image = images(i, j)
                        Dim imagePath As String = TargetPathLevel & "\" & My.Computer.FileSystem.GetName(currentfile).Split("."c)(0) & index & ".png"
                        ImageUtils.Save(myImage, imagePath, ImageFormat.Png)
                        index += 1
                    Next
                Next
            Next
            'Do the same for the resize images
            ResizeSourceImages(pSourcePath, pCurrentLevel + 1)

            'Get all the files in the current directory
            For Each currentfile As String In search
                'Resize the current image to the same folder
                Dim image As Image = ImageUtils.ResizeImage(currentfile, 900, 900)
                ImageUtils.Save(image, currentfile, ImageFormat.Png)
            Next

        End If
    End Sub

#End Region

#Region "Merge Region Images"

    Public Sub MergeRegionImages(ByVal pSourcePath As String, ByVal pImagesPath As String)
        'Check for the directories
        If (My.Computer.FileSystem.DirectoryExists(pSourcePath)) Then

            ''Create the new output
            'If (Not My.Computer.FileSystem.DirectoryExists(pImagesPath)) Then
            '    My.Computer.FileSystem.CreateDirectory(pImagesPath)
            'End If

            'Dim HSize As Decimal = MapRange(3) - MapRange(2)
            'Dim VSize As Decimal = MapRange(0) - MapRange(1)

            'Dim HSplit As Integer = HSize / IMAGES_TO_MERGE
            'Dim VSplit As Integer = VSize / IMAGES_TO_MERGE

            'For i As Integer = 0 To HSplit - 1
            '    For j As Integer = 0 To VSplit - 1
            '        MergeSubRegionImages(pSourcePath, pImagesPath, i * IMAGES_TO_MERGE, j * IMAGES_TO_MERGE)

            '    Next
            'Next


            Dim files(4, 4) As String
            Dim NextImagesPath As String = pImagesPath & "\Temp"
            'Create the new output
            If (Not My.Computer.FileSystem.DirectoryExists(NextImagesPath)) Then
                My.Computer.FileSystem.CreateDirectory(NextImagesPath)
            End If

            For i As Integer = 0 To 4
                For j As Integer = 0 To 4
                    files(i, j) = pImagesPath & "\" & "Image_" & (i * 10) & "_" & (j * 10) & ".png"
                Next
            Next

            'Merge Image
            Dim MergedImagePath As String = NextImagesPath & "\" & "Image.png"
            Dim myImage As Image = ImageUtils.MergeImages(files)
            ImageUtils.Save(myImage, MergedImagePath, ImageFormat.Png)

        End If
    End Sub

    Public Sub MergeSubRegionImages(ByVal pSourcePath As String, ByVal pImagesPath As String, Optional ByVal NorthOffset As Integer = 0, Optional ByVal WestOffset As Integer = 0)
        'Check for the directories
        If (My.Computer.FileSystem.DirectoryExists(pSourcePath)) Then

            'Create the new output
            If (Not My.Computer.FileSystem.DirectoryExists(pImagesPath)) Then
                My.Computer.FileSystem.CreateDirectory(pImagesPath)
            End If

            'Create the image to merge
            Dim imagesToMerge As New List(Of String)

            Dim PRN As String = "120"
            For NSteps As Integer = 0 To IMAGES_TO_MERGE - 1

                Dim North As Integer = MapRange(0) - NorthOffset - NSteps
                For WSteps As Integer = 0 To IMAGES_TO_MERGE - 1
                    Dim West As Integer = MapRange(2) + WestOffset + WSteps

                    Dim row As Integer = (MapRange(0) - North) * IMAGE_SIZE_PER_DEGREE
                    Dim column As Integer = ((MapRange(2) - West) * IMAGE_SIZE_PER_DEGREE) * -1
                    'Get the name for the old image
                    Dim Image As String = "ello.tif." & row & "_" & column & ".1." & PRN & ".geoterrainelev.txt.png"
                    'Check if the file exists
                    If (Not My.Computer.FileSystem.FileExists(pSourcePath & "\" & Image)) Then
                        'Move the image into the corresponding path
                        ' My.Computer.FileSystem.MoveFile(SourcePath & "\" & OldImageName, ImagesPath & "\" & nameChild & ".png")
                        'Add an empty image to merge
                        imagesToMerge.Add(EmptyImage)
                    Else
                        'Add image to merge
                        imagesToMerge.Add(pSourcePath & "\" & Image)
                    End If
                Next
            Next

            'Merge The file into the corresponding image
            If (imagesToMerge.Count > 0) Then
                'From 0 to 3 merge the images found using the parent name

                'Check if directory exist 
                If (Not My.Computer.FileSystem.DirectoryExists(pImagesPath)) Then
                    My.Computer.FileSystem.CreateDirectory(pImagesPath)
                End If

                Dim imageIndex As Integer = 0
                Dim files(IMAGES_TO_MERGE - 1, IMAGES_TO_MERGE - 1) As String
                For i As Integer = 0 To files.GetLength(0) - 1
                    For j As Integer = 0 To files.GetLength(1) - 1
                        files(i, j) = imagesToMerge(imageIndex)
                        imageIndex += 1
                    Next
                Next
                'Merge Image
                Dim MergedImagePath As String = pImagesPath & "\" & "Image_" & NorthOffset & "_" & WestOffset & ".png"
                Dim myImage As Image = ImageUtils.MergeImages(files)
                ImageUtils.Save(myImage, MergedImagePath, ImageFormat.Png)
                'Finally resample the image
                'Dim image As Image = ImageUtils.ResizeImage(MergedImagePath, 900, 900)
                'ImageUtils.Save(image, MergedImagePath, ImageFormat.Png)
            End If

        End If
    End Sub

#End Region

#Region "Rename Source Images"

    Public Sub RenameSourceImages(ByVal pSourcePath As String, ByVal pImagesPath As String, ByVal pPRN As String)

        'Set the PRN and image path that will be computed
        PRN = pPRN
        SourcePath = pSourcePath
        ImagesPath = pImagesPath & "\" & LEVEL_TO_MERGE 'This is because the images generated by Geo vosibility belong to the 6th level 

        'Check if directory exist 
        If (Not My.Computer.FileSystem.DirectoryExists(ImagesPath)) Then
            My.Computer.FileSystem.CreateDirectory(ImagesPath)
        End If

        'Create the initial level
        Dim ZeroLayer As New Layer()
        ZeroLayer.Name = "0"
        'Create the initial quadrants
        For index As Integer = 0 To MAX_QUADRANTS - 1
            ZeroLayer.NextQuadrants(index) = index.ToString()
        Next
        Dim MapPixelsHeight As Integer = (MapRange(0) - MapRange(1)) * OriginalImageSize
        Dim MapPixelsWidth As Integer = (MapRange(3) - MapRange(2)) * OriginalImageSize

        ZeroLayer.North = MapRange(0)
        'ZeroLayer.South = MapRange(1) - (128 - (MapRange(0) - MapRange(1))) 'OLD
        ZeroLayer.South = MapRange(1) - (((Math.Pow(2, MAX_LEVELS) * ImageSize) - MapPixelsHeight) / (ImageSize / ImageDegrees))
        ZeroLayer.West = MapRange(2)
        'ZeroLayer.East = (128 - (MapRange(3) - MapRange(2))) + MapRange(3) 'OLD
        ZeroLayer.East = (((Math.Pow(2, MAX_LEVELS) * ImageSize) - MapPixelsWidth) / (ImageSize / ImageDegrees)) + MapRange(3)

        'Generate Levels from zero
        GenerateLevel(ZeroLayer, 1, 0)
    End Sub

    Private Sub GenerateLevel(ByVal CurrentLayer As Layer, ByVal deepLevel As Integer, ByVal drawOrder As Integer)
        'Base case for recurrent calls
        If deepLevel <= MAX_LEVELS Then
            'For each quadrant create the  and use recursive calls to create the deeper nodes

            'Set the limit processes
            Dim options As New ParallelOptions()
            options.MaxDegreeOfParallelism = PARALLEL_PROCESSES
            Parallel.For(0, MAX_QUADRANTS, options,
                Sub(index) 'Row of the Matrix

                    'Create the new level
                    Dim NextLayer As New Layer()

                    'Path of quadrants
                    NextLayer.Name = CurrentLayer.NextQuadrants(index)
                    NextLayer.path = CurrentLayer.path
                    NextLayer.SetDefaultQuadrants(CurrentLayer.NextQuadrants(index))

                    Select Case index
                        Case 0
                            'First quadrant Coordinates
                            NextLayer.North = CurrentLayer.North
                            NextLayer.South = (CurrentLayer.North + CurrentLayer.South) / 2
                            NextLayer.West = CurrentLayer.West
                            NextLayer.East = (CurrentLayer.East + CurrentLayer.West) / 2
                        Case 1
                            'Second quadrant Coordinates
                            NextLayer.North = CurrentLayer.North
                            NextLayer.South = (CurrentLayer.North + CurrentLayer.South) / 2
                            NextLayer.West = (CurrentLayer.East + CurrentLayer.West) / 2
                            NextLayer.East = CurrentLayer.East
                        Case 2
                            'Third quadrant Coordinates
                            NextLayer.North = (CurrentLayer.North + CurrentLayer.South) / 2
                            NextLayer.South = CurrentLayer.South
                            NextLayer.West = CurrentLayer.West
                            NextLayer.East = (CurrentLayer.East + CurrentLayer.West) / 2
                        Case 3
                            'Fourth quadrant Coordinates
                            NextLayer.North = (CurrentLayer.North + CurrentLayer.South) / 2
                            NextLayer.South = CurrentLayer.South
                            NextLayer.West = (CurrentLayer.East + CurrentLayer.West) / 2
                            NextLayer.East = CurrentLayer.East
                    End Select

                    If (NextLayer.West < MapRange(3) AndAlso NextLayer.North > MapRange(1)) Then
                        'Generate Image Files 
                        GenerateImageFile(NextLayer, drawOrder + 1, deepLevel)
                        'Prepare for the nex recursive call
                        NextLayer.path &= "\" + CurrentLayer.NextQuadrants(index)
                        For j As Integer = 0 To MAX_QUADRANTS - 1
                            NextLayer.NextQuadrants(j) &= j.ToString()
                        Next
                        'Recursive calls to generate next level of  Files
                        GenerateLevel(NextLayer, deepLevel + 1, drawOrder + 1)
                    End If
                End Sub)
        End If
    End Sub


    Private Sub GenerateImageFile(ByVal CurrentLayer As Layer, ByVal drawOrder As Integer, ByVal deepLevel As Integer)

        'For each quadrant create the  and use recursive calls to create the deeper nodes
        For index As Integer = 0 To MAX_QUADRANTS - 1

            Dim name As String = CurrentLayer.NextQuadrants(index)
            Dim northCoordinates As Decimal = CurrentLayer.North
            Dim southCoordinates As Decimal = CurrentLayer.South
            Dim westCoordinates As Decimal = CurrentLayer.West
            Dim eastCoordinates As Decimal = CurrentLayer.East

            'Image must be 1 degree (level 6)
            If (northCoordinates - southCoordinates = 1) Then
                'This is one degree iamge
                'ello.tif.36000_162000.1.120.geoterrainelev.txt.png
                'Get the pixels using the degrees corresponding to this image.
                Dim row As Integer = (OriginalMapRange(0) - northCoordinates) * IMAGE_SIZE_PER_DEGREE
                Dim column As Integer = ((OriginalMapRange(2) - westCoordinates) * IMAGE_SIZE_PER_DEGREE) * -1
                'Get the name for the old image
                Dim OldImageName As String = "ello.tif." & row & "_" & column & ".1." & PRN & ".visible.txt.png"
                'Check if the file exists
                If (My.Computer.FileSystem.FileExists(SourcePath & "\" & OldImageName)) Then
                    'Copy the image into the corresponding path
                    My.Computer.FileSystem.CopyFile(SourcePath & "\" & OldImageName, ImagesPath & "\" & name & ".png", True)
                End If
            End If

        Next
    End Sub

    'Private Sub GenerateImageFile(ByVal CurrentLayer As Layer, ByVal drawOrder As Integer, ByVal deepLevel As Integer)

    '    'For each quadrant create the  and use recursive calls to create the deeper nodes
    '    For index As Integer = 0 To MAX_QUADRANTS - 1

    '        Dim name As String = CurrentLayer.NextQuadrants(index)
    '        Dim northCoordinates As Decimal = CurrentLayer.North
    '        Dim southCoordinates As Decimal = CurrentLayer.South
    '        Dim westCoordinates As Decimal = CurrentLayer.West
    '        Dim eastCoordinates As Decimal = CurrentLayer.East

    '        If deepLevel < MAX_LEVELS Then
    '            'Add the information for the deep levels
    '            For j As Integer = 0 To MAX_QUADRANTS - 1

    '                Dim nameChild As String = name + j.ToString()
    '                Dim NorthChild As Decimal
    '                Dim SouthChild As Decimal
    '                Dim WestChild As Decimal
    '                Dim EastChild As Decimal
    '                Select Case j
    '                    Case 0
    '                        'First quadrant child Coordinates
    '                        NorthChild = northCoordinates
    '                        SouthChild = (northCoordinates + southCoordinates) / 2
    '                        WestChild = westCoordinates
    '                        EastChild = (eastCoordinates + westCoordinates) / 2
    '                    Case 1
    '                        'Second quadrant child Coordinates
    '                        NorthChild = northCoordinates
    '                        SouthChild = (northCoordinates + southCoordinates) / 2
    '                        WestChild = (eastCoordinates + westCoordinates) / 2
    '                        EastChild = eastCoordinates
    '                    Case 2
    '                        'Third quadrant child Coordinates
    '                        NorthChild = (northCoordinates + southCoordinates) / 2
    '                        SouthChild = southCoordinates
    '                        WestChild = westCoordinates
    '                        EastChild = (eastCoordinates + westCoordinates) / 2
    '                    Case 3
    '                        'Fourth quadrant child Coordinates
    '                        NorthChild = (northCoordinates + southCoordinates) / 2
    '                        SouthChild = southCoordinates
    '                        WestChild = (eastCoordinates + westCoordinates) / 2
    '                        EastChild = eastCoordinates
    '                End Select
    '                'Image depending on the child
    '                If (NorthChild - SouthChild = 1) Then
    '                    'This is one degree iamge
    '                    'ello.tif.36000_162000.1.120.geoterrainelev.txt.png
    '                    'Get the pixels using the degrees corresponding to this image.
    '                    Dim row As Integer = (OriginalMapRange(0) - NorthChild) * IMAGE_SIZE_PER_DEGREE
    '                    Dim column As Integer = ((OriginalMapRange(2) - WestChild) * IMAGE_SIZE_PER_DEGREE) * -1
    '                    'Get the name for the old image
    '                    Dim OldImageName As String = "ello.tif." & row & "_" & column & ".1." & PRN & ".visible.txt.png"
    '                    'Check if the file exists
    '                    If (My.Computer.FileSystem.FileExists(SourcePath & "\" & OldImageName)) Then
    '                        'Copy the image into the corresponding path
    '                        My.Computer.FileSystem.CopyFile(SourcePath & "\" & OldImageName, ImagesPath & "\" & nameChild & ".png", True)
    '                    End If
    '                End If
    '            Next
    '        ElseIf deepLevel = MAX_LEVELS Then
    '            'Images for the deepest nodes

    '        End If

    '    Next
    'End Sub

#End Region

#Region "Merge Source Images"

    Public Sub MergeSourceImages(ByVal pSourcePath As String)

        'Set the PRN and image path that will be computed
        SourcePath = pSourcePath

        'Create the initial level
        Dim ZeroLayer As New Layer()
        ZeroLayer.Name = "0"
        'Create the initial quadrants
        For index As Integer = 0 To MAX_QUADRANTS - 1
            ZeroLayer.NextQuadrants(index) = index.ToString()
        Next

        Dim MapPixelsHeight As Integer = (MapRange(0) - MapRange(1)) * OriginalImageSize
        Dim MapPixelsWidth As Integer = (MapRange(3) - MapRange(2)) * OriginalImageSize

        ZeroLayer.North = MapRange(0)
        'ZeroLayer.South = MapRange(1) - (128 - (MapRange(0) - MapRange(1))) 'OLD
        ZeroLayer.South = MapRange(1) - (((Math.Pow(2, MAX_LEVELS) * ImageSize) - MapPixelsHeight) / (ImageSize / ImageDegrees))
        ZeroLayer.West = MapRange(2)
        'ZeroLayer.East = (128 - (MapRange(3) - MapRange(2))) + MapRange(3) 'OLD
        ZeroLayer.East = (((Math.Pow(2, MAX_LEVELS) * ImageSize) - MapPixelsWidth) / (ImageSize / ImageDegrees)) + MapRange(3)

        'Generate Levels from zero
        MergeLevel(ZeroLayer, 1, 0)
    End Sub

    Private Sub MergeLevel(ByVal CurrentLayer As Layer, ByVal deepLevel As Integer, ByVal drawOrder As Integer)
        'Base case for recurrent calls
        If deepLevel <= MAX_LEVELS Then
            'For each quadrant create the  and use recursive calls to create the deeper nodes

            'Set the limit processes
            Dim options As New ParallelOptions()
            options.MaxDegreeOfParallelism = PARALLEL_PROCESSES
            Parallel.For(0, MAX_QUADRANTS, options,
                Sub(index) 'Row of the Matrix
                    'Create the new level
                    Dim NextLayer As New Layer()

                    'Path of quadrants
                    NextLayer.Name = CurrentLayer.NextQuadrants(index)
                    NextLayer.SetDefaultQuadrants(CurrentLayer.NextQuadrants(index))

                    Select Case index
                        Case 0
                            'First quadrant Coordinates
                            NextLayer.North = CurrentLayer.North
                            NextLayer.South = (CurrentLayer.North + CurrentLayer.South) / 2
                            NextLayer.West = CurrentLayer.West
                            NextLayer.East = (CurrentLayer.East + CurrentLayer.West) / 2
                        Case 1
                            'Second quadrant Coordinates
                            NextLayer.North = CurrentLayer.North
                            NextLayer.South = (CurrentLayer.North + CurrentLayer.South) / 2
                            NextLayer.West = (CurrentLayer.East + CurrentLayer.West) / 2
                            NextLayer.East = CurrentLayer.East
                        Case 2
                            'Third quadrant Coordinates
                            NextLayer.North = (CurrentLayer.North + CurrentLayer.South) / 2
                            NextLayer.South = CurrentLayer.South
                            NextLayer.West = CurrentLayer.West
                            NextLayer.East = (CurrentLayer.East + CurrentLayer.West) / 2
                        Case 3
                            'Fourth quadrant Coordinates
                            NextLayer.North = (CurrentLayer.North + CurrentLayer.South) / 2
                            NextLayer.South = CurrentLayer.South
                            NextLayer.West = (CurrentLayer.East + CurrentLayer.West) / 2
                            NextLayer.East = CurrentLayer.East
                    End Select

                    If (NextLayer.West < MapRange(3) AndAlso NextLayer.North > MapRange(1)) Then

                        'Prepare for the nex recursive call
                        For j As Integer = 0 To MAX_QUADRANTS - 1
                            NextLayer.NextQuadrants(j) &= j.ToString()
                        Next

                        'Recursive calls to generate next level of  Files
                        MergeLevel(NextLayer, deepLevel + 1, drawOrder + 1)

                        'SEt again the4 default values for this layer after the recursive calls
                        NextLayer.SetDefaultQuadrants(CurrentLayer.NextQuadrants(index))
                        'Generate Image Files 
                        GenerateMergeFile(NextLayer, drawOrder + 1, deepLevel)
                    End If
                End Sub)
        End If
    End Sub

    Private Sub GenerateMergeFile(ByVal CurrentLayer As Layer, ByVal drawOrder As Integer, ByVal deepLevel As Integer)

        'For each quadrant create the  and use recursive calls to create the deeper nodes
        For index As Integer = 0 To MAX_QUADRANTS - 1

            Dim name As String = CurrentLayer.NextQuadrants(index)
            Dim northCoordinates As Decimal = CurrentLayer.North
            Dim southCoordinates As Decimal = CurrentLayer.South
            Dim westCoordinates As Decimal = CurrentLayer.West
            Dim eastCoordinates As Decimal = CurrentLayer.East

            If deepLevel < MAX_LEVELS Then

                'Create the image to merge
                Dim imagesToMerge As New List(Of String)

                'Add the information for the deep levels
                For j As Integer = 0 To MAX_QUADRANTS - 1

                    Dim nameChild As String = name + j.ToString()
                    Dim NorthChild As Decimal
                    Dim SouthChild As Decimal
                    Dim WestChild As Decimal
                    Dim EastChild As Decimal
                    Select Case j
                        Case 0
                            'First quadrant child Coordinates
                            NorthChild = northCoordinates
                            SouthChild = (northCoordinates + southCoordinates) / 2
                            WestChild = westCoordinates
                            EastChild = (eastCoordinates + westCoordinates) / 2
                        Case 1
                            'Second quadrant child Coordinates
                            NorthChild = northCoordinates
                            SouthChild = (northCoordinates + southCoordinates) / 2
                            WestChild = (eastCoordinates + westCoordinates) / 2
                            EastChild = eastCoordinates
                        Case 2
                            'Third quadrant child Coordinates
                            NorthChild = (northCoordinates + southCoordinates) / 2
                            SouthChild = southCoordinates
                            WestChild = westCoordinates
                            EastChild = (eastCoordinates + westCoordinates) / 2
                        Case 3
                            'Fourth quadrant child Coordinates
                            NorthChild = (northCoordinates + southCoordinates) / 2
                            SouthChild = southCoordinates
                            WestChild = (eastCoordinates + westCoordinates) / 2
                            EastChild = eastCoordinates
                    End Select
                    'Image depending on the child
                    If (deepLevel <= LEVEL_TO_MERGE) Then
                        'If (deepLevel = LEVEL_TO_MERGE) Then
                        'Check if this layer must be written into the kml file
                        'If (WestChild < MapRange(3) AndAlso NorthChild > MapRange(1)) Then 'JSA2016 Check condition
                        'Get the name for the old image
                        Dim image As String = SourcePath & "\" & deepLevel & "\" & nameChild & ".png"
                        If (Not My.Computer.FileSystem.FileExists(image)) Then
                            'Add an empty image to merge
                            imagesToMerge.Add(EmptyImage)
                        Else
                            'Add image to merge
                            imagesToMerge.Add(image)
                        End If
                        'Else
                        '    'Add an empty image to merge
                        '    imagesToMerge.Add(EmptyImage)
                        'End If
                    End If
                Next

                'Merge The file into the corresponding image
                If (imagesToMerge.Count > 0) Then
                    'From 0 to 3 merge the images found using the parent name
                    'Dim pathFile As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\test02.png"
                    Dim parentPath As String = SourcePath & "\" & (deepLevel - 1)

                    'Check if directory exist 
                    If (Not My.Computer.FileSystem.DirectoryExists(parentPath)) Then
                        My.Computer.FileSystem.CreateDirectory(parentPath)
                    End If

                    Dim imageIndex As Integer = 0
                    Dim files(1, 1) As String
                    For i As Integer = 0 To files.GetLength(0) - 1
                        For j As Integer = 0 To files.GetLength(1) - 1
                            files(i, j) = imagesToMerge(imageIndex)
                            imageIndex += 1
                        Next
                    Next
                    'Merge Image
                    Dim MergedImagePath As String = parentPath & "\" & CurrentLayer.Name & ".png"
                    Dim myImage As Image = ImageUtils.MergeImages(files)
                    ImageUtils.Save(myImage, MergedImagePath, ImageFormat.Png)
                    'Finally resample the image
                    Dim image As Image = ImageUtils.ResizeImage(MergedImagePath, 900, 900)
                    ImageUtils.Save(image, MergedImagePath, ImageFormat.Png)
                End If

            ElseIf deepLevel = MAX_LEVELS Then
                'Images for the deepest nodes

                'Get the name for the old image
                'System.Console.WriteLine("He llegado al final")

            End If

        Next

    End Sub

#End Region

#Region "Convert Images"
    Public Sub ConvertImages(ByVal pInputFolder As String, ByVal pOutputFolder As String, Optional OnlyVisibility As Boolean = True)
        'Check for the directories
        If (My.Computer.FileSystem.DirectoryExists(pInputFolder)) Then
            'Create the output
            If (Not My.Computer.FileSystem.DirectoryExists(pOutputFolder)) Then
                My.Computer.FileSystem.CreateDirectory(pOutputFolder)
            End If

            'Images to search
            Dim filters As New List(Of String)
            filters.Add("*.visible.txt")
            If (Not OnlyVisibility) Then
                filters.Add("*.geoterrainelev.txt")
            End If

            Dim enumerations(filters.Count - 1) As IEnumerable(Of String)
            Dim H_iterations As Integer = 0
            For index As Integer = 0 To filters.Count - 1
                enumerations(index) = Directory.EnumerateFiles(pInputFolder, filters(index), SearchOption.AllDirectories)
                H_iterations += New List(Of Object)(enumerations(index)).Count
            Next

            For index As Integer = 0 To filters.Count - 1
                'Dim current index
                Dim currentIdenx As Integer = index
                'Get the search founded for this filter
                Dim currentSearch As IEnumerable(Of String) = enumerations(index)
                Dim options As New ParallelOptions()
                options.MaxDegreeOfParallelism = PARALLEL_PROCESSES
                Parallel.ForEach(Of String)(currentSearch, options,
                    Sub(currentfile) 'Row of the Matrix
                        Dim outputPath As String = pOutputFolder & "\" & My.Computer.FileSystem.GetName(currentfile) & ".png"
                        Select Case currentIdenx
                            Case 0
                                'Create Visibility Image
                                ImageManager.CreateImageVisibility(outputPath, outputPath)
                            Case 1
                                'Create GEO Elevation Image
                                ImageManager.CreateImageGEOElevation(outputPath, outputPath)
                        End Select
                    End Sub)
            Next ' Next for
        End If
    End Sub

#End Region


#Region "Combine Images"
    Public Shared Sub CombineSourceImages(ByVal pSourcePath1 As String, ByVal pSourcePath2 As String, ByVal pOutputPath As String)
        'Check for the directories
        If (My.Computer.FileSystem.DirectoryExists(pSourcePath1) AndAlso My.Computer.FileSystem.DirectoryExists(pSourcePath2)) Then

            'Create the new output directory
            If (Not My.Computer.FileSystem.DirectoryExists(pOutputPath)) Then
                My.Computer.FileSystem.CreateDirectory(pOutputPath)
            End If

            Dim search As IEnumerable(Of String) = Directory.EnumerateFiles(pSourcePath1, "*.png", SearchOption.TopDirectoryOnly)
            'Get all the files in the current directory
            For Each file1 As String In search
                ' Original image file:  ello.tif.14400_216000.1.120.visible.txt.png
                Dim file1Name As String = My.Computer.FileSystem.GetName(file1)
                Dim values As String() = file1Name.Split("."c)
                If (values.Length = 8) Then
                    ' File parsing it's ok
                    ' Get current PRN in directory 1
                    Dim currentPRN As String = values(4)
                    Dim combinePRN As String = String.Empty
                    If (currentPRN = "120") Then
                        combinePRN = "136"
                    Else
                        combinePRN = "120"
                    End If
                    ' Generate file for path 2
                    Dim file2Name As String = file1Name.Replace("." & currentPRN & ".", "." & combinePRN & ".")

                    'Check if the file exists
                    If (My.Computer.FileSystem.FileExists(pSourcePath2 & "\" & file2Name)) Then
                        'Copy the image into the corresponding path
                        Dim outputFile As String = pOutputPath & "\" & file1Name.Replace("." & currentPRN & ".", ".comb.")

                        Dim myImage As Image = ImageUtils.CombineImages(file1, pSourcePath2 & "\" & file2Name)
                        If (myImage IsNot Nothing) Then
                            myImage.Save(outputFile, System.Drawing.Imaging.ImageFormat.Png)
                        End If

                    End If
                End If


            Next

        End If
    End Sub
#End Region

#Region "change color Images"
    Public Shared Sub ReplaceColorSourceImages(ByVal pSourcePath As String, ByVal pOutputPath As String, ByVal color As Color)
        'Check for the input directory
        If (My.Computer.FileSystem.DirectoryExists(pSourcePath)) Then

            'Create the new output directory
            If (Not My.Computer.FileSystem.DirectoryExists(pOutputPath)) Then
                My.Computer.FileSystem.CreateDirectory(pOutputPath)
            End If

            Dim search As IEnumerable(Of String) = Directory.EnumerateFiles(pSourcePath, "*.png", SearchOption.TopDirectoryOnly)
            'Get all the files in the current directory
            For Each file1 As String In search
                ' Original image file:  ello.tif.14400_216000.1.120.visible.txt.png
                Dim fileName As String = My.Computer.FileSystem.GetName(file1)

                'Copy the image into the corresponding path
                Dim outputFile As String = pOutputPath & "\" & fileName

                Dim myImage As Image = ImageUtils.ReplaceColorImage(file1, color)
                If (myImage IsNot Nothing) Then
                    myImage.Save(outputFile, System.Drawing.Imaging.ImageFormat.Png)
                End If

            Next

        End If
    End Sub
#End Region

#Region "Shared utils"

    Public Shared Sub OrganizeImages(path As String)
        SuperOverlayImages.MoveImages(path, path, False)
        SuperOverlayImages.RemoveDirectories(path)
    End Sub

    Public Shared Sub MoveImages(srcPath As String, dstPath As String, Optional TopDirectoryOnly As Boolean = True)
        'Create the output
        If (Not My.Computer.FileSystem.DirectoryExists(dstPath)) Then
            My.Computer.FileSystem.CreateDirectory(dstPath)
        End If
        Dim options As SearchOption = SearchOption.AllDirectories
        If (TopDirectoryOnly) Then
            options = SearchOption.TopDirectoryOnly
        End If
        Dim search As IEnumerable(Of String) = System.IO.Directory.EnumerateFiles(srcPath, "*.png", options)
        'Get all the files in the current directory
        For Each currentfile As String In search
            Dim fileName As String = My.Computer.FileSystem.GetName(currentfile)
            'Copy the image into the corresponding path
            If (currentfile <> dstPath & "\" & fileName) Then
                My.Computer.FileSystem.MoveFile(currentfile, dstPath & "\" & fileName, True)
            End If
        Next
    End Sub

    Public Shared Sub RemoveDirectories(path As String)
        Dim dir As DirectoryInfo = New DirectoryInfo(path)

        For Each di As DirectoryInfo In dir.GetDirectories()
            di.Delete()
        Next
    End Sub

#End Region


#Region "Resize Images (Not Used)"

    'Public Shared Sub ResizeImages(ByVal pSourcePath As String, ByVal pImagesPath As String)
    '    'Check for the directories
    '    If (My.Computer.FileSystem.DirectoryExists(pSourcePath)) Then

    '        'Create the new output
    '        If (Not My.Computer.FileSystem.DirectoryExists(pImagesPath)) Then
    '            My.Computer.FileSystem.CreateDirectory(pImagesPath)
    '        End If

    '        Dim search As IEnumerable(Of String) = Directory.EnumerateFiles(pSourcePath, "*.120.*.png", SearchOption.TopDirectoryOnly)

    '        'Get all the files in the current directory
    '        'For Each currentfile As String In search
    '        'Set the limit processes
    '        Dim options As New ParallelOptions()
    '        options.MaxDegreeOfParallelism = PARALLEL_PROCESSES
    '        Parallel.ForEach(Of String)(search, options,
    '            Sub(currentfile) 'Row of the Matrix
    '                'Resize the current image to the same folder
    '                Dim image As Image = ImageUtils.ResizeImage(currentfile, 900, 900)
    '                ImageUtils.Save(image, pImagesPath & "\" & My.Computer.FileSystem.GetName(currentfile), ImageFormat.Png)

    '            End Sub)
    '        'Next
    '    End If
    'End Sub

#End Region

End Class
