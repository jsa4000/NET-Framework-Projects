<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.txtProc1 = New System.Windows.Forms.TextBox()
        Me.txtProc2 = New System.Windows.Forms.TextBox()
        Me.btnClean = New System.Windows.Forms.Button()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.lstSteps = New System.Windows.Forms.ListBox()
        Me.txtProc3 = New System.Windows.Forms.TextBox()
        Me.txtProc4 = New System.Windows.Forms.TextBox()
        Me.btnAddTask = New System.Windows.Forms.Button()
        Me.nCores = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.txtDateTime = New System.Windows.Forms.TextBox()
        Me.btnTEST = New System.Windows.Forms.Button()
        CType(Me.nCores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(27, 264)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(113, 29)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "START"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'txtProc1
        '
        Me.txtProc1.Location = New System.Drawing.Point(27, 203)
        Me.txtProc1.Name = "txtProc1"
        Me.txtProc1.ReadOnly = True
        Me.txtProc1.Size = New System.Drawing.Size(113, 20)
        Me.txtProc1.TabIndex = 1
        '
        'txtProc2
        '
        Me.txtProc2.Location = New System.Drawing.Point(146, 203)
        Me.txtProc2.Name = "txtProc2"
        Me.txtProc2.ReadOnly = True
        Me.txtProc2.Size = New System.Drawing.Size(113, 20)
        Me.txtProc2.TabIndex = 2
        '
        'btnClean
        '
        Me.btnClean.Location = New System.Drawing.Point(27, 339)
        Me.btnClean.Name = "btnClean"
        Me.btnClean.Size = New System.Drawing.Size(116, 29)
        Me.btnClean.TabIndex = 3
        Me.btnClean.Text = "CLEAN"
        Me.btnClean.UseVisualStyleBackColor = True
        '
        'txtStatus
        '
        Me.txtStatus.Location = New System.Drawing.Point(27, 163)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(232, 20)
        Me.txtStatus.TabIndex = 4
        '
        'lstSteps
        '
        Me.lstSteps.FormattingEnabled = True
        Me.lstSteps.Location = New System.Drawing.Point(27, 13)
        Me.lstSteps.Name = "lstSteps"
        Me.lstSteps.Size = New System.Drawing.Size(232, 134)
        Me.lstSteps.TabIndex = 5
        '
        'txtProc3
        '
        Me.txtProc3.Location = New System.Drawing.Point(27, 229)
        Me.txtProc3.Name = "txtProc3"
        Me.txtProc3.ReadOnly = True
        Me.txtProc3.Size = New System.Drawing.Size(113, 20)
        Me.txtProc3.TabIndex = 6
        '
        'txtProc4
        '
        Me.txtProc4.Location = New System.Drawing.Point(146, 229)
        Me.txtProc4.Name = "txtProc4"
        Me.txtProc4.ReadOnly = True
        Me.txtProc4.Size = New System.Drawing.Size(113, 20)
        Me.txtProc4.TabIndex = 7
        '
        'btnAddTask
        '
        Me.btnAddTask.Location = New System.Drawing.Point(27, 301)
        Me.btnAddTask.Name = "btnAddTask"
        Me.btnAddTask.Size = New System.Drawing.Size(113, 29)
        Me.btnAddTask.TabIndex = 8
        Me.btnAddTask.Text = "ADD"
        Me.btnAddTask.UseVisualStyleBackColor = True
        '
        'nCores
        '
        Me.nCores.Location = New System.Drawing.Point(219, 307)
        Me.nCores.Maximum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.nCores.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nCores.Name = "nCores"
        Me.nCores.Size = New System.Drawing.Size(40, 20)
        Me.nCores.TabIndex = 9
        Me.nCores.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(146, 310)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Core Number"
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(146, 264)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(113, 29)
        Me.btnStop.TabIndex = 11
        Me.btnStop.Text = "STOP"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Interval = 10
        '
        'txtDateTime
        '
        Me.txtDateTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDateTime.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.txtDateTime.Enabled = False
        Me.txtDateTime.Location = New System.Drawing.Point(149, 344)
        Me.txtDateTime.Name = "txtDateTime"
        Me.txtDateTime.ReadOnly = True
        Me.txtDateTime.Size = New System.Drawing.Size(110, 13)
        Me.txtDateTime.TabIndex = 12
        Me.txtDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnTEST
        '
        Me.btnTEST.Location = New System.Drawing.Point(177, 344)
        Me.btnTEST.Name = "btnTEST"
        Me.btnTEST.Size = New System.Drawing.Size(75, 23)
        Me.btnTEST.TabIndex = 13
        Me.btnTEST.Text = "TEST"
        Me.btnTEST.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 380)
        Me.Controls.Add(Me.btnTEST)
        Me.Controls.Add(Me.txtDateTime)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.nCores)
        Me.Controls.Add(Me.btnAddTask)
        Me.Controls.Add(Me.txtProc4)
        Me.Controls.Add(Me.txtProc3)
        Me.Controls.Add(Me.lstSteps)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.btnClean)
        Me.Controls.Add(Me.txtProc2)
        Me.Controls.Add(Me.txtProc1)
        Me.Controls.Add(Me.btnStart)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.nCores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents txtProc1 As System.Windows.Forms.TextBox
    Friend WithEvents txtProc2 As System.Windows.Forms.TextBox
    Friend WithEvents btnClean As System.Windows.Forms.Button
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents lstSteps As System.Windows.Forms.ListBox
    Friend WithEvents txtProc3 As System.Windows.Forms.TextBox
    Friend WithEvents txtProc4 As System.Windows.Forms.TextBox
    Friend WithEvents btnAddTask As System.Windows.Forms.Button
    Friend WithEvents nCores As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents tmrUpdate As System.Windows.Forms.Timer
    Friend WithEvents txtDateTime As System.Windows.Forms.TextBox
    Friend WithEvents btnTEST As Button
End Class
