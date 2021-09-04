using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TerrariaApi.Server;

namespace MiniGamesAPI
{
    [ApiVersion(2,1)]
    public class MainPlugin : TerrariaPlugin
    {
        public MainPlugin(Terraria.Main game) : base(game){}

        public override string Name => "MiniGamesAPI";

        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public override string Author => "豆沙";

        public override string Description => "A MiniGames Framework";

        public override void Initialize()
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
    }
}
