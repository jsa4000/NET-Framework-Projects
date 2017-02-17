Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Public Class UNIVERSALHack
    Private Const DEFAULT_TIMEOUT As Integer = 60000

    Public Shared SW_HIDE As Integer = 0
    Public Shared SW_SHOW As Integer = 5
    Public Shared WS_SYSMENU As Integer = &H80000
    Public Shared GWL_STYLE As Integer = -16
    Public Shared WS_CHILD As Integer = &H40000000     'child window
    Public Shared WS_BORDER As Integer = &H800000     'window with border
    Public Shared WS_DLGFRAME As Integer = &H400000     'window with double border but no title
    Public Shared WS_CAPTION As Integer = WS_BORDER Or WS_DLGFRAME     'window with a title bar

    Public Const WM_COMMAND As Integer = &H111
    Public Const WM_DROPFILES As Integer = &H233
    Public Const BN_CLICKED As Integer = 0
    Private Const ButtonId As Integer = &H3F0
    Private Const ListId As Integer = &H3EB

    Private Const nChars As Integer = 256

    <DllImport("user32.dll")> _
    Public Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As Integer) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function GetWindowRect(hWnd As HandleRef, ByRef lpRect As RECT) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function MoveWindow(Handle As IntPtr, x As Integer, y As Integer, w As Integer, h As Integer, repaint As Boolean) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function SetParent(hWndChild As IntPtr, hWndNewParent As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function GetDlgItem(hWnd As IntPtr, nIDDlgItem As Integer) As IntPtr
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As Integer, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("Kernel32.dll", SetLastError:=True)> _
    Public Shared Function GlobalLock(Handle As IntPtr) As Integer
    End Function

    <DllImport("Kernel32.dll", SetLastError:=True)> _
    Public Shared Function GlobalUnlock(Handle As IntPtr) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function PostMessage(hWnd As IntPtr, Msg As UInteger, wParam As IntPtr, lParam As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function GetWindowText(hWnd As IntPtr, text As StringBuilder, count As Integer) As Integer
    End Function

    <Serializable> _
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure RECT
        Public Left As Integer         ' x position of upper-left corner
        Public Top As Integer         ' y position of upper-left corner
        Public Right As Integer         ' x position of lower-right corner
        Public Bottom As Integer         ' y position of lower-right corner
    End Structure

    <Serializable> _
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure POINT
        Public X As Int32
        Public Y As Int32

        Public Sub New(x As Int32, y As Int32)
            Me.X = x
            Me.Y = y
        End Sub
    End Structure
    <Serializable> _
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Public Class DROPFILES
        Public size As Integer         '<-- offset to filelist (this should be defined 20)
        Public pt As POINT         '<-- where we "release the mouse button"
        Public fND As Boolean         '<-- the point origins (0;0) (this should be false, if true, the origin will be the screens (0;0), else, the handle the the window we send in PostMessage)
        Public WIDE As Boolean         '<-- ANSI or Unicode (should be false)
    End Class

    Public AppFolder As String = [String].Empty
    Public FilesPath As String = [String].Empty

    Public IsOpen As Boolean = False

    Public Handle As IntPtr = IntPtr.Zero
    Private hWndButton As IntPtr = IntPtr.Zero
    Private hWndList As IntPtr = IntPtr.Zero
    Private MainProc As Process = Nothing
    Private DefaultButtonText As [String] = [String].Empty

    Public Sub New(AppFolder As String, FilesPath As String)
        Me.AppFolder = AppFolder
        Me.FilesPath = FilesPath
    End Sub

    Private Function RawSerialize(anything As Object) As Byte()
        Dim rawsize As Integer = Marshal.SizeOf(anything)
        Dim buffer As IntPtr = Marshal.AllocHGlobal(rawsize)
        Marshal.StructureToPtr(anything, buffer, False)
        Dim rawdatas As Byte() = New Byte(rawsize - 1) {}
        Marshal.Copy(buffer, rawdatas, 0, rawsize)
        Marshal.FreeHGlobal(buffer)
        Return rawdatas
    End Function

    Function GetModule(p As Process) As ProcessModule
        Dim pm As ProcessModule = Nothing
        Try
            pm = p.MainModule
        Catch
            Return Nothing
        End Try
        Return pm
    End Function

    Public Sub Hide(ByVal hide As Boolean)
        If (IsOpen) Then
            If (hide) Then
                UNIVERSALHack.ShowWindow(Handle, UNIVERSALHack.SW_HIDE)
            Else
                UNIVERSALHack.ShowWindow(Handle, UNIVERSALHack.SW_SHOW)
            End If
        End If
    End Sub

    Public Function Open() As Boolean
        'Check if it's already opened
        If IsOpen Then
            Return False
        End If

        'Check if the executable exists
        Dim AppPath As String = AppFolder & "\tool.exe"
        If Not System.IO.File.Exists(AppPath) Then
            Return False
        End If

        Try
            'Start the Application
            Dim startInfo As New ProcessStartInfo()
            startInfo.FileName = AppPath
            Process.Start(startInfo)

            'Initialize variables
            Handle = IntPtr.Zero
            hWndButton = IntPtr.Zero
            DefaultButtonText = [String].Empty
            MainProc = Nothing

            'Find until fonuded or timeOut
            Dim start As DateTime = DateTime.Now
            While Handle = IntPtr.Zero AndAlso DateTime.Now.Subtract(start).Milliseconds < DEFAULT_TIMEOUT
                Dim localAll As Process() = Process.GetProcesses()
                For Each proc As Process In localAll
                    If proc.MainWindowHandle <> IntPtr.Zero Then
                        Dim pm As ProcessModule = GetModule(proc)
                        If pm IsNot Nothing AndAlso proc.MainModule.FileName.ToUpper() = AppPath.ToUpper() Then
                            Handle = proc.MainWindowHandle
                            MainProc = proc
                        End If
                    End If
                Next
            End While

            If Handle = IntPtr.Zero Then
                Return False
            End If

            'Get the handle of the button
            hWndButton = GetDlgItem(Handle, ButtonId)
            'Get the handle of the list
            hWndList = GetDlgItem(Handle, ListId)

            'Get default text of the button
            Dim Buff As New StringBuilder(nChars)
            If GetWindowText(hWndButton, Buff, nChars) > 0 Then
                DefaultButtonText = Buff.ToString()
            End If

            'Is opend

            IsOpen = True
        Catch ex As Exception
            'Exception
            Return False
        End Try

        Return True
    End Function

    Public Sub ChangeParent(ByVal pControl As Control)
        If IsOpen Then
            UNIVERSALHack.SetWindowLong(Handle, UNIVERSALHack.GWL_STYLE, UNIVERSALHack.GetWindowLong(Handle, UNIVERSALHack.GWL_STYLE) And (Not UNIVERSALHack.WS_CAPTION))
            UNIVERSALHack.SetParent(Handle, pControl.Handle)
            UNIVERSALHack.MoveWindow(Handle, 0, 0, pControl.Width, pControl.Height, True)
        End If
    End Sub

    Public Function Close() As Boolean
        If Not IsOpen Then
            Return False
        End If

        Try
            Dim CurrentButtonText As String = [String].Empty
            Dim Buff As New StringBuilder(nChars)
            While CurrentButtonText <> DefaultButtonText
                If GetWindowText(hWndButton, Buff, nChars) > 0 Then
                    CurrentButtonText = Buff.ToString()
                    Buff.Length = 0
                End If
            End While

            'Kill process
            MainProc.Kill()

            'Initialize variables
            Handle = IntPtr.Zero
            hWndButton = IntPtr.Zero
            hWndList = IntPtr.Zero
            DefaultButtonText = [String].Empty
            MainProc = Nothing

            'Set IsOpen to false
            IsOpen = False
        Catch ex As Exception
            'Exception
            Return False
        End Try

        Return True
    End Function

    Public Function StartProcess() As Boolean
        If Not IsOpen OrElse Not System.IO.Directory.Exists(FilesPath) Then
            Return False
        End If

        'IntPtr hwnd = this.Handle;
        Dim s As New DROPFILES()
        s.size = 20        '<-- 20 is the size of this struct in memory
        s.pt = New POINT(10, 10)         '<-- drop file 20 pixels from left, total height minus 40 from top
        s.fND = False         '<-- the point 0;0 will be in the window
        s.WIDE = False         '<-- ANSI
        'Get all files from the folder

        Dim files As String() = System.IO.Directory.GetFiles(FilesPath)
        'string[] files = { "C:\\Universal.0000\0", "C:\\Universal.0001\0" };         //<-- add null terminator at end

        Dim lengthFiles As Int32 = 0
        For Each file As [String] In files
            '<-- add null terminator at end
            lengthFiles += file.Length + 2
        Next

        Dim filelen As Int32 = Convert.ToInt32(lengthFiles)
        Dim bytes As Byte() = RawSerialize(s)
        Dim structlen As Integer = CInt(bytes.Length)
        Dim size As Integer = structlen + filelen + 1
        Dim p As IntPtr = Marshal.AllocHGlobal(size)         '<-- allocate memory and save pointer to p
        GlobalLock(p)         '<-- lock p
        Dim i As Integer = 0
        For i = 0 To structlen - 1
            Marshal.WriteByte(p, i, bytes(i))
        Next

        For Each file As [String] In files
            Dim b As Byte() = ASCIIEncoding.ASCII.GetBytes(file & vbNullChar)
            '<-- convert filepath to bytearray //<-- add null terminator at end
            For k As Integer = 0 To b.Length - 1
                Marshal.WriteByte(p, i, b(k))
                i += 1
            Next
        Next

        'Write the end of the parameters to Send to the Window
        Marshal.WriteByte(p, i, 0)

        GlobalUnlock(p)
        PostMessage(Handle, WM_DROPFILES, p, IntPtr.Zero)

        'Process the files dragged
        Dim wParam As Integer = (BN_CLICKED << 16) Or (ButtonId And &HFFFF)
        SendMessage(Handle, WM_COMMAND, wParam, hWndButton)

        'Release all objects'
        'Marshal.FreeHGlobal(p);

        Return True
    End Function

End Class
