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
        Me.btnStart = New System.Windows.Forms.Button
        Me.btnModify = New System.Windows.Forms.Button
        Me.chkReadOnly = New System.Windows.Forms.CheckBox
        Me.btnStartOS = New System.Windows.Forms.Button
        Me.btnModifyOS = New System.Windows.Forms.Button
        Me.btnTest = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(12, 21)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(125, 37)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Create"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnModify
        '
        Me.btnModify.Location = New System.Drawing.Point(12, 64)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(125, 35)
        Me.btnModify.TabIndex = 1
        Me.btnModify.Text = "Modify"
        Me.btnModify.UseVisualStyleBackColor = True
        '
        'chkReadOnly
        '
        Me.chkReadOnly.AutoSize = True
        Me.chkReadOnly.Location = New System.Drawing.Point(61, 105)
        Me.chkReadOnly.Name = "chkReadOnly"
        Me.chkReadOnly.Size = New System.Drawing.Size(76, 17)
        Me.chkReadOnly.TabIndex = 2
        Me.chkReadOnly.Text = "Read Only"
        Me.chkReadOnly.UseVisualStyleBackColor = True
        '
        'btnStartOS
        '
        Me.btnStartOS.Location = New System.Drawing.Point(165, 21)
        Me.btnStartOS.Name = "btnStartOS"
        Me.btnStartOS.Size = New System.Drawing.Size(125, 37)
        Me.btnStartOS.TabIndex = 3
        Me.btnStartOS.Text = "Create OS"
        Me.btnStartOS.UseVisualStyleBackColor = True
        '
        'btnModifyOS
        '
        Me.btnModifyOS.Location = New System.Drawing.Point(165, 64)
        Me.btnModifyOS.Name = "btnModifyOS"
        Me.btnModifyOS.Size = New System.Drawing.Size(125, 35)
        Me.btnModifyOS.TabIndex = 4
        Me.btnModifyOS.Text = "ModifyOS"
        Me.btnModifyOS.UseVisualStyleBackColor = True
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(165, 113)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(71, 27)
        Me.btnTest.TabIndex = 5
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(333, 149)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.btnModifyOS)
        Me.Controls.Add(Me.btnStartOS)
        Me.Controls.Add(Me.chkReadOnly)
        Me.Controls.Add(Me.btnModify)
        Me.Controls.Add(Me.btnStart)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Form1"
        Me.Text = "Excel TEST"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents chkReadOnly As System.Windows.Forms.CheckBox
    Friend WithEvents btnStartOS As System.Windows.Forms.Button
    Friend WithEvents btnModifyOS As System.Windows.Forms.Button
    Friend WithEvents btnTest As System.Windows.Forms.Button

End Class
