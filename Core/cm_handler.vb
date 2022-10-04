Imports Discord.WebSocket
Imports Discord
Imports Discord.Commands
Imports System.Reflection

Public Class cm_handler

    Private Config As MasterConfig
    Private WithEvents client As DiscordSocketClient
    Private Command As CommandService

    Public Sub New(config As MasterConfig, Client As DiscordSocketClient)
        Me.client = Client
        Me.Config = config

        Dim CommandConfig As New CommandServiceConfig With {
        .DefaultRunMode = RunMode.Async
                }

        Command = New CommandService(CommandConfig)
        Command.AddModulesAsync(Assembly.GetEntryAssembly()).GetAwaiter()

    End Sub

    Private Async Function executeCommand(RawMsg As SocketMessage) As Task Handles client.MessageReceived
        Dim Message As IUserMessage = CType(RawMsg, IUserMessage)

        If Message Is Nothing OrElse Message.Author.IsBot Then
            Return
        End If

        Dim ArgPos As Integer = 0

        If Not (Message.HasStringPrefix(Config.Prefix, ArgPos) OrElse Message.HasMentionPrefix(client.CurrentUser, ArgPos)) Then
            Return
        End If

        Dim CommandContext As New CommandContext(client, Message)
        Dim CommandResult As IResult = Await Command.ExecuteAsync(CommandContext, ArgPos)


    End Function
End Class
