﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Sockets;

namespace TeamProject
{
    internal static class DataSave
    {
        public static bool HowChoose(Scene scene)
        {
            bool choose = false;
            int key = 0;
            while (!choose)
            {
                int x = 70;
                int y = 7;
                scene.DrawStar();
                scene.SetCursorString(x, y++, "  게임 시작", false);
                scene.SetCursorString(x, ++y + 5, "0. 새로 시작", false);
                scene.SetCursorString(x, ++y + 7, "1. 불러 오기", false);
                key = scene.InputString(0, 1, 0, "원하는 행동을 선택해 주세요", x-6, ++y + 10);
                if (key == 1)
                {
                    choose = true;
                    try
                    {
                        LoadPlayer();
                    }
                    catch (Exception e)
                    {
                        scene.SetCursorString(x, ++y + 7, $"{e}", false);
                        scene.SetCursorString(x, ++y + 9, "저장된 정보가 없습니다", false);
                        Thread.Sleep(10000);
                        Console.Clear();
                        choose = false;
                    }
                }
                else if(key == 0)
                {
                    choose = true;
                }
            }
            if(key == 0)
            {
                choose = false;
            }
            return choose;
        }
        public static void SavePlayerInfo(Player player, int stage, int round)
        {
            player.Stage = stage;
            player.Round = round;
            Encoding encoding = Encoding.UTF8;
            string path = Pathes.SavePlayer();
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

            StreamWriter sw = new StreamWriter(fs, encoding);
        
            sw.WriteLine("Level,Name,Chrd,Exp,Gold,Atk,Def,Hp,MaxHp,Mp,MaxMp,stage,round");
            sw.WriteLine($"{player.Level},{player.Name},{player.Chrd},{player.Exp},{player.Gold},{player.Atk},{player.Def},{player.Hp},{player.MaxHp},{player.Mp},{player.MaxMp},{player.Stage},{player.Round}");
            sw.Close();
            fs.Close();

            path = Pathes.SaveItem();
            encoding = Encoding.UTF8;
            fs = new FileStream(path, FileMode.OpenOrCreate);
            sw = new StreamWriter(fs, encoding);

            sw.WriteLine("EqAtk,EqDef,EqHp,EqMp,Type,Price,Name,IsEquiped,Info");
            if(player.Inventory.Length> 0)
            {
                for(int i = 0; i < player.Inventory.Length; i++)
                {
                    sw.WriteLine($"{player.Inventory[i].EqAtk},{player.Inventory[i].EqDef},{player.Inventory[i].EqHP},{player.Inventory[i].EqMP},{player.Inventory[i].Type},{player.Inventory[i].Price},{player.Inventory[i].Name},{player.Inventory[i].IsEquiped},{player.Inventory[i].Info},");
                }
            }
            sw.Close();
            fs.Close();
        }
        public static Player LoadPlayer()
        {
            string playerPath = Pathes.SavePlayer();
            string[] playerData = File.ReadAllLines(playerPath, Encoding.UTF8);
            string[] propertyNames = playerData[0].Split(',');

            string[] itemEach = playerData[1].Split(",");
            string name = itemEach[Array.IndexOf(propertyNames, "Name")];
            string chrd = itemEach[Array.IndexOf(propertyNames, "Chrd")];
            int level = int.Parse(itemEach[Array.IndexOf(propertyNames, "Level")]);
            int exp = int.Parse(itemEach[Array.IndexOf(propertyNames, "Exp")]);
            int gold = int.Parse(itemEach[Array.IndexOf(propertyNames, "Gold")]);
            int atk = int.Parse(itemEach[Array.IndexOf(propertyNames, "Atk")]);
            int def = int.Parse(itemEach[Array.IndexOf(propertyNames, "Def")]);
            int hp = int.Parse(itemEach[Array.IndexOf(propertyNames, "Hp")]);
            int maxHp = int.Parse(itemEach[Array.IndexOf(propertyNames, "MaxHp")]);
            int mp = int.Parse(itemEach[Array.IndexOf(propertyNames, "Mp")]);
            int maxMp = int.Parse(itemEach[Array.IndexOf(propertyNames, "MaxMp")]);
            int stage = int.Parse(itemEach[Array.IndexOf(propertyNames, "stage")]);
            int round = int.Parse(itemEach[Array.IndexOf(propertyNames, "round")]);
            Item[] inventory = LoadItem();
            Item[] eqItem = new Item[2];
            for(int i = 0; i<inventory.Length; i++)
            {
                if (inventory[i].IsEquiped)
                {
                    eqItem[inventory[i].Type] = inventory[i];
                }
            }
            if(chrd == "전사")
            {
                Player player = new Warrior(level, hp, maxHp, mp, maxMp, atk, def, name, chrd, exp, gold, stage, round, inventory, eqItem);
                return player;
            }
            else if (chrd == "사제")
            {
                Player player = new Prist(level, hp, maxHp, mp, maxMp, atk, def, name, chrd, exp, gold, stage, round, inventory, eqItem);
                return player;
            }
            else if (chrd == "궁수")
            {
                Player player = new Archer(level, hp, maxHp, mp, maxMp, atk, def, name, chrd, exp, gold, stage, round, inventory, eqItem);
                return player;
            }
            return null;
        }
        public static Item[] LoadItem()
        {
            Item[] allItems = new Item[] { };

            string itemPath = Pathes.SaveItem();
            string[] itemData = File.ReadAllLines(itemPath, Encoding.UTF8);
            string[] propertyNames = itemData[0].Split(",");
            for (int i = 1; i < itemData.Length; i++)
            {
                int value;
                string[] itemEach = itemData[i].Split(",");
                if (int.TryParse(itemEach[0], out value))
                {
                    string name = itemEach[Array.IndexOf(propertyNames, "Name")];
                    string info = itemEach[Array.IndexOf(propertyNames, "Info")];
                    int type = int.Parse(itemEach[Array.IndexOf(propertyNames, "Type")]);
                    int eqAtk = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqAtk")]);
                    int eqDef = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqDef")]);
                    int eqHP = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqHp")]);
                    int eqMP = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqMp")]);
                    int price = int.Parse(itemEach[Array.IndexOf(propertyNames, "Price")]);
                    bool isEquiped = bool.Parse(itemEach[Array.IndexOf(propertyNames, "IsEquiped")]);
                    Item item = new Item(eqAtk, eqDef, eqHP, eqMP, type, price, name, info, isEquiped);

                    Array.Resize(ref allItems, allItems.Length + 1);
                    allItems[i - 1] = item;
                }
            }
            return allItems;
        }
        public static void ExitGame(Scene scene)
        {
            int x = 70;
            int y = 20;
            Console.Clear();
            scene.SetCursorString(x - 1, y - 2, "===================", false);
            scene.SetCursorString(x, y, "게임을 종료합니다", false);
            scene.SetCursorString(x - 1, y + 2, "===================", false);
            scene.DrawStar();
            try
            {
                SavePlayerInfo(scene._player, scene._stage, scene._round);
            }
            catch(Exception ex)
            {
                Console.Clear();
                scene.SetCursorString(x, y - 7, $"{ex}", false);
                scene.SetCursorString(x, y, "저장에 실패했습니다.", false);
            }
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
    }
}
