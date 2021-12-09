using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envy.Models
{
    public class SteamUserStats
    {
        public PlayerStats playerstats { get; set; }
    }

    public class PlayerStats
    {
        public string steamID { get; set; }
        public string gameName { get; set; }

        public List<StatDescriptor> stats { get; set; }

        [JsonIgnore]
        public List<AchievementDescriptor> achievements { get; set; }
    }

    public class AchievementDescriptor
    {
        public string name { get; set; }
        public int achieved { get; set; }
    }

    public class StatDescriptor
    {

        public string name { get; set; }

        public int value { get; set; }
    }
}
