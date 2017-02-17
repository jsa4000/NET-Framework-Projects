Public Class NormalUI
    Implements IBaseUI

    Public ProcessorCount As Integer = Environment.ProcessorCount - 1 ' Used all cores but one, to be used for the OS

    Public Sub LoadSettings() Implements IBaseUI.LoadSettings
        Dim Settings_File As String = My.Application.Info.DirectoryPath & "\Settings\" & My.Application.Info.AssemblyName & ".xml"
        txtInputFolder.Text = ReadXMLSetting("Folders", "InputFolder", "", Settings_File)
        txtOutputFolder.Text = ReadXMLSetting("Folders", "OutputFolder", "", Settings_File)
        txtPRN.Text = ReadXMLSetting("Options", "PRN", "120", Settings_File)
        nAlpha.Value = Convert.ToDecimal(ReadXMLSetting("Options", "Alpha", "85", Settings_File))
        Dim dimensions() As String = ReadXMLSetting("Options", "Dimensions", "70;25;-35;45", Settings_File).Split(";"c)
        nTop.Value = Convert.ToInt32(dimensions(0))
        nBottom.Value = Convert.ToInt32(dimensions(1))
        nLeft.Value = Convert.ToInt32(dimensions(2))
        nRight.Value = Convert.ToInt32(dimensions(3))
    End Sub

    Private Sub SaveSettings() Implements IBaseUI.SaveSettings
        Dim Settings_File As String = My.Application.Info.DirectoryPath & "\Settings\" & My.Application.Info.AssemblyName & ".xml"
        SaveXMLSetting("Folders", "InputFolder", txtInputFolder.Text, Settings_File)
        SaveXMLSetting("Folders", "OutputFolder", txtOutputFolder.Text, Settings_File)
        SaveXMLSetting("Options", "PRN", txtPRN.Text, Settings_File)
        SaveXMLSetting("Options", "Alpha", nAlpha.Value.ToString, Settings_File)
        Dim dimensions As String = String.Format("{0};{1};{2};{3}", nTop.Value, nBottom.Value, nLeft.Value, nRight.Value)
        SaveXMLSetting("Options", "Dimensions", dimensions, Settings_File)
    End Sub

    Private Sub EnableGUI(enable As Boolean)
        Me.Enabled = enable
    End Sub

    Private Sub UpdateStatus(status As String)
        txtStatus.Text = status
        My.Application.DoEvents()
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Dim Alpha As Decimal = nAlpha.Value
        Dim PRN As String = txtPRN.Text
        Dim ConvertFolder As String = txtConvertFolder.Text
        Dim InputsFolder As String = txtInputFolder.Text
        Dim OutputsFolder As String = txtOutputFolder.Text
        Dim imagesFolder As String = "Images"

        EnableGUI(False)

        UpdateStatus("Started")

        'Create Super Overlay
        Dim superoverlay As New SuperOverlay(nTop.Value, nBottom.Value, nLeft.Value, nRight.Value)
        superoverlay.Alpha = nAlpha.Value

        UpdateStatus("Creating Super Overlay...")
        'Create Super Overlay
        superoverlay.CreateSuperOverlay(PRN, OutputsFolder, imagesFolder)

        Dim SOImages As New SuperOverlayImages(nTop.Value, nBottom.Value, nLeft.Value, nRight.Value)
        SuperOverlayImages.PARALLEL_PROCESSES = ProcessorCount
        'Check if the images must be created from the original text files
        If (chkConvert.Checked) Then
            UpdateStatus("Converting Images...")
            SOImages.ConvertImages(ConvertFolder, InputsFolder)
        End If
        'Change the outfolder to generate the images
        OutputsFolder &= "\" & imagesFolder
        'Rename the converted files and copy them into the desired folder
        UpdateStatus("Renaming Files")
        SOImages.RenameSourceImages(InputsFolder, OutputsFolder, PRN)
        'Resize the images
        UpdateStatus("Resizing images")
        SOImages.ResizeSourceImages(OutputsFolder)
        ''Merge images
        UpdateStatus("Merging Images...")
        SOImages.MergeSourceImages(OutputsFolder)

        ''Organize images 'NO MORE NECCESARY
        'UpdateStatus("Organizing files...")
        'SuperOverlayImages.OrganizeImages(OutputsFolder)

        UpdateStatus("Finished")

        MsgBox("Done!")

        EnableGUI(True)
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
                btnBrowse10.Click, cmdBrowse11.Click, cmdBrowse12.Click
        Dim currentTxt As TextBox = Nothing
        If (sender Is btnBrowse10) Then
            currentTxt = txtConvertFolder
        ElseIf (sender Is cmdBrowse11) Then
            currentTxt = txtInputFolder
        ElseIf (sender Is cmdBrowse12) Then
            currentTxt = txtOutputFolder
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

    Private Sub chkConvert_CheckedChanged(sender As Object, e As EventArgs) Handles chkConvert.CheckedChanged
        btnBrowse10.Enabled = chkConvert.Checked
        txtConvertFolder.Enabled = chkConvert.Checked
    End Sub

End Class
