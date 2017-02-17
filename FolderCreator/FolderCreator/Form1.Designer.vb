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
        Me.btnCreate = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtToDOY = New System.Windows.Forms.TextBox
        Me.txtFromDOY = New System.Windows.Forms.TextBox
        Me.dtpToDay = New System.Windows.Forms.DateTimePicker
        Me.dtpFromDay = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(68, 107)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(75, 23)
        Me.btnCreate.TabIndex = 0
        Me.btnCreate.Text = "Create Folders"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtToDOY)
        Me.GroupBox1.Controls.Add(Me.txtFromDOY)
        Me.GroupBox1.Controls.Add(Me.dtpToDay)
        Me.GroupBox1.Controls.Add(Me.dtpFromDay)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(198, 89)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Dates"
        '
        'txtToDOY
        '
        Me.txtToDOY.BackColor = System.Drawing.SystemColors.Window
        Me.txtToDOY.Location = New System.Drawing.Point(152, 56)
        Me.txtToDOY.Name = "txtToDOY"
        Me.txtToDOY.ReadOnly = True
        Me.txtToDOY.Size = New System.Drawing.Size(33, 20)
        Me.txtToDOY.TabIndex = 5
        '
        'txtFromDOY
        '
        Me.txtFromDOY.BackColor = System.Drawing.SystemColors.Window
        Me.txtFromDOY.Location = New System.Drawing.Point(152, 24)
        Me.txtFromDOY.Name = "txtFromDOY"
        Me.txtFromDOY.ReadOnly = True
        Me.txtFromDOY.Size = New System.Drawing.Size(33, 20)
        Me.txtFromDOY.TabIndex = 4
        '
        'dtpToDay
        '
        Me.dtpToDay.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDay.Location = New System.Drawing.Point(56, 56)
        Me.dtpToDay.Name = "dtpToDay"
        Me.dtpToDay.Size = New System.Drawing.Size(89, 20)
        Me.dtpToDay.TabIndex = 3
        '
        'dtpFromDay
        '
        Me.dtpFromDay.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDay.Location = New System.Drawing.Point(56, 24)
        Me.dtpFromDay.Name = "dtpFromDay"
        Me.dtpFromDay.Size = New System.Drawing.Size(89, 20)
        Me.dtpFromDay.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "To:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From:"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(223, 137)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCreate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtToDOY As System.Windows.Forms.TextBox
    Friend WithEvents txtFromDOY As System.Windows.Forms.TextBox
    Friend WithEvents dtpToDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
