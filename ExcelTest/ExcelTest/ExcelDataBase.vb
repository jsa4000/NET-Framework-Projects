'*********************************************************************
' MODULE: Excel Data Base
' FILENAME: ExcelDataBase.vb
' AUTHOR: Javier Santos
' DEPENDENCIES: None
'
' DESCRIPTION:
' This module allow the interaction through Excel files and .NET applications.
'
' MODIFICATION HISTORY:
' 1.0.0 27-Apr-2015 JSA - Initial Version
'*********************************************************************
Imports System.Threading
Imports System.Globalization

#Const USE_EXCEL_LIBARARY = False

Public Class ExcelDataBase

    Private Const xlWorkbookNormal As Integer = -4143
    Private Const xlShared As Integer = 2
    Private Const DEFAULT_SHEET_NAME As String = "Sheet1"

    Private ExcelFile As String = String.Empty

    Private IsOpened As Boolean = False

#If USE_EXCEL_LIBARARY Then
   'Create obects
    Private xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = Nothing
    Private xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing
    Private xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
#Else
    'Create obects
    Private xlWorkBook As Object = Nothing
    Private xlWorkSheet As Object = Nothing
    Private xlApp As Object = Nothing
#End If

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="FilePath"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal FilePath As String)
        ExcelFile = FilePath
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <remarks></remarks>
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pWorkBook"></param>
    ''' <param name="pName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function HasWorkSheet(ByVal pWorkBook As Object, ByVal pName As String)
        For Each xlWorkSheet As Object In pWorkBook.Sheets
            If (xlWorkSheet.Name = pName) Then
                Return True
            End If
        Next
        Return False
    End Function


    ' Yo eliminaría la variable Initialize, ya que cuando se crea el fichero excel siempre querremos inicializarlo
    ' Además lo sustituiría por una variable ReadOnly para cuando queremos leer valores y así abrir
    ' el excel como solo lectura, y solo cuando existe el fichero sin crearlo ni a él ni a su directorio.
    ''' <summary>
    ''' ''''
    ''' </summary>
    ''' <param name="IsReadOnly"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Open(Optional ByVal IsReadOnly As Boolean = False) As Boolean
        Dim result As Boolean = True

        'Check the path is correct
        If (String.IsNullOrEmpty(ExcelFile)) Then Return False

        'Check if already opened
        If (IsOpened) Then Close()

        Try
            'Check if the directory exists
            Dim OutputFolder As String = My.Computer.FileSystem.GetParentPath(ExcelFile)
            If (Not My.Computer.FileSystem.DirectoryExists(OutputFolder)) Then
                If (IsReadOnly) Then Return False
                My.Computer.FileSystem.CreateDirectory(OutputFolder)
            End If

            'Set the current language
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

            'Create the COM component
            xlApp = CreateObject("Excel.Application")

            'Check if Excel has been installled
            If xlApp Is Nothing Then
                Return False
            End If

            'Set document properties
            xlApp.Visible = False
            xlApp.DisplayAlerts = False

            'Create the Workbook
            If (My.Computer.FileSystem.FileExists(ExcelFile)) Then
                'Opens the file 
                xlWorkBook = xlApp.Workbooks.Open(ExcelFile, False, IsReadOnly)
            Else
                'Create a new file
                xlWorkBook = xlApp.Workbooks.Add()
                'Initialize the variables

                'Delete intial Sheets from the workbook
                Dim worksheet As Object
                For i As Integer = 1 To xlWorkBook.Sheets.Count - 1
                    'Delete the sheet
                    worksheet = xlWorkBook.Sheets(i)
                    worksheet.Delete()
                Next
                'Rename the initial (active) Sheet
                worksheet = xlWorkBook.ActiveSheet
                worksheet.Name = DEFAULT_SHEET_NAME
  
            End If
            'Set is opened to true
            IsOpened = True
        Catch ex As Exception
            'Close the document without saving if there has been an error
            Close()
            'Returns error
            result = False
        End Try
        'Return the value
        Return result
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Save"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Close(Optional ByVal Save As Boolean = False) As Boolean
        Dim result As Boolean = True

        If (Not IsOpened) Then Return True

        Try
            'Check if we must save the docuemnt
            If (Save) Then
                'Save the document and Close it if available
                If (Not xlWorkBook.ReadOnly) Then
                    'Save the document
                    'xlWorkBook.SaveAs(ExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, xlShared, False, False, Type.Missing, Type.Missing, Type.Missing)
                    xlWorkBook.SaveAs(ExcelFile)
                Else
                    'Error while saving the document
                    result = False
                End If
            End If

            'Finally dispose all the elements created
            If (xlWorkSheet IsNot Nothing) Then
                'Dispose all the elements
                releaseObject(xlWorkSheet)
                xlWorkSheet = Nothing
            End If

            If (xlWorkBook IsNot Nothing) Then
                'Close the wworkbook
                xlWorkBook.Close()
                'Dispose all the elements
                releaseObject(xlWorkBook)
                xlWorkBook = Nothing
            End If

            If (xlApp IsNot Nothing) Then
                'Exit the application
                xlApp.Quit()
                'Dispose all the elements
                releaseObject(xlApp)
                xlApp = Nothing
            End If

            'Initialize variables
            IsOpened = False

        Catch ex As Exception
            'Error while closing or saving the file
            result = False
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Returns the index of the column for the specified header
    ''' If not founded returns the last empty column 
    ''' </summary>
    ''' <param name="Header"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetColumnHeader(ByVal pWorkSheet As Object, ByVal Header As String) As Integer
        Dim columnIndex As Integer = 1
        While pWorkSheet.Cells(1, columnIndex).Value <> "" AndAlso pWorkSheet.Cells(1, columnIndex).Value <> Header
            columnIndex += 1
        End While
        'Check if the header is a new one
        If (pWorkSheet.Cells(1, columnIndex).Value = "") Then
            'Add the header's name
            pWorkSheet.Cells(1, columnIndex) = Header
            'Format the columns header
            pWorkSheet.Cells(1, columnIndex).Font.Bold = True
            pWorkSheet.Cells(1, columnIndex).ColumnWidth = 15
        End If
        Return columnIndex
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="SheetName"></param>
    ''' <param name="RowData"></param>
    ''' <param name="RowIndex"></param>
    ''' <param name="Headers"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddRow(ByVal SheetName As String, ByVal RowIndex As Integer, ByVal RowData() As Object, ByVal Headers As String()) As Boolean
        Dim result As Boolean = True
        'Check if the document was already opened 
        If (IsOpened) Then
            Try
                'Check whether the sheet already exists
                If (HasWorkSheet(xlWorkBook, SheetName)) Then
                    'Modify the Sheet
                    xlWorkSheet = xlWorkBook.Sheets(SheetName)
                Else
                    'Create the Sheet
                    If (HasWorkSheet(xlWorkBook, DEFAULT_SHEET_NAME)) Then
                        'Modify the default sheet if it was initialized Sheet
                        xlWorkSheet = xlWorkBook.Sheets(DEFAULT_SHEET_NAME)
                        xlWorkSheet.Name = SheetName
                    Else
                        xlWorkSheet = xlWorkBook.Sheets.Add()
                        xlWorkSheet.Name = SheetName
                    End If
                    'Freeze the top row
                    xlWorkSheet.Cells(2, 2).Select()
                    xlApp.ActiveWindow.FreezePanes = True
                End If
                'Add the data into the worksheet
                For index As Integer = 0 To Headers.Length - 1
                    'Get the column index for this Header.
                    If (Headers(index) <> "") Then
                        Dim column As Integer = GetColumnHeader(xlWorkSheet, Headers(index))
                        xlWorkSheet.Cells(RowIndex, column) = RowData(index)
                    End If
                Next

                'Select the cell to save the file in that position
                xlWorkSheet.Activate()
                xlWorkSheet.Select()
                xlWorkSheet.Cells(RowIndex, 1).Select()
                Dim ScrollRow As Integer = RowIndex - 20
                If ScrollRow < 2 Then ScrollRow = 2
                xlApp.ActiveWindow.ScrollRow = ScrollRow
                xlApp.ActiveWindow.ScrollColumn = 2
            Catch ex As Exception
                'Error while adding the row
                result = False
            End Try
        Else
            'Document not openend
            result = False
        End If
        'Return the results
        Return result
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="SheetName"></param>
    ''' <param name="RowDate"></param>
    ''' <param name="Header"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadData(ByVal SheetName As String, ByVal RowDate As Date, ByVal Header As String) As Double
        'Check if the document was already opened 
        If (IsOpened) Then
            Try
                'Check whether the sheet already exists
                If (HasWorkSheet(xlWorkBook, SheetName)) Then
                    'Read the data from the worksheet
                    xlWorkSheet = xlWorkBook.Sheets(SheetName)
                    Dim column As Integer = GetColumnHeader(xlWorkSheet, Header)
                    If xlWorkSheet.Cells(RowDate.DayOfYear + 1, column).Text <> "" Then
                        Return xlWorkSheet.Cells(RowDate.DayOfYear + 1, column).Value
                    End If
                End If
            Catch ex As Exception
                'Error while adding the row
                Return -20000000000
            End Try
        End If
        Return -20000000000
    End Function

End Class
