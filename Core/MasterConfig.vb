Imports Newtonsoft.Json
Imports System.IO

Public Class MasterConfig
    Public ReadOnly Property Token As String
    Public ReadOnly Property Prefix As String


    <JsonConstructor>
    Public Sub New(Token As String, Prefix As String)
        Me.Token = Token
        Me.Prefix = Prefix


    End Sub

    <JsonIgnore> Private Shared filename As String = "masterconfig.json"

    Public Shared Async Function LoadAsync() As Task(Of MasterConfig)

        If File.Exists(filename) Then
            Dim Json As String = Await File.ReadAllTextAsync(filename)
            Return JsonConvert.DeserializeObject(Of MasterConfig)(Json)
        End If
        Return Await CreateAsync()
    End Function

    Private Shared Async Function CreateAsync() As Task(Of MasterConfig)
        Dim Token As String = Nothing
        Dim Prefix As String = Nothing

        Do While String.IsNullOrWhiteSpace(Token)
            Console.Write("> Enter Token :")
            Token = Console.ReadLine
        Loop

        Do While String.IsNullOrWhiteSpace(Prefix)
            Console.Write("> Enter Prefix : ")
            Prefix = Console.ReadLine
        Loop




        Dim Config As New MasterConfig(Token, Prefix)
        Await Config.SaveAsync
        Return Config

    End Function

    Private Async Function SaveAsync() As Task
        Dim Json As String = JsonConvert.SerializeObject(Me, Formatting.Indented)
        Await File.WriteAllTextAsync(filename, Json)
    End Function

End Class
