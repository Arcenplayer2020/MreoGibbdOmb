using Discord;
using Discord.WebSocket;

namespace MreoGibbdOmb
{
    internal class DiscordBot
    {
        static public DiscordSocketClient Client = new DiscordSocketClient();
        async public Task StartBotAsync()
        {
            Client.Log += Log;
            string token = GetToken();
            await Client.LoginAsync(TokenType.Bot,token);
            await Client.StartAsync();
            Client.Ready += ClientReady;
            Client.MessageReceived += BuyManager.MessageHandler;
            Client.SlashCommandExecuted += SlashCommandManager.SlashCommandHandler;
            await Task.Delay(Timeout.Infinite);
        }

        private Task ClientReady()
        {
            _ = Task.Run(async () =>
            {
                await SlashCommandManager.CreateSlashCommand();
            });
            return Task.CompletedTask;
        }


        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
        private string GetToken()
        {
            string? token = Environment.GetEnvironmentVariable("token");
            if (token == null)
            {
                throw new Exception("token is null");
            }
            return token;

        }
    }
}
