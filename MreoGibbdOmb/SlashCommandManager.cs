using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MreoGibbdOmb
{
    internal class SlashCommandManager
    {
        internal async static Task CreateSlashCommand()
        {
            var BuyLicensePlateCommand = new SlashCommandBuilder();
            BuyLicensePlateCommand.WithName("buy-license-plate");
            BuyLicensePlateCommand.WithDescription("Покупка н-з");
            BuyLicensePlateCommand.AddOption("марка", ApplicationCommandOptionType.String, "Марка машины",true);
            BuyLicensePlateCommand.AddOption("модель", ApplicationCommandOptionType.String, "Модель машины",true);
            BuyLicensePlateCommand.AddOption("номер", ApplicationCommandOptionType.String, "С буквами А, В, Е, К, М, Н, О, Р, С, Т, У и Х. Как X000XX ", false);
            BuyLicensePlateCommand.AddOption("регион", ApplicationCommandOptionType.String, "Регион Номера",false);




            try
            {
                await DiscordBot.Client.CreateGlobalApplicationCommandAsync(BuyLicensePlateCommand.Build());
            }
            catch (HttpException exception)
            {
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
                Console.WriteLine(json);
            }
        }

        internal static Task SlashCommandHandler(SocketSlashCommand command)
        {
            _ = Task.Run(async () =>
            {
                if (command.CommandName == "buy-license-plate")
                {
                    await LicensePlateGenerator.SendLicensePlate(command);
                }
            });
            return Task.CompletedTask;
        }
    }
} 
