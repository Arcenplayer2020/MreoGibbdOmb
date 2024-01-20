using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MreoGibbdOmb
{
    internal class Player
    {
        public string Name { get; set; }
        public string DiscordId { get; set; }
        
        public Player(string DiscordId, string Name)
        {
            this.Name = Name;
            this.DiscordId = DiscordId;
        }
    }
}
