using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace MiniGamesAPI.Core
{
	public class MiniNpc
	{
		
		public NPC RealNpc
		{
			get
			{
				return this.realNpc;
			}
		}
		public bool Boss
		{
			get
			{
				return this.realNpc.boss;
			}
		}
		public bool Friendly
		{
			get
			{
				return this.realNpc.friendly;
			}
		}
		public bool TownNpc{get{return this.realNpc.townNPC;}}
		public List<MiniItem> Items { get; set; }
		public MiniNpc(NPC npc)
		{
			this.realNpc = npc;
			this.Items = new List<MiniItem>();
		}

		
		public void StoreSlot(int netid, int stack, byte prefix, int slot, bool sendData = false)
		{
		}

		
		private NPC realNpc;
	}
}
