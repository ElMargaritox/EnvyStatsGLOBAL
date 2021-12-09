using Envy.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EnvyStatsGlobalConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            if (args.Length == 0) return;



                try
                {
                if (args[0].ToLower() == "-k") { RegisterKey(args[1]); return; }
    
                    try
                    {
                        key = File.ReadAllText($"{Environment.CurrentDirectory}/key.txt");
                        ShowStats(args[0]);
                    }
                    catch
                    {
                        Console.WriteLine("Do you need register a key. Use param -k <Key>");
                    }

                    

                }
                catch { }  
        }

        private static void RegisterKey(string key)
        {
            File.WriteAllText($"{Environment.CurrentDirectory}/key.txt", key);
            Console.WriteLine("Succefully Registered");
        }

        public static SteamUserStats GetStats(string csteamId)
        {


            HttpClient client = new HttpClient();
            var statsForGameResponse = client.GetStringAsync(string.Format(_steamUserStatsUrl, "304930", key, csteamId));
            SteamUserStats statsForUser = JsonConvert.DeserializeObject<SteamUserStats>(statsForGameResponse.Result);
            return statsForUser;
        }

        private static void ShowStats(string csteamId)
        {
            
            try
            {
                SteamUserStats steamUserStats = GetStats(csteamId);

                for (int i = 0; i <= 18; i++)
                {
                    Console.Write($"{steamUserStats.playerstats.stats[i].name}: "); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"{steamUserStats.playerstats.stats[i].value} \n"); Console.ResetColor();
                }

                 
            }
            catch
            {
                Console.WriteLine("Couldnt find a SteamPlayer :( or Key is invalid. ");
            }
            
        }

        private static string key;
        private static string _steamUserStatsUrl = "http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v0002/?appid={0}&key={1}&steamid={2}&format=json";
    }
}
