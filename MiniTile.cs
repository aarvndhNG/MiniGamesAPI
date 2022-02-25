using OTAPI.Tile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace MiniGamesAPI
{
	public class MiniTile
	{
		public int X { get; set; }
		
		public int Y { get; set; }
		public ITile Tile { get; set; }
		public MiniTile(int x, int y, ITile tile)
		{
			this.X = x;
			this.Y = y;
			this.Tile = tile;
		}	
		public void Place()
		{
			WorldGen.PlaceTile(this.X, this.Y, (int)this.Tile.type, false, false, -1, 0);
		}
	}
}
