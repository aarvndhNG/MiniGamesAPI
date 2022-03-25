﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using TShockAPI;

namespace MiniGamesAPI.Core
{
	public class MiniRegion
	{

		public string Name { get; set; }


		public int ID { get; set; }


		public List<string> AllowGroups { get; set; }


		public List<string> AllowUsers { get; set; }


		public List<string> Owners { get; set; }


		public Point TopLeft
		{
			get
			{
				return new Point(this.Area.X, this.Area.Y);
			}
		}


		public Point TopRight
		{
			get
			{
				return new Point(this.Area.X + this.Area.Width , this.Area.Y);
			}
		}


		public Point BottomLeft
		{
			get
			{
				return new Point(this.Area.X, this.Area.Y + this.Area.Height);
			}
		}


		public Point BottomRight
		{
			get
			{
				return new Point(this.Area.X + this.Area.Width, this.Area.Y + this.Area.Height);
			}
		}


		public Point Center
		{
			get
			{
				return new Point((this.Area.Width + this.Area.X) / 2, (this.Area.Y + this.Area.Height) / 2);
			}
		}


		public Rectangle Area { get; set; }


		public bool IsLocked { get; set; }


		public MiniRegion(string name, int id, Rectangle area)
		{
			this.ID = id;
			this.Name = name;
			this.Area = area;
			this.IsLocked = false;
			this.AllowGroups = new List<string>();
			this.AllowUsers = new List<string>();
			this.Owners = new List<string>();
		}


		public void ShowFramework()
		{
			Vector2 width = new Vector2((float)((this.TopRight.X+1) * 16 - this.TopLeft.X * 16), (float)(this.TopRight.Y + 1) * 16 - this.TopLeft.Y * 16);
			Vector2 height = new Vector2(0f, (float)((this.BottomLeft.Y+1) * 16 - this.TopLeft.Y * 16));
			Vector2 center = new Vector2((float)(this.Area.Center.X * 16), (float)(this.Area.Center.Y * 16));
			int proj_ = Projectile.NewProjectile(new EntitySource_DebugCommand(), new Vector2((float)(this.TopLeft.X * 16), (float)(this.TopLeft.Y * 16)), width * 0.01f, 132, 0, 0f, 255, 0f, 0f);
			int proj_2 = Projectile.NewProjectile(new EntitySource_DebugCommand(), new Vector2((float)(this.TopLeft.X * 16), (float)(this.TopLeft.Y * 16)), height * 0.01f, 132, 0, 0f, 255, 0f, 0f);
			int proj_3 = Projectile.NewProjectile(new EntitySource_DebugCommand(), new Vector2((float)(this.BottomLeft.X * 16), (float)(this.BottomLeft.Y * 16)), width * 0.01f, 132, 0, 0f, 255, 0f, 0f);
			int proj_4 = Projectile.NewProjectile(new EntitySource_DebugCommand(), new Vector2((float)(this.TopRight.X * 16), (float)(this.TopRight.Y * 16)), height * 0.01f, 132, 0, 0f, 255, 0f, 0f);
			int proj_5 = Projectile.NewProjectile(new EntitySource_DebugCommand(), center, Vector2.Zero, 254, 0, 0f, 255, 0f, 0f);
			TSPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj_, 0f, 0f, 0f, 0);
			TSPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj_2, 0f, 0f, 0f, 0);
			TSPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj_3, 0f, 0f, 0f, 0);
			TSPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj_4, 0f, 0f, 0f, 0);
			TSPlayer.All.SendData(PacketTypes.ProjectileNew, "", proj_5, 0f, 0f, 0f, 0);
		}
		public bool Contain(Point point)
		{
			return Area.Contains(point);
		}
		public bool Contain(int x, int y)
		{
			return Area.Contains(x, y);
		}
		public void BuildFramework(int tileID) 
		{
            for (int i = TopLeft.X; i <= TopRight.X; i++)
            {
				for (int j = TopLeft.Y; j <= BottomLeft.Y; j++)
				{
					if (i > TopLeft.X && i < TopRight.X && j > TopLeft.Y && j < BottomLeft.Y) continue;
					WorldGen.PlaceTile(i,j,tileID);
				}
			}
			TSPlayer.All.SendTileRect((short)TopLeft.X, (short)TopLeft.Y,(byte)(Area.Width+3), (byte)(Area.Height+3));
			
		}
		public List<MiniRegion> Divide(int width,int height,int amount,int gap) 
		{
			int x = TopLeft.X;
			int y = TopLeft.Y;
			if (gap*amount-1+amount*width>Area.Width)return null;
			var regions = new List<MiniRegion>();
            for (int i = 0; i < amount; i++)
            {
				Rectangle area = new Rectangle(x,y,width,height);
				regions.Add(new MiniRegion(Name+$"_{i}",ID+i+1,area));
				x += gap + width+1 ;
            }
			return regions;
		}
	}
}
