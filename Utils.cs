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
    public static class Utils
    {
        public static readonly string EndLine_10 = new string('\n', 10);
        public static readonly string EndLine_15 = new string('\n', 15);
        public static readonly string EndLine_8 = new string('\n', 8);
        public static int DropItem(float x,float y,int netid,int stack,byte prefix) 
        {
            var tempItem = TShock.Utils.GetItemById(netid);
            int item = Item.NewItem((int)x,(int)y,tempItem.width,tempItem.height,netid,stack,false,prefix);
            TSPlayer.All.SendData(PacketTypes.ItemDrop,"",item);
            return item;
        }
        /// <summary>
        /// 比较点1是否和点2四周左边是否相同,适用宝石锁
        /// </summary>
        /// <param name="first">比较的点</param>
        /// <param name="main">被比较的点</param>
        /// <returns></returns>
        public static bool CompareOneAndTwo(Point first,Point main) 
        {
            if (main.X-1==first.X&&main.Y-1==first.Y)//1
            {
                return true;
            }
            if (main.X  == first.X && main.Y - 1 == first.Y)//2
            {
                return true;
            }
            if (main.X + 1 == first.X && main.Y - 1 == first.Y)//3
            {
                return true;
            }
            if (main.X - 1 == first.X && main.Y  == first.Y)//4
            {
                return true;
            }
            if (main.X  == first.X && main.Y== first.Y)//5
            {
                return true;
            }
            if (main.X + 1 == first.X && main.Y  == first.Y)//6
            {
                return true;
            }
            if (main.X - 1 == first.X && main.Y + 1 == first.Y)//7
            {
                return true;
            }
            if (main.X  == first.X && main.Y + 1 == first.Y)//8
            {
                return true;
            }
            if (main.X + 1 == first.X && main.Y + 1 == first.Y)//9
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// 清除世界内指定ID的物品，此ID非NetID
        /// </summary>
        /// <param name="id"></param>
        public static void ClearItem(int id) {
            Terraria.Main.item[id].active = false;
            NetMessage.SendData((int)PacketTypes.ItemDrop,-1,-1,null,id);
        }
        public static IEnumerable<float> TopTen(List<float> records) {
            return records.OrderByDescending(p=>p);        
        }
    }
}
