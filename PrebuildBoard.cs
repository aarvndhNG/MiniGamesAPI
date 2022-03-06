using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

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
					this.Tiles.Add(new MiniTile(i, j, Main.tile[i, j]));
				}
			}
		}

		
		public void ReBuild()
		{
			foreach (MiniTile miniTile in this.Tiles)
			{
				miniTile.Place();
			}
			int x = this.Region.TopLeft.X;
			int x2 = this.Region.TopRight.X - 1;
			int y = this.Region.TopLeft.Y;
			int y2 = this.Region.BottomLeft.Y - 1;
			int lowX = Netplay.GetSectionX(x);
			int highX = Netplay.GetSectionX(x2);
			int lowY = Netplay.GetSectionY(y);
			int highY = Netplay.GetSectionY(y2);
			foreach (RemoteClient client in Netplay.Clients.Where(c=>c.IsActive))
			{
				for (int i = lowX; i <= highX; i++)
				{
					for (int j = lowY; j <= highY; j++)
					{
						client.TileSections[i, j] = false;
					}
				}
			}
		}
		public string ShowInfo() 
		{
			StringBuilder info = new StringBuilder();
            foreach (var tile in Tiles)
            {
				info.Append($"[{tile.Tile.wall}]");
            }
			return info.ToString();
		}
	}
}
