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
            var RegisterCommand = new SlashCommandBuilder()
                .WithName("register")
                .WithDescription("добавляет в вайтлист")
                .AddOption("ник",ApplicationCommandOptionType.String, "ник в биммп",true);
            var GeneratePtsCommand = new SlashCommandBuilder()
                .WithName("generate-pts")
                .WithDescription("генерирует птс")
                .AddOption("марка", ApplicationCommandOptionType.String, "марка и модель машины", true)
                .AddOption("комплектация", ApplicationCommandOptionType.String, "комплектация машины", true)
                .AddOption("цвет", ApplicationCommandOptionType.String, "цвет машины", true)
                .AddOption("двигло", ApplicationCommandOptionType.String, "двигло машины", true)
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("состояние")
                    .WithDescription("состояние авто")
                    .AddChoice("ужасное", 2)
                    .AddChoice("нормальное", 1)
                    .AddChoice("хорошее", 0)
                    .WithRequired(true)
                    .WithType(ApplicationCommandOptionType.Integer))
                .AddOption("год", ApplicationCommandOptionType.Integer, "год машины",true)
                .AddOption("модификации",ApplicationCommandOptionType.String,"модификации иашины",false)
                .WithDefaultMemberPermissions(GuildPermission.Administrator);






            try
            {
                await DiscordBot.Client.CreateGlobalApplicationCommandAsync(BuyLicensePlateCommand.Build());
                await DiscordBot.Client.CreateGlobalApplicationCommandAsync(WithdrawCommand.Build());
                await DiscordBot.Client.CreateGlobalApplicationCommandAsync (GeneratePtsCommand.Build());
                await DiscordBot.Client.CreateGlobalApplicationCommandAsync(RegisterCommand.Build());
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
                else if (command.CommandName == "generate-pts")
                {
                    await VechiclePassport.GeneratePts(command);
                }
                else if(command.CommandName == "register")
                {
                    await WhiteList.AddPlayerToWhitelist(command);
                }
            });
            return Task.CompletedTask;
        }
    }
} 
