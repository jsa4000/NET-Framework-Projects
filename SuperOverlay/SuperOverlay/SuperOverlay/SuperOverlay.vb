Imports System.Threading.Tasks
Imports System.Drawing.Imaging

Public Class SuperOverlay
    'Public PARENT_TAG As String = "NetworkLink"
    Public PARENT_TAG As String = "Folder"

    Public Const MAX_LEVELS As Integer = 10

    Public Const MAX_QUADRANTS As Integer = 4
    Public Const CREATE_TEMP_IMAGE As Boolean = False

    Dim ImagesLocalPath As String = "Images"
    Dim OutputPath As String = String.Empty

    Public CreateImages As Boolean = False
    Public Alpha As Decimal = 85

    Public minLodPixels As Decimal = 500 '900
    Public maxLodPixels As Decimal = 1800 '1800
    Public minFadeExtent As Decimal = 400
    Public maxFadeExtent As Decimal = 450

    Private kml As System.IO.StreamWriter

    'Public MapRange() As Decimal = {75, 25, -35, 45}
    Public MapRange() As Decimal = {70, 65, 15, 20} '(75, 50, 5, 45)

    Dim ImageSize As Integer = 900 '  Each Image in pixels
    Dim OriginalImageSize As Integer = 3600 '  Each Image in pixels
    Dim ImageDegrees As Double = 0.25 '  Degrees at the lowest level -> ImageSize / 0.25 = Image Pixels for 1 degree

    Private Class Layer
        Public Name As String = String.Empty
        'Public localpath As String = String.Empty
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

    Public Sub New(Top As Integer, Bottom As Integer, Left As Integer, Right As Integer, Optional pCreateImages As Boolean = CREATE_TEMP_IMAGE)
        MapRange = {Top, Bottom, Left, Right}
        CreateImages = pCreateImages
    End Sub

    Public Function GetAlphaHex() As String
        Return Hex(Int((Alpha / 100) * 255)).PadLeft(2, "0"c) 'Pad with 0's for alpha values = 0
    End Function


    Public Sub CreateSuperOverlay(ByVal PRN As String, ByVal pOutputpath As String, ByVal pImagesLocalPath As String)

        OutputPath = pOutputpath
        'Check if directory exist 
        If (Not My.Computer.FileSystem.DirectoryExists(OutputPath)) Then
            My.Computer.FileSystem.CreateDirectory(OutputPath)
        End If

        ImagesLocalPath = pImagesLocalPath

        'Check if directory exist 
        If (Not My.Computer.FileSystem.DirectoryExists(OutputPath & "\" & ImagesLocalPath)) Then
            My.Computer.FileSystem.CreateDirectory(OutputPath & "\" & ImagesLocalPath)
        End If

        'Create the initial level
        Dim ZeroLayer As New Layer()
        ZeroLayer.Name = "0"
        'Create the initial quadrants
        For index As Integer = 0 To MAX_QUADRANTS - 1
            ZeroLayer.NextQuadrants(index) = ZeroLayer.Name & index.ToString()
        Next

        Dim MapPixelsHeight As Integer = (MapRange(0) - MapRange(1)) * OriginalImageSize
        Dim MapPixelsWidth As Integer = (MapRange(3) - MapRange(2)) * OriginalImageSize

        ZeroLayer.North = MapRange(0)
        ZeroLayer.South = MapRange(1) - (((Math.Pow(2, MAX_LEVELS) * ImageSize) - MapPixelsHeight) / (ImageSize / ImageDegrees)) / 2
        ZeroLayer.West = MapRange(2)
        ZeroLayer.East = ((((Math.Pow(2, MAX_LEVELS) * ImageSize) - MapPixelsWidth) / (ImageSize / ImageDegrees)) / 2) + MapRange(3)

        'Kml Files in specific quadrant
        kml = My.Computer.FileSystem.OpenTextFileWriter(OutputPath & "/PRN" & PRN & ".kml", False, System.Text.Encoding.UTF8)
        kml.WriteLine("<?xml version='1.0' encoding='UTF-8'?>")
        kml.WriteLine("<kml xmlns='http://earth.google.com/kml/2.1'>")
        kml.WriteLine("<Document>")

        'Generate Levels from zero
        GenerateLevel(ZeroLayer, 1, 0)

        kml.WriteLine("</Document>")
        kml.WriteLine("</kml>")

        kml.Close()
    End Sub

    Private Sub GenerateLevel(ByVal CurrentLayer As Layer, ByVal deepLevel As Integer, ByVal drawOrder As Integer)
        'Base case for recurrent calls
        If (deepLevel <= MAX_LEVELS AndAlso CurrentLayer.West < MapRange(3) AndAlso CurrentLayer.North > MapRange(1)) Then
            'If (deepLevel <= MAX_LEVELS AndAlso Intersect(CurrentLayer)) Then
            kml.WriteLine("<" & PARENT_TAG & ">")
            kml.WriteLine("<name>" + CurrentLayer.Name + "</name>")
            kml.WriteLine("<Region>")
            kml.WriteLine("<Lod>")
            kml.WriteLine("<minLodPixels>" & minLodPixels & "</minLodPixels>")
            If deepLevel = MAX_LEVELS Then
                kml.WriteLine("<maxLodPixels>-1</maxLodPixels>")
            Else
                kml.WriteLine("<maxLodPixels>" & maxLodPixels & "</maxLodPixels>")
            End If
            kml.WriteLine("<minFadeExtent>" & minFadeExtent & "</minFadeExtent>")
            kml.WriteLine("<maxFadeExtent>" & maxFadeExtent & "</maxFadeExtent>")
            kml.WriteLine("</Lod>")
            kml.WriteLine("<LatLonAltBox>")
            kml.WriteLine("<north>" + CStr(CurrentLayer.North) + "</north><south>" + CStr(CurrentLayer.South) + "</south>")
            kml.WriteLine("<west>" + CStr(CurrentLayer.West) + "</west><east>" + CStr(CurrentLayer.East) + "</east>")
            kml.WriteLine("</LatLonAltBox>")
            kml.WriteLine("</Region>")

            kml.WriteLine("<GroundOverlay>")
            kml.WriteLine("<drawOrder>" + CStr(drawOrder) + "</drawOrder>")
            kml.WriteLine("<color>" & GetAlphaHex() & "FFFFFF</color>")
            kml.WriteLine("<Icon>")
            kml.WriteLine("<href>" & ImagesLocalPath & "\" & (deepLevel - 1) & "\" & CurrentLayer.Name & ".png</href>")
            kml.WriteLine("</Icon>")
            kml.WriteLine("<LatLonBox>")

            kml.WriteLine("<north>" + CStr(CurrentLayer.North) + "</north><south>" + CStr(CurrentLayer.South) + "</south>")
            kml.WriteLine("<west>" + CStr(CurrentLayer.West) + "</west><east>" + CStr(CurrentLayer.East) + "</east>")
            kml.WriteLine("</LatLonBox>")
            kml.WriteLine("</GroundOverlay>")

            'Set the limit processes
            For index = 0 To MAX_QUADRANTS - 1
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

                'Prepare for the nex recursive call
                For j As Integer = 0 To MAX_QUADRANTS - 1
                    NextLayer.NextQuadrants(j) &= j.ToString()
                Next
                'Recursive calls to generate next level of KML Files
                GenerateLevel(NextLayer, deepLevel + 1, drawOrder + 1)
            Next
            kml.WriteLine("</" & PARENT_TAG & ">")
        End If
    End Sub

End Class
