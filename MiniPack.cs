using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using TShockAPI;

namespace MiniGamesAPI
{
    public class MiniPack
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int UnlockedBiomeTorches { get; set; }
        public int HappyFunTorchTime { get; set; }
        public int UsingBiomeTorches { get; set; }
        public int QuestsCompleted { get; set; }
        public bool[] HideVisuals { get; set; }
        public Color? EyeColor { get; set; }
        public Color? SkinColor { get; set; }
        public Color? ShoeColor { get; set; }
        public Color? UnderShirtColor { get; set; }
        public Color? ShirtColor { get; set; }
        public Color? HairColor { get; set; }
        public Color? PantsColor { get; set; }
        public int? Hair { get; set; }
        public int? SkinVariant { get; set; }
        public int? ExtraSlots { get; set; }
        public int SpawnY { get; set; }
        public int SpawnX { get; set; }
        public bool Exists { get; set; }
        public int MaxMana { get; set; }
        public int Mana { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public byte HairDye { get; set; }
        public List<MiniItem> Items { get; set; }
        public MiniPack(string name, int id)
        {
            Name = name;
            ID = id;
            Hair = 0;
            SkinVariant = 0;
            ExtraSlots = 0;
            SpawnY = -1;
            SpawnX = -1;
            Exists = true;
            MaxMana = 20;
            Mana = 20;
            HP = 100;
            MaxHP = 100;
            HairDye = 0;
            HideVisuals = new bool[10];
            UnlockedBiomeTorches = 0;
            HappyFunTorchTime = 0;
            UsingBiomeTorches = 0;
            QuestsCompleted = 0;
            EyeColor = new Color(4283128425);
            SkinColor = new Color(4284120575);
            ShoeColor = new Color(4282149280);
            UnderShirtColor = new Color(4292326560);
            ShirtColor = new Color(4287407535);
            HairColor = new Color(4287407535);
            PantsColor = new Color(4287407535);
            Items = new List<MiniItem>();
        }
        public void RestoreCharacter(TSPlayer player)
        {
            player.IgnoreSSCPackets = true;

            player.TPlayer.statLife = this.HP;
            player.TPlayer.statLifeMax = this.MaxHP;
            player.TPlayer.statMana = this.Mana;
            player.TPlayer.statManaMax = this.MaxMana;
            player.TPlayer.SpawnX = this.SpawnX;
            player.TPlayer.SpawnY = this.SpawnY;
            player.sX = this.SpawnX;
            player.sY = this.SpawnY;
            player.TPlayer.hairDye = this.HairDye;
            player.TPlayer.anglerQuestsFinished = this.QuestsCompleted;

            if (ExtraSlots != null)
                player.TPlayer.extraAccessory = ExtraSlots.Value == 1 ? true : false;
            if (this.SkinVariant != null)
                player.TPlayer.skinVariant = this.SkinVariant.Value;
            if (this.Hair != null)
                player.TPlayer.hair = this.Hair.Value;
            if (this.HairColor != null)
                player.TPlayer.hairColor = this.HairColor.Value;
            if (this.PantsColor != null)
                player.TPlayer.pantsColor = this.PantsColor.Value;
            if (this.ShirtColor != null)
                player.TPlayer.shirtColor = this.ShirtColor.Value;
            if (this.UnderShirtColor != null)
                player.TPlayer.underShirtColor = this.UnderShirtColor.Value;
            if (this.ShoeColor != null)
                player.TPlayer.shoeColor = this.ShoeColor.Value;
            if (this.SkinColor != null)
                player.TPlayer.skinColor = this.SkinColor.Value;
            if (this.EyeColor != null)
                player.TPlayer.eyeColor = this.EyeColor.Value;

            if (this.HideVisuals != null)
                player.TPlayer.hideVisibleAccessory = this.HideVisuals;
            else
                player.TPlayer.hideVisibleAccessory = new bool[player.TPlayer.hideVisibleAccessory.Length];

            //此循环用来清空玩家背包
            for (int j = 0; j < NetItem.MaxInventory; j++)
            {
                if (j < 59)
                {
                    //0-58
                    player.TPlayer.inventory[j].netDefaults(0);
                }
                else if (j < 79)
                {
                    //59-78
                    var index = j - NetItem.ArmorIndex.Item1;
                    player.TPlayer.armor[index].netDefaults(0);
                }
                else if (j < 89)
                {
                    //79-88
                    var index = j - NetItem.DyeIndex.Item1;
                    player.TPlayer.dye[index].netDefaults(0);
                }
                else if (j < 94)
                {
                    //89-93
                    var index = j - NetItem.MiscEquipIndex.Item1;
                    player.TPlayer.miscEquips[index].netDefaults(0);
                }
                else if (j < 99)
                {
                    //94-98
                    var index = j - NetItem.MiscDyeIndex.Item1;
                    player.TPlayer.miscDyes[index].netDefaults(0);
                }
                else if (j < 139)
                {
                    //99-138
                    var index = j - NetItem.PiggyIndex.Item1;
                    player.TPlayer.bank.item[index].netDefaults(0);
                }
                else if (j < 179)
                {
                    //139-178
                    var index = j - NetItem.SafeIndex.Item1;
                    player.TPlayer.bank2.item[index].netDefaults(0);
                }
                else if (j < 220)
                {
                    //179-219
                    if (j==179)
                    {
                        player.TPlayer.trashItem.netDefaults(0);
                        continue;
                    }
                    var index = j - NetItem.ForgeIndex.Item1;
                    player.TPlayer.bank3.item[index].netDefaults(0);
                   
                }
                else
                {
                    //220
                    var index = j - NetItem.VoidIndex.Item1;
                    player.TPlayer.bank4.item[index].netDefaults(0);
                }
            }

                //此循环用来加载玩家背包
                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];
                    Item trItem = TShock.Utils.GetItemById(item.NetID);
                    trItem.stack = item.Stack;
                    trItem.prefix = item.Prefix;
                    if (item.Slot >= 0 && item.Slot <= 58)
                    {
                        player.TPlayer.inventory[item.Slot] = trItem;
                    }
                    else if (item.Slot >= 59 && item.Slot <= 78)//饰品栏+时装栏
                    {
                        player.TPlayer.armor[item.Slot - 59] = trItem;

                    }
                    else if (item.Slot >= 79 && item.Slot <= 88)//时装染色栏
                    {
                        player.TPlayer.dye[item.Slot - 79] = trItem;

                    }
                    else if (item.Slot >= 89 && item.Slot <= 93)//配件栏
                    {
                        player.TPlayer.miscEquips[item.Slot - 89] = trItem;

                    }
                    else if (item.Slot >= 94 && item.Slot <= 98)//配件染色栏
                    {
                        player.TPlayer.miscDyes[item.Slot - 94] = trItem;

                    }
                    else if (item.Slot >= 99 && item.Slot <= 138)//猪猪
                    {
                        player.TPlayer.bank.item[item.Slot - 99] = trItem;

                    }
                    else if (item.Slot >= 139 && item.Slot <= 178)//保险箱
                    {
                        player.TPlayer.bank2.item[item.Slot - 139] = trItem;

                    }
                    else if (item.Slot >= 180 && item.Slot <= 219)//护卫熔炉
                    {
                        player.TPlayer.bank3.item[item.Slot - 180] = trItem;

                    }
                    else if (item.Slot >= 220 && item.Slot <= 259)//虚空袋子
                    {
                        player.TPlayer.bank4.item[item.Slot - 220] = trItem;

                    }
                    else
                    {
                        player.TPlayer.trashItem = trItem;
                    }
                }

                float slot = 0f;
                for (int k = 0; k < NetItem.InventorySlots; k++)
                {
                    NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].inventory[k].Name), player.Index, slot, (float)Main.player[player.Index].inventory[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.ArmorSlots; k++)
                {
                    NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].armor[k].Name), player.Index, slot, (float)Main.player[player.Index].armor[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.DyeSlots; k++)
                {
                    NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].dye[k].Name), player.Index, slot, (float)Main.player[player.Index].dye[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.MiscEquipSlots; k++)
                {
                    NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].miscEquips[k].Name), player.Index, slot, (float)Main.player[player.Index].miscEquips[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.MiscDyeSlots; k++)
                {
                    NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].miscDyes[k].Name), player.Index, slot, (float)Main.player[player.Index].miscDyes[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.PiggySlots; k++)
                {
                    NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].bank.item[k].Name), player.Index, slot, (float)Main.player[player.Index].bank.item[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.SafeSlots; k++)
                {
                    NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].bank2.item[k].Name), player.Index, slot, (float)Main.player[player.Index].bank2.item[k].prefix);
                    slot++;
                }
                NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].trashItem.Name), player.Index, slot++, (float)Main.player[player.Index].trashItem.prefix);
                for (int k = 0; k < NetItem.ForgeSlots; k++)
                {
                    NetMessage.SendData(5, -1, -1, NetworkText.FromLiteral(Main.player[player.Index].bank3.item[k].Name), player.Index, slot, (float)Main.player[player.Index].bank3.item[k].prefix);
                    slot++;
                }


                NetMessage.SendData(4, -1, -1, NetworkText.FromLiteral(player.Name), player.Index, 0f, 0f, 0f, 0);
                NetMessage.SendData(42, -1, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0);
                NetMessage.SendData(16, -1, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0);

                slot = 0f;
                for (int k = 0; k < NetItem.InventorySlots; k++)
                {
                    NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].inventory[k].Name), player.Index, slot, (float)Main.player[player.Index].inventory[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.ArmorSlots; k++)
                {
                    NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].armor[k].Name), player.Index, slot, (float)Main.player[player.Index].armor[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.DyeSlots; k++)
                {
                    NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].dye[k].Name), player.Index, slot, (float)Main.player[player.Index].dye[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.MiscEquipSlots; k++)
                {
                    NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].miscEquips[k].Name), player.Index, slot, (float)Main.player[player.Index].miscEquips[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.MiscDyeSlots; k++)
                {
                    NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].miscDyes[k].Name), player.Index, slot, (float)Main.player[player.Index].miscDyes[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.PiggySlots; k++)
                {
                    NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].bank.item[k].Name), player.Index, slot, (float)Main.player[player.Index].bank.item[k].prefix);
                    slot++;
                }
                for (int k = 0; k < NetItem.SafeSlots; k++)
                {
                    NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].bank2.item[k].Name), player.Index, slot, (float)Main.player[player.Index].bank2.item[k].prefix);
                    slot++;
                }
                NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].trashItem.Name), player.Index, slot++, (float)Main.player[player.Index].trashItem.prefix);
                for (int k = 0; k < NetItem.ForgeSlots; k++)
                {
                    NetMessage.SendData(5, player.Index, -1, NetworkText.FromLiteral(Main.player[player.Index].bank3.item[k].Name), player.Index, slot, (float)Main.player[player.Index].bank3.item[k].prefix);
                    slot++;
                }



                NetMessage.SendData(4, player.Index, -1, NetworkText.FromLiteral(player.Name), player.Index, 0f, 0f, 0f, 0);
                NetMessage.SendData(42, player.Index, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0);
                NetMessage.SendData(16, player.Index, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0);

                for (int k = 0; k < 22; k++)
                {
                    player.TPlayer.buffType[k] = 0;
                }

                /*
                 * The following packets are sent twice because the server will not send a packet to a client
                 * if they have not spawned yet if the remoteclient is -1
                 * This is for when players login via uuid or serverpassword instead of via
                 * the login command.
                 */
                NetMessage.SendData(50, -1, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0);
                NetMessage.SendData(50, player.Index, -1, NetworkText.Empty, player.Index, 0f, 0f, 0f, 0);

                NetMessage.SendData(76, player.Index, -1, NetworkText.Empty, player.Index);
                NetMessage.SendData(76, -1, -1, NetworkText.Empty, player.Index);

                NetMessage.SendData(39, player.Index, -1, NetworkText.Empty, 400);

        }
        public void CopyFromPlayer(TSPlayer plr)
        {
            if (plr == null) return;
            CopyFromPlayer(plr.TPlayer);

        }
        public void CopyFromPlayer(Terraria.Player plr)
        {
            if (plr == null) return;
            this.MaxHP = plr.statLifeMax;
            this.HP = plr.statLife;
            this.Mana = plr.statMana;
            this.MaxMana = plr.statManaMax;
            this.ExtraSlots = plr.extraAccessorySlots;
            this.EyeColor = plr.eyeColor;
            this.Hair = plr.hair;
            this.HairColor = plr.hairColor;
            this.HairDye = plr.hairDye;
            this.HappyFunTorchTime = plr.happyFunTorchTime.GetHashCode();
            this.HideVisuals = plr.hideVisibleAccessory;
            this.PantsColor = plr.pantsColor;
            this.ShoeColor = plr.shoeColor;
            this.SkinColor = plr.skinColor;
            this.ShirtColor = plr.shirtColor;
            this.SkinVariant = plr.skinVariant;
            this.SpawnX = plr.SpawnX;
            this.SpawnY = plr.SpawnY;
            this.UnderShirtColor = plr.underShirtColor;
            this.UnlockedBiomeTorches = plr.unlockedBiomeTorches.GetHashCode();
            this.UsingBiomeTorches = plr.UsingBiomeTorches.GetHashCode();
            this.Exists = true;

            for (int i = 0; i < 59; i++)
            {
                var tritem = plr.inventory[i];
                if (tritem.netID==0)
                {
                    continue;
                }
                MiniItem item = new MiniItem(i,tritem.prefix,tritem.netID,tritem.stack);
                Items.Add(item);
            }
            for (int i = 0; i < NetItem.ArmorSlots; i++)
            {
                var tritem = plr.armor[i];
                if (tritem.netID == 0)
                {
                    continue;
                }
                MiniItem item = new MiniItem(i+59, tritem.prefix, tritem.netID, tritem.stack);
                Items.Add(item);
            }
            for (int i = 0; i < NetItem.DyeSlots; i++)
            {
                var tritem = plr.dye[i];
                if (tritem.netID == 0)
                {
                    continue;
                }
                MiniItem item = new MiniItem(i+79, tritem.prefix, tritem.netID, tritem.stack);
                Items.Add(item);
            }
            for (int i = 0; i < NetItem.MiscEquipSlots; i++)
            {
                var tritem = plr.miscEquips[i];
                if (tritem.netID == 0)
                {
                    continue;
                }
                MiniItem item = new MiniItem(i + 89, tritem.prefix, tritem.netID, tritem.stack);
                Items.Add(item);
            }
            for (int i = 0; i < NetItem.MiscDyeSlots; i++)
            {
                var tritem = plr.miscDyes[i];
                if (tritem.netID == 0)
                {
                    continue;
                }
                MiniItem item = new MiniItem(i + 94, tritem.prefix, tritem.netID, tritem.stack);
                Items.Add(item);
            }
            for (int i = 0; i < NetItem.PiggySlots; i++)
            {
                var tritem = plr.bank.item[i];
                if (tritem.netID == 0)
                {
                    continue;
                }
                MiniItem item = new MiniItem(i + 99, tritem.prefix, tritem.netID, tritem.stack);
                Items.Add(item);
            }
            for (int i = 0; i < NetItem.SafeSlots; i++)
            {
                var tritem = plr.bank2.item[i];
                if (tritem.netID == 0)
                {
                    continue;
                }
                MiniItem item = new MiniItem(i + 139, tritem.prefix, tritem.netID, tritem.stack);
                Items.Add(item);
            }
            for (int i = 0; i < NetItem.ForgeSlots; i++)
            {
                var tritem = plr.bank3.item[i];
                if (tritem.netID == 0)
                {
                    continue;
                }
                MiniItem item = new MiniItem(i + 180, tritem.prefix, tritem.netID, tritem.stack);
                Items.Add(item);
            }
            for (int i = 0; i < NetItem.VoidSlots; i++)
            {
                var tritem = plr.bank4.item[i];
                if (tritem.netID == 0)
                {
                    continue;
                }
                MiniItem item = new MiniItem(i + 220, tritem.prefix, tritem.netID, tritem.stack);
                Items.Add(item);
            }
            Items.Add(new MiniItem(179,plr.trashItem.prefix,plr.trashItem.netID,plr.trashItem.stack));
        }
        public void RestoreCharacter(MiniPlayer plr)
        {
            if (plr == null) return;
            RestoreCharacter(plr.Player);
        }
    }
}

