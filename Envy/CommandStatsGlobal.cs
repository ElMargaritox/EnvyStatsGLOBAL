using Envy.Models;
using EnvyStatsGLOBAL;
using Newtonsoft.Json;
using Rocket.API;
using Rocket.Core.Utils;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Envy
{
    public class CommandStatsGlobal : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "statsglobal";

        public string Help => "show your stats global";

        public string Syntax => "/stats <playerName or ID>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {


            try
            {
                UnturnedPlayer target = UnturnedPlayer.FromName(command[0]);
                if (target == null) throw new Exception("Player not found");

                if (target.SteamProfile.PrivacyState != "public") throw new Exception(String.Format("{0} is steam profile is not public :(", target.CharacterName));

                SteamUserStats steamUserStats = GetStats(target.CSteamID.ToString());

                if (steamUserStats == null) throw new Exception("The profile is private");
                TaskDispatcher.QueueOnMainThread(() =>
                {
                    string message = string.Empty;
                    int x = 0;
                    for (int i = 0; i < 20; i++)
                    {
                        message += $"{steamUserStats.playerstats.stats[i].name}: { steamUserStats.playerstats.stats[i].value}"; x++;
                        if(x == 3) { UnturnedChat.Say(caller, message); message = string.Empty; x = 0;}

                        if(i == 19) { UnturnedChat.Say(caller, message); }
                    }
                });
            }
            catch(Exception x)
            {
                UnturnedChat.Say(caller, x.Message, Color.red);
            }
        }

        private  SteamUserStats GetStats(string csteamId)
        {


            HttpClient client = new HttpClient();
            var statsForGameResponse = client.GetStringAsync(string.Format(_steamUserStatsUrl, "304930", EnvyStatsGlobalPlugin.Instance.Configuration.Instance.Key, csteamId));
            SteamUserStats statsForUser = JsonConvert.DeserializeObject<SteamUserStats>(statsForGameResponse.Result);
            return statsForUser;
        }

        private string _steamUserStatsUrl = "http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v0002/?appid={0}&key={1}&steamid={2}&format=json";
    }
}
