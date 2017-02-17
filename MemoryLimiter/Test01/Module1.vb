Module Module1

    Sub Main()
        Dim text As New System.Text.StringBuilder("JavierSantosAndrés")
        While True
            text.Append(text.ToString())
            System.Threading.Thread.Sleep(1000)
        End While
    End Sub

End Module
