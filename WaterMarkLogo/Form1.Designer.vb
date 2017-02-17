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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.nImagesThreads = New System.Windows.Forms.NumericUpDown()
        Me.nCores = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtImagePath = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.chkNewMethod = New System.Windows.Forms.CheckBox()
        CType(Me.nImagesThreads, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nCores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(99, 69)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(142, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Start"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'nImagesThreads
        '
        Me.nImagesThreads.Location = New System.Drawing.Point(248, 71)
        Me.nImagesThreads.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nImagesThreads.Name = "nImagesThreads"
        Me.nImagesThreads.Size = New System.Drawing.Size(70, 20)
        Me.nImagesThreads.TabIndex = 1
        Me.nImagesThreads.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nCores
        '
        Me.nCores.Location = New System.Drawing.Point(97, 37)
        Me.nCores.Name = "nCores"
        Me.nCores.Size = New System.Drawing.Size(70, 20)
        Me.nCores.TabIndex = 2
        Me.nCores.Value = New Decimal(New Integer() {8, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Total Cores"
        '
        'txtImagePath
        '
        Me.txtImagePath.Location = New System.Drawing.Point(31, 11)
        Me.txtImagePath.Name = "txtImagePath"
        Me.txtImagePath.Size = New System.Drawing.Size(299, 20)
        Me.txtImagePath.TabIndex = 4
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 69)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(81, 23)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Newthread"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(280, 38)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Sample"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'chkNewMethod
        '
        Me.chkNewMethod.AutoSize = True
        Me.chkNewMethod.Location = New System.Drawing.Point(180, 42)
        Me.chkNewMethod.Name = "chkNewMethod"
        Me.chkNewMethod.Size = New System.Drawing.Size(87, 17)
        Me.chkNewMethod.TabIndex = 7
        Me.chkNewMethod.Text = "New Method"
        Me.chkNewMethod.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(367, 107)
        Me.Controls.Add(Me.chkNewMethod)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtImagePath)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.nCores)
        Me.Controls.Add(Me.nImagesThreads)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.nImagesThreads, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nCores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents nImagesThreads As NumericUpDown
    Friend WithEvents nCores As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents txtImagePath As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents chkNewMethod As CheckBox
End Class
