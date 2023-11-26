using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MreoGibbdOmb
{
    internal class BuyManager
    {
        internal static  Task MessageHandler(SocketMessage message)
        {
            //int money;
          

                





                if (message.Content == $"!pay {DiscordBot.Client.CurrentUser.Mention} 4000" && LicensePlateGenerator.UsersWhoCanBuy.Contains(message.Author))
                {
                    LicensePlateGenerator.AddPlayerToList(message.Author);
                }
                return Task.CompletedTask;

            }

        internal static async Task Withdraw(SocketSlashCommand command)
        {
            int money = 0;
            if (command.Data.Options.Count == 0)
            {
                money = await HTTPRequester.GetBalance(DiscordBot.Client.CurrentUser);
                
            }
            else
            {
                money = Convert.ToInt32(command.Data.Options.First().Value);
                int userBalance = await HTTPRequester.GetBalance(DiscordBot.Client.CurrentUser);
                if (money >= userBalance)
                {
                    await command.RespondAsync("у вас нет денег");
                    return;
                }
                
            }
            await HTTPRequester.TransferMoney(DiscordBot.Client.CurrentUser,command.User,money);
            await command.RespondAsync("деньги успешно выведены");
        }
    }
}

