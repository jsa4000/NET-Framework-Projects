<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.btnProcess = New System.Windows.Forms.Button
        Me.txtProcessName = New System.Windows.Forms.TextBox
        Me.lblProcessName = New System.Windows.Forms.Label
        Me.tmrProcess = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'btnProcess
        '
        Me.btnProcess.Location = New System.Drawing.Point(87, 47)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(75, 23)
        Me.btnProcess.TabIndex = 0
        Me.btnProcess.Text = "Start"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'txtProcessName
        '
        Me.txtProcessName.Location = New System.Drawing.Point(110, 12)
        Me.txtProcessName.Name = "txtProcessName"
        Me.txtProcessName.Size = New System.Drawing.Size(129, 20)
        Me.txtProcessName.TabIndex = 1
        '
        'lblProcessName
        '
        Me.lblProcessName.AutoSize = True
        Me.lblProcessName.Location = New System.Drawing.Point(18, 18)
        Me.lblProcessName.Name = "lblProcessName"
        Me.lblProcessName.Size = New System.Drawing.Size(79, 13)
        Me.lblProcessName.TabIndex = 2
        Me.lblProcessName.Text = "Process Name:"
        '
        'tmrProcess
        '
        Me.tmrProcess.Interval = 1000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(269, 81)
        Me.Controls.Add(Me.lblProcessName)
        Me.Controls.Add(Me.txtProcessName)
        Me.Controls.Add(Me.btnProcess)
        Me.Name = "Form1"
        Me.Text = "Process Monitoring"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents txtProcessName As System.Windows.Forms.TextBox
    Friend WithEvents lblProcessName As System.Windows.Forms.Label
    Friend WithEvents tmrProcess As System.Windows.Forms.Timer

End Class
