Imports System.Threading
Imports System.Globalization

#Const USE_EXCEL_LIBARARY = False

Public Class Form1
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Dim initTime As DateTime = DateTime.Now
        Dim App_Folder As String = My.Application.Info.DirectoryPath
        Dim OutputHeaders() As String = {"Date", "Column1", "Column2"}

        Dim Data() As String = {initTime.ToShortDateString(), "1", "2"}

        'Get the Excel file to be loaded
        Dim xlFilePath As String = App_Folder & "\Outputs\" & initTime.Year & "_Receivers_Performance_Historical.xlsx"
        'Create the excel database Class
        Dim xlDatabase As New ExcelDataBase(xlFilePath)
        'Open the file where the data will be stored
        If (xlDatabase.Open()) Then
            'Write the data into the excel file database
            If (xlDatabase.AddRow("Javier", initTime.DayOfYear + 1, Data, OutputHeaders)) Then

                Dim NewOutputHeaders() As String = {"Column2"}
                Dim NewData() As String = {"Column2 New"}

                xlDatabase.AddRow("Javier", initTime.DayOfYear + 1, NewData, NewOutputHeaders)

                Dim NewOutputHeaders2() As String = {"Column3"}
                Dim NewData2() As String = {"Column3 Data"}

                xlDatabase.AddRow("Javier", initTime.DayOfYear + 1, NewData2, NewOutputHeaders2)

                'Close and save the file
                xlDatabase.Close(True)
            Else
                'If error close wihout saving
                xlDatabase.Close()
            End If
        End If

        'End process
        MsgBox("Done")

    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        Dim initTime As DateTime = DateTime.Now
        Dim App_Folder As String = My.Application.Info.DirectoryPath
        Dim OutputHeaders() As String = {"Date", "Column1", "Column2"}

        Dim Data() As String = {initTime.ToShortDateString(), "1", "2"}

        'Get the Excel file to be loaded
        Dim xlFilePath As String = App_Folder & "\Outputs\" & initTime.Year & "_Receivers_Performance_Historical.xlsx"
        'Create the excel database Class
        Dim xlDatabase As New ExcelDataBase(xlFilePath)
        'Open the file where the data will be stored
        If (xlDatabase.Open(chkReadOnly.Checked)) Then
            'Write the data into the excel file database
            If (xlDatabase.AddRow("Santos", initTime.DayOfYear + 1, Data, OutputHeaders)) Then

                Dim NewOutputHeaders() As String = {"Column3"}
                Dim NewData() As String = {"Column3 New"}

                xlDatabase.AddRow("Santos", initTime.DayOfYear + 1, NewData, NewOutputHeaders)

                Dim NewOutputHeaders2() As String = {"Column4"}
                Dim NewData2() As String = {"Column4 Data"}

                xlDatabase.AddRow("Santos", initTime.DayOfYear + 1, NewData2, NewOutputHeaders2)

                'Close and save the file
                xlDatabase.Close(True)
            Else
                'If error close wihout saving
                xlDatabase.Close()
            End If
        End If

        'End process
        MsgBox("Done")
    End Sub

    Private Sub btnStartOS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartOS.Click
        Dim initTime As DateTime = DateTime.Now
        'Dim initTime As DateTime = New Date(2015, 1, 1)
        Dim App_Folder As String = My.Application.Info.DirectoryPath
        Dim OutputHeaders() As String = {"Date", "Column1", "Column2"}

        Dim Data() As Object = {initTime.ToShortDateString(), 1, "2"}

        'Get the Excel file to be loaded
        Dim xlFilePath As String = App_Folder & "\Outputs\" & initTime.Year & "_Receivers_Performance_Historical.xlsx"
        'Create the excel database Class
        Dim xlDatabase As New ExcelDataBaseOS(xlFilePath)
        'Open the file where the data will be stored
        If (xlDatabase.Open()) Then
            'Write the data into the excel file database
            'If (xlDatabase.AddRow("Javier", initTime.DayOfYear + 1, Data, OutputHeaders)) Then
            If (xlDatabase.AddRow("Javier", initTime.DayOfYear + 1, Data, OutputHeaders)) Then

                Dim NewOutputHeaders() As String = {"Column2"}
                Dim NewData() As String = {"Column2 New"}

                xlDatabase.AddRow("Javier", initTime.DayOfYear + 1, NewData, NewOutputHeaders)

                Dim NewOutputHeaders2() As String = {"Column3"}
                Dim NewData2() As String = {"Column3 Data"}

                xlDatabase.AddRow("Javier", initTime.DayOfYear + 1, NewData2, NewOutputHeaders2)

                'Close and save the file
                xlDatabase.Close(True)
            Else
                'If error close wihout saving
                xlDatabase.Close()
            End If
        End If

        'End process
        MsgBox("Done")
    End Sub

    Private Sub btnModifyOS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModifyOS.Click
        Dim initTime As DateTime = DateTime.Now
        Dim App_Folder As String = My.Application.Info.DirectoryPath
        Dim OutputHeaders() As String = {"Date", "Column1", "Column2"}

        Dim Data() As Object = {initTime.ToShortDateString(), 345.45, "2"}

        'Get the Excel file to be loaded
        Dim xlFilePath As String = App_Folder & "\Outputs\" & initTime.Year & "_Receivers_Performance_Historical.xlsx"
        'Create the excel database Class
        Dim xlDatabase As New ExcelDataBaseOS(xlFilePath)
        'Open the file where the data will be stored
        If (xlDatabase.Open(chkReadOnly.Checked)) Then
            'Write the data into the excel file database
            If (xlDatabase.AddRow("Santos", initTime.DayOfYear - 20, Data, OutputHeaders)) Then

                Dim NewOutputHeaders() As String = {"Column3"}
                Dim NewData() As String = {"Column3 New"}

                xlDatabase.AddRow("Santos", initTime.DayOfYear - 20, NewData, NewOutputHeaders)

                Dim NewOutputHeaders2() As String = {"Column4"}
                Dim NewData2() As String = {"Column4 Data"}

                xlDatabase.AddRow("Santos", initTime.DayOfYear - 20, NewData2, NewOutputHeaders2)

                'Close and save the file
                xlDatabase.Close(True)
            Else
                'If error close wihout saving
                xlDatabase.Close()
            End If
        End If

        'End process
        MsgBox("Done")
    End Sub

    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click

        Dim pck As New OfficeOpenXml.ExcelPackage(New System.IO.FileInfo("c:\test.xlsx"))
        Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets.Add("Sample1")

        ws.View.ActiveCell = "H100"


        ws.Cells("A1").Value = "Sample 1"
        ws.Cells("A1").Style.Font.Bold = True

        ws.View.FreezePanes(2, 1)
        ws.View.Panes(0).TopLeftCell = "A85"

        pck.Save()
    End Sub
End Class
