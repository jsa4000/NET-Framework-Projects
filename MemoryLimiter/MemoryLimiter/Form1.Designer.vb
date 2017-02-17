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
        Me.txtProcessID = New System.Windows.Forms.TextBox
        Me.lblProcessID = New System.Windows.Forms.Label
        Me.btnApply = New System.Windows.Forms.Button
        Me.lblMinimun = New System.Windows.Forms.Label
        Me.lblMaximun = New System.Windows.Forms.Label
        Me.nMinimun = New System.Windows.Forms.NumericUpDown
        Me.nMaximun = New System.Windows.Forms.NumericUpDown
        Me.lblInformation = New System.Windows.Forms.Label
        Me.gbOptions = New System.Windows.Forms.GroupBox
        CType(Me.nMinimun, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nMaximun, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtProcessID
        '
        Me.txtProcessID.Location = New System.Drawing.Point(102, 10)
        Me.txtProcessID.Name = "txtProcessID"
        Me.txtProcessID.Size = New System.Drawing.Size(107, 20)
        Me.txtProcessID.TabIndex = 0
        '
        'lblProcessID
        '
        Me.lblProcessID.AutoSize = True
        Me.lblProcessID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProcessID.Location = New System.Drawing.Point(32, 13)
        Me.lblProcessID.Name = "lblProcessID"
        Me.lblProcessID.Size = New System.Drawing.Size(69, 13)
        Me.lblProcessID.TabIndex = 1
        Me.lblProcessID.Text = "Process ID"
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(48, 124)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(130, 26)
        Me.btnApply.TabIndex = 2
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'lblMinimun
        '
        Me.lblMinimun.AutoSize = True
        Me.lblMinimun.Location = New System.Drawing.Point(28, 18)
        Me.lblMinimun.Name = "lblMinimun"
        Me.lblMinimun.Size = New System.Drawing.Size(80, 13)
        Me.lblMinimun.TabIndex = 4
        Me.lblMinimun.Text = "Minimun (bytes)"
        '
        'lblMaximun
        '
        Me.lblMaximun.AutoSize = True
        Me.lblMaximun.Location = New System.Drawing.Point(28, 49)
        Me.lblMaximun.Name = "lblMaximun"
        Me.lblMaximun.Size = New System.Drawing.Size(83, 13)
        Me.lblMaximun.TabIndex = 6
        Me.lblMaximun.Text = "Maximun (bytes)"
        '
        'nMinimun
        '
        Me.nMinimun.Location = New System.Drawing.Point(118, 16)
        Me.nMinimun.Maximum = New Decimal(New Integer() {1073741824, 0, 0, 0})
        Me.nMinimun.Name = "nMinimun"
        Me.nMinimun.Size = New System.Drawing.Size(100, 20)
        Me.nMinimun.TabIndex = 7
        Me.nMinimun.Value = New Decimal(New Integer() {256000, 0, 0, 0})
        '
        'nMaximun
        '
        Me.nMaximun.Location = New System.Drawing.Point(117, 47)
        Me.nMaximun.Maximum = New Decimal(New Integer() {1410065407, 2, 0, 0})
        Me.nMaximun.Name = "nMaximun"
        Me.nMaximun.Size = New System.Drawing.Size(101, 20)
        Me.nMaximun.TabIndex = 8
        Me.nMaximun.Value = New Decimal(New Integer() {350000, 0, 0, 0})
        '
        'lblInformation
        '
        Me.lblInformation.AutoSize = True
        Me.lblInformation.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInformation.Location = New System.Drawing.Point(120, 155)
        Me.lblInformation.Name = "lblInformation"
        Me.lblInformation.Size = New System.Drawing.Size(116, 12)
        Me.lblInformation.TabIndex = 9
        Me.lblInformation.Text = "(*) 1gb = 1073741824 bytes"
        '
        'gbOptions
        '
        Me.gbOptions.Controls.Add(Me.nMaximun)
        Me.gbOptions.Controls.Add(Me.nMinimun)
        Me.gbOptions.Controls.Add(Me.lblMaximun)
        Me.gbOptions.Controls.Add(Me.lblMinimun)
        Me.gbOptions.Location = New System.Drawing.Point(4, 42)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(235, 79)
        Me.gbOptions.TabIndex = 10
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Memory"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(247, 173)
        Me.Controls.Add(Me.gbOptions)
        Me.Controls.Add(Me.lblInformation)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.lblProcessID)
        Me.Controls.Add(Me.txtProcessID)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Form1"
        Me.Text = "Memory Limiter"
        CType(Me.nMinimun, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nMaximun, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtProcessID As System.Windows.Forms.TextBox
    Friend WithEvents lblProcessID As System.Windows.Forms.Label
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents lblMinimun As System.Windows.Forms.Label
    Friend WithEvents lblMaximun As System.Windows.Forms.Label
    Friend WithEvents nMinimun As System.Windows.Forms.NumericUpDown
    Friend WithEvents nMaximun As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblInformation As System.Windows.Forms.Label
    Friend WithEvents gbOptions As System.Windows.Forms.GroupBox

End Class
