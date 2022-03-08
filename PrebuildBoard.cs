using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TShockAPI;

namespace MiniGamesAPI
{
	public class PrebuildBoard
	{
		public string Name { get; set; }
		public int ID { get; set; }
		public MiniRegion Region { get; private set; }
		public List<MiniTile> Tiles { get; set; }
		public PrebuildBoard(MiniRegion region)
		{
			this.ID = region.ID;
			this.Name = region.Name + "的预制板";
			this.Region = region;
			this.Tiles = new List<MiniTile>();
			for (int i = region.TopLeft.X; i < region.TopRight.X; i++)
			{
				for (int j = region.TopLeft.Y; j < region.BottomLeft.Y; j++)
				{
					this.Tiles.Add(new MiniTile(i, j, Terraria.Main.tile[i, j]));
				}
			}
		}

		
		public void ReBuild()
		{
			foreach (MiniTile miniTile in this.Tiles)
			{
				miniTile.Place();
				miniTile.Paint();
			}
			TSPlayer.All.SendTileSquare(Region.TopLeft.X, Region.TopLeft.Y,Region.Area.Width *Region.Area.Height);
		}
		public string ShowInfo() 
		{
			StringBuilder info = new StringBuilder();
            foreach (var tile in Tiles)
            {
				info.Append($"[{tile.Type}]");
            }
			return info.ToString();
		}
	}
}
