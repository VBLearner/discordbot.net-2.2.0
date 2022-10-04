Imports Discord
Imports Discord.WebSocket

Public Class DiscordEvents
    Private WithEvents Client As DiscordSocketClient

    Public Sub New(Client As DiscordSocketClient)
        Me.Client = Client
    End Sub

    Private Function Log(Message As LogMessage) As Task Handles Client.Log
        Console.WriteLine(Message.ToString)
        Return Task.CompletedTask
    End Function
End Class
