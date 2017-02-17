<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NormalUI
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnBrowse10 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtConvertFolder = New System.Windows.Forms.TextBox()
        Me.txtOutputFolder = New System.Windows.Forms.TextBox()
        Me.cmdBrowse12 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdBrowse11 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtInputFolder = New System.Windows.Forms.TextBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.nTop = New System.Windows.Forms.NumericUpDown()
        Me.nRight = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.nLeft = New System.Windows.Forms.NumericUpDown()
        Me.nBottom = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.nAlpha = New System.Windows.Forms.NumericUpDown()
        Me.chkConvert = New System.Windows.Forms.CheckBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.txtPRN = New System.Windows.Forms.TextBox()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.nTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nAlpha, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(33, 126)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 69
        Me.Label8.Text = "PRN:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnBrowse10)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtConvertFolder)
        Me.GroupBox2.Controls.Add(Me.txtOutputFolder)
        Me.GroupBox2.Controls.Add(Me.cmdBrowse12)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cmdBrowse11)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtInputFolder)
        Me.GroupBox2.Location = New System.Drawing.Point(247, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(390, 109)
        Me.GroupBox2.TabIndex = 68
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Folders"
        '
        'btnBrowse10
        '
        Me.btnBrowse10.Enabled = False
        Me.btnBrowse10.Location = New System.Drawing.Point(345, 16)
        Me.btnBrowse10.Name = "btnBrowse10"
        Me.btnBrowse10.Size = New System.Drawing.Size(26, 20)
        Me.btnBrowse10.TabIndex = 15
        Me.btnBrowse10.Text = "..."
        Me.btnBrowse10.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Convert Folder:"
        '
        'txtConvertFolder
        '
        Me.txtConvertFolder.Enabled = False
        Me.txtConvertFolder.Location = New System.Drawing.Point(94, 16)
        Me.txtConvertFolder.Name = "txtConvertFolder"
        Me.txtConvertFolder.Size = New System.Drawing.Size(245, 20)
        Me.txtConvertFolder.TabIndex = 13
        '
        'txtOutputFolder
        '
        Me.txtOutputFolder.Location = New System.Drawing.Point(94, 82)
        Me.txtOutputFolder.Name = "txtOutputFolder"
        Me.txtOutputFolder.Size = New System.Drawing.Size(245, 20)
        Me.txtOutputFolder.TabIndex = 12
        '
        'cmdBrowse12
        '
        Me.cmdBrowse12.Location = New System.Drawing.Point(345, 82)
        Me.cmdBrowse12.Name = "cmdBrowse12"
        Me.cmdBrowse12.Size = New System.Drawing.Size(26, 20)
        Me.cmdBrowse12.TabIndex = 10
        Me.cmdBrowse12.Text = "..."
        Me.cmdBrowse12.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "OutputFolder:"
        '
        'cmdBrowse11
        '
        Me.cmdBrowse11.Location = New System.Drawing.Point(345, 49)
        Me.cmdBrowse11.Name = "cmdBrowse11"
        Me.cmdBrowse11.Size = New System.Drawing.Size(26, 20)
        Me.cmdBrowse11.TabIndex = 4
        Me.cmdBrowse11.Text = "..."
        Me.cmdBrowse11.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "InputFolder:"
        '
        'txtInputFolder
        '
        Me.txtInputFolder.Location = New System.Drawing.Point(94, 49)
        Me.txtInputFolder.Name = "txtInputFolder"
        Me.txtInputFolder.Size = New System.Drawing.Size(245, 20)
        Me.txtInputFolder.TabIndex = 0
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(469, 118)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(110, 27)
        Me.btnStart.TabIndex = 67
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label11)
        Me.GroupBox6.Controls.Add(Me.nTop)
        Me.GroupBox6.Controls.Add(Me.nRight)
        Me.GroupBox6.Controls.Add(Me.Label7)
        Me.GroupBox6.Controls.Add(Me.Label10)
        Me.GroupBox6.Controls.Add(Me.nLeft)
        Me.GroupBox6.Controls.Add(Me.nBottom)
        Me.GroupBox6.Controls.Add(Me.Label9)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 7)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(235, 108)
        Me.GroupBox6.TabIndex = 76
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Dimensions"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(121, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 13)
        Me.Label11.TabIndex = 73
        Me.Label11.Text = "Right:"
        '
        'nTop
        '
        Me.nTop.Location = New System.Drawing.Point(105, 20)
        Me.nTop.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.nTop.Minimum = New Decimal(New Integer() {180, 0, 0, -2147483648})
        Me.nTop.Name = "nTop"
        Me.nTop.Size = New System.Drawing.Size(55, 20)
        Me.nTop.TabIndex = 68
        Me.nTop.Value = New Decimal(New Integer() {70, 0, 0, 0})
        '
        'nRight
        '
        Me.nRight.Location = New System.Drawing.Point(162, 45)
        Me.nRight.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.nRight.Minimum = New Decimal(New Integer() {180, 0, 0, -2147483648})
        Me.nRight.Name = "nRight"
        Me.nRight.Size = New System.Drawing.Size(55, 20)
        Me.nRight.TabIndex = 74
        Me.nRight.Value = New Decimal(New Integer() {45, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(68, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 67
        Me.Label7.Text = "Top:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(54, 74)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(43, 13)
        Me.Label10.TabIndex = 71
        Me.Label10.Text = "Bottom:"
        '
        'nLeft
        '
        Me.nLeft.Location = New System.Drawing.Point(49, 45)
        Me.nLeft.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.nLeft.Minimum = New Decimal(New Integer() {180, 0, 0, -2147483648})
        Me.nLeft.Name = "nLeft"
        Me.nLeft.Size = New System.Drawing.Size(55, 20)
        Me.nLeft.TabIndex = 70
        Me.nLeft.Value = New Decimal(New Integer() {35, 0, 0, -2147483648})
        '
        'nBottom
        '
        Me.nBottom.Location = New System.Drawing.Point(106, 72)
        Me.nBottom.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
        Me.nBottom.Minimum = New Decimal(New Integer() {180, 0, 0, -2147483648})
        Me.nBottom.Name = "nBottom"
        Me.nBottom.Size = New System.Drawing.Size(55, 20)
        Me.nBottom.TabIndex = 72
        Me.nBottom.Value = New Decimal(New Integer() {25, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(28, 13)
        Me.Label9.TabIndex = 69
        Me.Label9.Text = "Left:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(141, 125)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(37, 13)
        Me.Label12.TabIndex = 81
        Me.Label12.Text = "Alpha:"
        '
        'nAlpha
        '
        Me.nAlpha.Location = New System.Drawing.Point(188, 122)
        Me.nAlpha.Name = "nAlpha"
        Me.nAlpha.Size = New System.Drawing.Size(45, 20)
        Me.nAlpha.TabIndex = 82
        Me.nAlpha.Value = New Decimal(New Integer() {85, 0, 0, 0})
        '
        'chkConvert
        '
        Me.chkConvert.AutoSize = True
        Me.chkConvert.Location = New System.Drawing.Point(261, 126)
        Me.chkConvert.Name = "chkConvert"
        Me.chkConvert.Size = New System.Drawing.Size(100, 17)
        Me.chkConvert.TabIndex = 83
        Me.chkConvert.Text = "Convert Images"
        Me.chkConvert.UseVisualStyleBackColor = True
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Menu
        Me.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(122, 164)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(406, 13)
        Me.txtStatus.TabIndex = 84
        Me.txtStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPRN
        '
        Me.txtPRN.Location = New System.Drawing.Point(70, 122)
        Me.txtPRN.Name = "txtPRN"
        Me.txtPRN.Size = New System.Drawing.Size(50, 20)
        Me.txtPRN.TabIndex = 16
        Me.txtPRN.Text = "120"
        '
        'NormalUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtPRN)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.chkConvert)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.nAlpha)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnStart)
        Me.Name = "NormalUI"
        Me.Size = New System.Drawing.Size(646, 193)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.nTop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nBottom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nAlpha, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label8 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtOutputFolder As TextBox
    Friend WithEvents cmdBrowse12 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents cmdBrowse11 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtInputFolder As TextBox
    Friend WithEvents btnStart As Button
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Label11 As Label
    Friend WithEvents nTop As NumericUpDown
    Friend WithEvents nRight As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents nLeft As NumericUpDown
    Friend WithEvents nBottom As NumericUpDown
    Friend WithEvents Label9 As Label
    Friend WithEvents btnBrowse10 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtConvertFolder As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents nAlpha As NumericUpDown
    Friend WithEvents chkConvert As CheckBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents txtStatus As TextBox
    Friend WithEvents txtPRN As TextBox
End Class
