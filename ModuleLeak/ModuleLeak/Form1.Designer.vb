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
        Me.btnTEST = New System.Windows.Forms.Button()
        Me.btnTEST2 = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.TextBox()
        Me.btnLaunh = New System.Windows.Forms.Button()
        Me.btnLaunchPool = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnTEST
        '
        Me.btnTEST.Location = New System.Drawing.Point(13, 51)
        Me.btnTEST.Name = "btnTEST"
        Me.btnTEST.Size = New System.Drawing.Size(152, 40)
        Me.btnTEST.TabIndex = 0
        Me.btnTEST.Text = "TEST GLOBAL"
        Me.btnTEST.UseVisualStyleBackColor = True
        '
        'btnTEST2
        '
        Me.btnTEST2.Location = New System.Drawing.Point(13, 97)
        Me.btnTEST2.Name = "btnTEST2"
        Me.btnTEST2.Size = New System.Drawing.Size(152, 40)
        Me.btnTEST2.TabIndex = 1
        Me.btnTEST2.Text = "START"
        Me.btnTEST2.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(13, 12)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.ReadOnly = True
        Me.lblStatus.Size = New System.Drawing.Size(152, 20)
        Me.lblStatus.TabIndex = 2
        '
        'btnLaunh
        '
        Me.btnLaunh.Location = New System.Drawing.Point(12, 144)
        Me.btnLaunh.Name = "btnLaunh"
        Me.btnLaunh.Size = New System.Drawing.Size(152, 40)
        Me.btnLaunh.TabIndex = 3
        Me.btnLaunh.Text = "LAUNCH"
        Me.btnLaunh.UseVisualStyleBackColor = True
        '
        'btnLaunchPool
        '
        Me.btnLaunchPool.Location = New System.Drawing.Point(12, 193)
        Me.btnLaunchPool.Name = "btnLaunchPool"
        Me.btnLaunchPool.Size = New System.Drawing.Size(152, 40)
        Me.btnLaunchPool.TabIndex = 4
        Me.btnLaunchPool.Text = "LAUNCH POOL"
        Me.btnLaunchPool.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(177, 244)
        Me.Controls.Add(Me.btnLaunchPool)
        Me.Controls.Add(Me.btnLaunh)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnTEST2)
        Me.Controls.Add(Me.btnTEST)
        Me.Name = "Form1"
        Me.Text = "TEST"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnTEST As System.Windows.Forms.Button
    Friend WithEvents btnTEST2 As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.TextBox
    Friend WithEvents btnLaunh As System.Windows.Forms.Button
    Friend WithEvents btnLaunchPool As System.Windows.Forms.Button

End Class
