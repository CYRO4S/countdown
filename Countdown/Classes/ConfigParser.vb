Class ConfigParser

#Region "Windows APIs"
    Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Int32, ByVal lpFileName As String) As Int32
    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Int32
    Private Declare Function GetPrivateProfileSection Lib "kernel32" Alias "GetPrivateProfileSectionA" (ByVal lpAppName As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lIniFileName As String) As Long
    Private Declare Function WritePrivateProfileSection Lib "kernel32" Alias "WritePrivateProfileSectionA" (ByVal lpAppName As String, ByVal lpString As String, ByVal lIniFileName As String) As Long
#End Region

#Region "Properties"

    ''' <summary>
    ''' Default INI file path
    ''' </summary>
    ''' <returns></returns>
    Public Property DefaultFilePath As String

#End Region

#Region "Exceptions"
    Public Class SectionNotFoundException : Inherits Exception
    End Class
#End Region

#Region "Methods"

    ''' <summary>
    ''' Create a new instance
    ''' </summary>
    Public Sub New()
        DefaultFilePath = ""
    End Sub

    ''' <summary>
    ''' Create a new instance from a specified INI file
    ''' </summary>
    ''' <param name="DefaultFilePath"></param>
    Public Sub New(ByVal DefaultFilePath As String)
        Me.DefaultFilePath = DefaultFilePath
    End Sub

    ''' <summary>
    ''' Get value from specified section and key
    ''' </summary>
    ''' <param name="Section">Section</param>
    ''' <param name="AppName">Key</param>
    ''' <param name="DefaultValue">Default Value</param>
    ''' <param name="FileName">INI file path</param>
    ''' <returns></returns>
    Public Shared Function GetValue(ByVal Section As String, ByVal AppName As String, ByVal DefaultValue As String, ByVal FileName As String) As String
        Dim Str As String = ""
        Str = LSet(Str, 256)
        GetPrivateProfileString(Section, AppName, DefaultValue, Str, Len(Str), FileName)
        Return Microsoft.VisualBasic.Left(Str, InStr(Str, Chr(0)) - 1)
    End Function

    ''' <summary>
    ''' Get value from specified section and key
    ''' </summary>
    ''' <param name="Section">Section</param>
    ''' <param name="AppName">Key</param>
    ''' <param name="DefaultValue">Default Value</param>
    ''' <returns></returns>
    Public Function GetValue(ByVal Section As String, ByVal AppName As String, ByVal DefaultValue As String) As String
        Dim Str As String = ""
        Str = LSet(Str, 256)
        GetPrivateProfileString(Section, AppName, DefaultValue, Str, Len(Str), Me.DefaultFilePath)
        Return Microsoft.VisualBasic.Left(Str, InStr(Str, Chr(0)) - 1)
    End Function

    ''' <summary>
    ''' Set value of specified section and key
    ''' </summary>
    ''' <param name="Section"></param>
    ''' <param name="AppName"></param>
    ''' <param name="Value"></param>
    ''' <param name="FileName">INI file path</param>
    ''' <returns></returns>
    Public Shared Function SetValue(ByVal Section As String, ByVal AppName As String, ByVal Value As String, ByVal FileName As String) As Long
        Return WritePrivateProfileString(Section, AppName, Value, FileName)
    End Function

    ''' <summary>
    ''' Set value of specified section and key
    ''' </summary>
    ''' <param name="Section"></param>
    ''' <param name="AppName"></param>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Public Function SetValue(ByVal Section As String, ByVal AppName As String, ByVal Value As String) As Long
        Return WritePrivateProfileString(Section, AppName, Value, Me.DefaultFilePath)
    End Function

    ''' <summary>
    ''' Get all section names from a specified INI file
    ''' </summary>
    ''' <param name="Path"></param>
    ''' <returns></returns>
    Public Shared Function GetAllSections(ByVal Path As String)
        Const ProStringLen = 8096
        Dim Res As Long, S As String, i As Long
        S = Space(ProStringLen)
        Res = GetPrivateProfileString(vbNullString, vbNullString, vbNullString, S, ProStringLen, Path)
        S = Trim(Left(S, Res))
        If S <> "" Then
            i = Len(S)
            Do While Mid(S, i, 1) = vbNullChar
                i = i - 1
            Loop
            S = Left(S, i)
        End If
        Return Split(Trim(S), vbNullChar)
    End Function

    ''' <summary>
    ''' Get all section names from INI file
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllSections()
        Const ProStringLen = 8096
        Dim Res As Long, S As String, i As Long
        S = Space(ProStringLen)
        Res = GetPrivateProfileString(vbNullString, vbNullString, vbNullString, S, ProStringLen, Me.DefaultFilePath)
        S = Trim(Left(S, Res))
        If S <> "" Then
            i = Len(S)
            Do While Mid(S, i, 1) = vbNullChar
                i = i - 1
            Loop
            S = Left(S, i)
        End If
        Return Split(Trim(S), vbNullChar)
    End Function

    ''' <summary>
    ''' Remove a section from a specified INI file
    ''' </summary>
    ''' <param name="Section"></param>
    ''' <param name="IniFile"></param>
    ''' <returns></returns>
    Public Shared Function RemoveSection(Section As String, IniFile As String) As Long
        Return WritePrivateProfileString(Section, vbNullString, vbNullString, IniFile)
    End Function

    ''' <summary>
    ''' Remove a section from INI file
    ''' </summary>
    ''' <param name="Section"></param>
    ''' <returns></returns>
    Public Function RemoveSection(Section As String) As Long
        Return WritePrivateProfileString(Section, vbNullString, vbNullString, Me.DefaultFilePath)
    End Function

    ''' <summary>
    ''' Remove a key from a specified section
    ''' </summary>
    ''' <param name="Section"></param>
    ''' <param name="Key"></param>
    ''' <param name="IniFile"></param>
    ''' <returns></returns>
    Public Shared Function RemoveKey(Section As String, Key As String, IniFile As String) As Long
        Return WritePrivateProfileString(Section, Key, 0&, IniFile)
    End Function

    ''' <summary>
    ''' Remove a key from a specified section
    ''' </summary>
    ''' <param name="Section"></param>
    ''' <param name="Key"></param>
    ''' <returns></returns>
    Public Function RemoveKey(Section As String, Key As String) As Long
        Return WritePrivateProfileString(Section, Key, 0&, Me.DefaultFilePath)
    End Function

    ''' <summary>
    ''' Remove all keys from a specified section
    ''' </summary>
    ''' <param name="Section"></param>
    ''' <param name="IniFile"></param>
    ''' <returns></returns>
    Public Shared Function ClearSection(Section As String, IniFile As String) As Long
        Return WritePrivateProfileSection(Section, "", IniFile)
    End Function

    ''' <summary>
    ''' Remove all keys from a specified section
    ''' </summary>
    ''' <param name="Section"></param>
    ''' <returns></returns>
    Public Function ClearSection(Section As String) As Long
        Return WritePrivateProfileSection(Section, "", Me.DefaultFilePath)
    End Function

    ''' <summary>
    ''' Return content of the INI file
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="ArgumentNullException">tsDefaultFilePath is null</exception>
    ''' <exception cref="IO.FileNotFoundException">INI file not found</exception>
    Public Overrides Function ToString() As String
        If Me.DefaultFilePath Is Nothing Then
            Throw New ArgumentException
            Exit Function
        ElseIf IO.File.Exists(Me.DefaultFilePath) = False Then
            Throw New IO.FileNotFoundException
            Exit Function
        End If
        Dim sr As New IO.StreamReader(Me.DefaultFilePath)
        Return sr.ReadToEnd
        sr.Close()
        sr.Dispose()
    End Function

    Public Function GetAllKeyValuePairs(ByVal Section As String) As Dictionary(Of String, String)
        Dim dicReturn As New Dictionary(Of String, String)

        If Not IO.File.Exists(DefaultFilePath) Then Throw New IO.FileNotFoundException
        Dim srIni As New IO.StreamReader(DefaultFilePath, Text.Encoding.UTF8)

        Dim strLine As String
        Do
            strLine = srIni.ReadLine()
            If srIni.EndOfStream = True Then
                Throw New SectionNotFoundException
                Exit Function
            End If
        Loop Until strLine = $"[{Section}]"

        Do
            strLine = srIni.ReadLine

            If strLine.IndexOf("]") > 0 Then Exit Do

            Dim strPair() As String = strLine.Split("=")
            Dim key As String = strPair(0).Trim()
            Dim value As String = strPair(1).Trim()
            dicReturn.Add(key, value)

        Loop Until srIni.EndOfStream

        Return dicReturn
    End Function

    Public Shared Function GetAllKeyValuePairs(ByVal Section As String, ByVal Path As String) As Dictionary(Of String, String)
        Dim dicReturn As New Dictionary(Of String, String)

        If Not IO.File.Exists(Path) Then Throw New IO.FileNotFoundException
        Dim srIni As New IO.StreamReader(Path, Text.Encoding.UTF8)

        Dim strLine As String
        Do
            strLine = srIni.ReadLine()
            If srIni.EndOfStream = True Then
                Throw New SectionNotFoundException
                Exit Function
            End If
        Loop Until strLine = $"[{Section}]"

        Do
            strLine = srIni.ReadLine

            If strLine.IndexOf("]") > 0 Then Exit Do

            Dim strPair() As String = strLine.Split("=")
            Dim key As String = strPair(0).Trim()
            Dim value As String = strPair(1).Trim()
            dicReturn.Add(key, value)

        Loop Until srIni.EndOfStream

        Return dicReturn
    End Function


#End Region

End Class