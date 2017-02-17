Module xmlSettings

    Public Sub SaveXMLSetting(ByVal Group As String, ByVal SubGroup As String, _
    ByVal SubGroupValue As String, ByVal XML_File As String)
        Dim DocXML As New Xml.XmlDocument
        DocXML = Create_XML_File(XML_File, "settings")
        CreateUniqueNode(DocXML, "//settings", Group)
        CreateUniqueNode(DocXML, "//settings" & "/" & Group, SubGroup, SubGroupValue)
        DocXML.Save(XML_File)
    End Sub

    Public Function ReadXMLSetting(ByVal Group As String, ByVal SubGroup As String, _
    ByVal DefaultValue As String, ByVal XML_File As String) As String

        ReadXMLSetting = DefaultValue
        If Not My.Computer.FileSystem.FileExists(XML_File) Then Exit Function
        Dim DocXML As New Xml.XmlDocument
        Dim MainNode As Xml.XmlNode
        Try
            DocXML.Load(XML_File)
        Catch ex As Exception
            'xml file doesn't exist or corrupted structure
            Exit Function
        End Try
        MainNode = DocXML.SelectSingleNode("//settings" & "/" & Group & "/" & SubGroup)
        If Not MainNode Is Nothing Then
            ReadXMLSetting = MainNode.InnerText
        End If
    End Function

End Module
