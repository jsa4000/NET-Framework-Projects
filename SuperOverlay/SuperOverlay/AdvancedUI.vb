Public Class AdvancedUI
    Implements IBaseUI

    Public ProcessorCount As Integer = Environment.ProcessorCount - 1 ' Used all cores but one, to be used for the OS

    Private control As IBaseUI = Nothing

    Private Sub AdvancedUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Create Basic UI
        control = New NormalUI()
        'Add the control to the main panel
        tabBasic.Controls.Add(CType(control, UserControl))
        'Load settings
        control.LoadSettings()
    End Sub


    Public Sub LoadSettings() Implements IBaseUI.LoadSettings
        Dim Settings_File As String = My.Application.Info.DirectoryPath & "\Settings\" & My.Application.Info.AssemblyName & ".xml"
        txtLevelPath.Text = ReadXMLSetting("Folders", "LevelPath", "", Settings_File)
        txtImagePath.Text = ReadXMLSetting("Folders", "ImagePath", "", Settings_File)
        txtInputFolderConvert.Text = ReadXMLSetting("Folders", "InputFolderConvert", "", Settings_File)
        txtOutputFolderConvert.Text = ReadXMLSetting("Folders", "OutputFolderConvert", "", Settings_File)
        txtInputFolderRename.Text = ReadXMLSetting("Folders", "InputFolderRename", "", Settings_File)
        txtOutputFolderRename.Text = ReadXMLSetting("Folders", "OutputFolderRename", "", Settings_File)
        txtInputFolderResize.Text = ReadXMLSetting("Folders", "InputFolderResize", "", Settings_File)
        txtInputFolderMerge.Text = ReadXMLSetting("Folders", "InputFolderMerge", "", Settings_File)
        txtInputFolder1Combine.Text = ReadXMLSetting("Folders", "InputFolder1Combine", "", Settings_File)
        txtInputFolder2Combine.Text = ReadXMLSetting("Folders", "InputFolder2Combine", "", Settings_File)
        txtOutputFolderCombine.Text = ReadXMLSetting("Folders", "OutputFolderCombine", "", Settings_File)
        txtInputFolderChange.Text = ReadXMLSetting("Folders", "InputFolderChange", "", Settings_File)
        txtOutputFolderChange.Text = ReadXMLSetting("Folders", "OutputFolderChange", "", Settings_File)

        nPRN.Value = Convert.ToDecimal(ReadXMLSetting("Options", "PRN", "120", Settings_File))
        nAlpha.Value = Convert.ToDecimal(ReadXMLSetting("Options", "Alpha", "85", Settings_File))
        Dim dimensions() As String = ReadXMLSetting("Options", "Dimensions", "70;25;-35;45", Settings_File).Split(";"c)
        nTop.Value = Convert.ToInt32(dimensions(0))
        nBottom.Value = Convert.ToInt32(dimensions(1))
        nLeft.Value = Convert.ToInt32(dimensions(2))
        nRight.Value = Convert.ToInt32(dimensions(3))

    End Sub

    Private Sub SaveSettings() Implements IBaseUI.SaveSettings
        Dim Settings_File As String = My.Application.Info.DirectoryPath & "\Settings\" & My.Application.Info.AssemblyName & ".xml"
        SaveXMLSetting("Folders", "LevelPath", txtLevelPath.Text, Settings_File)
        SaveXMLSetting("Folders", "ImagePath", txtImagePath.Text, Settings_File)
        SaveXMLSetting("Folders", "InputFolderConvert", txtInputFolderConvert.Text, Settings_File)
        SaveXMLSetting("Folders", "OutputFolderConvert", txtOutputFolderConvert.Text, Settings_File)
        SaveXMLSetting("Folders", "InputFolderRename", txtInputFolderRename.Text, Settings_File)
        SaveXMLSetting("Folders", "OutputFolderRename", txtOutputFolderRename.Text, Settings_File)
        SaveXMLSetting("Folders", "InputFolderResize", txtInputFolderResize.Text, Settings_File)
        SaveXMLSetting("Folders", "InputFolderMerge", txtInputFolderMerge.Text, Settings_File)
        SaveXMLSetting("Folders", "InputFolder1Combine", txtInputFolder1Combine.Text, Settings_File)
        SaveXMLSetting("Folders", "InputFolder2Combine", txtInputFolder2Combine.Text, Settings_File)
        SaveXMLSetting("Folders", "OutputFolderCombine", txtOutputFolderCombine.Text, Settings_File)
        SaveXMLSetting("Folders", "InputFolderChange", txtInputFolderChange.Text, Settings_File)
        SaveXMLSetting("Folders", "OutputFolderChange", txtOutputFolderChange.Text, Settings_File)

        SaveXMLSetting("Options", "PRN", nPRN.Value.ToString, Settings_File)
        SaveXMLSetting("Options", "Alpha", nAlpha.Value.ToString, Settings_File)
        Dim dimensions As String = String.Format("{0};{1};{2};{3}", nTop.Value, nBottom.Value, nLeft.Value, nRight.Value)
        SaveXMLSetting("Options", "Dimensions", dimensions, Settings_File)
    End Sub

    Private Sub EnableGUI(enable As Boolean)
        Me.Enabled = enable
    End Sub

    ''' <summary>
    ''' 
    ''' This method will create the SuperOverlay for the given PRN
    ''' The Path for the levels and images must be set prior to lunh this task.
    ''' 
    '''     NOTE: The tool will generate too much files so it could be heavy to process
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCreateSO_Click(sender As Object, e As EventArgs) Handles btnCreateSO.Click
        Dim PRN As Integer = nPRN.Value
        Dim levelPath As String = txtLevelPath.Text
        Dim ImagesPath As String = txtImagePath.Text

        EnableGUI(False)

        Dim superoverlay As New SuperOverlay(nTop.Value, nBottom.Value, nLeft.Value, nRight.Value, chkImages.Checked)
        'Set the alpha
        superoverlay.Alpha = nAlpha.Value
        superoverlay.CreateSuperOverlay(PRN.ToString(), levelPath, ImagesPath)

        MsgBox("Done!")

        EnableGUI(True)
    End Sub

    ''' <summary>
    ''' 
    ''' This method will search for all the files in folder and subfolders with the following filter ("*.visible.txt") 
    ''' Finally it generate one image per file with the proper palette for the given filter.
    ''' 
    ''' Also allows additional parameter to generate images
    '''          Public Sub ConvertImages(ByVal pInputFolder As String, ByVal pOutputFolder As String, Optional OnlyVisibility As Boolean = True)
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnConvert_Click(sender As System.Object, e As System.EventArgs) Handles btnConvert.Click
        'Createa a new visibility object

        Dim InputsFolder As String = txtInputFolderConvert.Text
        Dim OutputsFolder As String = txtOutputFolderConvert.Text

        EnableGUI(False)

        Dim superoverlay As New SuperOverlayImages(nTop.Value, nBottom.Value, nLeft.Value, nRight.Value)
        SuperOverlayImages.PARALLEL_PROCESSES = ProcessorCount
        superoverlay.ConvertImages(InputsFolder, OutputsFolder)

        MsgBox("Done!")

        EnableGUI(True)
    End Sub

    ''' <summary>
    '''  
    '''  This button is going to loop over a directory and search for images that its name has the following strucuture:
    '''  
    '''  ello.tif.28800_61200.1.136.visible.txt
    '''     ''' 
    ''' The function is going to generate the new name for the superoverlay (level 6 of 8). It take the images depending on the coordinates when generating the superoverlay tree.
    ''' PRN is required to take the proper files
    ''' 
    ''' The input file will be copied and the class will generate a new folder with the folder needed for resize and merge operations
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click

        Dim PRN As Integer = nPRN.Value
        Dim InputsFolder As String = txtInputFolderRename.Text
        Dim OutputsFolder As String = txtOutputFolderRename.Text

        EnableGUI(False)

        Dim superoverlay As New SuperOverlayImages(nTop.Value, nBottom.Value, nLeft.Value, nRight.Value)
        SuperOverlayImages.PARALLEL_PROCESSES = ProcessorCount
        superoverlay.RenameSourceImages(InputsFolder, OutputsFolder, PRN)

        MsgBox("Done!")

        EnableGUI(True)
    End Sub

    ''' <summary>
    ''' 
    ''' This method will split the images from layer 6 to 8. the starting image will have the following name:
    ''' 
    '''     0012001.ong  (3600x3600)
    '''     
    ''' The method will place the images into folders depending on the level from 7 to 8
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnResize_Click(sender As Object, e As EventArgs) Handles btnResize.Click

        Dim SourcePath As String = txtInputFolderResize.Text

        EnableGUI(False)

        Dim superoverlay As New SuperOverlayImages(nTop.Value, nBottom.Value, nLeft.Value, nRight.Value)
        SuperOverlayImages.PARALLEL_PROCESSES = ProcessorCount
        superoverlay.ResizeSourceImages(SourcePath)

        MsgBox("Done!")

        EnableGUI(True)

    End Sub



    ''' <summary>
    ''' 
    ''' This option will merge the images from the resized images in previous steps and recursively generate the upper levels for thesuper overla from 6 to 0.
    ''' 
    ''' This option need a directory as input and it will take the directory /6, /7, etc.. depending on the level its generating
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnMerge_Click(sender As Object, e As EventArgs) Handles btnMerge.Click
        Dim SourcePath As String = txtInputFolderMerge.Text

        EnableGUI(False)

        Dim superoverlay As New SuperOverlayImages(nTop.Value, nBottom.Value, nLeft.Value, nRight.Value)
        SuperOverlayImages.PARALLEL_PROCESSES = ProcessorCount
        superoverlay.MergeSourceImages(SourcePath)

        MsgBox("Done!")

        EnableGUI(True)
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
                cmdBrowse11.Click, cmdBrowse12.Click, cmdBrowse21.Click, cmdBrowse22.Click, cmdBrowse31.Click, cmdBrowse32.Click, cmdBrowse4.Click, cmdBrowse5.Click,
                cmdBrowse61.Click, cmdBrowse62.Click, cmdBrowse63.Click, cmdBrowse71.Click, cmdBrowse72.Click
        Dim currentTxt As TextBox = Nothing
        If (sender Is cmdBrowse11) Then
            currentTxt = txtLevelPath
        ElseIf (sender Is cmdBrowse12) Then
            currentTxt = txtImagePath
        ElseIf (sender Is cmdBrowse21) Then
            currentTxt = txtInputFolderConvert
        ElseIf (sender Is cmdBrowse22) Then
            currentTxt = txtOutputFolderConvert
        ElseIf (sender Is cmdBrowse31) Then
            currentTxt = txtInputFolderRename
        ElseIf (sender Is cmdBrowse32) Then
            currentTxt = txtOutputFolderRename
        ElseIf (sender Is cmdBrowse4) Then
            currentTxt = txtInputFolderResize
        ElseIf (sender Is cmdBrowse5) Then
            currentTxt = txtInputFolderMerge
        ElseIf (sender Is cmdBrowse61) Then
            currentTxt = txtInputFolder1Combine
        ElseIf (sender Is cmdBrowse62) Then
            currentTxt = txtInputFolder2Combine
        ElseIf (sender Is cmdBrowse63) Then
            currentTxt = txtOutputFolderCombine
        ElseIf (sender Is cmdBrowse71) Then
            currentTxt = txtInputFolderChange
        ElseIf (sender Is cmdBrowse72) Then
            currentTxt = txtOutputFolderChange
        End If

        With FolderBrowserDialog1
            .Reset()
            .Description = "Select Folder:"
            .SelectedPath = currentTxt.Text
            .ShowNewFolderButton = False
            Dim FolderDialogResult As DialogResult = .ShowDialog()
            If FolderDialogResult = Windows.Forms.DialogResult.OK Then
                currentTxt.Text = .SelectedPath
            End If
            .Dispose()
        End With
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        EnableGUI(False)

        Dim path1 As String = "D:\Tests\GEOTIFF\Converted_original_120"
        Dim path2 As String = "D:\Tests\GEOTIFF\Converted_original_136"
        Dim outputPath As String = "D:\Tests\GEOTIFF\Converted_original_comb"

        SuperOverlayImages.CombineSourceImages(path1, path2, outputPath)

        'ImageUtils.PARALLEL_PROCESSES = Environment.ProcessorCount
        'It creates a very sample image
        'ImageTest.CreateSampleImage()
        'ImageTest.CreateSampleImage2()
        'ImageTest.CreateSampleImage3()
        'ImageTest.CreateSampleImageResize()
        'ImageTest.CreateSampleImageMerge()
        'ImageTest.CreateSampleImageSplit2()
        'ImageTest.CreateSampleImageMerge3()

        'Dim PRN As Integer = 120
        'Dim levelPath As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\SuperOverlay"
        'Dim ImagesPath As String = "C:\Development\VB2015\GEOVisibility\Debug\Outputs\Images"

        'Dim superoverlay As New SuperOverlay()
        'SuperOverlay.PARALLEL_PROCESSES = Environment.ProcessorCount
        'superoverlay.CreateSuperOverlay(levelPath, ImagesPath)

        'SuperOverlayImages.ResizeImages("D:\Data\geotiff\SuperOverlay\GeoImages", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\GEOTerrain")
        'SuperOverlayImages.MergeRegionImages("C:\Development\VB2015\GEOVisibility\Debug\Outputs\GEOTerrain", "C:\Development\VB2015\GEOVisibility\Debug\Outputs\GEOTerrain\Merged")
        'SuperOverlayImages.MergeRegionImages("C:\TEMP", "C:\TEMP")

        'Dim SourcePath As String = txtInputFolderMerge.Text
        'SuperOverlayImages.OrganizeImages(SourcePath)

        MsgBox("Done!")

        EnableGUI(True)
    End Sub

    Private Sub btnOrganize_Click(sender As Object, e As EventArgs) Handles btnOrganize.Click
        EnableGUI(False)

        Dim SourcePath As String = txtInputFolderMerge.Text
        SuperOverlayImages.OrganizeImages(SourcePath)

        MsgBox("Done!")

        EnableGUI(True)
    End Sub

    Private Sub btnCombine_Click(sender As Object, e As EventArgs) Handles btnComb.Click
        EnableGUI(False)

        Dim path1 As String = txtInputFolder1Combine.Text
        Dim path2 As String = txtInputFolder2Combine.Text
        Dim outputPath As String = txtOutputFolderCombine.Text
        SuperOverlayImages.CombineSourceImages(path1, path2, outputPath)

        MsgBox("Done!")

        EnableGUI(True)
    End Sub

    Private Sub btnchange_Click(sender As Object, e As EventArgs) Handles btnchange.Click
        EnableGUI(False)

        Dim path As String = txtInputFolderChange.Text
        Dim outputPath As String = txtOutputFolderChange.Text
        Dim color As Color = btnColor.BackColor
        SuperOverlayImages.ReplaceColorSourceImages(path, outputPath, color)

        MsgBox("Done!")

        EnableGUI(True)
    End Sub

    Private Sub btyColor_Click(sender As Object, e As EventArgs) Handles btnColor.Click
        If (ColorDialog1.ShowDialog() = DialogResult.OK) Then
            btnColor.BackColor = ColorDialog1.Color
        End If

    End Sub

End Class
