namespace MreoGibbdOmb
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            DiscordBot bot = new DiscordBot();
            await bot.StartBotAsync();
        }
    }
}