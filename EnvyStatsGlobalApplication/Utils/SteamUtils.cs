using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnvyStatsGlobalApplication.Utils
{
    public static class SteamUtils
    {
        private static string _steamGetUser = "{0}?xml=1";
        private static string _steamGetUserWithSteamId = "https://steamcommunity.com/profiles/{0}?xml=1";
        public static string NormalizeUsername(string name)
        {
            if (name.Contains("https://steamcommunity.com/id"))
            {

                string[] texts = new WebClient().DownloadString(string.Format(_steamGetUser, name)).Split('<');

                return texts[3].Split('>')[1].Trim();


            }
            else if (name.Contains("https://steamcommunity.com/profiles"))
            {
                return name.Split('/')[4].Trim();
            }

            return null;

        }
    }
}
