using EnvyStatsGlobalApplication.Models;
using EnvyStatsGlobalApplication.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvyStatsGlobalApplication
{
    public partial class Form1 : Form
    {
        private static string key;
        private static string _steamUserStatsUrl = "http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v0002/?appid={0}&key={1}&steamid={2}&format=json";
        public Form1()
        {
            InitializeComponent();
            try
            {
                key = File.ReadAllText("key.txt");
            }
            catch (Exception)
            {

                MessageBox.Show("Tienes que crear un archivo llamado 'key.txt'");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string xd = SteamUtils.NormalizeUsername(textBox1.Text);

                var xd2 = GetStats(xd);

                label5.Text = xd2.playerstats.stats[1].value.ToString();
                label6.Text = xd2.playerstats.stats[6].value.ToString();
                label8.Text = ((xd2.playerstats.stats[12].value * 100) / xd2.playerstats.stats[11].value).ToString() + "%";
                double total = (double)xd2.playerstats.stats[1].value / xd2.playerstats.stats[6].value;
                label9.Text = Math.Abs(((float)total)).ToString();
                textBox1.Clear();
            }
            catch
            {
                MessageBox.Show("STEAM ID INGRESADA INCORRECTAMENTE O KEY");
            }

        }


        public static SteamUserStats GetStats(string csteamId)
        {


            HttpClient client = new HttpClient();
            var statsForGameResponse = client.GetStringAsync(string.Format(_steamUserStatsUrl, "304930", key, csteamId));
            SteamUserStats statsForUser = JsonConvert.DeserializeObject<SteamUserStats>(statsForGameResponse.Result);
            return statsForUser;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(key == null || key.Length < 5)Application.Exit();
        }
    }
}
