using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MreoGibbdOmb
{
    internal class LicensePlateGenerator
    {
        static public List<SocketUser> PayingUsers = new List<SocketUser>();
        static public List<SocketUser>UsersWhoCanBuy = new List<SocketUser>();
        private static readonly List<char> chars = new List<char>
        {
            'A',
            'В',
            'Е',
            'К',
            'М',
            'Н',
            'О',
            'Р',
            'С',
            'Т',
            'У',
            'Х',

        };
        internal static async Task SendLicensePlate(SocketSlashCommand command)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://unbelievaboat.com/api/v1/guilds/1071781069130563645/users/{command.User.Id}"),
                Headers =
            {
            { "accept", "application/json" },
            { "Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhcHBfaWQiOiIxMTc3NTc3NTQwMTU2NTI2MzgyIiwiaWF0IjoxNzAwODI4NzQyfQ.O7_cw10NJfQQgHLU-n9X-WHTkHGUUkWx8pgQybMQNi4" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(body);
                int money = (int)json["cash"]!;
                if (money >= 4000 && !UsersWhoCanBuy.Contains(command.User))
                {
                    UsersWhoCanBuy.Add(command.User);
                    await command.RespondAsync("оплатите, и заново используйте команду");

                }
                if (command.Data.Options.Count == 2)
                {
                    if (PayingUsers.Contains(command.User))
                    {
                        await GetRandomLicensePlate(command);
                        PayingUsers.Remove(command.User);
                        UsersWhoCanBuy.Remove(command.User);
                    }
                    
                    else if(!PayingUsers.Contains(command.User) && !UsersWhoCanBuy.Contains(command.User))
                    {
                        await command.RespondAsync("вы не оплатили", ephemeral: true);
                    }
                }
            }
        }
        private static async Task GetRandomLicensePlate(SocketSlashCommand command)
        {

            await command.RespondAsync($"Ваш номерной знак: {GenerateLicensePlate()}");
        }

        private static string GenerateLicensePlate()
        {
            string number = GenerateNumber();
            
            string regionCode = GenerateRegionCode().ToString();


            return $"{number} / {regionCode}";
        }

        private static string GenerateRegionCode()
        {
            List<int> numbers = GetRegionCodes();
            Random random = new Random();
            string regionCode = numbers[random.Next(numbers.Count)].ToString();
            if (regionCode.Length == 1)
            {
                regionCode = $"0{regionCode}";
            }
            return regionCode;

        }

        private static List<int> GetRegionCodes()
        {
            List<int> numbers = new List<int>();
            for (int i = 1; i < 96; i++)
            {
                numbers.Add(i);
            }
            numbers.AddRange(new List<int>
            {
                102,113,116,716,121,23,123,24,124,25,125,134,150,190,750,790,152,154,159,161,163,164,96,196,173,174,97,99,177,197,199,777,799,98,178,186
            });
            numbers.Remove(20);
            numbers.Remove(80);
            numbers.Remove(85);
            numbers.Remove(91);
            numbers.Remove(93);
            return numbers;
        }

        private static string GenerateNumber()
        {
            var random = new Random();
            var Letters = new char[3];
            for (int i = 0; i < 3; i++)
            {
                Letters[i] = chars[random.Next(chars.Count)];
            }
            return $"{Letters[0]} {random.Next(1, 999)} {Letters[1]} {Letters[2]}";
        }

        internal static void AddPlayerToList(SocketUser author)
        {
            PayingUsers.Add(author);
        }
    }
}
