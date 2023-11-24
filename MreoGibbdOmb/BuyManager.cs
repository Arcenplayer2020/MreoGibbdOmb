using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MreoGibbdOmb
{
    internal class BuyManager
    {
        internal static async Task MessageHandler(SocketMessage message)
        {
            //int money;
          

                





                if (message.Content == $"!pay {DiscordBot.Client.CurrentUser.Mention} 4000" && LicensePlateGenerator.UsersWhoCanBuy.Contains(message.Author))
                {
                    LicensePlateGenerator.AddPlayerToList(message.Author);
                }

            }
        }
    }

