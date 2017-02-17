Public Class MemoryManagement

    Structure LUID
        Public LowPart As UInt32
        Public HighPart As Integer
    End Structure

    Private Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Int32, ByVal dwMaximumWorkingSetSize As Int32) As Int32
    Declare Function LookupPrivilegeValue Lib "advapi32.dll" (ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Boolean

    Private SE_INC_BASE_PRIORITY_NAME As String = "SeIncreaseBasePriorityPrivilege"
    Private SE_INCREASE_QUOTA_NAME As String = "SeIncreaseQuotaPrivilege"
    Private SE_INC_WORKING_SET_NAME As String = "SeIncreaseWorkingSetPrivilege"

    Public Shared Function ProcessExists(ByVal pID As Integer) As Boolean
        Dim result As Boolean = False
        Try
            If (System.Diagnostics.Process.GetProcessById(pID) IsNot Nothing) Then
                result = True
            End If
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
        Return result
    End Function

    Public Shared Sub LimitSizeProcess(ByVal pID As Integer, ByVal Max As Integer, Optional ByVal Min As Integer = -1)
        Try
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            If Environment.OSVersion.Platform = PlatformID.Win32NT Then
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetProcessById(pID).Handle, Min, Max)
                'Dim Proc As Process = System.Diagnostics.Process.GetProcessById(pID)
                'If (Proc IsNot Nothing) Then
                '    Proc.MaxWorkingSet = Max
                '    'Proc.MinWorkingSet = Min
                'End If
            End If
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
    End Sub

End Class
