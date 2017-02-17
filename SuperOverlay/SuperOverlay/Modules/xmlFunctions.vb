Module xmlFunctions

    ' This function creates an xml file at XML_File and its
    ' root path will be RootPath
    Public Function Create_XML_File(ByVal XML_File As String, ByVal RootPath As String) As Xml.XmlDocument
        Create_XML_File = New Xml.XmlDocument
        Try
            Create_XML_File.Load(XML_File)
        Catch ex As Exception
            ' xml file doesn't exist or corrupted structure
            If My.Computer.FileSystem.FileExists(XML_File) Then
                My.Computer.FileSystem.DeleteFile(XML_File)
            End If
            ' Create directory if doesn't exist
            If Not My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.GetFileInfo(XML_File).DirectoryName) Then
                My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.GetFileInfo(XML_File).DirectoryName)
            End If
            ' Create xml file
            Dim versionXML As Xml.XmlProcessingInstruction
            versionXML = Create_XML_File.CreateProcessingInstruction("xml", "version=""1.0""")
            Create_XML_File.PreserveWhitespace = False
            Create_XML_File.AppendChild(versionXML)
            Create_XML_File.AppendChild(Create_XML_File.CreateElement(RootPath))
            Create_XML_File.Save(XML_File)
        End Try
    End Function

    ' This function creates a node in the xml file xmlDocument with the
    ' path XMLxpath\Node2Create with the optional value of NodeValue.
    ' If two nodes have the same name, two nodes with the same name
    ' will be created.
    Public Sub CreateNode(ByVal xmlDocument As Xml.XmlDocument, ByVal XMLxpath As String, _
    ByVal Node2Create As String, Optional ByVal NodeValue As String = Nothing)
        Dim MainNode As Xml.XmlNode
        Dim ElementToAdd As Xml.XmlElement

        MainNode = xmlDocument.SelectSingleNode(XMLxpath)
        ElementToAdd = MainNode.OwnerDocument.CreateElement(Node2Create)
        If Not NodeValue Is Nothing Then
            ElementToAdd.InnerText = NodeValue
        End If
        MainNode.AppendChild(ElementToAdd)
    End Sub

    ' This function creates a node in the xml file xmlDocument with the
    ' path XMLxpath\Node2Create with the optional value of NodeValue.
    ' If the node already exists, only the value will be updated.
    Public Sub CreateUniqueNode(ByVal xmlDocument As Xml.XmlDocument, ByVal XMLxpath As String, _
    ByVal Node2Create As String, Optional ByVal NodeValue As String = Nothing)
        Dim MainNode As Xml.XmlNode

        MainNode = xmlDocument.SelectSingleNode(XMLxpath & "/" & Node2Create)
        If MainNode Is Nothing Then
            CreateNode(xmlDocument, XMLxpath, Node2Create, NodeValue)
        Else
            If Not NodeValue Is Nothing Then
                MainNode.InnerText = NodeValue
            End If
        End If
    End Sub

    ' This function deletes the node in the xml file xmlDocument with
    ' the path XMLxpath
    Public Sub DeleteNode(ByVal xmlDocument As Xml.XmlDocument, ByVal XMLxpath As String)
        Dim MainNode As Xml.XmlNode

        MainNode = xmlDocument.SelectSingleNode(XMLxpath)
        If Not MainNode Is Nothing Then
            MainNode.ParentNode.RemoveChild(MainNode)
        End If
    End Sub
End Module
