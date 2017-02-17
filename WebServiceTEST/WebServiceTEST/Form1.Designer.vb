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
        Me.btnConnect = New System.Windows.Forms.Button
        Me.lblURL = New System.Windows.Forms.Label
        Me.txtURL = New System.Windows.Forms.TextBox
        Me.lblText1 = New System.Windows.Forms.Label
        Me.lblText5 = New System.Windows.Forms.Label
        Me.lblText4 = New System.Windows.Forms.Label
        Me.lblText3 = New System.Windows.Forms.Label
        Me.lblText2 = New System.Windows.Forms.Label
        Me.lblText6 = New System.Windows.Forms.Label
        Me.txtField2 = New System.Windows.Forms.TextBox
        Me.txtField1 = New System.Windows.Forms.TextBox
        Me.txtField6 = New System.Windows.Forms.TextBox
        Me.txtField5 = New System.Windows.Forms.TextBox
        Me.txtField4 = New System.Windows.Forms.TextBox
        Me.txtField3 = New System.Windows.Forms.TextBox
        Me.tlProperties = New System.Windows.Forms.TableLayoutPanel
        Me.txtContent = New System.Windows.Forms.TextBox
        Me.gbParameters = New System.Windows.Forms.GroupBox
        Me.txtResult = New System.Windows.Forms.TextBox
        Me.txtResponse = New System.Windows.Forms.TextBox
        Me.tlProperties.SuspendLayout()
        Me.gbParameters.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(285, 117)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(161, 26)
        Me.btnConnect.TabIndex = 0
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'lblURL
        '
        Me.lblURL.AutoSize = True
        Me.lblURL.Location = New System.Drawing.Point(12, 16)
        Me.lblURL.Name = "lblURL"
        Me.lblURL.Size = New System.Drawing.Size(69, 13)
        Me.lblURL.TabIndex = 1
        Me.lblURL.Text = "Web Service"
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(87, 13)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(557, 20)
        Me.txtURL.TabIndex = 2
        Me.txtURL.Text = "http://ec2-54-247-60-251.eu-west-1.compute.amazonaws.com/new_egnos_ops/?q=degrada" & _
            "tion_API/degradation"
        '
        'lblText1
        '
        Me.lblText1.AutoSize = True
        Me.lblText1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblText1.Location = New System.Drawing.Point(3, 0)
        Me.lblText1.Name = "lblText1"
        Me.lblText1.Size = New System.Drawing.Size(53, 26)
        Me.lblText1.TabIndex = 3
        Me.lblText1.Text = "Status"
        Me.lblText1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblText5
        '
        Me.lblText5.AutoSize = True
        Me.lblText5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblText5.Location = New System.Drawing.Point(3, 104)
        Me.lblText5.Name = "lblText5"
        Me.lblText5.Size = New System.Drawing.Size(53, 26)
        Me.lblText5.TabIndex = 4
        Me.lblText5.Text = "Coverage"
        Me.lblText5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblText4
        '
        Me.lblText4.AutoSize = True
        Me.lblText4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblText4.Location = New System.Drawing.Point(3, 78)
        Me.lblText4.Name = "lblText4"
        Me.lblText4.Size = New System.Drawing.Size(53, 26)
        Me.lblText4.TabIndex = 5
        Me.lblText4.Text = "Action"
        Me.lblText4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblText3
        '
        Me.lblText3.AutoSize = True
        Me.lblText3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblText3.Location = New System.Drawing.Point(3, 52)
        Me.lblText3.Name = "lblText3"
        Me.lblText3.Size = New System.Drawing.Size(53, 26)
        Me.lblText3.TabIndex = 6
        Me.lblText3.Text = "PRN"
        Me.lblText3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblText2
        '
        Me.lblText2.AutoSize = True
        Me.lblText2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblText2.Location = New System.Drawing.Point(3, 26)
        Me.lblText2.Name = "lblText2"
        Me.lblText2.Size = New System.Drawing.Size(53, 26)
        Me.lblText2.TabIndex = 7
        Me.lblText2.Text = "Type"
        Me.lblText2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblText6
        '
        Me.lblText6.AutoSize = True
        Me.lblText6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblText6.Location = New System.Drawing.Point(3, 130)
        Me.lblText6.Name = "lblText6"
        Me.lblText6.Size = New System.Drawing.Size(53, 27)
        Me.lblText6.TabIndex = 8
        Me.lblText6.Text = "Date"
        Me.lblText6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtField2
        '
        Me.txtField2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtField2.Location = New System.Drawing.Point(62, 29)
        Me.txtField2.Name = "txtField2"
        Me.txtField2.Size = New System.Drawing.Size(182, 20)
        Me.txtField2.TabIndex = 9
        Me.txtField2.Text = "24h"
        '
        'txtField1
        '
        Me.txtField1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtField1.Location = New System.Drawing.Point(62, 3)
        Me.txtField1.Name = "txtField1"
        Me.txtField1.Size = New System.Drawing.Size(182, 20)
        Me.txtField1.TabIndex = 11
        Me.txtField1.Text = "Active"
        '
        'txtField6
        '
        Me.txtField6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtField6.Location = New System.Drawing.Point(62, 133)
        Me.txtField6.Name = "txtField6"
        Me.txtField6.Size = New System.Drawing.Size(182, 20)
        Me.txtField6.TabIndex = 12
        Me.txtField6.Text = "123"
        '
        'txtField5
        '
        Me.txtField5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtField5.Location = New System.Drawing.Point(62, 107)
        Me.txtField5.Name = "txtField5"
        Me.txtField5.Size = New System.Drawing.Size(182, 20)
        Me.txtField5.TabIndex = 13
        Me.txtField5.Text = "68.4"
        '
        'txtField4
        '
        Me.txtField4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtField4.Location = New System.Drawing.Point(62, 81)
        Me.txtField4.Name = "txtField4"
        Me.txtField4.Size = New System.Drawing.Size(182, 20)
        Me.txtField4.TabIndex = 14
        Me.txtField4.Text = "Underperformance Started"
        '
        'txtField3
        '
        Me.txtField3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtField3.Location = New System.Drawing.Point(62, 55)
        Me.txtField3.Name = "txtField3"
        Me.txtField3.Size = New System.Drawing.Size(182, 20)
        Me.txtField3.TabIndex = 15
        Me.txtField3.Text = "Combined"
        '
        'tlProperties
        '
        Me.tlProperties.ColumnCount = 2
        Me.tlProperties.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlProperties.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlProperties.Controls.Add(Me.txtField1, 1, 0)
        Me.tlProperties.Controls.Add(Me.lblText6, 0, 5)
        Me.tlProperties.Controls.Add(Me.txtField2, 1, 1)
        Me.tlProperties.Controls.Add(Me.lblText5, 0, 4)
        Me.tlProperties.Controls.Add(Me.lblText4, 0, 3)
        Me.tlProperties.Controls.Add(Me.lblText3, 0, 2)
        Me.tlProperties.Controls.Add(Me.lblText2, 0, 1)
        Me.tlProperties.Controls.Add(Me.txtField3, 1, 2)
        Me.tlProperties.Controls.Add(Me.txtField4, 1, 3)
        Me.tlProperties.Controls.Add(Me.txtField5, 1, 4)
        Me.tlProperties.Controls.Add(Me.txtField6, 1, 5)
        Me.tlProperties.Controls.Add(Me.lblText1, 0, 0)
        Me.tlProperties.Location = New System.Drawing.Point(9, 25)
        Me.tlProperties.Name = "tlProperties"
        Me.tlProperties.RowCount = 6
        Me.tlProperties.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlProperties.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlProperties.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlProperties.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlProperties.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlProperties.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlProperties.Size = New System.Drawing.Size(247, 157)
        Me.tlProperties.TabIndex = 16
        '
        'txtContent
        '
        Me.txtContent.Location = New System.Drawing.Point(285, 53)
        Me.txtContent.Multiline = True
        Me.txtContent.Name = "txtContent"
        Me.txtContent.Size = New System.Drawing.Size(358, 58)
        Me.txtContent.TabIndex = 18
        '
        'gbParameters
        '
        Me.gbParameters.Controls.Add(Me.tlProperties)
        Me.gbParameters.Location = New System.Drawing.Point(11, 37)
        Me.gbParameters.Name = "gbParameters"
        Me.gbParameters.Size = New System.Drawing.Size(263, 196)
        Me.gbParameters.TabIndex = 21
        Me.gbParameters.TabStop = False
        Me.gbParameters.Text = "Parameters"
        '
        'txtResult
        '
        Me.txtResult.Location = New System.Drawing.Point(490, 118)
        Me.txtResult.Name = "txtResult"
        Me.txtResult.ReadOnly = True
        Me.txtResult.Size = New System.Drawing.Size(153, 20)
        Me.txtResult.TabIndex = 22
        '
        'txtResponse
        '
        Me.txtResponse.Location = New System.Drawing.Point(285, 150)
        Me.txtResponse.Multiline = True
        Me.txtResponse.Name = "txtResponse"
        Me.txtResponse.ReadOnly = True
        Me.txtResponse.Size = New System.Drawing.Size(358, 77)
        Me.txtResponse.TabIndex = 23
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(659, 239)
        Me.Controls.Add(Me.txtResponse)
        Me.Controls.Add(Me.txtResult)
        Me.Controls.Add(Me.gbParameters)
        Me.Controls.Add(Me.txtContent)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.lblURL)
        Me.Controls.Add(Me.btnConnect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Form1"
        Me.Text = "Web Service Tester"
        Me.tlProperties.ResumeLayout(False)
        Me.tlProperties.PerformLayout()
        Me.gbParameters.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents lblURL As System.Windows.Forms.Label
    Friend WithEvents txtURL As System.Windows.Forms.TextBox
    Friend WithEvents lblText1 As System.Windows.Forms.Label
    Friend WithEvents lblText5 As System.Windows.Forms.Label
    Friend WithEvents lblText4 As System.Windows.Forms.Label
    Friend WithEvents lblText3 As System.Windows.Forms.Label
    Friend WithEvents lblText2 As System.Windows.Forms.Label
    Friend WithEvents lblText6 As System.Windows.Forms.Label
    Friend WithEvents txtField2 As System.Windows.Forms.TextBox
    Friend WithEvents txtField1 As System.Windows.Forms.TextBox
    Friend WithEvents txtField6 As System.Windows.Forms.TextBox
    Friend WithEvents txtField5 As System.Windows.Forms.TextBox
    Friend WithEvents txtField4 As System.Windows.Forms.TextBox
    Friend WithEvents txtField3 As System.Windows.Forms.TextBox
    Friend WithEvents tlProperties As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtContent As System.Windows.Forms.TextBox
    Friend WithEvents gbParameters As System.Windows.Forms.GroupBox
    Friend WithEvents txtResult As System.Windows.Forms.TextBox
    Friend WithEvents txtResponse As System.Windows.Forms.TextBox

End Class
