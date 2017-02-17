
Public Class frmMain
    Private AdvanceMode As Boolean = False
    Private control As IBaseUI = Nothing

    Private Sub frmMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Check the mode the tool will be executed
        If Command() = "1" Then
            AdvanceMode = True
        End If
        'Check the launch mode 
        If (AdvanceMode) Then
            'Create Advance UI
            control = New AdvancedUI()
        Else
            'Create Advance UI
            control = New NormalUI()
        End If
        'Add the control to the main panel
        mainPanel.Controls.Add(CType(control, UserControl))
        'Load settings
        control.LoadSettings()
    End Sub

    Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Save settings
        control.SaveSettings()
    End Sub

End Class
