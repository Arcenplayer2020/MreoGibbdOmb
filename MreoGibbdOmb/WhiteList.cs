using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MreoGibbdOmb
{
    internal class WhiteList
    {
        const string jsonPath = "D:\\servak\\Resources\\Server\\AntiDDos\\Players.json";
        internal async static Task AddPlayerToWhitelist(SocketSlashCommand command)
        {

            var json = File.ReadAllText(jsonPath);
            var whiteList = JsonConvert.DeserializeObject<List<Player>>(json);
            CheckPlayerDiscordId(whiteList, command);
            whiteList.Add(new Player((string)command.User.Id.ToString(), (string)command.Data.Options.First().Value));
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(whiteList));
            
            await command.RespondAsync("Успешно добавились в вайт лист",ephemeral : true);

        }

        private static void CheckPlayerDiscordId(List<Player> whiteList, SocketSlashCommand command)
        {
            var Player = whiteList.Find(x => command.User.Id.ToString() == x.DiscordId);
            if (Player != null)
            {
                whiteList.Remove(Player);
            }
        }
    }
}
