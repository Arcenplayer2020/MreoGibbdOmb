using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MreoGibbdOmb
{
    internal class HTTPRequester
    {
        public static async Task<int> GetBalance(SocketUser user)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://unbelievaboat.com/api/v1/guilds/1141836586259071028/users/{user.Id}"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhcHBfaWQiOiIxMTc3OTkyMzY0OTE3MTI5OTI4IiwiaWF0IjoxNzAwOTI1NjY1fQ.PlHLyFpBJ7UafkUws8kcVClmm-pijTynC3CQOdSHwdw" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(body);
                int money = (int)json["cash"]!;
                return money;
            
            }
            
        }
        public static async Task TransferMoney(SocketUser userWhoTransfer, SocketUser userWhomTransfer,int money)
        {
            await UpdateMoney(userWhoTransfer, -Math.Abs(money));
            await UpdateMoney(userWhomTransfer, money);
        }

        private static async Task UpdateMoney(SocketUser user, int money)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri($"https://unbelievaboat.com/api/v1/guilds/1141836586259071028/users/{user.Id}"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhcHBfaWQiOiIxMTc3OTkyMzY0OTE3MTI5OTI4IiwiaWF0IjoxNzAwOTI1NjY1fQ.PlHLyFpBJ7UafkUws8kcVClmm-pijTynC3CQOdSHwdw" },
                },
                Content = new StringContent("{\"cash\":" + $"{money}" + "}")
                {
                    Headers =
                    {
                    ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            var response = await client.SendAsync(request);
        }
    }
}
