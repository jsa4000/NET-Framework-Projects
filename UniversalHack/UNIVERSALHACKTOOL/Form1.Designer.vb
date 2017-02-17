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
        Me.panelApp = New System.Windows.Forms.Panel()
        Me.btnHide = New System.Windows.Forms.Button()
        Me.btnGet = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnAll = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAppFolder = New System.Windows.Forms.TextBox()
        Me.txtFilesFolder = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'panelApp
        '
        Me.panelApp.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelApp.Location = New System.Drawing.Point(0, 0)
        Me.panelApp.Name = "panelApp"
        Me.panelApp.Size = New System.Drawing.Size(513, 448)
        Me.panelApp.TabIndex = 11
        '
        'btnHide
        '
        Me.btnHide.Location = New System.Drawing.Point(174, 514)
        Me.btnHide.Name = "btnHide"
        Me.btnHide.Size = New System.Drawing.Size(75, 23)
        Me.btnHide.TabIndex = 10
        Me.btnHide.Text = "HIDE"
        Me.btnHide.UseVisualStyleBackColor = True
        '
        'btnGet
        '
        Me.btnGet.Location = New System.Drawing.Point(93, 514)
        Me.btnGet.Name = "btnGet"
        Me.btnGet.Size = New System.Drawing.Size(75, 23)
        Me.btnGet.TabIndex = 9
        Me.btnGet.Text = "GET"
        Me.btnGet.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(255, 514)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 8
        Me.btnStart.Text = "START"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(336, 514)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "CLOSE"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(12, 514)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(75, 23)
        Me.btnOpen.TabIndex = 6
        Me.btnOpen.Text = "OPEN"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnAll
        '
        Me.btnAll.Location = New System.Drawing.Point(426, 514)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(75, 23)
        Me.btnAll.TabIndex = 12
        Me.btnAll.Text = "ALL"
        Me.btnAll.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 460)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "App Folder:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 484)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Files Folder:"
        '
        'txtAppFolder
        '
        Me.txtAppFolder.Location = New System.Drawing.Point(93, 455)
        Me.txtAppFolder.Name = "txtAppFolder"
        Me.txtAppFolder.Size = New System.Drawing.Size(339, 20)
        Me.txtAppFolder.TabIndex = 15
        Me.txtAppFolder.Text = "C:\Users\javier.santos\Desktop\New folder"
        '
        'txtFilesFolder
        '
        Me.txtFilesFolder.Location = New System.Drawing.Point(93, 481)
        Me.txtFilesFolder.Name = "txtFilesFolder"
        Me.txtFilesFolder.Size = New System.Drawing.Size(339, 20)
        Me.txtFilesFolder.TabIndex = 16
        Me.txtFilesFolder.Text = "C:\Users\javier.santos\Desktop\New folder\test"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(513, 544)
        Me.Controls.Add(Me.txtFilesFolder)
        Me.Controls.Add(Me.txtAppFolder)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAll)
        Me.Controls.Add(Me.panelApp)
        Me.Controls.Add(Me.btnHide)
        Me.Controls.Add(Me.btnGet)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnOpen)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents panelApp As System.Windows.Forms.Panel
    Private WithEvents btnHide As System.Windows.Forms.Button
    Private WithEvents btnGet As System.Windows.Forms.Button
    Private WithEvents btnStart As System.Windows.Forms.Button
    Private WithEvents btnClose As System.Windows.Forms.Button
    Private WithEvents btnOpen As System.Windows.Forms.Button
    Private WithEvents btnAll As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAppFolder As System.Windows.Forms.TextBox
    Friend WithEvents txtFilesFolder As System.Windows.Forms.TextBox

End Class
