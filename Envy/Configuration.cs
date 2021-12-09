using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envy
{
    public class Configuration : IRocketPluginConfiguration
    {
        public string Key;
        public void LoadDefaults()
        {
            Key = "Key Here";
        }
    }
}
