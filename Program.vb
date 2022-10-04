Imports System
Imports Discord
Imports Discord.Net.Providers.WS4Net
Imports Discord.WebSocket

Module Program
    Private Client As DiscordSocketClient
    Private Config As MasterConfig
    Private DEvents As DiscordEvents
    Private cm_handler As cm_handler


    Sub Main(args As String())
        RunBotAsync.GetAwaiter.GetResult()
    End Sub

    Private Async Function RunBotAsync() As Task

        Config = Await MasterConfig.LoadAsync


        Dim clientconfig As New DiscordSocketConfig With {
        .DefaultRetryMode = RetryMode.AlwaysRetry,
        .LogLevel = LogSeverity.Info,
        .WebSocketProvider = WS4NetProvider.Instance
        }

        'initiate the classes
        Client = New DiscordSocketClient(clientconfig)
        DEvents = New DiscordEvents(Client)
        cm_handler = New cm_handler(Config, Client)

        Await Client.LoginAsync(TokenType.Bot, Config.Token)
        Await Client.StartAsync
        Await Task.Delay(-1)

    End Function
End Module
