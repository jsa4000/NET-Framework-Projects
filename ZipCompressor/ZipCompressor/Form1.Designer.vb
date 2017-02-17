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
        Me.btnZip = New System.Windows.Forms.Button
        Me.txtFile = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'btnZip
        '
        Me.btnZip.Location = New System.Drawing.Point(64, 38)
        Me.btnZip.Name = "btnZip"
        Me.btnZip.Size = New System.Drawing.Size(145, 28)
        Me.btnZip.TabIndex = 0
        Me.btnZip.Text = "ZIP"
        Me.btnZip.UseVisualStyleBackColor = True
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(12, 12)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(259, 20)
        Me.txtFile.TabIndex = 1
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 79)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.btnZip)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnZip As System.Windows.Forms.Button
    Friend WithEvents txtFile As System.Windows.Forms.TextBox

End Class
