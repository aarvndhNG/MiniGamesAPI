using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;

namespace MiniGamesAPI
{
    public class MiniPack
    {
        [JsonIgnore]
        public PlayerData Data { get; set; }
        public string DataName { get; set; }
        public int ID { get; set; }
        public int UnlockedBiomeTorches { get { return Data.unlockedBiomeTorches; } set { Data.unlockedBiomeTorches = value; } }
        public int HappyFunTorchTime { get { return Data.happyFunTorchTime; } set { Data.happyFunTorchTime = value; } }
        public int UsingBiomeTorches { get { return Data.usingBiomeTorches; } set { Data.usingBiomeTorches = value; } }
        public int QuestsCompleted { get { return Data.questsCompleted; } set { Data.questsCompleted = value; } }
        public bool[] HideVisuals { get { return Data.hideVisuals; } set { Data.hideVisuals = value; } }
        public Color? EyeColor { get { return Data.eyeColor; } set { Data.eyeColor = value; } }
        public Color? SkinColor { get { return Data.skinColor; } set { Data.skinColor = value; } }
        public Color? ShoeColor { get { return Data.shoeColor; } set { Data.shoeColor = value; } }
        public Color? UnderShirtColor { get { return Data.underShirtColor; } set { Data.underShirtColor = value; } }
        public Color? ShirtColor { get { return Data.shirtColor; } set { Data.shirtColor = value; } }
        public Color? HairColor { get { return Data.shirtColor; } set { Data.shirtColor = value; } }
        public Color? PantsColor { get { return Data.shirtColor; } set { Data.shirtColor = value; } }
        public int? Hair { get { return Data.hair; } set { Data.hair = value; } }
        public int? SkinVariant { get { return Data.skinVariant; } set { Data.skinVariant = value; } }
        public int? ExtraSlots { get { return Data.extraSlot; } set { Data.extraSlot = value; } }
        public int SpawnY { get { return Data.spawnY; } set { Data.spawnY = value; } }
        public int SpawnX { get { return Data.spawnX; } set { Data.spawnX = value; } }
        public bool Exists { get { return Data.exists; } set { Data.exists = value; } }
        public int MaxMana { get { return Data.maxMana; } set { Data.maxMana = value; } }
        public int Mana { get { return Data.mana; } set { Data.mana = value; } }
        public int MaxHP { get { return Data.maxHealth; } set { Data.maxHealth = value; } }
        public int HP { get { return Data.health; } set { Data.health = value; } }
        public byte HairDye { get { return Data.hairDye; } set { Data.hairDye = value; } }
        public List<MiniItem> Items = new List<MiniItem>();
        public MiniPack(PlayerData data, int id, string dataName)
        {
            ID = id;
            DataName = dataName;
            Data = TShock.CharacterDB.GetPlayerData(new TSPlayer(TShock.UserAccounts.GetUserAccountByName(DataName).ID), TShock.UserAccounts.GetUserAccountByName(DataName).ID);
            //Data = new PlayerData() { };
            if (Data.inventory.Length != 0)
            {
                for (int i = 0; i < Data.inventory.Length; i++)
                {
                    if (Data.inventory[i].NetId != 0)
                    {
                        var item = Data.inventory[i];
                        Items.Add(new MiniItem(i, item.PrefixId, item.NetId, item.Stack));
                    }
                }
            }
        }
        public void RestoreCharacter(TSPlayer player)
        {
            if (Items.Count != 0)
            {
                Data.inventory = new NetItem[NetItem.MaxInventory];
                for (int i = 0; i < Items.Count; i++)
                {
                    Data.inventory[Items[i].Slot] = Items[i].ToNetItem();
                    if (i == Items.Count - 1)
                    {
                        Data.RestoreCharacter(player);
                        break;
                    }
                }
            }

        }
    }
    public class MiniItem
    {
        public int Slot { get; set; }
        public byte Prefix { get; set; }
        public int NetID { get; set; }
        public int Stack { get; set; }
        public MiniItem(int slot, byte prefix, int netid, int stack)
        {
            Slot = slot;
            Prefix = prefix;
            NetID = netid;
            Stack = stack;
        }
        public NetItem ToNetItem()
        {
            var item = new NetItem(NetID, Stack, Prefix);
            return item;
        }
    }
}

