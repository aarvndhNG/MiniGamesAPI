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
    public class MiniPlayer
    {
        public TSPlayer Player { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assistances { get; set; }
        
        public MiniPlayer(TSPlayer player) 
        {
            Player = player;
        }
        public override string ToString()
        {
            return Player.Name;
        }
        public virtual void Teleport(Point point) 
        {
            var x = Math.Max(0, point.X);
            var y = Math.Max(0, point.Y);
            x = Math.Min(x, Terraria.Main.maxTilesX - 1);
            y = Math.Min(y, Terraria.Main.maxTilesY - 1);
            Player.Teleport(x*16,y*16-48);
        }
        public virtual void SendInfoMessage(string msg) 
        {
            Player.SendMessage(msg,Color.DarkTurquoise);
        
        }
        public virtual void SendSuccessMessage(string msg) 
        {
            Player.SendMessage(msg,Color.MediumAquamarine);
        }
        public virtual void SendErrorMessage(string msg)
        {
            Player.SendMessage(msg,Color.Crimson);
        }
        public virtual void SetBuff(int type,int time=3600,bool bypass=false) 
        {
            Player.SetBuff(type,time,bypass);
        }
        public virtual void SetPVP(bool value)
        {
            Player.TPlayer.hostile = value;
            Player.SendData(PacketTypes.TogglePvp, "", Player.Index);
            TSPlayer.All.SendData(PacketTypes.TogglePvp, "", Player.Index);
        }
        public virtual void SetTeam(int id)
        {
            Player.SetTeam(id);
        }
        public virtual void RestorePlayerInv(MiniPack pack) {
            pack.RestoreCharacter(Player);
        }
        public virtual void DropItem(int itemID) 
        {
            int slot = Player.TPlayer.FindItem(itemID);
            var item = Player.TPlayer.inventory[slot];
            //下面是实现物品掉落
            var dropItem = Item.NewItem(Player.TPlayer.position,Player.TPlayer.width,Player.TPlayer.height,item.type,item.stack,true,item.prefix,false);
            TSPlayer.All.SendData(PacketTypes.ItemDrop,"",dropItem);

            //实现物品没收
            
            item.netID = 0;
            TSPlayer.All.SendData(PacketTypes.PlayerSlot, item.Name, Player.Index, slot, item.prefix);
        }
        public virtual bool CheckContainItem(int netid) 
        {
            return Player.TPlayer.HasItem(netid);
        }
        public float KDA() {
            float kd = Kills / Deaths;
            float kda = kd / Assistances;
            return kda;
        }
        public Vector2 ToAnotherPlayer(MiniPlayer player) {
            Vector2 velocity = player.Player.TPlayer.position - this.Player.TPlayer.position;
            return velocity;  
        }
        public bool ClearRecord() {
            Kills = 0;
            Deaths = 0;
            Assistances = 0;
            return true;
        }
    }
}
