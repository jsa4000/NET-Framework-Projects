'*********************************************************************
' MODULE: Excel Data Base
' FILENAME: ExcelDataBase.vb
' AUTHOR: Javier Santos
' DEPENDENCIES: EPPlus.dll v4.0.4
'
' DESCRIPTION:
' This module allow the interaction through Excel files and .NET applications.
'
' MODIFICATION HISTORY:
' 1.0.0 27-Apr-2015 JSA - Initial Version
' 2.0.0 14-Jul-2015 JSA - Replaced the use of Microsoft.Office.Interop.Excel libraries, so the installation of Microsoft Office is no longer needed.
'*********************************************************************
Imports System.Threading
Imports System.Globalization
Imports OfficeOpenXml
Imports System.IO

Public Class ExcelDataBaseOS

    Private ExcelFile As String = String.Empty
    Private IsOpened As Boolean = False
    Private IsReadOnly As Boolean = False

    'Create objects
    Private xlPackage As ExcelPackage = Nothing

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
    ''' <param name="pWorkBook"></param>
    ''' <param name="pName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function HasWorkSheet(ByVal pWorkBook As ExcelWorkbook, ByVal pName As String)
        For Each xlWorkSheet As ExcelWorksheet In pWorkBook.Worksheets
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

        'Set read only for the docuemnt
        IsReadOnly = IsReadOnly

        'Check if already opened
        If (IsOpened) Then Close()

        Try
            'If ReadOnly, check if file already exists
            If IsReadOnly AndAlso Not My.Computer.FileSystem.FileExists(ExcelFile) Then
                Return False
            End If

            'Check if the directory exists
            Dim OutputFolder As String = My.Computer.FileSystem.GetParentPath(ExcelFile)
            If (Not My.Computer.FileSystem.DirectoryExists(OutputFolder)) Then
                My.Computer.FileSystem.CreateDirectory(OutputFolder)
            End If

            ' 'Load or create the Workbook
            Dim file As New FileInfo(ExcelFile)
            xlPackage = New ExcelPackage(file)

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
            If (Save AndAlso Not IsReadOnly) Then
                'Save the document
                xlPackage.Save()
            End If

            'Initialize variables
            IsOpened = False

        Catch ex As Exception
            'Error while closing or saving the file
            result = False
        Finally
            'Finally dispose the excel
            If (xlPackage IsNot Nothing) Then
                'Exit the application
                xlPackage.Dispose()
                'Dispose all the elements
                xlPackage = Nothing
            End If
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
    Private Function GetColumnHeader(ByVal pWorkSheet As ExcelWorksheet, ByVal Header As String) As Integer
        Dim columnIndex As Integer = 1
        While pWorkSheet.Cells(1, columnIndex).Value <> "" AndAlso pWorkSheet.Cells(1, columnIndex).Value <> Header
            columnIndex += 1
        End While
        'Check if the header is a new one
        If (pWorkSheet.Cells(1, columnIndex).Value = "") Then
            'Add the header's name
            pWorkSheet.Cells(1, columnIndex).Value = Header
            'Format the columns header
            pWorkSheet.Cells(1, columnIndex).Style.Font.Bold = True
            pWorkSheet.Column(columnIndex).Width = 15
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
                Dim xlWorkBook As ExcelWorkbook = xlPackage.Workbook
                Dim xlWorkSheet As ExcelWorksheet
                'Check whether the sheet already exists
                If (HasWorkSheet(xlWorkBook, SheetName)) Then
                    'Modify the Sheet
                    xlWorkSheet = xlWorkBook.Worksheets(SheetName)
                Else
                    'Create the Sheet
                    xlWorkSheet = xlWorkBook.Worksheets.Add(SheetName)
                    'Freeze the panes again
                    xlWorkSheet.View.FreezePanes(2, 2)
                End If

                'Add the data into the worksheet
                For index As Integer = 0 To Headers.Length - 1
                    'Get the column index for this Header.
                    If (Headers(index) <> "") Then
                        Dim column As Integer = GetColumnHeader(xlWorkSheet, Headers(index))
                        xlWorkSheet.Cells(RowIndex, column).Value = RowData(index)
                    End If
                Next

               'Select the cell to save the file in that position
                Dim ScrollRow As Integer = RowIndex - 20
                If ScrollRow < 2 Then ScrollRow = 2
                xlWorkSheet.View.Panes(0).TopLeftCell = xlWorkSheet.Cells(ScrollRow, 2).Address
                xlWorkSheet.Select(xlWorkSheet.Cells(RowIndex, 1).Address, True)

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
                Dim xlWorkBook As ExcelWorkbook = xlPackage.Workbook
                'Check whether the sheet already exists
                If (HasWorkSheet(xlWorkBook, SheetName)) Then
                    'Read the data from the worksheet
                    Dim xlWorkSheet As ExcelWorksheet = xlWorkBook.Worksheets(SheetName)
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
