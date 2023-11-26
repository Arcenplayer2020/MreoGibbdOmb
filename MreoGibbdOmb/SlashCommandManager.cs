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
            //BuyLicensePlateCommand.AddOption("номер", ApplicationCommandOptionType.String, "С буквами А, В, Е, К, М, Н, О, Р, С, Т, У и Х. Как X000XX ", false);
            //BuyLicensePlateCommand.AddOption("регион", ApplicationCommandOptionType.String, "Регион Номера",false);

            var WithdrawCommand = new SlashCommandBuilder()
                .WithName("withdraw")
                .WithDescription("выводит средства")
                .AddOption("количество",ApplicationCommandOptionType.Number,"выводит деньги, оставьте пустым если хотите вывести все",isRequired: false)
                .WithDefaultMemberPermissions(GuildPermission.Administrator);



            try
            {
                await DiscordBot.Client.CreateGlobalApplicationCommandAsync(BuyLicensePlateCommand.Build());
                await DiscordBot.Client.CreateGlobalApplicationCommandAsync(WithdrawCommand.Build());
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
                else if (command.CommandName == "withdraw")
                {
                    await BuyManager.Withdraw(command);
                }
            });
            return Task.CompletedTask;
        }
    }
} 
