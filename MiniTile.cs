using OTAPI.Tile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TShockAPI;

namespace MiniGamesAPI
{
	public class MiniTile
	{
		public int X { get; set; }
		public int Y { get; set; }
		public byte PaintColor { get { return Tile.color(); } }
		public bool Active { get { return Tile.active(); } }
		public int Type { get { return Tile.type; } }
		public ITile Tile { get; set; }
		public MiniTile(int x, int y, ITile tile)
		{
			X = x;
			Y = y;
			Tile = new Tile(tile);

		}
		public void Place()
		{
			if (Active)
			{
				WorldGen.PlaceTile(X, Y, this.Type, false, false, -1, 0);
				WorldGen.PlaceWall(X,Y,this.Tile.wall);
			}
		}
		public void Kill() {
			WorldGen.KillTile(X, Y);
		}
		public void Paint()
		{
			WorldGen.paintTile(X, Y, PaintColor);
		}
		public void Update() 
		{
			TSPlayer.All.SendTileSquare(X,Y,1);
		}
	}
}
