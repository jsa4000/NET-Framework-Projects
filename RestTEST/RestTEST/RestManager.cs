using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel.Web;

namespace RestTEST
{
    class RestManager
    {

        Private Sub GetPOSTResponse(uri As Uri, data As String, callback As Action(Of Response))
                //Dim request As HttpWebRequest = DirectCast(HttpWebRequest.Create(uri), HttpWebRequest)

                //request.Method = "POST"
                //request.ContentType = "text/plain;charset=utf-8"

                //Dim encoding As New System.Text.UTF8Encoding()
                //Dim bytes As Byte() = encoding.GetBytes(data)

                //request.ContentLength = bytes.Length

                //Using requestStream As Stream = request.GetRequestStream()
                //    ' Send the data.
                //    requestStream.Write(bytes, 0, bytes.Length)
                //End Using

                //request.BeginGetResponse(
                //    Function(x)
                //        Using response As HttpWebResponse = DirectCast(request.EndGetResponse(x), HttpWebResponse)
                //            If callback IsNot Nothing Then
                //                Dim ser As New DataContractJsonSerializer(GetType(Response))
                //                callback(TryCast(ser.ReadObject(response.GetResponseStream()), Response))
                //            End If
                //        End Using
                //        Return 0
                //    End Function, Nothing)
        End Sub
    }
}
