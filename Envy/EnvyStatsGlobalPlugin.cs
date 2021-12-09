using Envy;
using Rocket.Core.Plugins;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvyStatsGLOBAL
{
    public class EnvyStatsGlobalPlugin : RocketPlugin<Configuration>
    {
        public static EnvyStatsGlobalPlugin Instance;
        protected override void Load()
        {
            Instance = this;
        }


    }
}
