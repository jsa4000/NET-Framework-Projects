

Public Class WSManager

    Private Shared CONTENT_TYPE As String = "application/x-www-form-urlencoded"
    Private Shared HTTP_ACTION As String = "POST"

    Public ServerResponse As String = String.Empty

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pUri"></param>
    ''' <param name="pData"></param>
    ''' <remarks></remarks>
    Public Function GetPOSTResponse(ByVal pUri As String, ByVal pData As String) As String
        Dim result As String = String.Empty
        Dim reader As StreamReader = Nothing
        Dim dataStream As Stream = Nothing
        Dim response As WebResponse = Nothing
        Try
            'Createt the URL o
            Dim uri As New Uri(pUri)
            'Create a Reqest
            Dim request As WebRequest = WebRequest.Create(pUri)
            'Fill the request
            request.Method = HTTP_ACTION
            request.ContentType = CONTENT_TYPE
            'Create the stream to send
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(pData)
            request.ContentLength = byteArray.Length
            dataStream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            'Send the request
            response = request.GetResponse()
            'Get the status of the request
            result = CType(response, HttpWebResponse).StatusDescription
            'Get the stream with the response from server
            dataStream = response.GetResponseStream()
            reader = New StreamReader(dataStream)
            ServerResponse = reader.ReadToEnd()
        Catch ex As Exception
            'Exception
            result = "ERROR"
        Finally
            'Close all handlers
            If (reader IsNot Nothing) Then
                reader.Close()
            End If
            If (dataStream IsNot Nothing) Then
                dataStream.Close()
            End If
            If (response IsNot Nothing) Then
                response.Close()
            End If
        End Try
        Return result
    End Function


End Class
