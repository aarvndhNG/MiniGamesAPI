using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using TShockAPI;

namespace MiniGamesAPI
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
				return new Point(this.Area.X + this.Area.Width + 1, this.Area.Y);
			}
		}

		
		public Point BottomLeft
		{
			get
			{
				return new Point(this.Area.X, this.Area.Y + this.Area.Height + 1);
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
			Vector2 width = new Vector2((float)(this.TopRight.X * 16 - this.TopLeft.X * 16), (float)(this.TopRight.Y * 16 - this.TopLeft.Y * 16));
			Vector2 height = new Vector2(0f, (float)(this.BottomLeft.Y * 16 - this.TopLeft.Y * 16));
			Vector2 center = new Vector2((float)(this.Area.Center.X * 16), (float)(this.Area.Center.Y * 16));
			int proj_ = Projectile.NewProjectile(new ProjectileSource_BySourceId(116), new Vector2((float)(this.TopLeft.X * 16), (float)(this.TopLeft.Y * 16)), width * 0.01f, 132, 0, 0f, 255, 0f, 0f);
			int proj_2 = Projectile.NewProjectile(new ProjectileSource_BySourceId(116), new Vector2((float)(this.TopLeft.X * 16), (float)(this.TopLeft.Y * 16)), height * 0.01f, 132, 0, 0f, 255, 0f, 0f);
			int proj_3 = Projectile.NewProjectile(new ProjectileSource_BySourceId(116), new Vector2((float)(this.BottomLeft.X * 16), (float)(this.BottomLeft.Y * 16)), width * 0.01f, 132, 0, 0f, 255, 0f, 0f);
			int proj_4 = Projectile.NewProjectile(new ProjectileSource_BySourceId(116), new Vector2((float)(this.TopRight.X * 16), (float)(this.TopRight.Y * 16)), height * 0.01f, 132, 0, 0f, 255, 0f, 0f);
			int proj_5 = Projectile.NewProjectile(new ProjectileSource_BySourceId(116), center, Vector2.Zero, 254, 0, 0f, 255, 0f, 0f);
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
		public bool Contain(int x,int y)
		{
			return Area.Contains(x,y);
		}
	}
}
