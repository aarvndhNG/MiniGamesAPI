using Microsoft.Xna.Framework;
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
		public Point TestPoint_1 { get; set; }
		public Point TestPoint_2 { get; set; }
		public List<MiniTile> Tiles { get; set; }
		public PrebuildBoard(MiniRegion region)
		{
			this.ID = region.ID;
			this.Name = region.Name + "的预制板";
			this.Region = region;
			this.Tiles = new List<MiniTile>();
			for (int i = region.TopLeft.X; i <= region.BottomRight.X; i++)
			{
				for (int j = region.TopLeft.Y; j <= region.BottomRight.Y; j++)
				{
					this.Tiles.Add(new MiniTile(i, j, Terraria.Main.tile[i, j]));
				}
			}
		}
		public PrebuildBoard(Point topLeft,Point bottomRight,int id,string name) 
		{
			this.ID = id;
			this.Name = name + "的预制板";
			this.Region = null;
			this.Tiles = new List<MiniTile>();
			TestPoint_1 = topLeft;
			TestPoint_2 = bottomRight;
			for (int i = topLeft.X ; i <= bottomRight.X ; i++)
			{
				for (int j = topLeft.Y ; j <= bottomRight.Y ; j++)
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
			}
			TSPlayer.All.SendTileRect((short)Region.TopLeft.X, (short)Region.TopLeft.Y, (byte)(Region.TopRight.X + 1 - Region.TopLeft.X) , (byte)(Region.BottomLeft.Y + 1 - Region.TopLeft.Y));
		}
		public string ShowInfo() 
		{
			StringBuilder info = new StringBuilder();
			info.AppendLine($"[{TestPoint_1.X},{TestPoint_1.Y}|{TestPoint_2.X},{TestPoint_2.Y}|]");
			foreach (var tile in Tiles)
            {
				
				info.Append($"[{tile.Type}|{tile.X}|{tile.Y}]");
            }
			return info.ToString();
		}
	}
}
