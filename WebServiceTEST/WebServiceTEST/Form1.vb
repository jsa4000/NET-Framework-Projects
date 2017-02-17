Public Class Form1

    Private Function GenerateContent() As String

        'Createt the URL 
        Dim content As String = "status=" & txtField1.Text & "&coverage_type=" & txtField2.Text & "&coverage_geo=" & txtField3.Text & _
        "&coverage_value=" & txtField4.Text & "&occurred=" & txtField5.Text & "&coverage_action=" & txtField6.Text
        'Encode the url
        Return Uri.EscapeUriString(content)
    End Function

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        'Get the URL and the parameters to send to the server
        If (String.IsNullOrEmpty(txtURL.Text)) Then
            MsgBox("You must specify the URL with the Web Service to call.")
            Return
        End If
        If (String.IsNullOrEmpty(txtContent.Text)) Then
            MsgBox("You must reload the Content to send to the Web Service.")
            Return
        End If
        Dim wsURL As String = txtURL.Text
        Dim content As String = txtContent.Text
        'Do the call to the server
        Dim wsManager As New WSManager()
        Dim startDate As Date = DateTime.Now
        Dim message As String = wsManager.GetPOSTResponse(wsURL, content)
        Dim millisencond As Double = DateTime.Now.Subtract(startDate).Milliseconds
        'MsgBox(message & "  (" & millisencond & "ms)")
        'Update GUI
        txtResponse.Text = wsManager.ServerResponse
        txtResult.Text = message & "  (" & millisencond & "ms)"

    End Sub

    Private Sub btnContent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Generate And show the Content 
        txtContent.Text = GenerateContent()
    End Sub

    Private Sub txtField1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    txtField1.TextChanged, txtField2.TextChanged, txtField3.TextChanged, txtField4.TextChanged, txtField5.TextChanged, txtField6.TextChanged
        'Generate And show the Content 
        txtContent.Text = GenerateContent()
    End Sub
End Class
